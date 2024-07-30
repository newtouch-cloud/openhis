using Mapster;
using NewtouchHIS.Base.Domain.Entity;
using NewtouchHIS.Base.Domain.EnumExtend;
using NewtouchHIS.Base.Domain.IDomainService;
using NewtouchHIS.Base.Domain.Organize;
using NewtouchHIS.Base.Domain.ValueObjects;
using NewtouchHIS.Lib.Base;
using NewtouchHIS.Lib.Base.EnumExtend;
using NewtouchHIS.Lib.Base.Exceptions;
using NewtouchHIS.Lib.Base.Extension;
using NewtouchHIS.Lib.Base.Model;
using NewtouchHIS.Lib.DataBaseSvr;
using SqlSugar;

namespace NewtouchHIS.Base.DomainService.SysManage
{
    /// <summary>
    /// 系统菜单
    /// 各系统通用功能，DBContext选择为主库,建议引用
    /// </summary>
    public class SysModuleDmnService : BaseDmnService<SysModuleEntity>, ISysModuleDmnService
    {
        private readonly ISysRoleDmnService _roleDmn;
        /// <summary>
        /// 应用需配置主库
        /// </summary>
        private string _mainDB = ConfigInitHelper.DbConfig.MainDB ?? throw new FailedException(ResponseResultCode.FAILOfConfigInit, "DbConfig.MainDB");
        public SysModuleDmnService(ISysRoleDmnService roleDmn)
        {
            _roleDmn = roleDmn;
            base.Context = SqlSugarDbContext.Db.GetConnection(_mainDB);
        }

        public async Task<SysModuleEntity?> GetEntity(string id)
        {
            return await FindKey(id);
        }
        public async Task<List<SysModuleEntity>> GetEntitybyorgId(string orgId)
        {
            return await GetByWhere<SysModuleEntity>(DBEnum.UnionDb.ToString(), p => p.zt == "1" && p.OrganizeId == orgId);
        }
        public async Task<SysModuleEntity?> GetEntity(string id, bool valid = true)
        {
            var ety = await GetByWhere(p => p.Id == id);
            if (valid && ety != null)
            {
                return ety.Where(p => p.zt == "1").FirstOrDefault();
            }
            return ety?.FirstOrDefault();
        }

        public async Task<BusResult<string>> AddRange(List<SysModuleVO> vo, string user, string orgId, string defaultParentId)
        {
            if (string.IsNullOrWhiteSpace(user) || string.IsNullOrWhiteSpace(orgId))
            {
                return new BusResult<string> { code = ResponseResultCode.FAIL, msg = "机构信息及用户信息不可为空" };
            }
            if (!(await MenuEditAuthcation(user, orgId)))
            {
                return new BusResult<string> { code = ResponseResultCode.FAIL, msg = "操作员权限不足" };
            }
            //查找重复菜单
            var menuNames = vo.Select(p => p.Name);
            var existsMenu = await GetByWhere(p => menuNames.Contains(p.Name) && p.OrganizeId == orgId && p.zt == "1");
            if (existsMenu != null && existsMenu.Count() > 0)
            {
                return new BusResult<string> { code = ResponseResultCode.FAIL, msg = $"菜单已存在：{existsMenu.Select(p => p.Name).ToList().ToJson()}" };
            }
            var parentMenu = vo.Where(p => string.IsNullOrWhiteSpace(p.ParentId)).ToList();
            if (parentMenu != null && parentMenu.Count() > 0)
            {
                var pEntityList = parentMenu.Adapt<List<SysModuleEntity>>();
                foreach (var p in pEntityList)
                {
                    p.NewEntity(orgId, user);
                    p.Id = Guid.NewGuid().ToString();
                    p.Target = p.Target ?? EnumModuleTargetType.expand.ToString();
                }
                var parentSync = await AddRange(pEntityList);
                if (parentSync <= 0)
                {
                    return new BusResult<string> { code = ResponseResultCode.FAIL, msg = "父级菜单添加失败" };
                }
            }
            //子菜单
            var childMenu = vo.Where(p => !string.IsNullOrWhiteSpace(p.ParentId)).ToList();
            if (childMenu != null && childMenu.Count() > 0)
            {
                var cEntityList = childMenu.Adapt<List<SysModuleEntity>>();
                foreach (var c in cEntityList)
                {
                    var thisParent = vo.FirstOrDefault(a => a.Id == c.ParentId);
                    if (thisParent != null)
                    {
                        c.ParentId = parentMenu?.FirstOrDefault(a => a.Name == thisParent.Name)?.Id ?? defaultParentId;
                    }
                    else
                    {
                        c.ParentId = defaultParentId ?? c.ParentId;
                    }

                    c.NewEntity(orgId, user);
                    c.Id = Guid.NewGuid().ToString();
                    c.Target = c.Target ?? EnumModuleTargetType.iframe.ToString();
                }
                var childSync = await AddRange(cEntityList);
                if (childSync <= 0)
                {
                    return new BusResult<string> { code = ResponseResultCode.FAIL, msg = "菜单添加失败" };
                }
            }
            return new BusResult<string> { code = ResponseResultCode.SUCCESS };
        }
        public async Task<BusResult<string>> AddEntity(SysModuleVO vo, string user, string orgId)
        {
            if (string.IsNullOrWhiteSpace(user) || string.IsNullOrWhiteSpace(orgId))
            {
                return new BusResult<string> { code = ResponseResultCode.FAIL, msg = "机构信息及用户信息不可为空" };
            }
            //获取用户权限
            var userEty = await GetByWhereWithAttr<SysUserEntity>(p => p.Account == user && p.TopOrganizeId == sysConfig.Top_OrganizeId && p.zt == "1");
            if (userEty == null || userEty.Count != 1)
            {
                return new BusResult<string> { code = ResponseResultCode.FAIL, msg = "用户账号异常" };
            }
            string userId = userEty.FirstOrDefault()?.Id;
            var isAdmin = await _roleDmn.IsHospAdmin(userId, orgId);
            if (!isAdmin)
            {
                return new BusResult<string> { code = ResponseResultCode.FAIL, msg = "菜单操作未授权" };
            }
            if (string.IsNullOrWhiteSpace(vo.Name) || string.IsNullOrWhiteSpace(vo.Target))
            {
                return new BusResult<string> { code = ResponseResultCode.FAILOfEmpty, msg = "菜单信息不完整：名称及目标不可为空" };
            }
            var exists = await GetByWhere(p => p.Name == vo.Name && p.zt == "1");
            if (exists != null && exists.Count > 0)
            {
                return new BusResult<string> { code = ResponseResultCode.FAILOfExists, msg = "菜单已存在" };
            }
            var newEty = vo.Adapt<SysModuleEntity>();
            newEty.NewEntity(orgId, user);
            newEty.Id = Guid.NewGuid().ToString();
            var result = await Add(newEty);
            if (result > 0)
            {
                return new BusResult<string> { code = ResponseResultCode.SUCCESS, Data = newEty.Id };
            }
            return new BusResult<string> { code = ResponseResultCode.FAIL, msg = "操作失败，请刷新重试" };
        }

