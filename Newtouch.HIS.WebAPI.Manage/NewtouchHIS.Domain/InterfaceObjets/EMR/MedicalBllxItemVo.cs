using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewtouchHIS.Domain.InterfaceObjets.EMR
{
    /// <summary>
    /// 病历类型
    /// </summary>
    public class MedicalBllxItemVo
    {
        public List<MedicalBllxRecord> bllxRecord { get; set; }
    }
    public class MedicalBllxRecord
    {
        public string Id { get; set; }

        public string OrganizeId { get; set; }

        public string Bllx { get; set; }
        public string Bllxmc { get; set; }
        public string ParentId { get; set; }
        public string IsRoot { get; set; }
    }
    /// <summary>
    /// 病历模板
    /// </summary>
    public class MedicalBlMbItemVo
    {
        public List<MedicalBlmbRecord> blmbRecord { get; set; }
    }
    public class MedicalBlmbRecord
    {
        public string Id { get; set; }

        public string OrganizeId { get; set; }

        public string Mbbm { get; set; }
        public string Mbmc { get; set; }
        public string BllxId { get; set; }
        public string Bllxmc { get; set; }
        public string Mblj { get; set; }
        public string Bllx { get; set; }
    }
    /// <summary>
    /// 病历和模板tree结构数据
    /// </summary>
    public class MedicalBllxMbTreeVo
    {
        public List<MedicalBllxmbTreeRecord> bllxmbRecord { get; set; }
    }
    public class MedicalBllxmbTreeRecord
    {
        public string Id { get; set; }

        public string OrganizeId { get; set; }

        public string parentId { get; set; }
        public string bllx { get; set; }
        public string bllxmc { get; set; }
        public string isroot { get; set; }
        public string ly { get; set; }
    }
}
