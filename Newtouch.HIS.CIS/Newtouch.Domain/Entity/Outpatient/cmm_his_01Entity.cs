using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FrameworkBase.MultiOrg.Domain.Entity;

namespace Newtouch.Domain.Entity
{
    /// <summary>
    /// 中医馆_推送患者
    /// </summary>
    [Table("cmm_his_01")]
    public class CmmHis01Entity : IEntity<CmmHis01Entity>
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        public string Id { get; set; }

        /// <summary>
        /// 患者编号（一个患者对应一个编号，且与身份证号码一一对应），对应接口id字段，本地保存blh
        /// </summary>
        public string cmmId { get; set; }

        /// <summary>
        /// 患者姓名
        /// 必填
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 患者性别编码
        /// 必填
        /// </summary>
        public string genderCode { get; set; }

        /// <summary>
        /// 患者性别
        /// 必填
        /// </summary>
        public string gender { get; set; }

        /// <summary>
        /// 出生日期 YYYYMMDD
        /// 必填
        /// </summary>
        public string birthday { get; set; }

        /// <summary>
        /// 证件类型编码
        /// 必填
        /// </summary>
        public string cardTypeCode { get; set; }

        /// <summary>
        /// 证件类型（一般指居民身份证号）
        /// 必填
        /// </summary>
        public string cardType { get; set; }

        /// <summary>
        /// 证件号码
        /// 必填
        /// </summary>
        public string cardNo { get; set; }

        /// <summary>
        /// 职业编码
        /// </summary>
        public string occupationCode { get; set; }

        /// <summary>
        /// 职业
        /// </summary>
        public string occupation { get; set; }

        /// <summary>
        /// 婚姻状况编码
        /// </summary>
        public string marriedCode { get; set; }

        /// <summary>
        /// 婚姻状况
        /// </summary>
        public string married { get; set; }

        /// <summary>
        /// 国籍编码
        /// </summary>
        public string countryCode { get; set; }

        /// <summary>
        /// 国籍
        /// </summary>
        public string country { get; set; }

        /// <summary>
        /// 民族编码
        /// </summary>
        public string nationalityCode { get; set; }

        /// <summary>
        /// 民族
        /// </summary>
        public string nationality { get; set; }

        /// <summary>
        /// 家庭住址省份编码
        /// 必填
        /// </summary>
        public string provinceCode { get; set; }

        /// <summary>
        /// 家庭住址市级编码
        /// 必填
        /// </summary>
        public string cityCode { get; set; }

        /// <summary>
        /// 家庭住址区县编码（保持与云平台的行政区划编码一致）
        /// 必填
        /// </summary>
        public string areaCode { get; set; }

        /// <summary>
        /// 家庭住址省份
        /// </summary>
        public string province { get; set; }

        /// <summary>
        /// 家庭住址市级
        /// </summary>
        public string city { get; set; }

        /// <summary>
        /// 家庭住址区县
        /// </summary>
        public string area { get; set; }

        /// <summary>
        /// 街道信息
        /// </summary>
        public string streetInfo { get; set; }

        /// <summary>
        /// 邮编
        /// </summary>
        public string postcode { get; set; }

        /// <summary>
        /// 联系人名称
        /// </summary>
        public string ctName { get; set; }

        /// <summary>
        /// 与联系人关系
        /// </summary>
        public string ctRoleId { get; set; }

        /// <summary>
        /// 联系人地址
        /// </summary>
        public string ctAddr { get; set; }

        /// <summary>
        /// 联系人电话
        /// </summary>
        public string ctTelephone { get; set; }

        /// <summary>
        /// 患者类型编码
        /// </summary>
        public string patiTypeCode { get; set; }

        /// <summary>
        /// 患者类型
        /// </summary>
        public string patiType { get; set; }

        /// <summary>
        /// 患者电话
        /// </summary>
        public string telephone { get; set; }

        /// <summary>
        /// 门诊号, 即就诊流水号
        /// 必填
        /// </summary>
        public string outpatientNo { get; set; }

        /// <summary>
        /// 机构编码
        /// 必填
        /// </summary>
        public string orgCode { get; set; }

        /// <summary>
        /// 发送应用程序，指定为：HIS
        /// </summary>
        public string sender { get; set; }

        /// <summary>
        /// 接收应用程序，指定为：EMR,HEAL,PLAT
        /// </summary>
        public string receiver { get; set; }

        /// <summary>
        /// 发送时间，格式：yyyyMMddHHmmss
        /// </summary>
        public string sendTime { get; set; }

        /// <summary>
        /// 消息类型：TCM_HIS_01
        /// </summary>
        public string msgType { get; set; }

        /// <summary>
        /// 消息 ID(应用程序编码+发 送 时 间 ) ， 如 ：HIS20170606020202
        /// </summary>
        public string msgID { get; set; }

        /// <summary>
        /// 组织机构ID
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// 返回结果状态码  1： 成功； 0：失败
        /// </summary>
        public string resultCode { get; set; }

        /// <summary>
        /// 返回结果描述  成功；失败(补充失败原因描述)
        /// </summary>
        public string resultDesc { get; set; }

        /// <summary>
        /// 状态     1:有效  0：无效
        /// </summary>
        public string zt { get; set; }

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
    }
}