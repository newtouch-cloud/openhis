using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Repository;
using Newtouch.Core.Common;
using Newtouch.Domain.Entity;
using Newtouch.Domain.IRepository;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Repository
{
    public class SysMedicineProfitLossReasonRepo : RepositoryBase<SysMedicineProfitLossReasonEntity>, ISysMedicineProfitLossReasonRepo
    {
        public SysMedicineProfitLossReasonRepo(IDefaultDatabaseFactory databaseFactory) : base(databaseFactory)
        {
        }


        /// <summary>
        /// 根据损益类型查询损益原因
        /// </summary>
        /// <returns></returns>
        public List<SysMedicineProfitLossReasonEntity> GetLossProfitReasonListByType(string sylx)
        {
            List<SysMedicineProfitLossReasonEntity> list = null;
            string sylxstr = sylx + ",-1";
            if (!string.IsNullOrEmpty(sylx))
            {
                list = this.IQueryable().Where(a => new[] { sylx, "-1" }.Contains(a.sybz) && a.zt == "1").ToList();
            }
            else
            {
                list = this.IQueryable().Where(a => a.zt == "1").ToList();
            }
            return list;
        }

        /// <summary>
        /// 获取分页实体列表
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public IList<SysMedicineProfitLossReasonEntity> GetPagintionList(Pagination pagination, string keyword)
        {
            var sql = new StringBuilder(@"
            SELECT [syyyId]
                  ,[syyy]
                  ,[sybz]
                  ,[zt]
                  ,[px]
                  ,[CreatorCode]
                  ,[CreateTime]
                  ,[LastModifyTime]
                  ,[LastModifierCode]
              FROM [xt_ypsyyy](nolock) where 1=1
            ");
            var pars = new List<DbParameter>();
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                sql.Append(" and(1 = 2");
                sql.Append(" or syyy like @keyword");
                sql.Append(")");
                pars.Add(new SqlParameter("@keyword", "%" + keyword.Trim() + "%"));
            }
            return QueryWithPage<SysMedicineProfitLossReasonEntity>(sql.ToString(), pagination, pars.ToArray());
        }

        public void SubmitForm(SysMedicineProfitLossReasonEntity SyyyDTO, string keyValue)
        {
            var entity = new SysMedicineProfitLossReasonEntity
            {
                syyy = SyyyDTO.syyy,
                sybz = SyyyDTO.sybz,
                px = SyyyDTO.px,
                zt = SyyyDTO.zt == "true" ? "1" : "0"
            };
            if (!string.IsNullOrEmpty(keyValue))
            {
                entity.Modify();
                entity.syyyId = keyValue;
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
            Delete(p => p.syyyId == keyValue);
        }
    }
}
