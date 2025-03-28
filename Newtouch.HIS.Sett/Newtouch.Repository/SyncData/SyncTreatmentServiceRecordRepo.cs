using Newtouch.Core.Common;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using System.Collections.Generic;
using System.Data.SqlClient;
using System;
using System.Linq;
using Newtouch.HIS.Domain.DTO.InputDto.OutpatientAccounting;
using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Repository;
using Newtouch.Core.Common.Exceptions;

namespace Newtouch.HIS.Repository
{
    /// <summary>
    /// 
    /// </summary>
    public class SyncTreatmentServiceRecordRepo : RepositoryBase<SyncTreatmentServiceRecordEntity>, ISyncTreatmentServiceRecordRepo
    {
        public SyncTreatmentServiceRecordRepo(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="clzt"></param>
        /// <param name="mzh"></param>
        /// <param name="zyh"></param>
        /// <returns></returns>
        public IList<SyncTreatmentServiceRecordEntity> GetList(string orgId, int? clzt = 1, bool? wtjlbz = false, string mzh = "", string zyh = "")
        {
            if (string.IsNullOrWhiteSpace(mzh) && string.IsNullOrWhiteSpace(zyh))
            {
                return null;
            }
            var sql = @"
select * from TB_Sync_TreatmentServiceRecord(nolock)
where siteId = @orgId";
            var pars = new List<SqlParameter>();
            pars.Add(new SqlParameter("@orgId", orgId));
            if (clzt.HasValue)
            {
                sql += " and clzt = @clzt";
                pars.Add(new SqlParameter("@clzt", clzt));
            }
            if (!string.IsNullOrWhiteSpace(mzh))
            {
                sql += " and LOWER(patientType) = 'outpatient' and admsNum = @mzh";
                pars.Add(new SqlParameter("@mzh", mzh));
            }
            if (!string.IsNullOrWhiteSpace(zyh))
            {
                sql += " and LOWER(patientType) = 'inpatient' and admsNum = @zyh";
                pars.Add(new SqlParameter("@zyh", zyh));
            }
            if (wtjlbz.HasValue)
            {
                if (wtjlbz.Value)
                {
                    //true 仅问题记录
                    sql += " and isnull(wtjlbz,0) = 1";
                }
                else
                {
                    //false 排除问题记录
                    sql += " and isnull(wtjlbz,0) = 0";
                }
            }

            //确保一下 没有确认过
            sql += " and jfbId is null";

            sql += " order by isnull(ModifiedDate,CreatedDate) desc";

            return FindList<SyncTreatmentServiceRecordEntity>(sql, pars.ToArray());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="orgId"></param>
        /// <param name="clzt"></param>
        /// <param name="mzh"></param>
        /// <param name="zyh"></param>
        /// <returns></returns>
        public IList<SyncTreatmentServiceRecordEntity> GetPagintionList(Pagination pagination, string orgId, DateTime? kssj, DateTime? jssj, string gh, bool? isDeleted, bool? isAdmini, string brlx = "", int? clzt = 1, bool? wtjlbz = false, string mzh = "", string zyh = "", string blh = "", string xm = "")
        {

            var sql = @"
select * from TB_Sync_TreatmentServiceRecord(nolock)
where siteId = @orgId";
            var pars = new List<SqlParameter>();
            pars.Add(new SqlParameter("@orgId", orgId));
            if (clzt.HasValue)
            {
                sql += " and clzt = @clzt";
                pars.Add(new SqlParameter("@clzt", clzt));
            }
            if (!string.IsNullOrWhiteSpace(brlx) && brlx != "-1")
            {
                sql += " and LOWER(patientType) = @brlx";
                pars.Add(new SqlParameter("@brlx", brlx));

                if (!string.IsNullOrWhiteSpace(mzh))
                {
                    sql += " and admsNum = @mzh";
                    pars.Add(new SqlParameter("@mzh", mzh));
                }
                if (!string.IsNullOrWhiteSpace(zyh))
                {
                    sql += " and admsNum = @zyh";
                    pars.Add(new SqlParameter("@zyh", zyh));
                }
            }
            if (wtjlbz.HasValue)
            {
                if (wtjlbz.Value)
                {
                    //true 仅问题记录
                    sql += " and isnull(wtjlbz,0) = 1";
                }
                else
                {
                    //false 排除问题记录
                    sql += " and isnull(wtjlbz,0) = 0";
                }
            }

            if (!string.IsNullOrWhiteSpace(blh))
            {
                sql += " and patientId like @blh";
                pars.Add(new SqlParameter("@blh", "%" + blh + "%"));
            }
            if (!string.IsNullOrWhiteSpace(xm))
            {
                sql += " and patientName like @xm";
                pars.Add(new SqlParameter("@xm", "%" + xm + "%"));
            }
            if (kssj.HasValue && kssj != DateTime.MinValue && jssj.HasValue && jssj != DateTime.MinValue)
            {
                if (kssj > jssj)
                {
                    throw new FailedException("开始时间不能大于结束时间");
                }
                sql += " and isnull(ModifiedDate,CreatedDate)>@kssj and isnull(ModifiedDate,CreatedDate)<@jssj";
                pars.Add(new SqlParameter("@kssj", kssj));
                pars.Add(new SqlParameter("@jssj", DateTime.Parse(jssj.ToString()).AddDays(1)));
            }
            if (!string.IsNullOrWhiteSpace(gh))
            {
                //if (isAdmini.HasValue && !(bool)isAdmini)
                //{
                //    sql += " and therapistId =@gh";
                //    pars.Add(new SqlParameter("@gh", gh));
                //}
                sql += " and therapistId =@gh";
                pars.Add(new SqlParameter("@gh", gh));
            }

            if (isDeleted.HasValue)
            {
                sql += " and isnull(isDeleted,0) =@isDeleted";
                pars.Add(new SqlParameter("@isDeleted", isDeleted));
            }

            //确保一下 没有确认过
            sql += " and jfbId is null and zt='1'";

            //sql += " order by isnull(LastModifyTime,CreateTime) desc";

            return QueryWithPage<SyncTreatmentServiceRecordEntity>(sql, pagination, pars.ToArray());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="kssj"></param>
        /// <param name="jssj"></param>
        /// <param name="type"></param>
        public List<OptimaAccLeftDto> GetUnitList(string orgId, string type, string rygh, string kssj = null, string jssj = null)
        {
            var sql = @"SELECT DISTINCT
                        patientId ,
                        admsNum ,
                        patientType ,
                        xx.xm patientName ,
                        ( CASE LOWER(patientType)
                            WHEN 'inpatient' THEN ( SELECT  COUNT(1)
                                                    FROM    dbo.zy_brjbxx
                                                    WHERE   zyh = a.admsNum
                                                            AND OrganizeId = a.siteId
                                                            AND ( zybz = 1
                                                                  OR zybz = 2
                                                                )
                                                            AND zt = 1
                                                  )
                            ELSE 1
                          END ) hidden
                FROM    TB_Sync_TreatmentServiceRecord (NOLOCK) a
                        LEFT JOIN dbo.xt_brjbxx xx ON xx.blh = patientId AND xx.OrganizeId = @orgId
                WHERE   siteId = @orgId
                        AND a.zt = 1
                        and a.isDeleted=0
                        AND ISNULL(wtjlbz, 0) = 0
                        AND therapistId = @rygh
                        AND xx.zt = 1";
            var pars = new List<SqlParameter>();
            pars.Add(new SqlParameter("@orgId", orgId));

            if (string.IsNullOrWhiteSpace(type))
            {
                sql += " and (clzt=1 or clzt=2)";
            }
            else
            {
                sql += " and clzt=@type";
                pars.Add(new SqlParameter("@type", type));
            }
            if (!string.IsNullOrWhiteSpace(kssj))
            {
                sql += " AND (@kssj='' or ISNULL(zhclsj,clsj)>@kssj)";
                pars.Add(new SqlParameter("@kssj", kssj));

            }
            if (!string.IsNullOrWhiteSpace(jssj))
            {
                sql += " AND (@jssj='' or ISNULL(zhclsj, clsj) < DATEADD(d, 1, @jssj))";
                pars.Add(new SqlParameter("@jssj", jssj));
            }
            sql += "  GROUP BY patientId ,admsNum ,patientType, siteId, xx.xm";
            pars.Add(new SqlParameter("@rygh", rygh));
            return FindList<OptimaAccLeftDto>(sql, pars.ToArray());
        }
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="SyncTreatmentServiceRecordEntity"></param>
        /// <param name="keyValue"></param>
        public void SubmitForm(SyncTreatmentServiceRecordEntity SyncTreatmentServiceRecordEntity, int? keyValue, string orgId)
        {
            if (keyValue > 0)
            {
                SyncTreatmentServiceRecordEntity.Modify(keyValue);
                this.Update(SyncTreatmentServiceRecordEntity);
            }
            else
            {
                SyncTreatmentServiceRecordEntity.siteId = orgId;
                SyncTreatmentServiceRecordEntity.Create(true, new Guid().ToString());
                this.Insert(SyncTreatmentServiceRecordEntity);
            }

        }

        /// <summary>
        /// 批量作废
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="Id"></param>
        public void SaveCancel(string orgId, List<string> Ids)
        {
            if (string.IsNullOrEmpty(orgId) || Ids == null && Ids.Count > 0)
            {
                return;
            }
            using (var db = new EFDbTransaction(this._databaseFactory).BeginTrans())
            {
                foreach (var item in Ids)
                {
                    SyncTreatmentServiceRecordEntity entity = this.IQueryable().Where(a => a.siteId == orgId && a.zt == "1" && a.Id == item && a.clzt == 1).FirstOrDefault();
                    if (entity == null)
                    {
                        throw new FailedException("不存在这条记录");
                    }
                    entity.clzt = 3;
                    entity.Modify();
                    this.Update(entity);
                }
                db.Commit();
            }
        }

    }
}
