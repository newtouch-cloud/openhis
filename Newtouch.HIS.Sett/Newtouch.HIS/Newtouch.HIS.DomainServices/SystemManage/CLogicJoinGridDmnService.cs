using FrameworkBase.MultiOrg.DmnService;
using FrameworkBase.MultiOrg.Infrastructure;
using Newtouch.Core.Common;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.HIS.Domain.ValueObjects.SystemManage;
using Newtouch.Tools;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Newtouch.HIS.DomainServices
{
    public class CLogicJoinGridDmnService : DmnServiceBase, ICLogicJoinGridDmnService
    {

        public CLogicJoinGridDmnService(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }

        /// <summary>
        /// 病人收费算法grid显示
        /// </summary>
        /// <returns></returns>
        /// <summary>
        /// 搜索病人收费算法
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="keyword"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public List<PatiChargeLogicVO> GetPatiChargeLogicBySearch(Pagination pagination, string keyword, string orgId)
        {
            StringBuilder strsql = new StringBuilder();
            IList<SqlParameter> inSqlParameterList = null;
            strsql.Append(@"SELECT DISTINCT
                            s.brsfsfbh ,
                            x.brxzmc ,
                            s.mzzybz ,
                            d.dlmc ,
                            s.dl ,
                            m.sfxmmc ,
                            fyfw ,
                            sfjb ,
                            s.zfbl ,
                            s.zfxz ,
                            s.fysx ,
                            s.zt ,
                            s.CreateTime
                    FROM    xt_brsfsf s
                            LEFT JOIN xt_brxz x ON x.brxz = s.brxz AND s.OrganizeId=x.OrganizeId
                            LEFT JOIN NewtouchHIS_Base..V_S_xt_sfdl d ON CONVERT(VARCHAR(50), d.dlCode) = s.dl AND d.OrganizeId=x.OrganizeId
                            LEFT JOIN NewtouchHIS_Base..V_S_xt_sfxm m ON m.sfxmCode = s.sfxm AND m.OrganizeId=d.OrganizeId");

            strsql.Append(" where s.OrganizeId=@orgId");
            inSqlParameterList = new List<SqlParameter> { new SqlParameter("@orgId", orgId == null ? "" : orgId) };
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                strsql.Append(" where x.brxzmc like @keywords");
                inSqlParameterList = new List<SqlParameter> { new SqlParameter("@keywords", "%" + keyword + "%")};
            }
            //strsql.Append(" where s.OrganizeId=@orgId");
            //inSqlParameterList = new List<SqlParameter> { new SqlParameter("@orgId", orgId == null ? "" : orgId) };
            return this.QueryWithPage<PatiChargeLogicVO>(strsql.ToString(), pagination, inSqlParameterList == null ? null : inSqlParameterList.ToArray()).ToList();

        }

        /// <summary>
        /// 编辑页面时，加载选中行的对象
        /// </summary>
        /// <param name="keyvalue"></param>
        /// <returns></returns>
        public List<PatiChargeLogicVO> GetPatiChargeLogicFirst(string keyvalue)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"   SELECT brsfsfbh ,
                                dlmc ,
                                dl.dlCode dl ,
                                brxzmc ,
                                xz.brxz ,
                                sfxmmc ,
                                xm.sfxmCode ,
                                sf.mzzybz ,
                                sfjb ,
                                fyfw ,
                                sf.zfbl ,
                                sf.zfxz ,
                                fysx ,
                                sf.zt ,
                                sf.CreateTime ,
                                sf.CreatorCode
                         FROM   dbo.xt_brsfsf sf
                                LEFT JOIN NewtouchHIS_Base.dbo.V_S_xt_sfdl dl ON dl.dlCode = sf.dl
                                LEFT JOIN NewtouchHIS_Base.dbo.V_S_xt_sfxm xm ON xm.sfxmCode = sf.sfxm
                                LEFT JOIN dbo.xt_brxz xz ON xz.brxz = sf.brxz
                            WHERE sf.brsfsfbh='" + keyvalue + "'");
            return this.FindList<PatiChargeLogicVO>(strSql.ToString());
        }
    }
}
