using Newtouch.HIS.Domain.IDomainServices.Settlement;
using Newtouch.HIS.Domain.ValueObjects;
using Newtouch.Infrastructure;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Newtouch.Core.Common;

namespace Newtouch.HIS.DomainServices.Settlement
{
    /// <summary>
    /// 
    /// </summary>
    public class SysWardBedDmnService : DmnServiceBase, ISysWardBedDmnService
    {
        public SysWardBedDmnService(IBaseDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }


        /// <summary>
        /// 获得所有列表、修改form
        /// </summary>
        public List<SysWardBedVO> GetWardBedList(int? cwId, string orgId = null, string keyword = null)
        {
            StringBuilder strSql = new StringBuilder();
            var pars = new List<SqlParameter>();
            strSql.Append(@"SELECT bq.bqmc,cw.[cwId],cw.[OrganizeId],cw.[cwCode],cw.[cwmc],cw.[bqCode],cw.[jcbz],cw.[cwf],
                            cw.[djyyjsf],cw.[bszlf],cw.[yymjkqjhf],cw.[zt],cw.[CreatorCode],cw.[CreateTime],cw.[LastModifyTime],
                            cw.[LastModifierCode],cw.[px],cw.[bfCode],cw.[cwlx],bf.bfNo,cw.cwdj
                            FROM xt_cw cw
                            LEFT JOIN xt_bq bq ON cw.bqCode=bq.bqCode  AND bq.OrganizeId=cw.organizeid
                            left join xt_bf bf on cw.bfcode=bf.bfcode and cw.organizeid=bf.organizeid
                            WHERE cw.OrganizeId=@orgId 
                         ");
            if (cwId != null && cwId > 0)
            {
                strSql.Append(" and cw.cwId=@cwId");
                pars.Add(new SqlParameter("@cwId", cwId));
            }
            if (!string.IsNullOrEmpty(keyword))
            {
                strSql.Append(" and (bq.bqmc like @keyword or cw.bqCode like @keyword or cw.py like @keyword)");
                pars.Add(new SqlParameter("@keyword", '%' + keyword + '%'));
            }
            pars.Add(new SqlParameter("@orgId", orgId));
            var list = this.FindList<SysWardBedVO>(strSql.ToString(), pars.ToArray()).ToList();
            return list;
        }

        /// <summary>
        /// 修改form
        /// </summary>
        public SysWardBedVO GetWardBedEntity(int cwId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"

                         ");
            SqlParameter[] param =
                {
                    new SqlParameter("@sfxmId",cwId)
                };
            var list = this.FindList<SysWardBedVO>(strSql.ToString(), param).FirstOrDefault();
            return list;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="organizeId"></param>
        /// <param name="pagination"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public IList<SysWardBedVO> GetPagintionList(string orgId, Pagination pagination, string keyword = null)
        {
            StringBuilder strSql = new StringBuilder();
            var pars = new List<SqlParameter>();
            strSql.Append(@"SELECT bq.bqmc,cw.[cwId],cw.[OrganizeId],cw.[cwCode],cw.[cwmc],cw.[bqCode],cw.[jcbz],cw.[cwf],
                            cw.[djyyjsf],cw.[bszlf],cw.[yymjkqjhf],cw.[zt],cw.[CreatorCode],cw.[CreateTime],cw.[LastModifyTime],
                            cw.[LastModifierCode],cw.[px],cw.[bfCode],cw.[cwlx],bf.bfNo
                            FROM xt_cw cw
                            LEFT JOIN xt_bq bq ON cw.bqCode=bq.bqCode  AND bq.OrganizeId=cw.organizeid
                            left join xt_bf bf on cw.bfcode=bf.bfcode and cw.organizeid=bf.organizeid
                            WHERE cw.OrganizeId=@orgId
                         ");
            if (!string.IsNullOrEmpty(keyword))
            {
                strSql.Append(" and (bq.bqmc like @keyword or cw.bqCode like @keyword or cw.cwmc like @keyword or cw.cwCode like @keyword)");
                pars.Add(new SqlParameter("@keyword", '%' + keyword + '%'));
            }
            pars.Add(new SqlParameter("@orgId", orgId));
            return this.QueryWithPage<SysWardBedVO>(strSql.ToString(), pagination, pars.ToArray());
        }
    }
}
