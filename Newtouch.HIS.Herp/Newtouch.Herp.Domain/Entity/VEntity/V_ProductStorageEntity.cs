﻿using System;

namespace Newtouch.Herp.Domain.Entity.VEntity
{
    /// <summary>
    /// 物资库存
    /// </summary>
    public class VProductStorageEntity
    {
        /// <summary>
        /// 物资ID
        /// </summary>
        public string productId { get; set; }

        /// <summary>
        /// 物资名称
        /// </summary>
        public string wzmc { get; set; }

        /// <summary>
        /// 拼音
        /// </summary>
        public string py { get; set; }

        /// <summary>
        /// 类别名称
        /// </summary>
        public string lb { get; set; }

        /// <summary>
        /// 部门带单位数量
        /// </summary>
        public string slStr { get; set; }

        /// <summary>
        /// 总库存  最小单位
        /// </summary>
        public int zkc { get; set; }

        /// <summary>
        /// 零售价
        /// </summary>
        public decimal lsj { get; set; }

        /// <summary>
        /// 零售总额
        /// </summary>
        public decimal lsze { get; set; }

        /// <summary>
        /// 进价单价（与kcxx.zhyz对应）
        /// </summary>
        public decimal jj { get; set; }

        /// <summary>
        /// 进价单价（最小单位）
        /// </summary>
        public decimal minjj { get; set; }

        /// <summary>
        /// 进价总额
        /// </summary>
        public decimal jjze { get; set; }

        /// <summary>
        /// 部门单位名称
        /// </summary>
        public string bmdwmc { get; set; }
        
        /// <summary>
        /// 最小单位名称
        /// </summary>
        public string zxdwmc { get; set; }

        /// <summary>
        /// 规格
        /// </summary>
        public string gg { get; set; }

        /// <summary>
        /// 品牌
        /// </summary>
        public string brand { get; set; }

        /// <summary>
        /// 生产厂商
        /// </summary>
        public string sccj { get; set; }

        /// <summary>
        /// 有效期 varchar（10）
        /// </summary>
        public string yxqStr { get; set; }

        /// <summary>
        /// 有效期
        /// </summary>
        public DateTime? yxq { get; set; }

        /// <summary>
        /// 转化因子
        /// </summary>
        public int zhyz { get; set; }

        /// <summary>
        /// 可用库存
        /// </summary>
        public int kykcsl { get; set; }

    }
}
