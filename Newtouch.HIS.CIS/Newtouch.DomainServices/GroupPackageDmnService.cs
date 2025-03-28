using FrameworkBase.MultiOrg.DmnService;
using FrameworkBase.MultiOrg.Infrastructure;
using Newtouch.Core.Common.Exceptions;
using Newtouch.Domain.BusinessObjects;
using Newtouch.Domain.Entity;
using Newtouch.Domain.IDomainServices;
using Newtouch.Domain.ValueObjects;
using Newtouch.HIS.Domain.IRepository;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace Newtouch.DomainServices
{
    public class GroupPackageDmnService : DmnServiceBase, IGroupPackageDmnService
    {
        private readonly IGroupPackageRepo _groupPackageRepo;
        private readonly IGroupPackageItemRepo _groupPackageItemRepo;
        public GroupPackageDmnService(IDefaultDatabaseFactory databaseFactory) : base(databaseFactory)
        {
        }

        /// <summary>
        /// 组套详情
        /// </summary>
        /// <param name="ztId"></param>
        public GroupPackageDetailBO GetGroupPackageDetail(string ztId, string orgId)
        {
            if (string.IsNullOrWhiteSpace(ztId))
            {
                throw new FailedException("数据异常，ztId为空或null");
            }

            //组套信息
            var ztEntity = _groupPackageRepo.IQueryable().Where(a => a.ztId == ztId).FirstOrDefault();
            if (ztEntity == null)
            {
                throw new FailedException("数据异常，没有找到组套信息");
            }
            //组套项目
            var sql = @"
SELECT ztxm.sfxmCode,ztxm.sfxmmc,ztxm.sfdl,ztxm.dj,ztxm.dw,
        sfdl.dlmc AS sfdlmc,ztxm.sl,ztxm.px 
FROM [dbo].[jyjc_ztxm] ztxm
LEFT JOIN NewtouchHIS_Base..V_S_xt_sfdl sfdl
    ON sfdl.dlCode=ztxm.sfdl
        AND sfdl.OrganizeId=ztxm.OrganizeId
WHERE ztxm.OrganizeId=@orgId
        AND ztxm.ztId=@ztId 
     order by px ";
            var ztxmList = this.FindList<GroupPackageItemVO>(sql, new[] { new SqlParameter("@ztId", ztId), new SqlParameter("@orgId", orgId) });

            var bo = new GroupPackageDetailBO();
            bo.ztEntity = ztEntity;
            bo.ztxmList = ztxmList;
            return bo;
        }


        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="ztobj"></param>
        /// <param name="ztxmlist"></param>
        public void SaveData(GroupPackageEntity ztobj, List<GroupPackageItemEntity> ztxmlist)
        {
            using (var db = new EFDbTransaction(this._databaseFactory).BeginTrans())
            {
                //组套
                if (!string.IsNullOrWhiteSpace(ztobj.ztId))  //修改
                {
                    ztobj.Modify();

                    db.Update(ztobj);

                    db.Delete<GroupPackageItemEntity>(a => a.ztId == ztobj.ztId); //先全删，再新增
                }
                else
                {
                    ztobj.ztId = Guid.NewGuid().ToString();
                    ztobj.Create();

                    db.Insert(ztobj);
                }


                //组套项目
                if (ztxmlist != null)
                {
                    foreach (var item in ztxmlist)
                    {
                        GroupPackageItemEntity ztxm = new GroupPackageItemEntity();
                        ztxm.ztxmId = Guid.NewGuid().ToString();
                        ztxm.OrganizeId = ztobj.OrganizeId;
                        ztxm.ztId = ztobj.ztId;
                        ztxm.sfxmCode = item.sfxmCode;
                        ztxm.sfxmmc = item.sfxmmc;
                        ztxm.sfdl = item.sfdl;
                        ztxm.dj = item.dj;
                        ztxm.dw = item.dw;
                        ztxm.sl = item.sl;
						ztxm.px = item.px;
                        ztxm.Create();

                        db.Insert(ztxm);
                    }
                }

                db.Commit();
            }

        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ztId"></param>
        public void DeleteData(string ztId)
        {
            using (var db = new EFDbTransaction(this._databaseFactory).BeginTrans())
            {
                db.Delete<GroupPackageEntity>(a => a.ztId == ztId);
                db.Delete<GroupPackageItemEntity>(a => a.ztId == ztId);

                db.Commit();
            }
        }
    }
}
