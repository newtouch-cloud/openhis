/**********************************************************/
// Copyright (C) 2016 Newtouch 版权所有。 
// Description： 
// Author：
// CreateDate： 2017/3/1 11:57:05 
//**********************************************************/

namespace Newtouch.HIS.Domain.ValueObjects
{
    public class OutPatChargeProperty
    {
        //[xt_brxz]
        /// <summary>
        /// 病人性质编号
        /// </summary>
        public int brxzbh { get; set; }
        /// <summary>
        /// 病人性质
        /// </summary>
        public string brxz { get; set; }
        /// <summary>
        /// 病人性质名称
        /// </summary>
        public string brxzmc { get; set; }
        /// <summary>
        /// 医保交易类型
        /// </summary>
        public string ybjylx { get; set; }

    }
}
