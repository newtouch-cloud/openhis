using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.EMR.Domain.DTO.OutputDto.MRUpload
{
    /// <summary>
    /// 住院病案首页手术麻醉信息
    /// </summary>
    public class YB_7620
    {
        /// <summary>
        /// 住院病案首页手术麻醉信息
        /// 1
        /// </summary>
        public string AKC190 { get; set; }
        /// <summary>
        /// 病案流水号
        /// 1
        /// </summary>
        public string BKF303 { get; set; }
        /// <summary>
        /// 住院流水号
        /// 1
        /// </summary>
        public string BKC191 { get; set; }
        /// <summary>
        /// 手术序号
        /// 1
        /// </summary>
        public string BKF919 { get; set; }
        /// <summary>
        /// 手术操作代码 手术及操作(ICD-9-CM-3)
        /// 1
        /// </summary>
        public string BKF720 { get; set; }
        /// <summary>
        /// 手术操作名称
        /// 1
        /// </summary>
        public string BKF721 { get; set; }
        /// <summary>
        /// 手术操作日期时间 格式：yyyymmddhhmiss
        /// 1
        /// </summary>
        public string BKF723 { get; set; }
        /// <summary>
        /// 手术操作类别 1 手术 2 操作
        /// 1
        /// </summary>
        public string BKF922 { get; set; }
        /// <summary>
        /// 手术主次关系 1 主要手术；0 次要手术
        /// </summary>
        public string BKF960 { get; set; }
        /// <summary>
        /// 手术级别 参考《手术级别》
        /// 1
        /// </summary>
        public string BKF773 { get; set; }
        /// <summary>
        /// 手术切口类别代码 参考《手术切口类别》
        /// 1
        /// </summary>
        public string BKF283 { get; set; }
        /// <summary>
        /// 切口等级
        /// </summary>
        public string BKF920 { get; set; }
        /// <summary>
        /// 切口愈合等级代码
        /// </summary>
        public string BKF257 { get; set; }
        /// <summary>
        /// 愈合等级
        /// </summary>
        public string BKF921 { get; set; }
        /// <summary>
        /// 手术者代码
        /// </summary>
        public string BKF724 { get; set; }
        /// <summary>
        /// 手术者姓名
        /// </summary>
        public string BKF725 { get; set; }
        /// <summary>
        /// Ⅰ助姓名
        /// </summary>
        public string BKC769 { get; set; }
        /// <summary>
        /// Ⅱ助姓名
        /// </summary>
        public string BKC770 { get; set; }
        /// <summary> 
        /// 麻醉方式 参考《麻醉方式》
        /// </summary>
        public string BKC772 { get; set; }
        /// <summary>
        /// 麻醉医师
        /// </summary>
        public string BKF732 { get; set; }
        
    }
}
