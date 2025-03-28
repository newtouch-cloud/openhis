using System;
using System.Collections.Generic;
using Newtouch.HIS.API.Common;

namespace Newtouch.PDS.Requset.PharmacyDepartment
{
    /// <summary>
    /// 授权药房部门
    /// </summary>
    public class EmpowermentPharmacyDepartmentAndRemoveOldRequestDto : RequestBase
    {
        /// <summary>
        /// 授权药房部门
        /// </summary>
        public List<EmpowermentPharmacyDepartment> epds { get; set; }
    }

    /// <summary>
    /// 授权药房部门
    /// </summary>
    public class EmpowermentPharmacyDepartmentRequestDto :  RequestBase
    {
        /// <summary>
        /// 部门药品Id
        /// </summary>
        public string bmypId { get; set; }

        /// <summary>
        /// 组织机构ID
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// 药房部门
        /// </summary>
        public string yfbmCode { get; set; }

        /// <summary>
        /// 药品代码
        /// </summary>
        public string Ypdm { get; set; }

        /// <summary>
        /// 药品库位
        /// </summary>
        public string Ypkw { get; set; }

        /// <summary>
        /// 账册序号
        /// </summary>
        public string Zcxh { get; set; }

        /// <summary>
        /// 排序方式1
        /// </summary>
        public string Pxfs1 { get; set; }

        /// <summary>
        /// 排序方式2
        /// </summary>
        public string Pxfs2 { get; set; }

        /// <summary>
        /// 库存上限
        /// </summary>
        public int? Kcsx { get; set; }

        /// <summary>
        /// 库存下限
        /// </summary>
        public int? Kcxx { get; set; }

        /// <summary>
        /// 进货点 低于进货点则自动生成内部申领单
        /// </summary>
        public int? Jhd { get; set; }

        /// <summary>
        /// 计划量 对应进货点，指定内部申领的数量
        /// </summary>
        public int? Jhl { get; set; }

        /// <summary>
        /// 药品属性 来自 表 xt_yptssx
        /// </summary>
        public string Ypsxdm { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? Ylsx { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? Sysx { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int? px { get; set; }

        /// <summary>
        /// 状态  1：正常 0：控制
        /// </summary>
        public string zt { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public string CreatorCode { get; set; }

        /// <summary>
        /// 创建试驾
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// 修改人
        /// </summary>
        public string LastModifierCode { get; set; }
    }

    /// <summary>
    /// 授权药房
    /// </summary>
    public class EmpowermentPharmacyDepartment
    {
        /// <summary>
        /// 部门药品Id
        /// </summary>
        public string bmypId { get; set; }

        /// <summary>
        /// 组织机构ID
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// 药房部门
        /// </summary>
        public string yfbmCode { get; set; }

        /// <summary>
        /// 药品代码
        /// </summary>
        public string Ypdm { get; set; }

        /// <summary>
        /// 药品库位
        /// </summary>
        public string Ypkw { get; set; }

        /// <summary>
        /// 账册序号
        /// </summary>
        public string Zcxh { get; set; }

        /// <summary>
        /// 排序方式1
        /// </summary>
        public string Pxfs1 { get; set; }

        /// <summary>
        /// 排序方式2
        /// </summary>
        public string Pxfs2 { get; set; }

        /// <summary>
        /// 库存上限
        /// </summary>
        public int? Kcsx { get; set; }

        /// <summary>
        /// 库存下限
        /// </summary>
        public int? Kcxx { get; set; }

        /// <summary>
        /// 进货点 低于进货点则自动生成内部申领单
        /// </summary>
        public int? Jhd { get; set; }

        /// <summary>
        /// 计划量 对应进货点，指定内部申领的数量
        /// </summary>
        public int? Jhl { get; set; }

        /// <summary>
        /// 药品属性 来自 表 xt_yptssx
        /// </summary>
        public string Ypsxdm { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? Ylsx { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? Sysx { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int? px { get; set; }

        /// <summary>
        /// 状态  1：正常 0：控制
        /// </summary>
        public string zt { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public string CreatorCode { get; set; }

        /// <summary>
        /// 创建试驾
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// 修改人
        /// </summary>
        public string LastModifierCode { get; set; }
    }
}