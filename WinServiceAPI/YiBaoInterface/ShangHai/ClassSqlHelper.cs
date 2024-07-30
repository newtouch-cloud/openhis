using CQYiBaoInterface.Models.ShangHai.Post;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using wqsj_PlatForm_DAS;

namespace CQYiBaoInterface.ShangHai
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
        /// 执行SQL 语句
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static int ExecuteSql(string sql)
        {
            return platFormServer.ExecuteSql(sql);
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
        /// 撤销门诊住院结算信息
        /// </summary>
        /// <param name="hisId"></param>
        /// <param name="errorNo"></param>
        /// <returns></returns>
        public static bool UpSettlement(string hisId, string lsh,string tflsh, string operatorId,string sflx, out int errorNo)
        {
            List<string> SQLList = new List<string>();
            if (sflx == "0")//门诊挂号
            {
                SQLList.Add(string.Format($"update Ybjk_SH02_Input set zt=0,zt_rq=GETDATE(),zt_czy='{operatorId}' where mzh='{hisId}' and lsh='{lsh}' "));
                SQLList.Add(string.Format($"update Ybjk_SH02_Output set zt=0,tlsh={tflsh}, zt_rq=GETDATE(),zt_czy='{operatorId}' where mzh='{hisId}' and lsh='{lsh}' "));
            }
            else if (sflx == "1")//门诊收费
            {
                SQLList.Add(string.Format($"update Ybjk_SI12_Input set zt=0,zt_rq=GETDATE(),zt_czy='{operatorId}' where mzh='{hisId}' and lsh='{lsh}' "));
                SQLList.Add(string.Format($"update Ybjk_SI12_MxzdhsInput set zt=0 where mzh='{hisId}' and lsh='{lsh}' "));
                SQLList.Add(string.Format($"update Ybjk_SI12_ZdnosInput set zt=0 where mzh='{hisId}' and lsh='{lsh}' "));
                SQLList.Add(string.Format($"update Ybjk_SI12_Output set zt=0,zt_rq=GETDATE(),tlsh={tflsh},zt_czy='{operatorId}' where mzh='{hisId}' and lsh='{lsh}' "));
            }
            else if(sflx=="2")//住院收费
            {
                SQLList.Add(string.Format($"update Ybjk_SI52_Input set zt=0,zt_rq=GETDATE(),zt_czy='{operatorId}' where zyh='{hisId}' and lsh='{lsh}' "));
                SQLList.Add(string.Format($"update Ybjk_SI52_MxzdhsInput set zt=0,zt_rq=GETDATE(),zt_czy='{operatorId}' where zyh='{hisId}' and lsh='{lsh}' "));
                SQLList.Add(string.Format($"update Ybjk_SI52_ZdnosInput set zt=0,zt_rq=GETDATE(),zt_czy='{operatorId}' where zyh='{hisId}' and lsh='{lsh}' "));
                SQLList.Add(string.Format($"update Ybjk_SI52_Output set zt=0,zt_rq=GETDATE(),tlsh={tflsh},zt_czy='{operatorId}' where zyh='{hisId}' and lsh='{lsh}' "));
            }
            return Merge(SQLList, out errorNo);
        }
        /// <summary>
        /// 查询门诊费用上传信息
        /// </summary>
        /// <param name="hisId">hisid</param>
        /// <returns></returns>
        public static DataTable QuertFeedetail(Post_SN01MZ postsn01)
        {
            /*
            @type varchar(1),-- 1处方  3 处方退费再传
            @mzh varchar(20),  --门诊号
            @cfnm varchar(50), --处方内码
            @orgId varchar(50),  --组织机构
            @jsnm varchar(50), --结算内码 
            @ksbm varchar(50),--科室编码
            @ysbm varchar(50),--医生编码
            @dise_codg varchar(50)--病种编码
             */
            Parameters.Clear();
            Parameters.Add("@type", postsn01.type);
            Parameters.Add("@mzh", postsn01.hisId);
            Parameters.Add("@cfnm", postsn01.cfnm);
            Parameters.Add("@orgId", postsn01.orgId);
            Parameters.Add("@jsnm", postsn01.jsnm);
            Parameters.Add("@ksbm", postsn01.ksbm);
            Parameters.Add("@ysbm", postsn01.ysbm);
            Parameters.Add("@dise_codg", postsn01.dise_codg);
            return platFormServer.RunProc_DataTable_WqServer("mz_fymxsc_shanghaiV5", Parameters);
        }

        public static int SaveLogSM01(string orgId,XmlDocument xml)
        {
            return platFormServer.ExecuteSql(string.Format($"exec usp_shybPatInfo_SM01 @orgId='{orgId}',@xml='{xml.InnerXml}' "));
        }

        /// <summary>
        /// 查询医保科室编码
        /// </summary>
        /// <param name="mzh"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public static DataTable QueryYbDept(string mzh, string orgId)
        {
            string sql= string.Format($@"select isnull(ybksbm2,Code) deptid2,isnull(ybksbm,code) deptid,dept.Name deptname,jzid  from mz_gh gh
left join NewtouchHIS_Base.[dbo].[Sys_Department] dept on  gh.ks=dept.code and gh.organizeid=dept.organizeid 
where gh.mzh='{mzh}' and gh.OrganizeId='{orgId}' and gh.zt=1 "); 
            return platFormServer.Query(sql).Tables[0];
        }
        /// <summary>
        /// 科室获取医保科室代码字典值
        /// </summary>
        /// <param name="deptCode"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public static DataTable QueryDeptToybdm(string deptCode, string orgId)
        {
            string sql = string.Format($@"select name ksmc,isnull(ybksbm,Code) gjybksdm ,isnull(ybksbm2,Code) sybdm
from NewtouchHIS_Base.[dbo].[Sys_Department] dept
where dept.Code='{deptCode}' and dept.OrganizeId='{orgId}' and dept.zt=1 ");
            return platFormServer.Query(sql).Tables[0];
        }

        public static DataTable QueryXmYbdm(string ptzl, string orgId)
        {
            string sql = string.Format($@"select top 1 ybdm from [dbo].[mz_gh_zlxmzh] zlzt
left join NewtouchHIS_Base..xt_sfxm  sfxm on zlzt.zlxm=sfxm.sfxmCode and zlzt.OrganizeId=sfxm.OrganizeId 
where zlzt.zt=1 and zlzt.OrganizeId='{orgId}' and zhcode='{ptzl}' ");
            return platFormServer.Query(sql).Tables[0];
        }

        public static DataTable QueryYbReturnData(string jylsh)
        {
            string sql = string.Format($@"
                select  mzh,lsh,totalexpense from [dbo].[Ybjk_SI12_Output] where lsh='{jylsh}' "); //and zt=1  
            return platFormServer.Query(sql).Tables[0];
        }
        /// <summary>
        /// 门诊退费获取撤销明细账单号
        /// </summary>
        /// <param name="jylsh"></param>
        /// <returns></returns>
        public static DataTable QueryYbCxmxzdhData(string jylsh)
        {
            string sql = string.Format($@"
                select mxzdh,lsh,mzh from [dbo].[Ybjk_SI12_MxzdhsInput] where lsh='{jylsh}' ");//and zt=1  
            return platFormServer.Query(sql).Tables[0];
        }

        /// <summary>
        /// 查询病人诊断信息
        /// </summary>
        /// <param name="model">1查询门诊 2查询住院入院诊断 3 查询住院出院诊断</param>
        /// <param name="hisId">门诊号或住院号</param>
        /// <returns></returns>
        public static DataTable QueryICD(int model, string hisId, string mdtrt_id, string psn_no)
        {
            string sql = "";
            if (model == 1)
            {
                sql = string.Format($@"select 1 diag_type,zy.zdlx diag_srt_no,zy.zdcode zdno,zy.zdmc zdmc,
isnull(dept.ybksbm, jz.jzks) diag_dept,  isnull(staff.gjybdm, jz.jzys) dise_dor_no, jz.jzysmc dise_dor_name, convert(varchar(20), zy.createtime, 120)diag_time,
 zy.zt vali_flag from Newtouch_CIS.dbo.xt_jz jz
 join Newtouch_CIS.dbo.xt_xyzd zy  on jz.jzId = zy.jzId and jz.OrganizeId = zy.OrganizeId and  jz.zt = 1
 join NewtouchHIS_Base..V_S_Sys_Staff staff on staff.gh = jz.jzys and staff.OrganizeId = jz.OrganizeId
 left join NewtouchHIS_Base.dbo.V_S_Sys_Department dept on dept.Code = jz.jzks and dept.OrganizeId = jz.OrganizeId
 where  jz.mzh ='{hisId}'" +
                  $@" union all select 2 diag_type, zy.zdlx diag_srt_no, zy.zdcode zdno, zy.zdmc zdmc, isnull(dept.ybksbm,jz.jzks) diag_dept,
 isnull(staff.gjybdm, jz.jzys) dise_dor_no, jz.jzysmc dise_dor_name, convert(varchar(20), zy.createtime, 120)diag_time, zy.zt vali_flag
 from Newtouch_CIS.dbo.xt_jz jz
 join Newtouch_CIS.dbo.xt_zyzd zy  on jz.jzId = zy.jzId and jz.OrganizeId = zy.OrganizeId and  jz.zt = 1
  join NewtouchHIS_Base..V_S_Sys_Staff staff on staff.gh = jz.jzys and staff.OrganizeId = jz.OrganizeId
 left join NewtouchHIS_Base.dbo.V_S_Sys_Department dept on dept.Code = jz.jzks and dept.OrganizeId = jz.OrganizeId
 where  jz.mzh = '{hisId}'");
            }
            else if (model == 2)
            {
                sql = string.Format($@"select 1 diag_type, zy.zdpx diag_srt_no, zy.icd10 zdno, zy.zdmc zdmc, isnull(dept.ybksbm,jz.ks) diag_dept, isnull(ys.gjybdm,jz.doctor) dise_dor_no,ys.Name dise_dor_name, convert(varchar(20), zy.createtime, 120)diag_time, zy.zt vali_flag ,case zy.zdpx when 1 then 1 else 0 end maindiag_flag, '{mdtrt_id}' mdtrt_id ,'{psn_no}' psn_no " +
                    $@"from NewtouchHis_sett.[dbo].[zy_brjbxx]  jz
 join NewtouchHis_sett.dbo.zy_rydzd zy on jz.zyh = zy.zyh and jz.OrganizeId = zy.OrganizeId and  jz.zt = 1 and zy.zt=1
 join NewtouchHIS_Base.dbo.Sys_Staff ys on jz.doctor = ys.gh 
 left join NewtouchHIS_Base.dbo.V_S_Sys_Department dept on dept.Code=jz.ks and dept.OrganizeId=jz.OrganizeId
where  jz.zyh = '{hisId}'");
            }
            else if (model == 3)
            {
                sql = string.Format($"select 1 diag_type,case zy.zdlx when 0 then 1 else 0 end maindiag_flag, zy.zdlx diag_srt_no, zy.zddm diag_code, zy.zdmc diag_name,  isnull(dept.ybksbm,jz.ks)  diag_dept, isnull(ys.gjybdm,jz.doctor)  dise_dor_no, ys.Name dise_dor_name, convert(varchar(20), zy.createtime, 120)diag_time, '{mdtrt_id}' mdtrt_id ,'{psn_no}' psn_no " +
                    $@" from NewtouchHis_sett.[dbo].[zy_brjbxx]  jz
 join [Newtouch_CIS].[dbo].[zy_PatDxInfo]  zy on jz.zyh = zy.zyh and jz.OrganizeId = zy.OrganizeId and  jz.zt = 1  and zy.zdlb = 2 and zy.zt = 1 and zy.zdmc <> '999999999'
 join NewtouchHIS_Base.dbo.Sys_Staff ys on jz.doctor = ys.gh
 left join NewtouchHIS_Base.dbo.V_S_Sys_Department dept on dept.Code = jz.ks and dept.OrganizeId = jz.OrganizeId " +
                    $"where  jz.zyh = '{hisId}'");
            }
            return platFormServer.Query(sql).Tables[0];
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
                //sql = string.Format($@"select mxzdh,(ybzje+zfzje) totalexpense, ybzje ybjsfwfyze, zfzje fybjsfwfyze from[dbo].[Ybjk_SN01_Output] where zt=1 and mzzyh = '{hisId}' and jylsh = '{chrg_bchno}' ");
                sql = string.Format($@"select max(a.mxzdh) mxzdh,sum(mxxmjyfy) totalexpense,sum(mxxmybjsfwfy) ybjsfwfyze,sum(mxxmje)-sum(mxxmybjsfwfy) fybjsfwfyze
,sum(isnull(b.zfbl,0.00)) zfbl,sum(isnull(b.zfje,0.00)) zfje,sum(isnull(b.fyxj,0.00)) fyxj
from Ybjk_SN01_Output a
left join Ybjk_SN01_MxXms_Output b on a.Id=b.mainId and b.zt=1 where a.zt=1 and a.mzzyh = '{hisId}' and jylsh = '{chrg_bchno}' ");
            }
            else
            {
                sql = string.Format($"select sum(fulamt_ownpay_amt) fulamt_ownpay_amt ,sum(overlmt_amt)  overlmt_selfpay ,sum(preselfpay_amt)preselfpay_amt,sum(inscp_scp_amt) inscp_scp_amt ,sum(det_item_fee_sumamt) det_item_fee_sumamt from drjk_zyfymxsc_output  where zyh = '{hisId}'  ");
            }
            return platFormServer.Query(sql).Tables[0];
        }

        /// <summary>
        /// 查询住院费用上传信息
        /// </summary>
        /// <param name="hisId">hisid</param>
        /// <returns></returns>
        public static DataTable QuertHospitalFeedetailV2(Post_SN01ZY post2204)
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
            Parameters.Add("@jfbbh", post2204.jfbbh ?? "");
            return platFormServer.RunProc_DataTable_WqServer("shyb_zy_fymxsc", Parameters);
        }

        public static bool UpHospitaFeedetail(string hisId, string feedetl_sn, out int errorNo)
        {
            List<string> SQLList = new List<string>();
            SQLList.Add(string.Format($"update Ybjk_SN01_Input set zt='0' where mzzyh='{hisId}' and fhmxzdh in (select * from dbo.f_split('{feedetl_sn}',','))"));
            SQLList.Add(string.Format($"update Ybjk_SN01_Mxxmz_Input set zt='0' where mzzyh='{hisId}' and mxzdh in (select * from dbo.f_split('{feedetl_sn}',','))"));
            SQLList.Add(string.Format($"update Ybjk_SN01_Output  set zt='0' where mzzyh='{hisId}' and mxzdh in (select * from dbo.f_split('{feedetl_sn}',','))"));
            //SQLList.Add(string.Format($"delete Ybjk_SN01_Xfmx_Input where mzzyh='{hisId}' and mxzdh in (select * from dbo.f_split('{feedetl_sn}',','))"));
            SQLList.Add(string.Format($"update Ybjk_SN01_MxXms_Output  set zt='0' where mzzyh='{hisId}' and mxzdh in (select * from dbo.f_split('{feedetl_sn}',','))"));
            return Merge(SQLList, out errorNo);
        }
        public static bool UpHospitaFeedetailZy(string hisId, string feedetl_sn, out int errorNo)
        {
            List<string> SQLList = new List<string>();
            SQLList.Add(string.Format($"update Ybjk_SN01_Mxxzy_Input set zt='0' where mzzyh='{hisId}' and mxzdh in (select * from dbo.f_split('{feedetl_sn}',','))"));
            SQLList.Add(string.Format($"update Ybjk_SN01_MxXms_zy_Output set zt='0' where mzzyh='{hisId}' and mxzdh in (select * from dbo.f_split('{feedetl_sn}',','))"));
            SQLList.Add(string.Format($"update Ybjk_SN01_zy_Input  set zt='0' where mzzyh='{hisId}' and mxzdh in (select * from dbo.f_split('{feedetl_sn}',','))"));
            //SQLList.Add(string.Format($"delete Ybjk_SN01_Xfmx_Input where mzzyh='{hisId}' and mxzdh in (select * from dbo.f_split('{feedetl_sn}',','))"));
            SQLList.Add(string.Format($"update Ybjk_SN01_zy_Output  set zt='0' where mzzyh='{hisId}' and mxzdh in (select * from dbo.f_split('{feedetl_sn}',','))"));
            return Merge(SQLList, out errorNo);
        }
        public static bool ExSJ21info(string operatorId, string hisId, out int errorNo)
        {
            List<string> SQLList = new List<string>();
            SQLList.Add(string.Format($"update Ybjk_SJ11_InPut set zt=0,zt_rq=GETDATE(),zt_czy='{operatorId}' where mzzyh='{hisId}'"));
            SQLList.Add(string.Format($"update Ybjk_SJ11_Output set zt=0,zt_rq=GETDATE(),zt_czy='{operatorId}' where mzzyh='{hisId}'"));
            return Merge(SQLList, out errorNo);

        }
        #region 住院登记查询
        /// <summary>
        /// 查询医保科室编码 住院信息
        /// </summary>
        /// <param name="zyh"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public static DataTable QueryYbDeptZy(string zyh, string orgId)
        {
            string sql = string.Format($@"select isnull(ybksbm,code) deptid,dept.Name deptname,convert(varchar(8),brxx.ryrq,112) ryrq,DATEDIFF(day,ryrq,isnull(brxx.cyrq,GETDATE())) zyts,jzout.jzdyh ,
	jzinput.carddata,jzinput.cardtype,convert(varchar(8),isnull(brxx.cyrq,GETDATE()),112) cyrq
from [NewtouchHIS_Sett]..zy_brjbxx brxx
left join NewtouchHIS_Base.[dbo].[Sys_Department] dept on  brxx.ks=dept.code and brxx.organizeid=dept.organizeid 
left join [NewtouchHIS_Sett]..Ybjk_SJ11_Output jzout on jzout.mzzyh=brxx.zyh and jzout.zt='1'
left join [NewtouchHIS_Sett]..Ybjk_SJ11_InPut jzinput on jzinput.mzzyh=jzout.mzzyh and jzinput.zt=1
where brxx.zyh='{zyh}' and brxx.OrganizeId='{orgId}' and brxx.zt=1 ");
            return platFormServer.Query(sql).Tables[0];
        }
        /// <summary>
        /// 查询住院登记内容
        /// </summary>
        /// <param name="hisId">hisid</param>
        /// <returns></returns>
        public static DataTable QuertZydj(string hisId, string orgId)
        {
            Parameters.Clear();
            Parameters.Add("@orgId", orgId);
            Parameters.Add("@zyh", hisId);
            return platFormServer.RunProc_DataTable_WqServer("zy_brdjxxcx", Parameters);
        }
        #endregion
        #region 住院费用明细上传查询
        /// <summary>
        /// 获取就诊单元号
        /// </summary>
        /// <param name="zyh"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public static DataTable Queryjzxh(string zyh, string orgId)
        {
            string sql = string.Format($@"select jzdyh,b.carddata,b.cardtype from Ybjk_SJ11_Output a
inner join [Ybjk_SJ11_InPut] b on a.mzzyh=b.mzzyh and b.zt=1
where a.mzzyh='{zyh}'");
            return platFormServer.Query(sql).Tables[0];
        }
        #endregion
        #region 获取住院诊断信息
        /// <summary>
        /// 获取出院结算后的诊断信息
        /// </summary>
        /// <param name="zyh"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public static DataTable Queryzyzdxx(string zyh, string orgId)
        {
            string sql = string.Format($@"select zddm zdno,zdmc zdmc from [Newtouch_CIS]..zy_PatDxInfo 
where zyh='{zyh}' and OrganizeId='{orgId}' and zt='1' ");
            return platFormServer.Query(sql).Tables[0];
        }
        /// <summary>
        /// 获取入院诊断信息
        /// </summary>
        /// <param name="zyh"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public static DataTable Queryryzdxx(string zyh, string orgId)
        {
            string sql = string.Format($@"select top 1 icd10 zdno,zdmc from NewtouchHis_sett.dbo.zy_rydzd 
where zyh='{zyh}' and OrganizeId='{orgId}' and zt='1' ");
            return platFormServer.Query(sql).Tables[0];
        }
        #endregion

        #region 获取住院明细账单号
        public static DataTable Querymxzdhsdataxx(string zyh)
        {
            string sql = string.Format($@"select mxzdh,ybzje,zfzje,b.carddata,b.cardtype from Ybjk_SN01_zy_Output a
inner join [Ybjk_SJ11_InPut] b on a.mzzyh=b.mzzyh and b.zt=1
where a.mzzyh='{zyh}' and a.zt='1' ");
            return platFormServer.Query(sql).Tables[0];
        }
        #endregion
        #region 获取住院明细账单号
        public static DataTable QueryYbReturnLsh(string zyh)
        {
            string sql = string.Format($@"
                select lsh,totalexpense from [dbo].Ybjk_SI52_Output where lsh='{zyh}' and zt='1' "); //and zt=1  
            return platFormServer.Query(sql).Tables[0];
        }
        #endregion
    }
}
