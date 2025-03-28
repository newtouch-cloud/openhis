
namespace Newtouch.HIS.Domain.DTO.OutputDto.OutpatientManage
{
    public class PatientChargeAlgorythm_ReturnValue_YBANDFYBDto
    {
        public PatientChargeAlgorythm_ReturnValue_YBANDFYBDto()
        {
            YB = new PatientChargeAlgorithm_ReturnValueDto();
            FYB = new PatientChargeAlgorithm_ReturnValueDto();
        }
        /// <summary>
        /// 需要医保交易
        /// </summary>
        public PatientChargeAlgorithm_ReturnValueDto YB { get; set; }

        /// <summary>
        /// 不需要医保交易
        /// </summary>
        public PatientChargeAlgorithm_ReturnValueDto FYB { get; set; }

        /// <summary>
        /// 医保结算范围费用总额
        /// </summary>
        public decimal ybjsfwze
        {
            get
            {
                return YB.ybjsfwze;
            }
        }
    }
}
