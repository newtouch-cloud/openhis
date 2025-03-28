using Newtouch.HIS.Domain.ValueObjects;
using System;
using System.Collections.Generic;

namespace Newtouch.HIS.Domain.IDomainServices
{
    public interface IOrderDmnService
    {
        /// <summary>
        /// 查询订单
        /// </summary>
        /// <param name="vo"></param>
        /// <returns></returns>
        List<OrderInfoVO> OrderQuery(OrderInfoBaseVO vo, string orgId);
        /// <summary>
        /// 订单明细查询
        /// </summary>
        /// <param name="orderNo"></param>
        /// <param name="orgId"></param>
        /// <param name="appId"></param>
        /// <returns></returns>
        OrderDetailVO OrderDetailQuery(string orderNo, string orgId, string appId);
        /// <summary>
        /// 门诊订单明细
        /// </summary>
        /// <param name="orderNo"></param>
        /// <param name="orgId"></param>
        /// <param name="appId"></param>
        /// <returns></returns>
        List<OrderMzVO> OrderDetailMzQuery(string orderNo, string orgId, string appId);
        /// <summary>
        /// 住院订单明细
        /// </summary>
        /// <param name="orderNo"></param>
        /// <param name="orgId"></param>
        /// <param name="appId"></param>
        /// <returns></returns>
        List<OrderZyBaseVO> OrderDetailZyQuery(string orderNo, string orgId, string appId);
        /// <summary>
        /// 创建门诊订单
        /// </summary>
        /// <param name="kh"></param>
        /// <param name="mzh"></param>
        /// <param name="orgId"></param>
        /// <param name="appId"></param>
        /// <returns></returns>
        OrderInfoBaseVO OrderCreateMz(string kh, string mzh, string orgId, string appId,int patId,string dzId);
        /// <summary>
        /// 创建住院结算订单
        /// </summary>
        /// <param name="kh"></param>
        /// <param name="zyh"></param>
        /// <param name="orgId"></param>
        /// <param name="appId"></param>
        /// <returns></returns>
        OrderInfoBaseVO OrderCreateZy(string kh, string zyh, string orgId, string appId);
        /// <summary>
        /// 锁定订单
        /// </summary>
        /// <param name="orderNo"></param>
        /// <param name="kh"></param>
        /// <param name="appId"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        OrderInfoBaseVO LockOrderApply(string orderNo, decimal? orderAmt, string kh, string appId, string orgId);
        /// <summary>
        /// 申请锁定延时
        /// </summary>
        /// <param name="orderNo"></param>
        /// <param name="appId"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        OrderInfoBaseVO LockOrderDelayedApply(string orderNo, string appId, string orgId);
        /// <summary>
        /// 订单支付
        /// </summary>
        /// <param name="vo"></param>
        /// <param name="appId"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        OrderInfoBaseVO OrderPaidSuccess(OrderInfoVO vo, string appId, string orgId);
        /// <summary>
        /// 门诊订单支付step1：支付前订单校验
        /// </summary>
        /// <param name="vo"></param>
        /// <returns></returns>
        OutpOrderVO PayForOutpBillOrderBefore(OrderInfoVO vo, string appId);
        /// <summary>
        /// 住院订单支付：支付前校验
        /// </summary>
        /// <param name="vo"></param>
        /// <param name="appId"></param>
        /// <returns></returns>
        InHosOrderVO PayForInHosBillOrderBefore(OrderInfoVO vo, string appId);
        /// <summary>
        /// 结算成功 业务系统同步推送
        /// </summary>
        /// <param name="cfnmList"></param>
        /// <param name="cfList"></param>
        /// <param name="mzh"></param>
        /// <param name="jsnm"></param>
        /// <param name="fph"></param>
        /// <param name="sfrq"></param>
        /// <param name="appId"></param>
        /// <param name="orgId"></param>
        string PushPresInfo(IList<int> cfnmList, List<string> cfList, string mzh, int jsnm, string fph, DateTime? sfrq, string appId, string orgId, int cflx = 0);
    }
}