using Newtouch.Domain.ValueObjects;
using System.Collections.Generic;

namespace Newtouch.Domain.DTO.OutputDto
{
    /// <summary>
    /// 推送处方
    /// </summary>
    public class PrescriptionAPIDto
    {
        /// <summary>
        /// 1 药品 2项目
        /// </summary>
        public int cflx { get; set; }
        /// <summary>
        /// 处方类型细分 EnumCflx
        /// </summary>
        public int cflxxf { get; set; }
        /// <summary>
        /// 处方类型名称
        /// </summary>
        public string cflxmc { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string cfh { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int? ts { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool? djbz { get; set; }

        /// <summary>
        /// 单一挂号可能有不同的医生给开处方
        /// </summary>
        public string ys { get; set; }

        /// <summary>
        /// 单一挂号可能有不同的医生给开处方
        /// </summary>
        public string ks { get; set; }

        /// <summary>
        /// 领药药房Code
        /// </summary>
        public string lyyf { get; set; }

        /// <summary>
        /// 药品明细
        /// </summary>
        public List<MedicineItemVO> AddMedicineItems { get; set; }
        /// <summary>
        /// 项目明细
        /// </summary>
        public List<TreamentItemVO> AddTreamentItems { get; set; }
    }
}
