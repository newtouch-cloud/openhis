/**********************************************************/
// Copyright (C) 2016 Newtouch 版权所有。 
// Description： 财务票据-预交金收据
// Author：HLF
// CreateDate： 2016/12/15 12:36:16 
//**********************************************************/
using Newtouch.HIS.Domain.Entity;
using System;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// 
    /// </summary>
    public interface IFinanceReceiptRepo : IRepositoryBase<FinanceReceiptEntity>
    {
        /// <summary>
        /// 根据工号获取可使用的收据号
        /// </summary>
        /// <param name="gh"></param>
        /// <returns></returns>
        string getDQSJH(string gh, string orgId);
        string getDQSJHNew(string gh, string orgId, out FinanceReceiptEntity outsjhEntity, out string outsjhType);
        string getDQSJHForpzh(string gh, string orgId);

        /// <summary>
        /// 根据工号获取凭证号数据Entity
        /// </summary>
        /// <param name="gh"></param>
        /// <returns></returns>
        FinanceReceiptEntity getSJKEntity(string gh, string orgId);


        /// <summary>
        ///  验证收据号
        /// </summary>
        /// <param name="sjhfull">完整的收据号</param>
        /// <param name="gh">工号</param>
        /// <returns></returns>
        bool checkSJH(string sjhfull, string gh, out FinanceReceiptEntity outEntity,out string type, string orgId);

        /// <summary>
        /// 判断当前收据号是否已被使用
        /// </summary>
        /// <param name="gh">工号</param>
        /// <param name="szm">首字母</param>
        /// <param name="sjh">收据号</param>
        /// <param name="outEntity">返回收据号实体</param>
        /// <returns></returns>
        bool checkSJIsUsedForUser(string gh, string szm, long sjh, out FinanceReceiptEntity outEntity,out string type, string orgId);

        /// <summary>
        /// 添加收据凭证号
        /// </summary>
        /// <param name="sjEntity"></param>
        void AddReceiptInfo(FinanceReceiptEntity sjEntity, string orgId);

        /// <summary>
        /// 修改收据凭证号
        /// </summary>
        /// <param name="sjEntity"></param>
        /// <param name="cwsjId"></param>
        void UpdateReceiptInfo(FinanceReceiptEntity sjEntity, string cwsjId);

        #region 分配收据号
        IList<FinanceReceiptEntity> GetListByUserCode(string lyry, string orgId);
        FinanceReceiptEntity GetFinancialInvoiceEntity(string Id, string orgId);
        void SubmitForm(FinanceReceiptEntity financialInvoiceEntity, string Id);
        void DeleteForm(string Id, string orgId);

        void VlidateRepeat(FinanceReceiptEntity entity, string orgId, string Id);
        #endregion

    }
}
