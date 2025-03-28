using Newtouch.HIS.Domain.Entity;

namespace Newtouch.HIS.Application.Interface
{
    /// <summary>
    /// 物资单位
    /// </summary>
    public interface IWzUnitApp
    {
        /// <summary>
        /// submit unit maintenance form
        /// </summary>
        /// <param name="wzUnitEntity"></param>
        /// <param name="keyWord"></param>
        int SubmitForm(WzUnitEntity wzUnitEntity, string keyWord);
    }
}
