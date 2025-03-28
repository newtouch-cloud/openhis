using System;
using System.ComponentModel.DataAnnotations;

namespace Newtouch.HIS.Domain.ValueObjects
{
    public class SysMedicinePriceAdjustmentProfitLossVO
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        public string TjsyId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string yfbmCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Ypdm { get; set; }

        /// <summary>
        /// 批号
        /// </summary>
        public string Ph { get; set; }

        /// <summary>
        /// 调价时间
        /// </summary>
        public DateTime Tjsj { get; set; }

        /// <summary>
        /// 调价文件
        /// </summary>
        public string Tjwj { get; set; }

        /// <summary>
        /// 当时数量
        /// </summary>
        public int Dssl { get; set; }

        /// <summary>
        /// 原批发价
        /// </summary>
        public decimal Ypfj { get; set; }

        /// <summary>
        /// 原零售价
        /// </summary>
        public decimal Ylsj { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal Yykpfj { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal Yyklsj { get; set; }

        /// <summary>
        /// 现批发价
        /// </summary>
        public decimal Xpfj { get; set; }

        /// <summary>
        /// 现零售价
        /// </summary>
        public decimal Xlsj { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal Xykpfj { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal Xyklsj { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int Zhyz { get; set; }

        /// <summary>
        /// 操作员
        /// </summary>
        public string Czy { get; set; }

        /// <summary>
        /// 批价利润
        /// </summary>
        public decimal Pfjtjlr { get; set; }

        /// <summary>
        /// 零价利润
        /// </summary>
        public decimal Lsjtjlr { get; set; }

        /// <summary>
        /// 批次
        /// </summary>
        public string pc { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CreatorCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string LastModifierCode { get; set; }
        
        /// <summary>
        /// 药品名称
        /// </summary>
        public string ypmc { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ypCode { get; set; }

        /// <summary>
        /// 包装单位
        /// </summary>
        public string bzdw { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal mzcls { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string mzcldw { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal zycls { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zycldw { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string djdw { get; set; }
        /// <summary>
        /// 单位 把门诊和住院都考虑进来
        /// </summary>

        public string dw { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal zfbl { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string mzzybz { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string spm { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public string py { get; set; }
        /// <summary>
        /// 药品规格
        /// </summary>
        public string ypgg { get; set; }
        /// <summary>
        /// 厂家
        /// </summary>
        public string ycmc { get; set; }

        public string zt { get; set; }
    }
}

