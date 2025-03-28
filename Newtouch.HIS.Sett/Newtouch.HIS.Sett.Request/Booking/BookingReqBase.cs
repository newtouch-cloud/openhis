using Newtouch.HIS.API.Common;
using Newtouch.HIS.Sett.Request.Booking;
using Newtouch.Infrastructure.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Newtouch.HIS.Sett.Request
{
    public class BookingReqBase: BookingBase<BookingReqBase>
    {
        [APIRequired]

        public new string AppId { get; set; }
        /// <summary>
        /// 组织机构Id（已经定位到了具体医院）
        /// </summary>
        [APIRequired]
        public string OrganizeId { get; set; }
        /// <summary>
        /// 请求编号
        /// </summary>
        [APIRequired]
        public string methodcode { get; set; }
        public string optype { get; set; }
        /// <summary>
        /// 当前用户
        /// </summary>
        public string user { get; set; }
        /// <summary>
        /// 入参
        /// </summary>
        public Dictionary<string, string> paradata { get; set; }


    }

    public class BookingReqBaseDto
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "AppID is required")]
        [RegularExpression(@"WeChat", ErrorMessage = "AppID is Error")]
        public string AppID { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "HospitalID is required")]
        public string HospitalID { get; set; }
    }
}
