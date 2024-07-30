using Newtouch.HIS.Domain.Entity.PharmacyDrugStorage;
using Newtouch.HIS.Domain.Entity.V;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.DTO.PharmacyDrugStorage
{
    /// <summary>
    /// 采购入库 
    /// </summary>
    public class PurchaseStoreDTO
    {
        public string cgmxId { get; set; }
        /// <summary>
        /// 采购编号
        /// </summary>
        public string cgId { get; set; }
        public string OrganizeId { get; set; }
        /// <summary>
        /// 药品编号
        /// </summary>
        public string ypCode { get; set; }
        /// <summary>
        /// 顺序号
        /// </summary>
        public int sxh { get; set; }
        /// <summary>
        /// 采购模式
        /// </summary>
        public string cglx { get; set; }
        /// <summary>
        /// 商品类型
        /// </summary>
        public string splx { get; set; }
        /// <summary>
        /// 统编代码
        /// </summary>
        public string zxspbm { get; set; }
        /// <summary>
        /// 采购计量单位
        /// </summary>
        public string cgjldw { get; set; }
        /// <summary>
        /// 规格包装
        /// </summary>
        public string ggbz { get; set; }
        /// <summary>
        /// 采购数量
        /// </summary>
        public decimal cgsl { get; set; }
        /// <summary>
        /// 采购单价
        /// </summary>
        public decimal cgdj { get; set; }
        /// <summary>
        /// 药企编码
        /// </summary>
        public string yqbm { get; set; }
        /// <summary>
        /// 多次配送标识（0不允许  1允许）
        /// </summary>
        public string dcpsbs { get; set; }
        /// <summary>
        /// 备注说明
        /// </summary>
        public string bzsm { get; set; }
        /// <summary>
        /// 单位
        /// </summary>
        public string dw { get; set; }

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
        /// 药企名称
        /// </summary>

        public string yqmc { get; set; }


        /// <summary>
        /// 药品代码
        /// </summary>
        public string ypdm { get; set; }

        /// <summary>
        /// 药品名称
        /// </summary>
        public string ypmc { get; set; }
    
        /// <summary>
        /// 大类名称
        /// </summary>
        public string dlmc { get; set; }

        /// <summary>
        /// 规格
        /// </summary>
        public string gg { get; set; }
        
        /// <summary>
        /// 带单位部门数量
        /// </summary>
        public string slStr { get; set; }
        

        /// <summary>
        /// 包装数单位
        /// </summary>
        public string bzdw { get; set; }

        /// <summary>
        /// 最小单位
        /// </summary>
        public string zxdw { get; set; }

        /// <summary>
        /// 转化因子
        /// </summary>
        public int? zhyz { get; set; }

        /// <summary>
        /// 包装数
        /// </summary>
        public int? bzs { get; set; }

        /// <summary>
        /// 部门批发价
        /// </summary>
        public decimal? pfj { get; set; }
        
        /// <summary>
        /// 部门零售价
        /// </summary>
        public decimal? lsj { get; set; }

        /// <summary>
        /// 最小单位零售价
        /// </summary>
        public decimal? zxdwlsj { get; set; }
        
        /// <summary>
        /// 药库批发价
        /// </summary>
        public decimal? ykpfj { get; set; }

        /// <summary>
        /// 药库零售价
        /// </summary>
        public decimal? yklsj { get; set; }

        /// <summary>
        /// 生产厂商
        /// </summary>
        public string sccj { get; set; }
        
        /// <summary>
        /// 零售价单价单位
        /// </summary>
        public string lsjdjdw { get; set; }
        
    }
}
