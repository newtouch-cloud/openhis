using NeiMengGuYiBaoApp.Models.Input;
using NeiMengGuYiBaoApp.Models.Input.NationECCodeDll;
using NeiMengGuYiBaoApp.Models.Input.YiBao;
using NeiMengGuYiBaoApp.Models.Post;
using NeiMengGuYiBaoApp.Models.Post.NationECCodeDll;
using NeiMengGuYiBaoApp.Models.Output.YiBao;
using System;
using System.Data.SqlClient;
using System.IO;

namespace NeiMengGuYiBaoApp.Service
{
	public class AppLogger
	{
        public static void Info(string message)
        {
            try
            {
                var root = "C:\\log_yibao";
                var date = DateTime.Now.ToString("yyyyMMddHHmm");
                var dirPath = string.Format("{0}\\{1}", root, date.Substring(0, 8));
                if (!Directory.Exists(dirPath))
                {
                    Directory.CreateDirectory(dirPath);
                }
                var filePath = string.Format("{0}\\{1}.txt", dirPath, date.Substring(8, 2));
                File.AppendAllText(filePath, string.Format("\r\n\r\n{0}.Info.{1}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), message));
            }
            catch { }
        }

        public static bool SaveJyDbLog(PostBase post, DateTime getdate, InputPost<InputBase> input, string inputStr, OutputReturn output, int isSucess)
        {
            bool fSuccess = true;

            string inHead = string.Empty;
            string inContent = string.Empty;
            string outHead = string.Empty;
            string outContent = string.Empty;
            string errorMsg = string.Empty;

            if (input != null)
            {
                inHead = "infno:" + input.infno 
                        + ",msgid" + input.msgid 
                        + ",mdtrtarea_admvs:" + input.mdtrtarea_admvs
                        + ",insuplc_admdvs:" + input.insuplc_admdvs
                        + ",recer_sys_code:" + input.recer_sys_code
                        + ",dev_no:" + input.dev_no
                        + ",dev_safe_info:" + input.dev_safe_info
                        + ",signtype:" + input.signtype
                        + ",infver:" + input.infver
                        + ",opter_type:" + input.opter_type
                        + ",opter:" + input.opter
                        + ",opter_name:" + input.opter_name
                        + ",inf_time:" + input.inf_time
                        + ",fixmedins_code:" + input.fixmedins_code
                        + ",fixmedins_name:" + input.fixmedins_name
                        + ",sign_no:" + input.sign_no;
            }
            if (inputStr != null)
            {
                if (inputStr.Length > 8000)
                {
                    inContent = inputStr.Substring(0, 8000);
                }
                else
                {
                    inContent = inputStr;
                }
            }
            if (output != null)
            {
                outHead = "infcode:" + output.infcode
                        + ",inf_refmsgid:" + output.inf_refmsgid
                        + ",refmsg_time:" + output.refmsg_time
                        + ",respond_time:" + output.inf_refmsgid;

                if (output.output != null)
                {
                    string outdataStr = Convert.ToString(output.output);
                    if (outdataStr.Length > 8000)
                    {
                        outContent = outdataStr.Substring(0, 8000);
                    }
                    else
                    {
                        outContent = outdataStr;
                    }
                }
                
                if (output.err_msg != null)
                {
                    if (output.err_msg.Length > 1000)
                    {
                        errorMsg = output.err_msg.Substring(0, 1000);
                    }
                    else
                    {
                        errorMsg = output.err_msg;
                    }
                }
            }

            try
            {
                ClassSqlHelper.platFormServer.RunProc_Direct_Wqserver("ybjk_save_log", new SqlParameter[]
                {
                    new SqlParameter("@inDate", getdate),
                    new SqlParameter("@tradiNumber", post.tradiNumber),
                    new SqlParameter("@userIp", YiBaoInitHelper.localIp),
                    new SqlParameter("@operatorId", post.operatorId),
                    new SqlParameter("@hisId", post.hisId),
                    new SqlParameter("@beginDate", getdate),
                    new SqlParameter("@inHead", inHead),
                    new SqlParameter("@inContent", inContent),
                    new SqlParameter("@OutHead", outHead),
                    new SqlParameter("@outContent", outContent),
                    new SqlParameter("@errorMsg", errorMsg),
                    new SqlParameter("@stateId", isSucess.ToString())
                });
            }
            catch(Exception ex)
            {
                fSuccess = false;
                Info("执行交易日志保存存储过程出现异常：" + ex.Message);
            }

            return fSuccess;
        }

        #region 内蒙古电子凭证医保日志
        public static bool SaveNMGJyDbLog(PostBase post, DateTime startDate, string inputStr, OutputBasePost output, string isSucess)
        {
            bool fSuccess = true;
            DateTime endDate = Convert.ToDateTime(ClassSqlHelper.GetServerTime().ToString("yyyy-MM-dd HH:mm:ss"));
            string inHead = string.Empty;
            string inContent = string.Empty;
            string outHead = string.Empty;
            string outContent = string.Empty;
            string errorMsg = string.Empty;

            inHead = "jysj:" + startDate  + ",transType" + post.tradiNumber;
           
            if (inputStr != null)
            {
                //保存医保原始日志内容
                inContent = inputStr;
            }
            if (output != null)
            {
                outHead = "jysj:" + startDate
                        + ",code" + output.code
                        + ",message:" + output.message;
                //保存医保原始日志内容
                outContent = Newtonsoft.Json.JsonConvert.SerializeObject(output);

                if (output.code != 0)
                {
                    errorMsg = output.message;
                }
            }

            try
            {
                ClassSqlHelper.platFormServer.RunProc_Direct_Wqserver("ybjk_save_log", new SqlParameter[]
                {
                    new SqlParameter("@inDate", startDate),
                    new SqlParameter("@tradiNumber", post.tradiNumber),
                    new SqlParameter("@userIp", YiBaoInitHelper.localIp),
                    new SqlParameter("@operatorId", post.operatorId),
                    new SqlParameter("@hisId", post.hisId),
                    new SqlParameter("@beginDate", endDate),
                    new SqlParameter("@inHead", inHead),
                    new SqlParameter("@inContent", inContent),
                    new SqlParameter("@OutHead", outHead),
                    new SqlParameter("@outContent", outContent),
                    new SqlParameter("@errorMsg", errorMsg),
                    new SqlParameter("@stateId", isSucess)
                });
            }
            catch (Exception ex)
            {
                fSuccess = false;
                Info("执行交易日志保存存储过程出现异常：" + ex.Message);
            }

            return fSuccess;
        }

        #endregion
    }
}
