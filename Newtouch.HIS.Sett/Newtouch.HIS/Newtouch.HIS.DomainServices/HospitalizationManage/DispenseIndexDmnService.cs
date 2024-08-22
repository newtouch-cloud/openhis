using FrameworkBase.MultiOrg.DmnService;
using FrameworkBase.MultiOrg.Infrastructure;
using Newtouch.Core.Common;
using Newtouch.HIS.Domain.IDomainServices.HospitalizationManage;
using Newtouch.HIS.Domain.ValueObjects.HospitalizationManage;
using Newtouch.HIS.Sett.Request.HospitalizationPharmacy;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Text;

namespace Newtouch.HIS.DomainServices.HospitalizationManage
{
    public class DispenseIndexDmnService : DmnServiceBase, IDispenseIndexDmnService
    {
        public DispenseIndexDmnService(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }

        #region 住院发药

        /// <summary>
        /// 住院发药病区
        /// </summary>
        /// <param name="bq">病区</param>
        /// <param name="organizeId">组织ID</param>
        /// <returns></returns>
        public IList<DispenseBQIndexVO> GetZYFYBQPagList(string lyyf, string organizeId)
        {
            const string sql = @"SELECT DISTINCT a.bqCode,a.bqmc 
                            FROM NewtouchHIS_Base..xt_bq a WITH(NOLOCK) 
                            inner join NewtouchHIS_Sett..zy_ypyzzx b WITH(NOLOCK) 
                            ON a.bqCode = b.bqCode AND b.lyyf = @lyyf 
                            WHERE a.OrganizeId=@OrganizeId";

            return this.FindList<DispenseBQIndexVO>(sql, new DbParameter[]
            {
                new SqlParameter("@lyyf", lyyf ?? "") ,
                new SqlParameter("@OrganizeId", organizeId)
            });
        }

        /// <summary>
        /// 住院发药病人
        /// </summary>
        /// <param name="bq">病区</param>
        /// <returns></returns>
        public IList<DispenseBQRYIndexVO> GetZYFYBRPagList(string zyyf, string bq)
        {
            string sql = @"select DISTINCT zyh,b.xm,bq,cw from NewtouchHIS_Sett..zy_brjbxx a WITH ( NOLOCK ) 
                           inner join NewtouchHIS_Sett..xt_brjbxx b WITH ( NOLOCK )
                           on a.patid = b.patid AND a.zybz IN ('1','2','3')   ";
            //有条件跟条件取
            if (!string.IsNullOrEmpty(bq))
                sql += " AND a.bq =@bq  ";
            else//没条件根据类型的第一个病区取
                sql += @" AND a.bq=(SELECT top 1 a.bqCode FROM NewtouchHIS_Base..xt_bq a WITH(NOLOCK) 
                           inner join NewtouchHIS_Sett..zy_ypyzzx b WITH(NOLOCK)
                            ON a.bqCode = b.bqCode and  b.lyyf = @lyyf) ";

            return this.FindList<DispenseBQRYIndexVO>(sql, new SqlParameter[] {
                new SqlParameter("@bq", bq ?? ""),
                new SqlParameter("@lyyf", zyyf ?? "")
            });
        }


