using FrameworkBase.MultiOrg.Infrastructure;
using Newtouch.Core.Common.Utils;
using Newtouch.HIS.Domain.DTO.PharmacyDrugStorage;
using Newtouch.HIS.Domain.IDomainServices.PharmacyDrugStorage;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtouch.Infrastructure;
using Newtouch.HIS.Domain.Entity.PharmacyDrugStorage;
using System.Xml;
using System.IO;
using System.Xml.Serialization;
using System.Xml.Linq;
using Newtonsoft.Json;
using System.Security.Cryptography;
using Newtouch.HIS.Domain.IRepository.PharmacyDrugStorage;
using Newtouch.HIS.DomainServices.PurchaseWebService;
using Newtouch.Infrastructure.Log;

namespace Newtouch.HIS.DomainServices.PharmacyDrugStorage
{
    public class SunPurchaseDmnService : DmnServiceBase, ISunPurchaseDmnService
    {
        public SunPurchaseDmnService(IDefaultDatabaseFactory databaseFactory) : base(databaseFactory)
        {
        }

        private readonly IPurchaseRepo _purchaseRepo;

        public string sJgbm = ConfigurationHelper.GetAppConfigValue("OrganizeCodeSd");
        public string HospitalName = ConfigurationHelper.GetAppConfigValue("HospitalName");
        public string sUser = ConfigurationHelper.GetAppConfigValue("PurchaseUser");
        public string sPwd = ConfigurationHelper.GetAppConfigValue("PurchasePwd");
        public string sVersion = "1.0.0.0";
        #region 接口公共方法

