﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeiMengGuYiBaoApp.Models.Input.YiBao
{
    public class Input_3512 : InputBase
    {
        public Data3512 data { get; set; }
    }

    public class Data3512
    {
        /// <summary>
        /// 1|定点医药机构编号|字符型|30|Y|
        /// </summary>
        public string fixmedins_code { get; set; }

        /// <summary>
        /// 2|医药机构目录编码|字符型|150|N|
        /// 必须传其中一个字段：医疗目录编码、医药机构目录编码、定点医药机构批次流水号
        /// </summary>
        public string medins_list_codg { get; set; }

        /// <summary>
        /// 3|定点医药机构批次流水号|字符型|30|N|
        /// 必须传其中一个字段：医疗目录编码、医药机构目录编码、定点医药机构批次流水号
        /// </summary>
        public string fixmedins_bchno { get; set; }

        /// <summary>
        /// 4|开始日期|日期型|Y|yyyy-MM-dd|
        /// </summary>
        public string begndate { get; set; }

        /// <summary>
        /// 5|结束日期|日期型|Y|yyyy-MM-dd|
        /// </summary>
        public string enddate { get; set; }

        /// <summary>
        /// 6|医疗目录编码|字符型|50|N|
        /// 必须传其中一个字段：医疗目录编码、医药机构目录编码、定点医药机构批次流水号
        /// </summary>
        public string med_list_codg { get; set; }

        /// <summary>
        /// 7|药品追溯码|字符型|30|
        /// </summary>
        public string drug_trac_codg { get; set; }
    }



}