        public async Task<BusResult<string>> UpdateEntity(SysModuleVO vo, string user, string orgId)
        {
            if (vo == null || string.IsNullOrEmpty(vo.Id) || string.IsNullOrEmpty(user))
            {
                return new BusResult<string> { code = ResponseResultCode.FAIL, msg = "无效操作，请核验菜单及操作员信息" };
            }
            if (!(await MenuEditAuthcation(user, orgId)))
            {
                return new BusResult<string> { code = ResponseResultCode.FAIL, msg = "操作员权限不足" };
            }
            if (vo.zt == "1")
            {
                var exists = await GetByWhere(p => p.Name == vo.Name && p.Id != vo.Id && p.zt == "1");
                if (exists != null && exists.Count > 0)
                {
                    return new BusResult<string> { code = ResponseResultCode.FAILOfExists, msg = "菜单已存在" };
                }
            }

            var ety = await GetFirstOrDefault(p => p.Id == vo.Id && p.OrganizeId == orgId);
            if (ety == null)
            {
                return new BusResult<string> { code = ResponseResultCode.FAIL, msg = "未找到相关菜单" };
            }
            var newEty = vo.Adapt<SysModuleEntity>();
            newEty.Id = ety.Id;
            newEty.CreateTime = ety.CreateTime;
            newEty.CreatorCode = ety.CreatorCode;
            //newEty.zt = ety.zt;
            newEty.OrganizeId = ety.OrganizeId;
            newEty.ModifiedEntity(orgId, user);
            var result = await Update(newEty);
            return new BusResult<string> { code = result == true ? ResponseResultCode.SUCCESS : ResponseResultCode.FAIL };
        }
        public async Task<BusResult<string>> DelEntity(string keyValue, string user, string orgId)
        {
            if (string.IsNullOrEmpty(keyValue) || string.IsNullOrEmpty(user) || string.IsNullOrEmpty(orgId))
            {
                return new BusResult<string> { code = ResponseResultCode.FAIL, msg = "无效操作，请核验菜单及操作员信息" };
            }
            if (!(await MenuEditAuthcation(user, orgId)))
            {
                return new BusResult<string> { code = ResponseResultCode.FAIL, msg = "操作员权限不足" };
            }
            var ety = await GetFirstOrDefault(p => p.Id == keyValue && p.OrganizeId == orgId);
            if (ety == null)
            {
                return new BusResult<string> { code = ResponseResultCode.FAIL, msg = "未找到相关菜单" };
            }
            if (ety.zt != "0")
            {
                ety.ModifiedEntity(orgId, user, true);
                var result = await Update(ety);
                if (string.IsNullOrWhiteSpace(ety.ParentId))
                {
                    var child = await GetByWhere(p => p.ParentId == ety.Id);
                    if (child != null)
                    {
                        child.ForEach(p =>
                        {
                            p.ModifiedEntity(orgId, user, true);
                        });
                        if (!(await UpdateRangeAsync(child)))
                        {
                            return new BusResult<string> { code = ResponseResultCode.FAIL, msg = "子菜单添加失败" };
                        }
                    }

                }
                return new BusResult<string> { code = result == true ? ResponseResultCode.SUCCESS : ResponseResultCode.FAIL };
            }
            return new BusResult<string> { code = ResponseResultCode.FAILOfRequestRepeat, msg = "菜单已作废" };
        }


        private async Task<bool> MenuEditAuthcation(string user, string orgId)
        {
            if (user == "root")
            {
                return true;
            }
            //获取用户权限
            var userEty = await GetByWhereWithAttr<SysUserEntity>(p => p.Account == user && p.TopOrganizeId == sysConfig.Top_OrganizeId && p.zt == "1");
            if (userEty == null || userEty.Count != 1)
            {
                return false;
            }
            string userId = userEty.FirstOrDefault()?.Id;
            var isAdmin = await _roleDmn.IsHospAdmin(userId, orgId);
            if (!isAdmin)
            {
                return false;
            }
            return true;
        }

    }
}

