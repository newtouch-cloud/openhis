using Newtouch.Domain.Entity;
using Newtouch.Domain.IRepository;
using FrameworkBase.MultiOrg.Infrastructure;
using System.Collections.Generic;
using Newtouch.Core.Common;
using System.Data.SqlClient;
using System.Text;
using FrameworkBase.MultiOrg.Repository;
using System.Linq;

namespace Newtouch.Repository
{
    /// <summary>
    /// 创 建：张冬林
    /// 日 期：2018-06-28 13:27
    /// 描 述：住院医嘱套餐
    /// </summary>
    public class InpatientOrderPackageRepo : RepositoryBase<InpatientOrderPackageEntity>, IInpatientOrderPackageRepo
    {
        /// <summary>
        /// 默认构造函数
        /// </summary>
        /// <param name="databaseFactory">EF上下文工厂</param>
        public InpatientOrderPackageRepo(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
            
        }

        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="keyValue">主键值</param>
        public void SubmitForm(InpatientOrderPackageEntity entity, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                var dbEntity = this.FindEntity(keyValue);
                //properties
                
                dbEntity.Modify(keyValue);
                this.Update(dbEntity);
            }
            else
            {
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
            this.Delete(p => p.Id == keyValue);
        }

        public List<InpatientOrderPackageEntity> GettcTree(int tcfw,string yzlx,string orgId)
        {
            var alldata = this.IQueryable().Where(p => p.tcfw == tcfw && p.tclx == int.Parse(yzlx) && p.OrganizeId == orgId && p.zt == "1").ToList();//_inspectionTemplateDmnService.GetTemplateListByType(this.OrganizeId, jyjcmbLx);
            return alldata;
        }

    }
}