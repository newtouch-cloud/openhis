/**********************************************************/
// Copyright (C) 2016 Newtouch 版权所有。 
// Description： 
// Author：
// CreateDate： 2016/12/26 19:07:26 
//**********************************************************/

using System;

namespace Newtouch.HIS.Domain.ValueObjects
{
    public class OutPatChargeDoctorVO
    {
        /// <summary>
        /// 医生名称
        /// </summary>
        public string rymc { get; set; }

        /// <summary>
        /// 工号
        /// </summary>
        public string gh { get; set; }

        /// <summary>
        /// 工号
        /// </summary>
        public string ry { get; set; }

        /// <summary>
        /// 科室名称
        /// </summary>
        public string ksmc { get; set; }

        /// <summary>
        /// 科室
        /// </summary>
        public string ks { get; set; }

        /// <summary>
        /// 科室编号
        /// </summary>
        public Int32 ksbh { get; set; }
    }
}
