using FrameworkBase.MultiOrg.DmnService;
using FrameworkBase.MultiOrg.Infrastructure;
using Newtouch.Core.Common;
using Newtouch.Core.Common.Exceptions;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IDomainServices.OutpatientManage;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.HIS.Domain.ValueObjects.OutpatientManage;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Newtouch.HIS.DomainServices.OutpatientManage
{
    public class CancelOutpatientRegDmnService : DmnServiceBase, ICancelOutpatientRegDmnService
    {
        private readonly IOutpatientRegistRepo _outpatientRegistRepo;

        public CancelOutpatientRegDmnService(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }

        /// <summary>
        /// 查询可以取消挂号的列表
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="orgId"></param>
        /// <param name="blh"></param>
        /// <param name="xm"></param>
        /// <param name="kssj"></param>
        /// <param name="jssj"></param>
        /// <returns></returns>
        public IList<CancelRegisterSelectVO> SelectRegisterlist(Pagination pagination, string orgId, string blh, string xm, DateTime? kssj, DateTime? jssj)
        {
            if (string.IsNullOrEmpty(orgId))
            {
                return null;
            }
            StringBuilder sqlStr = new StringBuilder();
            var parlist = new List<SqlParameter>();
            sqlStr.Append(@"
SELECT gh.ghnm,
		brjbxx.blh,
        brjbxx.xm,
        brxz.brxzmc,
        brjbxx.xb,
        brjbxx.csny,
        Department.Name AS ksmc,
        Staff.Name AS ysmc,
        ItemsDetail.Name AS brly,
        gh.mzh,
        gh.ghrq
FROM mz_gh gh
LEFT JOIN xt_brjbxx brjbxx
    ON brjbxx.patId =gh.patId
        AND brjbxx.OrganizeId=gh.OrganizeId
LEFT JOIN xt_brxz brxz
    ON brxz.brxz=brjbxx.brxz
        AND brxz.OrganizeId=gh.OrganizeId
LEFT JOIN NewtouchHIS_Base..V_S_Sys_Department Department
    ON Department.Code=gh.ks
        AND Department.OrganizeId=gh.OrganizeId
LEFT JOIN NewtouchHIS_Base..V_S_Sys_Staff Staff
    ON Staff.gh=gh.ys
        AND Staff.OrganizeId=gh.OrganizeId
LEFT JOIN [NewtouchHIS_Base]..V_C_Sys_ItemsDetail ItemsDetail
	ON ItemsDetail.Code=brjbxx.brly and ItemsDetail.CateCode = 'PatientSource'
		AND (ItemsDetail.OrganizeId = @orgId  OR ItemsDetail.OrganizeId = '*')
WHERE gh.zt='1'
        AND (gh.ghzt<>'2' or gh.ghzt is null)   --注意:null 值必须另外加判断
        AND gh.OrganizeId=@orgId
        AND gh.ghnm NOT IN
		(
			--存在有效门诊计费 不可以退
			select ghnm from mz_xm
where zt = '1'
union
select distinct ghnm from mz_cf cf
left join mz_cfmx cfmx
on cfmx.cfnm = cf.cfnm
where cf.zt = '1' and cfmx.zt = '1'
			)
                        ");
            if (!string.IsNullOrEmpty(blh))
            {
                sqlStr.Append(" AND brjbxx.blh like @blh");
                parlist.Add(new SqlParameter("@blh", "%" + blh.Trim() + "%"));
            }
            if (!string.IsNullOrEmpty(xm))
            {
                sqlStr.Append(" AND (brjbxx.xm like @xm or brjbxx.py like @xm)");
                parlist.Add(new SqlParameter("@xm", "%" + xm.Trim() + "%"));
            }
            if (kssj.HasValue)
            {
                sqlStr.Append(" AND gh.ghrq >= @kssj");
                parlist.Add(new SqlParameter("@kssj", kssj.Value));
            }
            if (jssj.HasValue)
            {
                sqlStr.Append(" AND gh.ghrq < @jssj");
                parlist.Add(new SqlParameter("@jssj", jssj.Value.AddDays(1).Date));
            }
            parlist.Add(new SqlParameter("@orgId", orgId));

            var list = this.QueryWithPage<CancelRegisterSelectVO>(sqlStr.ToString(), pagination, parlist.ToArray()).ToList();
            return list;
        }

        /// <summary>
        /// 保存取消挂号
        /// </summary>
        /// <param name="list"></param>
        public void SaveCancelRegister(string orgId, List<SaveCancelRegisterGhnmVO> list)
        {
            using (var db = new EFDbTransaction(this._databaseFactory).BeginTrans())
            {
                foreach (var item in list)
                {
                    var settlist = SelectEntityByGhnm(orgId, item.ghnm);
                    if (settlist.Count > 0)
                    {
                        throw new FailedCodeException("NOTICE_A_BILLING_RECORD_ALREADY_EXISTS_AND_CAN_NOT_BE_CANCELED");  //"注意：已存在计费记录，无法取消登记"
                    }
                    _outpatientRegistRepo.SaveCancelRegister(orgId, item.ghnm);
                }
                db.Commit();
            }
        }

        /// <summary>
        /// 根据ghnm查询list
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="ghnm"></param>
        /// <returns></returns>
        public List<OutpatientSettlementEntity> SelectEntityByGhnm(string orgId, int ghnm)
        {
            StringBuilder sqlStr = new StringBuilder();
            var parlist = new List<SqlParameter>();
            sqlStr.Append(@"
SELECT *
FROM mz_js js
WHERE zt='1'and OrganizeId=@orgId
        AND jszt<>2
        AND NOT EXISTS 
					(SELECT 1
					FROM mz_js b
					WHERE b.zt='1'
							AND jszt='2'
							AND b.cxjsnm = js.jsnm
							AND b.OrganizeId = js.OrganizeId ) 
		AND ghnm=@ghnm
                        ");
            parlist.Add(new SqlParameter("@orgId", orgId));
            parlist.Add(new SqlParameter("@ghnm", ghnm));
            var list = this.FindList<OutpatientSettlementEntity>(sqlStr.ToString(), parlist.ToArray()).ToList();
            return list;

        }

    }
}
