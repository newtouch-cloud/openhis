using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Repository;
using Newtouch.Core.Common;
using Newtouch.Core.Common.Exceptions;
using Newtouch.EMR.Domain.Entity;
using Newtouch.EMR.Domain.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.EMR.Repository
{
    public class bl_ElementDomain_DetailRepo : RepositoryBase<bl_ElementDomain_DetailEntity>, Ibl_ElementDomain_DetailRepo
    {
        /// <summary>
        /// 默认构造函数
        /// </summary>
        /// <param name="databaseFactory">EF上下文工厂</param>
        public bl_ElementDomain_DetailRepo(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }

        public void SubmitForm(bl_ElementDomain_DetailEntity entity, string keyValue)
        {

            if (!string.IsNullOrEmpty(keyValue))
            {
                int Id = Convert.ToInt32(keyValue);
                var dbEntity = this.FindEntity(Id);
                dbEntity.Table_Column_No = entity.Table_Column_No;
                dbEntity.Table_Column_Code = entity.Table_Column_Code;
                dbEntity.Table_Colunn_Name = entity.Table_Colunn_Name;
                dbEntity.Table_Column_Type = entity.Table_Column_Type;
                dbEntity.Element_Name = entity.Element_Name;
                dbEntity.Element_ID = entity.Element_ID;
                dbEntity.Element_Type = entity.Element_Type;
                dbEntity.Element_Type_Name = entity.Element_Type_Name;
                dbEntity.Sybz = entity.Sybz;
                dbEntity.ysmxId = entity.ysmxId;
                dbEntity.AreValue = entity.AreValue;
                dbEntity.Px = entity.Px;
                dbEntity.LastModifierCode = entity.LastModifierCode;
                dbEntity.LastModifyTime = DateTime.Now;
                dbEntity.Modify(Id);
                this.Update(dbEntity);
            }
            else
            {
                var check = this.FindEntity(p => p.Table_Column_Code == entity.Table_Column_Code && p.Zt == 1 && p.OrganizeId == entity.OrganizeId);
                if (check != null)
                {
                    throw new FailedException("病历字段已存在！");
                }
                entity.Zt = 1;
                entity.Create();
                this.Insert(entity);
            }
        }

        public void DeleteForm(string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                var dbEntity = this.FindEntity(Convert.ToInt32(keyValue));
                dbEntity.Zt = 0;
                dbEntity.Modify(Convert.ToInt32(keyValue));
                this.Update(dbEntity);
            }
        }

       
    }
}
