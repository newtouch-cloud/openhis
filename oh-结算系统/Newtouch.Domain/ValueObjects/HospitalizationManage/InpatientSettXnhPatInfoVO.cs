using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.ValueObjects
{
    /// <summary>
    /// 住院新农合结算 患者信息
    /// </summary>
    public class InpatientSettXnhPatInfoVO
    {
        /// <summary>
        /// 住院号
        /// </summary>
        public string zyh { get; set; }
        /// <summary>
        /// 患者主键
        /// </summary>
        public int patid { get; set; }
        /// <summary>
        /// 补偿序号
        /// </summary>
        public string inpId { get; set; }
        /// <summary>
        /// 住院医生
        /// </summary>
        public string doctor { get; set; }
        /// <summary>
        /// 患者姓名
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 医疗证卡号
        /// </summary>
        public string bookNo { get; set; }
        /// <summary>
        /// 家庭编号
        /// </summary>
        public string familyNo { get; set; }
        /// <summary>
        /// 个人编号
        /// </summary>
        public string memberNo { get; set; }
        /// <summary>
        /// 住院标志
        /// </summary>
        public string zybz { get; set; }
    }
}
