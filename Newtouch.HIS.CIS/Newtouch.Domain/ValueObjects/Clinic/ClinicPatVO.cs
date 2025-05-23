﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Domain.ValueObjects.Clinic
{
    public class ClinicPatVO
    {
        public string Id { get; set; }
        public string OrganizeId { get; set; }
        public DateTime? sqsj { get; set; }
        public DateTime? jzsj { get; set; }
        public string ks { get; set; }
        public string ysgh { get; set; }
        public int? sqzt { get; set; }
        public string patid { get; set; }
        public string xm { get; set; }
        public string xb { get; set; }
        public string nl { get; set; }
        public string zjh { get; set; }
        public DateTime? birth { get; set; }
        public string brxz { get; set; }
        public string mettingId { get; set; }
        public string sqr { get; set; }
        public string sqlsh { get; set; }
        public string kh { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CreatorCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? LastModifyTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string LastModifierCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string zt { get; set; }

        public string sexValue { get; set; }
        public string brxzmc { get; set; }
        public string nlshow { get; set; }
        public string mzh { get; set; }
        public string blh { get; set; }
        public string brxzCode { get; set; }
        public DateTime? csny { get; set; }
        public string zjlx { get; set; }
        public string ghksmc { get; set; }
        public string ysxm { get; set; }
        public string ghsj { get; set; }
        public string jzId { get; set; }
    }
}
