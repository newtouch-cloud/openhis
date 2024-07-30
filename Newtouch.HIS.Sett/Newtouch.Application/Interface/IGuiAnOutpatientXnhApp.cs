using System;
using System.Collections.Generic;
using Newtouch.HIS.Proxy.guian.DO;
using Newtouch.HIS.Proxy.guian.DTO;
using Newtouch.HIS.Proxy.guian.DTO.S21;
using Newtouch.HIS.Proxy.guian.DTO.S24;
using Newtouch.HIS.Proxy.guian.DTO.S25;

namespace Newtouch.HIS.Application.Interface
{
    /// <summary>
    /// 贵安门诊新农合
    /// </summary>
    public interface IGuiAnOutpatientXnhApp
    {
        /// <summary>
        /// 模拟结算全流程
        /// </summary>
        /// <param name="mzh"></param>
        /// <param name="userCode"></param>
        /// <param name="organizeId"></param>
        /// <param name="s24"></param>
        /// <returns></returns>
        string SimulationSettWholeProcess(string mzh, string userCode, string organizeId, out S24ResponseDTO s24);

        /// <summary>
        /// 模拟结算，单功能
        /// </summary>
        /// <param name="mzh"></param>
        /// <param name="zlkssj"></param>
        /// <param name="organizeId"></param>
        /// <param name="s24Response"></param>
        /// <returns></returns>
        string SimulationSett(string mzh, DateTime zlkssj, string organizeId, out Response<S24ResponseDTO> s24Response);

        /// <summary>
        /// 模拟结算
        /// </summary>
        /// <param name="mzh"></param>
        /// <param name="zlkssj"></param>
        /// <param name="s21Details">上传明细</param>
        /// <param name="organizeId"></param>
        /// <param name="s24Response"></param>
        /// <returns></returns>
        string SimulationSett(string mzh, DateTime zlkssj, List<S21Detail> s21Details, string organizeId, out Response<S24ResponseDTO> s24Response);

        /// <summary>
        /// 回退先前上传的明细
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="outpId"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        string DetailedUploadRetreat(string startDate, string endDate, string outpId, string organizeId);

        /// <summary>
        /// 查询门诊补偿序号
        /// </summary>
        /// <param name="mzh"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        string OutpIdQuery(string mzh, string organizeId);

        /// <summary>
        /// 门诊上传
        /// </summary>
        /// <param name="mzh"></param>
        /// <param name="organizeId"></param>
        /// <param name="zlkssj">诊疗开始时间</param>
        /// <returns></returns>
        string OutpatientUpload(string mzh, string organizeId, string userCode, out DateTime zlkssj);

        /// <summary>
        /// 保存新门诊补偿序号
        /// </summary>
        /// <param name="ghnm"></param>
        /// <param name="userCode"></param>
        /// <param name="mzh"></param>
        /// <param name="organizeId"></param>
        /// <param name="outpId"></param>
        /// <returns></returns>
        void SaveNewOutpId(int ghnm, string userCode, string mzh, string organizeId, string outpId);

        /// <summary>
        /// 保存新门诊补偿序号
        /// </summary>
        /// <param name="ghnm"></param>
        /// <param name="userCode"></param>
        /// <param name="mzh"></param>
        /// <param name="organizeId"></param>
        /// <param name="newOutpId"></param>
        /// <returns></returns>
        void UpdateMzghOutpId(int ghnm, string userCode, string mzh, string organizeId, string newOutpId);

        /// <summary>
        /// 门诊上传回退
        /// </summary>
        /// <param name="mzh"></param>
        /// <param name="organizeId"></param>
        /// <param name="userCode"></param>
        /// <returns></returns>
        string OutpatientUploadReturn(string mzh, string organizeId, string userCode);

        /// <summary>
        /// 门诊回退，可在取消挂号中使用
        /// </summary>
        /// <param name="mzh"></param>
        /// <param name="organizeId"></param>
        /// <param name="userCode"></param>
        /// <returns>失败原因</returns>
        string OutpatientReturn(string mzh, string organizeId, string userCode);

        /// <summary>
        /// 取消结算，门诊红冲
        /// </summary>
        /// <param name="mzh"></param>
        /// <param name="organizeId"></param>
        /// <param name="userCode"></param>
        /// <returns></returns>
        string CancelSett(string mzh, string organizeId, string userCode);

        /// <summary>
        /// 取消结算，门诊红冲
        /// </summary>
        /// <param name="mzh"></param>
        /// <param name="outpId"></param>
        /// <param name="organizeId"></param>
        /// <param name="userCode"></param>
        /// <returns></returns>
        string CancelSett(string mzh, string outpId, string organizeId, string userCode);

        /// <summary>
        /// 新农合门诊结算
        /// </summary>
        /// <param name="mzh"></param>
        /// <param name="organizeId"></param>
        /// <param name="userCode"></param>
        /// <param name="s25"></param>
        /// <param name="outpId"></param>
        /// <returns></returns>
        string Sett(string mzh, string organizeId, string userCode, out S25ResponseDTO s25, out string outpId);

        /// <summary>
        /// 新农合门诊结算
        /// </summary>
        /// <param name="mzh"></param>
        /// <param name="outpId"></param>
        /// <param name="organizeId"></param>
        /// <param name="userCode"></param>
        /// <param name="s25"></param>
        /// <returns></returns>
        string Sett(string mzh, string outpId, string organizeId, string userCode, out S25ResponseDTO s25);

        /// <summary>
        /// 组装退费后要重新生成的结算明细
        /// </summary>
        /// <param name="jsnm"></param>
        /// <param name="tjsxmDict"></param>
        /// <param name="mzh"></param>
        /// <param name="organizeId"></param>
        /// <param name="outpId"></param>
        /// <returns></returns>
        TFS21RequestDTO AssembleS21RequestDto(string jsnm, Dictionary<string, decimal> tjsxmDict, string mzh, string organizeId, string outpId = "");

        /// <summary>
        /// 退费
        /// </summary>
        /// <param name="jsnm"></param>
        /// <param name="tjsxmDict"></param>
        /// <param name="mzh"></param>
        /// <param name="outpId"></param>
        /// <param name="organizeId"></param>
        /// <param name="userCode"></param>
        /// <param name="tfRequest"></param>
        /// <returns></returns>
        string XnhTuiFeiProcess(string jsnm, Dictionary<string, decimal> tjsxmDict, string mzh, string outpId,
           string organizeId, string userCode, out TFS21RequestDTO tfRequest);

        /// <summary>
        /// 新农合医保请求
        /// </summary>
        /// <param name="itn">接口名称  例：S02</param>
        /// <param name="body"></param>
        /// <param name="organizeId"></param>
        /// <param name="response">返回报文</param>
        /// <returns>结果</returns>
        string XnhInterfaceSend(string itn, string body, string organizeId, out string response);
    }
}