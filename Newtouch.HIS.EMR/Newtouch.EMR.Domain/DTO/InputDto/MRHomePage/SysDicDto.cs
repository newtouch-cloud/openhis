﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.EMR.Domain.DTO.InputDto.MRHomePage
{
    public class SysDicDto
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string OrganizeId { get; set; }
        public string py { get; set; }

        public int? order { get; set; }
    }

    public class SysDicMarkDto : SysDicDto
    {
        public string Id { get; set; }
        /// <summary>
        /// 是否选中
        /// </summary>
        public int ischeck{get;set;}
    }
}
