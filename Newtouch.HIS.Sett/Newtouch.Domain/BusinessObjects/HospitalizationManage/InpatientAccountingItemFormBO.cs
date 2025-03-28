using System;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.BusinessObjects
{
    /// <summary>
    /// 住院记账 项目 提交FormBO
    /// </summary>
    public class InpatientAccountingPlanItemDto
    {
        /// <summary>
        /// 记账计划明细Id
        /// </summary>
        public string jzjhmxId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string yzxz { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? EndDate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string sfxmCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int sl { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? duration { get; set; }

        /// <summary>
        /// 是否‘团体治疗’
        /// </summary>
        public string ttbz { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string bz { get; set; }

        /// <summary>
        /// 关联医生
        /// </summary>
        public IList<InpatientAccountingPlanItemDoctorDto> ysList { get; set; }

        /// <summary>
        /// 医嘱类型 1药品 2项目
        /// </summary>
        public string yzlx { get; set; }

        /// <summary>
        /// 单价（提交记账计划时的单价）
        /// </summary>
        public decimal? dj { get; set; }

        /// <summary>
        /// 康复类别
        /// </summary>
        public string kflb { get; set; }
        public int? zll { get; set; }
        public int? dwjls { get; set; }
        public int? jjcl { get; set; }
    }
}
