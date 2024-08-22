using NeiMengGuYiBaoApp.Models.Input;
using NeiMengGuYiBaoApp.Models.Input.NationECCodeDll;
using NeiMengGuYiBaoApp.Models.Input.YiBao;
using NeiMengGuYiBaoApp.Models.Output;
using NeiMengGuYiBaoApp.Models.Output.NationECCodeDll;
using NeiMengGuYiBaoApp.Models.Output.YiBao;
using NeiMengGuYiBaoApp.Models.Post;
using NeiMengGuYiBaoApp.Models.Post.NationECCodeDll;
using NeiMengGuYiBaoApp.Models.Post.YiBao;
using NeiMengGuYiBaoApp.Models.Log;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using wqsj_PlatForm_DAS;

namespace NeiMengGuYiBaoApp.Service
{
    public static class ClassSqlHelper
    {
        private static PlatFormService _platFormServer;
        /// <summary>
        /// 调用公共sql执行方法
        /// </summary>
        public static PlatFormService platFormServer
        {
            get
            {
                if (_platFormServer == null)
                {
                    _platFormServer = new PlatFormService();
                }
                return _platFormServer;
            }
        }


        static Dictionary<string, object> Parameters = new Dictionary<string, object>();

        /// <summary>
        /// 获取数据库时间，若数据库异常则取本机当前时间
        /// </summary>
        /// <returns></returns>
        public static DateTime GetServerTime()
        {
            try
            {
                return Convert.ToDateTime(platFormServer.Query("select getdate()").Tables[0].Rows[0][0]);
            }
            catch
            {
                return DateTime.Now;
            }
        }

        /// <summary>
        /// 获取凭证号码
        /// </summary>
        /// <param name="pzhmmc">名称</param>
        /// <param name="pzhm">凭证号</param>
        /// <returns></returns>
        public static bool GetNumber(string pzhmmc, out decimal pzhm)
        {
            pzhm = 0;

            Parameters.Clear();
            Parameters.Add("@sr_pzhmmc", pzhmmc);
            Parameters.Add("@sc_pzhm", pzhm);
            DataTable dt_content = platFormServer.RunProc_DataTable_WqServer("ybjk_se_pzhm", Parameters);

            if (dt_content.Rows.Count == 0)
            {
                return false;
            }
            else
            {

                return true;
            }

        }

        #region  获取凭证号
        /// <summary>
        /// 根据凭证号码类型枚举获取最新凭证号码
        /// 在获取凭证号码前，该方法会自动将凭证号码加1
        /// </summary>
        /// <param name="certificateType">凭证号码类型</param>
        /// <returns>返回凭证号码</returns>
        public static bool GetCertificateNumber(string value, out decimal number)
        {
            number = -1;
            IDataParameter PCertificateType = new SqlParameter();
            PCertificateType.ParameterName = "@sr_pzhmmc";
            PCertificateType.DbType = DbType.String;
            PCertificateType.Value = value;
            IDataParameter PCertificateTypeOut = new SqlParameter();
            PCertificateTypeOut.ParameterName = "@sc_pzhm";
            PCertificateTypeOut.DbType = DbType.Decimal;
            PCertificateTypeOut.Direction = ParameterDirection.Output;
            PCertificateTypeOut.Value = 0;

            IDataParameter[] parameters = new IDataParameter[2] { PCertificateType, PCertificateTypeOut };
            platFormServer.Query("ybjk_se_pzhm", parameters, "table");
            number = Convert.ToDecimal(PCertificateTypeOut.Value);
            if (number > 0)
                return true;
            else
                return false;
        }

        #endregion

