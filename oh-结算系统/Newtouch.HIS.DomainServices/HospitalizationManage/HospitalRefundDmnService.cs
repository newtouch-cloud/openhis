using System;
using System.Linq;
using System.Collections.Generic;
using Newtouch.Infrastructure;
using System.Text;
using System.Data;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.Tools.DB;
using Newtouch.Common.Operator;
using System.Data.SqlClient;
using Newtouch.HIS.Domain.BusinessObjects.HospitalizationManage;
using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.DmnService;
using FrameworkBase.MultiOrg.Domain.IRepository;
using Newtouch.Core.Common.Exceptions;

namespace Newtouch.HIS.DomainServices
{
    /// <summary>
    /// 住院费用
    /// </summary>
    public class HospitalRefundDmnService : DmnServiceBase, IHospitalRefundDmnService
    {
        private readonly ISysDepartmentRepo _sysDepartmentRepo;

        public HospitalRefundDmnService(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }

        /// 根据住院号获取病人姓名
        /// </summary>
        /// <param name="zyh"></param>
        /// <returns></returns>
        public string GetPatInfoByZyh(string zyh)
        {
            //string strSQL = @" SELECT b.xm FROM dbo.zy_brjbxx a INNER JOIN  
            //                    dbo.xt_brjbxx b ON a.brnm=b.brnm WHERE zyh='" + zyh + "'";
            //DataTable dt = DbHelper.ExecuteSqlCommand2(strSQL.ToString());
            //if (dt.Rows.Count > 0)
            //{
            //    return dt.Rows[0];
            //    // txtXm.Text = dt.Rows[0]["xm"].ToString();
            //}

            string sql = @" SELECT b.xm FROM dbo.zy_brjbxx a INNER JOIN dbo.xt_brjbxx b ON a.patid=b.patid and b.OrganizeId=@OrganizeId WHERE zyh=@zyh and a.OrganizeId=@OrganizeId";
            SqlParameter[] para =
         {
                new SqlParameter("@zyh",zyh),
                new SqlParameter("@OrganizeId",OperatorProvider.GetCurrent().OrganizeId)
            };
            string name = this.FindList<string>(sql, para).FirstOrDefault();
            name = name == null ? "" : name;
            return name;
        }
        //根据科室名称获得科室代码
        public string GetKSByKSBH(string zxks)
        {
            var name = _sysDepartmentRepo.GetCodeByNameFirstOrDefault(zxks, OperatorProvider.GetCurrent().OrganizeId);
            if (!string.IsNullOrWhiteSpace(name))
            {
                return name;
            }
            return zxks;
        }

        /// <summary>
        /// 住院项目计费分组
        /// </summary>
        /// <param name="zyh">住院编号</param>
        /// <param name="zxks">执行科室编号</param>
        /// <param name="xmmc">收费项目</param>
        /// <param name="yrh">新生儿编号</param>

