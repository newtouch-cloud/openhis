using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.ValueObjects.HospitalizationManage
{
    public class DispenseIndexVO
    {
    }
    /// <summary>
    /// 存发药病区
    /// </summary>
    public class DispenseBQIndexVO
    {
        public string bqCode { get; set; }//病区编号
        public string bqmc { get; set; }//病区名称

    }
    /// <summary>
    /// 存发药病区病人
    /// </summary>
    public class DispenseBQRYIndexVO
    {
        /// <summary>
        /// 住院号
        /// </summary>
        public string zyh { get; set; }//住院号
        public string xm { get; set; }//姓名
        public string bq { get; set; }//病区
        public string cw { get; set; }//床位
    }


    /// <summary>
    /// 发药病区病人详细细心
    /// </summary>
    public class DispenseBQRYXXIndexVO
    {
        public int yzId { get; set; }//医嘱ID
        public string zyh { get; set; }//住院号
        public string xm { get; set; }//姓名
        public string cw { get; set; }//床位
        public string ypCode { get; set; }//药品code
        public string ypmc { get; set; }//药品名称
        public string ycmc { get; set; }//药厂名称
        public string ypgg { get; set; }//药品规格
        public decimal sl { get; set; }//数量
        public string dw { get; set; }//单位
        public string yldw { get; set; }//用量单位
        public decimal yl { get; set; }//用量数量 /药量
        public short cls { get; set; }//拆量数
        public string zlff { get; set; }//治疗方法
        public string yzxz { get; set; }//医嘱状态
        public string ypfz { get; set; }//药品分组
        public DateTime tdrq { get; set; }//提档日期
        public string fyrq { get; set; }//发药日期
        public string rymc { get; set; }//人员名称
        public string Pcmc { get; set; }// PC 名称
        public string fybz { get; set; }//发药状态 2 为已发，1为未发

    }

    /// <summary>
    /// 发药病区病人详细细心
    /// </summary>
    public class DispenseBQRYCXXXIndexVO
    {
        public string ksrq { get; set; }
        public string yxq { get; set; }
        public string xm { get; set; }
        public string cw { get; set; }
        public string ypCode { get; set; }
        public string ypmc { get; set; }
        public string ycmc { get; set; }
        public string ypgg { get; set; }
        public string sl { get; set; }
        public string sfdw { get; set; }
        public string slydw { get; set; }
        public string tysl { get; set; }
        public string tyry { get; set; }
        public string tyrymc { get; set; }
        public string tyrq { get; set; }
        public string tysqczyh { get; set; }
        public string tysqry { get; set; }
        public string tyjlrq { get; set; }
        public string tybz { get; set; }
        public string yldw { get; set; }
        public string yl { get; set; }
        public string ylydw { get; set; }
        public string dj { get; set; }
        public string je { get; set; }
        public string jxmc { get; set; }
        public string zlff { get; set; }
        public string yzxz { get; set; }
        public string czbh { get; set; }
        public string pc { get; set; }
        public string pyrq { get; set; }
        public string fyrq { get; set; }
        public string rymc { get; set; }
        public string yzId { get; set; }
        public string tdrq { get; set; }
        public string pyry { get; set; }
        public string ks { get; set; }
        public string fybz { get; set; }
        public string zyh { get; set; }
        public string yszyzid { get; set; }
        public string xb { get; set; }
        public string yppc { get; set; }
        public string gmxx { get; set; }
        public string sjap { get; set; }
        public string Cls { get; set; }
    }

    //病区病人配药操作
    public class DispenseBQBRPYIndexVO
    {
        public int yzId   { get; set; }//医嘱号码
        public string zyh { get; set; }//住院号
        public Decimal sl { get; set; }//数量
        public string lyyf { get; set; }//类要药房
        public string ypCode { get; set; }//药品code
        public short cls { get; set; }// 拆量数
        public DateTime jdrq { get; set; }//提档日期、操作日期,建档日期
    }

    /// <summary>
    /// 住院 退药信息
    /// </summary>
    public class DispenseBQRYTYXXIndexVO
    {
        public string xm { get; set; } // 姓名
        public string cw { get; set; }// 床位
        public string ypCode { get; set; }// 药品code
        public string ypmc { get; set; }// 药品名称
        public string ycmc { get; set; }// 药厂名称
        public string ypgg { get; set; }// 药品规格
        public Decimal sl { get; set; }// 数量
        public string tysl { get; set; }// 退药数量
        public string dw { get; set; }// 单位
        public string yldw { get; set; }// 用量单位
        public Decimal yl { get; set; }// 用量
        public string zlff { get; set; }// 用法
        public string yzxz { get; set; }//医嘱性质
        public string czbh { get; set; }// 药品 分子
        public string pc { get; set; }// 退药表 批次
        public DateTime? pyrq { get; set; }// 配药日期
        public DateTime? fyrq { get; set; }// 发药日期
        public string rymc { get; set; }//人员名称
        public string tymxId { get; set; }// 退药明细ID
        public string tdrq { get; set; }// 退档日期
        public string pyry { get; set; }//配药人员
        public string ks { get; set; }//科室
        public string tybz { get; set; }//退药标志
        public string zyh { get; set; }//住院号
        public int yszyzid { get; set; }//医生站 医嘱ID
        public string xb { get; set; }//性别
        public string yppc { get; set; }//药品批次
        public string bz { get; set; }//备注（默认为空）
        public string gmxx { get; set; }//过敏信息（默认为空）
        public string sjap { get; set; }//时间安排
        public string tyjlrq { get; set; }//退药记录日期
        public string tyxh { get; set; }// 退药序号
        public string ypph { get; set; }//退药表 药品批号
        public string tyrq { get; set; }// 退药表退药日期
        public string tyry { get; set; }//退药人员
    }
}
