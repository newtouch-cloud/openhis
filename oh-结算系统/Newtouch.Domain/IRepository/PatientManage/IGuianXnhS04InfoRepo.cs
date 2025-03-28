using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.Entity.PatientManage;
using Newtouch.HIS.Domain.ValueObjects.PatientManage;
using System;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// 
    /// </summary>
    public interface IGuianXnhS04InfoRepo : IRepositoryBase<GuianXnhS04InfoEntity>
    {
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="hospItemFeeEntity"></param>
        /// <param name="keyValue"></param>
        void SubmitForm(GuianXnhS04InfoEntity Entity, string keyValue);
    }
}
