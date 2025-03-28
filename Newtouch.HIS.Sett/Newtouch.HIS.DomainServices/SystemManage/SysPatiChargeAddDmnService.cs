using FrameworkBase.MultiOrg.DmnService;
using FrameworkBase.MultiOrg.Infrastructure;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.HIS.Domain.ValueObjects;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Newtouch.HIS.DomainServices
{
    public class SysPatiChargeAddDmnService : DmnServiceBase, ISysPatiChargeAddDmnService
    {
        public SysPatiChargeAddDmnService(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }

        /// <summary>
        /// 获取有效列表
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public List<SysPatiChargeAddVo> GetSysPatiChargeAddVoList(string keyword, int? bh = null)
        {
            List<SysPatiChargeAddVo> effectiveList;
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"
                             SELECT
                             f.brsffjbh,
                             x.brxzmc,
                             x.brxz,
                             d.dlmc,
							 d.dlCode,
                             f.fjxsdl,
                             l.dlmc fjxsdlmc,
                             m.sfxmmc,
							 m.sfxmCode,
                             f.fwfbl,
                             f.zt,
                             f.CreateTime,
                             f.CreatorCode
                        FROM xt_brsffj f
                        LEFT JOIN xt_brxz x ON x.brxz = f.brxz
                        LEFT JOIN NewtouchHIS_Base..V_S_xt_sfdl d ON d.dlCode = f.dl
                        LEFT JOIN NewtouchHIS_Base..V_S_xt_sfxm m ON m.sfxmCode = f.sfxm
                        LEFT JOIN xt_fjsfdl l ON l.dl = f.fjxsdl
                        where 1=1 
                           ");
            var parms = new List<SqlParameter>();
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                strSql.Append(@" and --x.brxzmc = @keyword or d.dlmc = @keyword or l.dlmc = @keyword or m.sfxmmc = @keyword
--or 
 (x.brxzmc like @serachkeyword or d.dlmc like @serachkeyword or l.dlmc like @serachkeyword or m.sfxmmc like @serachkeyword )");
                //parms.Add(new SqlParameter("@keyword", keyword));
                parms.Add(new SqlParameter("@serachkeyword", "%" + keyword + "%"));
            }
            if (bh.HasValue && bh.Value > 0)
            {
                strSql.Append(@" and f.brsffjbh=@bh");
                parms.Add(new SqlParameter("@bh", bh));
            }
            strSql.Append(" order by f.px, f.CreateTime desc");
            if (parms.Count > 0)
            {
                effectiveList = this.FindList<SysPatiChargeAddVo>(strSql.ToString(), parms.ToArray()).ToList();
            }
            else
            {
                effectiveList = this.FindList<SysPatiChargeAddVo>(strSql.ToString()).ToList();

            }
            return effectiveList;
        }
    }
}
