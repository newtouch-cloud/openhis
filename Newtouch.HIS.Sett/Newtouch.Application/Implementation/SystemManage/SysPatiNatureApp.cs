using Newtouch.HIS.Domain.IRepository;
using Newtouch.Common.Operator;
using FrameworkBase.MultiOrg.Application;

namespace Newtouch.HIS.Application
{
    /// <summary>
    /// 
    /// </summary>
    public class SysPatiNatureApp : AppBase, ISysPatiNatureApp
    {
        private readonly ISysPatientNatureRepo _xt_brxzResposity;

        /// <summary>
        /// 获取条件下病人性质下拉框
        /// </summary>
        /// <returns></returns>
        public string GetbrxzSelect(string bxzc)
        {
            var orgId = OperatorProvider.GetCurrent().OrganizeId;
            return _xt_brxzResposity.GetbxzcSelect(bxzc, orgId);
        }
    }
}
