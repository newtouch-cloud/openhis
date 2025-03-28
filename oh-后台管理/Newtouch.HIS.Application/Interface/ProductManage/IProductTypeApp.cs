using Newtouch.HIS.Domain.Entity;

namespace Newtouch.HIS.Application.Interface
{
    /// <summary>
    /// product type operate
    /// </summary>
    public interface IProductTypeApp
    {
        /// <summary>
        /// submit unit maintenance form
        /// </summary>
        /// <param name="wzTypeEntity"></param>
        /// <param name="keyWord"></param>
        int SubmitForm(WzTypeEntity wzTypeEntity, string keyWord);
    }
}
