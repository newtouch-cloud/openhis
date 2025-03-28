using System.Linq;
using Newtouch.Common;
using Newtouch.Common.Exceptions;
using Newtouch.HIS.Application.Interface.SystemManage;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;

namespace Newtouch.HIS.Application.Implementation.SystemManage
{
    public class SysDepartmentApp : ISysDepartmentApp
    {
        private readonly ISysDepartmentRepository _sysDepartrepo;

        public SysDepartmentApp(ISysDepartmentRepository sysDepartrepo)
        {
            this._sysDepartrepo = sysDepartrepo;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public SysDepartmentEntity GetForm(string keyValue)
        {
            return _sysDepartrepo.FindEntity(keyValue);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="SysDepartmentEntity"></param>
        /// <param name="keyValue"></param>
        public void SubmitForm(SysDepartmentEntity SysDepartmentEntity, string keyValue)
        {
            SysDepartmentEntity oldEntity = null;   //变更前Entity
            if (!string.IsNullOrEmpty(keyValue))
            {
                if (_sysDepartrepo.IQueryable().Any(p => p.OrganizeId == SysDepartmentEntity.OrganizeId && p.Code == SysDepartmentEntity.Code && p.Id != keyValue))
                {
                    throw new FailedException("编号不可重复");
                }
                oldEntity = _sysDepartrepo.FindEntity(t => t.Id == keyValue);
                _sysDepartrepo.DetacheEntity(oldEntity);
                SysDepartmentEntity.Modify(keyValue);
                _sysDepartrepo.Update(SysDepartmentEntity);
                if (oldEntity != null)
                {
                    AppLogger.WriteEntityChangeRecordLog(oldEntity, SysDepartmentEntity, SysModuleEntity.GetTableName(), oldEntity.Id);
                }
            }
            else
            {
                if (_sysDepartrepo.IQueryable().Any(p => p.OrganizeId == SysDepartmentEntity.OrganizeId && p.Code == SysDepartmentEntity.Code))
                {
                    throw new FailedException("编号不可重复");
                }
                SysDepartmentEntity.Create(true);
                _sysDepartrepo.Insert(SysDepartmentEntity);
            }
        }

    }
}
