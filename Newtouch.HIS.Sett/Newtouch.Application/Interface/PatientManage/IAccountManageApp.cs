/*********************************************************/
// Copyright (C) 2016 Newtouch 版权所有。 
// Description： 住院管理》账户管理》
// Author：HLF
// CreateDate： 2016/12/8 12:01:42 
//**********************************************************/
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.ValueObjects;
using System.Collections.Generic;
using Newtouch.HIS.Domain.DTO.InputDto;
using Newtouch.HIS.Domain.DTO.OutputDto;
using Newtouch.Core.Common;

namespace Newtouch.HIS.Application
{
    /// <summary>
    /// 
    /// </summary>
    public interface IAccountManageApp
    {
        ReserveDto GetHosPatAccInfo(int patid);
        ReserveDto GetHosPatAccInfo(int patid, string zhxz);
        List<PatAccPayVO> GetPayList(int zh, string zhxz);
        string GetFinRepSJPZH();
        bool CheckSJPZH(string pzh, out FinanceReceiptEntity sjEntity, out string type);
        bool PayDepositPost(DeposDto dto, out string szid);
        /// <summary>
        /// 充值
        /// </summary>
        /// <param name="dto"></param>
        /// <returns>返回新账户信息</returns>
        SysAccountEntity PayDepositPostAcnt(DeposDto dto);
        bool ExitDepositPost(DeposDto dto);
        /// <summary>
        /// 余额全退
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        bool RefundAccount(DeposDto dto);
        decimal GetzhYe(int zh);
        
        /// <summary>
        /// 获得账户余额
        /// </summary>
        decimal GetZhyeByPatid(int patid);
        decimal GetZhyeByPatid(int patid,int zhxz);

        /// <summary>
        /// 获取患者基本信息
        /// add by HLF
        /// 根据住院号 、卡号 、 姓名查询
        /// </summary>
        /// <param name="zyh">住院号</param>
        /// <param name="kh">卡号</param>
        /// <param name="xm">姓名</param>
        /// <returns></returns>
        HosPatAccountActionVO GetHosPatInfo(string zyh);
    }
}
