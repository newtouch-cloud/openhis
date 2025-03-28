using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.EMR.Domain.ValueObjects.MRHomePage
{
    /// <summary>
    /// 来源CIS [zy_cwsyjlk]
    /// </summary>
    public class InpPatTransferInfo
    {
        public int num { get; set; }
        public string OrganizeId { get; set; }
        public string zyh { get; set; }
        public string BedCode { get; set; }
        public string BedNo { get; set; }
        public string WardCode { get; set; }
        public string WardName { get; set; }
        public string RoomCode { get; set; }
        public string RoomName { get; set; }
        public string DeptCode { get; set; }
        public string DeptName { get; set; }
        public string TransBedCode { get; set; }
        public string TransDeptCode { get; set; }
        public string TransWardCode { get; set; }
        /// <summary>
        /// status 1 当前 2 转床 3 转区 4 出区 5 取消入区
        /// </summary>
        public string Status { get; set; }
        public string zt { get; set; }

        public string TransDeptName { get; set; }
        public string TransWardName { get; set; }
    }
}
