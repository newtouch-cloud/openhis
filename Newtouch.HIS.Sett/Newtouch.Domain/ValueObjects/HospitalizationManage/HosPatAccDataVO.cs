/**********************************************************/
// Copyright (C) 2016 Newtouch 版权所有。 
// Description：账户充值退费  事物处理实体 
// Author：HLF
// CreateDate： 2017/1/11 16:34:07 
//**********************************************************/

using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.Entity.PatientManage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.ValueObjects
{
    public class HosPatAccDataVO
    {

        /// <summary>
        /// 账户收支记录
        /// </summary>
        public SysPatientAccountRevenueAndExpenseEntity szjl { get; set; }
         

        /// <summary>
        /// 更新之前 账户
        /// </summary>
        public SysPatientAccountEntity oldzh { get; set; }
         

        /// <summary>
        /// 更新之前财务收据
        /// </summary>
        public FinanceReceiptEntity oldcwsj { get; set; }

        
    }

    public class PatAccDataVO
    {

        /// <summary>
        /// 账户收支记录
        /// </summary>
        public SysAccountRevenueAndExpenseEntity szjl { get; set; }


        /// <summary>
        /// 更新之前 账户
        /// </summary>
        public SysAccountEntity oldzh { get; set; }


        /// <summary>
        /// 更新之前财务收据
        /// </summary>
        public FinanceReceiptEntity oldcwsj { get; set; }


    }
    public class PatZyAccDataVO
    {

        /// <summary>
        /// 账户收支记录
        /// </summary>
        public InpatientAccountRevenueAndExpenseEntity szjl { get; set; }


        /// <summary>
        /// 更新之前 账户
        /// </summary>
        public InpatientAccountEntity oldzh { get; set; }


        /// <summary>
        /// 更新之前财务收据
        /// </summary>
        public FinanceReceiptEntity oldcwsj { get; set; }


    }
}