        /// <summary>
        /// 写入日志表 交易前进行调用
        /// </summary>
        /// <param name="log"></param>
        /// <returns></returns>
        public static int InputLogMain(LogMain log)
        {
            return platFormServer.ExecuteSql($"insert into ybjk_logmain(inNumber,inDate,tradiNumber,userIp,operatorId) select  '{log.inNumber}' ,'{log.inDate}','{log.tradiNumber}','{log.userIp}','{log.operatorId}'");
        }
        /// <summary>
        /// 写入日志内容表 交易后调用
        /// </summary>
        /// <param name="log"></param>
        /// <returns></returns>
        public static int InputLogContent(LogContent log)
        {
            return platFormServer.ExecuteSql($"insert into ybjk_logcontent(inNumbier,tradiNumber,hisId,beginDate,endDate,inHead,inContent,OutHead,outContent,errorMsg,stateId) select  '" +
                $"{log.inNumber}' ,'{log.tradiNumber}','{log.hisId}','{log.beginDate}','{log.endDate}','{log.inHead}','{log.inContent}','{log.outHead}','{log.outContent}','{log.errorMsg}','{log.stateId}'");
        }
        /// <summary>
        /// 查询病人诊断信息
        /// </summary>
        /// <param name="model">1查询门诊 2查询住院入院诊断 3 2查询住院出院诊断</param>
        /// <param name="hisId">门诊号或住院号</param>
        /// <returns></returns>
        public static DataTable QueryICD(int model, string hisId, string mdtrt_id, string psn_no)
        {
            string sql = "";
            if (model == 1)
            {
                sql = string.Format($@"select 1 diag_type,zy.zdlx diag_srt_no,zd.icd10 diag_code,zy.zdmc diag_name,
isnull(dept.ybksbm, jz.jzks) diag_dept,  isnull(staff.gjybdm, jz.jzys) dise_dor_no, jz.jzysmc dise_dor_name, convert(varchar(20), zy.createtime, 120)diag_time,
 zy.zt vali_flag from Newtouch_CIS.dbo.xt_jz jz
 join Newtouch_CIS.dbo.xt_xyzd zy  on jz.jzId = zy.jzId and jz.OrganizeId = zy.OrganizeId and  jz.zt = 1
 join NewtouchHIS_Base..V_S_Sys_Staff staff on staff.gh = jz.jzys and staff.OrganizeId = jz.OrganizeId
 left join NewtouchHIS_Base.dbo.V_S_Sys_Department dept on dept.Code = jz.jzks and dept.OrganizeId = jz.OrganizeId
 left join NewtouchHIS_Base..V_S_xt_zd zd on zd.zdCode=zy.zdCode and zd.OrganizeId=zy.OrganizeId and zd.zt='1'
 where  jz.mzh ='{hisId}'" +
                                        $@" union all select 2 diag_type, zy.zdlx diag_srt_no, zd.icd10 diag_code, zy.zdmc diag_name, isnull(dept.ybksbm,jz.jzks) diag_dept,
 isnull(staff.gjybdm, jz.jzys) dise_dor_no, jz.jzysmc dise_dor_name, convert(varchar(20), zy.createtime, 120)diag_time, zy.zt vali_flag
 from Newtouch_CIS.dbo.xt_jz jz
 join Newtouch_CIS.dbo.xt_zyzd zy  on jz.jzId = zy.jzId and jz.OrganizeId = zy.OrganizeId and  jz.zt = 1
  join NewtouchHIS_Base..V_S_Sys_Staff staff on staff.gh = jz.jzys and staff.OrganizeId = jz.OrganizeId
 left join NewtouchHIS_Base.dbo.V_S_Sys_Department dept on dept.Code = jz.jzks and dept.OrganizeId = jz.OrganizeId
  left join NewtouchHIS_Base..V_S_xt_zd zd on zd.zdCode=zy.zdCode and zd.OrganizeId=zy.OrganizeId and zd.zt='1'
 where  jz.mzh = '{hisId}'");
            }
            else if (model == 2)
            {
                sql = string.Format($@"select 1 diag_type, zy.zdpx diag_srt_no, zy.icd10 diag_code, zy.zdmc diag_name, isnull(dept.ybksbm,jz.ks) diag_dept, isnull(ys.gjybdm,jz.doctor) dise_dor_no,ys.Name dise_dor_name, convert(varchar(20), zy.createtime, 120)diag_time, zy.zt vali_flag ,case zy.zdpx when 1 then 1 else 0 end maindiag_flag, '{mdtrt_id}' mdtrt_id ,'{psn_no}' psn_no " +
                    $@"from NewtouchHis_sett.[dbo].[zy_brjbxx]  jz
 join NewtouchHis_sett.dbo.zy_rydzd zy on jz.zyh = zy.zyh and jz.OrganizeId = zy.OrganizeId and  jz.zt = 1 and zy.zt=1
 join NewtouchHIS_Base.dbo.Sys_Staff ys on jz.doctor = ys.gh 
 left join NewtouchHIS_Base.dbo.V_S_Sys_Department dept on dept.Code=jz.ks and dept.OrganizeId=jz.OrganizeId
where  jz.zyh = '{hisId}'");
            }
            else if (model == 3)
            {
                sql = string.Format($"select 1 diag_type,case zy.zdlx when 0 then 1 else 0 end maindiag_flag, zy.zdlx diag_srt_no, zd.icd10 diag_code, zy.zdmc diag_name,  isnull(dept.ybksbm,jz.ks)  diag_dept, isnull(ys.gjybdm,jz.doctor)  dise_dor_no, ys.Name dise_dor_name, convert(varchar(20), zy.createtime, 120)diag_time, '{mdtrt_id}' mdtrt_id ,'{psn_no}' psn_no " +
                    $@" from NewtouchHis_sett.[dbo].[zy_brjbxx]  jz
 join [Newtouch_CIS].[dbo].[zy_PatDxInfo]  zy on jz.zyh = zy.zyh and jz.OrganizeId = zy.OrganizeId and  jz.zt = 1  and zy.zdlb = 2 and zy.zt = 1 and zy.zdmc <> '999999999'
 join NewtouchHIS_Base.dbo.Sys_Staff ys on jz.doctor = ys.gh
 left join NewtouchHIS_Base.dbo.V_S_Sys_Department dept on dept.Code = jz.ks and dept.OrganizeId = jz.OrganizeId
  left join NewtouchHIS_Base.[dbo].[xt_zd] zd on zd.zdCode=zy.zddm and zd.OrganizeId=zy.OrganizeId and zd.zt='1'
" +
                    $"where  jz.zyh = '{hisId}'");
            }
            return platFormServer.Query(sql).Tables[0];
        }
        /// <summary>
        /// 执行SQL 语句
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static int ExecuteSql(string sql)
        {
            return platFormServer.ExecuteSql(sql);
        }

        /// <summary>
        /// 查询门诊费用上传信息
        /// </summary>
        /// <param name="hisId">hisid</param>
        /// <returns></returns>
        public static DataTable QuertFeedetail(Post_2204 post2204)
        {
            /*
            @type varchar(1),--0挂号  1处方  3 处方退费再传  
            @mzh varchar(20),    
            @ghxm varchar(20),  --挂号项目  
            @zlxm varchar(20),  --诊疗项目  
            @ckf  varchar(20),  --磁卡费  
            @gbf varchar(20),  --工本费  
            @orgId varchar(50),  --组织机构  
            @jzid varchar(50),  --就诊id  
            @rybh varchar(50),  --人员编号  
            @pch varchar(50), --收费批次号  
            @jsnm varchar(50), --结算内码   
            @ksbm varchar(50),--科室编码  
            @ysbm varchar(50)--医生编码  
             */
            Parameters.Clear();
            Parameters.Add("@type", post2204.type);
            Parameters.Add("@mzh", post2204.hisId);
            Parameters.Add("@cfnm", post2204.cfnm);
            Parameters.Add("@ghxm", post2204.ghxm);
            Parameters.Add("@zlxm", post2204.zlxm);
            Parameters.Add("@ckf", post2204.ckf);
            Parameters.Add("@gbf", post2204.gbf);
            Parameters.Add("@orgId", post2204.orgId);
            Parameters.Add("@jzid", post2204.jzid);
            Parameters.Add("@rybh", post2204.rybh);
            Parameters.Add("@pch", post2204.pch);
            Parameters.Add("@jsnm", post2204.jsnm);
            Parameters.Add("@ksbm", post2204.ksbm);
            Parameters.Add("@ysbm", post2204.ysbm);
            Parameters.Add("@dise_codg", post2204.dise_codg);
            return platFormServer.RunProc_DataTable_WqServer("mz_fymxsc", Parameters);
        }

        /// 合并执行多条sql语句
        /// </summary>
        /// <param name="SQLStringList">sql语句集合</param>
        /// <returns>受影响行数</returns>
        public static bool Merge(List<string> SQLStringList, out int errorNo)
        {
            return platFormServer.Merge(SQLStringList, out errorNo);
        }

        /// <summary>
        /// 删除门诊费用上传信息
        /// </summary>
        /// <param name="hisId"></param>
        /// <param name="errorNo"></param>
        /// <returns></returns>
        public static bool UpFeedetail(string hisId, string chrg_bchno, out int errorNo)
        {

            List<string> SQLList = new List<string>();
            if (chrg_bchno == "0000")//删除全部
            {
                SQLList.Add(string.Format($"delete Drjk_mzfymxxxsc_input where mzh='{hisId}'  "));
                SQLList.Add(string.Format($"delete Drjk_mzfymxxxsc_output where mzh='{hisId}'  "));
            }
            else
            {
                SQLList.Add(string.Format($"delete Drjk_mzfymxxxsc_input where mzh='{hisId}' and chrg_bchno='{chrg_bchno}' "));
                SQLList.Add(string.Format($"delete Drjk_mzfymxxxsc_output where mzh='{hisId}' and chrg_bchno='{chrg_bchno}' "));
            }
            return Merge(SQLList, out errorNo);
        }

        /// <summary>
        /// 查询病人费用合计信息
        /// </summary>
        /// <param name="model">1查询门诊 2查询住院</param>
        /// <param name="hisId">门诊号或住院号</param>
        /// <returns></returns>
        public static DataTable QueryCost(int model, string hisId, string chrg_bchno)
        {
            string sql = "";
            if (model == 1)
            {
                sql = string.Format($"select sum(fulamt_ownpay_amt) fulamt_ownpay_amt ,sum(overlmt_amt)  overlmt_selfpay ,sum(preselfpay_amt)preselfpay_amt,sum(inscp_scp_amt) inscp_scp_amt ,sum(det_item_fee_sumamt) det_item_fee_sumamt from Drjk_mzfymxxxsc_output where  mzh = '{hisId}' and chrg_bchno='{chrg_bchno}' ");
            }
            else
            {
                sql = string.Format($"select sum(fulamt_ownpay_amt) fulamt_ownpay_amt ,sum(overlmt_amt)  overlmt_selfpay ,sum(preselfpay_amt)preselfpay_amt,sum(inscp_scp_amt) inscp_scp_amt ,sum(det_item_fee_sumamt) det_item_fee_sumamt from drjk_zyfymxsc_output  where zyh = '{hisId}'  ");
            }
            return platFormServer.Query(sql).Tables[0];
        }

        /// <summary>
        /// 撤销门诊结算信息
        /// </summary>
        /// <param name="hisId"></param>
        /// <param name="errorNo"></param>
        /// <returns></returns>
        public static bool UpSettlement(string hisId, string setl_id, string operatorId,string cxsetl_id, out int errorNo)
        {
            List<string> SQLList = new List<string>();
            SQLList.Add(string.Format($"update drjk_mzjs_input set zt=0,zt_rq=GETDATE(),zt_czy='{operatorId}' where mzh='{hisId}' and setl_id='{setl_id}' "));
            SQLList.Add(string.Format($"update drjk_mzjs_output set zt=0,zt_rq=GETDATE(),zt_czy='{operatorId}',cxsetl_id='{cxsetl_id}' where mzh='{hisId}' and setl_id='{setl_id}' "));
            return Merge(SQLList, out errorNo);
        }
        /// <summary>
        /// 撤销门诊结算信息
        /// </summary>
        /// <param name="hisId"></param>
        /// <param name="errorNo"></param>
        /// <returns></returns>
        public static bool UpSettlement(string hisId, string setl_id, string operatorId, out int errorNo)
        {
            List<string> SQLList = new List<string>();
            SQLList.Add(string.Format($"update drjk_mzjs_input set zt=0,zt_rq=GETDATE(),zt_czy='{operatorId}' where mzh='{hisId}' and setl_id='{setl_id}' "));
            SQLList.Add(string.Format($"update drjk_mzjs_output set zt=0,zt_rq=GETDATE(),zt_czy='{operatorId}' where mzh='{hisId}' and setl_id='{setl_id}' "));
            return Merge(SQLList, out errorNo);
        }
        /// <summary>
        /// 查询住院登记信息
        /// </summary>
        /// <param name="hisId"></param>
        /// <param name="mdtrt_id">医保返回的就诊登记id</param>
        /// <param name="mode">查询类型 1 登记信息 2 修改信息 3. 出院登记</param>
        /// <returns></returns>
        public static DataTable QueryHospitalInfo(Post_2401 post, int mode)
        {
            /*
            @hisId varchar(20), --住院号  
            @orgId varchar(50),--组织机构  
            @mdtrt_cert_type varchar(5), --就诊类型  
            @mdtrt_cert_no varchar(50), --就诊凭证编号  
            @med_type varchar(20),--医疗类别  
            @mdtrt_id varchar(50), --就诊ID  
            @type varchar(2) --类型 1登记  2修改  
             */
            Parameters.Clear();
            Parameters.Add("@hisId", post.hisId);
            Parameters.Add("@orgId", post.orgId);
            Parameters.Add("@mdtrt_cert_type", post.mdtrt_cert_type);
            Parameters.Add("@mdtrt_cert_no", post.mdtrt_cert_no);
            Parameters.Add("@med_type", post.med_type);
            Parameters.Add("@mdtrt_id", post.mdtrt_id);
            Parameters.Add("@type", mode);
            Parameters.Add("@hisIdNum", post.hisIdNum);
            return platFormServer.RunProc_DataTable_WqServer("zy_rydjxx", Parameters);
        }

        public static int UpMdtrtinfo(string operatorId, string hisId, string mdtrt_id)
        {
            return platFormServer.ExecuteSql($"update drjk_rybl_input set zt=0,zt_rq=GETDATE(),zt_czy='{operatorId}' where zyh='{hisId}' and mdtrt_id='{mdtrt_id}'");
        }
        public static int UpMdtrCWXX(string adm_bed, string hisId, string mdtrt_id)
        {
            return platFormServer.ExecuteSql($"update drjk_rybl_input set adm_bed='{adm_bed}' where zyh='{hisId}' and mdtrt_id='{mdtrt_id}'");
        }
        /// <summary>
        /// 查询门诊费用上传信息
        /// </summary>
        /// <param name="hisId">hisid</param>
        /// <returns></returns>
        public static DataTable QuertHospitalFeedetail(Post_2301 post2204)
        {
            /*
             @orgId varchar(50),  --组织结构
            @zyh varchar(20),--住院号
            @yllb varchar(20)--医疗类别
             */
            Parameters.Clear();
            Parameters.Add("@orgId", post2204.orgId);
            Parameters.Add("@zyh", post2204.hisId);
            Parameters.Add("@yllb", post2204.med_type);
            return platFormServer.RunProc_DataTable_WqServer("zy_fymxsc", Parameters);
        }

        /// <summary>
        /// 查询住院费用上传信息
        /// </summary>
        /// <param name="hisId">hisid</param>
        /// <returns></returns>
        public static DataTable QuertHospitalFeedetailV2(Post_2301V2 post2204)
        {
            /*
             @orgId varchar(50),  --组织结构
            @zyh varchar(20),--住院号
            @yllb varchar(20)--医疗类别
             */
            Parameters.Clear();
            Parameters.Add("@orgId", post2204.orgId);
            Parameters.Add("@zyh", post2204.hisId);
            Parameters.Add("@yllb", post2204.med_type);
            Parameters.Add("@jssj", post2204.jssj);
            Parameters.Add("@jfbbh", post2204.jfbbh?? "");
            return platFormServer.RunProc_DataTable_WqServer("zy_fymxscV2", Parameters);
        }

        /// <summary>
        /// 删除住院费用上传信息
        /// </summary>
        /// <param name="hisId"></param>
        /// <param name="errorNo"></param>
        /// <returns></returns>
        public static bool UpHospitaFeedetail(string hisId, string feedetl_sn, out int errorNo)
        {
            List<string> SQLList = new List<string>();
            if (feedetl_sn == "0000")//删除全部
            {
                SQLList.Add(string.Format($"delete Drjk_zyfymxsc_input where zyh='{hisId}' "));
                SQLList.Add(string.Format($"delete Drjk_zyfymxsc_output where zyh='{hisId}' "));
            }
            else
            {
                SQLList.Add(string.Format($"delete Drjk_zyfymxsc_input where zyh='{hisId}' and feedetl_sn in (select * from dbo.f_split('{feedetl_sn}',','))"));
                SQLList.Add(string.Format($"delete Drjk_zyfymxsc_output where zyh='{hisId}' and feedetl_sn in (select * from dbo.f_split('{feedetl_sn}',','))"));
                //SQLList.Add(string.Format($"delete Drjk_zyfymxsc_input where zyh='{hisId}' and feedetl_sn='{feedetl_sn}' "));
                //SQLList.Add(string.Format($"delete Drjk_zyfymxsc_output where zyh='{hisId}' and feedetl_sn='{feedetl_sn}' "));
            }
            return Merge(SQLList, out errorNo);
        }

        /// <summary>
        /// 查询住院登记信息
        /// </summary>
        /// <param name="hisId"></param>
        /// <param name="mdtrt_id">医保返回的就诊登记id</param>
        /// <param name="mode">查询类型 1 登记信息 2 修改信息</param>
        /// <returns></returns>
        //public static DataTable QueryHospitalOutInfo(string hisId, string mdtrt_id, int mode)
        //{
        //    Parameters.Clear();
        //    Parameters.Add("@zyh", hisId);
        //    Parameters.Add("@jzh", hisId);
        //    return platFormServer.RunProc_DataTable_WqServer("mz_fymxsc", Parameters);
        //}

        public static int UpHospitaMdtrtinfo(string operatorId, string hisId, string mdtrt_id, int cybz)
        {
            return platFormServer.ExecuteSql($"update drjk_rybl_input set cybz={cybz} , cy_rq=GETDATE() ,cy_czydm='{operatorId}' where zyh='{hisId}' and mdtrt_id='{mdtrt_id}'and zt=1 ");
        }

        /// <summary>
        /// 撤销住院结算信息
        /// </summary>
        /// <param name="hisId"></param>
        /// <param name="errorNo"></param>
        /// <returns></returns>
        public static bool UpHospitaSettlement(string hisId, string setl_id, string operatorId,string cxsetl_id, out int errorNo)
        {
            List<string> SQLList = new List<string>();
            SQLList.Add(string.Format($"update Drjk_zyjs_input set zt=0,zt_rq=GETDATE(),zt_czy='{operatorId}' where zyh='{hisId}' and setl_id='{setl_id}' and zt=1 "));
            SQLList.Add(string.Format($"update drjk_zyjs_output set zt=0,zt_rq=GETDATE(),zt_czy='{operatorId}',cxsetl_id='{cxsetl_id}' where zyh='{hisId}' and setl_id='{setl_id}' and zt=1 "));
            return Merge(SQLList, out errorNo);
        }
        /// <summary>
        /// 撤销住院结算信息
        /// </summary>
        /// <param name="hisId"></param>
        /// <param name="errorNo"></param>
        /// <returns></returns>
        public static bool UpHospitaSettlement(string hisId, string setl_id, string operatorId, out int errorNo)
        {
            List<string> SQLList = new List<string>();
            SQLList.Add(string.Format($"update Drjk_zyjs_input set zt=0,zt_rq=GETDATE(),zt_czy='{operatorId}' where zyh='{hisId}' and setl_id='{setl_id}' and zt=1 "));
            SQLList.Add(string.Format($"update drjk_zyjs_output set zt=0,zt_rq=GETDATE(),zt_czy='{operatorId}' where zyh='{hisId}' and setl_id='{setl_id}' and zt=1 "));
            return Merge(SQLList, out errorNo);
        }

        /// <summary>
        /// 查询对账信息
        /// </summary>
        /// <param name="input2301"></param>
        /// <param name="mode">--1汇总  0 明细</param>
        /// <returns></returns>
        public static DataTable QueryReconciliation(Post_3201 input2301, int mode)
        {
            //@insutype varchar(30),--1  insutype 险种  字符型  6  Y Y
            //@clr_type varchar(30),--2  clr_type 清算类别  字符型  6  Y Y
            //@setl_optins varchar(40),--3  setl_optins 结算经办机构  字符型  6  Y
            //@stmt_begndate datetime,--4  stmt_begndate 对账开始日期  日期型 Y
            //@stmt_enddate datetime--5  stmt_enddate 对账结束日期  日期型 Y
            // @lb int ,--1汇总  0 明细
            Parameters.Clear();
            Parameters.Add("@lb", mode);
            Parameters.Add("@insutype", input2301.insutype);
            Parameters.Add("@clr_type", input2301.clr_type);
            Parameters.Add("@setl_optins", input2301.setl_optins);
            Parameters.Add("@stmt_begndate", input2301.stmt_begndate);
            Parameters.Add("@stmt_enddate", input2301.stmt_enddate);
            return platFormServer.RunProc_DataTable_WqServer("drjk_se_czxx", Parameters);
        }


		/// <summary>
		///  desc :3102 接口回调 存储信息  
		/// </summary>
		/// <param name="hisid"></param>
		/// <returns></returns>
		public static int Upload3102(string orgId, DateTime ksrq, DateTime jsrq, string clrtype, string insutype, XmlDocument xml)
		{
			//Parameters.Clear();
			//Parameters.Add("@orgId", orgId);
			//Parameters.Add("@zyh", hisid);
			//return platFormServer.RunProc_DataTable_WqServer("usp_Inp_ybupload_cyjs_diseinfoV2", Parameters);
			return platFormServer.ExecuteSql(string.Format($"exec usp_Inp_ybupload_cyjs_3102 @orgId='{orgId}',@ksrq='{ksrq}',@jsrq='{jsrq}',@clrtype='{clrtype}',@insutype='{insutype}',@xml='{xml.InnerXml}' "));

		}
		/// <summary>
		/// 查询对账信息
		/// </summary>
		/// <param name="input3211"></param>
		/// <param name="mode">--1汇总  0 明细</param>
		/// <returns></returns>
		public static DataTable DailyReconciliation(Post_3211 input3211, int mode)
        {
            //@insutype varchar(30),--1  insutype 险种  字符型  6  Y Y
            //@clr_type varchar(30),--2  clr_type 清算类别  字符型  6  Y Y
            //@setl_optins varchar(40),--3  setl_optins 结算经办机构  字符型  6  Y
            //@stmt_begndate datetime,--4  stmt_begndate 对账开始日期  日期型 Y
            //@stmt_enddate datetime--5  stmt_enddate 对账结束日期  日期型 Y
            // @lb int ,--1汇总  0 明细
            Parameters.Clear();
            Parameters.Add("@clr_begin_ymd", input3211.clr_begin_ymd);
            Parameters.Add("@clr_end_ymd", input3211.clr_end_ymd);
            return platFormServer.RunProc_DataTable_WqServer("drjk_se_rdzxx", Parameters);
        }

        /// <summary>
        /// 名    称：[drjk_se_dmxx] 东软接口_查询_对码信息表   
        /// </summary>
        /// <param name="mode">0 全部 1 上传 2 未上传</param>
        /// <returns></returns>
        public static DataTable QueryTheCode(int mode)
        {
            // @lb int ,--1汇总  0 明细
            Parameters.Clear();
            Parameters.Add("@lb", mode);

            return platFormServer.RunProc_DataTable_WqServer("drjk_se_dmxx", Parameters);
        }
        /// <summary>
        /// 更新对码信息
        /// </summary>
        /// <param name="hisId"></param>
        /// <param name="mode"></param>
        /// <param name="getdate"></param>
        /// <returns></returns>
        public static int UpTheCode(string hisId, int mode, DateTime getdate)
        {
            if (getdate.ToString("yyyy-MM-dd") == "1900-01-01")
            {
                if (mode == 101 || mode == 102 || mode == 104)
                {
                    return platFormServer.ExecuteSql($"update newtouchhis_base.dbo.xt_ypsx set  ybmlscrq=null where ypId='{hisId}'");
                }
                else
                {
                    return platFormServer.ExecuteSql($"update newtouchhis_base.dbo.xt_sfxm set  ybmlscrq=null where sfxmId='{hisId}'");
                }
            }
            else
            {
                if (mode == 101 || mode == 102 || mode == 104)
                {
                    return platFormServer.ExecuteSql($"update newtouchhis_base.dbo.xt_ypsx set  ybmlscrq='{getdate}' where ypId='{hisId}'");
                }
                else
                {
                    return platFormServer.ExecuteSql($"update newtouchhis_base.dbo.xt_sfxm set  ybmlscrq='{getdate}' where sfxmId='{hisId}'");
                }
            }
        }

        /// <summary>
        ///  查询注意转院信息  转院交易 2501
        /// </summary>
        /// <param name="hisid"></param>
        /// <returns></returns>
        public static DataTable QueryHospitalInfo(string hisid)
        {
            Parameters.Clear();
            Parameters.Add("@hisId", hisid);
            return platFormServer.RunProc_DataTable_WqServer("zy_zybaxx", Parameters);
        }
        /// <summary>
        /// 查询转院备案信息
        /// </summary>
        /// <param name="qrq"></param>
        /// <param name="zrq"></param>
        /// <returns></returns>
        public static DataTable QueryTransfer(DateTime qrq, DateTime zrq)
        {
            string sql = string.Format($"select psn_no  人员编号,tel 联系电话,addr  联系地址, diag_name  诊断名称,reflin_medins_name 转往医院名称,refl_rea 转院原因,refl_opnn 转院意见,trt_dcla_detl_sn 待遇申报明细流水号,czrq 办理日期,case zt when 0 then '已撤销' else '正常'end 状态,zt_rq 撤销日期 from drjk_zyba_input where czrq>='{qrq}' and czrq<='{zrq}'");
            return platFormServer.Query(sql).Tables[0];

        }

        /// <summary>
        /// 插销转院办理
        /// </summary>
        /// <param name="rybh"></param>
        /// <param name="jylsh"></param>
        /// <param name="czydm"></param>
        /// <returns></returns>
        public static int UpHospitalInfo(string rybh, string jylsh, string czydm)
        {

            return platFormServer.ExecuteSql($"update drjk_zyba_input set zt = 0, zt_rq = getdate(), zt_czy = '{czydm}' where psn_no = '{rybh}' and trt_dcla_detl_sn = '{jylsh}'");
        }

        /// <summary>
        /// 查询病种信息
        /// </summary>
        /// <returns></returns>
        public static DataTable QueryDiseasest()
        {
            string sql = string.Format($"select * from [NewtouchHIS_Base].[dbo].[Mz_Mtbbzml] ");

            return platFormServer.Query(sql).Tables[0];
        }

        /// <summary>
        /// 查询转院备案信息
        /// </summary>
        /// <param name="qrq"></param>
        /// <param name="zrq"></param>
        /// <returns></returns>
        public static DataTable QueryPatientDiseasest(DateTime qrq, DateTime zrq)
        {
            string sql = string.Format($"select psn_no  人员编号,tel 联系电话,addr  联系地址,insutype  险种类型,opsp_dise_name  门慢门特病种名称,trt_dcla_detl_sn 待遇申报明细流水号,opsp_dise_code  门慢门特病种目录代码,ide_fixmedins_name 鉴定定点医药机构名称,hosp_ide_date  医院鉴定日期,diag_dr_name  诊断医师姓名,czrq 办理日期,case zt when 0 then '已撤销' else '正常'end 状态,zt_rq 撤销日期 from Drjk_rymtbba_input where czrq>='{qrq}' and czrq<='{zrq}'");
            return platFormServer.Query(sql).Tables[0];

        }

        /// <summary>
        /// 插销慢特病办理
        /// </summary>
        /// <param name="rybh"></param>
        /// <param name="jylsh"></param>
        /// <param name="czydm"></param>
        /// <returns></returns>
        public static int UpPatientDiseasest(string rybh, string jylsh, string czydm)
        {

            return platFormServer.ExecuteSql($"update Drjk_rymtbba_input set zt = 0, zt_rq = getdate(), zt_czy = '{czydm}' where psn_no = '{rybh}' and trt_dcla_detl_sn = '{jylsh}'");
        }

        /// <summary>
        /// 查询定点信息
        /// </summary>
        /// <param name="qrq"></param>
        /// <param name="zrq"></param>
        /// <returns></returns>
        public static DataTable QueryFixedPoint(DateTime qrq, DateTime zrq)
        {
            string sql = string.Format($" select psn_no  人员编号,tel 联系电话,addr 联系地址, agnter_certno  代办人证件号码,fix_srt_no  定点排序号,trt_dcla_detl_sn 待遇申报明细流水号,agnter_tel  代办人联系方式 ,agnter_addr  代办人,fixmedins_name 鉴定定点医药机构名称,memo  备注,czrq 办理日期,case zt when 0 then '已撤销' else '正常'end 状态,zt_rq 撤销日期 from drjk_ryddba_input where czrq>='{qrq}' and czrq<='{zrq}'");
            return platFormServer.Query(sql).Tables[0];

        }

        public static int UpFixedPoint(string rybh, string jylsh, string czydm)
        {

            return platFormServer.ExecuteSql($"update drjk_ryddba_input set zt = 0, zt_rq = getdate(), zt_czy = '{czydm}' where psn_no = '{rybh}' and trt_dcla_detl_sn = '{jylsh}'");
        }


        /// <summary>
        ///  desc :医保上传 -医疗保障基金结算清单-setlinfo  
        /// </summary>
        /// <param name="hisid"></param>
        /// <returns></returns>
        public static DataTable QuerySetlinfo4101(string orgId,string hisid)
        {
            Parameters.Clear();
            Parameters.Add("@orgId", orgId);
            Parameters.Add("@zyh", hisid);
            return platFormServer.RunProc_DataTable_WqServer("usp_Inp_ybupload_cyjs_setlinfo", Parameters);
        }
        /// <summary>
        ///  desc :医保上传 -医疗保障基金结算清单-setlinfo  
        /// </summary>
        /// <param name="hisid"></param>
        /// <returns></returns>
        public static DataTable QuerySetlinfo4101A(string orgId, string hisid)
        {
            Parameters.Clear();
            Parameters.Add("@orgId", orgId);
            Parameters.Add("@zyh", hisid);
            return platFormServer.RunProc_DataTable_WqServer("usp_Inp_ybupload_cyjs_setlinfoV2", Parameters);
        }

        /// <summary>
        ///  desc :医保上传 -医疗保障基金结算清单-oprninfo 手术  
        /// </summary>
        /// <param name="hisid"></param>
        /// <returns></returns>
        public static DataTable QueryOprninfo4101(string orgId, string hisid)
        {
            Parameters.Clear();
            Parameters.Add("@orgId", orgId);
            Parameters.Add("@zyh", hisid);
            return platFormServer.RunProc_DataTable_WqServer("usp_Inp_ybupload_cyjs_oprninfo", Parameters);
        }
        /// <summary>
        ///  desc :医保上传 -医疗保障基金结算清单-oprninfo 手术  
        /// </summary>
        /// <param name="hisid"></param>
        /// <returns></returns>
        public static DataTable QueryOprninfo4101A(string orgId, string hisid)
        {
            Parameters.Clear();
            Parameters.Add("@orgId", orgId);
            Parameters.Add("@zyh", hisid);
            return platFormServer.RunProc_DataTable_WqServer("usp_Inp_ybupload_cyjs_oprninfoV2", Parameters);
        }
        /// <summary>
        ///  desc :医保上传 -医疗保障基金结算清单-diseinfo 住院诊断  
        /// </summary>
        /// <param name="hisid"></param>
        /// <returns></returns>
        public static DataTable QueryDiseinfo4101(string orgId, string hisid)
        {
            Parameters.Clear();
            Parameters.Add("@orgId", orgId);
            Parameters.Add("@zyh", hisid);
            return platFormServer.RunProc_DataTable_WqServer("usp_Inp_ybupload_cyjs_diseinfo", Parameters);
        }
        /// <summary>
        ///  desc :医保上传 -医疗保障基金结算清单-diseinfo 住院诊断  
        /// </summary>
        /// <param name="hisid"></param>
        /// <returns></returns>
        public static DataTable QueryDiseinfo4101A(string orgId, string hisid)
        {
            Parameters.Clear();
            Parameters.Add("@orgId", orgId);
            Parameters.Add("@zyh", hisid);
            return platFormServer.RunProc_DataTable_WqServer("usp_Inp_ybupload_cyjs_diseinfoV2", Parameters);
        }
        /// <summary>
        /// 东软接口_查询_基金支付信息（节点标识：payinfo）  
        /// </summary>
        /// <param name="hisid"></param>
        /// <returns></returns>
        public static DataTable QueryPayinfo4101(string orgId, string hisid)
        {
            Parameters.Clear();
            Parameters.Add("@orgId", orgId);
            Parameters.Add("@zyh", hisid);
            return platFormServer.RunProc_DataTable_WqServer("usp_Inp_ybupload_cyjs_payinfo", Parameters);
        }

        /// <summary>
        ///[usp_Inp_ybupload_cyjs_iteminfo] 表 158 收费项目信息（节点标识：iteminfo）    
        /// </summary>
        /// <param name="hisid"></param>
        /// <returns></returns>
        public static DataTable QueryIteminfo4101(string orgId, string hisid)
        {
            Parameters.Clear();
            Parameters.Add("@orgId", orgId);
            Parameters.Add("@zyh", hisid);
            return platFormServer.RunProc_DataTable_WqServer("usp_Inp_ybupload_cyjs_iteminfo", Parameters);
        }


        /// <summary>
        ///病案首页 表 169 输入-基本信息（节点标识：baseinfo）
        /// </summary>
        /// <param name="hisid"></param>
        /// <returns></returns>
        public static DataTable QueryBaseinfo4401(string orgId, string hisid,string orgcode)
        {
            Parameters.Clear();
            Parameters.Add("@orgId", orgId);
            Parameters.Add("@zyh", hisid);
            Parameters.Add("@orgcode", orgcode);
            return platFormServer.RunProc_DataTable_WqServer("usp_Inp_ybupload_basy_baseinfo", Parameters);
        }
        /// <summary>
        ///病案首页   诊断信息
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="hisid"></param>
        /// <returns></returns>
        public static DataTable QueryDiseinfo4401(string orgId, string hisid)
        {
            Parameters.Clear();
            Parameters.Add("@orgId", orgId);
            Parameters.Add("@zyh", hisid);
            return platFormServer.RunProc_DataTable_WqServer("usp_Inp_ybupload_basy_diseinfo", Parameters);
        }
        /// <summary>
        /// desc : -病案首页-oprninfo 手术记录  
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="hisid"></param>
        /// <returns></returns>
        public static DataTable QueryOprninfo4401(string orgId, string hisid)
        {
            Parameters.Clear();
            Parameters.Add("@orgId", orgId);
            Parameters.Add("@zyh", hisid);
            return platFormServer.RunProc_DataTable_WqServer("usp_Inp_ybupload_basy_oprninfo", Parameters);
        }
        /// <summary>
        /// 病案首页 icu
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="hisid"></param>
        /// <returns></returns>
        public static DataTable QueryIcuinfo4401(string orgId, string hisid)
        {
            Parameters.Clear();
            Parameters.Add("@orgId", orgId);
            Parameters.Add("@zyh", hisid);
            return platFormServer.RunProc_DataTable_WqServer("usp_Inp_ybupload_basy_icuinfo", Parameters);
        }

        /// <summary>
        /// 医嘱信息上传
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="hisid"></param>
        /// <returns></returns>
        public static DataTable QueryMedicalRecords4402(string orgId, string hisid)
        {
            Parameters.Clear();
            Parameters.Add("@orgId", orgId);
            Parameters.Add("@zyh", hisid);
            return platFormServer.RunProc_DataTable_WqServer("usp_Inp_ybupload_yzlist_data", Parameters);
        }


        /// <summary>
        /// 临床检查报告记录 表 174 输入-检查记录（节点标识：examinfo） 
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="hisid"></param>
        /// <returns></returns>
        public static DataTable QueryExaminfo4501(string orgId, string hisid)
        {
            Parameters.Clear();
            Parameters.Add("@orgId", orgId);
            Parameters.Add("@zyh", hisid);
            return platFormServer.RunProc_DataTable_WqServer("usp_Inp_ybupload_report_pacs_examinfo", Parameters);
        }

        /// <summary>
        /// 临床检查报告记录 表 177 检查影像信息（节点标识：imageinfo）
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="hisid"></param>
        /// <returns></returns>
        public static DataTable QueryImageinfo4501(string orgId, string hisid)
        {
            Parameters.Clear();
            Parameters.Add("@orgId", orgId);
            Parameters.Add("@zyh", hisid);
            return platFormServer.RunProc_DataTable_WqServer("usp_Inp_ybupload_report_pacs_imageinfo", Parameters);
        }

        /// <summary>
        /// 临床检查报告记录表 175 检查项目信息（节点标识：iteminfo
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="hisid"></param>
        /// <returns></returns>
        public static DataTable QueryIteminfo4501(string orgId, string hisid)
        {
            Parameters.Clear();
            Parameters.Add("@orgId", orgId);
            Parameters.Add("@zyh", hisid);
            return platFormServer.RunProc_DataTable_WqServer("usp_Inp_ybupload_report_pacs_iteminfo", Parameters);
        }

        /// <summary>
        /// 临床检查报告记录表 175 检查项目信息（节点标识：iteminfo
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="hisid"></param>
        /// <returns></returns>
        public static DataTable QuerySampleinfo4501(string orgId, string hisid)
        {
            Parameters.Clear();
            Parameters.Add("@orgId", orgId);
            Parameters.Add("@zyh", hisid);
            return platFormServer.RunProc_DataTable_WqServer("usp_Inp_ybupload_report_pacs_sampleinfo", Parameters);
        }


        /// <summary>
        /// 电子病历上传 187 输入-入院信息（节点标识：adminfo）
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="hisid"></param>
        /// <returns></returns>
        public static DataTable QueryAdminfo4701(string orgId, string hisid)
        {
            Parameters.Clear();
            Parameters.Add("@orgId", orgId);
            Parameters.Add("@zyh", hisid);
            return platFormServer.RunProc_DataTable_WqServer("usp_Inp_ybupload_dzbl_adminfo", Parameters);
        }


        /// <summary>
        ///电子病历上传 表 188 输入-诊断信息（节点标识：diseinfo
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="hisid"></param>
        /// <returns></returns>
        public static DataTable QueryDiseinfo4701(string orgId, string hisid)
        {
            Parameters.Clear();
            Parameters.Add("@orgId", orgId);
            Parameters.Add("@zyh", hisid);
            return platFormServer.RunProc_DataTable_WqServer("usp_Inp_ybupload_dzbl_diseinfo", Parameters);
        }

        /// <summary>
        ///电子病历上传 表 189 输入-病程记录（节点标识：coursrinfo）
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="hisid"></param>
        /// <returns></returns>
        public static DataTable QueryCoursrinfo4701(string orgId, string hisid)
        {
            Parameters.Clear();
            Parameters.Add("@orgId", orgId);
            Parameters.Add("@zyh", hisid);
            return platFormServer.RunProc_DataTable_WqServer("usp_Inp_ybupload_dzbl_coursrinfo", Parameters);
        }

        /// <summary>
        ///电子病历上传表 190 输入-手术记录（节点标识：oprninfo）
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="hisid"></param>
        /// <returns></returns>
        public static DataTable QueryOprninfo4701(string orgId, string hisid)
        {
            Parameters.Clear();
            Parameters.Add("@orgId", orgId);
            Parameters.Add("@zyh", hisid);
            return platFormServer.RunProc_DataTable_WqServer("usp_Inp_ybupload_dzbl_oprninfo", Parameters);
        }

        /// <summary>
        ///电子病历上传表 表 191 输入-病情抢救记录（节点标识：rescinfo）
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="hisid"></param>
        /// <returns></returns>
        public static DataTable QueryRescinfo4701(string orgId, string hisid)
        {
            Parameters.Clear();
            Parameters.Add("@orgId", orgId);
            Parameters.Add("@zyh", hisid);
            return platFormServer.RunProc_DataTable_WqServer("usp_Inp_ybupload_dzbl_rescinfo", Parameters);
        }

        /// <summary>
        ///电子病历上传表 表 192 输入-死亡记录（节点标识：dieinfo）
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="hisid"></param>
        /// <returns></returns>
        public static DataTable QueryDieinfo4701(string orgId, string hisid)
        {
            Parameters.Clear();
            Parameters.Add("@orgId", orgId);
            Parameters.Add("@zyh", hisid);
            return platFormServer.RunProc_DataTable_WqServer("usp_Inp_ybupload_dzbl_dieinfo", Parameters);
        }

        /// <summary>
        ///电子病历上传表 93 输入-出院小结（节点标识：dscginfo）
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="hisid"></param>
        /// <returns></returns>
        public static DataTable QueryDscginfo4701(string orgId, string hisid)
        {
            Parameters.Clear();
            Parameters.Add("@orgId", orgId);
            Parameters.Add("@zyh", hisid);
            return platFormServer.RunProc_DataTable_WqServer("usp_Inp_ybupload_dzbl_dscginfo", Parameters);
        }

        /// <summary>
        /// 医保结算清单上传更新本地结算信息中的流水号和上传时间        /// </summary>
        /// <param name="hisId"></param>
        /// <param name="errorNo"></param>
        /// <returns></returns>
        public static int UpHospitaSettlement4101(string hisId, string jsqd_lsh, string operatorId)
        {
           return platFormServer.ExecuteSql(string.Format($"update drjk_zyjs_output set jsqd_sclsh='{jsqd_lsh}',jsqd_scrq=GETDATE(),jsqd_scczy='{operatorId}' where zyh='{hisId}' and zt=1 "));
           
        }

        /// <summary>
        /// 医保目录下载解析压缩包存入数据库
        /// </summary>
        /// <param name="tbname">表名</param>
        /// <param name="path">文件路径</param>
        /// <returns></returns>
        /// 


        public static string Insert9102(string tbname, string path,int columnNum)
        {
            string results = "";
            try
            {

                using (FileStream fs = new FileStream(path, FileMode.Open))
                {
                    using (StreamReader sr = new StreamReader(fs))
                    {
                        while (!sr.EndOfStream)
                        {
                            string sLine = sr.ReadLine();
                            if (sLine.Length < 1)
                            {
                                continue;
                            }
                            string[] sRecordKbn = sLine.Split('\t');//过滤空格
                            var a = "";
                            for (int i = 0; i < columnNum; i++)
                            {

                                if (sRecordKbn[i].Contains("CST"))
                                {
                                    sRecordKbn[i] = DateTime.ParseExact(sRecordKbn[i], "ddd MMM dd HH:mm:ss CST yyyy", new CultureInfo("en-us")).ToString();
                                }
                                if (sRecordKbn[i].Contains("'"))
                                {
                                    sRecordKbn[i] = sRecordKbn[i].Replace("'", "’");
                                }
                                if (sRecordKbn[i] == null || sRecordKbn[i] == "null")
                                {
                                    sRecordKbn[i] = "";
                                }
                                else if (sRecordKbn[i].Contains("CST 999"))
                                {
                                    sRecordKbn[i] = DateTime.ParseExact("Thu Nov 30 00:00:00 CST 2006", "ddd MMM dd HH:mm:ss CST yyyy", new CultureInfo("en-us")).ToString();
                                }
                                a += sRecordKbn[i] + "\',\'";

                            }
                            a = "\'" + a.Substring(0, a.Length - 2);
                            //tbname = "G_yb_tcmherb_info_bV2";
                            //var sql = string.Format(@"insert into NewtouchHIS_Base.[dbo]." + tbname.Trim() + " values(" + a + ")");
                            try
                            {
                                results = platFormServer.ExecuteSql(@"insert into  NewtouchHIS_Base.[dbo]." + tbname.Trim() + " values(" + a + ")").ToString();
                            }
                            catch (Exception ex) {
                                AppLogger.Info("目录写入数据库失败:【" + tbname + "】-" + ex);
                                AppLogger.Info("错误的原数据是:【" + sLine + "】");
                                AppLogger.Info("错误的修改后的数据是:【" + a + "】");
                                continue;
                            }
                            
                            a = "";
                        }
                    }

                }
                results = "Success";
                return results;
            }
            catch (Exception ex)
            {
                AppLogger.Info("目录写入数据库失败:【"+ tbname + "】-" + ex);
                var errmsg = "目录写入数据库失败:【" + tbname + "】-" + ex;
                return errmsg;
            }
        }

        #region 3101
        public static DataTable DetailAuditData3101_fsi_order_dtos(string zyh, string orgId,string yzh,string txlx)
        {
            Parameters.Clear();
            Parameters.Add("@orgId", orgId);
            Parameters.Add("@zyh", zyh);
            Parameters.Add("@yzh", yzh);
            string tabName = "";
            if (txlx == "mz")
                tabName = "usp_Mz_DetailAudit_fsi_order_dtos3101";
            else
                tabName = "usp_Inp_DetailAudit_fsi_order_dtos3101";
            return platFormServer.RunProc_DataTable_WqServer(tabName, Parameters);
        }
        public static DataTable DetailAuditData3101_fsi_order_dtos(string zyh, string orgId, string yzh)
        {
            Parameters.Clear();
            Parameters.Add("@orgId", orgId);
            Parameters.Add("@zyh", zyh);
            Parameters.Add("@yzh", yzh);
            return platFormServer.RunProc_DataTable_WqServer("usp_Inp_DetailAudit_fsi_order_dtos3101", Parameters);
        }
        public static InputData3101 DetailAuditData3101(string zyh, string orgId)
        {

            //医嘱信息
            var yzsql = string.Format(@"select x.*,d.Name,s.Name ysghname into #yz from (
select '1' long_drord_flag,OrganizeId,zyh,WardCode,DeptCode,ysgh,xmdm,xmmc,yzzt,sl,yzlx,shsj,shr,kssj,zxsj,zxr,ypjl,ypgg,zxksdm,CreateTime,CreatorCode,hzxm,yzh,zzfbz,zt
 from Newtouch_CIS.dbo.zy_cqyz a with(nolock)
 union all
select '0' long_drord_flag,OrganizeId,zyh,WardCode,DeptCode,ysgh,xmdm,xmmc,yzzt,sl,yzlx,shsj,shr,kssj,zxsj,zxr,ypjl,ypgg,zxksdm,CreateTime,CreatorCode,hzxm,yzh,zzfbz,zt
 from Newtouch_CIS.dbo.zy_lsyz b with(nolock)) x
 left join NewtouchHIS_Base.dbo.Sys_Department d with(nolock)
 on x.DeptCode = d.Code and x.OrganizeId = d.OrganizeId and d.zt = '1'
 left join NewtouchHIS_Base.dbo.Sys_Staff s with(nolock)
 on x.ysgh = s.gh and x.OrganizeId = s.OrganizeId and s.zt = '1'
where x.OrganizeId = '" + orgId + "' and x.zt = '1' and x.zyh = '" + zyh + "'" +

" select z.*,d.Name zxksname, s.Name ysname  into #xm from (" +
"select OrganizeId OrganizeId2, zyh zyh2,sfxm xm, tdrq, ys, ks, cw, dj, sl sl2,jfdw,zfxz,zxks,yzxz,CreateTime CreateTime2, CreatorCode CreatorCode2,bq,zt zt2, dl" +
" from NewtouchHIS_Sett.dbo.zy_xmjfb a with(nolock) " +
" union all " +
"select OrganizeId  OrganizeId2,zyh zyh2, yp xm,tdrq,ys,ks,cw,dj,sl sl2, jfdw, zfxz, zxks, yzxz, CreateTime CreateTime2,CreatorCode CreatorCode2, bq, zt zt2,dl" +
       " from NewtouchHIS_Sett.dbo.zy_ypjfb b with(nolock))z" +
      " left join NewtouchHIS_Base.dbo.Sys_Department d with(nolock)" +
 " on z.zxks = d.Code and z.OrganizeId2 = d.Name and d.zt = '1'" +
 " left join NewtouchHIS_Base.dbo.Sys_Staff s with(nolock)" +
" on z.ys = s.gh and z.OrganizeId2 = s.OrganizeId and s.zt = '1'" +
" where z.OrganizeId2 = '" + orgId + "' and z.zt2 = '1' and z.zyh2 = '" + zyh + "'" +

" select yzh rx_id,yzh rxno, long_drord_flag,'1' hilist_type,'1' chrg_type,'1' drord_bhvr,'0' hilist_code,'默认' hilist_name," +
" '1' hilist_lv,'1' hilist_pric,'qhd' hosplist_code,'秦皇岛' hosplist_name,sl cnt, dj pric," +
"(select top 1 zje from NewtouchHIS_Sett.dbo.zy_js js with(nolock) where js.zyh = '" + zyh + "' and js.OrganizeId = '" + orgId + "' order by CreateTime desc) sumamt," +
"(case when zfxz = '6' then sl*dj else sl* dj end) ownpay_amt,(case when zfxz = '6' then sl*dj else sl* dj end) selfpay_amt,isnull(ypgg, '0') spec," +
"jfdw spec_unt, isnull(CONVERT(varchar(100), kssj, 120), CONVERT(varchar(100), tdrq, 120)) drord_begn_date,DeptCode drord_dept_codg, Name drord_dept_name,ysgh drord_dr_codg, ysghname drord_dr_name," +
"'1' drord_dr_profttl,'0' curr_drord_flag" +
" from(" +
"select *, row_number()over(partition by a.yzh, a.xmdm  order by a.CreateTime) px  from #yz a" +
" left join #xm b " +
"on a.zyh = b.zyh2 and a.xmdm = b.xm" +
")c" +
" where c.px = 1 " +

"drop table #yz" +
" drop table #xm");
            DataSet ds = platFormServer.Query(yzsql);
            InputFOD3101 fod = new InputFOD3101();
            List<InputFOD3101> listpod = new List<InputFOD3101>();
            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    fod.rx_id = Convert.ToString(ds.Tables[0].Rows[i]["rx_id"]);
                    fod.rxno = Convert.ToString(ds.Tables[0].Rows[i]["rxno"]);
                    fod.long_drord_flag = Convert.ToString(ds.Tables[0].Rows[i]["long_drord_flag"]);
                    fod.hilist_type = Convert.ToString(ds.Tables[0].Rows[i]["hilist_type"]);
                    fod.chrg_type = Convert.ToString(ds.Tables[0].Rows[i]["chrg_type"]);
                    fod.drord_bhvr = Convert.ToString(ds.Tables[0].Rows[i]["drord_bhvr"]);
                    fod.hilist_code = Convert.ToString(ds.Tables[0].Rows[i]["hilist_code"]);
                    fod.hilist_name = Convert.ToString(ds.Tables[0].Rows[i]["hilist_name"]);
                    fod.hilist_lv = Convert.ToString(ds.Tables[0].Rows[i]["hilist_lv"]);
                    fod.hilist_pric = Convert.ToString(ds.Tables[0].Rows[i]["hilist_pric"]);
                    fod.hosplist_code = Convert.ToString(ds.Tables[0].Rows[i]["hosplist_code"]);
                    fod.hosplist_name = Convert.ToString(ds.Tables[0].Rows[i]["hosplist_name"]);
                    fod.cnt = Convert.ToString(ds.Tables[0].Rows[i]["cnt"]);
                    fod.pric = Convert.ToString(ds.Tables[0].Rows[i]["pric"]);
                    fod.sumamt = Convert.ToString(ds.Tables[0].Rows[i]["sumamt"]);
                    fod.ownpay_amt = Convert.ToString(ds.Tables[0].Rows[i]["ownpay_amt"]);
                    fod.selfpay_amt = Convert.ToString(ds.Tables[0].Rows[i]["selfpay_amt"]);
                    fod.spec = Convert.ToString(ds.Tables[0].Rows[i]["spec"]);
                    fod.spec_unt = Convert.ToString(ds.Tables[0].Rows[i]["spec_unt"]);
                    fod.drord_begn_date = Convert.ToString(ds.Tables[0].Rows[i]["drord_begn_date"]);
                    fod.drord_dept_codg = Convert.ToString(ds.Tables[0].Rows[i]["drord_dept_codg"]);
                    fod.drord_dept_name = Convert.ToString(ds.Tables[0].Rows[i]["drord_dept_name"]);
                    fod.drord_dr_codg = Convert.ToString(ds.Tables[0].Rows[i]["drord_dr_codg"]);
                    fod.drord_dr_name = Convert.ToString(ds.Tables[0].Rows[i]["drord_dr_name"]);
                    fod.drord_dr_profttl = Convert.ToString(ds.Tables[0].Rows[i]["drord_dr_profttl"]);
                    fod.curr_drord_flag = Convert.ToString(ds.Tables[0].Rows[i]["curr_drord_flag"]);
                    listpod.Add(fod);
                }

            }

