/**********************************************************/
// Copyright (C) 2016 Newtouch 版权所有。 
// Description： 住院管理》账户管理》界面返回实体
// Author：HLF
// CreateDate： 2016/12/8 10:12:01 
//**********************************************************/

using Newtouch.HIS.Domain.DTO.InputDto;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.DTO.OutputDto
{
    /// <summary>
    /// 预交金
    /// </summary>
   public class ReserveDto
    {
        /// <summary>
        /// 基本信息
        /// </summary>
        public ReservePatientInfoDto patInfo;

        /// <summary>
        /// 账户信息
        /// </summary>
        public List<PatAccPayVO> accPayInfoList;
    }
    [NotMapped]
    public class ReservePatientInfoDto: OutpatAccInfoDto
    {
        public int zhCode { get; set; }
        /// <summary>
        /// 账户性质 区分院内账户时使用
        /// </summary>
        public int? zhxz { get; set; }
    }
}
