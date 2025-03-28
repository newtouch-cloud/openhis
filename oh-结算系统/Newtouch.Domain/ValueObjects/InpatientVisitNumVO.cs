using System;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.ValueObjects
{
    public class InpatientVisitNumVO
    {
        //public string isType { get; set; }

        //public string groupDate { get; set; }

        public int? num { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class InpatientVisitBasicVO
    {

        public string wbegin { get; set; }

        public string wend { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string zyh { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zybz { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime ryrq { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? cyrq { get; set; }
    }

    public class InpatientSCNumVO
    {
        public string name { get; set; }
        public decimal? num { get; set; }
    }

    public class Therapist
    {
        public string title { get; set; }
        public string name { get; set; }
        public decimal? Jan { get; set; }
        public decimal? Feb { get; set; }
        public decimal? Mar { get; set; }
        public decimal? Apr { get; set; }
        public decimal? May { get; set; }
        public decimal? Jun { get; set; }
        public decimal? Jul { get; set; }
        public decimal? Aug { get; set; }
        public decimal? Sep { get; set; }
        public decimal? Oct { get; set; }
        public decimal? Nov { get; set; }
        public decimal? Dec { get; set; }
    }

    public class TherapistVisit
    {
        public string title { get; set; }
        public string name { get; set; }
        public decimal Jan { get; set; }
        public decimal Feb { get; set; }
        public decimal Mar { get; set; }
        public decimal Apr { get; set; }
        public decimal May { get; set; }
        public decimal Jun { get; set; }
        public decimal Jul { get; set; }
        public decimal Aug { get; set; }
        public decimal Sep { get; set; }
        public decimal Oct { get; set; }
        public decimal Nov { get; set; }
        public decimal Dec { get; set; }
    }

    public class TherapistMonthCharge
    {
        public string title { get; set; }
        public string name { get; set; }
        public string gh { get; set; }
        public string ssrq { get; set; }
        public decimal? zje { get; set; }
    }
    public class TherapistMonthVisit
    {
        public string title { get; set; }
        public string name { get; set; }
        public string gh { get; set; }
        public string ssrq { get; set; }
        public decimal? yxris { get; set; }
    }

    public class PatientVisitPerThe
    {
        public List<TherapistVisit> discharge { get; set; }
        public List<TherapistVisit> visitper { get; set; }
    }
    public class PatientMonthVisitPerThe
    {
        public List<TherapistMonthCharge> discharge { get; set; }
        public List<TherapistMonthVisit> visitper { get; set; }
    }
}
