using System.Collections.Generic;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.HIS.Domain.ValueObjects;
using Newtouch.Infrastructure;
using System.Data.SqlClient;
using Newtouch.Core.Common;
using System;

namespace Newtouch.HIS.DomainServices
{
    /// <summary>
    /// 
    /// </summary>
    public class GzybBaseInfoDmnService : DmnServiceBase, IGzybBaseInfoDmnService
    {
        /// <summary>
        /// 
        /// </summary>
        public GzybBaseInfoDmnService(IBaseDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }

        /// <summary>
        /// 获取贵州服务项目目录,最大流水号
        /// </summary>
        /// <returns></returns>
        public int GetItemCodeMaxLsh()
        {
            const string strSql = @" SELECT ISNULL(MAX(aaalsh),0)  FROM Gzyb_ybItemCode ";
            return this.FirstOrDefault<int>(strSql);
        }

        #region 重庆目录相关

        public string Header(string tbname)
        {
            var sql = "select   CASE when isnull(VER,'')  =''  or VER='无' then VER_NAME ELSE  VER END ver from " + tbname + " a with(nolock) order by a.VER DESC";

            return this.FirstOrDefault<string>(sql);

        }
        public IList<G_yb_mluCommon_Info> Get_G_yb_mluCommon_Info(Pagination pagination, string mlbh, string key)
        {
            var sqlstr = "";
            var pars = new List<SqlParameter>();
            switch (mlbh.Trim())
            {
                case "1301":
                    sqlstr = @"select  
MED_LIST_CODG ,REG_NAME,DRUGSTDCODE,DRUG_DOSFORM,DRUG_DOSFORM_NAME,DRUG_TYPE,DRUG_SPEC,REG_DOSFORM,REG_SPEC,EACH_DOS,OTC_FLAG
,PACMATL,EFCC_ATD, MIN_PAC_CNT,MIN_PACUNT,MIN_PREPUNT,DRUG_EXPY,MIN_PRCUNT,PRODENTP_CODE,PRODENTP_NAME,MKT_STAS,VER,
case VALI_FLAG when '1' then '是' when '0' then '否' else '' end VALI_FLAG,VER_NAME 
               from [dbo].G_yb_wm_tcmpat_info_b with(nolock) 
               where (@key like '' or MED_LIST_CODG like @key) or  (@key like '' or REG_NAME like @key) ";
                    break;
                case "1302":
                    sqlstr = @"select 
        MED_LIST_CODG,TCMHERB_NAME,MED_PART,CNVL_USED,NATFLA,CAT,BEGNDATE,ENDDATE,VER,
        case VALI_FLAG when '1' then '是' when '0' then '否' else '' end VALI_FLAG,VER_NAME,MLMS_NAME,EFCC_ATD,
        PSDG_MTD,ECY_TYPE,MLMS_CAT_SOUC
                    from [dbo].G_yb_tcmherb_info_b with(nolock)
                    where (@key like '' or MED_LIST_CODG like @key) or  (@key like '' or TCMHERB_NAME like @key)";
                    break;
                case "1303":
                    sqlstr = @"select 
MED_LIST_CODG,DRUG_PRODNAME,DOSFORM,EFCC_ATD,DRUG_SPEC,EACH_DOS,DRUG_TYPE,PACMATL,MIN_PAC_CNT,MIN_PACUNT,MIN_PREPUNT,EFCC_ATD
PSDG_MTD,VER,BEGNTIME,ENDTIME,case VALI_FLAG when '1' then '是' when '0' then '否' else '' end VALI_FLAG,VER_NAME
                    from [dbo].G_yb_selfprep_info_b with(nolock)
                    where (@key like '' or MED_LIST_CODG like @key) or  (@key like '' or DRUG_PRODNAME like @key)";
                    break;
                case "1305":
                    sqlstr = @"select 
MED_LIST_CODG,PRCUNT,PRCUNT_NAME,TRT_ITEM_DSCR,TRT_EXCT_CONT,VER,SERVITEM_TYPE,SERVITEM_NAME,BEGNDATE,ENDDATE,
case VALI_FLAG when '1' then '是' when '0' then '否' else '' end VALI_FLAG,VER_NAME
                    from [dbo].G_yb_trt_serv_b with(nolock)
                    where (@key like '' or MED_LIST_CODG like @key) or  (@key like '' or SERVITEM_NAME like @key)";
                    break;
                case "1306":
                    sqlstr = @"select 
MED_LIST_CODG,HI_GENNAME,SPEC,MCS_MATL,BEGNDATE,ENDDATE,PRODENTP_CODE,PRODENTP_NAME,VER,
case VALI_FLAG when '1' then '是' when '0' then '否' else '' end VALI_FLAG,VER_NAME
                    from [dbo].G_yb_mcs_info_b with(nolock) 
                    where (@key like '' or MED_LIST_CODG like @key) or  (@key like '' or HI_GENNAME like @key)";
                    break;
                case "1307":
                    sqlstr = @"select 
DIAG_LIST_ID,CPR,CPR_CODE_SCP,CPR_NAME,SEC_CODE_SCP,SEC_NAME,CGY_CODE,CGY_NAME,SOR_CODE,SOR_NAME,
DIAG_CODE,DIAG_NAME,USED_STD,NATSTD_DIAG_CODE,NATSTD_DIAG_NAME,CLNC_DIAG_CODE,CLNC_DIAG_NAME,VER,case VALI_FLAG when '1' then '是' when '0' then '否' else '' end VALI_FLAG,VER_NAME
                    from [dbo].G_yb_diag_list_b with(nolock)  
                    where (@key like '' or DIAG_CODE like @key) or  (@key  like '' or DIAG_NAME like @key)";
                    break;
                case "1308":
                    sqlstr = @"select 
OPRN_STD_LIST_ID,CPR,CPR_CODE_SCP,CPR_NAME,CGY_CODE,CGY_NAME,SOR_CODE,SOR_NAME,DTLS_CODE,DTLS_NAME,OPRN_OPRT_CODE,OPRN_OPRT_NAME,
VER,case VALI_FLAG when '1' then '是' when '0' then '否' else '' end VALI_FLAG,VER_NAME 
                    from [dbo].G_yb_oprn_std_b with(nolock) 
                    where (@key like '' or OPRN_OPRT_CODE like @key) or  (@key like '' or OPRN_OPRT_NAME like @key)";
                    break;
                case "1309":
                    sqlstr = @"select 
OPSP_DISE_CODE,OPSP_DISE_MAJCLS_NAME,OPSP_DISE_SUBD_CLSS_NAME,ADMDVS,VER,OPSP_DISE_NAME,
OPSP_DISE_MAJCLS_CODE,case VALI_FLAG when '1' then '是' when '0' then '否' else '' end VALI_FLAG,VER_NAME
                    from [dbo].G_yb_opsp_dise_list_b with(nolock)
                    where (@key like '' or OPSP_DISE_CODE like @key) or  (@key like '' or OPSP_DISE_MAJCLS_NAME like @key)";
                    break;
                case "1310":
                    sqlstr = @"select 
DISE_SETL_LIST_ID,BYDISE_SETL_LIST_CODE,BYDISE_SETL_DISE_NAME,QUA_OPRN_OPRT_CODE,QUA_OPRN_OPRT_NAME,VER,
case VALI_FLAG when '1' then '是' when '0' then '否' else '' end VALI_FLAG,VER_NAME
                    from [dbo].G_yb_dise_setl_list_b with(nolock) 
                    where (@key like '' or BYDISE_SETL_LIST_CODE like @key) or  (@key like '' or BYDISE_SETL_DISE_NAME like @key)";
                    break;
                case "1311":
                    sqlstr = @"select 
DAYSRG_TRT_LIST_ID,DAYSRG_DISE_LIST_CODE,DAYSRG_DISE_NAME,VER,QUA_OPRN_OPRT_CODE,QUA_OPRN_OPRT_NAME,
case VALI_FLAG when '1' then '是' when '0' then '否' else '' end VALI_FLAG,VER_NAME
from [dbo].G_yb_daysrg_trt_list_b with(nolock) where (@key like '' or DAYSRG_DISE_LIST_CODE  like @key) or  (@key like '' or DAYSRG_DISE_NAME like @key)";
                    break;
                case "1313":
                    sqlstr = @"select 
TMOR_MPY_ID,TMOR_CELL_TYPE_CODE,TMOR_CELL_TYPE,MPY_TYPE_CODE,MPY_TYPE,VER,case VALI_FLAG when '1' then '是' when '0' then '否' else '' end VALI_FLAG,VER_NAME
                    from [dbo].G_yb_tmor_mpy_b with(nolock) 
                    where (@key like '' or TMOR_CELL_TYPE_CODE like @key) or  (@key like '' or TMOR_CELL_TYPE  like @key)";
                    break;
                case "1314":
                    sqlstr = @"select 
TCM_DIAG_ID,CATY_CGY_CODE,CATY_CGY_NAME,SPCY_SYS_TAXA_CODE,SPCY_SYS_TAXA_NAME,DISE_TYPE_CODE,DISE_TYPE_NAME,VER,
case VALI_FLAG when '1' then '是' when '0' then '否' else '' end VALI_FLAG,VER_NAME
                    from [dbo].G_yb_tcm_diag_b with(nolock) 
                    where (@key like '' or DISE_TYPE_CODE like @key) or  (@key like '' or DISE_TYPE_NAME  like @key)";
                    break;
                case "1315":
                    sqlstr = @"select TCMSYMP_ID,SDS_CGY_CODE,SDS_CGY_NAME,SDS_ATTR_CODE,SDS_ATTR,SDS_TYPE_CODE,SDS_TYPE_NAME,
VER,case VALI_FLAG when '1' then '是' when '0' then '否' else '' end VALI_FLAG,VER_NAME
                    from [dbo].G_yb_tcmsymp_type_b with(nolock)
                    where (@key like '' or SDS_TYPE_CODE like @key) or  (@key like '' or SDS_TYPE_NAME like @key)";
                    break;
                default:
                    break;
            }
            try
            {
                pars.Add(new SqlParameter("@key", "%" + key + "%"));
                return QueryWithPage<G_yb_mluCommon_Info>(sqlstr.ToString(), pagination, pars.ToArray());
            }
            catch (Exception e)
            {

            }
            return null;

        }

        #endregion
    }
}

