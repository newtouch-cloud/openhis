using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Newtouch.HIS.Domain.ValueObjects.PharmacyDrugStorage
{
    public class PharmacyDepartmentOpenMedicineRepoVO
    {
        /// <summary>
        /// 主键Id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 药房部门编号
        /// </summary>
        public string yfbmCode { get; set; }

        /// <summary>
        /// 组织机构Id
        /// </summary>
        public string OrganizeId { get; set; }

        /// <summary>
        /// 顶级组织机构Id
        /// </summary>
        public string TopOrganizeId { get; set; }

        /// <summary>
        /// 大类编号
        /// </summary>
        public string dlCode { get; set; }

        /// <summary>
        /// 修改用户
        /// </summary>
        public string CreatorCode { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 最后修改时间
        /// </summary>
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// 最后修改用户
        /// </summary>
        public string LastModifierCode { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public string zt { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int? px { get; set; }

        /// <summary>
        /// 药房部门名称
        /// </summary>
        public string yfbmmc { get; set; }

        /// <summary>
        /// 大类名称
        /// </summary>
        public string dlmc { get; set; }
    }
}