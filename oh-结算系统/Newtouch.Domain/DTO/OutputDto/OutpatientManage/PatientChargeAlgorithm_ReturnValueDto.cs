
using Newtouch.HIS.Domain.ValueObjects.OutpatientManage;
using System;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.DTO.OutputDto.OutpatientManage
{
    public class PatientChargeAlgorithm_ReturnValueDto
    {
        public PatientChargeAlgorithm_ReturnValueDto()
        {
            this.dlFymxList = new List<PatientChargeAlgorythm_Category_FeesDetailVO>();
        }

        /// <summary>
        /// 大类费用明细
        /// </summary>
        public List<PatientChargeAlgorythm_Category_FeesDetailVO> dlFymxList { get; set; }

        /// <summary>
        /// 医保结算范围费用总额
        /// </summary>
        public decimal ybjsfwze
        {
            get
            {
                if (dlFymxList == null)
                    return 0m;

                Dictionary<string, decimal> dlfy = new Dictionary<string, decimal>();

                decimal fy = 0m;
                foreach (PatientChargeAlgorythm_Category_FeesDetailVO fymx in this.dlFymxList)
                {
                    string key = fymx.dl;
                    if (!dlfy.ContainsKey(key))
                        dlfy.Add(key, 0);

                    dlfy[key] = dlfy[key] + (fymx.jzfy + fymx.flzf);
                }

                foreach (KeyValuePair<string, decimal> pair in dlfy)
                {
                    //四舍五入
                    fy += Math.Round(pair.Value, 2, MidpointRounding.AwayFromZero);
                }

                return fy;
            }
        }
    }
}
