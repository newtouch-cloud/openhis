/*********************************************************/
// Copyright (C) 2016 Newtouch 版权所有。
// Description： 获取住院管理》账户管理》
// Author：HLF
// CreateDate： 2016/12/6 17:01:42
//**********************************************************/
using Newtouch.HIS.Domain.ValueObjects;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.Infrastructure;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using Newtouch.Common;
using System.Linq;
using Newtouch.HIS.Domain.Entity;
using Newtouch.Common.Operator;
using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.DmnService;
using Newtouch.Core.Common.Exceptions;
using Newtouch.Tools;

namespace Newtouch.HIS.DomainServices
{
    public class HosPatAccDmnService : DmnServiceBase, IHosPatAccDmnService
    {

        public HosPatAccDmnService(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }


        /// <summary>
        /// 获取住院管理》账户管理》病人基本信息
        /// </summary>
        /// <param name="kh">卡号</param>
        /// <returns></returns>
        public HosPatAccountActionVO GetHosPatInfo(string zyh,string orgId)
        {
            HosPatAccountActionVO hosPatVo = null;
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"select a.zyh,a.kh,a.patid,a.xm,a.xb,a.blh,a.csny,a.ks,a.bq,a.ryrq,
                                    c.zhxz,c.zhye,c.zt,
                                    c.createtime,c.creatorcode,c.zhCode zh,c.zhCode zhbh,@zhxzmc zhxzMC
                            from    zy_brjbxx a with(nolock) 
                            inner join  xt_zh c with(nolock) 
                            on c.patid = a.patid  and a.zt='1' and c.zt='1' and c.OrganizeId=@orgId
                            and c.zhxz=@zhxz
                            and a.zyh=@zyh
                            where a.OrganizeId=@orgId and a.zybz not in(0,4)
                        ");
            SqlParameter[] par = {
                       new SqlParameter("@zhxz",((int)EnumXTZHXZ.ZYYJKZH).ToString()),
                       new SqlParameter("@zhxzMC",EnumXTZHXZ.ZYYJKZH.GetDescription()),
                       new SqlParameter("@zyh",zyh ?? ""),
                       new SqlParameter("@orgId",orgId)

                };
            hosPatVo = this.FindList<HosPatAccountActionVO>(strSql.ToString(), par).FirstOrDefault();

            if (hosPatVo == null)
            {
                throw new FailedCodeException("HOSP_PATIENT_BASICINFO_IS_NOT_EXIST");
            }
            return hosPatVo;
        }

        /// <summary>
        /// 获取住院管理》账户管理》获取账户收支记录信息
        /// </summary>
        /// <param name="zh">账户</param>
        /// <param name="zyh">住院号</param>
        /// <returns></returns>
        public List<HosPatAccPayVO> GetAccPayInfo(int zh, string orgId, string zyh = null)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"select a.szje,a.zhye,a.pzh,a.CreatorUserName,
                                       a.CreateTime,a.szxz,b.xjzffsmc,b.xjzffs
                                from   xt_brzhszjl a WITH(nolock)
                                left join xt_xjzffs b WITH(nolock) on a.xjzffs = b.xjzffs
                                where a.zh =@zh and (@zyh = '' or a.zyh = @zyh)
			                    and a.OrganizeId = @orgId
                                order by zhszjlbh DESC");
            SqlParameter[] par = {
                    new SqlParameter("@zh", zh),
                    new SqlParameter("@zyh", zyh ?? ""),
                    new SqlParameter("@orgId", orgId)
                };
            return this.FindList<HosPatAccPayVO>(strSql.ToString(), par);
        }

        /// <summary>
        /// 住院结算 获取病人基本信息（基本信息、账户、科室等）
        /// </summary>
        /// <param name="zyh">住院号</param> 
        /// <returns></returns>
        public IList<HospSettPatInfoVO> GetHospSettPatInfo(string zyh, string orgId)
        {
            if (string.IsNullOrWhiteSpace(zyh))
            {
                return null;
            }
            var sql = @"
                        --第一诊断
                        declare @ryzdCode varchar(20)
                        declare @ryzdicd10 varchar(20)
                        select top 1 @ryzdCode = n.zdCode, @ryzdicd10 = n.icd10 from zy_rydzd m with(nolock)
                        left join [NewtouchHIS_Base]..V_S_xt_zd n
                        on m.zdCode = n.zdCode and (n.OrganizeId=@OrganizeId or n.OrganizeId = '*')
                        where m.zyh = @zyh and m.OrganizeId=@OrganizeId
                        order by m.zdpx

                        select a.zyh,
                        a.zybz, b.xb, a.ryrq, a.cyrq, b.csny, b.patid patid, b.blh, b.pzh
                        , c.brxz, c.brxzmc, c.ybjylx, b.xm, f.CardNo kh, a.ks ksCode, d.Name ksmc, e.zhbh zyyjjzhbh, e.zh zyyjjzh, e.zhxz zyyjjzhzhxz
                        , @ryzdCode ryzdCode, @ryzdicd10 ryzdicd10
                        from zy_brjbxx a with(nolock)
                        left join xt_brjbxx b
                        on a.patid = b.patid and b.OrganizeId=@OrganizeId
                        left join xt_brxz c
                        on a.brxz = c.brxz and c.OrganizeId=@OrganizeId
                        left join [NewtouchHIS_Base]..V_S_Sys_Department d
                        on a.ks = d.Code and d.OrganizeId=@OrganizeId
                        left join xt_brzh e
                        on a.zyh = e.zyh and e.OrganizeId=@OrganizeId
                        left join xt_card f
                        on a.kh = f.CardNo and f.OrganizeId=@OrganizeId
                        where a.zyh = @zyh and e.zhxz = @xtzhxz and e.zt = '1' and a.OrganizeId=@OrganizeId ";
            var parameters = new List<SqlParameter>() {
                new SqlParameter("@zyh" , zyh)
                //住院病人等级过来的，一定已经有了一个‘预缴款账户’
                ,new SqlParameter("@xtzhxz" , ((int)EnumXTZHXZ.ZYYJKZH).ToString())
                ,new SqlParameter("@OrganizeId" , orgId)
            };
            return this.FindList<HospSettPatInfoVO>(sql, parameters.ToArray());
        }


