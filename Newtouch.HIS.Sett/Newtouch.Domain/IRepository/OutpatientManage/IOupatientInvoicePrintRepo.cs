using Newtouch.HIS.Domain.Entity;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// 
    /// </summary>
    public interface IOupatientInvoicePrintRepo : IRepositoryBase<OupatientInvoicePrintEntity>
    {
        OupatientInvoicePrintEntity SelectOutPatientInvoicePrintByJsnm(int jsnm, string orgId);
    }
}
