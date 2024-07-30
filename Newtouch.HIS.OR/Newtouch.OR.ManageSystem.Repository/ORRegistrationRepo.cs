using Newtouch.OR.ManageSystem.Domain.Entity;
using Newtouch.OR.ManageSystem.Domain.IRepository;
using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Repository;
using Newtouch.OR.ManageSystem.Domain.ValueObjects;
using Newtouch.OR.ManageSystem.Infrastructure;

namespace Newtouch.OR.ManageSystem.Repository
{
    /// <summary>
    /// 创 建：chl
    /// 日 期：2019-11-06 10:22
    /// 描 述：手术登记
    /// </summary>
    public class ORRegistrationRepo : RepositoryBase<ORRegistrationEntity>, IORRegistrationRepo
    {
        /// <summary>
        /// 默认构造函数
        /// </summary>
        /// <param name="databaseFactory">EF上下文工厂</param>
        public ORRegistrationRepo(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
            
        }

        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="keyValue">主键值</param>
        public int SubmitForm(RegistrationListVO entity, string keyValue,string ssxh)
        {
            if (!(string.IsNullOrEmpty(keyValue) || keyValue == "null"))
            {
                var dbEntity = this.FindEntity(keyValue);
                //properties
                dbEntity.OrganizeId = entity.OrganizeId;
                dbEntity.Id = keyValue;
                dbEntity.ArrangeId = entity.arrangeId;
                dbEntity.Applyno = entity.applyno;
                dbEntity.ssxh = ssxh;
                dbEntity.zyh = entity.zyh;
                dbEntity.sqzt = "2";
                dbEntity.ryzd = entity.ryzd;
                dbEntity.ryzdmc = entity.ryzdmc;
                dbEntity.sszd = entity.sszd;
                dbEntity.sszdmc = entity.sszdmc;
                dbEntity.shbq = entity.shbq;
                dbEntity.ssmc = entity.ssmc;
                dbEntity.ssdm = entity.ssdm;
                dbEntity.ssapsj = entity.ssapsj;
                dbEntity.sssqsj = entity.sssqsj;
                dbEntity.sskssj = entity.sskssj;
                dbEntity.ssjssj = entity.ssjssj;
                dbEntity.AnesCode = entity.AnesCode;
                dbEntity.oproom = entity.oproom;
                dbEntity.oporder = entity.oporder;
                dbEntity.qkdj = entity.qkdj;
                dbEntity.isgl = entity.isgl;
                dbEntity.isjun = entity.isjun;
                dbEntity.shuxl = entity.shuxl;
                dbEntity.shixl = entity.shixl;
                dbEntity.zrxl = entity.zrxl;
                dbEntity.zcxl = entity.zcxl;
                dbEntity.ssbw = entity.ssbw;
                dbEntity.zt = "1";
                dbEntity.memo = entity.memo;
                dbEntity.Modify(keyValue);
                return Update(dbEntity);
            }
            else
            {
            ORRegistrationEntity regEntity = new ORRegistrationEntity();
            //properties
            regEntity.OrganizeId = entity.OrganizeId;
            regEntity.Id = keyValue;
            regEntity.ArrangeId = entity.arrangeId;
            regEntity.Applyno = entity.applyno;
            regEntity.ssxh = ssxh;
            regEntity.zyh = entity.zyh;
            regEntity.sqzt = "2";
            regEntity.ryzd = entity.ryzd;
            regEntity.ryzdmc = entity.ryzdmc;
            regEntity.sszd = entity.sszd;
            regEntity.sszdmc = entity.sszdmc;
            regEntity.shbq = entity.shbq;
            regEntity.ssmc = entity.ssmc;
            regEntity.ssdm = entity.ssdm;
            regEntity.ssapsj = entity.ssapsj;
            regEntity.sssqsj = entity.sssqsj;
            regEntity.sskssj = entity.sskssj;
            regEntity.ssjssj = entity.ssjssj;
            regEntity.AnesCode = entity.AnesCode;
            regEntity.oproom = entity.oproom;
            regEntity.oporder = entity.oporder;
            regEntity.qkdj = entity.qkdj;
            regEntity.isgl = entity.isgl;
            regEntity.isjun = entity.isjun;
            regEntity.shuxl = entity.shuxl;
            regEntity.shixl = entity.shixl;
            regEntity.zrxl = entity.zrxl;
            regEntity.zcxl = entity.zcxl;
            regEntity.ssbw = entity.ssbw;
            regEntity.zt = "1";
            regEntity.memo = entity.memo;
            regEntity.Create(true);
            return Insert(regEntity);
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
            dbEntity.sqzt = "3";
            dbEntity.Modify(keyValue);
            return Update(dbEntity);
        }
        
    }
}