        /// <summary>
        /// 充值和退费事务处理
        /// </summary>
        /// <param name="vo"></param>
        public void HosPatAccDB(HosPatAccDataVO vo, SysPatientAccountEntity zh, FinanceReceiptEntity cwsj,string type)
        {  
            using (var db = new EFDbTransaction(this._databaseFactory).BeginTrans())
            {
                if (vo.szjl != null)
                {
                    db.Insert(vo.szjl);
                }

                //病人账户
                if (vo.oldzh != null)    //Update
                { 
                    db.Update(zh);
                }

                //财务收据
                if (type == "insert")
                {
                    db.Insert(cwsj);  // 添加收据凭证号 
                }
                if (type == "update")
                {
                    db.Update(cwsj);  //修改收据凭证号 
                }
                db.Commit();
            }

            //病人账户变更日志
            if (vo.oldzh != null)
            {
                AppLogger.WriteEntityChangeRecordLog(vo.oldzh, zh, SysPatientAccountEntity.GetTableName(), vo.oldzh.zhbh.ToString());
            }
            //财务收据变更日志
            if(vo.oldcwsj != null && type == "update"){
                AppLogger.WriteEntityChangeRecordLog(vo.oldcwsj, cwsj, FinanceReceiptEntity.GetTableName(), vo.oldcwsj.cwsjId.ToString());
            }
        }

        /// <summary>
        /// 获取住院病人基本信息（基本信息、科室等） 在院状态验证
        /// </summary>
        /// <param name="zyh">住院号</param> 
        /// <returns></returns>
        public IList<HospPatientInfoVO> GetHospPatientInfo(string zyh, string orgId)
        {
            if (string.IsNullOrWhiteSpace(zyh))
            {
                return null;
            }
            var sql = @"
                        --第一诊断
                        declare @ryzdCode varchar(50)
                        declare @ryzdicd10 varchar(50)
                        declare @ryzdmc varchar(50)
                        select top 1 @ryzdCode = n.zdCode, @ryzdicd10 = n.icd10, @ryzdmc = n.zdmc 
                        from zy_rydzd m with(nolock)
                        left join [NewtouchHIS_Base]..V_S_xt_zd n
                        on m.zdCode = n.zdCode and (n.OrganizeId=@OrganizeId or n.OrganizeId = '*')
                        where m.zyh = @zyh and m.OrganizeId=@OrganizeId
                        order by m.zdpx

                        select a.zyh, a.zjlx, a.zjh
                        , a.zybz, b.xb, a.ryrq, a.cyrq, b.csny, b.patid patid, b.blh
                        , c.brxz, c.brxzmc, c.ybjylx, b.xm, f.CardNo kh,a.doctor ysgh,g.Name ysmc, a.ks ksCode, d.Name ksmc, e.zhbh zyyjjzhbh, e.zh zyyjjzh, e.zhxz zyyjjzhzhxz
                        , @ryzdCode ryzdCode, @ryzdicd10 ryzdicd10, @ryzdmc ryzdmc
                        , b.phone phone
                        from zy_brjbxx a with(nolock)
                        --系统病人信息
                        left join xt_brjbxx b
                        on a.patid = b.patid and b.OrganizeId=@OrganizeId and b.zt = '1'
                        --病人账户
                        left join xt_brxz c
                        on a.brxz = c.brxz and c.OrganizeId=@OrganizeId and c.zt = '1'
                        --科室
                        left join [NewtouchHIS_Base]..V_S_Sys_Department d
                        on a.ks = d.Code and d.OrganizeId=@OrganizeId
                        --系统病人账户
                        left join xt_brzh e
                        on a.zyh = e.zyh and e.OrganizeId=@OrganizeId and e.zhxz = @xtzhxz and e.zt = '1'
                        --系统 卡
                        left join xt_card f
                        on a.patid = f.patid and f.OrganizeId = @OrganizeId and f.zt = '1'
                        left join [NewtouchHIS_Base]..V_C_Sys_UserStaff g
						on g.gh=a.doctor and g.OrganizeId=a.OrganizeId and g.zt='1'
                        --where
                        where a.zyh = @zyh and a.OrganizeId = @OrganizeId and a.zt = '1'";
            var parameters = new List<SqlParameter>() {
                new SqlParameter("@zyh" , zyh)
                ,new SqlParameter("@xtzhxz" , ((int)EnumXTZHXZ.ZYYJKZH).ToString())
                ,new SqlParameter("@OrganizeId" , orgId)
            };
            return this.FindList<HospPatientInfoVO>(sql, parameters.ToArray());
        }

    }
}
