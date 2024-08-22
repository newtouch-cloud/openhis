/*********************************************************/
// Copyright (C) 2016 Newtouch 版权所有。 
// Description： 住院管理》账户管理》
// Author：HLF
// CreateDate： 2016/12/7 17:01:42 
//**********************************************************/
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.ValueObjects;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IDomainServices
{
    public interface IHosPatAccDmnService
    { 

        /// <summary>
        /// 账户管理》获取病人信息
        /// </summary>
        /// <param name="kh"></param>
        /// <returns></returns>
        HosPatAccountActionVO GetHosPatInfo(string zyh,string orgId);


        /// <summary>
        /// 获取住院管理》账户管理》获取账户收支记录信息
        /// </summary>
        /// <returns></returns>
        List<HosPatAccPayVO> GetAccPayInfo(int zh, string orgId, string zyh = null);

        /// <summary>
        /// 住院结算 获取病人基本信息（基本信息、账户、科室等）
        /// </summary>
        /// <param name="zyh"></param>
        /// <param name="kh"></param>
        /// <returns></returns>
        IList<HospSettPatInfoVO> GetHospSettPatInfo(string zyh, string orgId);

        /// <summary>
        /// 事物保存账户信息
        /// </summary>
        /// <param name="vo"></param>
        void HosPatAccDB(HosPatAccDataVO vo, SysPatientAccountEntity zh, FinanceReceiptEntity cwsj,string type);

        /// <summary>
        /// 获取住院病人基本信息（基本信息、科室等） 在院状态验证
        /// </summary>
        /// <param name="zyh">住院号</param> 
        /// <returns></returns>
        IList<HospPatientInfoVO> GetHospPatientInfo(string zyh, string orgId);
    }
}