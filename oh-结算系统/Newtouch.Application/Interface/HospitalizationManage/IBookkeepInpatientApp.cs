using Newtouch.HIS.Domain.BusinessObjects;
using Newtouch.HIS.Domain.DTO.InputDto.OutpatientAccounting;
using Newtouch.HIS.Domain.DTO.OutputDto;
using Newtouch.HIS.Domain.ValueObjects;
using System;
using System.Collections.Generic;

namespace Newtouch.HIS.Application.Interface
{
    /// <summary>
    /// 
    /// </summary>
    public interface IBookkeepInpatientApp
    {
        /// <summary>
        /// 根据住院号 获取 病人 记账 信息
        /// </summary>
        /// <param name="zyh"></param>
        HosAccountingPatStatusDetailDto GetAccountingStatusDetail(string zyh);

        /// <summary>
        /// 提交记账计划
        /// </summary>
        /// <param name="zyh">住院号</param>
        /// <param name="xmList">记账项目列表（如果是但项目，也要先包装成数组）</param>
        void SubmitAccountingPlan(string zyh, IList<InpatientAccountingPlanItemDto> xmList);

        /// <summary>
        /// 待执行记账计划 查询
        /// </summary>
        /// <param name="zyhList"></param>
        /// <param name="zxrq"></param>
        IList<HospAccountingPlanWaitingExeVO> WaitingAccountingPlanQuery(IList<string> zyhList,string from = null);

        /// <summary>
        /// 执行记账计划
        /// </summary>
        /// <param name="jzjhmxIdList">jzjhmxId列表</param>
        void ExecuteAccountingPlan(IList<zxGirdDto> zxItem, DateTime? zxrq, string from = null);

        /// <summary>
        /// 停止记账计划
        /// </summary>
        /// <param name="jzjhmxIdList">jzjhmxId列表</param>
        void StopAccountingPlan(string jzjhmxIdStr, DateTime stopDate);

        /// <summary>
        /// 取消预停止
        /// </summary>
        /// <param name="jzjhmxIdList">jzjhmxId列表</param>
        void CancelPreStopAccountingPlan(string jzjhmxIdStr);

        /// <summary>
        /// 根据住院号 获取 病人 记账 信息
        /// </summary>
        /// <param name="zyh"></param>
        InpatientHosAccountingPatStatusDetailDto GetInpatientAccountingStatusDetail(string zyh);
    }
}
