﻿using System;
using System.Collections.Generic;
using Newtouch.HIS.API.Common;

namespace Newtouch.PDS.Requset.Zyypyz
{
    /// <summary>
    /// 住院药品医嘱执行
    /// </summary>
    public class ZyypyzzxRequest : RequestBase
    {
        /// <summary>
        /// 组织机构ID
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// 医嘱列表
        /// </summary>
        public List<YzDetail> yzList { get; set; }

        /// <summary>
        /// 接口调用流水号
        /// </summary>
        public string ClientNo { get; set; }
    }

    /// <summary>
    /// 医嘱明细
    /// </summary>
    public class YzDetail
    {
        /// <summary>
        /// 医嘱ID
        /// </summary>
        public string yzId { get; set; }

        /// <summary>
        /// 领药序号
        /// </summary>
        public long lyxh { get; set; }

        /// <summary>
        /// 患者姓名
        /// </summary>
        public string patientName { get; set; }

        /// <summary>
        /// 住院号
        /// </summary>
        public string zyh { get; set; }

        /// <summary>
        /// 药品代码
        /// </summary>
        public string ypCode { get; set; }

        /// <summary>
        /// 药品名称
        /// </summary>
        public string ypmc { get; set; }

        /// <summary>
        /// 大类
        /// </summary>
        public string dl { get; set; }

        /// <summary>
        /// 数量（部门单位）
        /// </summary>
        public decimal sl { get; set; }

        /// <summary>
        /// 转化因子  sl*zhyz=最小单位数量
        /// </summary>
        public int zhyz { get; set; }

        /// <summary>
        /// 口服、静滴...
        /// </summary>
        public string zlff { get; set; }

        /// <summary>
        /// 执行时间：04,06,08...
        /// </summary>
        public string sjap { get; set; }

        /// <summary>
        /// 与执行数量对应
        /// </summary>
        public string pcmc { get; set; }

        /// <summary>
        /// 用量
        /// </summary>
        public decimal yl { get; set; }

        /// <summary>
        /// 用量单位
        /// </summary>
        public string yldw { get; set; }

        /// <summary>
        /// 医生工号
        /// </summary>
        public string ysgh { get; set; }

        /// <summary>
        /// 开始日期
        /// </summary>
        public DateTime ksrq { get; set; }

        /// <summary>
        /// 结束日期
        /// </summary>
        public DateTime jsrq { get; set; }

        /// <summary>
        /// 发药药房
        /// </summary>
        public string fyyf { get; set; }

        /// <summary>
        /// 医嘱性质 1：临时；2：长期
        /// </summary>
        public string yzxz { get; set; }

        /// <summary>
        /// 嘱托
        /// </summary>
        public string yzbz { get; set; }

        /// <summary>
        /// 执行数量
        /// </summary>
        public int zxsl { get; set; }

        /// <summary>
        /// 单价
        /// </summary>
        public decimal dj { get; set; }

        /// <summary>
        /// 金额
        /// </summary>
        public decimal je { get; set; }

        /// <summary>
        /// 自负比例
        /// </summary>
        public decimal? zfbl { get; set; }

        /// <summary>
        /// 自负性质
        /// </summary>
        public string zfxz { get; set; }

        /// <summary>
        /// 科室编码
        /// </summary>
        public string ksCode { get; set; }

        /// <summary>
        /// 病区编码
        /// </summary>
        public string bqCode { get; set; }

        /// <summary>
        /// 床位
        /// </summary>
        public string cw { get; set; }

        /// <summary>
        /// 医嘱执行申请人
        /// </summary>
        public string yzzxsqr { get; set; }

        /// <summary>
        /// 执行日期
        /// </summary>
        public DateTime zxrq { get; set; }

        /// <summary>
        /// 组号
        /// </summary>
        public int? zh { get; set; }

        /// <summary>
        /// 贴数
        /// </summary>
        public decimal? ts { get; set; }
        /// <summary>
        /// 医嘱类型
        /// </summary>
        public int? yzlx { get; set; }
    }
}
