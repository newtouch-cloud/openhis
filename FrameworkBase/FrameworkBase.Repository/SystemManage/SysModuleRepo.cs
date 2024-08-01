using FrameworkBase.Domain.Entity;
using FrameworkBase.Domain.IRepository;
using FrameworkBase.Infrastructure;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace FrameworkBase.Repository
{
    /// <summary>
    /// 创 建：张冬林
    /// 日 期：2017-11-17 16:13
    /// 描 述：系统菜单
    /// </summary>
    public sealed class SysModuleRepo : RepositoryBase<SysModuleEntity>, ISysModuleRepo
    {
        /// <summary>
        /// 默认构造函数
        /// </summary>
        /// <param name="databaseFactory">EF上下文工厂</param>
        public SysModuleRepo(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }

        /// <summary>
        /// 获取菜单列表
        /// </summary>
        /// <returns></returns>
        public List<SysModuleEntity> GetList()
        {
            var sql = new StringBuilder();
            sql.Append("select * from Sys_Module(nolock) where 1 = 1");
            List<SqlParameter> pars = null;
            sql.Append(" order by px");
            return this.FindList<SysModuleEntity>(sql.ToString(), pars == null ? null : pars.ToArray());
        }

        /// <summary>
        /// 获取有效菜单列表
        /// </summary>
        /// <returns></returns>
        public List<SysModuleEntity> GetValidList()
        {
            var sql = new StringBuilder();
            sql.Append("select * from Sys_Module(nolock) where 1 = 1 and zt = '1'");
            List<SqlParameter> pars = null;
            sql.Append(" order by px");
            return this.FindList<SysModuleEntity>(sql.ToString(), pars == null ? null : pars.ToArray());
        }

        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="keyValue">主键值</param>
        public void SubmitForm(SysModuleEntity entity, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                var dbEntity = this.FindEntity(keyValue);
                //properties
                dbEntity.ParentId = entity.ParentId;
                dbEntity.Name = entity.Name;
                dbEntity.EnName = entity.EnName;
                dbEntity.Code = entity.Code;
                dbEntity.Icon = entity.Icon;
                dbEntity.UrlAddress = entity.UrlAddress;
                dbEntity.Target = entity.Target;
                dbEntity.px = entity.px;
                dbEntity.Description = entity.Description;
                dbEntity.zt = entity.zt;

                dbEntity.Modify(keyValue);
                this.Update(dbEntity);
            }
            else
            {
                entity.Create(true);
                this.Insert(entity);
            }
        }

    }
}