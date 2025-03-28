using FrameworkBase.MultiOrg.DmnService;
using FrameworkBase.MultiOrg.Infrastructure;
using Newtouch.Core.Common;
using Newtouch.HIS.Domain.IDomainServices.SystemManage;
using Newtouch.HIS.Domain.ValueObjects.SystemManage;
using Newtouch.Tools;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Newtouch.HIS.DomainServices.SystemManage
{
    public class SysCISpecialMarkDmnService : DmnServiceBase, ISysCISpecialMarkDmnService
    {
        public SysCISpecialMarkDmnService(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }

        public List<SysCISpecialMarkVO> GetGridBySearch(Pagination panigation, string keyword)
        {
            StringBuilder strsql = new StringBuilder();
            IList<SqlParameter> inSqlParameterList = null;
            strsql.Append(@"SELECT  m.sfxmmc ,
                            b.brxzmc ,
                            z.zt ,
                            z.sfxmtsbzbh ,
                            z.CreatorCode ,
                            z.CreateTime
                    FROM    dbo.xt_sfxmtsbz z
                            LEFT JOIN dbo.xt_sfxm m ON m.sfxm = z.sfxm
                            LEFT JOIN dbo.xt_brxz b ON b.brxz = z.brxz ");
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                strsql.Append(" where x.sfxmmc like @keywords or b.brxzmc like @keywords");
                inSqlParameterList = new List<SqlParameter>();
                inSqlParameterList.Add(new SqlParameter("@keywords", "%" + keyword + "%"));
            }
            return this.QueryWithPage<SysCISpecialMarkVO>(strsql.ToString(), panigation, inSqlParameterList == null ? null : inSqlParameterList.ToArray()).ToList();

        }

        public SysCISpecialMarkVO GetFormJson(int keyValue)
        {
            StringBuilder strsql = new StringBuilder();
            IList<SqlParameter> inSqlParameterList = null;
            strsql.Append(@"SELECT  m.sfxmmc ,
                            b.brxzmc ,
                            z.zt ,
                            z.CreatorCode ,
                            z.CreateTime,
                            z.sfxm ,
                            m.sfxmbh ,
                            b.brxz
                    FROM    dbo.xt_sfxmtsbz z
                            LEFT JOIN dbo.xt_sfxm m ON m.sfxm = z.sfxm
                            LEFT JOIN dbo.xt_brxz b ON b.brxz = z.brxz where z.sfxmtsbzbh=@keywords");
            if (!string.IsNullOrWhiteSpace(keyValue.ToString()))
            {
                inSqlParameterList = new List<SqlParameter>();
                inSqlParameterList.Add(new SqlParameter("@keywords", keyValue));
            }
            return this.FindList<SysCISpecialMarkVO>(strsql.ToString(), inSqlParameterList == null ? null : inSqlParameterList.ToArray()).ToList()[0];

        }
    }
}
