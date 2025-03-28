using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.ValueObjects.API
{
    public class SysRecipeDrugVO
    {
        /// <summary>
        /// 处方明细编号
        /// </summary>
        public int cfmxId { get; set; }
        /// <summary>
        /// 处方ID
        /// </summary>
        public int cfnm { get; set; }
        /// <summary>
        /// 组号
        /// </summary>
        public string czh { get; set; }
        /// <summary>
        /// 药品编号
        /// </summary>
        public string yp { get; set; }
        /// <summary>
        /// 药品通用名
        /// </summary>
        public string ypmc { get; set; }
        /// <summary>
        /// 药品商品名
        /// </summary>
        public string spm { get; set; }
        /// <summary>
        /// 包装规格数量
        /// </summary>
        public Decimal bzs { get; set; }
        /// <summary>
        /// 包装规格单
        /// </summary>
        public string bzdw { get; set; }
        /// <summary>
        /// 生产厂家名称
        /// </summary>
        public string ycmc { get; set; }
        /// <summary>
        /// 剂型编号
        /// </summary>
        public string jxCode { get; set; }
        /// <summary>
        /// 剂型
        /// </summary>
        public string jx { get; set; }
        /// <summary>
        /// 规格
        /// </summary>
        public string ypgg { get; set; }
        /// <summary>
        /// 单价
        /// </summary>
        public Decimal dj { get; set; }
        /// <summary>
        /// 发药数量
        /// </summary>
        public Decimal sl { get; set; }
        /// <summary>
        /// 总价
        /// </summary>
        public Decimal je { get; set; }
        /// <summary>
        /// 是否退药标志
        /// </summary>
        public string tybz { get; set; }
        /// <summary>
        /// 医保等级
        /// </summary>
        public string zfxz { get; set; }
    }
}
