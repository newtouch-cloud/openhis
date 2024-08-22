/**********************************************************/
// Copyright (C) 2016 Newtouch 版权所有。 
// Description：门诊收费信息集合
// Author：HLF
// CreateDate： 2016/12/26 18:38:25 
//**********************************************************/

using Newtouch.HIS.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.DTO.OutputDto
{
    public class OutPatChargeDto
    {
        /// <summary>
        /// 病人基本信息
        /// </summary>
        public OutPatChargeInfoVO patInfo;

        /// <summary>
        /// 挂号信息
        /// </summary>
        public List<OutPatChargeItemVO> ghItemList;

        /// <summary>
        /// 挂号专家和科室信息
        /// </summary>
        public OutPatChargeDoctorVO ghDocVo;
    }
}
