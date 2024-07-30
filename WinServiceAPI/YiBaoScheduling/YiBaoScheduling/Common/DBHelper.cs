using Dapper;
using SqlHelper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using YiBaoScheduling.Model;

namespace YiBaoScheduling.Common
{
    public static class DBHelper
    {
        static string orgId = ConfigurationManager.AppSettings["orgId"];
        static string operatorId = ConfigurationManager.AppSettings["operatorId"];
        static string operatorName = ConfigurationManager.AppSettings["operatorName"];
        static string uploadCount = ConfigurationManager.AppSettings["uploadCount"];
        static int SqlTimeOutNumber = Convert.ToInt32(ConfigurationManager.AppSettings["SqlTimeOut"]);

        static string WSJGDM = ConfigurationManager.AppSettings["WSJGDM"];
        static string YLJGDM = ConfigurationManager.AppSettings["YLJGDM"];

        #region 数据库连接
        /// <summary>
        /// 获取默认的连接字符串
        /// </summary>
        static IDbConnection Conn
        {
            get
            {
                return new SqlConnection(DESEncrypt.Decrypt(ConfigurationManager.ConnectionStrings["Connection"].ToString()));
            }
        }
        static IDbConnection PD_UPLOAD
        {
            get
            {
                return new SqlConnection(DESEncrypt.Decrypt(ConfigurationManager.ConnectionStrings["PD_UPLOAD"].ToString()));
            }
        }
        static string LogConn
        {
            get
            {
                return DESEncrypt.Decrypt(ConfigurationManager.ConnectionStrings["HisLog"].ToString());
            }
        }
        #endregion

        #region http请求
        public static string HttpPost(string url, string body)
        {
            Encoding encoding = Encoding.UTF8;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            request.Accept = "text/html, application/xhtml+xml, */*";
            request.ContentType = "application/json";

            byte[] buffer = encoding.GetBytes(body);
            request.ContentLength = buffer.Length;
            request.GetRequestStream().Write(buffer, 0, buffer.Length);
            HttpWebResponse response;
            try
            {
                response = (HttpWebResponse)request.GetResponse();
            }
            catch (WebException ex)
            {
                response = (HttpWebResponse)ex.Response;
            }
            using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
            {
                return reader.ReadToEnd();
            }
        }
        #endregion

        #region 医保费用自动上传
        public static List<UploadData> Query()
        {
           
            //cblb:3 离休使用老医保
            const string sql = @" SELECT  
        a.zyh hisId, b.mdtrt_id,
        case when rytj is not null  then rytj else '21' end med_type, e.cbdbm insuplc_admdvs, e.grbh psn_no, xzlx insutype,
        @operatorId operatorId,@operatorName operatorName,jzlx mdtrt_cert_type, kh mdtrt_cert_no,isnull(bzbm, '') dise_codg,isnull(bzmc, '') dise_name
		,a.OrganizeId orgId, @uploadCount uploadCount,convert(varchar(19), GETDATE(), 120) jssj
  FROM    NewtouchHIS_Sett..zy_brjbxx a
        --LEFT JOIN [cqyb_OutPut02] b on a.zyh = b.zymzh and a.OrganizeId = b.OrganizeId and b.zt = '1'
        LEFT JOIN drjk_rybl_input b on a.zyh=b.zyh and b.zt=1
        LEFT JOIN NewtouchHIS_Sett..xt_brjbxx e ON e.patid = a.patid
                                                            AND e.OrganizeId = a.OrganizeId
                                                            AND e.zt = '1'
WHERE a.brxz != 0 and a.OrganizeId = @orgId
        AND a.zt = '1' and e.cblb != 3 and a.zybz not in ('3', '9') ";
            return Conn.Query<UploadData>(sql, new { orgId= orgId, operatorId= operatorId , operatorName = operatorName, uploadCount= uploadCount }).ToList();
        }
        #endregion

