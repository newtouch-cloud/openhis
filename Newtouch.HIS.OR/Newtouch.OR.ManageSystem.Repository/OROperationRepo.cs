using Newtouch.OR.ManageSystem.Domain.Entity;
using Newtouch.OR.ManageSystem.Domain.IRepository;
using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Repository;
using System.Collections.Generic;
using Newtouch.Core.Common;
using System.Text;
using System.Data.SqlClient;
using Newtouch.Core.Common.Exceptions;
using System.Linq;
using System;
using FrameworkBase.MultiOrg.Domain.IRepository;
using Newtouch.OR.ManageSystem.Domain.DTO.OutputDto;

namespace Newtouch.OR.ManageSystem.Repository
{
    /// <summary>
    /// 创 建：chl
    /// 日 期：2019-10-31 14:15
    /// 描 述：手术字典表
    /// </summary>
    public class OROperationRepo : RepositoryBase<OROperationEntity>, IOROperationRepo
    {

		private readonly ISysConfigRepo _sysConfigRepo;

		/// <summary>
		/// 默认构造函数
		/// </summary>
		/// <param name="databaseFactory">EF上下文工厂</param>
		public OROperationRepo(IDefaultDatabaseFactory databaseFactory, ISysConfigRepo SysConfigRepo)
            : base(databaseFactory)
        {
			_sysConfigRepo = SysConfigRepo;
		}

        /// <summary>
        /// 获取分页实体列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="keyword">筛选关键字</param>
        public IList<OROperationEntity> GetPagintionList(Pagination pagination, string keyword,string organizeId)
        {
            var sql = new StringBuilder();
            sql.Append("select * from OR_Operation(nolock) where zt != 0");
            List <SqlParameter> pars = null;
            if (!string.IsNullOrWhiteSpace(organizeId))
            {
                pars = pars ?? new List<SqlParameter>();
                sql.Append(" and organizeid = @organizeId");
                pars.Add(new SqlParameter("@organizeId", organizeId));
            }
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                pars = pars ?? new List<SqlParameter>();
                sql.Append(" and(1 = 2");
                sql.Append(" or ssdm like @keyword");
                sql.Append(" or ssmc like @keyword");
                sql.Append(")");
                pars.Add(new SqlParameter("@keyword", "%" + keyword.Trim() + "%"));
            }
            return this.QueryWithPage<OROperationEntity>(sql.ToString(), pagination, pars == null ? null : pars.ToArray());
        }

        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="keyValue">主键值</param>
        public int SubmitForm(OROperationEntity entity, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                if (this.IQueryable().Any(p => p.ssdm == entity.ssdm && p.Id != keyValue && p.zt=="1"))
                {
                    throw new FailedException("编号不可重复");
                }
                var dbEntity = this.FindEntity(keyValue);
                    //properties
                    dbEntity.ssdm = entity.ssdm;
                    dbEntity.ssmc = entity.ssmc;
                    dbEntity.zjm = entity.zjm;
                    dbEntity.ssjb = entity.ssjb;

                    dbEntity.Modify(keyValue);
                    return Update(dbEntity);
            }
            else
            {
                if (this.IQueryable().Any(p => p.ssdm == entity.ssdm))
                {
                    throw new FailedException("编号不可重复");
                }
                entity.Create(true);
                return Insert(entity);
            }
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键值</param>
        public int DeleteForm(string keyValue)
        {
            var dbEntity = this.FindEntity(keyValue);
            //properties
            dbEntity.zt = "0";
            dbEntity.Modify(keyValue);
            return Update(dbEntity);
        }

        /// <summary>
        /// 获取手术列表
        /// </summary>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        public IList<OROperationEntity> GetOperationList(string organizeId,string keyword)
        {
            string sql = @"select * from OR_Operation(nolock) where zt != 0";
            if (!string.IsNullOrWhiteSpace(organizeId))
            {
                sql += " and organizeid = @organizeId";
            }
            //if (!string.IsNullOrWhiteSpace(keyword))
            //{
                sql += " and zjm like @keyword";
            //}
            var result = this.FindList<OROperationEntity>(sql, new SqlParameter[] {
                new SqlParameter("@organizeId", organizeId),
                new SqlParameter("@keyword", "%" + keyword.Trim() + "%"),
            });
            return result;
        }


		/// <summary>
		/// 手术字典列表
		/// </summary>
		/// <param name="orgId"></param>
		/// <param name="keyword"></param>
		/// <returns></returns>
		public IList<SysOpListDto> OpList(string orgId, string keyword, bool type)
		{
			string sql = "";
			string dd = DateTime.Now.AddYears(-1).ToString("yyyy-MM-dd HH:mm:ss");
			var showItems = _sysConfigRepo.GetByCode("ShowOpItemsCount", orgId);

			if (type && string.IsNullOrWhiteSpace(keyword))
			{
				string count = showItems == null ? "100" : showItems.Value;
				sql = "select top " + count + " ssdm,ssmc,zjm,ssjb from [Newtouch_OR].[dbo].[OR_Operation] a with(nolock) " +
					"where OrganizeId=@orgId and zt='1'" ;

				var list = this.FindList<SysOpListDto>(sql, new SqlParameter[] {
				new SqlParameter("@orgId",orgId),
				new SqlParameter("@date",dd) });

				if (list == null || list.Count < Convert.ToInt32(count))
				{
					sql = @"select top " + count + " ssdm,ssmc,zjm,ssjb from [Newtouch_OR].[dbo].[OR_Operation] with(nolock) where OrganizeId=@orgId and zt='1' ";
				}
				else
				{
					return list;
				}
			}
			else
			{
				sql = @"select ssdm,ssmc,zjm,ssjb from [Newtouch_OR].[dbo].[OR_Operation] with(nolock) where OrganizeId=@orgId and zt='1' ";
			}

			if (!string.IsNullOrWhiteSpace(keyword))
			{
				sql += " and (ssdm like @kwd or ssmc like @kwd or zjm like @kwd)";
			}
			return this.FindList<SysOpListDto>(sql, new SqlParameter[] {
				new SqlParameter("@orgId",orgId),
				new SqlParameter("@kwd",string.IsNullOrWhiteSpace(keyword)==true? "":keyword+"%"),
				new SqlParameter("@date",dd)
			});
		}
	}
}