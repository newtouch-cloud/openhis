using Newtouch.Core.Common;
using Newtouch.HIS.API.Common;
using System;
using System.Collections.Generic;

namespace Newtouch.HIS.Sett.Request.HospitalizationPharmacy
{

    #region 发药
    /// <summary>
    /// 接受病区，住院发药病区
    /// </summary>
    public class WaitingDispenseBQMedicineDispenseRequest : RequestBase
    {
        /// <summary>
        /// 当前类型 zyyf 住院药房
        /// </summary>
        public string zyyf { get; set; }
    }
    /// <summary>
    /// 接受病区，住院发药病人
    /// </summary>
    public class WaitingDispenseMedicineDispenseRequest : OrgRequestBase
    {
        public string zyyf { get; set; }
        public string bq { get; set; }
    }

    /// <summary>
    /// 接受病区，住院发药人员详细信息
    /// </summary>
    public class WaitingDispenseBRXXMedicineDispenseRequest : OrgRequestBase
    {//Newtouch.Common
        public Pagination pagination { get; set; } //分页
        public string organizeId { get; set; }// 用户权限
        public string zyyf { get; set; }// 用户登陆类型
        public string Bq { get; set; }//病区
        public string Zyh { get; set; }//病人
        public string ypCode { get; set; }//药品Code
        public string Kssj { get; set; }//开始时间
        public string Jssj { get; set; }//结束时间
        public string Cl { get; set; } //长临
        public string Fyzt { get; set; }//发药状态
    }
    #endregion

    #region 配药

    /// <summary>
    /// 病区病人配药
    /// </summary>
    public class WaitingDispenseBQBRPYMedicineDispenseRequest : RequestBase
    {
        public string Bq { get; set; }//病区
        public string lyyf { get; set; }//登陆医嘱类型
        public string UserLogin { get; set; }//操作用户
        public string Kssj { get; set; }//开始时间
        public string Jssj { get; set; }//结束时间

    }

    /// <summary>
    /// 查询用户配药完成，修改发药备足状态
    /// </summary>
    public class WaitingDispenseBQPYyzzxZTDispenseRequest : OrgRequestBase
    {
        public string pyry { get; set; } //配药人员
        public string pyrq { get; set; } //配药日期
        public string yzId { get; set; } //医嘱执行ID
        public string tdrq { get; set; } // 提档日期、建档日期
        public string fybz { get; set; } // 当前发药状态
        public string ypdj { get; set; }//药品单价
        public string ypje { get; set; } //药品金额
        public string zfbl { get; set; }// 自负比例
        public string zfxz { get; set; }// 自负性质
    }
    #endregion

    #region 病区发药查询
    /// <summary>
    /// 接受病区，调用病人
    /// </summary>
    public class WaitingDispenseBRCXXXMedicineDispenseRequest : OrgRequestBase
    {//Newtouch.Common
        public Pagination pagination { get; set; } //分页
        public string Bq { get; set; }//病区
        public string Zyh { get; set; }//病人
        public string zyyf { get; set; }//住房发药
        public string ypCode { get; set; }//药品Code
        public string Kssj { get; set; }//开始时间
        public string Jssj { get; set; }//结束时间
        public string Cl { get; set; } //长临
        public string Fyzt { get; set; }//发药状态

        public List<ModelBQBRYZZXTYInfoVO> YpTymxxxList { get; set; }//退药明细集合List
    }
    #endregion

    #region 退药
    /// <summary>
    /// 接受退药查询的条件
    /// </summary>
    public class WaitingDispenseBRTYXXMedicineDispenseRequest : OrgRequestBase
    {
        public string organizeId { get; set; }// 用户权限
        public string zyyf { get; set; }// 用户登陆类型
        public string Bq { get; set; }//病区
        public string Zyh { get; set; }//病人
        public string ypCode { get; set; }//药品Code
        public string Kssj { get; set; }//开始时间
        public string Jssj { get; set; }//结束时间
        public string Cl { get; set; } //长临
        public List<ModelBQBRYZZXTYInfoVO> YpTymxxxList { get; set; }//退药明细集合List

    }

    /// <summary>
    /// 接收退药明细表，医嘱批号表能退药的记录
    /// </summary>
    public class ModelBQBRYZZXTYInfoVO
    {
        public string yszyzid { get; set; } //医嘱ID  yszyzid 医生组 医嘱ID
        public DateTime? tdrq { get; set; } //退档日期
        public string ypCode { get; set; } //药品Code


        public string tysl { get; set; }//退药数量
        public string tymxId { get; set; }//退药明细ID
        public string tybz { get; set; }// 退药标志
        public DateTime? tyjlrq { get; set; }//退药记录日期
        public string tyxh { get; set; }//退药序号 
        public string tyry { get; set; }//退药人员
        public DateTime? tyrq { get; set; }//退药日期
        public string yppc { get; set; }//医嘱执行批号表 批次
        public string ypph { get; set; }//医嘱执行批号表 批号
        public decimal sl { get; set; }//数量
        public string tysqczyh { get; set; }
    }

    public class WaitingDispenseTYYszyzIdDispenseRequest : OrgRequestBase
    {
        public string yzdm { get; set; }// 医嘱代码
        public string organizeId { get; set; }// 权限
        public List<ModelZyTyYszyzIdInfoVO> yszyzList { get; set; }//退药明细集合List
    }
    public class ModelZyTyYszyzIdInfoVO
    {
        public int yszyzid { get; set; } //医嘱ID  yszyzid 医生站 医嘱ID
        public int yzid { get; set; } //医嘱表ID  
    }
    #endregion
}
