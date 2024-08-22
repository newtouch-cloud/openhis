using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeiMengGuYiBaoApp.Models.Output.YiBao
{
    public class Output_1101 : OutputBase
    { 
        public baseinfo baseinfo { get; set; }
        /// <summary>
        /// 表 7 输出-参保信息列表（节点标识 insuinfo）
        /// </summary>
        public List<insuinfo> insuinfo { get; set; }
        /// <summary>
        /// 表 8 输出-身份信息列表（节点标识：idetinfo）
        /// </summary>
        public List<idetinfo> idetinfo { get; set; }
    }

    public class baseinfo
    {
        /// <summary> 
        /// 1  psn_no  人员编号  字符型  30  Y 
        /// </summary>
        [Description("人员编号")]
        public string psn_no { get; set; }
        /// <summary>
        /// 2  psn_cert_type  人员证件类型  字符型  6  Y  Y 
        /// </summary>
        [Description("人员证件类型")]
        public string psn_cert_type { get; set; }
        /// <summary>
        /// 3  certno  证件号码  字符型  50  Y 
        /// </summary>
        [Description("证件号码")]
        public string certno { get; set; }
        /// <summary>
        /// 4  psn_name  人员姓名  字符型  50  Y 
        /// </summary>
        [Description("人员姓名")]
        public string psn_name { get; set; }
        /// <summary>
        /// 5  gend  性别  字符型  6  Y  Y 
        /// </summary>
        [Description("性别")]
        public string gend { get; set; }
        /// <summary>
        /// 6  naty  民族  字符型  3  Y
        /// </summary>
        [Description("民族")]
        public string naty { get; set; }
        /// <summary>
        /// 7  brdy  出生日期  日期型  yyyy-MM-dd
        /// </summary>
        [Description("出生日期")]
        public string brdy { get; set; }
        /// <summary>
        /// 8  age  年龄  数值型  4,1  Y 
        /// </summary>
        [Description("年龄")]
        public decimal age { get; set; }
        /// <summary>
        /// 字段扩展 4000  2021-11-9 +
        /// </summary>
        [Description("字段扩展")]
        public string expContent { get; set; }
    }

    public class insuinfo
    {

        /// <summary>
        /// 1  balc  余额  数值型  16,2  Y 
        /// </summary>
        [Description("余额")]
        public decimal balc { get; set; }
        /// <summary>
        /// 2  insutype  险种类型  字符型  6  Y  Y 
        /// </summary>
        [Description("险种类型")]
        public string insutype { get; set; }
        /// <summary>
        /// 3  psn_type  人员类别  字符型  6  Y  Y 
        /// </summary>
        [Description("人员类别")]
        public string psn_type { get; set; }

        /// <summary>
        /// 4  psn_insu_stas  人员参保状态  字符型  6  Y 
        /// </summary>
        [Description("人员参保状态")]
        public string psn_insu_stas { get; set; }

        /// <summary>
        /// 5  psn_insu_date 个人参保日期  日期型
        /// </summary>
        [Description("个人参保日期")]
        public string psn_insu_date { get; set; }
        /// <summary>
        /// 6  paus_insu_date  暂停参保日期  日期型 
        /// </summary>
        [Description("暂停参保日期")]
        public string paus_insu_date { get; set; }
        /// <summary>
        /// 7  cvlserv_flag  公务员标志  字符型  3  Y  Y 
        /// </summary>
        [Description("公务员标志")]
        public string cvlserv_flag { get; set; }
        /// <summary>
        /// 8  insuplc_admdvs  参保地医保区划  字符型  6  Y 
        /// </summary>
        [Description("参保地医保区划")]
        //public string insu_admdvs { get; set; }
        public string insuplc_admdvs { get; set; }
        /// <summary>
        /// 9  emp_name  单位名称  字符型  200 
        /// </summary>
        [Description("单位名称")]
        public string emp_name { get; set; }
    }

    public class idetinfo
    {
        /// <summary>
        /// 1 psn_idet_type人员身份类别字符型  3  Y  Y
        /// </summary>
        [Description("人员身份类别")]
        public string psn_idet_type { get; set; }
        /// <summary>
        /// 2 psn_type_lv人员类别等级字符型  3  Y  详见残疾等级字典
        /// </summary>
        [Description("人员类别等级")]
        public string psn_type_lv { get; set; }
        /// <summary>
        /// 3  memo  备注  字符型  500 
        /// </summary>
        [Description("备注")]
        public string memo { get; set; }
        /// <summary>
        /// 4  begntime  开始时间日期时间型Yyyyy-MM-dd HH:mm:ss
        /// </summary>
        [Description("开始时间")]
        public string begntime { get; set; }
        /// <summary>
        /// 5  endtime  结束时间日期时间型yyyy-MM-dd HH:mm:ss
        /// </summary>
        [Description("结束时间")]
        public string endtime { get; set; }
    }
}
