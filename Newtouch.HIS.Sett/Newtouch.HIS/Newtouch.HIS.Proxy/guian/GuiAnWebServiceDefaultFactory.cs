using System;
using FrameworkBase.MultiOrg.Domain.DBContext.Infrastructure;
using FrameworkBase.MultiOrg.Repository.SystemManage;
using Newtouch.Core.Redis.Cache;
using Newtouch.HIS.Proxy.Attribute;
using Newtouch.HIS.Proxy.Common;
using Newtouch.HIS.Proxy.guian.DTO;
using Newtouch.HIS.Proxy.guian.DTO.S02;
using Newtouch.HIS.Proxy.GuiAnXnhReference;
using Newtouch.HIS.Proxy.Log;
using NLog.Client.Util;

namespace Newtouch.HIS.Proxy.guian
{
    /// <summary>
    /// GuiAn WebService Factory
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TV"></typeparam>
    public class GuiAnWebServiceDefaultFactory<T, TV>
    {

        public Request<T> Request { get; set; }

        /// <summary>
        /// 组织机构ID
        /// </summary>
        public string OrganizeId { get; set; }

        protected string _userName { get; set; }

        protected string _passWord { get; set; }

        public GuiAnWebServiceDefaultFactory(T body, string organizeId)
        {
            OrganizeId = organizeId;
            Request = new Request<T>
            {
                body = body,
                head = new RequestHeader()
            };
            foreach (var attribute in body.GetType().GetCustomAttributes(true)) //2.通过映射，找到成员属性上关联的特性类实例，
            {
                if (!(attribute is InterfaceCodeAttribute)) continue;
                Request.head.operCode = (attribute as InterfaceCodeAttribute).InterfaceCode;
                break;
            }
            Request.head.rsa = MD5Helper.GetMd5Hash(XMLSerializer.Serialize(body));
            Request.head.billCode = GetBillCode();
        }

        /// <summary>
        /// get billCode
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="passWord"></param>
        /// <returns></returns>
        public string GetBillCode(string userName = "", string passWord = "")
        {
            var sysConfigRepo = new SysConfigRepo(new DefaultDatabaseFactory(), new SERedisCache());
            var userInfo = sysConfigRepo.GetValueByCode("guian_xnh_userInfo", OrganizeId);
            var s02Body = new S02RequestDTO();
            if (!string.IsNullOrWhiteSpace(userInfo))
            {
                s02Body = userInfo.ToObject<S02RequestDTO>();
            }

            s02Body = s02Body ?? new S02RequestDTO();
            _userName = !string.IsNullOrWhiteSpace(userName) ? userName : s02Body.userName;
            _passWord = !string.IsNullOrWhiteSpace(passWord) ? passWord : s02Body.passWord;
            if (!string.IsNullOrWhiteSpace(Request.head.billCode)) return Request.head.billCode;
            var response = CommonProxy.GetInstance(OrganizeId).S02(_userName, _passWord);
            return response.state ? response.data.billCode : "";
        }

        /// <summary>
        /// Call interface
        /// </summary>
        /// <returns></returns>
        public object Call()
        {
            try
            {
                LogCore.WriteInfo("交易代码：" + Request.head.operCode);
                var inHeadXml = XMLSerializer.Serialize(Request.head);
                LogCore.WriteInfo("【" + Request.head.operCode + "】head入参：" + inHeadXml);
                var inBodyXml = XMLSerializer.Serialize(Request.body);
                LogCore.WriteInfo("【" + Request.head.operCode + "】body入参：" + inBodyXml);
                var requestXml = XMLSerializer.Serialize(Request);
                var responseStr = PerformanceMonitoring.RequestMonitor(_ => new HisServiceClient().request(inHeadXml, inBodyXml), requestXml, "S02");
                LogCore.WriteInfo("【" + Request.head.operCode + "】返回：" + responseStr);
                return XMLSerializer.DeSerialize(responseStr, typeof(TV));
            }
            catch (Exception e)
            {
                LogCore.Error("GuiAnWebServiceFactory call error", e);
                LogCore.WriteInfo("【" + Request.head.operCode + "】返回：\n" + string.Format("StackTrace:{0} \n Message:{1} \n InnerException.Message:{2}", e.StackTrace, e.Message, (e.InnerException == null ? "" : e.InnerException.Message)));
                throw;
            }
        }
    }
}