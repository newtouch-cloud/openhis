using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.HIS.Domain.ValueObjects;
using Newtouch.Infrastructure;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using FrameworkBase.MultiOrg.DmnService;
using FrameworkBase.MultiOrg.Infrastructure;

namespace Newtouch.HIS.DomainServices.Medicine
{
    /// <summary>
    /// 部门 药品
    /// </summary>
    public class DepartmentMedicineInfoDmnService : DmnServiceBase, IDepartmentMedicineInfoDmnService
    {
        public DepartmentMedicineInfoDmnService(IDefaultDatabaseFactory databaseFactory): base(databaseFactory)
        {
        }

        /// <summary>
        /// 根据药房代码、药品代码 获取药房的 药品 的 库存数量和单位
        /// </summary>
        /// <param name="yfbmCode"></param>
        /// <param name="ypCode"></param>
        /// <param name="orgId"></param>
        /// <param name="fybm"></param>
        /// <returns></returns>
        public DepartmentMedicineStockUnitVO GetYpKcslAndYpdw(string yfbmCode, string ypCode, string orgId, string fybm)
        {
            const string sql = @"
DECLARE @tmpYpCode VARCHAR(100);
DECLARE @fybmdw VARCHAR(100);
DECLARE @fybmzhyz NUMERIC(9,4);
IF EXISTS(SELECT 1 FROM tempdb..sysobjects where id=object_id(N'tempdb..#tmp') and type='U')
BEGIN
	DROP TABLE #tmp;
END
SELECT yp.ypCode, sum(kc.kcsl) kcsl,
[dbo].[f_getComplexYpSlandDw](sum(kc.kcsl), yp.bzs, yp.bzdw, yp.zxdw) kcslandDw,
case yfbm.mzzybz when '0' then yp.bzdw  when '1' then yp.mzcldw when '2' then yp.zycldw end dw,
case yfbm.mzzybz when '0' then yp.bzs  when '1' then yp.mzcls when '2' then yp.zycls end bbmzhyz
INTO #tmp
from xt_yp_kcxx(nolock) kc
left join [NewtouchHIS_Base]..V_S_xt_yp(nolock) yp on kc.ypdm = yp.ypCode and yp.OrganizeId = kc.OrganizeId
left join [NewtouchHIS_Base]..V_S_xt_yfbm_yp(nolock) yfbmYp on yp.dlCode = yfbmYp.dlCode and yfbmYp.OrganizeId = kc.OrganizeId
left join [NewtouchHIS_Base]..V_S_xt_yfbm(nolock) yfbm on yfbmYp.yfbmCode = yfbm.yfbmCode and yfbm.OrganizeId = kc.OrganizeId
where yp.ypCode=@ypCode
AND kc.OrganizeId=@OrganizeId 
and yfbm.yfbmCode=@yfbmCode
and kc.yxq >= GETDATE() 
and kc.Tybz = 0
group BY yp.ypCode, kc.ypdm, yfbm.mzzybz ,yp.bzdw,yp.mzcldw,yp.zycldw, yp.bzs, yp.zxdw,yp.mzcls,yp.zycls

SET @tmpYpCode=(SELECT TOP 1 ypCode from #tmp);

SELECT @fybmdw=(CASE yfbm.mzzybz WHEN '0' then yp.bzdw  when '1' then yp.mzcldw when '2' then yp.zycldw END),
@fybmzhyz=(case yfbm.mzzybz when '0' then yp.bzs  when '1' then yp.mzcls when '2' then yp.zycls end)
FROM [NewtouchHIS_Base]..V_S_xt_yp(nolock) yp
INNER JOIN [NewtouchHIS_Base]..V_S_xt_yfbm_yp(nolock) yfbmYp on yp.dlCode = yfbmYp.dlCode and yfbmYp.OrganizeId =yp.OrganizeId
INNER JOIN [NewtouchHIS_Base]..V_S_xt_yfbm(nolock) yfbm ON yfbm.yfbmCode = yfbmYp.yfbmCode and yfbm.OrganizeId = yp.OrganizeId
WHERE yfbm.yfbmCode=@fybm 
and yp.OrganizeId = @OrganizeId
AND yp.ypCode=@tmpYpCode

SELECT @fybmdw AS fybmdw, @fybmzhyz AS fybmzhyz, * FROM #tmp
";
            return FirstOrDefault<DepartmentMedicineStockUnitVO>(sql, new DbParameter[] {
                new SqlParameter("@yfbmCode",yfbmCode),
                new SqlParameter("@ypCode",ypCode),
                new SqlParameter("@OrganizeId",orgId),
                new SqlParameter("@fybm", fybm)
            });
        }

        /// <summary>
        /// 获取 内部申领 药品数据源
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="fybm"></param>
        /// <param name="orgId"></param>
        public IList<RequisitionDepartmentMedicineSeleVO> GetRequisitionDepartmentMedicineSeleVOList(string keyword, string fybm, string orgId)
        {
            const string sql = @"
SELECT TOP 500 * FROM 
(
	select  K.ypdm ypCode,Y.ypmc,Y.ycmc,S.ypgg,L.dlmc as ypdlmc
	,dbo.f_getComplexYpSlandDw(sum(case when K.yxq >= getdate() then K.kcsl else 0 end), Y.bzs, Y.bzdw, Y.zxdw) Klslanddw --可领库存
	,sum(case when K.yxq >= getdate() then K.kcsl else 0 end) Kcsl	--库存数量 库存数量存的一定是 最小单位
	,Y.zxdw Kcsldw	--最小单位
	,Y.pfj	--批发价
	,Y.lsj	--零售价
	,Y.bzs ykzhyz   --药库转换因子（计算价格用的）
	from XT_YP_KCXX(NOLOCK) K
	INNER JOIN [NewtouchHIS_Base]..V_S_xt_yp(nolock) Y on K.ypdm=Y.YpCode and Y.OrganizeId = K.OrganizeId
	INNER JOIN [NewtouchHIS_Base]..V_S_xt_ypsx(nolock) S on S.ypCode=Y.ypCode and S.OrganizeId = K.OrganizeId
	INNER JOIN [NewtouchHIS_Base]..V_S_xt_yfbm_yp(nolock) PK on pk.yfbmCode=K.yfbmCode and PK.dlCode=Y.dlCode and PK.OrganizeId = K.OrganizeId
	INNER JOIN [NewtouchHIS_Base]..V_S_xt_yfbm(nolock) M on M.yfbmCode =PK.yfbmCode and M.OrganizeId = K.OrganizeId
	INNER JOIN [NewtouchHIS_Base]..V_S_xt_sfdl(nolock) L on L.dlCode=Y.dlCode and L.OrganizeId = K.OrganizeId
	where K.yfbmCode = @fybm and K.Tybz = 0
	and (@keyword = '' or Y.ypCode Like @searchkeyword or Y.ypmc Like @searchkeyword or Y.spm Like @searchkeyword or Y.py Like @searchkeyword)
	and M.mzzybz = '0'  --药库
	and K.OrganizeId = @OrganizeId
	group by K.ypdm,Y.ypmc,Y.ycmc,Y.bzs,Y.bzdw,Y.zxdw,Y.pfj,Y.lsj,S.ypgg,L.dlmc
) as tttt
order by ypCode";
            return FindList<RequisitionDepartmentMedicineSeleVO>(sql, new DbParameter[] {
                new SqlParameter("@fybm",fybm),
                new SqlParameter("@keyword", keyword ?? ""),
                new SqlParameter("@searchkeyword", "%" + (keyword??"") + "%"),
                new SqlParameter("@OrganizeId",orgId)
            });
        }

    }
}
