using System;
using System.Collections.Generic;

namespace NeiMengGuYiBaoApp.Models.Output.YiBao
{
    public class Output_4208 : OutputBase
    {
        public List<Output4208> data { get; set; }
    }

    /// <summary>
    /// 自费病人就诊信息（节点标识：data）
    /// </summary>
    public class Output4208
    {
        /// <summary>
        /// 医药机构就诊 ID (长度: 30)
        /// </summary>
        public string fixmedinsMdtrtId { get; set; }

        /// <summary>
        /// 定点医药机构编号 (长度: 30)
        /// </summary>
        public string fixmedinsCode { get; set; }

        /// <summary>
        /// 定点医药机构名称 (长度: 200)
        /// </summary>
        public string fixmedinsName { get; set; }

        /// <summary>
        /// 人员证件类型 (长度: 6)
        /// </summary>
        public string psnCertType { get; set; }

        /// <summary>
        /// 证件号码 (长度: 600)
        /// </summary>
        public string certno { get; set; }

        /// <summary>
        /// 人员姓名 (长度: 50)
        /// </summary>
        public string psnName { get; set; }

        /// <summary>
        /// 性别 (长度: 6)
        /// </summary>
        public string gend { get; set; }

        /// <summary>
        /// 民族 (长度: 3)
        /// </summary>
        public string naty { get; set; }

        /// <summary>
        /// 出生日期 (格式: yyyy-MM-dd)
        /// </summary>
        public DateTime brdy { get; set; }

        /// <summary>
        /// 年龄 (数值型: 4,1)
        /// </summary>
        public decimal age { get; set; }

        /// <summary>
        /// 联系人姓名 (长度: 50)
        /// </summary>
        public string conerName { get; set; }

        /// <summary>
        /// 联系电话 (长度: 50)
        /// </summary>
        public string tel { get; set; }

        /// <summary>
        /// 联系地址 (长度: 500)
        /// </summary>
        public string addr { get; set; }

        /// <summary>
        /// 开始时间 (格式: yyyy-MM-dd HH:mm:ss)
        /// </summary>
        public DateTime begntime { get; set; }

        /// <summary>
        /// 结束时间 (格式: yyyy-MM-dd HH:mm:ss)
        /// </summary>
        public DateTime endtime { get; set; }

        /// <summary>
        /// 医疗类别 (长度: 6)
        /// </summary>
        public string medType { get; set; }

        /// <summary>
        /// 住院/门诊号 (长度: 30)
        /// </summary>
        public string iptOtpNo { get; set; }

        /// <summary>
        /// 病历号 (长度: 30)
        /// </summary>
        public string medrcdno { get; set; }

        /// <summary>
        /// 主诊医师代码 (长度: 30)
        /// </summary>
        public string chfpdrCode { get; set; }

        /// <summary>
        /// 入院诊断描述 (长度: 200)
        /// </summary>
        public string admDiagDscr { get; set; }

        /// <summary>
        /// 入院科室编码 (长度: 30)
        /// </summary>
        public string admDeptCodg { get; set; }

        /// <summary>
        /// 入院科室名称 (长度: 100)
        /// </summary>
        public string admDeptName { get; set; }

        /// <summary>
        /// 入院床位 (长度: 30)
        /// </summary>
        public string admBed { get; set; }

        /// <summary>
        /// 病区床位 (长度: 50)
        /// </summary>
        public string wardareaBed { get; set; }

        /// <summary>
        /// 转科室标志 (长度: 6)
        /// </summary>
        public string trafDeptFlag { get; set; }

        /// <summary>
        /// 出院主诊断代码 (长度: 20)
        /// </summary>
        public string dscgMaindiagCode { get; set; }

        /// <summary>
        /// 出院科室编码 (长度: 30)
        /// </summary>
        public string dscgDeptCodg { get; set; }

        /// <summary>
        /// 出院科室名称 (长度: 100)
        /// </summary>
        public string dscgDeptName { get; set; }

        /// <summary>
        /// 出院床位 (长度: 30)
        /// </summary>
        public string dscgBed { get; set; }

        /// <summary>
        /// 离院方式 (长度: 3)
        /// </summary>
        public string dscgWay { get; set; }

        /// <summary>
        /// 主要病情描述 (长度: 1000)
        /// </summary>
        public string mainCondDscr { get; set; }

        /// <summary>
        /// 病种编号 (长度: 30)
        /// </summary>
        public string diseNo { get; set; }

        /// <summary>
        /// 病种名称 (长度: 500)
        /// </summary>
        public string diseName { get; set; }

        /// <summary>
        /// 手术操作代码 (长度: 30)
        /// </summary>
        public string oprnOprtCode { get; set; }

