/**********************************************************/
// Copyright (C) 2016 Newtouch 版权所有。 
// Description： 
// Author：
// CreateDate： 2016/12/28 16:06:41 
//**********************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.DTO.OutputDto
{
   public  class OutPatChargeCFDto
    {
        /// <summary>
        /// 处方号
        /// </summary>
        public string  cfh { get; set; }

        /// <summary>
        /// 服务费
        /// </summary>
        public decimal? fwf { get; set; }

        /// <summary>
        /// 金额
        /// </summary>
        public double jeNum { get; set; }
    }
}
