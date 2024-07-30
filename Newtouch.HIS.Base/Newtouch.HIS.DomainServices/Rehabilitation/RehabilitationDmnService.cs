using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IDomainServices.Rehabilitation;
using Newtouch.HIS.Domain.ValueObjects.Rehabilitation;
using Newtouch.Infrastructure;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Newtouch.HIS.DomainServices.Rehabilitation
{
    /// <summary>
    /// 
    /// </summary>
    public class RehabilitationDmnService : DmnServiceBase, IRehabilitationDmnService
    {
        public RehabilitationDmnService(IBaseDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }

        #region 康复收费项目

        /// <summary>
        /// 获得所有列表(康复收费项目)
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public List<RehabChargeItemVO> GetRehabChargeItemList(string keyword, string OrganizeId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"
SELECT sfxm.*,
        sffl.Name sfflName
FROM kf_sfxm sfxm
LEFT JOIN kf_sffl sffl
    ON sffl.Code=sfxm.sfflCode
        AND sffl.OrganizeId=sfxm.OrganizeId
WHERE sfxm.OrganizeId=@OrganizeId
                         ");
            if (!string.IsNullOrEmpty(keyword))
            {
                strSql.Append(" and (sfxm.Code LIKE @keyword  OR sfxm.Name LIKE @keyword OR sfxm.py LIKE @keyword)");
            }
            SqlParameter[] param =
                {
                    new SqlParameter("@keyword",'%' + keyword + '%'),
                    new SqlParameter("@OrganizeId",OrganizeId)
                };
            var list = this.FindList<RehabChargeItemVO>(strSql.ToString(), param).ToList();
            return list;
        }

        /// <summary>
        /// 修改form(康复收费项目)
        /// </summary>
        /// <param name="sfxmId"></param>
        /// <returns></returns>
        public RehabChargeItemVO GetRehabChargeItemEntity(string sfxmId, string OrganizeId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"
SELECT sfxm.*,
        sffl.Name sfflName
FROM kf_sfxm sfxm
LEFT JOIN kf_sffl sffl
    ON sffl.Code=sfxm.sfflCode
        AND sffl.OrganizeId=sfxm.OrganizeId
WHERE sfxm.OrganizeId=@OrganizeId
        AND sfxm.sfxmId=@sfxmId
                         ");
            SqlParameter[] param =
                {
                    new SqlParameter("@sfxmId",sfxmId),
                    new SqlParameter("@OrganizeId",OrganizeId)
                };
            var list = this.FindList<RehabChargeItemVO>(strSql.ToString(), param).FirstOrDefault();
            return list;
        }

        #endregion


        #region 康复收费项目对照

        /// <summary>
        /// 获得所有列表(康复收费项目对照)
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="OrganizeId"></param>
        /// <returns></returns>
        public List<RehabChargeItemComparisonVO> GetRehabChargeItemComparisonList(string keyword, string OrganizeId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"
                            select sfxmdz.sfxmdzId, kfsfxm.Name as kfsfxmName,sfxmdz.kfsfxmCode, xtsfxm.sfxmmc as xtsfxmName, sfxmdz.xtsfxmCode, xtsfxm.duration as xtsfxm_duration, xtsfxm.dj from xt_kf_sfxmdz sfxmdz 
							left join kf_sfxm kfsfxm on kfsfxm.Code=sfxmdz.kfsfxmCode 
                            left join xt_sfxm xtsfxm on xtsfxm.sfxmCode=sfxmdz.xtsfxmCode and xtsfxm.OrganizeId=@OrganizeId
                            where sfxmdz.OrganizeId=@OrganizeId
                         ");
            if (!string.IsNullOrEmpty(keyword))
            {
                strSql.Append(" and sfxmdz.kfsfxmCode like @keyword or sfxmdz.xtsfxmCode like @keyword ");
            }
            SqlParameter[] param =
                {
                    new SqlParameter("@keyword",'%' + keyword + '%'),
                    new SqlParameter("@OrganizeId",OrganizeId)
                };
            var list = this.FindList<RehabChargeItemComparisonVO>(strSql.ToString(), param).ToList();
            return list;
        }

        /// <summary>
        /// 修改form(康复收费项目对照)
        /// </summary>
        /// <param name="sfxmdzId"></param>
        /// <returns></returns>
        public RehabChargeItemComparisonVO GetRehabChargeItemComparisonEntity(string sfxmdzId, string OrganizeId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"
                            select sfxmdz.OrganizeId, sfxmdz.sfxmdzId, kfsfxm.Name as kfsfxmName,sfxmdz.kfsfxmCode, xtsfxm.sfxmmc as xtsfxmName, sfxmdz.xtsfxmCode, xtsfxm.duration as xtsfxm_duration, xtsfxm.dj from xt_kf_sfxmdz sfxmdz 
							left join kf_sfxm kfsfxm on kfsfxm.Code=sfxmdz.kfsfxmCode 
                            left join xt_sfxm xtsfxm on xtsfxm.sfxmCode=sfxmdz.xtsfxmCode and xtsfxm.OrganizeId=@OrganizeId
                            where sfxmdz.OrganizeId=@OrganizeId and sfxmdz.sfxmdzId=@sfxmdzId
                         ");
            SqlParameter[] param =
                {
                    new SqlParameter("@sfxmdzId",sfxmdzId),
                    new SqlParameter("@OrganizeId",OrganizeId)
                };
            var list = this.FindList<RehabChargeItemComparisonVO>(strSql.ToString(), param).FirstOrDefault();
            return list;
        }

        /// <summary>
        /// 收费项目检索
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public IList<SysChargeItemVO> GetHisBindSelect(string keyword, string orgId)
        {
            if (string.IsNullOrWhiteSpace(orgId))
            {
                return null;
            }
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"
WITH cteTree
        AS (SELECT *
              FROM V_S_xt_sfdl
              WHERE dlCode = 'FZLXM01' --第一个查询作为递归的基点(锚点)
            UNION ALL
            SELECT V_S_xt_sfdl.*     --第二个查询作为递归成员， 下属成员的结果为空时，此递归结束。
              FROM
                   cteTree INNER JOIN V_S_xt_sfdl ON cteTree.dlId= V_S_xt_sfdl.ParentId
				   )
select top 800 * from(
select sfxmCode, sfxmmc, a.sfdlCode sfdlCode, b.dlmc sfdlmc, dw, dj, a.py, a.duration
,'2' yzlx   --1药品 2项目
,px
from V_S_xt_sfxm(nolock) a
left join V_S_xt_sfdl(nolock) b
on a.sfdlCode = b.dlCode and b.OrganizeId = @orgId

where a.OrganizeId = @orgId
and b.dlCode not in (SELECT dlCode FROM cteTree)
                        ");
            var par = new List<SqlParameter>();
            par.Add(new SqlParameter("@orgId", orgId));

            if (!string.IsNullOrEmpty(keyword))
            {
                strSql.Append(" and (a.sfxmCode like @searchkeyword or a.sfxmmc like @searchkeyword or a.py like @searchkeyword)");
                par.Add(new SqlParameter("@searchkeyword", '%' + keyword.Trim() + '%'));
            }

            strSql.Append(@"
) as tttttttttt
order by yzlx, px, sfxmmc 
                        ");

            var list = this.FindList<SysChargeItemVO>(strSql.ToString(), par.ToArray()).ToList();
            return list;
        }

        #endregion
    }
}
