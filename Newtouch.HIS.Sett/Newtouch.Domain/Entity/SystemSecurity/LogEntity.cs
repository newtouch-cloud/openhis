/*******************************************************************************
 * Copyright © 2016 Newtouch 版权所有
 * Author: Newtouch
 * Description: 
*********************************************************************************/
using System;

namespace Newtouch.HIS.Domain.Entity
{
    public class SysLogEntity : IEntity<SysLogEntity>
    {
        public string Id { get; set; }
        public DateTime? Date { get; set; }
        public string Account { get; set; }
        public string NickName { get; set; }
        public string Type { get; set; }
        public string IPAddress { get; set; }
        public string IPAddressName { get; set; }
        public string ModuleId { get; set; }
        public string ModuleName { get; set; }
        public bool? Result { get; set; }
        public string Description { get; set; }
        public DateTime CreateTime { get; set; }
        public int? px { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string TopOrganizeId { get; set; }
    }
}
