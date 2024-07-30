using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using FrameworkBase.MultiOrg.Infrastructure;
using Newtouch.Core.Common;
using Newtouch.Herp.Domain.Entity;
using Newtouch.Herp.Domain.IRepository;
using Newtouch.Herp.Domain.ValueObjects;
using Newtouch.Herp.Infrastructure.Enum;
using Newtouch.Infrastructure;
using Newtouch.Infrastructure.EF;

namespace Newtouch.Herp.Repository
{
    /// <summary>
    /// 供应商联系人
    /// </summary>
    public class GysContactsRepo : RepositoryBase<GysContactsEntity>, IGysContactsRepo
    {
        public GysContactsRepo(IDefaultDatabaseFactory databaseFactory) : base(databaseFactory)
        {
        }

        /// <summary>
        /// insert contact
        /// </summary>
        /// <param name="supplierContactVo"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        public int InserContact(SupplierContactVO supplierContactVo, string organizeId)
        {
            if (supplierContactVo == null || string.IsNullOrWhiteSpace(supplierContactVo.contactName)) return 0;
            var entity = new GysContactsEntity
            {
                contactName = supplierContactVo.contactName,
                supplierId = supplierContactVo.keyWord,
                OrganizeId = organizeId,
                duties = supplierContactVo.duties,
                email = supplierContactVo.email,
                phone = supplierContactVo.phone,
                telphone = supplierContactVo.telphone,
                zt = ((int)Enumzt.Enable).ToString()
            };
            entity.Create(true);
            return Insert(entity);
        }

        /// <summary>
        /// update contact
        /// </summary>
        /// <param name="supplierContactVo"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        public int UpdateContact(SupplierContactVO supplierContactVo)
        {
            var dbContact = FindEntity(p => p.Id == supplierContactVo.id);
            if (dbContact == null)
            {
                return 0;
            }
            dbContact.contactName = supplierContactVo.contactName;
            dbContact.duties = supplierContactVo.duties;
            dbContact.email = supplierContactVo.email;
            dbContact.phone = supplierContactVo.phone;
            dbContact.telphone = supplierContactVo.telphone;
            dbContact.Modify();
            return Update(dbContact);
        }
    }
}
