using Newtouch.Herp.Domain.Entity;

namespace Newtouch.Herp.Application.Interface
{
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