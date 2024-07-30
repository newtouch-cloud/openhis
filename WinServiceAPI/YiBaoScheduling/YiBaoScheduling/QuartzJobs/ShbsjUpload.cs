using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YiBaoScheduling.Common;
using YiBaoScheduling.Model;
using YiBaoScheduling.Proxy;

namespace YiBaoScheduling.QuartzJobs
{
    public sealed class ShbsjUpload : JobBase<ShbsjUpload>
    {
        public override void Body()
        {
           
            Log_TbHealthyUpload logs = new Log_TbHealthyUpload();//上传日志表记录
            List<string> logSqlList = new List<string>();
            string errmsg = "";//上传状态
            #region 门诊就诊记录信息
            Console.WriteLine("1.1.1.查询当天门诊就诊记录信息Start...");
            List<string> preSqlList = new List<string>();
            List<TB_YL_MZ_Medical_Record> queryshbsjscData = DBHelper.QueryTB_YL_MZ_Medical_Record();
            AppLogger.Info("插入门诊就诊记录表开始····");
            foreach (var item in queryshbsjscData)
            {
                TB_YL_MZ_Medical_Record yjs = new TB_YL_MZ_Medical_Record();
                yjs = Function.Mapping<TB_YL_MZ_Medical_Record, TB_YL_MZ_Medical_Record>(item);
                preSqlList.Add(yjs.ToAddSql());
            }
            DBHelper.ExecuteSqlTran(preSqlList, "U",out errmsg);

            logs.createtime = DateTime.Now;
            logs.TabName = "TB_YL_MZ_Medical_Record";
            logs.status = "0";
            logs.statusName = "传输成功";
            logs.err_msg = errmsg;
            if (!string.IsNullOrWhiteSpace(errmsg))
            {
                logs.status = "-1";
                logs.err_msg = errmsg;
                logs.statusName = "传输失败";
            }
            logSqlList.Add(logs.ToAddSql());
            AppLogger.Info("插入门诊就诊记录表结束");
            #endregion

            #region 门诊处方明细表
            Console.WriteLine("1.1.1.查询当天门诊处方明细信息Start...");
            List<string> preSqlcfmxList = new List<string>();
            errmsg = "";
            List<TB_CIS_Prescription_Detail> querycfmxData = DBHelper.QueryTB_CIS_Prescription_Detail();
            AppLogger.Info("插入门诊处方明细表开始····");
            foreach (var item in querycfmxData)
            {
                TB_CIS_Prescription_Detail yjs = new TB_CIS_Prescription_Detail();
                yjs = Function.Mapping<TB_CIS_Prescription_Detail, TB_CIS_Prescription_Detail>(item);
                preSqlcfmxList.Add(yjs.ToAddSql());
            }
            DBHelper.ExecuteSqlTran(preSqlcfmxList, "U",out errmsg);

            logs = new Log_TbHealthyUpload();
            logs.createtime = DateTime.Now;
            logs.TabName = "TB_CIS_Prescription_Detail";
            logs.status = "0";
            logs.statusName = "传输成功";
            logs.err_msg = errmsg;
            if (!string.IsNullOrWhiteSpace(errmsg))
            {
                logs.status = "-1";
                logs.err_msg = errmsg;
                logs.statusName = "传输失败";
            }
            logSqlList.Add(logs.ToAddSql());
            AppLogger.Info("插入门诊处方明细表结束");
            #endregion

            #region 门诊收费明细表
            Console.WriteLine("1.1.1.查询当天门诊收费明细信息Start...");
            List<string> preSqlsfmxList = new List<string>();
            errmsg = "";
            List<TB_HIS_MZ_Fee_Detail> querysfmxData = DBHelper.QueryTB_HIS_MZ_Fee_Detail();
            AppLogger.Info("插入门诊收费明细表开始····");
            foreach (var item in querysfmxData)
            {
                TB_HIS_MZ_Fee_Detail yjs = new TB_HIS_MZ_Fee_Detail();
                yjs = Function.Mapping<TB_HIS_MZ_Fee_Detail, TB_HIS_MZ_Fee_Detail>(item);
                preSqlsfmxList.Add(yjs.ToAddSql());
            }
            DBHelper.ExecuteSqlTran(preSqlsfmxList, "U",out errmsg);

            logs = new Log_TbHealthyUpload();
            logs.createtime = DateTime.Now;
            logs.TabName = "TB_HIS_MZ_Fee_Detail";
            logs.status = "0";
            logs.statusName = "传输成功";
            logs.err_msg = errmsg;
            if (!string.IsNullOrWhiteSpace(errmsg))
            {
                logs.status = "-1";
                logs.err_msg = errmsg;
                logs.statusName = "传输失败";
            }
            logSqlList.Add(logs.ToAddSql());
            AppLogger.Info("插入门诊收费明细表结束");
            #endregion

            #region 医学影像检查报告表
            Console.WriteLine("1.1.1.查询当天医学影像检查报告信息Start...");
            List<string> preSqljcbgList = new List<string>();
            errmsg = "";
            List<TB_RIS_Report> queryjcData = DBHelper.QueryTB_RIS_Report();
            AppLogger.Info("插入医学影像检查报告表开始····");
            foreach (var item in queryjcData)
            {
                TB_RIS_Report yjs = new TB_RIS_Report();
                yjs = Function.Mapping<TB_RIS_Report, TB_RIS_Report>(item);
                preSqljcbgList.Add(yjs.ToAddSql());
            }
            DBHelper.ExecuteSqlTran(preSqljcbgList, "U",out errmsg);

            logs = new Log_TbHealthyUpload();
            logs.createtime = DateTime.Now;
            logs.TabName = "TB_RIS_Report";
            logs.status = "0";
            logs.statusName = "传输成功";
            logs.err_msg = errmsg;
            if (!string.IsNullOrWhiteSpace(errmsg))
            {
                logs.status = "-1";
                logs.err_msg = errmsg;
                logs.statusName = "传输失败";
            }
            logSqlList.Add(logs.ToAddSql());
            AppLogger.Info("插入医学影像检查报告表结束");
            #endregion

            #region 住院就诊记录表
            Console.WriteLine("1.1.1.查询当天住院就诊记录Start...");
            List<string> preSqlzyjzList = new List<string>();
            errmsg = "";
            List<TB_YL_ZY_Medical_Record> queryzyjzData = DBHelper.QueryTB_YL_ZY_Medical_Record();
            AppLogger.Info("插入住院就诊记录表开始····");
            foreach (var item in queryzyjzData)
            {
                TB_YL_ZY_Medical_Record yjs = new TB_YL_ZY_Medical_Record();
                yjs = Function.Mapping<TB_YL_ZY_Medical_Record, TB_YL_ZY_Medical_Record>(item);
                preSqlzyjzList.Add(yjs.ToAddSql());
            }
            DBHelper.ExecuteSqlTran(preSqlzyjzList, "U",out errmsg);

            logs = new Log_TbHealthyUpload();
            logs.createtime = DateTime.Now;
            logs.TabName = "TB_YL_ZY_Medical_Record";
            logs.status = "0";
            logs.statusName = "传输成功";
            logs.err_msg = errmsg;
            if (!string.IsNullOrWhiteSpace(errmsg))
            {
                logs.status = "-1";
                logs.err_msg = errmsg;
                logs.statusName = "传输失败";
            }
            logSqlList.Add(logs.ToAddSql());
            AppLogger.Info("住院就诊记录表结束");
            #endregion

            #region 住院医嘱明细表
            Console.WriteLine("1.1.1.查询当天住院医嘱明细Start...");
            List<string> preSqlzyyzmxList = new List<string>();
            errmsg = "";
            List<TB_CIS_DrAdvice_Detail> queryzyyzmxData = DBHelper.QueryTB_CIS_DrAdvice_Detail();
            AppLogger.Info("插入住院医嘱明细表开始····");
            foreach (var item in queryzyyzmxData)
            {
                TB_CIS_DrAdvice_Detail yjs = new TB_CIS_DrAdvice_Detail();
                yjs = Function.Mapping<TB_CIS_DrAdvice_Detail, TB_CIS_DrAdvice_Detail>(item);
                preSqlzyyzmxList.Add(yjs.ToAddSql());
            }
            DBHelper.ExecuteSqlTran(preSqlzyyzmxList, "U",out errmsg);

            logs = new Log_TbHealthyUpload();
            logs.createtime = DateTime.Now;
            logs.TabName = "TB_CIS_DrAdvice_Detail";
            logs.status = "0";
            logs.statusName = "传输成功";
            logs.err_msg = errmsg;
            if (!string.IsNullOrWhiteSpace(errmsg))
            {
                logs.status = "-1";
                logs.err_msg = errmsg;
                logs.statusName = "传输失败";
            }
            logSqlList.Add(logs.ToAddSql());
            AppLogger.Info("插入住院医嘱明细结束");
            #endregion

            #region 住院收费明细表
            Console.WriteLine("1.1.1.查询当天住院收费明细表Start...");
            List<string> preSqlzysfmxList = new List<string>();
            errmsg = "";
            List<TB_HIS_ZY_Fee_Detail> queryzysfmxData = DBHelper.QueryTB_HIS_ZY_Fee_Detail();
            AppLogger.Info("插入住院收费明细表开始····");
            foreach (var item in queryzysfmxData)
            {
                TB_HIS_ZY_Fee_Detail yjs = new TB_HIS_ZY_Fee_Detail();
                yjs = Function.Mapping<TB_HIS_ZY_Fee_Detail, TB_HIS_ZY_Fee_Detail>(item);
                preSqlzysfmxList.Add(yjs.ToAddSql());
            }
            DBHelper.ExecuteSqlTran(preSqlzysfmxList, "U",out errmsg);

            logs = new Log_TbHealthyUpload();
            logs.createtime = DateTime.Now;
            logs.TabName = "TB_HIS_ZY_Fee_Detail";
            logs.status = "0";
            logs.statusName = "传输成功";
            logs.err_msg = errmsg;
            if (!string.IsNullOrWhiteSpace(errmsg))
            {
                logs.status = "-1";
                logs.err_msg = errmsg;
                logs.statusName = "传输失败";
            }
            logSqlList.Add(logs.ToAddSql());
            AppLogger.Info("插入住院收费明细表结束");
            #endregion

            #region 手术明细表
            Console.WriteLine("1.1.1.查询当天手术明细表Start...");
            List<string> preSqlzyssmxList = new List<string>();
            errmsg = "";
            List<TB_Operation_Detail> queryzyssmxData = DBHelper.QueryTB_Operation_Detail();
            AppLogger.Info("插入手术明细表开始····");
            foreach (var item in queryzyssmxData)
            {
                TB_Operation_Detail yjs = new TB_Operation_Detail();
                yjs = Function.Mapping<TB_Operation_Detail, TB_Operation_Detail>(item);
                preSqlzyssmxList.Add(yjs.ToAddSql());
            }
            DBHelper.ExecuteSqlTran(preSqlzyssmxList, "U",out errmsg);

            logs = new Log_TbHealthyUpload();
            logs.createtime = DateTime.Now;
            logs.TabName = "TB_Operation_Detail";
            logs.status = "0";
            logs.statusName = "传输成功";
            logs.err_msg = errmsg;
            if (!string.IsNullOrWhiteSpace(errmsg))
            {
                logs.status = "-1";
                logs.err_msg = errmsg;
                logs.statusName = "传输失败";
            }
            logSqlList.Add(logs.ToAddSql());
            AppLogger.Info("插入手术明细表结束");
            #endregion

            #region 医院的科室字典表
            Console.WriteLine("1.1.1.查询当天医院的科室字典表Start...");
            List<string> preSqlkszdList = new List<string>();
            errmsg = "";
            List<TB_DIC_Department> queryzykszdData = DBHelper.QueryTB_DIC_Department();
            AppLogger.Info("插入医院的科室字典表开始····");
            foreach (var item in queryzykszdData)
            {
                TB_DIC_Department yjs = new TB_DIC_Department();
                yjs = Function.Mapping<TB_DIC_Department, TB_DIC_Department>(item);
                preSqlkszdList.Add(yjs.ToAddSql());
            }
            DBHelper.ExecuteSqlTran(preSqlkszdList, "U",out errmsg);
            logs = new Log_TbHealthyUpload();
            logs.createtime = DateTime.Now;
            logs.TabName = "TB_DIC_Department";
            logs.status = "0";
            logs.statusName = "传输成功";
            logs.err_msg = errmsg;
            if (!string.IsNullOrWhiteSpace(errmsg))
            {
                logs.status = "-1";
                logs.err_msg = errmsg;
                logs.statusName = "传输失败";
            }
            logSqlList.Add(logs.ToAddSql());
            AppLogger.Info("插入医院的科室字典表结束");
            #endregion

            #region 医护人员字典表
            Console.WriteLine("1.1.1.查询当天医护人员字典表Start...");
            List<string> preSqlryzdList = new List<string>();
            errmsg = "";
            List<TB_DIC_Practitioner> queryzyryzdData = DBHelper.QueryTB_DIC_Practitioner();
            AppLogger.Info("插入医护人员字典表开始····");
            foreach (var item in queryzyryzdData)
            {
                TB_DIC_Practitioner yjs = new TB_DIC_Practitioner();
                yjs = Function.Mapping<TB_DIC_Practitioner, TB_DIC_Practitioner>(item);
                preSqlryzdList.Add(yjs.ToAddSql());
            }
            DBHelper.ExecuteSqlTran(preSqlryzdList, "U",out errmsg);
            logs = new Log_TbHealthyUpload();
            logs.createtime = DateTime.Now;
            logs.TabName = "TB_DIC_Practitioner";
            logs.status = "0";
            logs.statusName = "传输成功";
            logs.err_msg = errmsg;
            if (!string.IsNullOrWhiteSpace(errmsg))
            {
                logs.status = "-1";
                logs.err_msg = errmsg;
                logs.statusName = "传输失败";
            }
            logSqlList.Add(logs.ToAddSql());
            AppLogger.Info("插入医护人员字典表结束");
            #endregion

            #region 药品目录字典表
            Console.WriteLine("1.1.1.查询当天医护人员字典表Start...");
            List<string> preSqlypmlzdList = new List<string>();
            errmsg = "";
            List<TB_DIC_MEDICINES> queryypmlzdData = DBHelper.QueryTB_DIC_MEDICINES();
            AppLogger.Info("插入医护人员字典表开始····");
            foreach (var item in queryypmlzdData)
            {
                TB_DIC_MEDICINES yjs = new TB_DIC_MEDICINES();
                yjs = Function.Mapping<TB_DIC_MEDICINES, TB_DIC_MEDICINES>(item);
                preSqlypmlzdList.Add(yjs.ToAddSql());
            }
            DBHelper.ExecuteSqlTran(preSqlypmlzdList, "U",out errmsg);

            logs = new Log_TbHealthyUpload();
            logs.createtime = DateTime.Now;
            logs.TabName = "TB_DIC_MEDICINES";
            logs.status = "0";
            logs.statusName = "传输成功";
            logs.err_msg = errmsg;
            if (!string.IsNullOrWhiteSpace(errmsg))
            {
                logs.status = "-1";
                logs.err_msg = errmsg;
                logs.statusName = "传输失败";
            }
            logSqlList.Add(logs.ToAddSql());
            AppLogger.Info("插入医护人员字典表结束");
            #endregion

            #region 非药品目录字典表
            Console.WriteLine("1.1.1.查询当天非药品目录字典表Start...");
            List<string> preSqlfypmlzdList = new List<string>();
            errmsg = "";
            List<TB_DIC_Materials> queryfypmlzdData = DBHelper.QueryTB_DIC_Materials();
            AppLogger.Info("插入医护人员字典表开始····");
            foreach (var item in queryfypmlzdData)
            {
                TB_DIC_Materials yjs = new TB_DIC_Materials();
                yjs = Function.Mapping<TB_DIC_Materials, TB_DIC_Materials>(item);
                preSqlfypmlzdList.Add(yjs.ToAddSql());
            }
            DBHelper.ExecuteSqlTran(preSqlfypmlzdList, "U",out errmsg);

            logs = new Log_TbHealthyUpload();
            logs.createtime = DateTime.Now;
            logs.TabName = "TB_DIC_Materials";
            logs.status = "0";
            logs.statusName = "传输成功";
            logs.err_msg = errmsg;
            if (!string.IsNullOrWhiteSpace(errmsg))
            {
                logs.status = "-1";
                logs.err_msg = errmsg;
                logs.statusName = "传输失败";
            }
            logSqlList.Add(logs.ToAddSql());
            AppLogger.Info("插入医护人员字典表结束");
            #endregion

            #region 日志记录
            DBHelper.ExecuteSqlTran(logSqlList, "N", out errmsg);
            #endregion
        }
    }
}
