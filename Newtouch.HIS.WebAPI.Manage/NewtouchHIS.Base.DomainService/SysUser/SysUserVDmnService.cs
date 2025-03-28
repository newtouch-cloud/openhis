using NewtouchHIS.Base.Domain;
using NewtouchHIS.Base.Domain.Entity;
using NewtouchHIS.Base.Domain.EnumExtend;
using NewtouchHIS.Base.Domain.Organize;
using NewtouchHIS.Base.Domain.ValueObjects;
using NewtouchHIS.Lib.Base;
using NewtouchHIS.Lib.Base.Exceptions;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.Base.Security;
using NewtouchHIS.Lib.DataBaseSvr;
using SqlSugar;

namespace NewtouchHIS.Base.DomainService
{
    /// <summary>
    /// 系统用户管理（query）
    /// </summary>
    public class SysUserVDmnService : BaseDmnService<SysUserVEntity>, ISysUserVDmnService
    {
        private readonly ISysStaffDmnService _sysStaffDmn;
        private readonly ISysDepartmentDmnService _sysDepartmentDmn;
        private readonly ISysRoleDmnService _roleDmn;
        /// <summary>
        /// 应用需配置主库
        /// </summary>
        private string _mainDB = ConfigInitHelper.DbConfig.MainDB ?? throw new FailedException(ResponseResultCode.FAILOfConfigInit, "DbConfig.MainDB");

        public SysUserVDmnService(ISysStaffDmnService sysStaffDmn, ISysDepartmentDmnService sysDepartmentDmn, ISysRoleDmnService roleDmn)
        {
            base.Context = SqlSugarDbContext.Db.GetConnection(_mainDB);
            _sysStaffDmn = sysStaffDmn;
            _sysDepartmentDmn = sysDepartmentDmn;
            _roleDmn = roleDmn;
        }        

        #region login
        public async Task<BusResult<SysUserVEntity>> UserLogin(LoginBO login)
        {
            if (login == null || string.IsNullOrWhiteSpace(login.Username) || string.IsNullOrWhiteSpace(login.Password))
            {
                return new BusResult<SysUserVEntity> { code = ResponseResultCode.FAIL, msg = "用户名密码不可为空" };
            }
            if (sysConfig == null || string.IsNullOrWhiteSpace(sysConfig.Top_OrganizeId))
            {
                return new BusResult<SysUserVEntity> { code = ResponseResultCode.FAIL, msg = "请配置顶级机构信息" };
            }
            if (login.Username == "root" && CheckRootPwd(login.Password))
            {
                return new BusResult<SysUserVEntity>
                {
                    code = ResponseResultCode.SUCCESS,
                    Data = new SysUserVEntity()
                    {
                        Id = "root",
                        Account = "root"
                    }
                };
            }
            var user = await GetFirstOrDefaultWithAttr<SysUserVEntity>(p => p.TopOrganizeId == sysConfig.Top_OrganizeId && p.zt == "1" && p.Account == login.Username);
            if (user == null)
            {
                return new BusResult<SysUserVEntity> { code = ResponseResultCode.FAIL, msg = "未找到用户信息" };
            }

            if (user.Locked.HasValue && user.Locked.Value)
            {
                //不提供 自动解锁 功能
                return new BusResult<SysUserVEntity> { code = ResponseResultCode.FAIL, msg = string.Format("密码连续输错超过{0}次,账号已被锁定", sysConfig.LoginFailedTimesLimit_Count) };
            }
            string dbPassword = MD5Encrypt.Md5(DESEncrypt.Encrypt(login.Password.ToLower(), user.UserSecretkey).ToLower(), 32).ToLower();
            if (dbPassword != user.UserPassword)
            {
                return new BusResult<SysUserVEntity> { code = ResponseResultCode.FAILOfPwdError, msg = "密码不正确，请重新输入" };
            }

            return new BusResult<SysUserVEntity> { code = ResponseResultCode.SUCCESS, Data = new SysUserVEntity { Id = user.Id, Account = user.Account, TopOrganizeId = user.TopOrganizeId } };
        }

