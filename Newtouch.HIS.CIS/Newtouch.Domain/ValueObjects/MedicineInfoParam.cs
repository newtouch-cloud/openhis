using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Domain.ValueObjects
{
    /// <summary>
    /// 药品查询条件
    /// </summary>
    public class MedicineInfoParam
    {
        /// <summary>
        /// 药品代码
        /// </summary>
        public string ypdm { get; set; }

        /// <summary>
        /// 药品类别
        /// </summary>
        public string yplb { get; set; }

        /// <summary>
        /// 药品熟悉
        /// </summary>
        public string ypsx { get; set; }

        /// <summary>
        /// 药品剂型
        /// </summary>
        public string ypjx { get; set; }

        /// <summary>
        /// 使用状态 控制、正常
        /// </summary>
        public string syzt { get; set; }

        /// <summary>
        /// 药品状态 停用、启用
        /// </summary>
        public string ypzt { get; set; }

        /// <summary>
        /// 输入码
        /// </summary>
        public string srm { get; set; }
    }
}
