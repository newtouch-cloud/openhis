using FrameworkBase.MultiOrg.DmnService;
using FrameworkBase.MultiOrg.Infrastructure;
using Newtouch.Core.Common;
using Newtouch.Core.Common.Exceptions;
using Newtouch.HIS.Domain.DTO;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.HIS.Domain.ValueObjects;
using Newtouch.HIS.Domain.ValueObjects.PatientManage;
using Newtouch.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace Newtouch.HIS.DomainServices
{
    /// <summary>
    /// 病人病历DmnService
    /// </summary>
    public class SysPatientMedicalRecordDmnService : DmnServiceBase, ISysPatientMedicalRecordDmnService
    {
        public SysPatientMedicalRecordDmnService(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }

        /// <summary>
        /// 获取病历
        /// </summary>
        /// <param name="blh"></param>
        /// <param name="blId"></param>
        /// <returns></returns>
        public IList<SysPatientMedicalRecordDTO> GetMedicalRecordList(string orgId, string blh, string blId = null)
        {
            if (string.IsNullOrWhiteSpace(blh) && string.IsNullOrWhiteSpace(blId))
            {
                throw new FailedException("BLH_OR_BLID_IS_REQUIRED", "缺少请求参数");
            }
            var sql = @"select a.zt, a.Id, a.blh, CONVERT(varchar(100), a.rq, 23) rq, a.bz
, a.CreatorCode, c.Name CreatorUserName, CONVERT(varchar(100), a.CreateTime, 120) CreateTime
from xt_brlsbl a
left join [NewtouchHIS_Base]..V_C_Sys_UserStaff c
on c.OrganizeId = a.OrganizeId and c.Account = a.CreatorCode
where 1 = 1 --a.zt = '1'
and a.OrganizeId = @orgId
and ('' = @blh or a.blh = @blh)
and ('' = @blId or a.Id = @blId)
order by a.zt desc, a.rq desc, a.CreateTime";
            var pars = new List<SqlParameter>();
            pars.Add(new SqlParameter("@orgId", orgId));
            pars.Add(new SqlParameter("@blh", blh ?? ""));
            pars.Add(new SqlParameter("@blId", blId ?? ""));
            var blList = this.FindList<SysPatientMedicalRecordDTO>(sql, pars.ToArray());

            foreach (var bl in blList)
            {
                var sql2 = @"select a.zt, b.Id, b.attachType, b.attachName, b.attachPath
, CONVERT(varchar(100), b.CreateTime, 120) CreateTime
, c.Name CreatorUserName
from xt_brlsbl a
left
join xt_brlsblmx b
on b.blId = a.Id
left join [NewtouchHIS_Base]..V_C_Sys_UserStaff c
on c.OrganizeId = a.OrganizeId and c.Account = a.CreatorCode
where 1 = 1 --a.zt = '1'
and a.OrganizeId = @orgId
and a.Id = @blId
and b.zt = '1'
order by a.zt desc, a.rq desc, a.CreateTime, b.CreateTime";
                var pars2 = new List<SqlParameter>();
                pars2.Add(new SqlParameter("@orgId", orgId));
                pars2.Add(new SqlParameter("@blId", bl.Id));
                var mxList = this.FindList<SysPatientMedicalRecordDetailVO>(sql2, pars2.ToArray());
                bl.Details = mxList;

                foreach (var mx in bl.Details)
                {
                    mx.attachUrl = CommmHelper.GetLocalFileNetUrl(mx.attachPath);
                }
            }

            return blList;
        }

        /// <summary>
        /// 获取病历
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public SysPatientMedicalRecordEntity GetMedicalRecordById(string Id)
        {
            var sql = "select * from xt_brlsbl(nolock) where Id = @Id";
            return this.FirstOrDefault<SysPatientMedicalRecordEntity>(sql, new List<SqlParameter>() {
                new SqlParameter("@Id", Id) }.ToArray());
        }

        /// <summary>
        /// 获取病历明细
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public IList<SysPatientMedicalRecordDetailEntity> GetMedicalRecordDetailListByMainId(string Id)
        {
            var sql = "select * from xt_brlsblmx(nolock) where blId = @blId";
            return this.FindList<SysPatientMedicalRecordDetailEntity>(sql, new List<SqlParameter>() {
                new SqlParameter("@blId", Id) }.ToArray());
        }

        public void SubmitForm(string keyValue, string blh
            , DateTime rq
            , string zt, string bz, IList<string> delDetailIdList, List<SysPatientMedicalRecordDetailEntity> addDetailEntityList,
            List<SysPatientMedicalRecordDetailEntity> updateDetailEntityList
            , string orgId)
        {
            using (var db = new EFDbTransaction(this._databaseFactory).BeginTrans())
            {
                SysPatientMedicalRecordEntity mEntity = null;
                if (!string.IsNullOrWhiteSpace(keyValue))
                {
                    mEntity = db.FindEntity<SysPatientMedicalRecordEntity>(keyValue);
                }
                if (mEntity == null)
                {
                    mEntity = new SysPatientMedicalRecordEntity()
                    {
                        blh = blh,
                        OrganizeId = orgId,
                        rq = rq,
                        bz = bz,
                    };
                    mEntity.Create(true);
                    db.Insert(mEntity);
                }
                else
                {
                    mEntity.zt = zt;
                    mEntity.rq = rq;
                    mEntity.bz = bz;
                    mEntity.Modify();
                    db.Update(mEntity);
                }

                foreach (var item in addDetailEntityList)
                {
                    item.Create(true);
                    item.blId = mEntity.Id;
                    item.OrganizeId = orgId;
                    db.Insert(item);
                }

                foreach (var item in updateDetailEntityList)
                {
                    item.Modify();
                    item.blId = mEntity.Id;
                    item.OrganizeId = orgId;
                    db.Update(item);
                }

                if (delDetailIdList != null)
                {
                    foreach (var detailId in delDetailIdList)
                    {
                        db.Delete<SysPatientMedicalRecordDetailEntity>(p => p.Id == detailId);
                    }
                }

                db.Commit();
            }
        }
        #region 卫健数据上传日志
        /// <summary>
        /// 卫健上传日志
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="ksrq"></param>
        /// <param name="jsrq"></param>
        /// <param name="tabname"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public IList<HealthyUploadVo> GetHealthUpload(Pagination pagination, DateTime ksrq, DateTime jsrq, string tabname,string orgId)
        {
            IList<SqlParameter> inSqlParameterList = new List<SqlParameter>(); ;
            string sql = @"select Id,TabName,TabNameDesc,status,err_msg,createtime,statusName,OrganizeId 
                    from Newtouch_His_Log..[Log_TbHealthyUpload]
                    where OrganizeId=@orgId  and createtime>=@ksrq and createtime<=@jsrq ";
            if (!string.IsNullOrEmpty(tabname))
            {
                sql += " and TabName=@tabname";
                inSqlParameterList.Add(new SqlParameter("@tabname", tabname));
            }
            inSqlParameterList.Add(new SqlParameter("@ksrq", ksrq));
            inSqlParameterList.Add(new SqlParameter("@jsrq", jsrq));
            inSqlParameterList.Add(new SqlParameter("@orgId", orgId));
            return this.QueryWithPage<HealthyUploadVo>(sql, pagination, inSqlParameterList.ToArray()).ToList();

        }
        #endregion
    }
}
