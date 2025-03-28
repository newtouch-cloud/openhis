using Newtouch.EMR.Domain.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.EMR.Domain.ValueObjects
{
    [NotMapped]
    public class MedRecordTreeEditVO:BlmblbEntity
    {
        public string zyh { get; set; }
        public string xm { get; set; }
        public string sex { get; set; }
        public DateTime? birth { get; set; }
        public string brxzmc { get; set; }
        /// <summary>
        /// 患者简述
        /// </summary>
        public string brjs { get; set; }
        public string mzh { get; set; }
    }
}

