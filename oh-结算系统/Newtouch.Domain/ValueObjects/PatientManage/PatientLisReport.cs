using System;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.ValueObjects
{
    public class PatientReportIndex  
    {
        /// <summary>
        /// 申请单号
        /// </summary>
        public string sqdh { get; set; }
        /// <summary>
        /// 项目名称
        /// </summary>
        public string xmmc { get; set; }
        /// <summary>
        /// 项目编号
        /// </summary>
        public string xmdm { get; set; }
        /// <summary>
        /// 科室
        /// </summary>
        public string ksmc { get; set; }
        /// <summary>
        /// 医生
        /// </summary>
        public string ysmc { get; set; }
        /// <summary>
        /// 申请单状态
        /// </summary>
        public string sqdzt { get; set; }
        /// <summary>
        /// 申请单业务类型 1 门诊 2 住院
        /// </summary>
        public string ywlx { get; set; }
        /// <summary>
        /// 申请时间
        /// </summary>
        public DateTime? sqsj { get; set; }
    }

    public class PatLisReportDetail {
        /// <summary>
        /// 患者信息
        /// </summary>
        public LisPatInfo patInfo { get; set; }
        /// <summary>
        /// 检验详情
        /// </summary>
        public List<LisReportBase> sqdInfo { get; set; }
    }

    public class LisPatInfo
    {
        /// <summary>
        /// 姓名
        /// </summary>
        public string xm { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public string xb { get; set; }
        /// <summary>
        /// 年龄
        /// </summary>
        public string nlshow { get; set; }
        /// <summary>
        /// 科室
        /// </summary>
        public string ksmc { get; set; }
        /// <summary>
        /// 医生
        /// </summary>
        public string ysmc { get; set; }
        /// <summary>
        /// 住院-病区
        /// </summary>
        public string bq { get; set; }
        /// <summary>
        /// 住院-床位
        /// </summary>
        public string cwmc { get; set; }
        public string brxz { get; set; }
        public string mzh { get; set; }
        public string zyh { get; set; }
        public string zdmc { get; set; }

    }

    public class LisReportBase
    {
        /// <summary>
        /// 申请单号
        /// </summary>
        public string sqdh { get; set; }
        /// <summary>
        /// 序号
        /// </summary>
        public int xh { get; set; }
        /// <summary>
        /// 项目名称
        /// </summary>
        public string xmzwmc { get; set; }
        /// <summary>
        /// 英文简称
        /// </summary>
        public string xmywmc { get; set; }
        /// <summary>
        /// 检验结果
        /// </summary>
        public string jyjg { get; set; }
        /// <summary>
        /// 结果提示
        /// </summary>
        public string gdbj { get; set; }
        /// <summary>
        /// 单位
        /// </summary>
        public string xmdw { get; set; }
        /// <summary>
        /// 参考值范围最低值
        /// </summary>
        public string ckzdx { get; set; }
        /// <summary>
        /// 参考值范围最高值
        /// </summary>
        public string ckzgx { get; set; }
        /// <summary>
        /// 参考值
        /// </summary>
        public string ckz { get; set; }
        /// <summary>
        /// 危急值最低值
        /// </summary>
        public string wjzdx { get; set; }
        /// <summary>
        /// 危急值最高值
        /// </summary>
        public string wjzgx { get; set; }
        /// <summary>
        /// 业务类型 1 门诊 2 住院
        /// </summary>
        public string ywlx { get; set; }
        /// <summary>
        /// 录入人
        /// </summary>
        public string lrrxm { get; set; }
        /// <summary>
        /// 校验人
        /// </summary>
        public string jyrxm { get; set; }
        /// <summary>
        /// 审核人
        /// </summary>
        public string shrxm { get; set; }
        /// <summary>
        /// 检验日期
        /// </summary>
        public DateTime? jyrq { get; set; }
        /// <summary>
        /// 审核日期
        /// </summary>
        public DateTime? shrq { get; set; }
        /// <summary>
        /// 项目名称
        /// </summary>
        public string xmmc { get; set; }
        /// <summary>
        /// 项目代码
        /// </summary>
        public string xmdm { get; set; }

    }
}
