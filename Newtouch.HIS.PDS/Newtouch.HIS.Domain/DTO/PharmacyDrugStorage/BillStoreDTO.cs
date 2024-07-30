using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.DTO.PharmacyDrugStorage
{
    public class BillStoreDTO
    {
        public string Id { get; set; }
        public string OrganizeId { get; set; }
        public string fph { get; set; }

        public string fprq { get; set; }
        public decimal fphszje { get; set; }
        public string yqbm { get; set; }
        public string yybm { get; set; }
        public string psdbm { get; set; }
        public string dlsgbz { get; set; }
        public string fpbz { get; set; }
        public string sfwpsfp { get; set; }
        public string wpsfpsm { get; set; }
        public string fpmxbh { get; set; }
        public string splx { get; set; }
        public string sfch { get; set; }
        public string zxspbm { get; set; }
        public string scph { get; set; }
        public string scrq { get; set; }
        public decimal spsl { get; set; }
        public string glmxbh { get; set; }
        public string xsddh { get; set; }
        public int? sxh { get; set; }
        public string yxrq { get; set; }
        public decimal wsdj { get; set; }
        public decimal hsdj { get; set; }
        public decimal sl { get; set; }
        public decimal se { get; set; }
        public decimal hsje { get; set; }
        public decimal pfj { get; set; }
        public decimal lsj { get; set; }
        public string pzwh { get; set; }


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


        public string dlsgbzmc { get; set; }
        public string sfwpsfpmc { get; set; }
        public string splxmc { get; set; }
        public string sfchmc { get; set; }

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
        /// 单位
        /// </summary>
        public string dw { get; set; }


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
        //public decimal? pfj { get; set; }

        ///// <summary>
        ///// 部门零售价
        ///// </summary>
        //public decimal? lsj { get; set; }

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