        public List<GridZyXmYpjfb> GetZyXmjfbGroup(string zyh, string zxks, string xmmc, string yrh)
        {
            List<KeyValuePair<string, string>> paramList = new List<KeyValuePair<string, string>>();
            //根据科室编号获得科室
            string ks = GetKSByKSBH(zxks);
            string strSQL = string.Format(@"
                        --根据住院号查询所有的计算记录，把已结和已冲销的记录进行对冲，得到未冲销的数据
                        select * --已冲销
                        from zy_js a
                        where not exists (select * from zy_js b where a.jsnm = b.cxjsnm and b.OrganizeId='{1}')
                        and zyh ='{0}' and jszt='1' and a.OrganizeId='{1}'
                        ", zyh,OperatorProvider.GetCurrent().OrganizeId);
            DataTable cxJsDt = DbHelper.ExecuteSqlCommand2(strSQL);

            string where = "0";
            foreach (DataRow dr in cxJsDt.Rows)
            {
                where += dr["jsnm"].ToString() + ",";
            }
            where = where.TrimEnd(',');

            strSQL = string.Format(@"where not exists --该住院号下已冲销和未计费的数据
( 
	select * 
	from zy_jsmx b 
	where ZY_XMJFB.jfbbh = b.xmjfbbh and b.yzlx='2' and b.OrganizeId='{1}' and ZY_XMJFB.OrganizeId='{1}'
	and b.jsnm in ({0}) --该住院号下未冲销的数据
) ", where,OperatorProvider.GetCurrent().OrganizeId);
            StringBuilder sqlBuilder = new StringBuilder("SELECT tdrq, ZYH,XT_SFXM.SFXMCode SFXM,1 AS IS_SFXM,SFXMMC, JFDW, SUM(sl) AS SL, ZY_XMJFB.DJ FROM ZY_XMJFB ");
            sqlBuilder.AppendLine("LEFT JOIN ( ");
            sqlBuilder.AppendLine("    SELECT * FROM NewtouchHIS_Base..V_S_xt_sfxm ");
            sqlBuilder.AppendLine(") AS XT_SFXM ON ZY_XMJFB.SFXM = XT_SFXM.SFXMCode AND ZY_XMJFB.SFXM = XT_SFXM.SFXMCode AND XT_SFXM.OrganizeId=@OrganizeId");
            sqlBuilder.AppendLine(strSQL);
            sqlBuilder.AppendLine(" AND  XT_SFXM.SFXMCode IS NOT NULL AND ZY_XMJFB.OrganizeId = @OrganizeId");
            SqlParameter[] param = { new SqlParameter("@zyh", zyh) };
            if (!string.IsNullOrWhiteSpace(zyh))
            {
                sqlBuilder.AppendLine("AND ZYH = @zyh ");
                paramList.Add(new KeyValuePair<string, string>("@zyh", zyh));
            }
            if (!string.IsNullOrWhiteSpace(zxks))
            {
                sqlBuilder.AppendLine("AND ksmc = @zxks AND KS = @ks ");
                paramList.Add(new KeyValuePair<string, string>("@zxks", zxks));
                paramList.Add(new KeyValuePair<string, string>("@ks", ks));
            }
            if (!string.IsNullOrWhiteSpace(xmmc))
            {
                sqlBuilder.AppendLine("AND XT_SFXM.SFXMMC LIKE @sfxmmc ");
                paramList.Add(new KeyValuePair<string, string>("@sfxmmc", string.Format("%{0}%", xmmc)));
            }
            //if (!string.IsNullOrWhiteSpace(yrh))
            //{
            //    sqlBuilder.AppendLine("AND XSEBH = @xsebh ");
            //    paramList.Add(new KeyValuePair<string, string>("@xsebh", yrh));
            //}
            sqlBuilder.AppendLine("GROUP BY tdrq, ZYH,XT_SFXM.SFXMCode,SFXMMC,JFDW,ZY_XMJFB.DJ order by tdrq desc");
            paramList.Add(new KeyValuePair<string, string>("@OrganizeId", OperatorProvider.GetCurrent().OrganizeId));
            try
            {
                //var resultData = FindList<OptimAccInfoDto>(sqlBuilder.ToString(), paramList);
                var resultData = DbHelper.GetDataTableBySql(sqlBuilder.ToString(), paramList);
                return ToMod(resultData);
            }
            catch (Exception ex)
            {
                throw new FailedCodeException(ex.Message);
            }

        }
        public List<GridZyXmYpjfb> ToMod(DataTable data)
        {
            var resultData = new List<GridZyXmYpjfb>();
            for (int i = 0; i < data.Rows.Count; i++)
            {
                int sl = Convert.ToInt32(data.Rows[i]["SL"]);
                if (sl > 0)
                {
                    //项目的数量大于0的才显示
                    var gr = new GridZyXmYpjfb();
                    gr.ZYH = data.Rows[i]["ZYH"].ToString();
                    gr.SL = data.Rows[i]["SL"].ToString();
                    //gr.XSEBH = data.Rows[i]["XSEBH"].ToString();
                    gr.SFXM = data.Rows[i]["SFXM"].ToString();
                    gr.IS_SFXM = data.Rows[i]["IS_SFXM"].ToString();
                    gr.CreateTime = Convert.ToDateTime(data.Rows[i]["tdrq"]).ToString("G");     //2009-09-06 10:56:13
                    gr.SFXMMC = data.Rows[i]["SFXMMC"].ToString();
                    gr.JFDW = data.Rows[i]["JFDW"].ToString();
                    gr.DJ = data.Rows[i]["DJ"].ToString();
                    resultData.Add(gr);
                }
            }
            return resultData;
        }


        /// 住院药品计费分组
        /// </summary>
        /// <param name="zyh">住院编号</param>
        /// <param name="zxks">执行科室编号</param>
        /// <param name="xmmc">收费项目</param>
        /// <param name="yrh">新生儿编号</param>
        /// <returns></returns>
        public List<GridZyXmYpjfb> GetZyYpjfbGroup(string zyh, string zxks, string xmmc, string yrh)
        {
            List<KeyValuePair<string, string>> paramList = new List<KeyValuePair<string, string>>();

            string ks = GetKSByKSBH(zxks);
            string strSQL = string.Format(@"
                                            --根据住院号查询所有的计算记录，把已结和已冲销的记录进行对冲，得到未冲销的数据
                                            select * --已冲销
                                            from zy_js a
                                            where not exists (select * from zy_js b where a.jsnm = b.cxjsnm and a.OrganizeId='{1}')
                                            and zyh ='{0}' and jszt='1' and a.OrganizeId='{1}'
                                            ", zyh,OperatorProvider.GetCurrent().OrganizeId);
            DataTable cxJsDt = DbHelper.ExecuteSqlCommand2(strSQL);

            string where = "0";
            foreach (DataRow dr in cxJsDt.Rows)
            {
                where += dr["jsnm"].ToString() + ",";
            }
            where = where.TrimEnd(',');

            strSQL = string.Format(@"where not exists --该住院号下已冲销和未计费的数据
                                    ( 
	                                    select * 
	                                    from zy_jsmx b 
	                                    where ZY_YPJFB.jfbbh = b.ypjfbbh and b.yzlx='1' and b.OrganizeId='{1}'
	                                    and b.jsnm in ({0}) --该住院号下未冲销的数据
                                    )", where,OperatorProvider.GetCurrent().OrganizeId);

            StringBuilder sqlBuilder = new StringBuilder("select tdrq, zyh,xt_yp.ypCode as sfxm,0 as is_sfxm, ypmc as sfxmmc, jfdw, sum(sl) as sl, zy_ypjfb.dj from zy_ypjfb ");
            sqlBuilder.AppendLine("left join ( ");
            sqlBuilder.AppendLine("    select * from NewtouchHIS_Base..V_S_xt_yp ");
            sqlBuilder.AppendLine(") as xt_yp on zy_ypjfb.yp = xt_yp.ypCode and xt_yp.OrganizeId=@OrganizeId");
            sqlBuilder.AppendLine(strSQL);
            sqlBuilder.AppendLine(" and  xt_yp.ypCode is not null and zy_ypjfb.OrganizeId=@OrganizeId");

            if (!string.IsNullOrEmpty(zyh))
            {
                sqlBuilder.AppendLine("AND ZYH = @zyh ");
                paramList.Add(new KeyValuePair<string, string>("@zyh", zyh));
            }
            if (!string.IsNullOrEmpty(zxks))
            {
                sqlBuilder.AppendLine("AND ksmc = @zxks AND KS = @ks ");
                paramList.Add(new KeyValuePair<string, string>("@zxks", zxks));
                paramList.Add(new KeyValuePair<string, string>("@ks", ks));
            }
            if (!string.IsNullOrEmpty(xmmc))
            {
                sqlBuilder.AppendLine("AND XT_YP.YPMC LIKE @ypmc ");
                paramList.Add(new KeyValuePair<string, string>("@ypmc", string.Format("%{0}%", xmmc)));
            }
            //if (!string.IsNullOrEmpty(yrh))
            //{
            //    sqlBuilder.AppendLine("AND XSEBH = @xsebh ");
            //    paramList.Add(new KeyValuePair<string, string>("@xsebh", yrh));
            //}

            sqlBuilder.AppendLine("group by tdrq, zyh,xt_yp.ypCode,ypmc,jfdw,zy_ypjfb.dj order by tdrq desc");
            paramList.Add(new KeyValuePair<string, string>("@OrganizeId", OperatorProvider.GetCurrent().OrganizeId));

            var resultData = DbHelper.GetDataTableBySql(sqlBuilder.ToString(), paramList);
            return ToMod(resultData);
        }

        /// <summary>
        /// 住院药品计费明细
        /// </summary>
        /// <param name="zyh">住院号</param>
        /// <param name="xsebh">新生儿编号</param>
        /// <param name="ypbh">药品编号</param>
        /// <returns></returns>
        public List<gridZyXmYpItemsjfb> GetZyYpjfbList(string zyh, string xsebh, string ypCode, string CreateTime)
        {
            List<KeyValuePair<string, string>> paramList = new List<KeyValuePair<string, string>>();
            StringBuilder sqlBuilder = new StringBuilder("WITH ZY_YPJFB_SET ");
            sqlBuilder.AppendLine("AS ( ");
            sqlBuilder.AppendLine("    SELECT JFBBH,TDRQ,ZY_YPJFB.YP,YPMC,JFDW,KS,ZY_YPJFB.DJ,");
            sqlBuilder.AppendLine("    CASE WHEN YZZT = 1 THEN SL ELSE 0 END AS SL,");
            sqlBuilder.AppendLine("    CASE WHEN YZZT = 1 THEN 0 ELSE SL END AS RETURN_SL,YZZT,CXZYJFBBH FROM ZY_YPJFB ");
            sqlBuilder.AppendLine("    LEFT JOIN ( ");
            sqlBuilder.AppendLine("        SELECT * FROM NewtouchHIS_Base..V_S_xt_yp ");
            sqlBuilder.AppendLine("    ) AS XT_YP ON ZY_YPJFB.YP = XT_YP.YPCODE and XT_YP.OrganizeId=@OrganizeId  WHERE 1 = 1  and ZY_YPJFB.OrganizeId=@OrganizeId");
            if (false == string.IsNullOrEmpty(zyh))
            {
                sqlBuilder.AppendLine("AND ZYH = @zyh ");
                paramList.Add(new KeyValuePair<string, string>("@zyh", zyh));
            }
            if (false == string.IsNullOrEmpty(xsebh))
            {
                sqlBuilder.AppendLine("AND XSEBH = @xsebh ");
                paramList.Add(new KeyValuePair<string, string>("@xsebh", xsebh));
            }
            if (false == string.IsNullOrEmpty(ypCode))
            {
                sqlBuilder.AppendLine("AND ZY_YPJFB.YP = @ypCode ");
                paramList.Add(new KeyValuePair<string, string>("@ypCode", ypCode));
            }
            if (false == string.IsNullOrEmpty(CreateTime))
            {
                sqlBuilder.AppendLine("AND ZY_YPJFB.tdrq = @tdrq ");
                paramList.Add(new KeyValuePair<string, string>("@tdrq", CreateTime));
            }
            sqlBuilder.AppendLine(") ");
            sqlBuilder.AppendLine("SELECT JFBBH,TDRQ,YP,YPMC,JFDW,KS,DJ,SL,RETURN_SL,(SL+ISNULL(SUM_RETURN_SL,0)) AS REMAIN,YZZT,ZY_YPJFB_SET.CXZYJFBBH FROM ZY_YPJFB_SET ");
            sqlBuilder.AppendLine("LEFT JOIN ( ");
            sqlBuilder.AppendLine("    SELECT CXZYJFBBH,SUM(RETURN_SL) AS SUM_RETURN_SL FROM ZY_YPJFB_SET WHERE CXZYJFBBH <> 0  GROUP BY CXZYJFBBH ");
            sqlBuilder.AppendLine(") AS RETURN_SET ON ZY_YPJFB_SET.JFBBH = RETURN_SET.CXZYJFBBH ");
            sqlBuilder.AppendLine("ORDER BY CASE WHEN ZY_YPJFB_SET.CXZYJFBBH <> 0 THEN ZY_YPJFB_SET.CXZYJFBBH ELSE JFBBH END ");

            paramList.Add(new KeyValuePair<string, string>("@OrganizeId", OperatorProvider.GetCurrent().OrganizeId));
            var data = DbHelper.GetDataTableBySql(sqlBuilder.ToString(), paramList);
            return ToModMx(data, "yp");
        }

        /// <summary>
        /// 根据药品和项目返回同一实体结构的数据类型
        /// </summary>
        /// <param name="data"></param>
        /// <param name="type">区分是否为药品</param>
        /// <returns></returns>
        public List<gridZyXmYpItemsjfb> ToModMx(DataTable data, string type)
        {
            var resultData = new List<gridZyXmYpItemsjfb>();
            for (int i = 0; i < data.Rows.Count; i++)
            {
                var mx = new gridZyXmYpItemsjfb();
                mx.JFBBH = data.Rows[i]["jfbbh"].ToString();
                mx.CXZYJFBBH = data.Rows[i]["cxzyjfbbh"].ToString();
                mx.CreateTime = Convert.ToDateTime(data.Rows[i]["TDRQ"]).ToString("G");     //2009-09-06 10:56:13
                if (type == "yp")
                {
                    mx.SFXMCode = data.Rows[i]["YP"].ToString();
                    mx.SFXMMC = data.Rows[i]["YPMC"].ToString();
                }
                else
                {
                    mx.SFXMCode = data.Rows[i]["SFXM"].ToString();
                    mx.SFXMMC = data.Rows[i]["SFXMMC"].ToString();
                }
                mx.JFDW = data.Rows[i]["JFDW"].ToString();
                mx.SL = data.Rows[i]["SL"].ToString();
                mx.RETURN_SL = data.Rows[i]["RETURN_SL"].ToString();
                resultData.Add(mx);
            }
            return resultData;
        }
        /// <summary>
        /// 住院项目计费明细
        /// </summary>
        /// <param name="zyh"></param>
        /// <param name="xsebh"></param>
        /// <param name="sfxmbh"></param>
        /// <returns></returns>
        public List<gridZyXmYpItemsjfb> GetZyXmjfbList(string zyh, string xsebh, string sfxm, string CreateTime)
        {
            List<KeyValuePair<string, string>> paramList = new List<KeyValuePair<string, string>>();
            StringBuilder sqlBuilder = new StringBuilder("WITH ZY_XMJFB_SET AS ( ");
            sqlBuilder.AppendLine("    SELECT JFBBH,TDRQ,ZY_XMJFB.SFXM,SFXMMC,JFDW,KS,ZY_XMJFB.DJ,");
            sqlBuilder.AppendLine("    CASE WHEN YZZT = 1 THEN SL ELSE 0 END AS SL,");
            sqlBuilder.AppendLine("    CASE WHEN YZZT = 1 THEN 0 ELSE SL END AS RETURN_SL,");
            sqlBuilder.AppendLine("    YZZT,CXZYJFBBH FROM ZY_XMJFB ");
            sqlBuilder.AppendLine("    LEFT JOIN ( ");
            sqlBuilder.AppendLine("        SELECT * FROM NewtouchHIS_Base..V_S_xt_sfxm ");
            sqlBuilder.AppendLine("    ) AS XT_SFXM ON ZY_XMJFB.SFXM = XT_SFXM.SFXMCode and XT_SFXM.OrganizeId=@OrganizeId WHERE 1 = 1 and ZY_XMJFB.OrganizeId=@OrganizeId ");
            if (false == string.IsNullOrEmpty(zyh))
            {
                sqlBuilder.AppendLine("AND ZYH = @zyh ");
                paramList.Add(new KeyValuePair<string, string>("@zyh", zyh));
            }
            if (false == string.IsNullOrEmpty(xsebh) && xsebh != "0")
            {
                sqlBuilder.AppendLine("AND XSEBH = @xsebh ");
                paramList.Add(new KeyValuePair<string, string>("@xsebh", xsebh));
            }
            if (false == string.IsNullOrEmpty(sfxm))
            {
                sqlBuilder.AppendLine("AND ZY_XMJFB.SFXM = @sfxm ");
                paramList.Add(new KeyValuePair<string, string>("@sfxm", sfxm));
            }
            if (false == string.IsNullOrEmpty(CreateTime))
            {
                sqlBuilder.AppendLine("AND ZY_XMJFB.tdrq = @tdrq ");
                paramList.Add(new KeyValuePair<string, string>("@tdrq", CreateTime));
            }
            sqlBuilder.AppendLine(") ");
            sqlBuilder.AppendLine("SELECT JFBBH,TDRQ,SFXM,SFXMMC,JFDW,KS,DJ,SL,RETURN_SL,(SL+ISNULL(SUM_RETURN_SL,0)) AS REMAIN,YZZT,ZY_XMJFB_SET.CXZYJFBBH FROM ZY_XMJFB_SET ");
            sqlBuilder.AppendLine("LEFT JOIN ( ");
            sqlBuilder.AppendLine("    SELECT CXZYJFBBH,SUM(RETURN_SL) AS SUM_RETURN_SL FROM ZY_XMJFB_SET WHERE CXZYJFBBH <> 0 GROUP BY CXZYJFBBH ");
            sqlBuilder.AppendLine(") AS RETURN_SET ON ZY_XMJFB_SET.JFBBH = RETURN_SET.CXZYJFBBH ");
            sqlBuilder.AppendLine("ORDER BY CASE WHEN ZY_XMJFB_SET.CXZYJFBBH <> 0 THEN ZY_XMJFB_SET.CXZYJFBBH ELSE JFBBH END ");

            paramList.Add(new KeyValuePair<string, string>("@OrganizeId", OperatorProvider.GetCurrent().OrganizeId));
            var data = DbHelper.GetDataTableBySql(sqlBuilder.ToString(), paramList);
            return ToModMx(data, "xm");
        }
        /// <summary>
        /// 药品退费
        /// </summary>
        /// <param name="returnDict"></param>
        /// <returns></returns>
        public string SaveReturnZyyp(Dictionary<string, decimal> returnDict)
        {
            string result = "退费成功！";

            List<string> insertSql = new List<string>();
            List<string> keyList = new List<string>(returnDict.Keys);
            string strSql = "select * from zy_ypjfb " + string.Format(" WHERE OrganizeId='{1}' and JFBBH IN ({0}) ", string.Join(",", keyList.ToArray()), OperatorProvider.GetCurrent().OrganizeId);
            var zyYpjfbList = this.FindList<HospDrugBillingEntity>(strSql.ToString());
            var YPList = new List<HospDrugBillingEntity>();
            foreach (var tmpZyYpjfb in zyYpjfbList)
            {
                tmpZyYpjfb.cxzyjfbbh = tmpZyYpjfb.jfbbh;
                tmpZyYpjfb.sl = -1 * returnDict[tmpZyYpjfb.jfbbh.ToString()];
                tmpZyYpjfb.CreatorCode = OperatorProvider.GetCurrent().UserCode; //获取当前用户工号;
                tmpZyYpjfb.CreateTime = DateTime.Now;
                tmpZyYpjfb.jfbbh = EFDBBaseFuncHelper.Instance.GetNewPrimaryKeyInt("zy_ypjfb");
                tmpZyYpjfb.yzzt = "2";
                tmpZyYpjfb.zt = "1";
                tmpZyYpjfb.OrganizeId = OperatorProvider.GetCurrent().OrganizeId;

                YPList.Add(tmpZyYpjfb);
            }
            try
            {
                var db = new EFDbTransaction(this._databaseFactory);    //这怎么开事务了
                db.Insert(YPList);
                db.Commit();
            }
            catch (Exception ex)
            {
                //  AppLogger.Info("报错：" + ex.InnerException);
                throw new FailedCodeException("HOSP_REFUNG_FAIL");
            }
            return result;
        }


        /// <summary>
        /// 项目退费
        /// </summary>
        /// <param name="returnDict"></param>
        /// <returns></returns>
        public string SaveReturnZyxm(Dictionary<string, decimal> returnDict)
        {
            string result = "退费成功！";
            List<string> insertSql = new List<string>();
            List<string> keyList = new List<string>(returnDict.Keys);
            string strSql = "select * from zy_xmjfb " + string.Format(" WHERE OrganizeId='{1}' and JFBBH IN ({0}) ", string.Join(",", keyList.ToArray()), OperatorProvider.GetCurrent().OrganizeId);
            var zyXmjfbList = this.FindList<HospItemBillingEntity>(strSql.ToString());
            var XMList = new List<HospItemBillingEntity>();
            foreach (var tmpZyXmjfb in zyXmjfbList)
            {
                tmpZyXmjfb.cxzyjfbbh = tmpZyXmjfb.jfbbh;
                tmpZyXmjfb.sl = -1 * returnDict[tmpZyXmjfb.jfbbh.ToString()];
                tmpZyXmjfb.CreatorCode = OperatorProvider.GetCurrent().UserCode; //获取当前用户工号;
                tmpZyXmjfb.CreateTime = DateTime.Now;
                tmpZyXmjfb.jfbbh = EFDBBaseFuncHelper.Instance.GetNewPrimaryKeyInt("zy_xmjfb");
                tmpZyXmjfb.yzzt = "2";
                tmpZyXmjfb.zt = "1";
                tmpZyXmjfb.OrganizeId = OperatorProvider.GetCurrent().OrganizeId;
                XMList.Add(tmpZyXmjfb);
            }
            try
            {
                var db = new EFDbTransaction(this._databaseFactory);
                db.Insert(XMList);
                db.Commit();
            }
            catch (Exception ex)
            {
                //  AppLogger.Info("报错：" + ex.InnerException);
                throw new FailedCodeException("HOSP_REFUNG_FAIL");
            }
            return result;
        }

    }

}
