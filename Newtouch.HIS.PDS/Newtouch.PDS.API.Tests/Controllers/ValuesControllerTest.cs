using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtouch.Common.Web;
using Newtouch.HIS.API.Common;
using Newtouch.Infrastructure;
using Newtouch.PDS.API.Controllers;
using Newtouch.PDS.Requset.ResourcesOperate;
using Newtouch.Tools.Security;

namespace Newtouch.PDS.API.Tests.Controllers
{
    [TestClass]
    public class ValuesControllerTest
    {
        [TestMethod]
        public void Get()
        {
            var reqObj = new OutpatientBookRequestDTO
            {
                Items = null,
                OrganizeId = "cdbfce48-5494-42aa-a947-07815e546ce5",
                CreatorCode = "fymzyf"
            };
            var apiResp = SiteSettAPIHelper.Request<OutpatientBookRequestDTO, APIRequestHelper.DefaultResponse>("api/ResourcesOperate/Book", reqObj);

            // 断言
            Assert.IsNotNull(apiResp);
            Assert.AreEqual(APIRequestHelper.ResponseResultCode.SUCCESS, apiResp.code);
            //Assert.AreEqual("value1", result.ElementAt(0));
            //Assert.AreEqual("value2", result.ElementAt(1));
        }

        [TestMethod]
        public void GetById()
        {
            // 排列
            ValuesController controller = new ValuesController();

            // 操作
            string result = controller.Get(5);

            // 断言
            Assert.AreEqual("value", result);
        }

        /// <summary>
        /// 测试token
        /// </summary>
        [TestMethod]
        public void TestTokenWithJson()
        {
            var request = new
            {
                AppId = "newtouch.pds.test",
                CardNo = "No.0001",
                Jsnm = 123123,
                Sfsj = DateTime.Now,
                Cfh = "N00012",
                Cfnm = 123123,
                Timestamp = DateTime.Now
            };
            var response = SitePDSAPIHelper.Request<object, DefaultResponse>("api/test/TestToken", request);
            Assert.IsNotNull(response);
            Assert.IsTrue(response.code == ResponseResultCode.SUCCESS);
        }

        /// <summary>
        /// 测试token
        /// </summary>
        [TestMethod]
        public void TestTokenWithObj()
        {
            var request = new OutpatientCommitRequestDTO
            {
                AppId = "newtouch.pds.test",
                CardNo = "No.0001",
                Jsnm = 123123,
                Sfsj = DateTime.Now,
                Cfh = "N00012",
                Cfnm = 123123,
                Timestamp = DateTime.Now
            };
            var response = SitePDSAPIHelper.Request<object, DefaultResponse>("api/test/TestToken", request);
            Assert.IsNotNull(response);
            Assert.IsTrue(response.code == ResponseResultCode.SUCCESS);
        }

        /// <summary>
        /// 测试token
        /// </summary>
        [TestMethod]
        public void TestTokenWithStr()
        {
            var request = "123123";
            var response = SitePDSAPIHelper.Request<object, DefaultResponse>("api/test/TestToken", request);
            Assert.IsNotNull(response);
            Assert.IsTrue(response.code == ResponseResultCode.SUCCESS);
        }
    }
}