            //诊断信息
            var zdsql = string.Format(@"select (case when ZDOrder='1' then JBDM end) dise_id,'1' inout_dise_type,
(case when ZDOrder='1' then JBDM end) maindise_flag,'1' dias_srt_no,JBDM dise_codg,JBMC dise_name,CreateTime dise_date
 from Newtouch_EMR.[dbo].[mr_basy_zd] a with(nolock)
 where OrganizeId='" + orgId + "' and zt='1' and zyh='" + zyh + "'  and ZDOrder='1' " +
" order by ZDOrder");
            DataSet zdds = platFormServer.Query(zdsql);
            List<InputFDD3101> zdlist = new List<InputFDD3101>();
            InputFDD3101 fdd = new InputFDD3101();
            if (zdds.Tables[0].Rows.Count > 0)
            {
                for (int j = 0; j < zdds.Tables[0].Rows.Count; j++)
                {
                    fdd.dise_id = Convert.ToString(zdds.Tables[0].Rows[j]["dise_id"]);
                    fdd.inout_dise_type = Convert.ToString(zdds.Tables[0].Rows[j]["inout_dise_type"]);
                    fdd.maindise_flag = Convert.ToString(zdds.Tables[0].Rows[j]["maindise_flag"]);
                    fdd.dias_srt_no = Convert.ToString(zdds.Tables[0].Rows[j]["dias_srt_no"]);
                    fdd.dise_codg = Convert.ToString(zdds.Tables[0].Rows[j]["dise_codg"]);
                    fdd.dise_name = Convert.ToString(zdds.Tables[0].Rows[j]["dise_name"]);
                    fdd.dise_date = Convert.ToString(zdds.Tables[0].Rows[j]["dise_date"]);
                    zdlist.Add(fdd);
                }
            }

