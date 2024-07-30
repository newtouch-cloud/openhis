using Newtouch.HIS.Domain.Entity;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// 
    /// </summary>
    public interface IFinancialInvoiceRepo : IRepositoryBase<FinancialInvoiceEntity>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="lyry"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        IList<FinancialInvoiceEntity> GetListByUserCode(string lyry, string orgId);

        /// <summary>
        /// 使用发票，数据库变更 同步当前发票号
        /// </summary>
        /// <param name="fph"></param>
        /// <param name="lyry"></param>
        /// <param name="updateEntity"></param>
        /// <param name="insertEntity"></param>
        /// <param name="orgId"></param>
        void UpdateCurrentGetEntitys(string fph, string lyry, out FinancialInvoiceEntity updateEntity, out FinancialInvoiceEntity insertEntity, string orgId);

        /// <summary>
        /// 获取所有发票列表
        /// </summary>
        /// <param name="keyValue"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        IList<FinancialInvoiceEntity> GetFinancialInvoiceList(string keyValue, string organizeId);

        /// <summary>
        /// 修改页面，根据主键获取实体
        /// </summary>
        /// <param name="fpdm"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        FinancialInvoiceEntity GetFinancialInvoiceEntity(string fpdm, string orgId);

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="financialInvoiceEntity"></param>
        /// <param name="fpdm"></param>
        void SubmitForm(FinancialInvoiceEntity financialInvoiceEntity, string fpdm);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="fpdm"></param>
        /// <param name="orgId"></param>
        void DeleteForm(string fpdm, string orgId);

        /// <summary>
        /// 验证数据合法性
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="orgId"></param>
        /// <param name="Id"></param>
        void VlidateRepeat(FinancialInvoiceEntity entity, string orgId, string Id);
    }
}