        /// <summary>
        /// 手术操作名称 (长度: 500)
        /// </summary>
        public string oprnOprtName { get; set; }

        /// <summary>
        /// 门诊诊断信息 (长度: 200)
        /// </summary>
        public string otpDiagInfo { get; set; }

        /// <summary>
        /// 在院状态 (长度: 3)
        /// </summary>
        public string inhospStas { get; set; }

        /// <summary>
        /// 死亡日期 (格式: yyyy-MM-dd)
        /// </summary>
        public DateTime dieDate { get; set; }

        /// <summary>
        /// 住院天数 (数值型: 16)
        /// </summary>
        public int iptDays { get; set; }

        /// <summary>
        /// 计划生育服务证号 (长度: 50)
        /// </summary>
        public string fpscNo { get; set; }

        /// <summary>
        /// 生育类别 (长度: 6)
        /// </summary>
        public string matnType { get; set; }

        /// <summary>
        /// 计划生育手术类别 (长度: 6)
        /// </summary>
        public string birctrlType { get; set; }

        /// <summary>
        /// 晚育标志 (长度: 3)
        /// </summary>
        public string latechbFlag { get; set; }

        /// <summary>
        /// 孕周数 (数值型: 2)
        /// </summary>
        public int gesoVal { get; set; }

        /// <summary>
        /// 胎次 (数值型: 3)
        /// </summary>
        public int fetts { get; set; }

        /// <summary>
        /// 胎儿数 (数值型: 3)
        /// </summary>
        public int fetusCnt { get; set; }

        /// <summary>
        /// 早产标志 (长度: 3)
        /// </summary>
        public string pretFlag { get; set; }

        /// <summary>
        /// 妊娠时间 (格式: yyyy-MM-dd)
        /// </summary>
        public DateTime preyTime { get; set; }

        /// <summary>
        /// 计划生育手术或生育日期 (格式: yyyy-MM-dd)
        /// </summary>
        public DateTime birctrlMatnDate { get; set; }

        /// <summary>
        /// 伴有并发症标志 (长度: 3)
        /// </summary>
        public string copFlag { get; set; }

        /// <summary>
        /// 有效标志 (长度: 3)
        /// </summary>
        public string valiFlag { get; set; }

        /// <summary>
        /// 备注 (长度: 500)
        /// </summary>
        public string memo { get; set; }

        /// <summary>
        /// 经办人 ID (长度: 20)
        /// </summary>
        public string opterId { get; set; }

        /// <summary>
        /// 经办人姓名 (长度: 50)
        /// </summary>
        public string opterName { get; set; }

        /// <summary>
        /// 经办时间 (格式: yyyy-MM-dd HH:mm:ss)
        /// </summary>
        public DateTime optTime { get; set; }

        /// <summary>
        /// 唯一记录号 (长度: 30)
        /// </summary>
        public string rid { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime updtTime { get; set; }

        /// <summary>
        /// 创建人 ID (长度: 20)
        /// </summary>
        public string crterId { get; set; }

        /// <summary>
        /// 创建人姓名 (长度: 50)
        /// </summary>
        public string crterName { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime crteTime { get; set; }

        /// <summary>
        /// 创建机构 (长度: 20)
        /// </summary>
        public string crteOptinsNo { get; set; }

        /// <summary>
        /// 经办机构 (长度: 20)
        /// </summary>
        public string optinsNo { get; set; }

        /// <summary>
        /// 统筹区编码 (长度: 10)
        /// </summary>
        public string poolareaNo { get; set; }

        /// <summary>
        /// 主诊医师姓名 (长度: 50)
        /// </summary>
        public string chfpdrName { get; set; }

        /// <summary>
        /// 住院主诊断名称 (长度: 300)
        /// </summary>
        public string dscgMaindiagName { get; set; }

        /// <summary>
        /// 医疗总费用 (数值型: 16,2)
        /// </summary>
        public decimal medfeeSumamt { get; set; }

        /// <summary>
        /// 电子票据代码 (长度: 50)
        /// </summary>
        public string elecBillCode { get; set; }

        /// <summary>
        /// 电子票据号码 (长度: 50)
        /// </summary>
        public string elecBillnoCode { get; set; }

        /// <summary>
        /// 电子票据校验码 (长度: 6)
        /// </summary>
        public string elecBillChkcode { get; set; }

        /// <summary>
        /// 完成标志 (长度: 6)
        /// </summary>
        public string cpltFlag { get; set; }

        /// <summary>
        /// 扩展字段 (长度: 4000)
        /// </summary>
        public string expContent { get; set; }
    }



}
