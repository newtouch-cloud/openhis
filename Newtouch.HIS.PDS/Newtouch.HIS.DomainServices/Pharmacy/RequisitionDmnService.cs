using Newtouch.Core.Common;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.HIS.Domain.ValueObjects;
using Newtouch.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using FrameworkBase.MultiOrg.DmnService;
using FrameworkBase.MultiOrg.Infrastructure;
using Newtouch.Core.Common.Exceptions;


namespace Newtouch.HIS.DomainServices
{
    /// <summary>
    /// 内部申领
    /// </summary>
    public class RequisitionDmnService : DmnServiceBase, IRequisitionDmnService
    {
        private readonly ISysPharmacyDepartmentMedicineRepo _sysPharmacyDepartmentMedicineRepo;

        public RequisitionDmnService(IDefaultDatabaseFactory databaseFactory) : base(databaseFactory)
        {
        }

        /// <summary>
        /// 新申领 提交保存
        /// </summary>
        /// <param name="sldh">申领单号</param>
        /// <param name="fybmCode">发药部门</param>
        /// <param name="slbmCode">申领部门（当前登录用户药房部门）</param>
        /// <param name="mxList">申领药品明细</param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public bool SubmitRequisition(string sldh, string fybmCode, string slbmCode, IList<RequisitionDepartmentMedicineSubmitItemVO> mxList, string orgId)
        {
            using (var db = new EFDbTransaction(_databaseFactory).BeginTrans())
            {
                var mainEntity = new SysMedicineRequisitionEntity
                {
                    OrganizeId = orgId,
                    Sldh = sldh,
                    Slbm = slbmCode,
                    Ckbm = fybmCode,
                    ffzt = (int)EnumSLDDeliveryStatus.None,
                };
                mainEntity.Create(true);
                db.Insert(mainEntity);

                foreach (var mx in mxList)
                {
                    if (!db.IQueryable<SysPharmacyDepartmentMedicineEntity>().Any(p => p.yfbmCode == slbmCode && p.Ypdm == mx.ypCode))
                    {
                        throw new FailedException("不能申领，请先完善本部门药品信息，药品代码：" + mx.ypCode);
                    }
                    var mxEntity = new SysMedicineRequisitionDetailEntity
                    {
                        sldId = mainEntity.sldId,
                        ypCode = mx.ypCode,
                        //Yxrq = mx.Yxq,
                        Zhyz = mx.bbmzhyz,  //发药部门的转换因子
                        //ph = "",
                        //pc = "",
                        slsl = mx.slsl,    //转换成最小单位，保存之
                        yfsl = 0,   //已发数量
                    };
                    mxEntity.Create(true);
                    db.Insert(mxEntity);
                }
                db.Commit();
            }
            return true;
        }

        /// <summary>
        /// 内部申领单 查询
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="orgId"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="sldh"></param>
        /// <param name="slbm">申领部门</param>
        /// <param name="ckbm">出库部门</param>
        /// <param name="ffzt">发放状态</param>
        /// <param name="allUseableFfzt"></param>
        /// <returns></returns>
        public IList<RequisitionSelectVO> RequisitionSearch(Pagination pagination, string orgId, DateTime? startDate, DateTime? endDate
            , string sldh, string slbm, string ckbm, int? ffzt, string allUseableFfzt)
        {
            var sb = new StringBuilder(@"
select sld.sldId,sld.Sldh,sld.CreateTime,sld.ffzt ,slyfbm.yfbmmc slbmmc,ckyfbm.yfbmmc ckbmmc
from xt_yp_sld(nolock) sld
left join [NewtouchHIS_Base]..V_S_xt_yfbm(nolock) slyfbm on slyfbm.yfbmCode = sld.Slbm and slyfbm.OrganizeId = sld.OrganizeId 
left join [NewtouchHIS_Base]..V_S_xt_yfbm(nolock) ckyfbm on ckyfbm.yfbmCode = sld.Slbm and ckyfbm.OrganizeId = sld.OrganizeId
where sld.zt = '1' 
and sld.OrganizeId =@Organizeid
");
            var par = new List<SqlParameter>();
            if (startDate.HasValue)
            {
                sb.Append(" and sld.CreateTime >= @qsrj");
                startDate = startDate < Constants.MinDateTime ? Constants.MinDateTime : startDate;
                par.Add(new SqlParameter("@qsrj", startDate.Value));
            }
            if (endDate.HasValue)
            {
                sb.Append(" and sld.CreateTime <= @jsrj");
                endDate = endDate < Constants.MinDateTime ? Constants.MinDateTime : endDate;
                par.Add(new SqlParameter("@jsrj", endDate.Value));
            }
            if (!string.IsNullOrWhiteSpace(sldh))
            {
                sb.Append(" and sld.Sldh like @sldh");
                par.Add(new SqlParameter("@sldh", "%" + sldh + "%"));
            }
            if (!string.IsNullOrWhiteSpace(slbm))
            {
                sb.Append(" and sld.Slbm like @slbm");
                par.Add(new SqlParameter("@slbm", "%" + slbm + "%"));
            }
            if (!string.IsNullOrWhiteSpace(ckbm))
            {
                sb.Append(" and sld.Ckbm = @ckbm");
                par.Add(new SqlParameter("@ckbm", ckbm));
            }
            if (ffzt.HasValue)
            {
                sb.Append(" and sld.ffzt = @ffzt");
                par.Add(new SqlParameter("@ffzt", ffzt));
            }
            if (!string.IsNullOrWhiteSpace(allUseableFfzt))
            {
                sb.AppendFormat(" and sld.ffzt in ({0})", allUseableFfzt);
            }
            par.Add(new SqlParameter("@Organizeid", orgId));
            return QueryWithPage<RequisitionSelectVO>(sb.ToString(), pagination, par.ToArray());
        }

        /// <summary>
        /// 内部申领单 明细查询
        /// </summary>
        /// <param name="sldId"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public IList<RequisitionSelectDetailVO> RequisitionDetailListBySlId(string sldId, string orgId)
        {
            if (string.IsNullOrWhiteSpace(sldId))
            {
                throw new FailedException("缺少参数：sldId");
            }
            var strSql = new StringBuilder(@"
SELECT yp.ypmc ypmc, sldmx.ph, sldmx.pc, sldmx.Yxrq, 
FLOOR(sldmx.slsl) slsl, 
yp.bzdw slsldw,
FLOOR(sldmx.yfsl) yfsl,
yp.bzdw yfsldw
FROM XT_YP_SLDMX(nolock) sldmx
INNER JOIN dbo.xt_yp_sld(NOLOCK) sld ON sld.sldId=sldmx.sldId
INNER JOIN [NewtouchHIS_Base]..V_S_xt_yfbm yfbm ON yfbm.yfbmCode=sld.Slbm and  yfbm.OrganizeId = sld.OrganizeId
INNER JOIN NewtouchHIS_Base.dbo.xt_yp(nolock) yp on sldmx.ypCode = yp.ypCode and yp.OrganizeId = sld.OrganizeId AND yp.zt='1'
INNER JOIN NewtouchHIS_Base.dbo.xt_ypsx(NOLOCK) ypsx ON ypsx.ypId=yp.ypId AND ypsx.zt='1'
WHERE sldmx.SldId = @sldId
AND sld.OrganizeId=@OrganizeId
");
            DbParameter[] par = {
                new SqlParameter("@sldId",sldId),
                new SqlParameter("@OrganizeId",orgId)
            };
            return FindList<RequisitionSelectDetailVO>(strSql.ToString(), par);
        }

    }
}
