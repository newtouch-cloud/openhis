using System;
using System.Collections.Generic;
using System.Data;
using Newtouch.HIS.Domain.Entity;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// 
    /// </summary>
    public interface IHospDrugBillingRepo : IRepositoryBase<HospDrugBillingEntity>
    {
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="hospItemFeeEntity"></param>
        /// <param name="keyValue"></param>
        void SubmitForm(HospDrugBillingEntity hospMedicinFeeEntity, int? keyValue);

        void ExecPartialSettleFeeDetail(string zyh,string jsnm,string czlx);
        void Updatezy_brxxexpand(string OrganizeId, string zyh);
        void Updatezyaddfee(string OrganizeId, decimal sl, string yfbm, string ypdm);
    }
}
