using System;

namespace Newtouch.HIS.Domain.ValueObjects
{
    /// <summary>
    /// 公用model 测试
    /// </summary>
    public class ModelInfoVO
    {
        public string model1 { get; set; }
        public string model2 { get; set; }
        public string model3 { get; set; }
        public string model4 { get; set; }
        public string model5 { get; set; }
        public string model6 { get; set; }
        public string model7 { get; set; }
        public string model8 { get; set; }
        public string model9 { get; set; }
        public string model10 { get; set; }
        public string model11 { get; set; }
        public string model12 { get; set; }
    }

    public class ModelBRXXInfoVO
    {
        public int model1 { get; set; }
        public int model2 { get; set; }
        public string model3 { get; set; }
        public string model4 { get; set; }
        public string model5 { get; set; }
        public string model6 { get; set; }
        public string model7 { get; set; }
        public DateTime model8 { get; set; }
        public string model9 { get; set; }
        public short model10 { get; set; }
        public string model11 { get; set; }
        public string model12 { get; set; }

    }

    /// <summary>
    /// 发药病区病人详细信息
    /// </summary>
    public class DispenseBQRYXXIndexVO
    {
        public int Id { get; set; }//主键
        public string zxId { get; set; }//医嘱执行ID
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
        public string OrganizeId { get; set; }//组织机构
        public string yppc { get; set; }//批次
        public string ypph { get; set; }//批号
    }


    /// <summary>
    /// 获取计算需要发药的的单价，金额，
    /// </summary>
    public class ModelBQBRYZZXJEInfoVO
    {
        public Decimal dj { get; set; } //单价
        public Decimal je { get; set; } //金额
        public Decimal zfbl { get; set; } //自负比例
        public string zfxz { get; set; } //自负性质
    }

    /// <summary>
    /// 住院退药，医生站医嘱ID
    /// </summary>
    public class ModelZyTyYszyzIdInfoVO
    {
        public int yszyzId { get; set; } //医嘱ID  yszyzid 医生站 医嘱ID
        public int zxId { get; set; } //医嘱执行ID 
        public string yzdm { get; set; }// 医嘱代码
    }

    /// <summary>
    /// 存储退药明细表，批次
    /// </summary>
    public class ModelBQBRYZZXTYInfoVO
    {
        public int Id { get; set; }//主键
        public string zxId { get; set; } //医嘱执行ID
        public DateTime? tdrq { get; set; } //退档日期
        public string ypCode { get; set; } //药品Code
        public decimal tysl { get; set; }//退药数量
        public int tymxId { get; set; }//退药明细ID
        public string tybz { get; set; }// 退药备注
        public DateTime? tyjlrq { get; set; }//退药记录日期
        public string tyxh { get; set; }//退药序号 
        public string tyry { get; set; }//退药人员
        public DateTime? tyrq { get; set; }//退药日期
        public string yppc { get; set; }//医嘱执行批号表 批次
        public string ypph { get; set; }//医嘱执行批号表 批号
        public decimal sl { get; set; }//数量
        public string OrganizeId { get; set; }//组织机构
        public DateTime zxrq { get; set; }//执行日期
        public string zyh { get; set; }//住院号
    }

    /// <summary>
    /// 住院发药
    /// </summary>
    public class DispensingDrugsParam
    {
        /// <summary>
        /// 医嘱ID
        /// </summary>
        public string yzId { get; set; }

        /// <summary>
        /// 医嘱执行ID
        /// </summary>
        public string zxId { get; set; }

        /// <summary>
        /// 药品代码
        /// </summary>
        public string ypCode { get; set; }

        /// <summary>
        /// 批号
        /// </summary>
        public string ph { get; set; }

        /// <summary>
        /// 批次
        /// </summary>
        public string pc { get; set; }

        /// <summary>
        /// 药房部门代码
        /// </summary>
        public string yfbmCode { get; set; }

        /// <summary>
        /// 组织机构ID
        /// </summary>
        public string organizeId { get; set; }

        /// <summary>
        /// 追溯码
        /// </summary>
        public string zsm { get; set; }

        /// <summary>
        /// 是否拆零
        /// 1： 是
        /// 2： 否
        /// </summary>
        public int? sfcl { get; set; }

    }
}
