using System;
using System.Web;
using System.Collections.Generic;

using Newtouch.Tools;
using Newtouch.Common.Operator;
using Newtouch.HIS.Repository;
using Newtouch.HIS.Domain.Entity;
using Newtouch.His.OrderService.Client;
using Newtouch.His.OrderService.Contact.DTO;

namespace Newtouch.HIS.DomainServices.Proxy
{
    /// <summary>
    /// 调下单接口
    /// </summary>
    public class CreateOrderProxy
    {
        #region 单例

        private CreateOrderProxy() { }


        public static CreateOrderProxy Instance { get; } = new CreateOrderProxy();

        #endregion

        /// <summary>
        /// 创建暂存单
        /// </summary>
        /// <param name="gh">挂号信息</param>
        /// <param name="ghxmList">挂号项目明细</param>
        /// <param name="mzjs">门诊结算信息</param>
        /// <returns></returns>
        public OrderCreateResponseDTO Book(OutpatientRegistEntity gh, OutpatientSettlementEntity mzjs, List<OutpatientItemEntity> ghxmList)
        {
            return CreateOrder(BuildBookRequest(gh, mzjs, ghxmList));
        }


        /// <summary>
        /// 创建暂存单
        /// </summary>
        /// <param name="orderId">订单号</param>
        /// <returns></returns>
        public OrderCreateResponseDTO Commit(long orderId)
        {
            return CreateOrder(BuildCommitRequest(orderId));
        }


        /// <summary>
        /// 组装下单报文
        /// </summary>
        /// <param name="gh">挂号信息</param>
        /// <param name="ghxmList">挂号项目明细</param>
        /// <param name="mzjs">门诊结算信息</param>
        /// <returns></returns>
        private static OrderCreateRequestDTO BuildBookRequest(OutpatientRegistEntity gh, OutpatientSettlementEntity mzjs, List<OutpatientItemEntity> ghxmList)
        {
            return new OrderCreateRequestDTO
            {
                SubmitOrderType = SubmitOrderTypeEnum.TemporaryOrderNeedPay,
                OrderBasicInfo = BuildOrderBasicInfo(mzjs),
                ContactInfo = BuildContactInfo(gh.patid.ToString()),
                InvoiceInfo = new InvoiceDTO(),
                OrderExtendInfo = new OrderExtendDTO(),
                PaymentInfo = new PaymentDTO(),
                ItemInfo = BuildItems(ghxmList)
            };
        }

        /// <summary>
        /// 组装下单报文
        /// </summary>
        /// <param name="orderId">订单号</param>
        /// <returns></returns>
        private static OrderCreateRequestDTO BuildCommitRequest(long orderId)
        {
            return new OrderCreateRequestDTO
            {
                OrderId = orderId,
                SubmitOrderType = SubmitOrderTypeEnum.OfficialOrder
            };
        }

        /// <summary>
        /// 组装订单基本信息
        /// </summary>
        /// <returns></returns>
        private static OrderBasicDTO BuildOrderBasicInfo(OutpatientSettlementEntity mzjs)
        {
            return new OrderBasicDTO
            {
                Amount = mzjs.zje,
                BookingType = "Offline",
                Eid = OperatorProvider.GetCurrent().UserCode,
                IsDirectlySubmit = false,
                IsOnline = "F",
                IsReceiptFirst = true,
                IsSplitPayment = false,
                IsSuperOrder = false,
                OrderDate = DateTime.Now,
                PkgName = "",
                Remark = "",
                ServerFrom = HttpContext.Current.Request.Url.Host,
                Uid = ""
            };
        }

        /// <summary>
        /// 组装订单联系人信息
        /// </summary>
        /// <param name="patid">病人ID</param>
        /// <returns></returns>
        private static ContactDTO BuildContactInfo(string patid)
        {
            var patientInfo = new SysPatientBasicInfoRepo(new FrameworkBase.MultiOrg.Domain.DBContext.Infrastructure.DefaultDatabaseFactory()).GetInfoByPatid(patid, OperatorProvider.GetCurrent().OrganizeId);
            if (patientInfo == null)
            {
                return null;
            }
            return new ContactDTO
            {
                ContactName = patientInfo.xm,
                ContactMobile = patientInfo.phone,
                ContactTel = patientInfo.dh,
                CountryCode = "86",
                ContactEmail = patientInfo.email,
                ContactFax = "",
                ContactAddress = patientInfo.xian_sheng + patientInfo.xian_shi + patientInfo.xian_xian + patientInfo.xian_dz
            };
        }

        /// <summary>
        /// 组装订单资源信息
        /// </summary>
        /// <param name="ghxmList"></param>
        /// <returns></returns>
        private static List<ItemDTO> BuildItems(List<OutpatientItemEntity> ghxmList)
        {
            var result = new List<ItemDTO>();
            if (ghxmList == null || ghxmList.Count <= 0)
            {
                return result;
            }
            ghxmList.ForEach(p =>
            {
                var item = new ItemDTO
                {
                    ItemID = Convert.ToInt32(p.sfxm),
                    ItemName = "",
                    Quantity = p.sl,
                    Unit = "",
                    UnitPrice = p.dj,
                    TotalPrice = p.je,
                    ResourceType = p.dl,
                    ResourceDesc = "",
                    UseDate = DateTime.Now,
                    ReceiptCode = ""
                };
                result.Add(item);
            });
            return result;
        }

        /// <summary>
        /// 创建暂存单
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private static OrderCreateResponseDTO CreateOrder(OrderCreateRequestDTO request)
        {
            var response = new OrderCreateResponseDTO();
            try
            {
                response = OrderServiceClient.Instance().CreateOrder(request);
                //response = OrderServiceClient.Instance().Test(1);
                return response;
            }
            catch (Exception ex)
            {
                Common.AppLogger.Instance.Error("CreateOrder Error", ex);
                return null;
            }
            finally
            {
                Common.AppLogger.Instance.Info("RequestXML:     " + request.ToJson());
                Common.AppLogger.Instance.Info("ResponseXML:    " + response.ToJson());
            }
        }
    }

}
