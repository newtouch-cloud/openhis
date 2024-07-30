using Newtouch.Herp.Domain.Entity;

namespace Newtouch.Herp.Application.Interface
{
    /// <summary>
    /// 供应商处理
    /// </summary>
    public interface ISupplierApp
    {
        /// <summary>
        /// submit Supplier maintenance form
        /// </summary>
        /// <param name="supplierEntity"></param>
        /// <param name="keyWord"></param>
        void SubmitForm(GysSupplierEntity supplierEntity, string keyWord);

        /// <summary>
        /// 快速创建生产商  返回生产商ID
        /// </summary>
        /// <param name="name"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        string CreateProducerQuick(string id, string name, string organizeId);
    }
}