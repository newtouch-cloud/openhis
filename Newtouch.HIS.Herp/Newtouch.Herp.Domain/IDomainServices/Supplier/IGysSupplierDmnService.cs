namespace Newtouch.Herp.Domain.IDomainServices
{
    /// <summary>
    /// 供应商操作
    /// </summary>
    public interface IGysSupplierDmnService
    {
        /// <summary>
        /// delete supplier
        /// </summary>
        /// <param name="id">库房Id</param>
        void DeleteSupplier(string id);
    }
}