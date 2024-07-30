using Newtouch.Herp.Domain.Entity;

namespace Newtouch.Herp.Application.Interface
{
    /// <summary>
    /// 出入库方式
    /// </summary>
    public interface IWzCrkfsApp
    {
        /// <summary>
        /// 表单提交
        /// </summary>
        /// <param name="wzCrkfsEntity"></param>
        /// <param name="keyWord"></param>
        /// <returns></returns>
        int SubmitForm(WzCrkfsEntity wzCrkfsEntity, string keyWord);
    }
}