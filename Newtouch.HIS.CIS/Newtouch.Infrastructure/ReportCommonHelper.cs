using Newtouch.Common;
using Newtouch.Tools;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml;

namespace NewtouchCIS.Infrastructure
{
    public class ReportCommonHelper
    {
        public static DataSet GetReportDS(string rptlj, string json)
        {
            DataSet dsPrint = new DataSet();
            string filename = rptlj;
            //格式化数据参数
            XmlDocument paraXml = Newtonsoft.Json.JsonConvert.DeserializeXmlNode(json, "parameter");
            //AppLogger.Info("GetReportDS4");

            //读取报告格式
            string rptFile = fileToString(filename);
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(rptFile);
            XmlNodeList rptTableDataSourceList = doc.SelectNodes(@"/Report/Dictionary/TableDataSource");
            string rptDataband = doc.SelectSingleNode(@"/Report/ReportPage/GroupHeaderBand/DataBand").Attributes
                ["DataSource"].InnerText;/// Report / ReportPage / DataBand
            if (rptTableDataSourceList.Count == 0)
            {
                return dsPrint;
            }
            //if (rptTableDataSourceList.Count > 0)
            //{
            int idx = 0;
            foreach (XmlNode item in rptTableDataSourceList) //报表数据源循环加载
            {
                //数据源名称
                string dsname = item.Attributes["Name"].InnerText;
                //相应数据源入参数据
                XmlNodeList paraList = paraXml.SelectNodes(@"/parameter/" + dsname);
                //获取数据源参数字段
                DataTable dtRptPara = ConvertXMLToDataSet(dsname, "<row>" + item.InnerXml + "</row>").Tables[0];
                if (dtRptPara != null && dtRptPara.Rows.Count > 0)
                {
                    DataTable dt = new DataTable(dsname);
                    foreach (DataRow rptPara in dtRptPara.Rows)  //数据源参数循环
                    {
                        string colname = rptPara["Name"].ToString();
                        string typestring = rptPara["DataType"].ToString();
                        DataColumn cl = new DataColumn(colname,Type.GetType(typestring));
                        if (rptPara.Table.Columns.Contains("Enabled"))
                        {
                            if (rptPara["Enabled"].ToString() != "false")
                            {
                                dt.Columns.Add(cl);
                            }
                        }
                        else
                        {
                            dt.Columns.Add(cl);
                        }
                    }

                    int j = 0;
                    foreach (XmlNode para in paraList)  //数据源参数对应值
                    {
                        DataRow dr = dt.NewRow();
                        int k = 0;
                        foreach (DataColumn rptPara in dr.Table.Columns)  //数据源参数循环
                        {
                            string col = rptPara.ColumnName;
                            string val = para.SelectSingleNode(col) == null ? "" : para.SelectSingleNode(col).InnerText;
                            dr[col] = val;
                            k++;
                        }
                        dt.Rows.Add(dr);
                        j++;
                    }
                    dsPrint.Tables.Add(dt);
                }
                idx++;
            }
            //}
            return dsPrint;
        }

        /// <summary>
        /// 获取文件中的数据
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static string fileToString(String filePath)
        {
            string strData = "";
            try
            {
                string line;
                // 创建一个 StreamReader 的实例来读取文件 ,using 语句也能关闭 StreamReader
                using (System.IO.StreamReader sr = new System.IO.StreamReader(filePath))
                {
                    // 从文件读取并显示行，直到文件的末尾 
                    while ((line = sr.ReadLine()) != null)
                    {
                        //Console.WriteLine(line);
                        strData += line;
                    }
                }
            }
            catch (Exception e)
            {
                // 向用户显示出错消息
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
            return strData;
        }

        // Xml结构的文件读到DataTable中
        public static DataSet ConvertXMLToDataSet(string tbname, string xmlData)
        {
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.LoadXml(xmlData);
            StringReader stream = null;
            XmlTextReader reader = null;
            try
            {
                DataSet xmlDS = new DataSet();
                stream = new StringReader(xmldoc.InnerXml);
                reader = new XmlTextReader(stream);
                xmlDS.ReadXml(reader);
                reader.Close();
                return xmlDS;
            }
            catch (System.Exception ex)
            {
                reader.Close();
                throw ex;
            }
        }

    }
}