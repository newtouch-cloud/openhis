using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQYiBaoInterface.Models.Input
{
    public class InputPost<O>
    {

        /// <summary>
        ///  1  infno 交易编号  字符型  6  Y 交易编号详见接口列表
        /// </summary>
        [Description("交易编号")]
        public string infno { get; set; }
        /// <summary>
        ///  2  msgid 发送方报文 ID 字符型 30  Y定点医药机构编号(12)+时间(14)+顺序号(4)时间格式：yyyyMMddHHmmss
        /// </summary>
        [Description("发送方报文")]
        public string msgid { get; set; }
        /// <summary>
        ///   3 mdtrtarea_admvs 就医地医保区划 字符型  6  Y 
        /// </summary>
        [Description("就医地医保区划")]
        public string mdtrtarea_admvs { get; set; }
        /// <summary>
        ///  4  insuplc_admdvs 参保地医保区划 字符型  6 如果交易输入中含有人员编号，此项必填，可通过【1101】人员信息获取交易取得
        /// </summary>
        [Description("参保地医保区划")]
        public string insuplc_admdvs { get; set; }
        // 5  recer_sys_code 接收方系统代码 字符型 10  Y普通交易传 YBXT, 与人相关的交易时离休人员业务传 LXXT,工伤人员业务传 GSX
        [Description("接收方系统代码")]
        public string recer_sys_code { get; set; }
        /// <summary>
        ///     6  dev_no 设备编号  字符型 100 
        /// </summary>
        [Description("设备编号")]
        public string dev_no { get; set; }
        /// <summary>
        /// 7  dev_safe_info 设备安全信息 字符型 2000 
        /// </summary>
        [Description("设备安全信息")]
        public string dev_safe_info { get; set; }
        /// <summary>
        /// 8  cainfo 数字签名信息 字符型 1024 
        /// </summary>
        [Description("数字签名信息")]
        public string cainfo { get; set; }
        /// <summary>
        ///  9  signtype 签名类型  字符型 10  建议使用 SM2、SM3
        /// </summary>
        [Description("签名类型")]
        public string signtype { get; set; }
        /// <summary>
        ///  10  infver 接口版本号  字符型  6  Y例如：“V1.0”，版本号由医保下发通知。
        /// </summary>
        [Description("接口版本号")]
        public string infver { get; set; }
        /// <summary>
        /// 11  opter_type 经办人类别  3  Y Y1-经办人；2-自助终端；3-移动终端
        /// </summary>
        [Description("经办人类别")]
        public string opter_type { get; set; }
        /// <summary>
        /// 12  opter 经办人  字符型 30  Y 按地方要求传入经办人/终端编号
        /// </summary>
        [Description("经办人")]
        public string opter { get; set; }
        /// <summary>
        /// 13  opter_name 经办人姓名  字符型 50  Y按地方要求传入经办人姓名/终端名称
        /// </summary>
        [Description("经办人姓名")]
        public string opter_name { get; set; }
        /// <summary>
        ///  14  inf_time 交易时间日期时间型19  Y 
        /// </summary>
        [Description("交易时间日期时间型")]
        public string inf_time { get; set; }
        /// <summary>
        /// 15 fixmedins_code 定点医药机构编号字符型 30  Y 
        /// </summary>
        [Description("定点医药机构编号")]
        public string fixmedins_code { get; set; }
        /// <summary>
        /// 16 fixmedins_name定点医药机构名称字符型 200  Y 
        /// </summary>
        [Description("定点医药机构名称")]
        public string fixmedins_name { get; set; }
        /// <summary>
        /// 17  sign_no 交易签到流水号 字符型 30  通过签到【9001】交易获取
        /// </summary>
        [Description("交易签到流水号")]
        public string sign_no { get; set; }
        /// <summary>
        ///  18  input 交易输入  字符型 40000  Y
        /// </summary>
        //  [Description("交易输入")]
        // public string input { get; set; }

        /// <summary>
        ///  18  input 交易输入  字符型 40000  Y
        /// </summary>
        [Description("交易输入")]
        public input input { get; set; }
    }

    public class input
    {
        /// <summary>
        /// data
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]//控制如果值未空的话，转换JSON时候不出现
        public object data { get; set; }

        /// <summary>
        /// mdtrtinfo 就诊信息
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]//控制如果值未空的话，转换JSON时候不出现
        public object mdtrtinfo { get; set; }

        /// <summary>
        /// 费用信息
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]//控制如果值未空的话，转换JSON时候不出现
        public object feedetail { get; set; }

        /// <summary>
        /// 签到时用到
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]//控制如果值未空的话，转换JSON时候不出现
        public object signIn { get; set; }

        /// <summary>
        /// 入院登记修改
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]//控制如果值未空的话，转换JSON时候不出现
        public object adminfo { get; set; }

        /// <summary>
        /// 出院登记
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]//控制如果值未空的话，转换JSON时候不出现
        public object dscginfo { get; set; }

        /// <summary>
        /// 文件上传
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]//控制如果值未空的话，转换JSON时候不出现
        public object fsUploadIn { get; set; }

        /// <summary>
        /// 文件上传
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]//控制如果值未空的话，转换JSON时候不出现
        public object fsDownloadIn { get; set; }

        /// <summary>
        /// 诊断信息上传
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]//控制如果值未空的话，转换JSON时候不出现
        public object diseinfo { get; set; }
    }
}
