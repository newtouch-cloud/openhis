using System;

namespace Newtouch.HIS.Domain.ValueObjects.PatientManage
{
    public class SysPatientManageSelectVO
    {
        /// <summary>
        /// 
        /// </summary>
        public int patId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string blh { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string xm { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string brxzmc { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string xb { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime? csny { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string brly { get; set; }
        public string CardId { get; set; }
        public string cardTypeName { get; set; }
        public string cardType { get; set; }
        public string zjh { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? LastModifyTime { get; set; }
        public string zt { get; set; }
    }
}
