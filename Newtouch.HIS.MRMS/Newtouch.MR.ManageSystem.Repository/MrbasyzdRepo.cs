using Newtouch.MR.ManageSystem.Domain.Entity;
using Newtouch.MR.ManageSystem.Domain.IRepository;
using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Repository;

namespace Newtouch.MR.ManageSystem.Repository
{
    /// <summary>
    /// 创 建：chl
    /// 日 期：2019-11-20 11:05
    /// 描 述：病案首页诊断表
    /// </summary>
    public class MrbasyzdRepo : RepositoryBase<MrbasyzdEntity>, IMrbasyzdRepo
    {
        /// <summary>
        /// 默认构造函数
        /// </summary>
        /// <param name="databaseFactory">EF上下文工厂</param>
        public MrbasyzdRepo(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
            
        }

        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="keyValue">主键值</param>
        public void SubmitForm(MrbasyzdEntity entity, string keyValue)
        {
            if(entity!=null)
            {

                if (!string.IsNullOrEmpty(keyValue))
                {
                    var dbEntity = this.FindEntity(keyValue);
                    dbEntity.CYQK = entity.CYQK;
                    dbEntity.CYQKMS = entity.CYQKMS;
                    dbEntity.JBDM = entity.JBDM;
                    dbEntity.JBMC = entity.JBMC;
                    dbEntity.ZDOrder = entity.ZDOrder;
                    dbEntity.RYBQ = entity.RYBQ;
                    dbEntity.RYBQMS = entity.RYBQMS;
                    dbEntity.zt = entity.zt;

                    dbEntity.Modify(keyValue);
                    Update(dbEntity);
                }
                entity.Create(true);
                Insert(entity);
            }

        }

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键值</param>
        public int DeleteForm(string keyValue)
        {
            return Delete(p => p.Id == keyValue);
        }
    }
}