            //就诊信息
            var jzsql = string.Format(@"select xm,zyh,sum(zfje) zfje,zje  into #js from (
select xm, zyh, sum(zfje) zfje, zje from(
select  '项目' xm, a.zyh, b.jyje zfje, a.zje from NewtouchHIS_Sett.dbo.zy_js a with(nolock)
left join NewtouchHIS_Sett.dbo.zy_jsmx b with(nolock)
on a.jsnm = b.jsnm and a.OrganizeId = b.OrganizeId and b.zt = '1'
left join NewtouchHIS_Sett.dbo.zy_xmjfb c with(nolock)
on b.xmjfbbh = c.jfbbh and b.OrganizeId = c.OrganizeId and c.zt = '1'
where a.OrganizeId = '" + orgId + "' and a.zt = '1'  and b.xmjfbbh is not null" +
" and c.zfxz = '1') z group by xm, zyh, zje" +
" union all" +
" select xm, zyh, zfje, zje from(" +
" select '药品' xm, a.zyh, sum(b.jyje)zfje, a.zje  from NewtouchHIS_Sett.dbo.zy_js a with(nolock)" +
" left join NewtouchHIS_Sett.dbo.zy_jsmx b with(nolock)" +
" on a.jsnm = b.jsnm and a.OrganizeId = b.OrganizeId and b.zt = '1'" +
" left join NewtouchHIS_Sett.dbo.zy_ypjfb c with(nolock)" +
" on b.ypjfbbh = c.jfbbh and b.OrganizeId = c.OrganizeId and c.zt = '1'" +
" where a.OrganizeId = '" + orgId + "' and a.zt = '1' and b.ypjfbbh is not null" +
" and c.zfxz = '6'" +
" group by a.zyh, b.jyje, a.zje) x" +
")s" +
" group by xm, zyh, zje " +

"select jz.mzh mdtrt_id, '1' medins_type,'1' medins_lv,CONVERT(varchar(30), a.ryrq, 120) adm_date,CONVERT(varchar(30), a.cqrq, 120) dscg_date,zddm dscg_main_dise_codg,zdmc dscg_main_dise_name,ysgh dr_codg,c.Code adm_dept_codg," +
" c.Name adm_dept_name,c.Code dscg_dept_codg,c.Name dscg_dept_name,brxzdm med_mdtrt_type,'21' med_type," +
" '0' matn_stas,b.zje medfee_sumamt,b.zfje ownpay_amt,b.zfje selfpay_amt,'1' setl_totlnum,'390' insutype,'0' reim_flag,'0' out_setl_flag" +
" from Newtouch_CIS.dbo.zy_brxxk a with(nolock)" +
" left join #js b" +
" on a.zyh=b.zyh" +
" left join  NewtouchHIS_Base.dbo.V_S_Sys_Department c with(nolock)" +
" on a.DeptCode=c.Code and a.OrganizeId=c.OrganizeId and c.zt='1'" +
" left join NewtouchHIS_Sett.dbo.zy_brjbxx br with(nolock)" +
" on a.zyh=br.zyh and a.OrganizeId=br.OrganizeId and br.zt='1'" +
" left join Newtouch_CIS.dbo.xt_jz jz with(nolock)" +
" on br.blh=jz.blh and a.OrganizeId=jz.OrganizeId and jz.zt='1'" +
" where a.OrganizeId='" + orgId + "' and a.zyh='" + zyh + "'" +

" drop table #js");
            DataSet jzds = platFormServer.Query(jzsql);
            List<InputFED3101> jzlist = new List<InputFED3101>();
            InputFED3101 fed = new InputFED3101();
            if (jzds.Tables[0].Rows.Count > 0)
            {
                for (int k = 0; k < jzds.Tables[0].Rows.Count; k++)
                {
                    fed.mdtrt_id = Convert.ToString(jzds.Tables[0].Rows[k]["mdtrt_id"]);
                    fed.medins_id = ConfigurationManager.AppSettings["fixmedins_code"];
                    fed.medins_name = ConfigurationManager.AppSettings["fixmedins_name"];
                    fed.medins_admdvs = ConfigurationManager.AppSettings["mdtrtarea_admvs"];
                    fed.medins_type = Convert.ToString(jzds.Tables[0].Rows[k]["medins_type"]);
                    fed.medins_lv = Convert.ToString(jzds.Tables[0].Rows[k]["medins_lv"]);
                    //fed.wardarea_codg = Convert.ToString(jzds.Tables[0].Rows[k]["wardarea_codg"]);
                    //fed.wardno = Convert.ToString(jzds.Tables[0].Rows[k]["wardno"]);
                    //fed.bedno = Convert.ToString(jzds.Tables[0].Rows[k]["bedno"]);
                    fed.adm_date = Convert.ToString(jzds.Tables[0].Rows[k]["adm_date"]);
                    fed.dscg_date = Convert.ToString(jzds.Tables[0].Rows[k]["dscg_date"]);
                    fed.dscg_main_dise_codg = Convert.ToString(jzds.Tables[0].Rows[k]["dscg_main_dise_codg"]);
                    fed.dscg_main_dise_name = Convert.ToString(jzds.Tables[0].Rows[k]["dscg_main_dise_name"]);

                    fed.dr_codg = Convert.ToString(jzds.Tables[0].Rows[k]["dr_codg"]);
                    fed.adm_dept_codg = Convert.ToString(jzds.Tables[0].Rows[k]["adm_dept_codg"]);
                    fed.adm_dept_name = Convert.ToString(jzds.Tables[0].Rows[k]["adm_dept_name"]);
                    fed.dscg_dept_codg = Convert.ToString(jzds.Tables[0].Rows[k]["dscg_dept_codg"]);
                    fed.dscg_dept_name = Convert.ToString(jzds.Tables[0].Rows[k]["dscg_dept_name"]);
                    fed.med_mdtrt_type = Convert.ToString(jzds.Tables[0].Rows[k]["med_mdtrt_type"]);

                    fed.matn_stas = Convert.ToString(jzds.Tables[0].Rows[k]["matn_stas"]);
                    fed.medfee_sumamt = Convert.ToString(jzds.Tables[0].Rows[k]["medfee_sumamt"]);
                    fed.ownpay_amt = Convert.ToString(jzds.Tables[0].Rows[k]["ownpay_amt"]);
                    fed.selfpay_amt = Convert.ToString(jzds.Tables[0].Rows[k]["selfpay_amt"]);
                    //fed.acct_payamt = Convert.ToString(jzds.Tables[0].Rows[k]["acct_payamt"]);
                    //fed.ma_amt = Convert.ToString(jzds.Tables[0].Rows[k]["ma_amt"]);
                    //fed.hifp_payamt = Convert.ToString(jzds.Tables[0].Rows[k]["hifp_payamt"]);
                    fed.setl_totlnum = Convert.ToString(jzds.Tables[0].Rows[k]["setl_totlnum"]);
                    fed.insutype = Convert.ToString(jzds.Tables[0].Rows[k]["insutype"]);
                    fed.reim_flag = Convert.ToString(jzds.Tables[0].Rows[k]["reim_flag"]);
                    fed.out_setl_flag = Convert.ToString(jzds.Tables[0].Rows[k]["out_setl_flag"]);
                    //fed.fsi_operation_dtos = Convert.ToString(jzds.Tables[0].Rows[k]["fsi_operation_dtos"]);
                    jzlist.Add(fed);
                }
                fed.fsi_diagnose_dtos = zdlist;
                fed.fsi_order_dtos = listpod;
            }

            //参保人信息
            var cbrsql = string.Format(@"select patid patn_id,xm patn_name,xb gend,CONVERT(varchar(100),csny, 23) brdy,zyh curr_mdtrt_id from NewtouchHIS_Sett.dbo.zy_brjbxx with(nolock) where 
  zyh='" + zyh + "' and OrganizeId='" + orgId + "' and zt='1'");

            DataSet cbrds = platFormServer.Query(cbrsql);
            List<InputPD3101> cbrlist = new List<InputPD3101>();
            InputPD3101 pd = new InputPD3101();
            if (cbrds.Tables[0].Rows.Count > 0)
            {
                for (int c = 0; c < cbrds.Tables[0].Rows.Count; c++)
                {
                    pd.patn_id = Convert.ToString(cbrds.Tables[0].Rows[c]["patn_id"]);
                    pd.patn_name = Convert.ToString(cbrds.Tables[0].Rows[c]["patn_name"]);
                    pd.gend = Convert.ToString(cbrds.Tables[0].Rows[c]["gend"]);
                    pd.brdy = Convert.ToString(cbrds.Tables[0].Rows[c]["brdy"]);
                    pd.poolarea = ConfigurationManager.AppSettings["mdtrtarea_admvs"];
                    pd.curr_mdtrt_id = Convert.ToString(cbrds.Tables[0].Rows[c]["curr_mdtrt_id"]);

                    cbrlist.Add(pd);
                }
                pd.fsi_encounter_dtos = jzlist;
            }

            InputData3101 datas = new InputData3101();
            //datas.syscode = ConfigurationManager.AppSettings["fixmedins_code"];
            datas.patient_dtos = cbrlist;
            datas.trig_scen = "1";

            return datas;


        }
        public static int Inser3101(InputPD3101 inputPD3101, string czydm, string zyh)
        {
            string sql = @"INSERT INTO [dbo].[Drjk_sqsh_input]
           ([patn_id]
           ,[patn_name]
           ,[gend]
           ,[brdy]
           ,[poolarea]
           ,[curr_mdtrt_id]
           ,[fsi_encounter_dtos]
           ,[fsi_his_data_dto]
           ,[czrq]
           ,[czydm]
           ,[zyh]
           ,[zt]
           ,[zt_czy]
           ,[zt_rq])
     VALUES('" + inputPD3101.patn_id + "','" + inputPD3101.patn_name + "','" + inputPD3101.gend + "','" + inputPD3101.brdy + "','" + inputPD3101.poolarea + "','" + inputPD3101.curr_mdtrt_id + "','','','" + ClassSqlHelper.GetServerTime() + "','" + czydm + "','" + zyh + "','1',null,null)";
            return platFormServer.ExecuteSql(sql);
        }
        public static string InserOutput3101(Outputrs3101 Output3101, string czydm, string zyh,string issqsz)
        {
            try
            {

            string sql = @"INSERT INTO [dbo].[Drjk_sqsh_Output]
           ([jr_id]
           ,[rule_id]
           ,[rule_name]
           ,[vola_cont]
           ,[patn_id]
           ,[mdtrt_id]
           ,[judge_result_detail_dtos]
           ,[vola_amt]
           ,[vola_amt_stas]
           ,[sev_deg]
           ,[vola_evid]
           ,[vola_bhvr_type]
           ,[task_id]
           ,[issqsz]
           ,[czrq]
           ,[czydm]
           ,[zyh]
           ,[zt]
           ,[zt_czy]
           ,[zt_rq])
     VALUES('" + Output3101.jr_id 
     + "','" + Output3101.rule_id
     + "','" + Output3101.rule_name
     + "','" + Output3101.jr_id
     + "','" + Output3101.rule_id
     + "','" + Output3101.rule_name
     + "','" + Output3101.vola_cont
     + "','" + Output3101.patn_id
     + "','" + Output3101.mdtrt_id
     + "','" + ""
     + "','" + Output3101.vola_amt
     + "','" + Output3101.vola_amt_stas
     + "','" + Output3101.sev_deg
     + "','" + Output3101.vola_evid
     + "','" + Output3101.vola_bhvr_type
     + "','" + Output3101.task_id
     + "','"+ issqsz + "','" + ClassSqlHelper.GetServerTime() 
     + "','" + czydm 
     + "','" + zyh 
     + "','1',null,null)";
                if (platFormServer.ExecuteSql(sql)<1)
                {
                    return "插入Drjk_sqsh_Output表失败";
                }
            if (Output3101.judge_result_detail_dtos!=null)
            {
                foreach (var item in Output3101.judge_result_detail_dtos)
                {
                    string sqlmx = @"INSERT INTO [dbo].[Drjk_sqsh_OutputMX]
           ([jrd_id]
           ,[patn_id]
           ,[mdtrt_id]
           ,[rx_id]
           ,[vola_item_type]
           ,[vola_amt]
           ,[czrq]
           ,[czydm]
           ,[zyh]
           ,[zt]
           ,[zt_czy]
           ,[zt_rq])
     VALUES('" 
                        + "','" + item.jrd_id
                        + "','" + item.patn_id
                        + "','" + item.mdtrt_id
                        + "','" + item.rx_id
                        + "','" + item.vola_item_type
                        + "','" + item.vola_amt
                        + "','" + ClassSqlHelper.GetServerTime()
                        + "','" + czydm
                        + "','" + zyh
                        + "','1',null,null)";
                        if (platFormServer.ExecuteSql(sqlmx) < 1)
                        {
                            return "插入Drjk_sqsh_OutputMX表失败";
                        }
                    }
            }
            }
            catch (Exception ex)
            {
                AppLogger.Info("目录写入数据库失败:【" + ex.Message + "】" );
                return "插入Drjk_sqsh_OutputMX表失败" + ex.Message;
            }
            return "";
        }
        #endregion

        #region 3102 事前事中提醒
        public static DataTable DetailAuditData3102_patient_dtos(string zyh, string orgId,string txlx)
        {
            Parameters.Clear();
            Parameters.Add("@orgId", orgId);
            Parameters.Add("@zyh", zyh);
            string tabName = "";
            if (txlx == "mz")
                tabName = "usp_Mz_DetailAudit_patient_dtos";
            else
                tabName = "usp_Inp_DetailAudit_patient_dtos";
            DataTable dt= platFormServer.RunProc_DataTable_WqServer(tabName, Parameters);
            return dt;
        }
        public static DataTable DetailAuditData3102_fsi_diagnose_dtos(string zyh, string orgId,string txlx)
        {
            Parameters.Clear();
            Parameters.Add("@orgId", orgId);
            Parameters.Add("@zyh", zyh);
            string tabName = "";
            if (txlx == "mz")
                tabName = "usp_Mz_DetailAudit_fsi_diagnose_dtos";
            else
                tabName = "usp_Inp_DetailAudit_fsi_diagnose_dtos";
            return platFormServer.RunProc_DataTable_WqServer(tabName, Parameters);
        }
        public static DataTable DetailAuditData3102_fsi_diagnose_dtos(string zyh, string orgId)
        {
            Parameters.Clear();
            Parameters.Add("@orgId", orgId);
            Parameters.Add("@zyh", zyh);
            return platFormServer.RunProc_DataTable_WqServer("usp_Inp_DetailAudit_fsi_diagnose_dtos", Parameters);
        }
        public static DataTable DetailAuditData3102_fsi_order_dtos(string zyh, string orgId)
        {
            Parameters.Clear();
            Parameters.Add("@orgId", orgId);
            Parameters.Add("@zyh", zyh);
            return platFormServer.RunProc_DataTable_WqServer("usp_Inp_DetailAudit_fsi_order_dtos", Parameters);
        }
        public static DataTable DetailAuditData3102_fsi_encounter_dtos(string zyh, string orgId,string ddyyid, string ddyymc, string insuplc_admdvs,string txlx)
        {
            Parameters.Clear();
            Parameters.Add("@orgId", orgId);
            Parameters.Add("@zyh", zyh);
            Parameters.Add("@ddyyid", ddyyid);
            Parameters.Add("@ddyymc", ddyymc);
            Parameters.Add("@insuplc_admdvs", insuplc_admdvs);
            string tabName = "";
            if (txlx == "mz")
                tabName = "usp_Mz_DetailAudit_fsi_encounter_dtos";
            else
                tabName = "usp_Inp_DetailAudit_fsi_encounter_dtos";
            return platFormServer.RunProc_DataTable_WqServer(tabName, Parameters);
        }
        public static int Inser3102(InputPD3102 inputPD3102,string czydm,string zyh )
        {
            string sql = @"INSERT INTO [dbo].[Drjk_szsh_input]
           ([patn_id]
           ,[patn_name]
           ,[gend]
           ,[brdy]
           ,[poolarea]
           ,[curr_mdtrt_id]
           ,[fsi_encounter_dtos]
           ,[fsi_his_data_dto]
           ,[czrq]
           ,[czydm]
           ,[zyh]
           ,[zt]
           ,[zt_czy]
           ,[zt_rq])
     VALUES('" + inputPD3102.patn_id + "','" + inputPD3102.patn_name + "','" + inputPD3102.gend + "','" + inputPD3102.brdy + "','" + inputPD3102.poolarea + "','" + inputPD3102.curr_mdtrt_id + "','','','" + ClassSqlHelper.GetServerTime() + "','" + czydm + "','" + zyh + "','1',null,null)";
            return platFormServer.ExecuteSql(sql);
        }
        public static InputData3102 DetailAuditData3102(string zyh, string orgId)
        {
            //医嘱信息
            var yzsql = string.Format(@"select x.*,d.Name,s.Name ysghname into #yz from (
select '1' long_drord_flag,OrganizeId,zyh,WardCode,DeptCode,ysgh,xmdm,xmmc,yzzt,sl,yzlx,shsj,shr,kssj,zxsj,zxr,ypjl,ypgg,zxksdm,CreateTime,CreatorCode,hzxm,yzh,zzfbz,zt
 from Newtouch_CIS.dbo.zy_cqyz a with(nolock)
 union all
select '0' long_drord_flag,OrganizeId,zyh,WardCode,DeptCode,ysgh,xmdm,xmmc,yzzt,sl,yzlx,shsj,shr,kssj,zxsj,zxr,ypjl,ypgg,zxksdm,CreateTime,CreatorCode,hzxm,yzh,zzfbz,zt
 from Newtouch_CIS.dbo.zy_lsyz b with(nolock)) x
 left join NewtouchHIS_Base.dbo.Sys_Department d with(nolock)
 on x.DeptCode = d.Code and x.OrganizeId = d.OrganizeId and d.zt = '1'
 left join NewtouchHIS_Base.dbo.Sys_Staff s with(nolock)
 on x.ysgh = s.gh and x.OrganizeId = s.OrganizeId and s.zt = '1'
where x.OrganizeId = '" + orgId + "' and x.zt = '1' and x.zyh = '" + zyh + "'" +

" select z.*,d.Name zxksname, s.Name ysname  into #xm from (" +
"select OrganizeId OrganizeId2, zyh zyh2,sfxm xm, tdrq, ys, ks, cw, dj, sl sl2,jfdw,zfxz,zxks,yzxz,CreateTime CreateTime2, CreatorCode CreatorCode2,bq,zt zt2, dl" +
" from NewtouchHIS_Sett.dbo.zy_xmjfb a with(nolock) " +
" union all " +
"select OrganizeId  OrganizeId2,zyh zyh2, yp xm,tdrq,ys,ks,cw,dj,sl sl2, jfdw, zfxz, zxks, yzxz, CreateTime CreateTime2,CreatorCode CreatorCode2, bq, zt zt2,dl" +
       " from NewtouchHIS_Sett.dbo.zy_ypjfb b with(nolock))z" +
      " left join NewtouchHIS_Base.dbo.Sys_Department d with(nolock)" +
 " on z.zxks = d.Code and z.OrganizeId2 = d.Name and d.zt = '1'" +
 " left join NewtouchHIS_Base.dbo.Sys_Staff s with(nolock)" +
" on z.ys = s.gh and z.OrganizeId2 = s.OrganizeId and s.zt = '1'" +
" where z.OrganizeId2 = '" + orgId + "' and z.zt2 = '1' and z.zyh2 = '" + zyh + "'" +

" select yzh rx_id,yzh rxno, long_drord_flag,'1' hilist_type,'1' chrg_type,'1' drord_bhvr,'0' hilist_code,'默认' hilist_name," +
" '1' hilist_lv,'1' hilist_pric,'qhd' hosplist_code,'秦皇岛' hosplist_name,sl cnt, dj pric," +
"(select top 1 zje from NewtouchHIS_Sett.dbo.zy_js js with(nolock) where js.zyh = '" + zyh + "' and js.OrganizeId = '" + orgId + "' order by CreateTime desc) sumamt," +
"(case when zfxz = '6' then sl*dj else sl* dj end) ownpay_amt,(case when zfxz = '6' then sl*dj else sl* dj end) selfpay_amt,isnull(ypgg, '0') spec," +
"jfdw spec_unt, isnull(CONVERT(varchar(100), kssj, 120), CONVERT(varchar(100), tdrq, 120)) drord_begn_date,DeptCode drord_dept_codg, Name drord_dept_name,ysgh drord_dr_codg, ysghname drord_dr_name," +
"'1' drord_dr_profttl,'0' curr_drord_flag" +
" from(" +
"select *, row_number()over(partition by a.yzh, a.xmdm  order by a.CreateTime) px  from #yz a" +
" left join #xm b " +
"on a.zyh = b.zyh2 and a.xmdm = b.xm" +
")c" +
" where c.px = 1 " +

"drop table #yz" +
" drop table #xm");
            DataSet ds = platFormServer.Query(yzsql);
            InputFOD3102 fod = new InputFOD3102();
            List<InputFOD3102> listpod = new List<InputFOD3102>();
            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    fod.rx_id = Convert.ToString(ds.Tables[0].Rows[i]["rx_id"]);
                    fod.rxno = Convert.ToString(ds.Tables[0].Rows[i]["rxno"]);
                    fod.long_drord_flag = Convert.ToString(ds.Tables[0].Rows[i]["long_drord_flag"]);
                    fod.hilist_type = Convert.ToString(ds.Tables[0].Rows[i]["hilist_type"]);
                    fod.chrg_type = Convert.ToString(ds.Tables[0].Rows[i]["chrg_type"]);
                    fod.drord_bhvr = Convert.ToString(ds.Tables[0].Rows[i]["drord_bhvr"]);
                    fod.hilist_code = Convert.ToString(ds.Tables[0].Rows[i]["hilist_code"]);
                    fod.hilist_name = Convert.ToString(ds.Tables[0].Rows[i]["hilist_name"]);
                    fod.hilist_lv = Convert.ToString(ds.Tables[0].Rows[i]["hilist_lv"]);
                    fod.hilist_pric = Convert.ToString(ds.Tables[0].Rows[i]["hilist_pric"]);
                    fod.hosplist_code = Convert.ToString(ds.Tables[0].Rows[i]["hosplist_code"]);
                    fod.hosplist_name = Convert.ToString(ds.Tables[0].Rows[i]["hosplist_name"]);
                    fod.cnt = Convert.ToString(ds.Tables[0].Rows[i]["cnt"]);
                    fod.pric = Convert.ToString(ds.Tables[0].Rows[i]["pric"]);
                    fod.sumamt = Convert.ToString(ds.Tables[0].Rows[i]["sumamt"]);
                    fod.ownpay_amt = Convert.ToString(ds.Tables[0].Rows[i]["ownpay_amt"]);
                    fod.selfpay_amt = Convert.ToString(ds.Tables[0].Rows[i]["selfpay_amt"]);
                    fod.spec = Convert.ToString(ds.Tables[0].Rows[i]["spec"]);
                    fod.spec_unt = Convert.ToString(ds.Tables[0].Rows[i]["spec_unt"]);
                    fod.drord_begn_date = Convert.ToString(ds.Tables[0].Rows[i]["drord_begn_date"]);
                    fod.drord_dept_codg = Convert.ToString(ds.Tables[0].Rows[i]["drord_dept_codg"]);
                    fod.drord_dept_name = Convert.ToString(ds.Tables[0].Rows[i]["drord_dept_name"]);
                    fod.drord_dr_codg = Convert.ToString(ds.Tables[0].Rows[i]["drord_dr_codg"]);
                    fod.drord_dr_name = Convert.ToString(ds.Tables[0].Rows[i]["drord_dr_name"]);
                    fod.drord_dr_profttl = Convert.ToString(ds.Tables[0].Rows[i]["drord_dr_profttl"]);
                    fod.curr_drord_flag = Convert.ToString(ds.Tables[0].Rows[i]["curr_drord_flag"]);
                    listpod.Add(fod);
                }

            }

            //诊断信息
            var zdsql = string.Format(@"select (case when ZDOrder='1' then JBDM end) dise_id,'1' inout_dise_type,
(case when ZDOrder='1' then JBDM end) maindise_flag,'1' dias_srt_no,JBDM,JBMC,CreateTime
 from Newtouch_EMR.[dbo].[mr_basy_zd] a with(nolock)
 where OrganizeId='" + orgId + "' and zt='1' and zyh='" + zyh + "'  and ZDOrder='1' " +
" order by ZDOrder");
            DataSet zdds = platFormServer.Query(zdsql);
            List<InputFDD3102> zdlist = new List<InputFDD3102>();
            InputFDD3102 fdd = new InputFDD3102();
            if (zdds.Tables[0].Rows.Count > 0)
            {
                for (int j = 0; j < zdds.Tables[0].Rows.Count; j++)
                {
                    fdd.dise_id = Convert.ToString(zdds.Tables[0].Rows[j]["dise_id"]);
                    fdd.inout_dise_type = Convert.ToString(zdds.Tables[0].Rows[j]["inout_dise_type"]);
                    fdd.maindise_flag = Convert.ToString(zdds.Tables[0].Rows[j]["maindise_flag"]);
                    fdd.dias_srt_no = Convert.ToString(zdds.Tables[0].Rows[j]["dias_srt_no"]);
                    fdd.dise_codg = Convert.ToString(zdds.Tables[0].Rows[j]["dise_codg"]);
                    fdd.dise_name = Convert.ToString(zdds.Tables[0].Rows[j]["dise_name"]);
                    fdd.dise_date = Convert.ToString(zdds.Tables[0].Rows[j]["dise_date"]);
                    zdlist.Add(fdd);
                }
            }

            //就诊信息
            var jzsql = string.Format(@"select xm,zyh,sum(zfje) zfje,zje  into #js from (
select xm, zyh, sum(zfje) zfje, zje from(
select  '项目' xm, a.zyh, b.jyje zfje, a.zje from NewtouchHIS_Sett.dbo.zy_js a with(nolock)
left join NewtouchHIS_Sett.dbo.zy_jsmx b with(nolock)
on a.jsnm = b.jsnm and a.OrganizeId = b.OrganizeId and b.zt = '1'
left join NewtouchHIS_Sett.dbo.zy_xmjfb c with(nolock)
on b.xmjfbbh = c.jfbbh and b.OrganizeId = c.OrganizeId and c.zt = '1'
where a.OrganizeId = '" + orgId + "' and a.zt = '1'  and b.xmjfbbh is not null" +
" and c.zfxz = '1') z group by xm, zyh, zje" +
" union all" +
" select xm, zyh, zfje, zje from(" +
" select '药品' xm, a.zyh, sum(b.jyje)zfje, a.zje  from NewtouchHIS_Sett.dbo.zy_js a with(nolock)" +
" left join NewtouchHIS_Sett.dbo.zy_jsmx b with(nolock)" +
" on a.jsnm = b.jsnm and a.OrganizeId = b.OrganizeId and b.zt = '1'" +
" left join NewtouchHIS_Sett.dbo.zy_ypjfb c with(nolock)" +
" on b.ypjfbbh = c.jfbbh and b.OrganizeId = c.OrganizeId and c.zt = '1'" +
" where a.OrganizeId = '" + orgId + "' and a.zt = '1' and b.ypjfbbh is not null" +
" and c.zfxz = '6'" +
" group by a.zyh, b.jyje, a.zje) x" +
")s" +
" group by xm, zyh, zje " +

"select jz.mzh mdtrt_id, '1' medins_type,'1' medins_lv,CONVERT(varchar(30), a.ryrq, 120) adm_date,CONVERT(varchar(30), a.cqrq, 120) dscg_date,zddm dscg_main_dise_codg,zdmc dscg_main_dise_name,ysgh dr_codg,c.Code adm_dept_codg," +
" c.Name adm_dept_name,c.Code dscg_dept_codg,c.Name dscg_dept_name,brxzdm med_mdtrt_type,'21' med_type," +
" '0' matn_stas,b.zje medfee_sumamt,b.zfje ownpay_amt,b.zfje selfpay_amt,'1' setl_totlnum,'390' insutype,'0' reim_flag,'0' out_setl_flag" +
" from Newtouch_CIS.dbo.zy_brxxk a with(nolock)" +
" left join #js b" +
" on a.zyh=b.zyh" +
" left join  NewtouchHIS_Base.dbo.V_S_Sys_Department c with(nolock)" +
" on a.DeptCode=c.Code and a.OrganizeId=c.OrganizeId and c.zt='1'" +
" left join NewtouchHIS_Sett.dbo.zy_brjbxx br with(nolock)" +
" on a.zyh=br.zyh and a.OrganizeId=br.OrganizeId and br.zt='1'" +
" left join Newtouch_CIS.dbo.xt_jz jz with(nolock)" +
" on br.blh=jz.blh and a.OrganizeId=jz.OrganizeId and jz.zt='1'" +
" where a.OrganizeId='" + orgId + "' and a.zyh='" + zyh + "'" +

" drop table #js");
            DataSet jzds = platFormServer.Query(jzsql);
            List<InputFED3102> jzlist = new List<InputFED3102>();
            InputFED3102 fed = new InputFED3102();
            if (jzds.Tables[0].Rows.Count > 0)
            {
                for (int k = 0; k < jzds.Tables[0].Rows.Count; k++)
                {
                    fed.mdtrt_id = Convert.ToString(jzds.Tables[0].Rows[k]["mdtrt_id"]);
                    fed.medins_id = ConfigurationManager.AppSettings["fixmedins_code"];
                    fed.medins_name = ConfigurationManager.AppSettings["fixmedins_name"];
                    fed.medins_admdvs = ConfigurationManager.AppSettings["mdtrtarea_admvs"];
                    fed.medins_type = Convert.ToString(jzds.Tables[0].Rows[k]["medins_type"]);
                    fed.medins_lv = Convert.ToString(jzds.Tables[0].Rows[k]["medins_lv"]);
                    //fed.wardarea_codg = Convert.ToString(jzds.Tables[0].Rows[k]["wardarea_codg"]);
                    //fed.wardno = Convert.ToString(jzds.Tables[0].Rows[k]["wardno"]);
                    //fed.bedno = Convert.ToString(jzds.Tables[0].Rows[k]["bedno"]);
                    fed.adm_date = Convert.ToString(jzds.Tables[0].Rows[k]["adm_date"]);
                    fed.dscg_date = Convert.ToString(jzds.Tables[0].Rows[k]["dscg_date"]);
                    fed.dscg_main_dise_codg = Convert.ToString(jzds.Tables[0].Rows[k]["dscg_main_dise_codg"]);
                    fed.dscg_main_dise_name = Convert.ToString(jzds.Tables[0].Rows[k]["dscg_main_dise_name"]);

                    fed.dr_codg = Convert.ToString(jzds.Tables[0].Rows[k]["dr_codg"]);
                    fed.adm_dept_codg = Convert.ToString(jzds.Tables[0].Rows[k]["adm_dept_codg"]);
                    fed.adm_dept_name = Convert.ToString(jzds.Tables[0].Rows[k]["adm_dept_name"]);
                    fed.dscg_dept_codg = Convert.ToString(jzds.Tables[0].Rows[k]["dscg_dept_codg"]);
                    fed.dscg_dept_name = Convert.ToString(jzds.Tables[0].Rows[k]["dscg_dept_name"]);
                    fed.med_mdtrt_type = Convert.ToString(jzds.Tables[0].Rows[k]["med_mdtrt_type"]);

                    fed.matn_stas = Convert.ToString(jzds.Tables[0].Rows[k]["matn_stas"]);
                    fed.medfee_sumamt = Convert.ToString(jzds.Tables[0].Rows[k]["medfee_sumamt"]);
                    fed.ownpay_amt = Convert.ToString(jzds.Tables[0].Rows[k]["ownpay_amt"]);
                    fed.selfpay_amt = Convert.ToString(jzds.Tables[0].Rows[k]["selfpay_amt"]);
                    //fed.acct_payamt = Convert.ToString(jzds.Tables[0].Rows[k]["acct_payamt"]);
                    //fed.ma_amt = Convert.ToString(jzds.Tables[0].Rows[k]["ma_amt"]);
                    //fed.hifp_payamt = Convert.ToString(jzds.Tables[0].Rows[k]["hifp_payamt"]);
                    fed.setl_totlnum = Convert.ToString(jzds.Tables[0].Rows[k]["setl_totlnum"]);
                    fed.insutype = Convert.ToString(jzds.Tables[0].Rows[k]["insutype"]);
                    fed.reim_flag = Convert.ToString(jzds.Tables[0].Rows[k]["reim_flag"]);
                    fed.out_setl_flag = Convert.ToString(jzds.Tables[0].Rows[k]["out_setl_flag"]);
                    //fed.fsi_operation_dtos = Convert.ToString(jzds.Tables[0].Rows[k]["fsi_operation_dtos"]);
                    jzlist.Add(fed);
                }
                fed.fsi_diagnose_dtos = zdlist;
                fed.fsi_order_dtos = listpod;
            }

            //参保人信息
            var cbrsql = string.Format(@"select patid patn_id,xm patn_name,xb gend,CONVERT(varchar(100),csny, 23) brdy,zyh curr_mdtrt_id from NewtouchHIS_Sett.dbo.zy_brjbxx with(nolock) where 
  zyh='" + zyh + "' and OrganizeId='" + orgId + "' and zt='1'");

            DataSet cbrds = platFormServer.Query(cbrsql);
            List<InputPD3102> cbrlist = new List<InputPD3102>();
            InputPD3102 pd = new InputPD3102();
            if (cbrds.Tables[0].Rows.Count > 0)
            {
                for (int c = 0; c < cbrds.Tables[0].Rows.Count; c++)
                {
                    pd.patn_id = Convert.ToString(cbrds.Tables[0].Rows[c]["patn_id"]);
                    pd.patn_name = Convert.ToString(cbrds.Tables[0].Rows[c]["patn_name"]);
                    pd.gend = Convert.ToString(cbrds.Tables[0].Rows[c]["gend"]);
                    pd.brdy = Convert.ToString(cbrds.Tables[0].Rows[c]["brdy"]);
                    pd.poolarea = ConfigurationManager.AppSettings["mdtrtarea_admvs"];
                    pd.curr_mdtrt_id = Convert.ToString(cbrds.Tables[0].Rows[c]["curr_mdtrt_id"]);

                    cbrlist.Add(pd);
                }
                pd.fsi_encounter_dtos = jzlist;
            }

            InputData3102 datas = new InputData3102();
            datas.patient_dtos = cbrlist;
            datas.trig_scen = "1";

            return datas;


        }
        #endregion

        public static DataTable QueryInventory3501(string pdId,string orgId,string ddyyid,string ddyymc)
        {
            Parameters.Clear();
            Parameters.Add("@orgId", orgId);
            Parameters.Add("@pdId", pdId);
            Parameters.Add("@ddyyid", ddyyid);
            Parameters.Add("@ddyymc", ddyymc);
            return platFormServer.RunProc_DataTable_WqServer("usp_Inp_InventoryUpload_invinfo", Parameters);
        }
        public static DataTable QueryInventory3501(string orgId)
        {
            Parameters.Clear();
            Parameters.Add("@orgId", orgId);
            return platFormServer.RunProc_DataTable_WqServer("usp_Inp_InventoryUpload_invinfo", Parameters);
        }
        public static int DeleteInventory(string id, string type)
        {
            return platFormServer.ExecuteSql(string.Format($"delete Drjk_jxcsc_output where mlbm_id = '{id}' and type = '{type}' "));
        }
        public static DataTable QueryInventory3502(string crkId, string orgId, string ddyyid, string ddyymc)
        {
            Parameters.Clear();
            Parameters.Add("@orgId", orgId);
            Parameters.Add("@crkId", crkId);
            Parameters.Add("@ddyyid", ddyyid);
            Parameters.Add("@ddyymc", ddyymc);
            return platFormServer.RunProc_DataTable_WqServer("usp_Inp_InventoryUpdate_invinfo", Parameters);
        }
        public static DataTable QueryInventory3502(string orgId)
        {
            Parameters.Clear();
            Parameters.Add("@orgId", orgId);
            return platFormServer.RunProc_DataTable_WqServer("usp_Inp_InventoryUpdate_invinfo", Parameters);
        }
        public static DataTable QueryInventory3503(string crkId, string orgId, string ddyyid, string ddyymc)
        {
            Parameters.Clear();
            Parameters.Add("@orgId", orgId);
            Parameters.Add("@crkId", crkId);
            Parameters.Add("@ddyyid", ddyyid);
            Parameters.Add("@ddyymc", ddyymc);
            return platFormServer.RunProc_DataTable_WqServer("usp_Inp_InventoryUpdate_purchase", Parameters);
        }
        public static DataTable QueryInventory3503(string orgId)
        {
            Parameters.Clear();
            Parameters.Add("@orgId", orgId);
            return platFormServer.RunProc_DataTable_WqServer("usp_Inp_InventoryUpdate_purchase", Parameters);
        }
        public static DataTable QueryInventory3504(string crkId, string orgId, string ddyyid, string ddyymc)
        {
            Parameters.Clear();
            Parameters.Add("@orgId", orgId);
            Parameters.Add("@crkId", crkId);
            Parameters.Add("@ddyyid", ddyyid);
            Parameters.Add("@ddyymc", ddyymc);
            return platFormServer.RunProc_DataTable_WqServer("usp_Inp_InventoryRetreat_purchase", Parameters);
        }
        public static DataTable QueryInventory3504(string orgId)
        {
            Parameters.Clear();
            Parameters.Add("@orgId", orgId);
            return platFormServer.RunProc_DataTable_WqServer("usp_Inp_InventoryRetreat_purchase", Parameters);
        }
        public static DataTable QueryInventory3505(string orgId)
        {
            Parameters.Clear();
            Parameters.Add("@orgId", orgId);
            return platFormServer.RunProc_DataTable_WqServer("usp_Inp_InventoryUpload_sale", Parameters);
        }
        public static DataTable Queryqltctrlinfo4104(string orgId, string hisid)
        {
            Parameters.Clear();
            Parameters.Add("@orgId", orgId);
            Parameters.Add("@zyh", hisid);
            return platFormServer.RunProc_DataTable_WqServer("usp_Inp_ybupload_cyjs_qltctrl", Parameters);
        }
        public static DataTable QueryStastinfo4102(string orgId, string hisid)
        {
            Parameters.Clear();
            Parameters.Add("@orgId", orgId);
            Parameters.Add("@zyh", hisid);
            return platFormServer.RunProc_DataTable_WqServer("usp_Inp_ybupload_stastinfo", Parameters);
        }
        public static int UpHospitaSettlement3203(string qsny, string qslb, string sfyd, string sqdid, string operatorId)
		{
			return platFormServer.ExecuteSql(string.Format($"update ybjsqd_qssq set sqzt=1 ,sqrq=getdate(),sqczr='{operatorId}',sqdid='{sqdid}' where qsny='{qsny}' and qslb='{qslb}' and sfyd='{sfyd}' "));

		}
		public static int UpHospitaSettlement3204(string sqdid)
		{
			return platFormServer.ExecuteSql(string.Format($"update ybjsqd_qssq set sqzt=0,sqczr=null,sqrq=null,sqdid=null where sqdid='{sqdid}' "));

		}
        /// <summary>
        /// 医疗保障基金结算清单信息查询
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="hisid"></param>
        /// <returns></returns>
        public static DataTable QuerySetlinfoData4103(string orgId, string hisid)
        {
            Parameters.Clear();
            Parameters.Add("@orgId", orgId);
            Parameters.Add("@zyh", hisid);
            return platFormServer.RunProc_DataTable_WqServer("usp_Inp_ybupload_setlinfo_data", Parameters);
        }
        /// <summary>
		/// 临床检验报告记录表 175 检查项目信息（节点标识：iteminfo
		/// </summary>
		/// <param name="orgId"></param>
		/// <param name="hisid"></param>
		/// <returns></returns>
		public static DataTable QueryIteminfo4502(string orgId, string hisid)
        {
            Parameters.Clear();
            Parameters.Add("@orgId", orgId);
            Parameters.Add("@zyh", hisid);
            return platFormServer.RunProc_DataTable_WqServer("usp_Inp_ybupload_report_lis_iteminfo", Parameters);
        }
        /// <summary>
		/// 临床检验报告记录表 175 检查项目信息（节点标识：iteminfo
		/// </summary>
		/// <param name="orgId"></param>
		/// <param name="hisid"></param>
		/// <returns></returns>
		public static DataTable QuerySampleinfo4502(string orgId, string hisid)
        {
            Parameters.Clear();
            Parameters.Add("@orgId", orgId);
            Parameters.Add("@zyh", hisid);
            return platFormServer.RunProc_DataTable_WqServer("usp_Inp_ybupload_report_lis_sampleinfo", Parameters);
        }
        /// <summary>
		/// 临床检验报告记录 表 174 输入-检查记录（节点标识：examinfo） 
		/// </summary>
		/// <param name="orgId"></param>
		/// <param name="hisid"></param>
		/// <returns></returns>
		public static DataTable QueryLabinfo4502(string orgId, string hisid)
        {
            Parameters.Clear();
            Parameters.Add("@orgId", orgId);
            Parameters.Add("@zyh", hisid);
            return platFormServer.RunProc_DataTable_WqServer("usp_Inp_ybupload_report_lis_labinfo", Parameters);
        }
        /// <summary>
        /// 病案首页上传 基础信息
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="hisid"></param>
        /// <returns></returns>
        public static DataTable QueryInputYbsjsc(string orgId, string hisid)
        {
            Parameters.Clear();
            Parameters.Add("@orgId", orgId);
            Parameters.Add("@zyh", hisid);
            return platFormServer.RunProc_DataTable_WqServer("usp_Inp_shybupload_basy_baseinfo", Parameters);
        }
        /// <summary>
        /// 病案首页上传 诊断信息
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="hisid"></param>
        /// <returns></returns>
        public static DataTable QueryInputYbsjscZDXX(string orgId, string hisid)
        {
            Parameters.Clear();
            Parameters.Add("@orgId", orgId);
            Parameters.Add("@zyh", hisid);
            return platFormServer.RunProc_DataTable_WqServer("usp_Inp_shybupload_basy_diseinfo", Parameters);
        }
        /// <summary>
        /// 病案首页上传 手术信息
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="hisid"></param>
        /// <returns></returns>
        public static DataTable QueryInputYbsjscSSXX(string orgId, string hisid)
        {
            Parameters.Clear();
            Parameters.Add("@orgId", orgId);
            Parameters.Add("@zyh", hisid);
            return platFormServer.RunProc_DataTable_WqServer("usp_Inp_shybupload_basy_oprninfo", Parameters);
        }
        /// <summary>
        /// 病案首页上传 费用信息
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="hisid"></param>
        /// <returns></returns>
        public static DataTable QueryInputYbsjscFYXX(string orgId, string hisid)
        {
            Parameters.Clear();
            Parameters.Add("@orgId", orgId);
            Parameters.Add("@zyh", hisid);
            return platFormServer.RunProc_DataTable_WqServer("usp_Inp_shybupload_basy_fyxx", Parameters);
        }
        /// <summary>
        /// 病案首页上传 流水号信息
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="hisid"></param>
        /// <returns></returns>
        public static DataTable QueryInputYbsjscLSHXX(string orgId, string hisid)
        {
            Parameters.Clear();
            Parameters.Add("@orgId", orgId);
            Parameters.Add("@zyh", hisid);
            return platFormServer.RunProc_DataTable_WqServer("usp_Inp_shybupload_basy_fpxx", Parameters);
        }
        /// <summary>
        /// 病案首页上传 发票信息
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="hisid"></param>
        /// <returns></returns>
        public static DataTable QueryInputYbsjscFPXX(string orgId, string hisid)
        {
            Parameters.Clear();
            Parameters.Add("@orgId", orgId);
            Parameters.Add("@zyh", hisid);
            return platFormServer.RunProc_DataTable_WqServer("usp_Inp_shybupload_basy_fpxx", Parameters);
        }

        /// <summary>
        /// 电子处方上传内容查询--前20个字段
        /// </summary>
        /// <param name="hisId"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public static DataTable ElectronicPrescription_D003_Prescription_top20(string hisId, string orgId, string cfh, string shks, string shys)
        {
            Parameters.Clear();
            Parameters.Add("@orgId", orgId);
            Parameters.Add("@hisId", hisId);
            Parameters.Add("@cfh", cfh);
            Parameters.Add("@fixmedinsName", ConfigurationManager.AppSettings["fixmedins_name"]);
            Parameters.Add("@fixmedinsCode", ConfigurationManager.AppSettings["fixmedins_code"]);
            Parameters.Add("@shks", shks);
            Parameters.Add("@shys", shys);
            return platFormServer.RunProc_DataTable_WqServer("usp_ElectronicPrescription_D003_Prescription_top20", Parameters);
        }
        /// <summary>
        /// 电子处方上传内容查询
        /// </summary>
        /// <param name="hisId"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public static DataTable ElectronicPrescription_D003_Prescription(string hisId, string orgId, string cfh, string shks, string shys, string rxFile, string signDigest)
        {
            Parameters.Clear();
            Parameters.Add("@orgId", orgId);
            Parameters.Add("@hisId", hisId);
            Parameters.Add("@cfh", cfh);
            Parameters.Add("@fixmedinsName", ConfigurationManager.AppSettings["fixmedins_name"]);
            Parameters.Add("@fixmedinsCode", ConfigurationManager.AppSettings["fixmedins_code"]);
            Parameters.Add("@shks", shks);
            Parameters.Add("@shys", shys);
            Parameters.Add("@rxFile", rxFile);
            Parameters.Add("@signDigest", signDigest);
            return platFormServer.RunProc_DataTable_WqServer("usp_ElectronicPrescription_D003_Prescription", Parameters);
        }
        /// <summary>
        /// 电子处方上传内容查询
        /// </summary>
        /// <param name="hisId"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public static DataTable ElectronicPrescription_D004_Prescription(string hisId, string orgId, string cfh, string cxys)
        {
            Parameters.Clear();
            Parameters.Add("@orgId", orgId);
            Parameters.Add("@hisId", hisId);
            Parameters.Add("@fixmedinsCode", ConfigurationManager.AppSettings["fixmedins_code"]);
            Parameters.Add("@cfh", cfh);
            Parameters.Add("@cxys", cxys);
            return platFormServer.RunProc_DataTable_WqServer("usp_ElectronicPrescription_D004_Prescription", Parameters);
        }
        /// <summary>
        /// 撤销电子处方上传
        /// </summary>
        /// <param name="hisId"></param>
        /// <param name="errorNo"></param>
        /// <returns></returns>
        public static bool RevokePrescriptionToD003(string hiRxno, string cxyy, string rxStasCodg, string rxStasName, out int errorNo)
        {
            List<string> SQLList = new List<string>();
            SQLList.Add(string.Format($"update [NewtouchHIS_Sett]..Dzcf_D003_output set rxStasCodg='{rxStasCodg}',rxStasName='{rxStasName}',cxyy='{cxyy}',cxsj=convert(varchar(50),getdate(),120) where hiRxno='{hiRxno}' and zt='1' "));
            return Merge(SQLList, out errorNo);
        }
        /// <summary>
        /// 电子处方审核结果查询
        /// </summary>
        /// <param name="hisId"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public static DataTable ElectronicPrescription_D006_Prescription(string hisId, string orgId, string cfh)
        {
            Parameters.Clear();
            Parameters.Add("@orgId", orgId);
            Parameters.Add("@hisId", hisId);
            Parameters.Add("@fixmedinsCode", ConfigurationManager.AppSettings["fixmedins_code"]);
            Parameters.Add("@cfh", cfh);
            return platFormServer.RunProc_DataTable_WqServer("usp_ElectronicPrescription_D006_Prescription", Parameters);
        }
        /// <summary>
        /// 修改电子处方审核状态
        /// </summary>
        /// <returns></returns>
        public static bool D006UpdatePrescriptionToD003(string hiRxno, string rxStasCodg, string rxStasName, string rxChkStasCodg, string rxChkOpnn, string rxChkTime, string rxChkStasName, out int errorNo)
        {
            List<string> SQLList = new List<string>();
            SQLList.Add(string.Format($"update [NewtouchHIS_Sett]..Dzcf_D003_output set rxStasCodg='{rxStasCodg}',rxStasName='{rxStasName}',rxChkStasCodg='{rxChkStasCodg}',rxChkOpnn='{rxChkOpnn}',rxChkTime='{rxChkTime}',rxChkStasName='{rxChkStasName}' where hiRxno='{hiRxno}' and zt='1' "));
            return Merge(SQLList, out errorNo);
        }
        /// <summary>
        /// 修改电子处方使用状态
        /// </summary>
        /// <returns></returns>
        public static bool D007UpdatePrescriptionToD003(string hiRxno, string rxStasCodg, string rxStasName, string rxUsedStasCodg, string rxUsedStasName, out int errorNo)
        {
            List<string> SQLList = new List<string>();
            SQLList.Add(string.Format($"update [NewtouchHIS_Sett]..Dzcf_D003_output set rxStasCodg='{rxStasCodg}',rxStasName='{rxStasName}',rxUsedStasCodg='{rxUsedStasCodg}',rxUsedStasName='{rxUsedStasName}' where hiRxno='{hiRxno}' and zt='1' "));
            return Merge(SQLList, out errorNo);
        }
        /// <summary>
        /// 处方明细信息
        /// </summary>
        /// <param name="hisId"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public static DataTable ElectronicPrescription_D001_Prescription_DetailData(string hisId, string orgId, string cfh)
        {
            Parameters.Clear();
            Parameters.Add("@orgId", orgId);
            Parameters.Add("@hisId", hisId);
            Parameters.Add("@cfh", cfh);
            return platFormServer.RunProc_DataTable_WqServer("usp_ElectronicPrescription_D001_Prescription_DetailData", Parameters);
        }
        /// <summary>
        /// 就诊信息
        /// </summary>
        /// <param name="hisId"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public static DataTable ElectronicPrescription_D001_Prescription_mdtrtData(string hisId, string orgId, string cfh)
        {
            Parameters.Clear();
            Parameters.Add("@orgId", orgId);
            Parameters.Add("@hisId", hisId);
            Parameters.Add("@cfh", cfh);
            Parameters.Add("@fixmedinsName", ConfigurationManager.AppSettings["fixmedins_name"]);
            Parameters.Add("@fixmedinsCode", ConfigurationManager.AppSettings["fixmedins_code"]);
            return platFormServer.RunProc_DataTable_WqServer("usp_ElectronicPrescription_D001_Prescription_mdtrtData", Parameters);
        }

        /// <summary>
        /// 处方信息获取
        /// </summary>
        /// <param name="hisId"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public static DataTable ElectronicPrescription_D001_PrescriptionData(string hisId, string orgId, string cfh)
        {
            Parameters.Clear();
            Parameters.Add("@orgId", orgId);
            Parameters.Add("@hisId", hisId);
            Parameters.Add("@cfh", cfh);
            return platFormServer.RunProc_DataTable_WqServer("usp_ElectronicPrescription_D001_PrescriptionData", Parameters);
        }

        /// <summary>
        /// 诊断信息
        /// </summary>
        /// <param name="hisId"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public static DataTable ElectronicPrescription_D001_Prescription_diseData(string hisId, string orgId, string cfh)
        {
            Parameters.Clear();
            Parameters.Add("@orgId", orgId);
            Parameters.Add("@hisId", hisId);
            Parameters.Add("@cfh", cfh);
            return platFormServer.RunProc_DataTable_WqServer("usp_ElectronicPrescription_D001_Prescription_diseData", Parameters);
        }
        public static DataTable DetailAuditData3102_fsi_encounter_dtos(string zyh, string orgId, string ddyyid, string ddyymc, string insuplc_admdvs)
        {
            Parameters.Clear();
            Parameters.Add("@orgId", orgId);
            Parameters.Add("@zyh", zyh);
            Parameters.Add("@ddyyid", ddyyid);
            Parameters.Add("@ddyymc", ddyymc);
            Parameters.Add("@insuplc_admdvs", insuplc_admdvs);
            return platFormServer.RunProc_DataTable_WqServer("usp_Inp_DetailAudit_fsi_encounter_dtos", Parameters);
        }
        public static DataTable DetailAuditData3102_patient_dtos(string zyh, string orgId)
        {
            Parameters.Clear();
            Parameters.Add("@orgId", orgId);
            Parameters.Add("@zyh", zyh);
            DataTable dt = platFormServer.RunProc_DataTable_WqServer("usp_Inp_DetailAudit_patient_dtos", Parameters);
            return dt;
        }
        public static DataTable QueryInventory3506(string orgId)
        {
            Parameters.Clear();
            Parameters.Add("@orgId", orgId);
            return platFormServer.RunProc_DataTable_WqServer("usp_Inp_InventoryUpload_Salesreturn", Parameters);
        }
    }
}