using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.ValueObjects.API
{
    public class SysDrugVO
    {
        /// <summary>
        /// 药品编号
        /// </summary>
        public  int ypId { get; set; }
        /// <summary>
        /// 药品通用名
        /// </summary>
        public string ypmc { get; set; }
        /// <summary>
        /// 药品商品名
        /// </summary>
        public string spm { get; set; }
        /// <summary>
        /// 生产厂家名称
        /// </summary>
        public string ycmc { get; set; }
        /// <summary>
        /// 规格
        /// </summary>
        public string ypgg { get; set; }
        /// <summary>
        /// 含量规格单位
        /// </summary>
        public string jldw { get; set; }
        /// <summary>
        /// 含量规格
        /// </summary>
        public decimal jl { get; set; }

        /// <summary>
        /// 剂型编码
        /// </summary>
        public string jxCode { get; set; }
        /// <summary>
        /// 剂型
        /// </summary>
        public string jx { get; set; }
        /// <summary>
        /// 包装规格单位
        /// </summary>
        public string bzdw { get; set; }
        /// <summary>
        /// 包装规格数量
        /// </summary>
        public decimal bzs { get; set; }
        /// <summary>
        /// 抗菌药物标记
        /// </summary>
        public string isKss { get; set; }
        /// <summary>
        /// 基本药物标记(固定值基本药物)
        /// </summary>
        public string jbywbj { get; set; }
        /// <summary>
        /// 药品单价
        /// </summary>
        public decimal lsj { get; set; }
        /// <summary>
        /// 医保等级
        /// </summary>
        public string zfxz { get; set; }
        /// <summary>
        /// 医保药品编码
        /// </summary>
        public string ypCode { get; set; }
    }
}
