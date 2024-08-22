using Newtouch.Common;
using Newtouch.Core.Common;
using Newtouch.HIS.Domain.IDomainServices.SystemManage;
using Newtouch.HIS.Domain.ValueObjects.SystemManage;
using Newtouch.Infrastructure;
using Newtouch.Tools;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Newtouch.HIS.DomainServices.SystemManage
{
    public class SysCIRemaindContentDmnService : DmnServiceBase, ISysCIRemaindContentDmnService
    {
        public SysCIRemaindContentDmnService(INewtouchDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }

        public SysCIRemindContentVO GetnrFormJson(int keyvalue)
        {
            StringBuilder sql = new StringBuilder();
            IList<SqlParameter> list = null;
            sql.Append(@"SELECT  sfxjsnrbh ,
                        mzjsnr ,
                        zyjsnr ,
                        mzjsjb ,
                        zyjsjb ,
                        n.sfxm ,
                        sfxmmc ,
                        n.CreatorCode ,
                        n.CreateTime,
                        n.zt
                FROM    dbo.xt_sfxmjsnr n
                        LEFT JOIN dbo.xt_sfxm m ON m.sfxm = n.sfxm
                        WHERE   N.sfxjsnrbh =@keyvalue");
            if (!string.IsNullOrWhiteSpace(keyvalue.ToString()))
            {
                list = new List<SqlParameter>();
                list.Add(new SqlParameter("@keyvalue", keyvalue));
            }
            return this.FindList<SysCIRemindContentVO>(sql.ToString(), list.ToArray())[0];
        }

        public List<SysCIRemindContentVO> GetGridBySearch(Pagination pagination, string keyword)
        {
            StringBuilder sql = new StringBuilder();
            IList<SqlParameter> list = null;
            sql.Append(@"SELECT  sfxjsnrbh ,
                        mzjsnr ,
                        zyjsnr ,
                        mzjsjb ,
                        zyjsjb ,
                        n.sfxm ,
                        sfxmmc ,
                        n.CreatorCode ,
                        n.CreateTime
                FROM    dbo.xt_sfxmjsnr n
                        LEFT JOIN dbo.xt_sfxm m ON m.sfxm = n.sfxm
                        WHERE  1=1");
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                sql.Append(" and  N.sfxjsnrbh  like @keyword or m.sfxmmc like @keyword");
                list = new List<SqlParameter>();
                list.Add(new SqlParameter("@keyword", keyword));
            }
            //  return this.QueryWithPage<PatiChargeLogicVO>(strsql.ToString(), pagination, inSqlParameterList == null ? null : inSqlParameterList.ToArray()).ToList();
            return this.QueryWithPage<SysCIRemindContentVO>(sql.ToString(), pagination, list == null ? null : list.ToArray()).ToList();
        }
    }
}
