using Newtouch.HIS.Proxy.guian.DTO;
using Newtouch.HIS.Proxy.GuiAnXnhReference;
using System;

namespace Newtouch.HIS.Proxy.guian
{
    public static class WebServiceHelper
    {
        #region 接口调用

        public static ResponseBaseDTO<V> xnh_HospitalCall<T, V>(T inBody, string opercode) where V : class
        {
            //return PerformanceMonitoring.RequestMonitor(_ => WebServiceHelper<S04ResponseDTO, S04RequestDTO>.Post(request), request, "BeHospitalizedProxy S04");
            try
            {
                CommonProxy _commonProxy = CommonProxy.GetInstance("");
                string billCode = string.Empty;
                if (_commonProxy.InterfaceS02(out billCode))
                {
                    RequestHeadDTO inHead = new RequestHeadDTO
                    {
                        operCode = opercode,
                        billCode = billCode
                    };
                    string responseDtoStr = WebServiceHelper.xnh_InterfaceCall<RequestHeadDTO, T>(inHead, inBody);
                    return XMLHelper.XmlDeserialize<ResponseBaseDTO<V>>(responseDtoStr);
                }
                else
                {
                    return new ResponseBaseDTO<V>
                    {
                        state = false,
                        message = "S02认证鉴权接口调用失败",
                        data = default(V)
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseBaseDTO<V>
                {
                    state = false,
                    message = ex.Message,
                    data = default(V)
                };
            }

        }
        /// <summary>
        /// 对于返回是List，且data下直接是detail的集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="V"></typeparam>
        /// <param name="inBody"></param>
        /// <param name="opercode"></param>
        /// <returns></returns>
        public static ResponseBaseDetailDTO<V> xnh_HospitalDetailAttrCall<T, V>(T inBody, string opercode) where V : class
        {
            //return PerformanceMonitoring.RequestMonitor(_ => WebServiceHelper<S04ResponseDTO, S04RequestDTO>.Post(request), request, "BeHospitalizedProxy S04");
            try
            {
                CommonProxy _commonProxy = CommonProxy.GetInstance("");
                string billCode = string.Empty;
                if (_commonProxy.InterfaceS02(out billCode))
                {
                    RequestHeadDTO inHead = new RequestHeadDTO
                    {
                        operCode = opercode,
                        billCode = billCode
                    };
                    string responseDtoStr = WebServiceHelper.xnh_InterfaceCall<RequestHeadDTO, T>(inHead, inBody);
                    return XMLHelper.XmlDeserialize<ResponseBaseDetailDTO<V>>(responseDtoStr);
                }
                else
                {
                    return new ResponseBaseDetailDTO<V>
                    {
                        state = false,
                        message = "S02认证鉴权接口调用失败",
                        data = default(V)
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseBaseDetailDTO<V>
                {
                    state = false,
                    message = ex.Message,
                    data = default(V)
                };
            }

        }
        /// <summary>
        /// 对于返回是List，且data下直接是Inp的集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="V"></typeparam>
        /// <param name="inBody"></param>
        /// <param name="opercode"></param>
        /// <returns></returns>
        public static ResponseBaseInpDTO<V> xnh_HospitalInpAttrCall<T, V>(T inBody, string opercode) where V : class
        {
            //return PerformanceMonitoring.RequestMonitor(_ => WebServiceHelper<S04ResponseDTO, S04RequestDTO>.Post(request), request, "BeHospitalizedProxy S04");
            try
            {
                CommonProxy _commonProxy = CommonProxy.GetInstance("");
                string billCode = string.Empty;
                if (_commonProxy.InterfaceS02(out billCode))
                {
                    RequestHeadDTO inHead = new RequestHeadDTO
                    {
                        operCode = opercode,
                        billCode = billCode
                    };
                    string responseDtoStr = WebServiceHelper.xnh_InterfaceCall<RequestHeadDTO, T>(inHead, inBody);
                    return XMLHelper.XmlDeserialize<ResponseBaseInpDTO<V>>(responseDtoStr);
                }
                else
                {
                    return new ResponseBaseInpDTO<V>
                    {
                        state = false,
                        message = "S02认证鉴权接口调用失败",
                        data = default(V)
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseBaseInpDTO<V>
                {
                    state = false,
                    message = ex.Message,
                    data = default(V)
                };
            }

        }
        #endregion
        public static string xnh_InterfaceCall<H,T>(H inHead, T inBody) where H: RequestBaseHeadDTO
        {
            string inBodyXml = XMLHelper.XmlSerialize(inBody);
            inHead.rsa = MD5Helper.GetMd5Hash(inBodyXml);
            string inHeadXml = XMLHelper.XmlSerialize(inHead);
            HisServiceClient _hisServiceClient = new HisServiceClient();
            return _hisServiceClient.request(inHeadXml, inBodyXml);
        }
    }
}