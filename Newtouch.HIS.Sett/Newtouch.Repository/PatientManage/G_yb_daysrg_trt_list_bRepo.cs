using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Repository;
using Newtouch.Core.Common;
using Newtouch.HIS.Domain.Entity.PatientManage;
using Newtouch.HIS.Domain.IRepository.PatientManage;
using Newtouch.HIS.Domain.ReportTemplateVO;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Repository.PatientManage
{
    public class G_yb_daysrg_trt_list_bRepo : RepositoryBase<G_yb_daysrg_trt_list_bEntity>, IG_yb_daysrg_trt_list_bRepo
    {
        public G_yb_daysrg_trt_list_bRepo(IDefaultDatabaseFactory databaseFactory)
           : base(databaseFactory)
        {
        }

        public IList<G_yb_mluCommon_Info> Get_G_yb_mluCommon_Info(Pagination pagination,string mlbh,string key)
        {
            var sqlstr = "";
            var pars = new List<SqlParameter>();
            switch (mlbh.Trim())
            {
                case "1301":
                    sqlstr = @"select  MED_LIST_CODG ,REG_NAME,DRUGSTDCODE,DRUG_DOSFORM,DRUG_DOSFORM_NAME,DRUG_TYPE,DRUG_SPEC,REG_DOSFORM,REG_SPEC,EACH_DOS,OTC_FLAG
,PACMATL,EFCC_ATD, MIN_PAC_CNT,MIN_PACUNT,MIN_PREPUNT,DRUG_EXPY,MIN_PRCUNT,PRODENTP_CODE,PRODENTP_NAME,MKT_STAS,VALI_FLAG,VER_NAME 
from NewtouchHIS_Base.[dbo].G_yb_wm_tcmpat_info_b with(nolock) where (@key='' or MED_LIST_CODG=@key) or  (@key='' or REG_NAME=@key) ";
                    break;
                case "1302":
                    sqlstr = @"select MED_LIST_CODG,TCMHERB_NAME,MED_PART,CNVL_USED,NATFLA,CAT,BEGNDATE,ENDDATE,VALI_FLAG,VER_NAME,MLMS_NAME,EFCC_ATD,PSDG_MTD,ECY_TYPE,MLMS_CAT_SOUC
                        from NewtouchHIS_Base.[dbo].G_yb_tcmherb_info_b with(nolock) where (@key='' or MED_LIST_CODG=@key) or  (@key='' or TCMHERB_NAME=@key)";
                    break;
                case "1303":
                    sqlstr = @"select MED_LIST_CODG,DRUG_PRODNAME,DOSFORM,EFCC_ATD,DRUG_SPEC,EACH_DOS,DRUG_TYPE,PACMATL,MIN_PAC_CNT,MIN_PACUNT,MIN_PREPUNT,EFCC_ATD
PSDG_MTD,VALI_FLAG,BEGNTIME,ENDTIME,VER,VER_NAME
from NewtouchHIS_Base.[dbo].G_yb_selfprep_info_b with(nolock) where (@key='' or MED_LIST_CODG=@key) or  (@key='' or DRUG_PRODNAME=@key)";
                    break;
                case "1305":
                    sqlstr = @"select MED_LIST_CODG,PRCUNT,PRCUNT_NAME,TRT_ITEM_DSCR,TRT_EXCT_CONT,VALI_FLAG,SERVITEM_TYPE,SERVITEM_NAME,BEGNDATE,ENDDATE,VER,VER_NAME
from NewtouchHIS_Base.[dbo].G_yb_trt_serv_b with(nolock) where (@key='' or MED_LIST_CODG=@key) or  (@key='' or SERVITEM_NAME=@key)";
                    break;
                case "1306":
                    sqlstr = @"select MED_LIST_CODG,HI_GENNAME,SPEC,MCS_MATL,BEGNDATE,ENDDATE,PRODENTP_CODE,PRODENTP_NAME,VALI_FLAG,VER,VER_NAME
from NewtouchHIS_Base.[dbo].G_yb_mcs_info_b with(nolock) where (@key='' or MED_LIST_CODG=@key) or  (@key='' or HI_GENNAME=@key)";
                    break;
                case "1307":
                    sqlstr = @"select DIAG_LIST_ID,CPR,CPR_CODE_SCP,CPR_NAME,SEC_CODE_SCP,SEC_NAME,CGY_CODE,CGY_NAME,SOR_CODE,SOR_NAME,DIAG_CODE,DIAG_NAME,USED_STD,VER,VER_NAME
from NewtouchHIS_Base.[dbo].G_yb_diag_list_b with(nolock)  where (@key='' or DIAG_CODE=@key) or  (@key='' or DIAG_NAME=@key)";
                    break;
                case "1308":
                    sqlstr = @"select OPRN_STD_LIST_ID,CPR,CPR_CODE_SCP,CPR_NAME,CGY_CODE,CGY_NAME,SOR_CODE,SOR_NAME,DTLS_CODE,DTLS_NAME,OPRN_OPRT_CODE,OPRN_OPRT_NAME,
VALI_FLAG,VER,VER_NAME from NewtouchHIS_Base.[dbo].G_yb_oprn_std_b with(nolock) where (@key='' or OPRN_OPRT_CODE=@key) or  (@key='' or OPRN_OPRT_NAME=@key)";
                    break;
                case "1309":
                    sqlstr = @"select OPSP_DISE_CODE,OPSP_DISE_MAJCLS_NAME,OPSP_DISE_SUBD_CLSS_NAME,ADMDVS,VALI_FLAG,OPSP_DISE_NAME,OPSP_DISE_MAJCLS_CODE,VER,VER_NAME
from NewtouchHIS_Base.[dbo].G_yb_opsp_dise_list_b with(nolock) where (@key='' or OPSP_DISE_CODE=@key) or  (@key='' or OPSP_DISE_MAJCLS_NAME=@key)";
                    break;
                case "1310":
                    sqlstr = @"select DISE_SETL_LIST_ID,BYDISE_SETL_LIST_CODE,BYDISE_SETL_DISE_NAME,QUA_OPRN_OPRT_CODE,QUA_OPRN_OPRT_NAME,VALI_FLAG,VER,VER_NAME
from NewtouchHIS_Base.[dbo].G_yb_dise_setl_list_b with(nolock)  where (@key='' or BYDISE_SETL_LIST_CODE=@key) or  (@key='' or BYDISE_SETL_DISE_NAME=@key)";
                    break;
                case "1311":
                    sqlstr = @"select DAYSRG_TRT_LIST_ID,DAYSRG_DISE_LIST_CODE,DAYSRG_DISE_NAME,VALI_FLAG,QUA_OPRN_OPRT_CODE,QUA_OPRN_OPRT_NAME,VER,VER_NAME
from NewtouchHIS_Base.[dbo].G_yb_daysrg_trt_list_b with(nolock) where (@key='' or DAYSRG_DISE_LIST_CODE=@key) or  (@key='' or DAYSRG_DISE_NAME=@key)";
                    break;
                case "1313":
                    sqlstr = @"select TMOR_MPY_ID,TMOR_CELL_TYPE_CODE,TMOR_CELL_TYPE,MPY_TYPE_CODE,MPY_TYPE,VALI_FLAG,VER,VER_NAME
from NewtouchHIS_Base.[dbo].G_yb_tmor_mpy_b with(nolock)  where (@key='' or TMOR_CELL_TYPE_CODE=@key) or  (@key='' or TMOR_CELL_TYPE=@key)";
                    break;
                case "1314":
                    sqlstr = @"select TCM_DIAG_ID,CATY_CGY_CODE,CATY_CGY_NAME,SPCY_SYS_TAXA_CODE,SPCY_SYS_TAXA_NAME,DISE_TYPE_CODE,DISE_TYPE_NAME,VALI_FLAG,VER,VER_NAME
from NewtouchHIS_Base.[dbo].G_yb_tcm_diag_b with(nolock)  where (@key='' or DISE_TYPE_CODE=@key) or  (@key='' or DISE_TYPE_NAME=@key)";
                    break;
                case "1315":
                    sqlstr = @"select TCMSYMP_ID,SDS_CGY_CODE,SDS_CGY_NAME,SDS_ATTR_CODE,SDS_ATTR,SDS_TYPE_CODE,SDS_TYPE_NAME,VALI_FLAG,VER,VER_NAME
from NewtouchHIS_Base.[dbo].G_yb_tcmsymp_type_b with(nolock) where (@key='' or SDS_TYPE_CODE=@key) or  (@key='' or SDS_TYPE_NAME=@key)";
                    break;
                default:
                    break;
            }
            try {
                pars.Add(new SqlParameter("@key", key));
                return QueryWithPage<G_yb_mluCommon_Info>(sqlstr.ToString(), pagination, pars.ToArray());
            }
            catch (Exception e) {

            }
            return null;
          
        }

        public IList<G_yb_daysrg_trt_list_bVO> G_yb_daysrg_trt_list_b(string tbname) {

            var sql = "select * from NewtouchHIS_Base.[dbo]."+tbname+" with(nolock)";

            return this.FindList<G_yb_daysrg_trt_list_bVO>(sql);
        }

        public IList<G_yb_diag_list_bVO> G_yb_diag_list_b(string tbname)
        {
            var sql = "select * from NewtouchHIS_Base.[dbo]." + tbname + " with(nolock)";

            return this.FindList<G_yb_diag_list_bVO>(sql);
        }

        public IList<G_yb_dise_setl_list_bVO> G_yb_dise_setl_list_b(string tbname)
        {
            var sql = "select * from NewtouchHIS_Base.[dbo]." + tbname + " with(nolock)";

            return this.FindList<G_yb_dise_setl_list_bVO>(sql);
        }

        public IList<G_yb_mcs_info_bVO> G_yb_mcs_info_b(string tbname)
        {
            var sql = "select * from NewtouchHIS_Base.[dbo]." + tbname + " with(nolock)";

            return this.FindList<G_yb_mcs_info_bVO>(sql);
        }

        public IList<G_yb_oprn_std_bVO> G_yb_oprn_std_b(string tbname)
        {
            var sql = "select * from NewtouchHIS_Base.[dbo]." + tbname + " with(nolock)";

            return this.FindList<G_yb_oprn_std_bVO>(sql);
        }

        public IList<G_yb_opsp_dise_list_bVO> G_yb_opsp_dise_list_b(string tbname)
        {
            var sql = "select * from NewtouchHIS_Base.[dbo]." + tbname + " with(nolock)";

            return this.FindList<G_yb_opsp_dise_list_bVO>(sql);
        }

        public IList<G_yb_selfprep_info_bVO> G_yb_selfprep_info_b(string tbname)
        {
            var sql = "select * from NewtouchHIS_Base.[dbo]." + tbname + " with(nolock)";

            return this.FindList<G_yb_selfprep_info_bVO>(sql);
        }

        public IList<G_yb_tcmherb_info_bVO> G_yb_tcmherb_info_b(string tbname)
        {
            var sql = "select * from NewtouchHIS_Base.[dbo]." + tbname + " with(nolock)";

            return this.FindList<G_yb_tcmherb_info_bVO>(sql);
        }

        public IList<G_yb_tcmsymp_type_bVO> G_yb_tcmsymp_type_b(string tbname)
        {
            var sql = "select * from NewtouchHIS_Base.[dbo]." + tbname + " with(nolock)";

            return this.FindList<G_yb_tcmsymp_type_bVO>(sql);
        }

        public IList<G_yb_tcm_diag_bVO> G_yb_tcm_diag_b(string tbname)
        {
            var sql = "select * from NewtouchHIS_Base.[dbo]." + tbname + " with(nolock)";

            return this.FindList<G_yb_tcm_diag_bVO>(sql);
        }

        public IList<G_yb_tmor_mpy_bVO> G_yb_tmor_mpy_b(string tbname)
        {
            var sql = "select * from NewtouchHIS_Base.[dbo]." + tbname + " with(nolock)";

            return this.FindList<G_yb_tmor_mpy_bVO>(sql);
        }

        public IList<G_yb_trt_serv_bVO> G_yb_trt_serv_b(string tbname)
        {
            var sql = "select * from NewtouchHIS_Base.[dbo]." + tbname + " with(nolock)";

            return this.FindList<G_yb_trt_serv_bVO>(sql);
        }

        public IList<G_yb_wm_tcmpat_info_bVO> G_yb_wm_tcmpat_info_b(Pagination pagination, string tbname)
        {
            var sql = "select * from NewtouchHIS_Base.[dbo]." + tbname + " with(nolock)";

            return this.FindList<G_yb_wm_tcmpat_info_bVO>(sql);
        }

        public string Header(string tbname)
        {
            //var sql = string.Format(@"SELECT
            //            C.value AS Explain
            //            FROM sys.tables A
            //            INNER JOIN sys.columns B ON B.object_id = A.object_id
            //            LEFT JOIN sys.extended_properties C ON C.major_id = B.object_id AND C.minor_id = B.column_id
            //            WHERE A.name = '"+tbname+"'");
            //return this.FindList<string>(sql);
            var sql = "select isnull(VER,'0000') ver from NewtouchHIS_Base.[dbo]." + tbname + " order by VER DESC";

            return this.FirstOrDefault<string>(sql);

        }


        public void DataListSQl(string tbname, string path)
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
                        //else
                        //{
                        //    string dtvalues = sLine.Replace("\t", "\',\'");
                        //    dtvalues = "'" + dtvalues + "'";
                        //    //a = "'4','4','4','4','4','4','4','4','4','4','4','2021-11-10T14:17:58.14','2021-11-10T14:17:58.14','4','4','4','4','4','2021-11-10T14:17:58.14','4','4','4','4','2021-11-10T14:17:58.14','2021-11-10T14:17:58.14'";
                        //    var sql = string.Format(@"insert into " + tbname.Trim() + " values(" + dtvalues.Trim() + ")");
                        //    ExecuteSqlCommand(sql);

                        //}


                        string[] sRecordKbn = sLine.Split('\t');//过滤空格
                        var a = "";
                        for (int i = 0; i < sRecordKbn.Length-1; i++)
                        {

                            if (sRecordKbn[i].Contains("CST"))
                            {
                                sRecordKbn[i]= DateTime.ParseExact(sRecordKbn[i], "ddd MMM dd HH:mm:ss CST yyyy", new CultureInfo("en-us")).ToString();
                            }

                            a += sRecordKbn[i] + "\',\'";
                            //foreach (string s in sRecordKbn)
                            //{
                            //    a += s + "\',\'";
                            //    //string dtvalues = s.Replace("\t", "\',\'");
                            //    //dtvalues = "'" + dtvalues + "'";
                            //    ////a = "'4','4','4','4','4','4','4','4','4','4','4','2021-11-10T14:17:58.14','2021-11-10T14:17:58.14','4','4','4','4','4','2021-11-10T14:17:58.14','4','4','4','4','2021-11-10T14:17:58.14','2021-11-10T14:17:58.14'";
                            //    //var sql = string.Format(@"insert into " + tbname.Trim() + " values(" + dtvalues.Trim() + ")");
                            //    //ExecuteSqlCommand(sql);
                            //}
                            
                        }

                        a = "\'" + a.Substring(0, a.Length - 2);
                        //tbname = "G_yb_tcmherb_info_b";
                        var sql = string.Format(@"insert into NewtouchHIS_Base.[dbo]." + tbname.Trim() + " values(" + a + ")");
                        ExecuteSqlCommand(sql);
                    }
                }

            }
            
        }
        public List<MedinsDaySetlResult> DataListSQl_V2(string tbname, string path)
        {
            var va = new List<MedinsDaySetlResult>();
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
                        //else
                        //{
                        //    string dtvalues = sLine.Replace("\t", "\',\'");
                        //    dtvalues = "'" + dtvalues + "'";
                        //    //a = "'4','4','4','4','4','4','4','4','4','4','4','2021-11-10T14:17:58.14','2021-11-10T14:17:58.14','4','4','4','4','4','2021-11-10T14:17:58.14','4','4','4','4','2021-11-10T14:17:58.14','2021-11-10T14:17:58.14'";
                        //    var sql = string.Format(@"insert into " + tbname.Trim() + " values(" + dtvalues.Trim() + ")");
                        //    ExecuteSqlCommand(sql);

                        //}


                        string[] sRecordKbn = sLine.Split('\t');//过滤空格
                        var a = "";
                        for (int i = 0; i < sRecordKbn.Length - 1; i++)
                        {

                            if (sRecordKbn[i].Contains("CST"))
                            {
                                sRecordKbn[i] = DateTime.ParseExact(sRecordKbn[i], "ddd MMM dd HH:mm:ss CST yyyy", new CultureInfo("en-us")).ToString();
                            }

                            a += sRecordKbn[i] + "\',\'";
                            //foreach (string s in sRecordKbn)
                            //{
                            //    a += s + "\',\'";
                            //    //string dtvalues = s.Replace("\t", "\',\'");
                            //    //dtvalues = "'" + dtvalues + "'";
                            //    ////a = "'4','4','4','4','4','4','4','4','4','4','4','2021-11-10T14:17:58.14','2021-11-10T14:17:58.14','4','4','4','4','4','2021-11-10T14:17:58.14','4','4','4','4','2021-11-10T14:17:58.14','2021-11-10T14:17:58.14'";
                            //    //var sql = string.Format(@"insert into " + tbname.Trim() + " values(" + dtvalues.Trim() + ")");
                            //    //ExecuteSqlCommand(sql);
                            //}

                        }

                        a = "\'" + a.Substring(0, a.Length - 2);
                        //tbname = "G_yb_tcmherb_info_b";
                        string money = a.Split(',')[2].ToString().Replace("(", "");
                        money= money.Replace(")", "");
                        
                        var identity = new MedinsDaySetlResult
                        {
                            psn_no = a.Split(',')[0].ToString(),
                            medinsSetlId = a.Split(',')[1].ToString(),
                            medfee_sumamt = money.Split('|')[0].ToString(),
                            fund_pay_sumamt = money.Split('|')[1].ToString(),
                            acct_pay = money.Split('|')[2].ToString()
                        };
                        va.Add(identity);
                        
                    }
                }

            }

            return va;
        }



    }
}
