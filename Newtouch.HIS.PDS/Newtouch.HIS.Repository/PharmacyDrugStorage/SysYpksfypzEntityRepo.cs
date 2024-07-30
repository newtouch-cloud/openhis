using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Repository;
using Newtouch.Core.Common;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.HIS.Domain.VO;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Text;

namespace Newtouch.HIS.Repository
{
    /// <summary>
    /// xt_yp_fypz
    /// </summary>
    public class SysYpksfypzEntityRepo : RepositoryBase<SysYpksfypzEntity>, ISysYpksfypzEntityRepo
    {
        public SysYpksfypzEntityRepo(IDefaultDatabaseFactory databaseFactory): base(databaseFactory)
        {
        }
        /// <summary>
        /// 获取科室名称
        /// </summary>
        /// <returns></returns>
        public List<CodeNameVO> GetCodeName(string organizeId, int yjbmjb, string keyword = null)
        {
            var sqlStr = new StringBuilder(@"
SELECT Distinct [Code] as code
      ,[Name] as name
  FROM [NewtouchHIS_Base].[dbo].[Sys_Department]
  where zt = '1'
  and OrganizeId=@organizeId
            ");
            var pars = new List<DbParameter>();
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                 sqlStr.AppendLine( @" and ( name like @keyword or py like @keyword)");
                pars.Add(new SqlParameter("@keyword", "%"+keyword+"%"));
            }
            
            pars.Add(new SqlParameter("@organizeId", organizeId));
            return FindList<CodeNameVO>(sqlStr.ToString(), pars.ToArray());
        }
        /// <summary>
        /// 获取分页实体列表
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public IList<SysYpksfypzEntity> GetPagintionList(Pagination pagination, string keyword,string organizeId)
        {
            var sql = new StringBuilder(@"
SELECT a.[Id]
      ,a.[OrganizeId]
      ,b.yfbmmc as yfCode
      ,c.Name as ksCode
      ,a.[CreatorCode]
      ,a.[CreateTime]
      ,a.[zt]
  FROM [NewtouchHIS_PDS].[dbo].[xt_yp_ksfypz] a 
  join [NewtouchHIS_Base].[dbo].[xt_yfbm] b on a.yfCode=b.yfbmCode and b.zt='1' and b.OrganizeId=@organizeId
  left join [NewtouchHIS_Base].[dbo].[Sys_Department](nolock) c on c.Code=a.ksCode and c.OrganizeId=@OrganizeId
  where a.OrganizeId = @organizeId
            ");
            var pars = new List<DbParameter>();
            pars.Add(new SqlParameter("@organizeId", organizeId));
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                sql.Append(" and(1 = 2");
                sql.Append(" or b.yfbmmc like @keyword");
                sql.Append(")");
                pars.Add(new SqlParameter("@keyword", "%" + keyword.Trim() + "%"));
            }
            return QueryWithPage<SysYpksfypzEntity>(sql.ToString(), pagination, pars.ToArray());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="FypzDTO"></param>
        /// <param name="keyValue"></param>
        public void SubmitForm(SysYpksfypzEntity FypzDTO, string keyValue)
        {
            var entity = new SysYpksfypzEntity
            {
                ksCode = FypzDTO.ksCode,
                yfCode = FypzDTO.yfCode,
                OrganizeId = FypzDTO.OrganizeId,
                zt = FypzDTO.zt == "true" ? "1" : "0"
            };
            if (!string.IsNullOrEmpty(keyValue))
            {
                entity.Modify();
                entity.Id = keyValue;
                Update(entity);
            }
            else
            {
                entity.Create(true);
                Insert(entity);
            }
        }
        public void DeleteForm(string keyValue)
        {
            Delete(p => p.Id == keyValue);
        }
    }
}