        public async Task<OperatorModel?> BuildLoginStatusOpr(string userId, string staffOrganizeId = null)
        {
            SysUserVEntity user = new SysUserVEntity();
            if (userId == "root")
            {
                user.Id = userId;
                user.Account = userId;
                user.TopOrganizeId = staffOrganizeId;
            }
            else
            {
                user = await GetFirstOrDefaultWithAttr<SysUserVEntity>(p => p.Id == userId);
                if (user == null)
                {
                    return null;
                }
            }

            //登录成功，构造系统身份信息
            var logEntity = new SysUserLoginLog
            {
                Id = Guid.NewGuid().ToString(),
                TopOrganizeId = sysConfig.Top_OrganizeId ?? throw new FailedException(ResponseResultCode.FAILOfConfigInit, "Top_OrganizeId"),
                Date = DateTime.Now,
                CreateTime = DateTime.Now,
                ModuleName = "系统登录",
                Type = DbLogType.Login.ToString(),
                Account = userId == "root" ? userId : user?.Account,
            };

            return await BuildLoginStatusOpr(user, logEntity, staffOrganizeId);
        }
        /// <summary>
        /// 构造登录身份（前面身份已经验证通过了）
        /// </summary>
        /// <param name="userEntity"></param>
        /// <param name="logEntity"></param>
        /// <param name="staffOrganizeId">指定了OrganizeId</param>
        public async Task<OperatorModel?> BuildLoginStatusOpr(SysUserVEntity userEntity, SysUserLoginLog logEntity, string staffOrganizeId = null)
        {
            SysStaffVEntity? staff = null;
            bool writedNeedChooseOrgFlag = false;
            OperatorModel? operatorModel = null;
            if (userEntity != null)
            {
                IList<SysStaffVEntity>? staffList = null;
                if (userEntity.Account != "root" && userEntity.Account != "admin")
                {
                    //root admin 不可以关联医院Id，建立了关联也无视
                    staffList = await _sysStaffDmn.GetStaffListByUserId(userEntity.Id);
                    if (!string.IsNullOrWhiteSpace(staffOrganizeId))
                    {
                        staffList = staffList.Where(p => p.OrganizeId == staffOrganizeId).ToList();
                    }
                    if (staffList.Count > 1)
                    {
                        //主页需要做Org选择
                        writedNeedChooseOrgFlag = true;
                    }
                    else if (staffList.Count == 0)
                    {
                        throw new FailedException("登录失败，尚未关联系统人员");
                    }
                }
                staff = staffList != null && staffList.Count == 1 ? staffList[0] : null;
                operatorModel = new OperatorModel()
                {
                    UserId = userEntity.Id,
                    UserCode = userEntity.Account,
                    UserName = staff == null ? null : staff.Name,
                    DepartmentCode = staff == null ? null : staff.DepartmentCode,
                    OrganizeId = staff == null ? null : staff.OrganizeId,
                    LoginIPAddress = "",
                    LoginTime = DateTime.Now,
                    StaffId = staff == null ? null : staff.Id,
                    rygh = staff == null ? null : staff.gh,
                    TopOrganizeId = sysConfig.Top_OrganizeId,
                    AppId = sysConfig.AppId,
                    DepartmentName = staff == null ? null : await _sysDepartmentDmn.GetNameByCode(staff.DepartmentCode, staff.OrganizeId),
                    NeedChooseOrgFlag = writedNeedChooseOrgFlag
                };

                if (userEntity.Account == "root")
                {
                    operatorModel.IsRoot = true;
                }
                else if (userEntity.Account == "admin")
                {
                    operatorModel.IsAdministrator = true;
                }
                else
                {
                    var roleList = await _roleDmn.GetUserRoleList(userEntity.Id, operatorModel.OrganizeId);
                    operatorModel.RoleIdList = roleList.Select(p => p.Id).ToList();
                    operatorModel.IsHospAdministrator = roleList.Any(p => p.Code == "HospAdministrator");
                }

                logEntity.NickName = staff == null ? null : staff.Name;
                logEntity.Result = true;
                logEntity.Description = "系统登录成功";
                //关联多组织机构时 不准确
                logEntity.OrganizeId = staff == null ? null : staff?.OrganizeId;
            }
            return operatorModel;
        }
        #endregion

        #region private
        /// <summary>
        /// root登录（特例），验证密码
        /// 通过返回true，否则返回false
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        private bool CheckRootPwd(string password)
        {
            return password == MD5Encrypt.Md5(DateTime.Now.ToString("HHMMdd"), 32).ToLower();
        }

        #endregion



    }
}
;