        /// <summary>
        /// 住院发药信息集合
        /// </summary>
        /// <param name="bq"></param>
        /// <param name="Zyh"></param>
        /// <param name="Kssj"></param>
        /// <param name="Jssj"></param>
        /// <param name="Cl"></param>
        /// <param name="Fyzt"></param>
        /// <returns></returns>
        public IList<DispenseBQRYXXIndexVO> GetZYFYXXPagList(WaitingDispenseBRXXMedicineDispenseRequest model)
        {
            Pagination pagination = model.pagination;
            string sql = @"SELECT DISTINCT k.yzId,k.zyh,d.xm,l.cw,k.ypCode,j.ypmc,j.ycmc,j.ypgg,ISNULL(k.sl,0) sl,
                            CASE Kf.mzzybz WHEN 0 THEN J.bzdw WHEN 1 THEN J.mzcldw WHEN 2 THEN J.zycldw END dw,k.yldw,k.yl,
                            k.zlff,CASE k.yzxz WHEN '1' THEN '临时医嘱' WHEN '2' THEN '长期医嘱' WHEN '3' THEN '出院医嘱' END yzxz,
                            l.ypfz,k.Pcmc pc,l.pyrq,CONVERT(VARCHAR(20),l.fyrq,120) fyrq,m.Name, l.CreateTime,l.pyry,l.ks,l.fybz,
                            k.yszyzid,
                            dbo.fn_getGender(d.xb) xb,
                            '' bz,'' gmxx,k.sjap,d.nl,k.Pcmc,k.cls,l.CreateTime  tdrq
                            from NewtouchHIS_Sett..zy_ypyz k --住院药品医嘱
                            inner join NewtouchHIS_Sett..zy_ypyzzx l on l.yzid=k.yzId ---住院药品医嘱执行
                            left join NewtouchHIS_Sett..zy_brjbxx e on k.zyh = e.zyh --住院病人基本信息
                            left join NewtouchHIS_Sett..xt_brjbxx d on d.patid=e.patid --住院病人基本信息
                            --left join NewtouchHIS_Sett..xt_ypsx i on i.ypCode=k.ypCode --系统药品属性
                            --left join NewtouchHIS_Base..xt_yp j on j.ypCode=k.ypCode --药品
                            left join NewtouchHIS_Base..V_C_xt_yp j on j.ypCode=k.ypCode -- 药品，药品熟悉视图表
                            INNER JOIN NewtouchHIS_Base..V_S_xt_yfbm_yp(NOLOCK) k1 ON k1.dlCode=j.dlCode
                            inner join NewtouchHIS_Base..xt_yfbm Kf on Kf.yfbmCode=k1.yfbmCode and k.Lyyf=kf.ksCode --药房代码必须使用inner join
                            LEFT JOIN NewtouchHIS_Base..V_S_Sys_Staff m ON l.Fyrq=m.gh
                            WHERE k.OrganizeId=@OrganizeId AND l.fybz IN ('1','2') AND l.zt='1' ";

            if (!string.IsNullOrEmpty(model.Bq))
                sql += " AND l.bqCode = @bq ";
            else
            {
                sql += @" AND l.bqCode=(SELECT top 1 a.bqCode FROM NewtouchHIS_Base..xt_bq a WITH(NOLOCK) 
                          inner join NewtouchHIS_Sett..zy_ypyzzx b WITH(NOLOCK) 
                          ON a.bqCode = b.bqCode and  b.lyyf = @lyyf) ";
            }
            if (!string.IsNullOrEmpty(model.Zyh))
                sql += " AND k.zyh = @Zyh ";
            if (!string.IsNullOrEmpty(model.Kssj))
                sql += " AND convert(varchar(10),k.Kssj,23) >= @Kssj ";
            if (!string.IsNullOrEmpty(model.Jssj))
                sql += " AND convert(varchar(10),k.Jssj,23) <= @Jssj ";
            if (!string.IsNullOrEmpty(model.Cl) && model.Cl.Trim() != "0")
                sql += " AND k.yzxz = @Cl ";
            if (!string.IsNullOrEmpty(model.Fyzt) && model.Fyzt.Trim() != "0")
                sql += "AND l.fybz = @Fyzt ";
            SqlParameter[] par = new SqlParameter[] {
                new SqlParameter("@OrganizeId", model.organizeId ?? ""),
                new SqlParameter("@bq", model.Bq ?? ""),
                new SqlParameter("@lyyf", model.zyyf ?? ""),
                new SqlParameter("@Zyh", model.Zyh ?? ""),
                new SqlParameter("@Kssj", model.Kssj ?? ""),
                new SqlParameter("@Jssj", model.Jssj ?? ""),
                new SqlParameter("@Cl", model.Cl ?? ""),
                new SqlParameter("@Fyzt", model.Fyzt ?? "")
            };
            return this.QueryWithPage<DispenseBQRYXXIndexVO>(sql, pagination, par);
        }


        //根据开始时间结束时间获取住院病人医嘱,药品，数量等信息
        public IList<DispenseBQBRPYIndexVO> SetZYBRBQPY(WaitingDispenseBQBRPYMedicineDispenseRequest model)
        {
            string sql = @" SELECT DISTINCT a.yzId,a.zyh,isnull(b.sl,0)sl,b.lyyf,a.ypCode,b.CreateTime as jdrq,a.cls 
                FROM NewtouchHIS_Sett..Zy_Ypyz a WITH(NOLOCK) INNER JOIN NewtouchHIS_Sett..zy_ypyzzx b WITH(NOLOCK) ON a.yzId = b.yzId
                WHERE b.fybz='0' AND a.Zt='1' AND convert(varchar(10),a.Kssj ,23) >= @Kssj  
                AND convert(varchar(10),a.JSsj ,23) <= @Jssj ";
            SqlParameter[] para ={
                //new SqlParameter("@Bq",model.Bq ?? ""),
                //new SqlParameter("@lyyf",model.lyyf ?? ""),
                new SqlParameter("@Kssj",model.Kssj ?? ""),
                new SqlParameter("@Jssj",model.Jssj ?? "")
            };
            return this.FindList<DispenseBQBRPYIndexVO>(sql, para);// 取出来的 datetime 少了毫秒
        }

        //如果配药或者退药完成，修改医嘱执行 fybz 状态为1
        public int UpBQPYyzzx(WaitingDispenseBQPYyzzxZTDispenseRequest model)
        {
            string sql = "";
            if (model.fybz == "1")
            {//配药执行SQL
                sql = @" update zy_ypyzzx set fybz = @fybz where yzid = @yzId 
                and CONVERT(varchar(19),CreateTime,20)=CONVERT(varchar(19),CAST(@CreateTime as datetime),20) 
                select @@ROWCOUNT as Con  ";
            }
            else if (model.fybz == "2")
            {//发药执行SQL
                sql = @" update zy_ypyzzx set fybz = @fybz, dj=@ypdj, je=@ypje, zfbl=@zfbl, zfxz=@zfxz where yzid = @yzId 
                        and CONVERT(varchar(19),CreateTime,20)=CONVERT(varchar(19),CAST(@CreateTime as datetime),20) 
                        select @@ROWCOUNT as Con  ";
            }
            SqlParameter[] para ={
                new SqlParameter("@fybz",model.fybz ?? ""),
                new SqlParameter("@yzId",model.yzId ?? ""),
                new SqlParameter("@ypdj",model.ypdj ?? ""),
                new SqlParameter("@ypje",model.ypje ?? ""),
                new SqlParameter("@zfbl",model.zfbl ?? ""),
                new SqlParameter("@zfxz",model.zfxz ?? ""),
                new SqlParameter("@CreateTime",model.tdrq ?? ""),
            };

            this.FindList<int>(sql, para);
            return 1;
        }


        #endregion

        #region 住院发药查询

        /// <summary>
        /// 住院发药查询人员信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public IList<DispenseBQRYCXXXIndexVO> GetZYFYCXXXPagList(WaitingDispenseBRCXXXMedicineDispenseRequest model)
        {
            #region 不能全部支持无用
            //Pagination pagination = model.pagination;
            //var outpar = new SqlParameter("@tempSql", System.Data.SqlDbType.VarChar,8000);
            //outpar.Direction = System.Data.ParameterDirection.Output;
            //var sqlParList = new List<SqlParameter>();
            //sqlParList.Add(new SqlParameter("@Kssj", model.Kssj));//开始时间
            //sqlParList.Add(new SqlParameter("@Jssj", model.Jssj));//结束时间
            //sqlParList.Add(new SqlParameter("@lyyf", model.zyyf));//住院药房
            //sqlParList.Add(new SqlParameter("@yp", model.ypCode));//药品code
            //sqlParList.Add(new SqlParameter("@bq", model.Bq));// 病区
            //sqlParList.Add(new SqlParameter("@clbz", model.Cl));// 长临
            //sqlParList.Add(outpar);
            //_databaseFactory.Get().Database.ExecuteSqlCommand("Exec YP_XT_ZYFYCX @Kssj,@Jssj,@lyyf,@yp,@bq,@clbz,@tempSql out", sqlParList.ToArray());
            //if (!string.IsNullOrEmpty(outpar.Value.ToString()))
            //{//获取拼好SQL 语句
            //    return this.QueryWithPage<DispenseBQRYCXXXIndexVO>(outpar.Value.ToString(), pagination, new SqlParameter[] {
            //    //new SqlParameter("@lyyf", lyyf ?? "")
            //    });
            //}
            #endregion

            StringBuilder SQLT = new StringBuilder();
            string strunion = "";
            List<ModelBQBRYZZXTYInfoVO> list = model.YpTymxxxList;
            if (list.Count > 0)
            {
                foreach (ModelBQBRYZZXTYInfoVO item in list)
                {
                    SQLT.Append("" + strunion + "select yszyzid='" + item.yszyzid + "',tdrq='" + item.tdrq + "' ");
                    SQLT.Append(",tysl='" + item.tysl + "',tymxId='" + item.tymxId + "',tybz='" + item.tybz + "' ");
                    SQLT.Append(",tyjlrq='" + item.tyjlrq + "',tyxh='" + item.tyxh + "',tyry='" + item.tyry + "' ");
                    SQLT.Append(",ypCode='" + item.ypCode + "',pc='" + item.yppc + "',ph='" + item.ypph + "' ");
                    SQLT.Append(",tyrq='" + item.tyrq + "',sl='" + item.sl + "' ,tysqczyh='" + item.tysqczyh + "' ");
                    strunion = " union all ";//第二次循环执行拼装
                }
            }
            else
            {
                SQLT.Append("" + strunion + "select yszyzid='',tdrq='',tysl='',tymxId='',tybz='' ");
                SQLT.Append(" ,tyjlrq='',tyxh='',tyry='',ypCode='',pc='',ph='',tyrq='',sl='',tysqczyh='' ");
            }
            string sql = @"SELECT DISTINCT  CONVERT(VARCHAR(10),k.JSsj,120) ksrq,CONVERT(VARCHAR(10),yxq,120) yxq, d.xm,l.cw,k.ypCode,j.ypmc,j.ycmc,i.ypgg,t.sl,
	               CASE Kf.mzzybz WHEN 0 THEN J.bzdw WHEN 1 THEN J.mzcldw WHEN 2 THEN J.zycldw END sfdw,
	               (CONVERT(NVARCHAR(50),t.sl)+CASE Kf.mzzybz WHEN 0 THEN J.bzdw WHEN 1 THEN J.mzcldw WHEN 2 THEN J.zycldw END )slydw,m.rymc tyrymc,
	               t.tysl,t.tyry,t.tyrq,t.tysqczyh,m.rymc tysqry,t.tyjlrq,
	               CASE t.tybz WHEN '1' THEN  '待退' WHEN '2' THEN '已退' WHEN '3' THEN '已归架' ELSE '已发' END tybz,
	               k.yldw,k.yl,(CONVERT(NVARCHAR(50),k.yl)+k.yldw)ylydw,l.dj,(t.sl*l.dj) je,jx.jxmc,--k.sl,l.je,
	               k.zlff,CASE k.yzxz WHEN '1' THEN '临时医嘱' WHEN '2' THEN '长期医嘱' WHEN '3' THEN '出院医嘱' END yzxz,
	               l.ypfz czbh,k.Pcmc pc,l.pyrq,l.fyrq,m.rymc,k.yzId,l.CreateTime tdrq,l.pyry,l.ks,l.fybz,k.zyh,k.yszyzid,d.xb,t.pc yppc,'' gmxx,k.sjap
	               FROM NewtouchHIS_Sett..zy_ypyz k 
	               INNER JOIN NewtouchHIS_Sett..zy_brjbxx e ON k.zyh = e.zyh 
	               INNER JOIN NewtouchHIS_Sett..xt_brjbxx d ON e.patid = d.patid 
	               INNER JOIN NewtouchHIS_Sett..xt_brxz f ON e.brxz = f.brxz  
	               INNER JOIN NewtouchHIS_Base..xt_ypsx i ON k.ypCode = i.ypCode 
	               INNER JOIN NewtouchHIS_Base..xt_yp j ON k.ypCode= j.ypCode 
	               INNER JOIN NewtouchHIS_Sett..zy_ypyzzx l ON k.yzId = l.yzId 
	               INNER JOIN NewtouchHIS_Sett..xt_ks h ON l.ks = h.ks 
	               INNER JOIN NewtouchHIS_Sett..xt_ypks K1 ON J.dlCode = K1.dl AND K1.ks=k.lyyf 
	               INNER JOIN NewtouchHIS_Base..xt_yfbm Kf ON K1.ykdm = Kf.yfbmCode 
	               LEFT JOIN NewtouchHIS_Sett..xt_ry m ON l.fyry = m.gh 
	               --LEFT JOIN NewtouchHIS_PDS..zy_ypyzzxph n ON t.yzzxId = l.yzzxId
	               LEFT JOIN NewtouchHIS_Base..xt_ypjx jx ON j.jx=jx.jxId 
	               --LEFT JOIN NewtouchHIS_Sett..zy_yptymx t ON k.yzId=t.YsZyZID
                   left join (" + SQLT + ")t on k.yzId=t.yszyzid "
                   + "LEFT JOIN NewtouchHIS_PDS..XT_YP_CRKMX n1 ON i.ypCode = n1.Ypdm AND t.ph = n1.Ph AND t.pc = n1.pc "
                   + " WHERE l.fybz ='2' AND l.zt='1' AND k.lyyf = @lyyf AND l.bqCode=@bq "
                   + " AND k.Kssj>=@Kssj AND k.JSsj<=@Jssj "
                   + " AND (k.ypCode LIKE @yp or j.ypmc LIKE @yp or j.py LIKE @yp)";
            return this.FindList<DispenseBQRYCXXXIndexVO>(sql, new SqlParameter[] {
                new SqlParameter("@Kssj", model.Kssj ?? ""),
                new SqlParameter("@Jssj", model.Jssj ?? ""),
                new SqlParameter("@lyyf", model.zyyf ?? ""),
                new SqlParameter("@yp","%" + model.ypCode + "%"),
                new SqlParameter("@bq", model.Bq ?? ""),
                new SqlParameter("@clbz", model.Cl ?? "")
                });

        }
        #endregion

        #region 住院退药明细信息
        /// <summary>
        /// 返回医生站医嘱ID 医嘱表ID
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public List<ModelZyTyYszyzIdInfoVO> GetZYYszysList(WaitingDispenseTYYszyzIdDispenseRequest model)
        {
            string SQLYZ = "", strsut = "";
            if (model.yszyzList.Count < 1)
                return new List<ModelZyTyYszyzIdInfoVO>();
            foreach (ModelZyTyYszyzIdInfoVO item in model.yszyzList)
            {
                SQLYZ += strsut + " select YszyzID=" + item.yszyzid + " ";
                strsut = " union  all ";
            }
            string sql = @"  select DISTINCT y.yzId,y.YszyzID from NewtouchHIS_Sett..Zy_Ypyz y inner join  (" + SQLYZ + ")t " +
                 " on y.YszyzID=t.YszyzID where  OrganizeId=@OrganizeId and y.Lyyf=@Lyyf  ";
            return this.FindList<ModelZyTyYszyzIdInfoVO>(sql, new SqlParameter[] {
                    new SqlParameter("@OrganizeId",  model.organizeId ?? ""),
                    new SqlParameter("@Lyyf",  model.yzdm ?? "")
                });
        }

        /// <summary>
        /// 住院退药信息集合
        /// </summary>
        /// <param name="bq"></param>
        /// <param name="Zyh"></param>
        /// <param name="Kssj"></param>
        /// <param name="Jssj"></param>
        /// <param name="Cl"></param>
        /// <param name="Fyzt"></param>
        /// <returns></returns>
        public IList<DispenseBQRYTYXXIndexVO> GetZYTYXXPagList(WaitingDispenseBRTYXXMedicineDispenseRequest model)
        {
            StringBuilder SQLT = new StringBuilder();
            List<ModelBQBRYZZXTYInfoVO> list = model.YpTymxxxList;
            if (list.Count < 1)
                return null;//退药表没有能退药数据信息
            else
            {
                String strunion = " ";
                SQLT.Append("( ");
                foreach (ModelBQBRYZZXTYInfoVO item in list)
                {//拼PDS 库 退药明细表信息
                    SQLT.Append("" + strunion + "select yszyzid='" + item.yszyzid + "',tdrq='" + item.tdrq + "' ");
                    SQLT.Append(",tysl='" + item.tysl + "',tymxId='" + item.tymxId + "',tybz='" + item.tybz + "' ");
                    SQLT.Append(",tyjlrq='" + item.tyjlrq + "',tyxh='" + item.tyxh + "',tyry='" + item.tyry + "' ");
                    SQLT.Append(",ypCode='" + item.ypCode + "',pc='" + item.yppc + "',ph='" + item.ypph + "' ");
                    SQLT.Append(",tyrq='" + item.tyrq + "' ");
                    strunion = " union all ";//第二次循环执行拼装
                }
                SQLT.Append("  ) ");
                string sql = @"SELECT DISTINCT d.xm,l.cw,k.ypCode,j.ypmc,j.ycmc,i.ypgg,k.sl,t.tysl,t.tyry,
                    CASE Kf.mzzybz WHEN 0 THEN J.bzdw WHEN 1 THEN J.mzcldw WHEN 2 THEN J.zycldw END dw,k.yldw,k.yl,
                    k.zlff,CASE k.yzxz WHEN '1' THEN '临时医嘱' WHEN '2' THEN '长期医嘱' WHEN '3' THEN '出院医嘱' END yzxz,
                    l.ypfz czbh,k.Pcmc pc,l.pyrq,l.fyrq,m.rymc,t.tymxId,t.tdrq,l.pyry,l.ks,t.tybz,k.zyh,k.yszyzid,
                    dbo.fn_getGender(d.xb) xb,
                    t.pc yppc,t.ph ypph, t.tyrq,'' bz,'' gmxx,k.sjap,t.tyjlrq,t.tyxh
                    FROM  NewtouchHIS_Sett..zy_ypyz k
                    INNER JOIN  NewtouchHIS_Sett..zy_brjbxx e ON k.zyh = e.zyh
                    INNER JOIN  NewtouchHIS_Sett..xt_brjbxx d ON e.patid = d.patid
                    INNER JOIN  NewtouchHIS_Sett..xt_brxz f ON e.brxz = f.brxz 
                    INNER JOIN  NewtouchHIS_Sett..xt_ypsx i ON k.ypCode = i.ypCode 
                    INNER JOIN  NewtouchHIS_Sett..xt_yp j ON k.ypCode= j.ypCode
                    INNER JOIN  NewtouchHIS_Sett..zy_ypyzzx l ON k.yzId = l.yzId
                    INNER JOIN  " + SQLT.ToString() + " t ON k.yszyzid = t.yszyzid "
                   + " INNER JOIN  NewtouchHIS_Sett..xt_ks h ON l.ks = h.ks "
                   + " INNER JOIN  NewtouchHIS_Sett..xt_ypks K1 ON J.dlCode = K1.dl "
                   + " INNER JOIN  NewtouchHIS_Base..xt_yfbm Kf ON K1.ks = Kf.yfbmCode AND K1.ks=k.lyyf "
                   + " LEFT JOIN  NewtouchHIS_Sett..xt_ry m ON t.tyry = m.gh "
                   + " WHERE k.OrganizeId=@OrganizeId and l.fybz ='2' AND l.zt='1' AND CONVERT(VARCHAR(10),k.JSsj,120) >= @Kssj "
                   + " AND CONVERT(VARCHAR(10),k.JSsj,120) <= @Jssj AND k.lyyf = @lyyf ";
                if (!string.IsNullOrEmpty(model.Bq))
                    sql += " AND l.bqCode = @bq ";
                else
                {
                    sql += @" AND l.bqCode=(SELECT top 1 a.bqCode FROM NewtouchHIS_Base..xt_bq a WITH(NOLOCK) 
                          inner join NewtouchHIS_Sett..zy_ypyzzx b WITH(NOLOCK) 
                          ON a.bqCode = b.bqCode and  b.lyyf = @lyyf) ";
                }
                if (!string.IsNullOrEmpty(model.Zyh))
                    sql += "  AND k.zyh = @Zyh ";
                if (!string.IsNullOrEmpty(model.Cl) && model.Cl.Trim() != "0")
                    sql += "  AND k.zyh = @Cl ";
                return this.FindList<DispenseBQRYTYXXIndexVO>(sql, new SqlParameter[] {
                    new SqlParameter("@SQLT", SQLT.ToString() ?? ""),
                    new SqlParameter("@OrganizeId", model.organizeId ?? ""),
                    new SqlParameter("@bq", model.Bq ?? ""),
                    new SqlParameter("@lyyf", model.zyyf ?? ""),
                    new SqlParameter("@Zyh", model.Zyh ?? ""),
                    new SqlParameter("@Kssj", model.Kssj ?? ""),
                    new SqlParameter("@Jssj", model.Jssj ?? ""),
                    new SqlParameter("@Cl", model.Cl ?? "")
                });
            }
        }
        #endregion
    }
}
