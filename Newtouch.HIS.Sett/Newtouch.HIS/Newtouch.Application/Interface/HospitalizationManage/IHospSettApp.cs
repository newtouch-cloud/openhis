using Newtouch.HIS.Domain.BusinessObjects;
using System;
using System.Collections.Generic;
using Newtouch.HIS.Domain.DTO.OutputDto;

namespace Newtouch.HIS.Application.Interface
{
    /// <summary>
    /// 住院结算 接口
    /// </summary>
    public interface IHospSettApp
    {
        /// <summary>
        /// 住院结算，查看结算信息
        /// </summary>
        /// <param name="zyh"></param>
        /// <param name="kh"></param>
        HosSettPatStatusDetailDto GetPatHospStatusDetail(string zyh, string kh);

        /// <summary>
        /// 住院结算 病人 分类收费 预览
        /// </summary>
        /// <param name="zyh"></param>
        /// <param name="jsje">结算金额 防止过程中的费用变更</param>
        HosSettPatClassifyChargePreviewDto GetHospSettPatClassifyChargePreview(string zyh, DateTime expectedcyrq, decimal jsje);

        /// <summary>
        /// 根据住院号获取 本次结算 计费 按大类（分类项目）
        /// </summary>
        /// <param name="zyh"></param>
        /// <returns></returns>
        IList<HospFeeClassifyWithDLBO> GetHospFeeClassifyWithDLBOList(string zyh);

        /// <summary>
        /// 提交结算，保存结果
        /// </summary>
        /// <param name="zyh"></param>
        /// <param name="expectedjsje"></param>
        /// <param name="expectedzhaoling"></param>
        /// <param name="xjzfListStr"></param>
        SettSaveSuccessResultDto SaveSett(string zyh, DateTime expectedcyrq, string fph, decimal expectedyjjzhye, decimal expectedjsje, decimal expectedzhaoling, string xjzfListStr, decimal shishoukuan);

    }
}
