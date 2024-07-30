using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Soap;
using FrameworkBase.MultiOrg.Domain.ValueObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtouch.CIS.Proxy;
using Newtouch.CIS.Proxy.CMMPlatform.DTO;
using Newtouch.CIS.Proxy.CMMPlatform.DTO.TCM_HIS_01;
using Newtouch.CIS.Proxy.CMMPlatform.DTO.TCM_HIS_08;
using Newtouch.CIS.Proxy.CMMPlatform.DTO.TCM_HIS_09;
using Newtouch.CIS.Proxy.Dapper;
using Newtouch.CIS.Proxy.TcmHis01ServiceReference;
using Newtouch.Infrastructure;
using FileHelper = Newtouch.Tools.FileHelper;

namespace Newtouch.CIS.UnitTestProject
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var patient = new Patient
            {
                id = "03132", //取病历号
                name = "刘伟浩",
                genderCode = "2261.1-2003",
                gender = "1",
                birthday = "20171031",
                cardTypeCode = "364-2011",
                cardType = "01",
                cardNo = "520421201710312217",
                provinceCode = "34",
                cityCode = "3401",
                areaCode = "340101",
                outpatientNo = "1906280350049", //门诊号
                orgCode = "520000002312"
            };
            var result = ProxyDapperFactory.CreateTcmhis01ProxyDapper(patient).Execute() as Result;
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TestMethod2()
        {
            var result = new List<TCM_HEAL_03Response>();
            try
            {
                using (FileStream fs = new FileStream(@"D:\BugReport.txt", FileMode.Open, FileAccess.Read,
                    FileShare.Read))
                {
                    SoapFormatter soapFormat = new SoapFormatter();
                    result = (List<TCM_HEAL_03Response>)soapFormat.Deserialize(fs);
                }

                Assert.IsNotNull(result);
            }
            catch (Exception e)
            {
                Console.Write(e);
                throw;
            }

        }

        [TestMethod]
        public void TestMethod3()
        {
            try
            {
                var obj = new TCM_HEAL_03Response
                {
                    @return =
                        @"&lt;Response&gt;&lt;Header&gt;&lt;sender&gt;HIS&lt;/sender&gt;&lt;receiver&gt;EMR,HEAL,PLAT&lt;/receiver&gt;&lt;sendTime&gt;20190820174912&lt;/sendTime&gt;&lt;msgType&gt;TCM_HIS_01&lt;/msgType&gt;&lt;msgID&gt;HIS20190820174912&lt;/msgID&gt;&lt;/Header&gt;&lt;Result&gt;&lt;code&gt;1&lt;/code&gt;&lt;desc&gt;&#x6210;&#x529F;&lt;/desc&gt;&lt;/Result&gt;&lt;Object/&gt;&lt;/Response&gt;"
                };

                SoapFormatter soapFormat = new SoapFormatter();
                using (Stream stream = new FileStream(@"D:\BugReport2.txt", FileMode.Create, FileAccess.Write,
                    FileShare.None))
                {
                    soapFormat.Serialize(stream, obj);
                }


                //Assert.IsNotNull(result);
            }
            catch (Exception e)
            {
                var str = FileHelper.ReadAllLines(@"D:\BugReport.txt");
                var obj = str.XmlDeSerialize<acceptMessageResponse>();
                throw;
            }

        }

        [TestMethod]
        public void TestMethod4()
        {
            try
            {
                var obj = new TCM_HEAL_03Response();

                SoapFormatter soapFormat = new SoapFormatter();
                using (Stream stream =
                    new FileStream(@"D:\BugReport2.txt", FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    obj = (TCM_HEAL_03Response)soapFormat.Deserialize(stream);
                }

                Assert.IsNotNull(obj);
            }
            catch (Exception e)
            {
                Console.Write(e);
            }
        }

        [TestMethod]
        public void TestMethod5()
        {
            try
            {
                var str = FileHelper.ReadAllLines(@"D:\BugReport.txt");
                Assert.IsTrue(str.IsReturnSuccess());
                str = str.ExtractReturnContentAndParsing();
                Assert.IsNotNull(str);
            }
            catch (Exception e)
            {
                Console.Write(e);
                throw;
            }
        }


        [TestMethod]
        public void TestMethod7()
        {
            var str = FileHelper.ReadAllLines(@"D:\BugReport.txt");
            var obj = str.XmlDeSerialize<PullDiagInfoRequestEntity>();
            Assert.IsNotNull(obj);
            Assert.IsFalse(obj.DiagInfo == null);
        }

        [TestMethod]
        public void TestMethod8()
        {
            SfxmYpSelectResultVO item = new SfxmYpSelectResultVO
            {
                bz = "1",
                cls = 2,
                dj = 3,
                dw = "g",
                isKss = "true",
                //drugCode = "123123",
                //drugQuantity = "2",
                //drugUnit = "ggg"
            };
            var cl = new List<SfxmYpSelectResultVO> { item };
            DrugDataEx p = new DrugDataEx();
            p = (DrugDataEx)item;
            p.ybdm = "123";
            Assert.IsNotNull(p);
        }
    }

    [Serializable]
    public class TCM_HEAL_03Response
    {
        public string @return { get; set; }
    }
}