        #region 更新入院信息
        public static List<BedDifference> QueryCWCY()
        {
            //判断入区的床位和病人基本信息表的是否是一致的
            const string sql = @"
select 
zyxx.zyh hisId,
@operatorId operatorId,
@operatorName operatorName,
brxx.cbdbm insuplc_admdvs,
rybl.med_type,
'' hisIdNum,
rybl.mdtrt_id,
rybl.psn_no,
rybl.mdtrt_cert_type,
rybl.mdtrt_cert_no,
@orgId orgId,
rybl.dise_codg,
rybl.dise_name,
cwsyjl.BedNo adm_bed
from drjk_rybl_input rybl
inner join xt_brjbxx brxx on brxx.grbh=rybl.psn_no
inner join zy_brjbxx zyxx on brxx.patid=zyxx.patid
inner join [Newtouch_CIS].[dbo].[zy_cwsyjlk] cwsyjl on zyxx.zyh=cwsyjl.zyh
where rybl.zt='1'  and brxx.zt='1' and zyxx.zt='1' and cwsyjl.zt='1'
and brxx.OrganizeId=@orgId
and zyxx.OrganizeId=@orgId
and zyxx.zybz='1'
and (zyxx.cw!=cwsyjl.BedNo or rybl.adm_bed='')
and rybl.adm_bed!=cwsyjl.BedNo";
            return Conn.Query<BedDifference>(sql, new { orgId = orgId, operatorId = operatorId, operatorName = operatorName }).ToList();
        }

        #endregion

