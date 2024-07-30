using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.EMR.Domain.ValueObjects.MedicalRecord
{
    public class BlHljlData
    {
        public string Id { get; set; }
        public string blId { get; set; }
        /// <summary>
        /// 记录日期
        /// </summary>
        public string jlrq { get; set; }
        /// <summary>
        /// 体温
        /// </summary>
        public string tw { get; set; }
        /// <summary>
        /// 脉搏
        /// </summary>
        public string mb { get; set; }
        /// <summary>
        /// 呼吸
        /// </summary>
        public string hx { get; set; }
        /// <summary>
        /// 血压
        /// </summary>
        public string xy { get; set; }
        /// <summary>
        /// 氧饱和度 
        /// </summary>
        public string ybhd { get; set; }
        /// <summary>
        /// 持续心电监测
        /// </summary>
        public string cxxdjc { get; set; }
        /// <summary>
        /// 吸氧        L/分
        /// </summary>
        public string xroyx { get; set; }
        /// <summary>
        /// 基护级别
        /// </summary>
        public string hljb { get; set; }
        /// <summary>
        /// 协助进食
        /// </summary>
        public string xzjs { get; set; }
        /// <summary>
        /// 拍背及有效咳痰
        /// </summary>
        public string pbjyxkt { get; set; }
        /// <summary>
        /// 压疮预防
        /// </summary>
        public string ycyf { get; set; }
        /// <summary>
        /// 跌倒预防
        /// </summary>
        public string ddyf { get; set; }
        /// <summary>
        /// 其他基护
        /// </summary>
        public string qtjh { get; set; }
        /// <summary>
        /// 专科护理
        /// </summary>
        public string zkhl { get; set; }
        /// <summary>
        /// 导管类别/措施
        /// </summary>
        public string dglb { get; set; }
        /// <summary>
        /// 护理指导
        /// </summary>
        public string hlzd { get; set; }
        /// <summary>
        /// 尿量     ml
        /// </summary>
        public string nl { get; set; }
        /// <summary>
        /// 胃液   ml
        /// </summary>
        public string wy { get; set; }
        /// <summary>
        /// 病情观察/护理措施效果
        /// </summary>
        public string bqhlcontent { get; set; }
        /// <summary>
        /// 签名
        /// </summary>
        public string hsqm { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string zt { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string CreatorCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? LastModifyTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string LastModifierCode { get; set; }
        public string OrganizeId { get; set; }
    }
}
