using System;
using Newtouch.HIS.Proxy.guian.DTO.S25;

namespace Newtouch.HIS.Proxy.guian
{
    /// <summary>
    /// 贵安新农合 公共方法
    /// </summary>
    public static class Common
    {

        /// <summary>
        /// 计算新农合医保自负费用
        /// </summary>
        /// <param name="s25ResponseDto"></param>
        /// <returns></returns>
        public static decimal CalculationXnhZffy(S25ResponseDTO s25ResponseDto)
        {
            decimal res = 0;
            if (s25ResponseDto == null) return res;
            var qtfy = CalculationXnhQtbcfy(s25ResponseDto);
            return s25ResponseDto.totalCost - Convert.ToDecimal(s25ResponseDto.compensateCost) - qtfy;
        }

        /// <summary>
        /// 计算新农合医保账户支付金额
        /// </summary>
        /// <param name="s25ResponseDto"></param>
        /// <returns></returns>
        public static decimal CalculationXnhZhzf(S25ResponseDTO s25ResponseDto)
        {
            decimal res = 0;
            if (s25ResponseDto == null) return res;
            var qtfy = CalculationXnhQtbcfy(s25ResponseDto);
            return Convert.ToDecimal(s25ResponseDto.compensateCost) + qtfy;
        }

        /// <summary>
        /// 计算新农合其他补偿费用
        /// </summary>
        /// <param name="s25ResponseDto"></param>
        /// <returns></returns>
        public static decimal CalculationXnhQtbcfy(S25ResponseDTO s25ResponseDto)
        {
            decimal res = 0;
            if (s25ResponseDto == null) return res;
            return Convert.ToDecimal(s25ResponseDto.civilCost) +
            Convert.ToDecimal(s25ResponseDto.insureCost) +
            Convert.ToDecimal(s25ResponseDto.salvaJSCost) +
            Convert.ToDecimal(s25ResponseDto.bottomRedeem) +
            Convert.ToDecimal(s25ResponseDto.bottomSecondRedeem) +
            Convert.ToDecimal(s25ResponseDto.medicineCost) +
            Convert.ToDecimal(s25ResponseDto.salvaYFCost) +
            Convert.ToDecimal(s25ResponseDto.salvaCLCost) +
            Convert.ToDecimal(s25ResponseDto.salvaFPCost) +
            Convert.ToDecimal(s25ResponseDto.salvaJKCost) +
            Convert.ToDecimal(s25ResponseDto.continentInsuranceCost) +
            Convert.ToDecimal(s25ResponseDto.nurseCost);
        }
    }
}
