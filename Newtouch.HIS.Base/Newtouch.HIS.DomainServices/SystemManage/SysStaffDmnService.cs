using Newtouch.Common;
using Newtouch.Core.Common;
using Newtouch.Core.Common.Exceptions;
using Newtouch.Core.Common.Security;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.Entity.SystemManage;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.HIS.Domain.IRepository.SystemManage;
using Newtouch.HIS.Domain.ValueObjects;
using Newtouch.Infrastructure;
using Newtouch.Tools;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace Newtouch.HIS.DomainServices.SystemManage
{
	/// <summary>
	/// 
	/// </summary>
	public class SysStaffDmnService : DmnServiceBase, ISysStaffDmnService
	{
		private readonly ISysStaffRepo _sysStaffRepo;
		private readonly ISysUserRepo _sysUserRepo;
		private readonly ISysOrganizeRepository _sysOrganizeRepository;
		private readonly ISysStaffDutyRepo _sysStaffDutyRepo;
		private readonly ISysStaffWardRepo _sysStaffWardRepo;
        private readonly ISysStaffConsultRepo _sysStaffConsultRepo;

        public SysStaffDmnService(IBaseDatabaseFactory databaseFactory
			, ISysStaffRepo sysStaffRepo, ISysUserRepo sysUserRepo, ISysOrganizeRepository sysOrganizeRepository
			, ISysStaffDutyRepo sysStaffDutyRepo, ISysStaffWardRepo sysStaffWardRepo
            , ISysStaffConsultRepo sysStaffConsultRepo)
			: base(databaseFactory)
		{
			this._sysStaffRepo = sysStaffRepo;
			this._sysUserRepo = sysUserRepo;
			this._sysOrganizeRepository = sysOrganizeRepository;
			this._sysStaffDutyRepo = sysStaffDutyRepo;
			this._sysStaffWardRepo = sysStaffWardRepo;
            this._sysStaffConsultRepo = sysStaffConsultRepo;

        }

		/// <summary>
		/// 获取系统人员 分页列表（不包括子机构）
		/// </summary>
		/// <param name="topOrganizeId"></param>
		/// <param name="pagination"></param>
		/// <param name="keyword"></param>
		/// <returns></returns>
		public IList<SysStaffVO> GetPaginationList(Pagination pagination, string OrganizeId, string keyword = null)
		{
			var sql = @"exec wj_xtrycx @OrganizeId=@OrganizeId,@keyword=@keyword,@searchkeyword=@searchkeyword";

			SqlParameter[] par = new SqlParameter[] {
				new SqlParameter("@OrganizeId",OrganizeId),
				new SqlParameter("@keyword",keyword ?? ""),
				new SqlParameter("@searchkeyword","%" + keyword + "%" )
			};
			var result = this.FindList<SysStaffVO>(sql, par);
			pagination.records = result.Count();
			var list = result.Skip((pagination.page - 1) * pagination.rows).Take(pagination.rows).ToList();
			return list;
		}

		/// <summary>
		/// 添加、更新 系统人员
		/// </summary>
		/// <param name="entity"></param>
		/// <param name="keyValue"></param>
		public void SubmitForm(SysStaffEntity entity, string keyValue, bool asLoginUser, out string staffId)
		{
			if (!string.IsNullOrEmpty(keyValue))
			{
				if (_sysStaffRepo.IQueryable().Any(p => p.OrganizeId == entity.OrganizeId && p.Id != keyValue
					&& p.gh == entity.gh))  //同一医院内不能有gh一样的
				{
					throw new FailedException("工号不可重复");
				}

				SysStaffEntity oldEntity = null;   //变更前Entity
				oldEntity = _sysStaffRepo.FindEntity(keyValue);
				_sysStaffRepo.DetacheEntity(oldEntity);

				entity.Modify(keyValue);
				_sysStaffRepo.Update(entity);
				staffId = keyValue;
				if (oldEntity != null)
				{
					AppLogger.WriteEntityChangeRecordLog(oldEntity, entity, SysStaffEntity.GetTableName(), oldEntity.Id);
				}
			}
			else
			{
				var staff = "";
				//新增用户 开事物
				using (var db = new EFDbTransaction(this._databaseFactory).BeginTrans())
				{
					if (db.IQueryable<SysStaffEntity>().Any(p => p.OrganizeId == entity.OrganizeId
	   && p.gh == entity.gh))   //同一医院内不能有gh一样的
					{
						throw new FailedException("工号不可重复");
					}
					entity.Create(true);
					db.Insert(entity);

					if (asLoginUser)
					{
						var orgCode = db.IQueryable<SysOrganizeEntity>().Where(p => p.Id == entity.OrganizeId).Select(p => p.Code).FirstOrDefault();    //组织机构编码
						var topOrgCode = entity.OrganizeId != Constants.TopOrganizeId
								  ? db.IQueryable<SysOrganizeEntity>().Where(p => p.Id == Constants.TopOrganizeId).Select(p => p.Code).FirstOrDefault()    //顶级组织机构编码
								  : orgCode;
                        if (!string.IsNullOrWhiteSpace(orgCode) && !string.IsNullOrWhiteSpace(entity.gh))
                        {
                            var accont = entity.gh;

                            //创建系统账户

                            if (!_sysUserRepo.IQueryable().Any(p => p.TopOrganizeId == entity.TopOrganizeId
    && p.Account == accont))    //同一顶级机构内不能有Account一样的
                            {
                                var userEntity = new SysUserEntity()    //
                                {
                                    TopOrganizeId = entity.TopOrganizeId,
                                    Account = accont,
                                };
                                userEntity.Create(true);
                                var userLogOnEntity = new SysUserLogOnEntity()  //用户登录
                                {
                                    UserPassword = Constants.LogonDefaultPassword,
                                };
                                userLogOnEntity.UserId = userEntity.Id;
                                userLogOnEntity.UserSecretkey = Md5.md5(Comm.CreateNo(), 16).ToLower();
                                //topOrgCode + orgCode + default
                                //先为明文密码赋值在加密处理
                                userLogOnEntity.UserPassword = userLogOnEntity.UserPassword;
                                userLogOnEntity.UserPwdPlaintext = userLogOnEntity.UserPassword;
                                userLogOnEntity.UserPassword = Md5.md5(DESEncrypt.Encrypt(Md5.md5(userLogOnEntity.UserPassword, 32).ToLower(), userLogOnEntity.UserSecretkey).ToLower(), 32).ToLower();
                                userLogOnEntity.Create(true);
                                var userStaffEntity = new SysUserStaffEntity()  //人员用户 关联关系
                                {
                                    UserId = userEntity.Id,
                                    StaffId = entity.Id
                                };
                                staff = userEntity.Id;
                                userStaffEntity.Create(true);
                                db.Insert(userEntity);
                                db.Insert(userLogOnEntity);
                                db.Insert(userStaffEntity);

                            }
                            else
                            {
                                throw new FailedException("工号不可使用或已存在,请修改工号");
                            }
                        }
                        
					}

					db.Commit();
				}
				staffId = staff;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="staffId"></param>
		/// <param name="dutyIdList"></param>
		public void UpdateStaffDuty(string staffId, string[] dutyIdList)
		{
			if (string.IsNullOrWhiteSpace(staffId) || dutyIdList == null || dutyIdList.Count() == 0)
			{
				return;
			}
			//角色list
			var dutyLists = new List<SysStaffDutyEntity>();
			foreach (var item in dutyIdList.Where(p => !string.IsNullOrWhiteSpace(p)).Distinct())
			{
				var entity = new SysStaffDutyEntity();
				entity.Create(true);
				entity.StaffId = staffId;
				entity.DutyId = item;
				entity.zt = "1";
				dutyLists.Add(entity);
			}

			using (var db = new EFDbTransaction(this._databaseFactory).BeginTrans())
			{
				var oldDutyList = db.IQueryable<SysStaffDutyEntity>().Where(p => p.StaffId == staffId).ToList();
				for (int i = 0; i < dutyLists.Count; i++)
				{
					if (oldDutyList.Any(p => p.DutyId == dutyLists[i].DutyId))
					{
						oldDutyList.Remove(oldDutyList.Where(p => p.DutyId == dutyLists[i].DutyId).First());
						continue;
					}
					db.Insert(dutyLists[i]);
				}
				foreach (var item in oldDutyList)
				{
					db.Delete(item);
				}
				db.Commit();
			}

		}

		public void UpdateStaffWard(string staffId, string[] wardList, string OrganizeId)
		{
			var staffwards = new List<SysStaffWardEntity>();
			foreach (var item in wardList.Where(p => !string.IsNullOrWhiteSpace(p)).Distinct())
			{
				var entity = new SysStaffWardEntity();
				entity.Create(true);
				entity.StaffId = staffId;
				entity.bqCode = item;
				entity.OrganizeId = OrganizeId;
				entity.zt = "1";
				staffwards.Add(entity);
			}

			using (var db = new EFDbTransaction(this._databaseFactory).BeginTrans())
			{
				var oldList = db.IQueryable<SysStaffWardEntity>().Where(p => p.StaffId == staffId).ToList();
				for (int i = 0; i < staffwards.Count; i++)
				{
					if (oldList.Any(p => p.bqCode == staffwards[i].bqCode))
					{
						oldList.Remove(oldList.Where(p => p.bqCode == staffwards[i].bqCode).First());
						continue;
					}
					db.Insert(staffwards[i]);
				}
				foreach (var item in oldList)
				{
					db.Delete(item);
				}
				db.Commit();
			}
		}

		public SysStaffEntity GetSysStaff(string msEmaile, string organizeId)
		{
			var sql = @"select ry.*,usry.UserId from dbo.Sys_Staff as ry(nolock),dbo.Sys_UserStaff as usry(nolock) where ry.Id=usry.StaffId and ry.msEmail=@msEmaile and TopOrganizeId=@TopOrganizeId and ry.zt=1";
			SqlParameter[] par = new SqlParameter[] {
				new SqlParameter("@msEmaile",msEmaile),
				new SqlParameter("@TopOrganizeId",organizeId ),
			};
			return this.FindList<SysStaffEntity>(sql, par).FirstOrDefault();
		}

		public SysUserStaffEntity GetUserStaff(string staffId)
		{
			var sql = @" select * from Sys_UserStaff where StaffId=@StaffId and zt=1";
			SqlParameter[] par = new SqlParameter[] {
				new SqlParameter("@StaffId",staffId),
			};
			return this.FindList<SysUserStaffEntity>(sql, par).FirstOrDefault();
		}

		public SysStaffEntity GetUserStaffByUserId(string userId, string organizeId)
		{
			var sql = @"select ry.*,usry.UserId from dbo.Sys_Staff as ry(nolock),dbo.Sys_UserStaff as usry(nolock) where ry.Id=usry.StaffId and usry.UserId=@UserId and TopOrganizeId=@TopOrganizeId and ry.zt=1";
			SqlParameter[] par = new SqlParameter[] {
				new SqlParameter("@UserId",userId),
				 new SqlParameter("@TopOrganizeId",organizeId ),
			};
			return this.FindList<SysStaffEntity>(sql, par).FirstOrDefault();
		}


        //public void UpdateStaffConsult(string staffId, string zsCode, string OrganizeId)
        //{
        //    var entity = new SysStaffConsultEntity();
        //    using (var db = new EFDbTransaction(this._databaseFactory).BeginTrans()) {
        //        var staffConsult = db.IQueryable<SysStaffConsultEntity>().Where(p => p.StaffId == staffId &&p.OrganizeId==OrganizeId && p.zt == "1").FirstOrDefault();

        //        if (staffConsult !=null)
        //        {
        //            //修改
        //            var keyValue = staffConsult.Id;
        //            entity.Id = staffConsult.Id;
        //            entity.OrganizeId = OrganizeId;
        //            entity.StaffId = staffId;
        //            entity.zsCode = zsCode;
        //            entity.CreatorCode = staffConsult.CreatorCode;
        //            entity.CreateTime = staffConsult.CreateTime;
        //            entity.zt = staffConsult.zt;
        //            staffConsult.px = staffConsult.px;
        //            entity.Modify(keyValue);
        //            db.Update(entity);
        //        }
        //        else {
        //            //新增
        //            entity.OrganizeId = OrganizeId;
        //            entity.StaffId = staffId;
        //            entity.zsCode = zsCode;
        //            entity.Create();
        //            _sysStaffConsultRepo.Insert(entity);
        //        }
        //        db.Commit();
        //    }
        //}


        public void UpdateStaffConsult(string staffId, string zsCode, string OrganizeId)
        {
            using (var db = new EFDbTransaction(this._databaseFactory).BeginTrans())
            {
               
                //删除
                var oldEntity = db.IQueryable<SysStaffConsultEntity>().Where(p => p.StaffId == staffId && p.OrganizeId == OrganizeId && p.zt=="1").FirstOrDefault();
                db.Delete(oldEntity);

                //新增
                var entity = new SysStaffConsultEntity();
                entity.OrganizeId = OrganizeId;
                entity.StaffId = staffId;
                entity.zsCode = zsCode;
                entity.zt = "1";
                entity.Create();
                db.Insert(entity);

                db.Commit();
            }
        }

    }
}
