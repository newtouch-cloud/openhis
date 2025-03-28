using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Domain.ValueObjects.Inpatient
{
    public class EntryRegistrationVO
    {
        public string zyh { get; set; }
        public DateTime rqrq { get; set; }
        public string wzjb { get; set; }
        public string cwCode { get; set; }
        public string qqlx { get; set; }
        public string ysgh { get; set; }
        public string ysmc { get; set; }
        public string bqdm { get; set; }

        /// <summary>
        /// 上一个床位
        /// </summary>
        public string lastcwCode { get; set; }
        public string DeptCode { get; set; }
    }
    /// <summary>
    /// 待入区患者信息
    /// </summary>
    public class NewPatInfoVO
    {
        public string zyh { get; set; }
        public string blh { get; set; }
        public string xm { get; set; }
        public string nl { get; set; }
        public string nlshow { get; set; }
        public string xb { get; set; }
        public string ryrq { get; set; }
        public string zd { get; set; }
        public string brxzmc { get; set; }
        public string ysgh { get; set; }
        public string ysmc { get; set; }

        public string lastcwCode { get; set; }
        public string DeptCode { get; set; }
    }

    public class BedInfoViewRequestVO
    {
        public string bqCode { get; set; }
        public string OrganizeId { get; set; }
        //是否占用
        public string sfzy { get; set; }
    }
    public class BedInfoViewResponseVO
    {
        public string cwCode { get; set; }
        public string cwmc { get; set; }
        public bool? sfzy { get; set; }
        public string bfNo { get; set; }
        public string cwlx { get; set; }
        public string zyh { get; set; }
        public string xm { get; set; }
        public string xb { get; set; }
        public string ysmc { get; set; }
        public string brxzmc { get; set; }
        public string sfzt { get; set; }
        /// <summary>
        /// 床位使用状态
        /// </summary>
        public int? cwsystu { get; set; }

        public int? cnt { get; set; }
    }

    public class patBedSyjlInfoVO
    {
        public string OrganizeId { get; set; }
        public string blh { get; set; }
        public string zyh { get; set; }
        public string BedCode { get; set; }
        public string BedNo { get; set; }
        public string WardCode { get; set; }
        public string WardName { get; set; }
        public string RoomCode { get; set; }
        public string RoomName { get; set; }
        public string DeptCode { get; set; }
        public string DeptName { get; set; }
        public DateTime? OccBeginDate { get; set; }
        public int Status { get; set; }
    }

    public class patBedInfoVO
    {
        public string OrganizeId { get; set; }
        public string BedCode { get; set; }
        public string BedNo { get; set; }
        public string WardCode { get; set; }
        public string WardName { get; set; }
        public string RoomCode { get; set; }
        public string RoomName { get; set; }
    }
}
