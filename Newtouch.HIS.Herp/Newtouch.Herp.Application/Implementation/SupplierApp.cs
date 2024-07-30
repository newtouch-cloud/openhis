using Newtouch.Herp.Application.Interface;
using Newtouch.Herp.Domain.Entity;
using Newtouch.Herp.Domain.IRepository;
using Newtouch.Herp.Infrastructure.Enum;
using System;

namespace Newtouch.Herp.Application.Implementation
{
    /// <summary>
    /// 供应商处理
    /// </summary>
    public class SupplierApp : AppBase, ISupplierApp
    {
        private readonly IGysSupplierRepo _gysSupplierRepo;

        /// <summary>
        /// submit Supplier maintenance form
        /// </summary>
        /// <param name="supplierEntity"></param>
        /// <param name="keyWord"></param>
        public void SubmitForm(GysSupplierEntity supplierEntity, string keyWord)
        {
            if (string.IsNullOrWhiteSpace(keyWord))
            {
                supplierEntity.Create(true);
                _gysSupplierRepo.Insert(supplierEntity);
            }
            else
            {
                var dbSupplier = _gysSupplierRepo.FindEntity(p => p.Id == keyWord);
                if (dbSupplier == null) return;
                dbSupplier.address = supplierEntity.address;
                dbSupplier.fax = supplierEntity.fax;
                dbSupplier.jszq = supplierEntity.jszq;
                dbSupplier.khh = supplierEntity.khh;
                dbSupplier.khhzh = supplierEntity.khhzh;
                dbSupplier.name = supplierEntity.name;
                dbSupplier.supplierType = supplierEntity.supplierType;
                dbSupplier.py = supplierEntity.py;
                dbSupplier.sh = supplierEntity.sh;
                dbSupplier.tel = supplierEntity.tel;
                dbSupplier.zipCode = supplierEntity.zipCode;
                dbSupplier.zt = supplierEntity.zt;
                supplierEntity.Modify();
                _gysSupplierRepo.Update(dbSupplier);
            }
        }

        /// <summary>
        /// 快速创建生产商  返回生产商ID
        /// </summary>
        /// <param name="name"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        public string CreateProducerQuick(string id, string name, string organizeId)
        {
            var entityOld = _gysSupplierRepo.FindEntity(p => p.Id == id && p.OrganizeId == organizeId);
            if (entityOld != null && name.Equals(entityOld.name)) return id;
            entityOld = _gysSupplierRepo.FindEntity(p => p.name == name && p.OrganizeId == organizeId);
            if (entityOld != null) return entityOld.Id;
            var entity = new GysSupplierEntity
            {
                name = (name ?? "").Trim(),
                supplierType = (int)EnumSupplierType.Producer,
                OrganizeId = organizeId,
                py = "",
                zt = ((int)Enumzt.Enable).ToString()
            };
            entity.Create(true);
            return _gysSupplierRepo.Insert(entity) > 0 ? entity.Id : "";
        }
    }
}
