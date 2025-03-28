using Newtouch.Core.Common;
using Newtouch.Infrastructure.EF.Attributes;
using System;
using System.Collections.Generic;

namespace Newtouch.Domain.ValueObjects
{
    public class MedicineInfoVO
    {
        public string yfbmmc { get; set; }
        public string kcId { get; set; }

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
        public string ypdm { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ph { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? yxq { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int kcsl { get; set; }

        /// <summary>
        /// 
        /// </summary>
        //public string ypkw { get; set; }

        /// <summary>
        /// 0：不控制1：控制
        /// </summary>
        //public short kzbz { get; set; }

        /// <summary>
        /// 已配药未发（领）药的数量
        /// </summary>
        public int djsl { get; set; }

        /// <summary>
        /// 0：启用1：停用
        /// </summary>
        public short tybz { get; set; }

        /// <summary>
        /// 来自 表 XT_YP_CRKMXK 
        /// </summary>
        //public string crkmxId { get; set; }

        /// <summary>
        /// 进价  药库单位
        /// </summary>
        [DecimalPrecision(11, 4)]
        public decimal? jj { get; set; }

        /// <summary>
        /// 对应药房药房的拆零数
        /// </summary>
        public int? zhyz { get; set; }

        /// <summary>
        /// 产地目录
        /// </summary>
        public int? cd { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string pc { get; set; }

        /// <summary>
        /// 是否已锁  0：未锁 ；    1/null：已锁
        /// </summary>
        public string locked { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zt { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? px { get; set; }

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


        public string Updating { get; set; }
        public string ksbm { get; set; }
        public string bqbm { get; set; }
        public string ksmc { get; set; }
        public string bqmc { get; set; }
        public string ypmc { get; set; }
    }

    public class MedicineInfoVO2
    {
        public string ypId { get; set; }
        public string ypCode { get; set; }

        public string ypmc { get; set; }

        public string ycmc { get; set; }

        public string ypxzmc { get; set; }

        public string jxmc { get; set; }
        public string ypgg { get; set; }

        public int kcsl { get; set; }
        public string Zcxh { get; set; }
        public string syzt { get; set; }

        /// <summary>
        /// 药库库存
        /// </summary>
        public decimal? YkKcsl { get; set; }

        /// <summary>
        /// 药库库位
        /// </summary>
        public string YkDw { get; set; }

        /// <summary>
        /// 部门单位
        /// </summaYkDwry>
        public string deptdw { get; set; }

        /// <summary>
        /// 零售价
        /// </summary>
        public decimal? lsj { get; set; }

        /// <summary>
        /// 批发价
        /// </summary>
        public decimal? pfj { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Ypkw { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Pxfs1 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Pxfs2 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int Kcsx { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int Kcxx { get; set; }
        public int Jhd { get; set; }
        public int Jhl { get; set; }
        public string yplb { get; set; }
        public string ypzt { get; set; }
        public int? Sysx { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string klsl { get; set; }

        /// <summary>
        /// 本部门药品ID XT_YP_BMYPXX 主键
        /// </summary>
        public string bmypId { get; set; }
        /// <summary>
        /// 有效期
        /// </summary>
        public System.DateTime? yxq { get; set; }

        public string yfbmCode { get; set; }
        public string yfbmmc { get; set; }
    }
    public class MedicineQueryResponse
    {

        public List<MedicineInfoVO2> list { get; set; }
        public Pagination pagination { get; set; }

    }
}
