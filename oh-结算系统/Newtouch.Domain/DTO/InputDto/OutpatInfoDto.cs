/**********************************************************/
// Copyright (C) 2016 Newtouch 版权所有。 
// Description：收费保存需要的个人信息 
// Author：HLF
// CreateDate： 2017/1/17 13:21:19 
//**********************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.DTO
{
    public class OutpatInfoDto
    {
        /// <summary>
        /// 是否组合病人性质
        /// </summary>
        public bool IsZhbrxz { get; set; }

        public string zh_sbrxz { get; set; }

        public string zh_mbrxz { get; set; }
        /// <summary>
        /// 是否医保病人
        /// </summary>
        public bool isYbbr { get; set; }
        /// <summary>
        /// 病人内码
        /// </summary>
        public int patid { get; set; }



        /// <summary>
        /// 挂号内码
        /// </summary>
        public int ghnm { get; set; }

        /// <summary>
        /// 病人性质
        /// </summary>
        public string brxz { get; set; }
        /// <summary>
        /// 病人性质编号
        /// </summary>
        public int brxzbh { get; set; }

        /// <summary>
        /// 病人性质-名称
        /// </summary>
        public string brxzmc { get; set; }


        /// <summary>
        /// 姓名
        /// </summary>
        public string xm { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public string xb { get; set; }

        /// <summary>
        /// 病历号
        /// </summary>
        public string blh { get; set; }

        /// <summary>
        /// 出生年月
        /// </summary>
        public DateTime? csny { get; set; }

        /// <summary>
        /// 证件号
        /// </summary>
        public string zjh { get; set; }

        /// <summary>
        /// 证件类型
        /// </summary>
        public string zjlx { get; set; }
    }
}
