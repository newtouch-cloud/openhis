﻿using System.Collections.Generic;
using Newtouch.HIS.Domain.ValueObjects.SystemManage;
using Newtouch.Core.Common;
using Newtouch.HIS.Domain.Entity;
using System;

namespace Newtouch.HIS.Domain.IDomainServices
{
    public interface ISysFinancialDmnService
    {
        /// <summary>
        /// 获取所有发票列表
        /// </summary>
        /// <param name="keyValue"></param>
        /// <param name="fpId"></param>
        /// <returns></returns>
        IList<FinanceReceiptVO> GetFinancialInvoiceList(string keyValue, string OrganizeId, string zt = "1");

        IList<FinanceInvoiceVO> GetCwfpList(string keyValue, string lyry, string OrganizeId);

        /// <summary>
        /// 查询发票详情
        /// </summary>
        /// <param name="kssj"></param>
        /// <param name="jssk"></param>
        /// <param name="orgId"></param>
        /// <param name="creatorCode"></param>
        /// <returns></returns>
        IList<InvoiceDetailVO> InvoiceQueryList(DateTime kssj, DateTime jssj, string orgId, string creatorCode);
    }
}
