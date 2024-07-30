using System;
using System.Collections.Generic;
using Newtouch.Common;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Infrastructure;
using System.Data.SqlClient;
using System.Linq;

namespace Newtouch.HIS.Repository
{
    /// <summary>
    /// 
    /// </summary>
    public class SysPharmacyDepartmentOpenMedicineRepo : RepositoryBase<SysPharmacyDepartmentOpenMedicineEntity>, ISysPharmacyDepartmentOpenMedicineRepo
    {
        public SysPharmacyDepartmentOpenMedicineRepo(IBaseDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }  

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="keyValue"></param>
        public void submitForm(SysPharmacyDepartmentOpenMedicineEntity entity, string keyValue)
        {
            if (string.IsNullOrWhiteSpace(keyValue))
            {
                entity.Id = Guid.NewGuid().ToString();
                entity.Create();
                this.Insert(entity);
            }
            else
            {
                entity.Modify();
                entity.Id = keyValue;
                this.Update(entity);
            }
        }
    }
}
