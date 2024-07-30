using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.BusinessObjects
{
    public class JS_ENTITY
    {
        /// <summary>
        /// 病人内码
        /// </summary>
        public int patid { get; set; }

        /// <summary>
        /// 挂号内码(作废)
        /// </summary>
        public int ghnm { get; set; }

        /// <summary>
        /// 挂号内码
        /// </summary>
        public List<int> ghnmList { get; set; }

        /// <summary>
        /// 是否自费
        /// </summary>
        public bool isZf { get; set; }

        /// <summary>
        /// 是否医保与自费转换
        /// </summary>
        public bool isZh { get; set; }

        /// <summary>
        /// 是否挂号
        /// </summary>
        public bool isGh { get; set; }

        /// <summary>
        /// 结算项目列表
        /// </summary>
        public List<jsxm_entity> jsxmList { get; set; }

        /// <summary>
        /// 结算类型
        /// </summary>
        public string jslx { get; set; }

        /// <summary>
        /// 发票号
        /// </summary>
        public string fph { get; set; }

        /// <summary>
        /// 家床号
        /// </summary>
        public int jch { get; set; }

        /// <summary>
        /// 病人性质(作废)
        /// </summary>
        public string brxz { get; set; }

        /// <summary>
        /// 病人性质编号
        /// </summary>
        public int brxzbh { get; set; }

        /// <summary>
        /// 医生工号（如果是造口袋项目需要传该值）
        /// </summary>
        public string ysgh { get; set; }

    }

    public class jsxm_entity
    {
        /// <summary>
        /// 大类
        /// </summary>
        public string dl { get; set; }

        /// <summary>
        /// 收费项目
        /// </summary>
        public string sfxm { get; set; }

        /// <summary>
        /// 单价
        /// </summary>
        public decimal dj { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public decimal sl { get; set; }

        /// <summary>
        /// 金额 = (服务费单价+单价)*数量
        /// </summary>
        public decimal je { get; set; }

        /// <summary>
        /// 自负比例
        /// </summary>
        public decimal zfbl { get; set; }

        /// <summary>
        /// 自负性质
        /// </summary>
        public string zfxz { get; set; }

        /// <summary>
        /// 减免比例
        /// </summary>
        public decimal jmbl { get; set; }

        /// <summary>
        /// 减免金额
        /// </summary>
        public decimal jmje { get; set; }

        /// <summary>
        /// 明细内码
        /// </summary>
        public int mxnm { get; set; }

        /// <summary>
        /// 明细编码
        /// </summary>
        //public string mxbm { get; set; }

        /// <summary>
        /// 处方明细内码
        /// </summary>
        public int cf_mxnm { get; set; }

        /// <summary>
        /// 处方号
        /// </summary>
        public string cfh { get; set; }

        /// <summary>
        /// 成组号
        /// </summary>
        public string czh { get; set; }

        //----------农保使用字段------------------//
        /// <summary>
        /// His名称（药品或诊疗项目名称）
        /// </summary>
        public string Hismc { get; set; }

        /// <summary>
        /// 用药中心项目编码（农保大类）
        /// </summary>
        public string Xmbm { get; set; }

        /// <summary>
        /// 药品单位
        /// </summary>
        public string Dw { get; set; }

        /// <summary>
        /// 药品规格
        /// </summary>
        public string gg { get; set; }

        /// <summary>
        /// 药品剂型
        /// </summary>
        public string Jx { get; set; }

        /// <summary>
        /// 操作员名称
        /// </summary>
        public string Czymc { get; set; }

        ///// <summary>
        ///// 每次剂量
        ///// </summary>
        //public string Jl { get; set; }

        ///// <summary>
        ///// 剂量单位
        ///// </summary>
        //public string Jldw { get; set; }

        ///// <summary>
        ///// 药品用法
        ///// </summary>
        //public string Ypyf { get; set; }

        ///// <summary>
        ///// 频次
        ///// </summary>
        //public string Pc { get; set; }

        ///// <summary>
        ///// 天数
        ///// </summary>
        //public string Ts { get; set; }

        /// <summary>
        /// 开单医生
        /// </summary>
        public string Kdys { get; set; }

        /// <summary>
        /// 开单科室
        /// </summary>
        public string Kdks { get; set; }

        /// <summary>
        /// 门诊诊断
        /// </summary>
        public string mzzd { get; set; }

        /// <summary>
        /// 就诊日期
        /// </summary>
        public DateTime jzrq { get; set; }


        //----------农保使用字段------------------//

        /// <summary>
        /// 服务费单价
        /// </summary>
        public decimal fwfdj { get; set; }

        /// <summary>
        /// 医保交易金额（计算之后得到的数据）
        /// </summary>
        public decimal jyje { get; set; }

        /// <summary>
        /// 医保交易范围金额（计算之后得到的数据）
        /// </summary>
        public decimal jyfwje { get; set; }

        /// <summary>
        /// 可报金额（计算之后得到的数据）
        /// </summary>
        public decimal kbje { get; set; }

        /// <summary>
        /// 分类自负（计算之后得到的数据）
        /// </summary>
        public decimal flzf { get; set; }

        /// <summary>
        /// 自理费用（计算之后得到的数据）
        /// </summary>
        public decimal zlfy { get; set; }

        //----------门诊医生站处方状态使用字段------------------//
        /// <summary>
        /// 医生站处方内码
        /// </summary>
        public int pt_cfnm { get; set; }
        //----------门诊医生站处方状态使用字段------------------//

    }
}
