using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Herp.Domain.ValueObjects.Purchase
{
    public class PurchaseStoreVO
    {

        public string productmc { get; set; }
        public string cglxmc { get; set; }
        public string psyqmc { get; set; }
        public string qymc { get; set; }
        public string productId { get; set; }


        public string cgmxId { get; set; }
        /// <summary>
        /// 采购编号
        /// </summary>
        public string cgId { get; set; }
        public string OrganizeId { get; set; }
        /// <summary>
        /// 药品编号
        /// </summary>
        public string productCode { get; set; }

        public string sxh { get; set; }
        public string cglx { get; set; }
        public string hctbdm { get; set; }
        public string hcxfdm { get; set; }
        public string yybddm { get; set; }
        public string cgggxh { get; set; }
        public string pssm { get; set; }
        public decimal cgsl { get; set; }
        public decimal cgdj { get; set; }
        public string qybm { get; set; }
        public string sfjj { get; set; }
        public string psyq { get; set; }
        public string dcpsbs { get; set; }
        public string cwxx { get; set; }
        public string bzsm { get; set; }


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
        /// 
        /// </summary>
        public string zt { get; set; }


        /// <summary>
        /// 物资ID
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 物资名称
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 库存 最小单位
        /// </summary>
        public int? kcsl { get; set; }

        /// <summary>
        /// 可用库存 最小单位
        /// </summary>
        public int? kykcsl { get; set; }

        /// <summary>
        /// 部门单位数量（带单位）
        /// </summary>
        public string slstr { get; set; }

        /// <summary>
        /// 最小单位零售价
        /// </summary>
        public decimal? minlsj { get; set; }

        /// <summary>
        /// 部门单位零售价
        /// </summary>
        public decimal? bmlsj { get; set; }

        /// <summary>
        /// 转化因子
        /// </summary>
        public int? zhyz { get; set; }

        /// <summary>
        /// 部门单位转化因子
        /// </summary>
        public int? bmdwzhyz { get; set; }

        /// <summary>
        /// 拼音
        /// </summary>
        public string py { get; set; }

        /// <summary>
        /// 规格
        /// </summary>
        public string gg { get; set; }

        /// <summary>
        /// 生产商Id
        /// </summary>
        public string supplierId { get; set; }

        /// <summary>
        /// 生产商名称
        /// </summary>
        public string supplierName { get; set; }

        /// <summary>
        /// 最小单位ID
        /// </summary>
        public string zxdwId { get; set; }

        /// <summary>
        /// 最小单位名称
        /// </summary>
        public string mindwmc { get; set; }

        /// <summary>
        /// 部门单位ID
        /// </summary>
        public string bmdwId { get; set; }

        /// <summary>
        /// 部门单位名称
        /// </summary>
        public string bmdwmc { get; set; }

        /// <summary>
        /// 类别名称
        /// </summary>
        public string lbmc { get; set; }

        /// <summary>
        /// 类别ID
        /// </summary>
        public string lbId { get; set; }
        /// <summary>
        /// 国家医保代码(耗材27位)
        /// </summary>
        public string hcgjybdm { get; set; }
        /// <summary>
        /// 耗材统编代码
        /// </summary>
        public string gjybdm { get; set; }
    }
}
