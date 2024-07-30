using System;

namespace Newtouch.Domain.ViewModels.Outpatient
{
    /// <summary>
    /// 输液信息
    /// </summary>
    public class MzsyypxxVO
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        public long Id { get; set; }
        public string syzxId { get; set; }
        public string cfId { get; set; }

        /// <summary>
        /// 患者姓名
        /// </summary>
        public string xm { get; set; }

        /// <summary>
        /// 患者就诊卡号
        /// </summary>
        public string kh { get; set; }

        /// <summary>
        /// 发票号
        /// </summary>
        public string fph { get; set; }

        /// <summary>
        /// 处方明细ID
        /// </summary>
        public int cfmxId { get; set; }

        /// <summary>
        /// 计算明细内码
        /// </summary>
        public int jsmxnm { get; set; }

        /// <summary>
        /// 床号/座号
        /// </summary>
        public string seatNum { get; set; }

        /// <summary>
        /// 收费时间  结算时间
        /// </summary>
        public DateTime sfsj { get; set; }

        /// <summary>
        /// 处方号
        /// </summary>
        public string cfh { get; set; }

        /// <summary>
        /// 药品代码
        /// </summary>
        public string ypCode { get; set; }

        /// <summary>
        /// 药品名称
        /// </summary>
        public string ypmc { get; set; }

        /// <summary>
        /// 药品规格
        /// </summary>
        public string ypgg { get; set; }

        /// <summary>
        /// 用量+用量单位
        /// </summary>
        public string ylStr { get; set; }

        /// <summary>
        /// 数量+数量单位
        /// </summary>
        public string slStr { get; set; }

        /// <summary>
        /// 剂量+剂量单位
        /// </summary>
        public string jlStr { get; set; }
        public string yfcode { get; set; }
        public string yfmc { get; set; }
        /// <summary>
        /// 组号
        /// </summary>
        public string groupNo { get; set; }

        /// <summary>
        /// 配药师工号
        /// </summary>
        public string dispenser { get; set; }

        /// <summary>
        /// 配药师名称
        /// </summary>
        public string dispenserName { get; set; }

        /// <summary>
        /// 执行者工号
        /// </summary>
        public string executor { get; set; }

        /// <summary>
        /// 执行者名称
        /// </summary>
        public string executorName { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string remark { get; set; }

        /// <summary>
        /// 输液开始时间
        /// </summary>
        public DateTime? sykssj { get; set; }

        /// <summary>
        /// 输液结束时间
        /// </summary>
        public DateTime? syjssj { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 创建者
        /// </summary>
        public string CreatorCode { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// 修改者
        /// </summary>
        public string LastModifierCode { get; set; }
        public int zcs { get; set; }
        public int yzxcs { get; set; }
        /// <summary>
        /// 皮试结果
        /// </summary>
        public string psjg { get; set; }
        /// <summary>
        /// 皮试标志 0:否 1：是
        /// </summary>
        public string ispsbz { get; set; }
        /// <summary>
        /// 留观标志
        /// </summary>

        public string islgbz { get; set; }
    }
}
