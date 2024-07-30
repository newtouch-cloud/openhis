using Newtouch.Core.Common;
using Newtouch.HIS.Domain.DTO;
using Newtouch.HIS.Domain.DTO.InputDto;
using Newtouch.HIS.Domain.DTO.OutputDto;
using Newtouch.HIS.Domain.DTO.OutputDto.HospitalizationManage;
using Newtouch.HIS.Domain.DTO.OutputDto.OutpatientManage;
using Newtouch.HIS.Domain.ValueObjects;
using Newtouch.HIS.Domain.ValueObjects.HospitalizationManage;
using Newtouch.HIS.Proxy.guian.DTO;
using System;

namespace Newtouch.HIS.Application.Interface.HospitalizationManage
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDischargeSettleApp
    {
        #region 患者信息
        InpatientSettPatInfoVO GetInpatientSettlePatInfo(ref string zyh, string sfz = null, string kh = null, string cardType = null, string jslx = null, string orgId = null);
        #endregion

        #region 出院结算

        /// <summary>
        /// 住院号查询数据（包括病人信息和计费明细） zyh 或 kh +cardType 或 sfz +cardType
        /// </summary>
        /// <param name="zyh"></param>
        /// <param name="sfz"></param>
        /// <param name="kh"></param>
        /// <param name="cardType"></param>
        /// <returns></returns>
        InpatientSettPatStatusDetailDto GetInpatientSettleStatusDetail(string zyh, string sfz, string kh, string cardType, string jslx, string ver);

        /// <summary>
        /// 保存结算
        /// </summary>
        /// <param name="zyh"></param>
        /// <param name="expectedcyrq"></param>
        /// <param name="fph"></param>
        /// <param name="feeRelated">金额及支付信息</param>
        /// <param name="ybfeeRelated">医保相关费用信息</param>
        void SaveSett(string zyh, DateTime expectedcyrq, string fph, InpatientSettFeeRelatedDTO feeRelated
            , CQZyjs05Dto ybfeeRelated, S14OutResponseDTO xnhfeeRelated
            , string outTradeNo, string jslx, out int jsnm);
        /// <summary>
        /// 结算-接口版
        /// </summary>
        /// <param name="zyh"></param>
        /// <param name="expectedcyrq"></param>
        /// <param name="fph"></param>
        /// <param name="feeRelated"></param>
        /// <param name="ybfeeRelated"></param>
        /// <param name="xnhfeeRelated"></param>
        /// <param name="outTradeNo"></param>
        /// <param name="jslx"></param>
        /// <param name="orgId"></param>
        /// <param name="jsnm"></param>
        void SaveSett(string zyh, DateTime expectedcyrq, string fph, InpatientSettFeeRelatedDTO feeRelated
    , CQZyjs05Dto ybfeeRelated, S14OutResponseDTO xnhfeeRelated
    , string outTradeNo, string jslx, string orgId, out int jsnm);

        InpatientSettPatStatusDetailDto GetZyUploadDetail(string zyh, string jslx);

        InpatIentFeeSumVo GetInpatientSettleJe(string zyh, string sczt, DateTime? kssj, DateTime jssj, string xmmc);
        #endregion

        #region 取消出院结算
        /// <summary>
        /// 查询
        /// </summary>
        InpatientCancelSettPatStatusDetailDto GetCancelSettleStatusDetail(string zyh, string sfz, string kh, string cardType);

        /// <summary>
        /// 取消出院结算
        /// </summary>
        void DoCancel(string zyh, int expectedjsnm, string cancelReason, string cancelyblsh, out string outTradeNo, out decimal refundAmount);

        #endregion

        #region 模拟结算（GA）



        #endregion


        #region 中途结算
        void SavePartialSettle(string zyh, DateTime startTime, DateTime endTime, string fph, InpatientSettFeeRelatedDTO feeRelated
           , CQZyjs05Dto ybfeeRelated, string outTradeNo, string jslx, out int jsnm);

        InpatientSettPatStatusDetailDto GetPartialInpatientSettleStatusDetail(string zyh, string sfz, string kh, string cardType, string jslx, string ver);

        /// <summary>
        /// 转出院结算 预处理
        /// </summary>
        /// <param name="zyh"></param>
        string PreCYsettle(string zyh);
        #endregion
    }
}
