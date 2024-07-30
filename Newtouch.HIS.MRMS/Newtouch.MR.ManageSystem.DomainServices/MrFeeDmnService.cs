using FrameworkBase.MultiOrg.DmnService;
using FrameworkBase.MultiOrg.Infrastructure;
using Newtouch.Core.Common;
using Newtouch.MR.ManageSystem.Domain.Entity;
using Newtouch.MR.ManageSystem.Domain.IDomainServices;
using Newtouch.MR.ManageSystem.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.MR.ManageSystem.DomainServices
{
    public class MrFeeDmnService : DmnServiceBase, IMrFeeDmnService
    {
        public MrFeeDmnService(IDefaultDatabaseFactory databaseFactory)
               : base(databaseFactory)
        {

        }
        /// <summary>
        /// 获取病案收费大类三级分类的编码名称
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public IList<bafeeVO> GetPaginationList(Pagination pagination, string orgId, string keyword)
        {
            string sql = @"select a.Id as id1,b.id as id2,c.id as id3,a.code as code1,a.name as name1,b.code as code2,b.name as name2,c.code as code3,c.name as name3
from mr_dic_bafeetype a
left join mr_dic_bafeetype b on a.code=b.parentcode and a.organizeid=b.organizeid and b.zt='1'
left join mr_dic_bafeetype c on b.code=c.parentcode and b.organizeid=c.organizeid and c.zt='1'
where a.zt='1' ";
            var para = new List<SqlParameter>();

            if (!string.IsNullOrWhiteSpace(orgId))
            {
                sql += " and a.organizeId=@orgId";
            }
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                sql += " and(1 = 2";
                sql += " or a.code like @keyword";
                sql += " or a.name like @keyword";
                sql += " or b.code like @keyword";
                sql += " or b.name like @keyword";
                sql += " or c.code like @keyword";
                sql += " or c.name like @keyword";
                sql += ")";

                para.Add(new SqlParameter("@keyword", "%"+keyword+"%"));
            }
            para.Add(new SqlParameter("@orgId", orgId));
            sql += " and a.lev = 1 ";
            return this.QueryWithPage<bafeeVO>(sql.ToString(), pagination, para == null ? null : para.ToArray());
        }

        public bafeeVO GetFormJson(string keyValue,int index)
        {
            string sql = "select ";
            if (index == 1)
            {
                sql += "a.id,a.shortcode,a.Lev,a.ParentCode,a.code,a.px,";
            }
            else if (index == 2)
            {
                sql += "b.id,b.shortcode,b.Lev,b.ParentCode,b.code,b.px,";
            }
            else if (index == 3) {
                sql += "c.id,c.shortcode,c.Lev,c.ParentCode,c.code,c.px,";
            }
            sql += "a.code as code1,a.name as name1,b.code as code2,b.name as name2,c.code as code3,c.name as name3 from mr_dic_bafeetype a left join mr_dic_bafeetype b on a.code = b.parentcode and a.organizeid = b.organizeid and b.zt = '1'  left join mr_dic_bafeetype c on b.code = c.parentcode and b.organizeid = c.organizeid and c.zt = '1' where a.zt = '1' ";
            var para = new List<SqlParameter>();
            if (index == 1)
            {
                sql += "and a.id=@id";
            }
            else if (index == 2)
            {
                sql += "and b.id=@id";
            }
            else if (index == 3)
            {
                sql += "and c.id=@id";
            }
            para.Add(new SqlParameter("@id", keyValue));
            para.Add(new SqlParameter("@index", index));
            return this.FindList<bafeeVO>(sql, para.ToArray()).FirstOrDefault();
        }

        /// <summary>
        /// 获取一级收费大类
        /// </summary>
        /// <param name="code"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public IList<feeSelVO> GetFeeOne(string orgId)
        {
            string sql = @"select code,name from mr_dic_bafeetype with(nolock)
where zt='1'  and organizeid=@orgId and  Lev=1";
            
            return this.FindList<feeSelVO>(sql, new SqlParameter[] {
                new SqlParameter("@orgId",orgId)
            });
        }

        /// <summary>
        /// 获取二级收费大类
        /// </summary>
        /// <param name="code"></param>
        /// <param name="orgId"></param>
        /// <param name="parentCode"></param>
        /// <returns></returns>
        public IList<feeSelVO> GetFeeTwo( string orgId,string parentCode)
        {
            string sql = @"select code,name from mr_dic_bafeetype with(nolock)
where zt='1'  and organizeid=@orgId and  Lev=2";

            if (!string.IsNullOrWhiteSpace(parentCode))
            {
                sql += " and parentCode=@parentCode ";
            }

            return this.FindList<feeSelVO>(sql, new SqlParameter[] {
                new SqlParameter("@orgId",orgId),
                new SqlParameter("@parentCode",parentCode==null?"":parentCode)
            });
        }
	}
}
