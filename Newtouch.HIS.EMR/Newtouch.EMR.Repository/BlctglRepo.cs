using Newtouch.EMR.Domain.Entity;
using Newtouch.EMR.Domain.IRepository;
using Newtouch.Infrastructure;
using System.Collections.Generic;
using Newtouch.Core.Common;
using System.Data.SqlClient;
using System.Text;
using FrameworkBase.MultiOrg.Repository;
using FrameworkBase.MultiOrg.Infrastructure;
using System.Linq;
using System;
using Newtouch.EMR.Domain.ValueObjects;

namespace Newtouch.EMR.Domain.Entity
{
    /// <summary>
    /// 创 建：hyj
    /// 日 期：2018-09-07 15:46
    /// 描 述：词条管理
    /// </summary>
    public class BlctglRepo : RepositoryBase<BlctglEntity>, IBlctglRepo
    {
        /// <summary>
        /// 默认构造函数
        /// </summary>
        /// <param name="databaseFactory">EF上下文工厂</param>
        public BlctglRepo(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
            
        }

     
        public List<BlctglEntity> GetTreeList(string orgId,int qx, string UserCode, string parentId)
        {
            return this.IQueryable().Where(p => p.OrganizeId == orgId &&p.zt=="1"&&p.qx== qx&&p.CreatorCode== UserCode&& p.parentId == parentId).OrderByDescending(p => p.CreateTime).ToList();
        }
        public BlctglEntity GetCitiaoByID(string orgId, string ID)
        {
              return this.FindEntity(p => p.ID == ID&&p.OrganizeId== orgId);
        }

        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="keyValue">主键值</param>
        public void SubmitForm(BlctglEntity entity, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                var dbEntity = this.FindEntity(keyValue);
                //properties
                dbEntity.ctnr = entity.ctnr;
                dbEntity.mc = entity.mc;
                dbEntity.LastModifyTime = DateTime.Now;
                dbEntity.LastModifierCode = entity.LastModifierCode;
                dbEntity.Modify(keyValue);
                this.Update(dbEntity);
            }
            else
            {
                entity.ID= Guid.NewGuid().ToString();
                entity.CreateTime = DateTime.Now;
                entity.zt = "1";
                entity.Create(true);
                this.Insert(entity);
            }
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键值</param>
        public void DeleteForm(string keyValue)
        {
            // this.Delete(p => p.ID == keyValue);

            var dbEntity = this.FindEntity(keyValue);
            dbEntity.zt = "0";
            this.Update(dbEntity);
        }


		public List<BLYXFHVO> Getyxfhlist(int ls)
		{
			//var response = new List<DSrepeatResponseDto>();
			var inParameters = new List<SqlParameter>();
			inParameters.Add(new SqlParameter("@ls", ls));
			//inParameters.Add(new SqlParameter("@zyh", zyh));
			//inParameters.Add(new SqlParameter("@yzpartable", SqlDbType.Structured) { Value = req.ToDataTable(), TypeName = "yzpartable" });
			return FindList<BLYXFHVO>("exec usp_se_tsfh @xsl=@ls", inParameters.ToArray());
		}
	}
}