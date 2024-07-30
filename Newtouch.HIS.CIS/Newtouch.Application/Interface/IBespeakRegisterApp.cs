using Newtouch.Domain.Entity;
using Newtouch.Domain.ViewModels;
using System;
using System.Collections.Generic;

namespace Newtouch.Application.Interface
{
    /// <summary>
    /// 预约挂号
    /// </summary>
    public interface IBespeakRegisterApp
    {
        /// <summary>
        /// 修改预约管理
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        string UpdateBespeakRegisterManage(List<SysBespeakRegisterEntity> entities);

        /// <summary>
        /// 获取默认的预约挂号时间
        /// </summary>
        /// <param name="sourceDate"></param>
        /// <returns></returns>
        List<SysBespeakRegisterDateTimeVO> GetRegisterDateByWeek(DateTime sourceDate);

        /// <summary>
        /// 获取系统配置预约时间段和可预约总数
        /// </summary>
        /// <returns></returns>
        List<SysBespeakRegisterTimeVO> GetConfigureRegTime();

        /// <summary>
        /// 组装追加行HTML
        /// </summary>
        /// <param name="regDatas"></param>
        /// <param name="regTimes"></param>
        /// <returns></returns>
        string BuildNewRow(List<SysBespeakRegisterDateTimeVO> regDatas, List<SysBespeakRegisterTimeVO> regTimes);

        /// <summary>
        /// 提交预约挂号编辑
        /// </summary>
        /// <param name="regData"></param>
        /// <returns></returns>
        string SubmitForm(List<SysBespeakRegisterEntity> regData);

        /// <summary>
        /// 删除过期预约挂号
        /// </summary>
        void DeleteExpireRegData();

        /// <summary>
        /// 获取科室预约挂号信息
        /// </summary>
        /// <param name="deptCode"></param>
        /// <returns></returns>
        string GetRegDataHtmlByKs(string deptCode);

        /// <summary>
        /// 获取科室默认预约挂号
        /// </summary>
        /// <param name="deptCode"></param>
        /// <returns></returns>
        string GetDefaultHtmlByKs(string deptCode);

        /// <summary>
        /// 根据科室和工号组装预约挂号信息
        /// </summary>
        /// <param name="deptCode"></param>
        /// <param name="gh"></param>
        /// <returns></returns>
        string GetRegDataHtmlByKsAndGh(string deptCode, string gh);

        /// <summary>
        /// 获取可预约总人数和已预约数
        /// </summary>
        /// <param name="deptCode"></param>
        /// <param name="regDate"></param>
        /// <param name="regTime"></param>
        /// <param name="gh"></param>
        /// <returns></returns>
        object GetBespeakMaxCount(string deptCode, DateTime regDate, string regTime, string gh);

        /// <summary>
        /// 保存预约挂号
        /// </summary>
        /// <param name="yyId"></param>
        /// <param name="brId"></param>
        /// <param name="blh"></param>
        /// <param name="zjlx"></param>
        /// <param name="zjh"></param>
        /// <returns></returns>
        string SaveYY(string yyId, string brId, string blh, int? zjlx, string zjh);

        /// <summary>
        /// 生成预约排班日历
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="deptCode"></param>
        /// <param name="ysgh"></param>
        /// <returns></returns>
        string BuildCalendar(int year, int month, string deptCode, string ysgh);
    }
}
