using FrameworkBase.MultiOrg.Application;
using Newtouch.HIS.Application.Interface;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.PDS.Requset;

namespace Newtouch.HIS.Application.Implementation
{
    /// <summary>
    /// 患者基本信息管理
    /// </summary>
    public class PatientBaseInfoApp : AppBase, IPatientBaseInfoApp
    {
        private readonly IMzCfRepo _mzCfRepo;

        /// <summary>
        /// 补充门诊患者基本信息
        /// </summary>
        /// <param name="pbi"></param>
        /// <returns></returns>
        public string SupplementMzPatientBaseInfo(SupplementPatientBaseInfoRequest pbi)
        {
            if (pbi != null)
            {
                return _mzCfRepo.UpdateGender(pbi.cfh, pbi.xb, pbi.OrganizeId) > 0 ? "" : string.Format("根据处方号：【{0}】、组织机构ID：【{1}】、性别：【{2}】未找到可更新记录", pbi.cfh, pbi.OrganizeId, pbi.xb);
            }

            return "传入患者基本信息不能为空！";
        }
    }
}