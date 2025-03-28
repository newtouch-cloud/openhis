using Newtouch.Domain.ViewModels;

namespace Newtouch.Application.Interface
{
    /// <summary>
    /// 门诊出诊设置
    /// </summary>
    public interface IVisitDeptSetApp
    {
        /// <summary>
        /// 提交表单
        /// </summary>
        /// <param name="vo"></param>
        /// <param name="userCode"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        string SubmitForm(VisitDeptDetail vo, string userCode, string organizeId);
    }
}