        /// <summary>
        /// SHA1加密计算
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public string getMessageDigest(string content)
        {
            try
            {
                using (SHA1 sha1 = SHA1.Create())
                {
                    byte[] inputBytes = Encoding.UTF8.GetBytes(content);
                    byte[] hashBytes = sha1.ComputeHash(inputBytes);

                    StringBuilder sb = new StringBuilder();
                    foreach (byte b in hashBytes)
                    {
                        sb.Append(b.ToString("x2"));
                    }
                    return sb.ToString().ToUpper();
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        //读取xml指定节点的值
        public string ReadXmlNodes(string xml, string node)
        {
            string text = string.Empty;
            XmlDocument xmlDoc = new XmlDocument();
            try
            {
                xmlDoc.LoadXml(xml);
                XmlNode nodeA = xmlDoc.SelectSingleNode("XMLDATA");
                XmlNode nodeB = nodeA.SelectSingleNode("MAIN");
                XmlNode nodeC = nodeB.SelectSingleNode(node);
                if (nodeC != null)
                {
                    text = nodeC.InnerText;
                }
            }
            catch (Exception ex)
            {
                AppLogger.Info("读取xml指定节点的值异常: " + ex);
            }
            return text;
        }
        public string ReadXmlContent(string xml, string nodeUrl)
        {

            string text = string.Empty;
            XmlDocument xmlDoc = new XmlDocument();
            try
            {
                xmlDoc.LoadXml(xml);
                XmlNode node = xmlDoc.SelectSingleNode(nodeUrl);
                if (node != null)
                {
                    text = node.InnerText;
                }
            }
            catch (Exception ex)
            {
                AppLogger.Info("读取xml指定节点的值异常: " + ex);
                throw new Exception(ex.ToString());
            }
            return text;
        }

        //查询错误提示
        public void GetErrorOut(string responseXML)
        {
            //错误提示
            var ztcljg = ReadXmlContent(responseXML, "XMLDATA/HEAD/ZTCLJG");
            if (ztcljg != "00000")
            {
                var cwxx = ReadXmlContent(responseXML, "XMLDATA/HEAD/CWXX");
                throw new Exception(cwxx);
            }
        }


        public PurchaseHead GetHead()
        {
            PurchaseHead head = new PurchaseHead();
            head.IP = "192.168.2.140";
            head.MAC = "CC96E5EF3897";
            head.BZXX = "备注信息";
            return head;
        }

        public string PurchaseInterface(string responsexml, string OrganizeId, string param, string jydm, string jymc)
        {
            using (var db = new EFDbTransaction(this._databaseFactory).BeginTrans())
            {
                try
                {
                    //var sUser = "";
                    //var sPwd = "";
                    //var sJgbm = "";
                    //var sVersion = "1.0.0.0";
                    //var sXxlx = "";
                    //var sSign = "";
                    //var xmlData = responsexml;
                    //var responseXML = "<?xml version=\"1.0\" encoding=\"utf - 8\"?><XMLDATA><HEAD><JSSJ>接收时间</JSSJ><ZTCLJG>消息主体处理结果</ZTCLJG><CWXX>错误提示内容</CWXX><BZXX>备注信息</BZXX></HEAD><MAIN><DDBH>订单编号</DDBH></MAIN><DETAIL><OutputDetailYY009><DDMXBH>订单明细编号</DDMXBH><SXH>顺序号</SXH><SPLX>商品类型</SPLX><ZXSPBM>商品统编代码</ZXSPBM><CLJG>处理结果</CLJG><CLQKMS>处理情况描述</CLQKMS></OutputDetailYY009></DETAIL></XMLDATA>";

                    //responsexml = responsexml.XmlSerialize().Replace("\r\n", "").Replace(" ", "");
                    //responsexml = responsexml.Replace("<?xmlversion=\"1.0\"?>", "<?xml version=\"1.0\" encoding=\"utf-8\"?>");

                    var sXxlx = jydm;
                    var sSign = getMessageDigest(responsexml);

                    //调用阳光采购平台WebService接口
                    AppLogger.Info("============= " + sXxlx + " ============= ");
                    AppLogger.Info("调用采购交易规定入参: " + sUser + "," + sPwd + "," + sJgbm + "," + sVersion + "," + sXxlx + "," + sSign);
                    AppLogger.Info("调用采购交易请求url: " + responsexml);
                    YsxtMainServiceClient ysxtMainServiceClient = new YsxtMainServiceClient();
                    var responseXML = ysxtMainServiceClient.sendRecv(sUser, sPwd, sJgbm, sVersion, sXxlx, sSign, responsexml);
                    AppLogger.Info("调用采购交易出参: " + responseXML);
                    
                    //var jsonout = responseXML.XmlDeSerialize<OutputYY009>();
                    //var purchaseLogEntity = new PurchaseLogEntity
                    //{
                    //    OrganizeId = OrganizeId,
                    //    jydm = jydm,
                    //    jymc = jymc,
                    //    param = param,
                    //    XmlRequest = responsexml,
                    //    XmlResponse = responseXML,
                    //    zt = "1",
                    //};
                    //purchaseLogEntity.Create(true);
                    //db.Insert(purchaseLogEntity);
                    //db.Commit();
                }
                catch (Exception ex)
                {

                    return ex.Message;
                }
                return "";
            }
        }
        #endregion

        #region 阳光采购平台接口
        /// <summary>
        /// 订单填报 YY009
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="cgId"></param>
        /// <returns></returns>

        public string Purchase_YY009(string orgId, string cgId)
        {
            try
            {

                string HospitalCode = ConfigurationHelper.GetAppConfigValue("OrganizeCodeSd");
                string HospitalName = ConfigurationHelper.GetAppConfigValue("HospitalName");
                //main
                var mainsqlpar = new List<SqlParameter>();
                var mainsql = @"exec usp_PurchaseMain_YY009 @orgId,@cgId";
                mainsqlpar.Add(new SqlParameter("@orgId", orgId));
                mainsqlpar.Add(new SqlParameter("@cgId", cgId));
                PurchaseMainYY009 main = FirstOrDefault<PurchaseMainYY009>(mainsql.ToString(), mainsqlpar.ToArray());
                //detail
                var detailsqlpar = new List<SqlParameter>();
                var detailsql = @"exec usp_PurchaseDetail_YY009 @orgId,@cgId";
                detailsqlpar.Add(new SqlParameter("@orgId", orgId));
                detailsqlpar.Add(new SqlParameter("@cgId", cgId));
                List<STRUCT> detail = FindList<STRUCT>(detailsql.ToString(), detailsqlpar.ToArray());

                PurchaseHead head = GetHead();
                PurchaseYY009 xmlData = new PurchaseYY009();
                xmlData.MAIN = main;
                xmlData.DETAIL = detail;
                xmlData.HEAD = head;
                //string responsexml = xmlData.XmlSerialize().Replace("<?xml version=\"1.0\"?>", "<?xml version=\"1.0\" encoding=\"utf-8\"?>");

                string responsexml = xmlData.XmlSerialize().Replace("\r\n", "").Replace(" ", "");
                responsexml = responsexml.Replace("<?xmlversion=\"1.0\"?>", "<?xml version=\"1.0\" encoding=\"utf-8\"?>");

                // var param = cgId;
                //var logstr = PurchaseInterface(responsexml, orgId,param,"YY009", "订单填报");//接口

                var sXxlx = "YY009";
                var sSign = getMessageDigest(responsexml);
                //调用阳光采购平台WebService接口
                AppLogger.Info("============= " + sXxlx + " ============= ");
                AppLogger.Info("调用采购交易规定入参: " + sUser + "," + sPwd + "," + sJgbm + "," + sVersion + "," + sXxlx + "," + sSign);
                AppLogger.Info("调用采购交易请求url: " + responsexml);
                YsxtMainServiceClient ysxtMainServiceClient = new YsxtMainServiceClient();
                var responseXML = ysxtMainServiceClient.sendRecv(sUser, sPwd, sJgbm, sVersion, sXxlx, sSign, responsexml);
                AppLogger.Info("调用采购交易出参: " + responseXML);

                var param = cgId;
                using (var db = new EFDbTransaction(this._databaseFactory).BeginTrans())
                {
                    var purchaseLogEntity = new PurchaseLogEntity
                    {
                        OrganizeId = orgId,
                        jydm = "YY009",
                        jymc = "订单填报",
                        param = param,
                        XmlRequest = responsexml,
                        XmlResponse = responseXML,
                        zt = "1",
                    };
                    purchaseLogEntity.Create(true);
                    db.Insert(purchaseLogEntity);
                    db.Commit();
                }

                //错误提示
                var ztcljg = ReadXmlContent(responseXML, "XMLDATA/HEAD/ZTCLJG");
                if (ztcljg != "00000")
                {
                    var cwxx = ReadXmlContent(responseXML, "XMLDATA/HEAD/CWXX");
                    throw new Exception(cwxx);
                }


                //更新订单编号
                //var ddbh = entity.MAIN.DDBH;
                //var ddbh = entity.MAIN.DDBH;
                //_purchaseRepo.PurchaseDdbhUpdate(cgId, ddbh, orgId);

                var ddbh = ReadXmlNodes(responseXML, "DDBH");
                AppLogger.Info("调用采购交易返回DDBH： " + ddbh);
                return ddbh;
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        /// <summary>
        /// 订单填报确认 YY010
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="cgId"></param>
        /// <returns></returns>
        public string Purchase_YY010(string orgId, string cgId)
        {
            try
            {

                //string HospitalCode = ConfigurationHelper.GetAppConfigValue("OrganizeCodeSd");
                //string HospitalName = ConfigurationHelper.GetAppConfigValue("HospitalName");
                //main
                var mainsqlpar = new List<SqlParameter>();
                var mainsql = @"exec usp_PurchaseMain_YY010 @orgId,@cgId";
                mainsqlpar.Add(new SqlParameter("@orgId", orgId));
                mainsqlpar.Add(new SqlParameter("@cgId", cgId));
                PurchaseMainYY010 main = FirstOrDefault<PurchaseMainYY010>(mainsql.ToString(), mainsqlpar.ToArray());

                PurchaseHead head = GetHead();
                PurchaseYY010 xmlData = new PurchaseYY010();
                xmlData.MAIN = main;
                xmlData.HEAD = head;
                //string responsexml = xmlData.XmlSerialize().Replace("<?xml version=\"1.0\"?>", "<?xml version=\"1.0\" encoding=\"utf-8\"?>");

                string responsexml = xmlData.XmlSerialize().Replace("\r\n", "").Replace(" ", "");
                responsexml = responsexml.Replace("<?xmlversion=\"1.0\"?>", "<?xml version=\"1.0\" encoding=\"utf-8\"?>");

                //var logstr = PurchaseInterface(responsexml, orgId, param, "YY010", "订单填报确认");//接口

                var sXxlx = "YY010";
                var sSign = getMessageDigest(responsexml);
                //调用阳光采购平台WebService接口
                AppLogger.Info("============= " + sXxlx + " ============= ");
                AppLogger.Info("调用采购交易规定入参: " + sUser + "," + sPwd + "," + sJgbm + "," + sVersion + "," + sXxlx + "," + sSign);
                AppLogger.Info("调用采购交易请求url: " + responsexml);
                YsxtMainServiceClient ysxtMainServiceClient = new YsxtMainServiceClient();
                var responseXML = ysxtMainServiceClient.sendRecv(sUser, sPwd, sJgbm, sVersion, sXxlx, sSign, responsexml);
                AppLogger.Info("调用采购交易出参: " + responseXML);

                var param = cgId;
                using (var db = new EFDbTransaction(this._databaseFactory).BeginTrans())
                {
                    var purchaseLogEntity = new PurchaseLogEntity
                    {
                        OrganizeId = orgId,
                        jydm = "YY010",
                        jymc = "订单填报确认",
                        param = param,
                        XmlRequest = responsexml,
                        XmlResponse = responseXML,
                        zt = "1",
                    };
                    purchaseLogEntity.Create(true);
                    db.Insert(purchaseLogEntity);
                    db.Commit();
                }

                //错误提示
                var ztcljg = ReadXmlContent(responseXML, "XMLDATA/HEAD/ZTCLJG");
                if (ztcljg != "00000")
                {
                    var cwxx = ReadXmlContent(responseXML, "XMLDATA/HEAD/CWXX");
                    throw new Exception(cwxx);
                }
                return "";

            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public string Purchase_YY011(string orgId, string cgId)
        {
            try
            {

                string HospitalCode = ConfigurationHelper.GetAppConfigValue("OrganizeCodeSd");
                string HospitalName = ConfigurationHelper.GetAppConfigValue("HospitalName");
                //main
                var mainsqlpar = new List<SqlParameter>();
                var mainsql = @"exec usp_PurchaseMain_YY011 @orgId,@cgId";
                mainsqlpar.Add(new SqlParameter("@orgId", orgId));
                mainsqlpar.Add(new SqlParameter("@cgId", cgId));
                PurchaseMainYY011 main = FirstOrDefault<PurchaseMainYY011>(mainsql.ToString(), mainsqlpar.ToArray());

                PurchaseHead head = GetHead();
                PurchaseYY011 xmlData = new PurchaseYY011();
                xmlData.MAIN = main;
                xmlData.HEAD = head;
                //string responsexml = xmlData.XmlSerialize().Replace("<?xml version=\"1.0\"?>", "<?xml version=\"1.0\" encoding=\"utf-8\"?>");

                string responsexml = xmlData.XmlSerialize().Replace("\r\n", "").Replace(" ", "");
                responsexml = responsexml.Replace("<?xmlversion=\"1.0\"?>", "<?xml version=\"1.0\" encoding=\"utf-8\"?>");

                var param = cgId;

                var sXxlx = "YY011";
                var sSign = getMessageDigest(responsexml);
                //调用阳光采购平台WebService接口
                AppLogger.Info("============= " + sXxlx + " ============= ");
                AppLogger.Info("调用采购交易规定入参: " + sUser + "," + sPwd + "," + sJgbm + "," + sVersion + "," + sXxlx + "," + sSign);
                AppLogger.Info("调用采购交易请求url: " + responsexml);
                YsxtMainServiceClient ysxtMainServiceClient = new YsxtMainServiceClient();
                var responseXML = ysxtMainServiceClient.sendRecv(sUser, sPwd, sJgbm, sVersion, sXxlx, sSign, responsexml);
                AppLogger.Info("调用采购交易出参: " + responseXML);
                using (var db = new EFDbTransaction(this._databaseFactory).BeginTrans())
                {
                    var purchaseLogEntity = new PurchaseLogEntity
                    {
                        OrganizeId = orgId,
                        jydm = "YY011",
                        jymc = "退货单填报",
                        param = param,
                        XmlRequest = responsexml,
                        XmlResponse = responseXML,
                        zt = "1",
                    };
                    purchaseLogEntity.Create(true);
                    db.Insert(purchaseLogEntity);
                    db.Commit();
                }
                //错误提示
                var ztcljg = ReadXmlContent(responseXML, "XMLDATA/HEAD/ZTCLJG");
                if (ztcljg != "00000")
                {
                    var cwxx = ReadXmlContent(responseXML, "XMLDATA/HEAD/CWXX");
                    throw new Exception(cwxx);
                }

                return null;

            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        /// <summary>
        /// 退货单填报确认 YY012
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="cgId"></param>
        /// <returns></returns>
        public string Purchase_YY012(string orgId, string cgId)
        {
            try
            {

                string HospitalCode = ConfigurationHelper.GetAppConfigValue("OrganizeCodeSd");
                string HospitalName = ConfigurationHelper.GetAppConfigValue("HospitalName");
                //main
                var mainsqlpar = new List<SqlParameter>();
                var mainsql = @"exec usp_PurchaseMain_YY012 @orgId,@cgId";
                mainsqlpar.Add(new SqlParameter("@orgId", orgId));
                mainsqlpar.Add(new SqlParameter("@cgId", cgId));
                PurchaseMainYY012 main = FirstOrDefault<PurchaseMainYY012>(mainsql.ToString(), mainsqlpar.ToArray());

                PurchaseHead head = GetHead();
                PurchaseYY012 xmlData = new PurchaseYY012();
                xmlData.MAIN = main;
                xmlData.HEAD = head;
                //string responsexml = xmlData.XmlSerialize().Replace("<?xml version=\"1.0\"?>", "<?xml version=\"1.0\" encoding=\"utf-8\"?>");

                string responsexml = xmlData.XmlSerialize().Replace("\r\n", "").Replace(" ", "");
                responsexml = responsexml.Replace("<?xmlversion=\"1.0\"?>", "<?xml version=\"1.0\" encoding=\"utf-8\"?>");

                //var param = "serviceId{=}AUDIT00101{,}userName{=}admin{,}password{=}adm@123{,}clientMAC{=}";
                var param = cgId;
                using (var db = new EFDbTransaction(this._databaseFactory).BeginTrans())
                {
                    //var responseXML = "<XMLDATA><HEAD><JSSJ>20130831/102341/</JSSJ><ZTCLJG>00000</ZTCLJG><CWXX/><BZXX/></HEAD></XMLDATA>";
                    var sXxlx = "YY012";
                    var sSign = getMessageDigest(responsexml);
                    //调用阳光采购平台WebService接口
                    AppLogger.Info("============= " + sXxlx + " ============= ");
                    AppLogger.Info("调用采购交易规定入参: " + sUser + "," + sPwd + "," + sJgbm + "," + sVersion + "," + sXxlx + "," + sSign);
                    AppLogger.Info("调用采购交易请求url: " + responsexml);
                    YsxtMainServiceClient ysxtMainServiceClient = new YsxtMainServiceClient();
                    var responseXML = ysxtMainServiceClient.sendRecv(sUser, sPwd, sJgbm, sVersion, sXxlx, sSign, responsexml);
                    AppLogger.Info("调用采购交易出参: " + responseXML);
                    var purchaseLogEntity = new PurchaseLogEntity
                    {
                        OrganizeId = orgId,
                        jydm = "YY012",
                        jymc = "退货单填报确认",
                        param = param,
                        XmlRequest = responsexml,
                        XmlResponse = responseXML,
                        zt = "1",
                    };
                    purchaseLogEntity.Create(true);
                    db.Insert(purchaseLogEntity);
                    db.Commit();
                    //错误提示
                    var ztcljg = ReadXmlContent(responseXML, "XMLDATA/HEAD/ZTCLJG");
                    if (ztcljg != "00000")
                    {
                        var cwxx = ReadXmlContent(responseXML, "XMLDATA/HEAD/CWXX");
                        throw new Exception(cwxx);
                    }
                    return null;
                }

            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        /// <summary>
        /// 发票查询并获取
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="yqbm"></param>
        /// <param name="fpmxbh"></param>
        /// <returns></returns>
        public List<OutputStructYY004> Purchase_YY004(string orgId, string yqbm, string fpmxbh)
        {
            try
            {
                //main
                var mainsqlpar = new List<SqlParameter>();
                var mainsql = @"exec usp_PurchaseMain_YY004 @orgId,@yqbm,@fpmxbh";
                mainsqlpar.Add(new SqlParameter("@orgId", orgId));
                mainsqlpar.Add(new SqlParameter("@yqbm", yqbm));
                mainsqlpar.Add(new SqlParameter("@fpmxbh", fpmxbh));
                PurchaseMainYY004 main = FirstOrDefault<PurchaseMainYY004>(mainsql.ToString(), mainsqlpar.ToArray());

                PurchaseHead head = GetHead();
                PurchaseYY004 xmlData = new PurchaseYY004();
                xmlData.MAIN = main;
                xmlData.HEAD = head;
                string responsexml = xmlData.XmlSerialize().Replace("\r\n", "").Replace(" ", "");
                responsexml = responsexml.Replace("<?xmlversion=\"1.0\"?>", "<?xml version=\"1.0\" encoding=\"utf-8\"?>");
                //string responsexml = xmlData.XmlSerialize().Replace("<?xml version=\"1.0\"?>", "<?xml version=\"1.0\" encoding=\"utf-8\"?>");

                using (var db = new EFDbTransaction(this._databaseFactory).BeginTrans())
                {
                    var sXxlx = "YY004";
                    var sSign = getMessageDigest(responsexml);
                    //调用阳光采购平台WebService接口
                    AppLogger.Info("============= " + sXxlx + " ============= ");
                    AppLogger.Info("调用采购交易规定入参: " + sUser + "," + sPwd + "," + sJgbm + "," + sVersion + "," + sXxlx + "," + sSign);
                    AppLogger.Info("调用采购交易请求url: " + responsexml);
                    YsxtMainServiceClient ysxtMainServiceClient = new YsxtMainServiceClient();
                    var responseXML = ysxtMainServiceClient.sendRecv(sUser, sPwd, sJgbm, sVersion, sXxlx, sSign, responsexml);
                    AppLogger.Info("调用采购交易出参: " + responseXML);

                    //XElement xmlElement = XElement.Parse(responseXML);
                    //string jsonString = JsonConvert.SerializeXNode(xmlElement);
                    //OutputYY004 entity = JsonConvert.DeserializeObject<OutputYY004>(jsonString);
                    //var jsonout = responseXML.XmlDeSerialize<OutputYY004>();


                    //测试
                    //var responseXML = "<?xml version=\"1.0\" encoding=\"UTF - 8\"?><XMLDATA><HEAD><JSSJ>20230814/160916/</JSSJ><ZTCLJG>00000</ZTCLJG><CWXX></CWXX><BZXX>备注信息</BZXX></HEAD><MAIN><JLS>5</JLS><SFWJ>1</SFWJ></MAIN><DETAIL><STRUCT><FPH>03100220010508178424</FPH><FPRQ>20230628</FPRQ><FPHSZJE>61.1</FPHSZJE><YQBM>SYSDZD01</YQBM><YYBM>77370296000</YYBM><PSDBM>77370296000</PSDBM><DLSGBZ>0</DLSGBZ><FPBZ/><SFWPSFP>1</SFWPSFP><WPSFPSM/><FPMXBH>20230628010082660484</FPMXBH><SPLX>1</SPLX><SFCH>0</SFCH><ZXSPBM>X01211740010012</ZXSPBM><SCPH>2301072</SCPH><SCRQ>20230129</SCRQ><SPSL>5</SPSL><GLMXBH>20230627000098852552</GLMXBH><XSDDH>506202306280335</XSDDH><SXH>4</SXH><YXRQ>20251231</YXRQ><WSDJ>10.814</WSDJ><HSDJ>12.22</HSDJ><SL>.13</SL><SE>7.03</SE><HSJE>61.1</HSJE><PFJ>10.8142</PFJ><LSJ>61.1</LSJ><PZWH>国药准字H32024827</PZWH></STRUCT><STRUCT><FPH>03100220010508178425</FPH><FPRQ>20230628</FPRQ><FPHSZJE>99.5</FPHSZJE><YQBM>SYSDZD01</YQBM><YYBM>77370296000</YYBM><PSDBM>77370296000</PSDBM><DLSGBZ>0</DLSGBZ><FPBZ/><SFWPSFP>1</SFWPSFP><WPSFPSM/><FPMXBH>20230628010082660485</FPMXBH><SPLX>1</SPLX><SFCH>0</SFCH><ZXSPBM>XN0000030094082</ZXSPBM><SCPH>KRU0315</SCPH><SCRQ>20220301</SCRQ><SPSL>10</SPSL><GLMXBH>20230627000098852560</GLMXBH><XSDDH>506202306280335</XSDDH><SXH>12</SXH><YXRQ>20240229</YXRQ><WSDJ>8.805</WSDJ><HSDJ>9.95</HSDJ><SL>.13</SL><SE>11.45</SE><HSJE>99.5</HSJE><PFJ>8.8053</PFJ><LSJ>9.95</LSJ><PZWH>国药准字J20150061</PZWH></STRUCT><STRUCT><FPH>03100220010508184713</FPH><FPRQ>20230710</FPRQ><FPHSZJE>-61.1</FPHSZJE><YQBM>SYSDZD01</YQBM><YYBM>77370296000</YYBM><PSDBM>77370296000</PSDBM><DLSGBZ>0</DLSGBZ><FPBZ/><SFWPSFP>1</SFWPSFP><WPSFPSM/><FPMXBH>20230711010083218323</FPMXBH><SPLX>1</SPLX><SFCH>1</SFCH><ZXSPBM>X01211740010012</ZXSPBM><SCPH>2301072</SCPH><SCRQ>20230129</SCRQ><SPSL>-5</SPSL><GLMXBH>20230711010002158605</GLMXBH><XSDDH>506202306290002</XSDDH><SXH/><YXRQ>20251231</YXRQ><WSDJ>10.814</WSDJ><HSDJ>12.22</HSDJ><SL>.13</SL><SE>-7.03</SE><HSJE>-61.1</HSJE><PFJ>10.8142</PFJ><LSJ>61.1</LSJ><PZWH>国药准字H32024827</PZWH></STRUCT><STRUCT><FPH>03100220010508184712</FPH><FPRQ>20230710</FPRQ><FPHSZJE>-99.5</FPHSZJE><YQBM>SYSDZD01</YQBM><YYBM>77370296000</YYBM><PSDBM>77370296000</PSDBM><DLSGBZ>0</DLSGBZ><FPBZ/><SFWPSFP>1</SFWPSFP><WPSFPSM/><FPMXBH>20230711010083218406</FPMXBH><SPLX>1</SPLX><SFCH>1</SFCH><ZXSPBM>XN0000030094082</ZXSPBM><SCPH>KRU0315</SCPH><SCRQ>20220301</SCRQ><SPSL>-10</SPSL><GLMXBH>20230711010002158604</GLMXBH><XSDDH>506202306290002</XSDDH><SXH/><YXRQ>20240229</YXRQ><WSDJ>8.805</WSDJ><HSDJ>9.95</HSDJ><SL>.13</SL><SE>-11.45</SE><HSJE>-99.5</HSJE><PFJ>8.8053</PFJ><LSJ>9.95</LSJ><PZWH>国药准字J20150061</PZWH></STRUCT><STRUCT><FPH>03100220010508778395</FPH><FPRQ>20230808</FPRQ><FPHSZJE>-521</FPHSZJE><YQBM>SYSDZD01</YQBM><YYBM>77370296000</YYBM><PSDBM>77370296000</PSDBM><DLSGBZ>0</DLSGBZ><FPBZ/><SFWPSFP>1</SFWPSFP><WPSFPSM/><FPMXBH>20230808010084421506</FPMXBH><SPLX>1</SPLX><SFCH>1</SFCH><ZXSPBM>XJ01XDT045A001010104642</ZXSPBM><SCPH>30230202</SCPH><SCRQ>20230209</SCRQ><SPSL>-50</SPSL><GLMXBH>20230808010002209423</GLMXBH><XSDDH>506202306283009</XSDDH><SXH/><YXRQ>20260131</YXRQ><WSDJ>9.2212</WSDJ><HSDJ>10.42</HSDJ><SL>.13</SL><SE>-59.94</SE><HSJE>-521</HSJE><PFJ>9.2212</PFJ><LSJ>10.42</LSJ><PZWH>国药准字H33020324</PZWH></STRUCT></DETAIL></XMLDATA>";
                    //var jsonoutTest = responseXMLTest.XmlDeSerialize<OutputYY004>();

                    List<OutputStructYY004> tmpList = new List<OutputStructYY004>();
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.LoadXml(responseXML);
                    XmlNodeList nodelist = xmlDoc.SelectNodes("XMLDATA/DETAIL/STRUCT");
                    foreach (XmlNode node in nodelist)
                    {
                        OutputStructYY004 entity = new OutputStructYY004();
                        entity.FPH = node["FPH"].InnerText;
                        entity.FPRQ = node["FPRQ"].InnerText;
                        entity.FPHSZJE = decimal.Parse(node["FPHSZJE"].InnerText);
                        entity.YQBM = node["YQBM"].InnerText;
                        entity.YYBM = node["YYBM"].InnerText;
                        entity.PSDBM = node["PSDBM"].InnerText;
                        entity.DLSGBZ = node["DLSGBZ"].InnerText;
                        entity.FPBZ = node["FPBZ"].InnerText;
                        entity.SFWPSFP = node["SFWPSFP"].InnerText;
                        entity.WPSFPSM = node["WPSFPSM"].InnerText;
                        entity.FPMXBH = node["FPMXBH"].InnerText;
                        entity.SPLX = node["SPLX"].InnerText;
                        entity.SFCH = node["SFCH"].InnerText;
                        entity.ZXSPBM = node["ZXSPBM"].InnerText;
                        entity.SCPH = node["SCPH"].InnerText;
                        entity.SCRQ = node["SCRQ"].InnerText;
                        entity.SPSL = decimal.Parse(node["SPSL"].InnerText);
                        entity.GLMXBH = node["GLMXBH"].InnerText;
                        entity.XSDDH = node["XSDDH"].InnerText;
                        var SXH = node["SXH"].InnerText == "" ? "0" : node["SXH"].InnerText;
                        entity.SXH = int.Parse(SXH);
                        entity.YXRQ = node["YXRQ"].InnerText;
                        entity.WSDJ = decimal.Parse(node["WSDJ"].InnerText);
                        entity.HSDJ = decimal.Parse(node["HSDJ"].InnerText);
                        entity.SL = decimal.Parse(node["SL"].InnerText);
                        entity.SE = decimal.Parse(node["SE"].InnerText);
                        entity.HSJE = decimal.Parse(node["HSJE"].InnerText);
                        entity.PFJ = decimal.Parse(node["PFJ"].InnerText);
                        entity.LSJ = decimal.Parse(node["LSJ"].InnerText);
                        entity.PZWH = node["PZWH"].InnerText;

                        tmpList.Add(entity);
                    }

                    var param = yqbm;
                    var purchaseLogEntity = new PurchaseLogEntity
                    {
                        OrganizeId = orgId,
                        jydm = "YY004",
                        jymc = "发票查询并获取",
                        param = param,
                        XmlRequest = responsexml,
                        XmlResponse = responseXML,
                        zt = "1",
                    };
                    purchaseLogEntity.Create(true);
                    db.Insert(purchaseLogEntity);
                    db.Commit();
                    //错误提示
                    var ztcljg = ReadXmlContent(responseXML, "XMLDATA/HEAD/ZTCLJG");
                    if (ztcljg != "00000")
                    {
                        var cwxx = ReadXmlContent(responseXML, "XMLDATA/HEAD/CWXX");
                        throw new Exception(cwxx);
                    }
                    return tmpList;
                }
            }
            catch (Exception e)
            {
                AppLogger.Info("调用采购交易失败: " + e.Message);
                return null;
            }
        }

        public string Purchase_YY019(PurchaseMainYY019 main, string orgId)
        {
            try
            {
                string HospitalCode = ConfigurationHelper.GetAppConfigValue("OrganizeCodeSd");
                string HospitalName = ConfigurationHelper.GetAppConfigValue("HospitalName");
                PurchaseHead head = GetHead();
                PurchaseYY019 xmlData = new PurchaseYY019();
                xmlData.MAIN = main;
                xmlData.HEAD = head;
                //string responsexml = xmlData.XmlSerialize().Replace("<?xml version=\"1.0\"?>", "<?xml version=\"1.0\" encoding=\"utf-8\"?>");

                string responsexml = xmlData.XmlSerialize().Replace("\r\n", "").Replace(" ", "");
                responsexml = responsexml.Replace("<?xmlversion=\"1.0\"?>", "<?xml version=\"1.0\" encoding=\"utf-8\"?>");

                var param = main.FPH;
                using (var db = new EFDbTransaction(this._databaseFactory).BeginTrans())
                {
                    //var responseXML = "<XMLDATA><HEAD><JSSJ>20130831/102341/</JSSJ><ZTCLJG>00000</ZTCLJG><CWXX/><BZXX/></HEAD></XMLDATA>";
                    var sXxlx = "YY019";
                    var sSign = getMessageDigest(responsexml);
                    //调用阳光采购平台WebService接口
                    AppLogger.Info("============= " + sXxlx + " ============= ");
                    AppLogger.Info("调用采购交易规定入参: " + sUser + "," + sPwd + "," + sJgbm + "," + sVersion + "," + sXxlx + "," + sSign);
                    AppLogger.Info("调用采购交易请求url: " + responsexml);
                    YsxtMainServiceClient ysxtMainServiceClient = new YsxtMainServiceClient();
                    var responseXML = ysxtMainServiceClient.sendRecv(sUser, sPwd, sJgbm, sVersion, sXxlx, sSign, responsexml);
                    AppLogger.Info("调用采购交易出参: " + responseXML);
                    var purchaseLogEntity = new PurchaseLogEntity
                    {
                        OrganizeId = orgId,
                        jydm = "YY019",
                        jymc = "发票验收",
                        param = param,
                        XmlRequest = responsexml,
                        XmlResponse = responseXML,
                        zt = "1",
                    };
                    purchaseLogEntity.Create(true);
                    db.Insert(purchaseLogEntity);
                    db.Commit();
                    //错误提示
                    var ztcljg = ReadXmlContent(responseXML, "XMLDATA/HEAD/ZTCLJG");
                    if (ztcljg != "00000")
                    {
                        var cwxx = ReadXmlContent(responseXML, "XMLDATA/HEAD/CWXX");
                        throw new Exception(cwxx);
                    }
                    return null;
                }
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }


        /// <summary>
        /// 获取配送单明细数据
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="yqbm"></param>
        /// <param name="fpmxbh"></param>
        /// <returns></returns>
        public List<OutputStructYY003> Purchase_YY003(string orgId, string yqbm, string psmxbh)
        {
            try
            {
                //main
                var mainsqlpar = new List<SqlParameter>();
                var mainsql = @"exec usp_PurchaseMain_YY003 @orgId,@yqbm,@psmxbh";
                mainsqlpar.Add(new SqlParameter("@orgId", orgId));
                mainsqlpar.Add(new SqlParameter("@yqbm", yqbm));
                mainsqlpar.Add(new SqlParameter("@psmxbh", psmxbh));
                PurchaseMainYY003 main = FirstOrDefault<PurchaseMainYY003>(mainsql.ToString(), mainsqlpar.ToArray());

                PurchaseHead head = GetHead();
                PurchaseYY003 xmlData = new PurchaseYY003();
                xmlData.MAIN = main;
                xmlData.HEAD = head;
                string responsexml = xmlData.XmlSerialize().Replace("\r\n", "").Replace(" ", "");
                responsexml = responsexml.Replace("<?xmlversion=\"1.0\"?>", "<?xml version=\"1.0\" encoding=\"utf-8\"?>");
                //string responsexml = xmlData.XmlSerialize().Replace("<?xml version=\"1.0\"?>", "<?xml version=\"1.0\" encoding=\"utf-8\"?>");

                using (var db = new EFDbTransaction(this._databaseFactory).BeginTrans())
                {
                    var sXxlx = "YY003";
                    var sSign = getMessageDigest(responsexml);
                    //调用阳光采购平台WebService接口
                    AppLogger.Info("============= " + sXxlx + " ============= ");
                    AppLogger.Info("调用采购交易规定入参: " + sUser + "," + sPwd + "," + sJgbm + "," + sVersion + "," + sXxlx + "," + sSign);
                    AppLogger.Info("调用采购交易请求url: " + responsexml);
                    YsxtMainServiceClient ysxtMainServiceClient = new YsxtMainServiceClient();
                    var responseXML = ysxtMainServiceClient.sendRecv(sUser, sPwd, sJgbm, sVersion, sXxlx, sSign, responsexml);
                    AppLogger.Info("调用采购交易出参: " + responseXML);
                    
                    List<OutputStructYY003> tmpList = new List<OutputStructYY003>();
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.LoadXml(responseXML);
                    XmlNodeList nodelist = xmlDoc.SelectNodes("XMLDATA/DETAIL/STRUCT");
                    foreach (XmlNode node in nodelist)
                    {
                        OutputStructYY003 entity = new OutputStructYY003();
                        if (node.SelectSingleNode("PSDH") != null) { entity.PSDH = node["PSDH"].InnerText; }
                        if (node.SelectSingleNode("YQBM") != null) { entity.YQBM = node["YQBM"].InnerText; }
                        if (node.SelectSingleNode("PSDBM") != null) { entity.PSDBM = node["PSDBM"].InnerText; }
                        if (node.SelectSingleNode("CJRQ") != null) { entity.CJRQ = node["CJRQ"].InnerText; }
                        if (node.SelectSingleNode("PSMXBH") != null) { entity.PSMXBH = node["PSMXBH"].InnerText; }
                        if (node.SelectSingleNode("PSDTM") != null) { entity.PSDTM = node["PSDTM"].InnerText; }
                        if (node.SelectSingleNode("ZXLX") != null) { entity.ZXLX = node["ZXLX"].InnerText; }
                        if (node.SelectSingleNode("CGLX") != null) { entity.CGLX = node["CGLX"].InnerText; }
                        if (node.SelectSingleNode("SPLX") != null) { entity.SPLX = node["SPLX"].InnerText; }
                        if (node.SelectSingleNode("YPLX") != null) { entity.YPLX = node["YPLX"].InnerText; }
                        if (node.SelectSingleNode("ZXSPBM") != null) { entity.ZXSPBM = node["ZXSPBM"].InnerText; }
                        if (node.SelectSingleNode("CPM") != null) { entity.CPM = node["CPM"].InnerText; }
                        if (node.SelectSingleNode("YPJX") != null) { entity.YPJX = node["YPJX"].InnerText; }
                        if (node.SelectSingleNode("GG") != null) { entity.GG = node["GG"].InnerText; }
                        if (node.SelectSingleNode("BZDWXZ") != null) { entity.BZDWXZ = node["BZDWXZ"].InnerText; }
                        if (node.SelectSingleNode("BZDWMC") != null) { entity.BZDWMC = node["BZDWMC"].InnerText; }
                        if (node.SelectSingleNode("YYDWMC") != null) { entity.YYDWMC = node["YYDWMC"].InnerText; }
                        if (node.SelectSingleNode("BZNHSL") != null) { entity.BZNHSL=decimal.Parse(node["BZNHSL"].InnerText); }
                        if (node.SelectSingleNode("SCQYMC") != null) { entity.SCQYMC = node["SCQYMC"].InnerText; }
                        if (node.SelectSingleNode("SCPH") != null) { entity.SCPH = node["SCPH"].InnerText; }
                        if (node.SelectSingleNode("SCRQ") != null) { entity.SCRQ = node["SCRQ"].InnerText; }
                        if (node.SelectSingleNode("YXRQ") != null) { entity.YXRQ = node["YXRQ"].InnerText; }
                        if (node.SelectSingleNode("JHDH") != null) { entity.JHDH = node["JHDH"].InnerText; }
                        if (node.SelectSingleNode("XSDDH") != null) { entity.XSDDH = node["XSDDH"].InnerText; }
                        if (node.SelectSingleNode("DDMXBH") != null) { entity.DDMXBH = node["DDMXBH"].InnerText; }
                        if (node.SelectSingleNode("SXH") != null) { entity.SXH=int.Parse(node["SXH"].InnerText);}
                        if (node.SelectSingleNode("PSL") != null) { entity.PSL =decimal.Parse(node["PSL"].InnerText); }
                        tmpList.Add(entity);
                    }
                    var param = yqbm;
                    var purchaseLogEntity = new PurchaseLogEntity
                    {
                        OrganizeId = orgId,
                        jydm = "YY003",
                        jymc = "获取配送单明细数据",
                        param = param,
                        XmlRequest = responsexml,
                        XmlResponse = responseXML,
                        zt = "1",
                    };
                    purchaseLogEntity.Create(true);
                    db.Insert(purchaseLogEntity);
                    db.Commit();
                    //错误提示
                    var ztcljg = ReadXmlContent(responseXML, "XMLDATA/HEAD/ZTCLJG");
                    if (ztcljg != "00000")
                    {
                        var cwxx = ReadXmlContent(responseXML, "XMLDATA/HEAD/CWXX");
                        throw new Exception(cwxx);
                    }
                    return tmpList;
                }
            }
            catch (Exception e)
            {
                AppLogger.Info("调用采购交易失败: " + e.Message);
                return null;
            }
        }

        /// <summary>
        /// 配送明细验收
        /// </summary>
        /// <param name="main"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public string Purchase_YY018(PurchaseMainYY018 main, string orgId)
        {
            try
            {
                string HospitalCode = ConfigurationHelper.GetAppConfigValue("OrganizeCodeSd");
                string HospitalName = ConfigurationHelper.GetAppConfigValue("HospitalName");
                PurchaseHead head = GetHead();
                PurchaseYY018 xmlData = new PurchaseYY018();
                xmlData.MAIN = main;
                xmlData.HEAD = head;
                //string responsexml = xmlData.XmlSerialize().Replace("<?xml version=\"1.0\"?>", "<?xml version=\"1.0\" encoding=\"utf-8\"?>");

                string responsexml = xmlData.XmlSerialize().Replace("\r\n", "").Replace(" ", "");
                responsexml = responsexml.Replace("<?xmlversion=\"1.0\"?>", "<?xml version=\"1.0\" encoding=\"utf-8\"?>");

                var param = main.PSMXBH;
                using (var db = new EFDbTransaction(this._databaseFactory).BeginTrans())
                {
                    //var responseXML = "<XMLDATA><HEAD><JSSJ>20130831/102341/</JSSJ><ZTCLJG>00000</ZTCLJG><CWXX/><BZXX/></HEAD></XMLDATA>";
                    var sXxlx = "YY018";
                    var sSign = getMessageDigest(responsexml);
                    //调用阳光采购平台WebService接口
                    AppLogger.Info("============= " + sXxlx + " ============= ");
                    AppLogger.Info("调用采购交易规定入参: " + sUser + "," + sPwd + "," + sJgbm + "," + sVersion + "," + sXxlx + "," + sSign);
                    AppLogger.Info("调用采购交易请求url: " + responsexml);
                    YsxtMainServiceClient ysxtMainServiceClient = new YsxtMainServiceClient();
                    var responseXML = ysxtMainServiceClient.sendRecv(sUser, sPwd, sJgbm, sVersion, sXxlx, sSign, responsexml);
                    AppLogger.Info("调用采购交易出参: " + responseXML);
                    var purchaseLogEntity = new PurchaseLogEntity
                    {
                        OrganizeId = orgId,
                        jydm = "YY018",
                        jymc = "配送明细验收",
                        param = param,
                        XmlRequest = responsexml,
                        XmlResponse = responseXML,
                        zt = "1",
                    };
                    purchaseLogEntity.Create(true);
                    db.Insert(purchaseLogEntity);
                    db.Commit();
                    //错误提示
                    var ztcljg = ReadXmlContent(responseXML, "XMLDATA/HEAD/ZTCLJG");
                    if (ztcljg != "00000")
                    {
                        var cwxx = ReadXmlContent(responseXML, "XMLDATA/HEAD/CWXX");
                        throw new Exception(cwxx);
                    }
                    return null;
                }
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }


        public string Purchase_YY001(string orgId, string Id, string czlx)
        {
            try
            {
                //main
                var mainsqlpar = new List<SqlParameter>();
                var mainsql = @"exec usp_PurchaseMain_YY001 @orgId,@Id,@czlx";
                mainsqlpar.Add(new SqlParameter("@orgId", orgId));
                mainsqlpar.Add(new SqlParameter("@Id", Id));
                mainsqlpar.Add(new SqlParameter("@czlx", czlx));
                PurchaseMainYY001 main = FirstOrDefault<PurchaseMainYY001>(mainsql.ToString(), mainsqlpar.ToArray());

                PurchaseHead head = GetHead();
                PurchaseYY001 xmlData = new PurchaseYY001();
                xmlData.MAIN = main;
                xmlData.HEAD = head;
                string responsexml = xmlData.XmlSerialize().Replace("<?xml version=\"1.0\"?>", "<?xml version=\"1.0\" encoding=\"utf-8\"?>");

                var sXxlx = "YY001";
                var sSign = getMessageDigest(responsexml);
                //调用阳光采购平台WebService接口
                AppLogger.Info("============= " + sXxlx + " ============= ");
                AppLogger.Info("调用采购交易规定入参: " + sUser + "," + sPwd + "," + sJgbm + "," + sVersion + "," + sXxlx + "," + sSign);
                AppLogger.Info("调用采购交易请求url: " + responsexml);
                YsxtMainServiceClient ysxtMainServiceClient = new YsxtMainServiceClient();
                var responseXML = ysxtMainServiceClient.sendRecv(sUser, sPwd, sJgbm, sVersion, sXxlx, sSign, responsexml);
                AppLogger.Info("调用采购交易出参: " + responseXML);

                var param = Id;
                using (var db = new EFDbTransaction(this._databaseFactory).BeginTrans())
                {
                    var purchaseLogEntity = new PurchaseLogEntity
                    {
                        OrganizeId = orgId,
                        jydm = "YY001",
                        jymc = "医院配送点基础信息传报",
                        param = param,
                        XmlRequest = responsexml,
                        XmlResponse = responseXML,
                        zt = "1",
                    };
                    purchaseLogEntity.Create(true);
                    db.Insert(purchaseLogEntity);
                    db.Commit();
                }
                //错误提示
                var ztcljg = ReadXmlContent(responseXML, "XMLDATA/HEAD/ZTCLJG");
                if (ztcljg != "00000")
                {
                    var cwxx = ReadXmlContent(responseXML, "XMLDATA/HEAD/CWXX");
                    throw new Exception(cwxx);
                }

                return "Success";
            }
            catch (Exception e)
            {
                AppLogger.Info("调用采购交易失败: " + e.Message);
                return e.Message;
            }
        }

        public static T DESerializerStringToEntity<T>(string strXML) where T : class
        {
            try
            {
                using (StringReader sr = new StringReader(strXML))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(T));
                    return serializer.Deserialize(sr) as T;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion


    }
}
