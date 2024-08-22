using Newtouch.HIS.Domain.ValueObjects.API;
using System.Collections.Generic;
using Newtouch.HIS.Domain.DTO.OutputDto;
using Newtouch.HIS.Domain.ValueObjects;
using Newtouch.HIS.Sett.Request;
using Newtouch.HIS.Sett.Request.Booking.Response;
using Newtouch.HIS.Sett.Request.Booking.Request;
using Newtouch.HIS.Domain.DTO.InputDto;

namespace Newtouch.HIS.Domain.IDomainServices.API
{
    public interface IBookingRegisterDmnService
    {
        IList<CardInfoResp> GetCardInfo(CardInfoReq req);

        /// <summary>
        /// 获取科室信息
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="ks"></param>
        /// <param name="ksmc"></param>
        /// <returns></returns>
        IList<DepartmentResp> GetDepartmentInfo(DepartmentDTO dto);
        /// <summary>
        /// 门诊排班 schedule
        /// </summary>
        /// <returns></returns>
        IList<MzpbScheduleResponse> GetMzpbSchedule(DepartmentSchedulingDTO dto);
        /// <summary>
        /// 科室排班信息
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        IList<MzpbResponse> GetMzpb(MzKsPbDto dto);
        /// <summary>
        /// 出诊科室排班详细
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        IList<MzpbScheduleResponse> GetMzpbDetail(MzKsPbDto dto);
        /// <summary>
        /// 挂号预约
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        MzAppointmentResp OutAppointment(MzAppointmentReq dto);
        /// <summary>
        /// 签到
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        RegSettResp OutpatRegSett(RegSettReq dto);
        /// <summary>
        /// 预约号查询预约信息
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        MzAppointmentRecordResp QueryAppRecord(MzAppointmentRecordReq req);

        /// <summary>
        /// 预约list
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        IList<MzAppointmentRecordListResp> QueryAppRecordList(MzAppointmentRecordListReq req);
        /// <summary>
        /// 取消预约
        /// </summary>
        /// <param name="req"></param>
        int CancelOutApp(MzAppointmentRecordReq req);
        /// <summary>
        /// 获取医生/科室医生
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        IList<DeptDoctorResp> GetStaffInfo(DeptDoctorReq req);
        /// <summary>
        /// 建卡
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        CardInfoResp SysPatInfoSet(RegisterReq req);
        /// <summary>
        /// 取消结算
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        CancalSettResp CancalSett(CancalSettReq req);
        /// <summary>
        /// 待缴费订单
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        IList<CostOrderResp> QueryCostOrder(CostOrderReq req);
        /// <summary>
        /// 待缴费订单明细
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        CostOrderDetailResp QueryCostOrderDetail(CostOrderDetailReq req);
        /// <summary>
        /// 取消订单
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        int CancalOrde(CancalOrderReq req);
        /// <summary>
        /// 重置订单状态
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        int ErrOrderCancal(CancalOrderReq req);
        /// <summary>
        /// 预结算
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        PreSettResp OutPreSett(PreSettReq req);
        /// <summary>
        /// 处方结算
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        void OutSett(SettReq req, int jsnm,string orgId);
        /// <summary>
        /// 对账信息
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        List<ContrastBill> ContrastBill(ContrastBillReq req);

        string ValidFee(string mzh, string orgId);
        List<CfOrderVo> GetOrderAmount(string mzh, string orgId);
        BasicInfoDto2018 getPatInfo(string mzh, string orgId);
        List<int> getCfnmList(string mzh, string orgId);
    }
}
