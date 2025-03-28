using System;

namespace Newtouch.HIS.Domain.ValueObjects
{
    public class HandOutMedicinesrmVO
    {
        public string zt { get; set; }

        public string ypmc { get; set; }
        public string py { get; set; }

        public string ypgg { get; set; }

        public string ycmc { get; set; }

        public string dlmc { get; set; }

        /// <summary>
        /// 包装数
        /// </summary>
        public decimal bzs { get; set; }

        /// <summary>
        /// 包装单位
        /// </summary>
        public string bzdw { get; set; }

        /// <summary>
        /// 部门单位
        /// </summary>
        public string deptdw { get; set; }

        /// <summary>
        /// 有效期
        /// </summary>
        public DateTime? yxq { get; set; }

        /// <summary>
        /// 转换成部门单位的总可用数量
        /// </summary>
        public int? bmdwsl { get; set; }

        /// <summary>
        /// 部门单位
        /// </summary>
        public string bmdw { get; set; }

        /// <summary>
        /// 批号
        /// </summary>
        public string ph { get; set; }
        
        /// <summary>
        /// 批次
        /// </summary>
        public string pc { get; set; }

        /// <summary>
        /// 转化因子
        /// </summary>
        public int zhyz { get; set; }

        /// <summary>
        /// 药品代码
        /// </summary>
        public string ypcode { get; set; }

        /// <summary>
        /// 部门批发价
        /// </summary>
        public decimal pfj { get; set; }

        /// <summary>
        /// 部门零售价
        /// </summary>
        public decimal lsj { get; set; }

        /// <summary>
        /// 药库批发价
        /// </summary>
        public decimal ykpfj { get; set; }

        /// <summary>
        /// 药库零售价
        /// </summary>
        public decimal yklsj { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string klsl { get; set; }

        /// <summary>
        /// 库存数量
        /// </summary>
        public int kcsl { get; set; }

        /// <summary>
        /// 冻结数量
        /// </summary>
        public int? djsl { get; set; }

        /// <summary>
        /// 可用库存数量
        /// </summary>
        public int? kykcsl { get; set; }

        /// <summary>
        /// 最小单位
        /// </summary>
        public string zxdw { get; set; }

        /// <summary>
        /// 包装单位数量
        /// </summary>
        public int? bzdwsl { get; set; }

        /// <summary>
        /// 带单位的包装单位数量
        /// </summary>
        public string bzdwslstr { get; set; }

        /// <summary>
        /// 药库单位/包装单位
        /// </summary>
        public string ykdw { get; set; }

        /// <summary>
        /// 进价 部门单位
        /// </summary>
        public decimal? jj { get; set; }
    }
}