        #region 国家医保数据采集
        public static List<Inventory3501> QueryInventory3501()
        {
            //判断入区的床位和病人基本信息表的是否是一致的
            const string sql = @"select @operatorId operatorId,
@operatorName operatorName,
   pdId pdId
   from [NewtouchHIS_PDS].dbo.xt_yp_pdxx (NOLOCK) 
   where CreateTime>=@kssj and CreateTime<=@jssj
   order by CreateTime desc";
            return Conn.Query<Inventory3501>(sql, new { orgId = orgId, operatorId = operatorId, operatorName = operatorName,kssj=(DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd")+" 00:00:00"),jssj= (DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd") + " 23:59:59") }).ToList();
        }
        public static List<Inventory3502> QueryPurcinfo3503()
        {
            //判断入区的床位和病人基本信息表的是否是一致的
            const string sql = @"select @operatorId operatorId,
@operatorName operatorName,
   crkId crkId
   from [NewtouchHIS_PDS].[dbo].[xt_yp_crkdj] (nolock)
   where CreateTime>=@kssj and CreateTime<=@jssj and djlx='1'
   and shzt='1'
   order by CreateTime desc";
            return Conn.Query<Inventory3502>(sql, new { orgId = orgId, operatorId = operatorId, operatorName = operatorName, kssj = (DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd") + " 00:00:00"), jssj = (DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd") + " 23:59:59") }).ToList();
        }
        public static List<Inventory3502> QueryPurcinfo3504()
        {
            //判断入区的床位和病人基本信息表的是否是一致的
            const string sql = @"select @operatorId operatorId,
@operatorName operatorName,
   crkId crkId
   from [NewtouchHIS_PDS].[dbo].[xt_yp_crkdj] (nolock)
   where CreateTime>=@kssj and CreateTime<=@jssj and djlx='5'
   and shzt='1'
   order by CreateTime desc";
            return Conn.Query<Inventory3502>(sql, new { orgId = orgId, operatorId = operatorId, operatorName = operatorName, kssj = (DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd") + " 00:00:00"), jssj = (DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd") + " 23:59:59") }).ToList();
        }
        public static List<Inventory3502> QueryInventory3502()
        {
            //判断入区的床位和病人基本信息表的是否是一致的
            const string sql = @"select @operatorId operatorId,
@operatorName operatorName,
   crkId crkId
   from [NewtouchHIS_PDS].[dbo].[xt_yp_crkdj] (nolock)
   where CreateTime>=@kssj and CreateTime<=@jssj
   and shzt='1'
   order by CreateTime desc";
            return Conn.Query<Inventory3502>(sql, new { orgId = orgId, operatorId = operatorId, operatorName = operatorName, kssj = (DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd") + " 00:00:00"), jssj = (DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd") + " 23:59:59") }).ToList();
        }
        public static List<PreSett> PreSettQuery()
        {

            //cblb:3 离休使用老医保
            const string sql = @" select 
hisId,mdtrt_id,med_type,insuplc_admdvs,psn_no,insutype,operatorId,operatorName,mdtrt_cert_type,mdtrt_cert_no,dise_codg,dise_name,
orgId,uploadCount,jssj,sum(det_item_fee_sumamt) medfee_sumamt,dscgTime,'01' psn_setlway ,'0' mid_setl_flag,'1' acct_used_flag from 
(
SELECT  
  a.zyh hisId, b.mdtrt_id,
        case when rytj is not null  then rytj else '21' end med_type, e.cbdbm insuplc_admdvs, e.grbh psn_no, xzlx insutype,
        @operatorId operatorId,@operatorName operatorName,jzlx mdtrt_cert_type, kh mdtrt_cert_no,isnull(bzbm, '') dise_codg,isnull(bzmc, '') dise_name
		,a.OrganizeId orgId, @uploadCount uploadCount,convert(varchar(19), GETDATE(), 120) jssj,det_item_fee_sumamt,CONVERT(varchar(10),isnull(a.cyrq,GETDATE()),120) dscgTime
  FROM  NewtouchHIS_Sett..zy_brjbxx a
        LEFT JOIN drjk_rybl_input b on a.zyh=b.zyh and b.zt=1
        --LEFT JOIN [cqyb_OutPut02] b on a.zyh = b.zymzh and a.OrganizeId = b.OrganizeId and b.zt = '1'
        LEFT JOIN NewtouchHIS_Sett..xt_brjbxx e ON e.patid = a.patid
                                                            AND e.OrganizeId = a.OrganizeId AND e.zt = '1'
		LEFT JOIN drjk_zyfymxsc_input mxsc on mxsc.zyh=a.zyh
WHERE a.brxz != 0 and a.OrganizeId = @orgId
        AND a.zt = '1' and e.cblb != 3 and a.zybz not in ('3', '9')
) tab 
group by hisId,mdtrt_id,med_type,insuplc_admdvs,psn_no,insutype,operatorId,operatorName,mdtrt_cert_type,mdtrt_cert_no,dise_codg,dise_name,
orgId,uploadCount,jssj,dscgTime ";
            return Conn.Query<PreSett>(sql, new { orgId = orgId, operatorId = operatorId, operatorName = operatorName, uploadCount = uploadCount }).ToList();
        }
        #endregion

        #region shanghai卫健数据上传
        public static List<TB_YL_MZ_Medical_Record> QueryTB_YL_MZ_Medical_Record()
        {
            const string sql = @"exec TB_YL_MZ_Medical_Record @orgId ,@YLJGDM, @wsjgdm";
            return Conn.Query<TB_YL_MZ_Medical_Record>(sql,new { orgId= orgId , YLJGDM = YLJGDM , WSJGDM = WSJGDM }).ToList();
        }
        public static List<TB_CIS_Prescription_Detail> QueryTB_CIS_Prescription_Detail()
        {
            const string sql = @"exec TB_CIS_Prescription_Detail  @orgId ,@YLJGDM, @wsjgdm";
            return Conn.Query<TB_CIS_Prescription_Detail>(sql, new { orgId = orgId, YLJGDM = YLJGDM, WSJGDM = WSJGDM }).ToList();
        }
        public static List<TB_HIS_MZ_Fee_Detail> QueryTB_HIS_MZ_Fee_Detail()
        {
            const string sql = @"exec TB_HIS_MZ_Fee_Detail @orgId ,@YLJGDM, @wsjgdm";
            return Conn.Query<TB_HIS_MZ_Fee_Detail>(sql, new { orgId = orgId, YLJGDM = YLJGDM, WSJGDM = WSJGDM }).ToList();
        }
        public static List<TB_RIS_Report> QueryTB_RIS_Report()
        {
            const string sql = @"exec TB_RIS_Report @orgId ,@YLJGDM, @wsjgdm";
            return Conn.Query<TB_RIS_Report>(sql, new { orgId = orgId, YLJGDM = YLJGDM, WSJGDM = WSJGDM }).ToList();
        }
        /// <summary>
        /// 住院就诊记录
        /// </summary>
        /// <returns></returns>
        public static List<TB_YL_ZY_Medical_Record> QueryTB_YL_ZY_Medical_Record()
        {
            const string sql = @"exec TB_YL_ZY_Medical_Record @orgId ,@YLJGDM, @wsjgdm";
            return Conn.Query<TB_YL_ZY_Medical_Record>(sql, new { orgId = orgId, YLJGDM = YLJGDM, WSJGDM = WSJGDM }).ToList();
        }
        /// <summary>
        /// 住院医嘱明细记录
        /// </summary>
        /// <returns></returns>
        public static List<TB_CIS_DrAdvice_Detail> QueryTB_CIS_DrAdvice_Detail()
        {
            const string sql = @"exec TB_CIS_DrAdvice_Detail @orgId ,@YLJGDM, @wsjgdm";
            return Conn.Query<TB_CIS_DrAdvice_Detail>(sql, new { orgId = orgId, YLJGDM = YLJGDM, WSJGDM = WSJGDM }).ToList();
        }
        /// <summary>
        /// 住院收费明细
        /// </summary>
        /// <returns></returns>
        public static List<TB_HIS_ZY_Fee_Detail> QueryTB_HIS_ZY_Fee_Detail()
        {
            const string sql = @"exec TB_HIS_ZY_Fee_Detail @orgId ,@YLJGDM, @wsjgdm";
            return Conn.Query<TB_HIS_ZY_Fee_Detail>(sql, new { orgId = orgId, YLJGDM = YLJGDM, WSJGDM = WSJGDM }).ToList();
        }
        /// <summary>
        /// 手术明细表
        /// </summary>
        /// <returns></returns>
        public static List<TB_Operation_Detail> QueryTB_Operation_Detail()
        {
            const string sql = @"exec TB_Zy_Operation_Detail @orgId ,@YLJGDM, @wsjgdm";
            return Conn.Query<TB_Operation_Detail>(sql, new { orgId = orgId, YLJGDM = YLJGDM, WSJGDM = WSJGDM }).ToList();
        }
        /// <summary>
        /// 医院的科室字典表
        /// </summary>
        /// <returns></returns>
        public static List<TB_DIC_Department> QueryTB_DIC_Department()
        {
            const string sql = @"exec TB_DIC_Department @orgId ,@YLJGDM, @wsjgdm";
            return Conn.Query<TB_DIC_Department>(sql, new { orgId = orgId, YLJGDM = YLJGDM, WSJGDM = WSJGDM }).ToList();
        }
        /// <summary>
        /// 医护人员字典
        /// </summary>
        /// <returns></returns>
        public static List<TB_DIC_Practitioner> QueryTB_DIC_Practitioner()
        {
            const string sql = @"exec TB_DIC_Practitioner @orgId ,@YLJGDM, @wsjgdm";
            return Conn.Query<TB_DIC_Practitioner>(sql, new { orgId = orgId, YLJGDM = YLJGDM, WSJGDM = WSJGDM }).ToList();
        }
        /// <summary>
        /// 药品目录字典
        /// </summary>
        /// <returns></returns>
        public static List<TB_DIC_MEDICINES> QueryTB_DIC_MEDICINES()
        {
            const string sql = @"exec TB_DIC_MEDICINES @orgId ,@YLJGDM, @wsjgdm";
            return Conn.Query<TB_DIC_MEDICINES>(sql, new { orgId = orgId, YLJGDM = YLJGDM, WSJGDM = WSJGDM }).ToList();
        }
        /// <summary>
        /// 非药品目录字典
        /// </summary>
        /// <returns></returns>
        public static List<TB_DIC_Materials> QueryTB_DIC_Materials()
        {
            const string sql = @"exec TB_DIC_Materials @orgId ,@YLJGDM, @wsjgdm";
            return Conn.Query<TB_DIC_Materials>(sql, new { orgId = orgId, YLJGDM = YLJGDM, WSJGDM = WSJGDM }).ToList();
        }
        #endregion

        #region 执行sql语句脚本
        public static void ExecuteSqlTran(List<String> SQLStringList,string req,out string errmsg)
        {
            errmsg = "";
            var dbstring = "";
            if (req == "Y")
            {
                dbstring = Conn.ConnectionString;
            }
            else if(req == "U")
            {
                dbstring = PD_UPLOAD.ConnectionString;
            }
            else
            {
                dbstring = LogConn;
            }
            using (SqlConnection conn = new SqlConnection(dbstring))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandTimeout = SqlTimeOutNumber;
                cmd.Connection = conn;
                SqlTransaction tx = conn.BeginTransaction();
                cmd.Transaction = tx;

                try
                {
                    for (int n = 0; n < SQLStringList.Count; n++)
                    {
                        string strsql = SQLStringList[n];
                        if (strsql.Trim().Length > 1)
                        {
                            cmd.CommandText = strsql;
                            cmd.CommandType = CommandType.Text;
                            cmd.ExecuteNonQuery();
                        }
                    }
                    tx.Commit();
                }
                catch (Exception e)
                {
                    errmsg = e.Message;
                    tx.Rollback();
                    AppLogger.Info("轮询数据数据库日志记录失败："+e.Message);
                }
                finally {
                    cmd.Dispose();
                    conn.Close();
                }
            }
        }
        #endregion
    }

}
