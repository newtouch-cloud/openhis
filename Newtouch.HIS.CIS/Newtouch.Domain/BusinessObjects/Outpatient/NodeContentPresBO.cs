using Newtouch.Domain.Entity;
using Newtouch.Domain.ValueObjects;
using System;
using System.Collections.Generic;

namespace Newtouch.Domain.BusinessObjects
{
    public class NodeContentPresBO
    {
        /// <summary>
        /// 处方主表
        /// </summary>
        public PrescriptionEntity presEntity { get; set; }

        /// <summary>
        /// 处方明细
        /// </summary>
        public List<PrescriptionDetailQueryVO> presDetailList { get; set; }

        /// <summary>
        /// 处方打印时间
        /// </summary>
        public DateTime? cfzhdysj { get; set; }

        /// <summary>
        /// 治疗单打印时间（仅康复）
        /// </summary>
        public DateTime? zldzhdysj { get; set; }

        /// <summary>
        /// 同步至HIS 的 result
        /// </summary>
        public string sendtohisResult { get; set; }
    }
}
