using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using FrameworkBase.MultiOrg.Infrastructure;
using Newtouch.Common;
using Newtouch.Common.Operator;
using Newtouch.Core.Common;
using Newtouch.Core.Common.Exceptions;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.Entity.V;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.HIS.Domain.ValueObjects;
using Newtouch.Infrastructure;
using Newtouch.Tools;
using static Newtouch.Tools.DB.DbHelper;

namespace Newtouch.HIS.DomainServices
{
	/// <summary>
	/// 库存管理
	/// </summary>
	public partial class DrugStorageDmnService : DmnServiceBase, IDrugStorageDmnService
	{
		private readonly ISysMedicineRepo _sysMedicineRepo;
		private readonly ISysMedicinePriceAdjustmentRepo _sysMedicinePriceAdjustmentRepo;
		private readonly ISysMedicineStockInfoRepo _sysMedicineStockInfoRepo;

		public DrugStorageDmnService(IDefaultDatabaseFactory databaseFactory) : base(databaseFactory)
		{

		}

		/// <summary>
		/// 查询当前部门药品(入库)
		/// </summary>
		/// <param name="keyword">ypmc、py、spm、ypCode</param>
		/// <returns></returns>
		public List<DepartmentMedicineVO> SelectDepartmentMedicineList(string keyword, string yfbmCode, string organizeId)
		{
			var strSql = new StringBuilder(@"
IF EXISTS(SELECT 1 FROM tempdb..sysobjects where id=object_id(N'tempdb..#temp') and type='U')
BEGIN
	DROP TABLE #temp;
END

SELECT kc.ypdm , sum(kc.kcsl-kc.djsl) as kcl 
INTO #temp 
FROM xt_yp_kcxx(NOLOCK) kc 
where yxq >= getdate() 
AND tybz = 0 
AND yfbmCode = @yfbmCode 
AND kc.OrganizeId=@OrganizeId 
AND kc.zt='1'
AND kc.kcsl>0
group by kc.ypdm

SELECT TOP 500 LTRIM(RTRIM(sd.dlmc)) yplb ,
yp.ypCode ypCode ,yp.ypmc ypmc ,ypsx.ypgg gg ,isnull(kcxx.kcl, 0) xykc ,
(select dbo.[f_getYfbmYpComplexYpSlandDw](isnull(kcxx.kcl, 0),@yfbmCode,yp.ypcode,@OrganizeId)) xykcstr ,
yp.bzdw ykdw ,yp.zxdw zxdw ,yp.ycmc sccj , ISNULL(ypsx.pzwh, '') pzwh ,
isnull(yp.lsj, 0) lsj ,
isnull(yp.pfj, 0) pfj ,
isnull(yp.bzs, 1) bzs
FROM xt_yp_bmypxx(NOLOCK) bmyp
INNER JOIN NewtouchHIS_Base.dbo.xt_yp(NOLOCK) yp on yp.ypCode = bmyp.ypdm and yp.OrganizeId=bmyp.OrganizeId and yp.mzzybz = '1' AND yp.zt='1'
INNER JOIN NewtouchHIS_Base.dbo.xt_ypsx(NOLOCK) ypsx ON ypsx.ypId=yp.ypId AND ypsx.zt='1' AND ypsx.OrganizeId=bmyp.OrganizeId AND ypsx.zt='1'
INNER JOIN NewtouchHIS_Base.dbo.V_S_xt_yfbm yfbm on yfbm.yfbmCode=bmyp.yfbmCode and yfbm.OrganizeId=bmyp.OrganizeId 
INNER JOIN newtouchhis_base.dbo.V_S_xt_sfdl sd on sd.dlCode = yp.dlCode and sd.OrganizeId=bmyp.OrganizeId
LEFT JOIN #temp kcxx on kcxx.ypdm=bmyp.Ypdm
WHERE bmyp.yfbmCode = @yfbmCode 
and bmyp.OrganizeId=@OrganizeId
and bmyp.zt = '1'
");
			if (!string.IsNullOrWhiteSpace(keyword))
			{
				strSql.AppendLine(
					"AND ( yp.ypmc like @srm or yp.py like @srm or yp.spm like lower(@srm) or yp.ypCode like @srm ) ");
			}
			var parms = new DbParameter[]
			{
				new SqlParameter("@yfbmCode", yfbmCode),
				new SqlParameter("@OrganizeId", organizeId),
				new SqlParameter("@srm", "%" + (string.IsNullOrEmpty(keyword) ? "" : keyword.Trim()) + "%")
			};
			strSql.AppendLine("order by sd.dlmc, yp.ypCode, yp.ypmc ");
			return FindList<DepartmentMedicineVO>(strSql.ToString(), parms).ToList();
		}

		/// <summary>
		/// 药品发票信息
		/// </summary>
		/// <param name="fph">fph</param>
		/// <returns></returns>
		public List<MedicineInvoiceInfoVO> SelectMedicineListByFPH(string fph)
		{
			const string strSql = @"
SELECT DISTINCT REPLACE(LTRIM(CONVERT(VARCHAR(20),mx.Fph)),'	','') fph, g.gysCode ,g.gysmc
from xt_yp_crkmx(nolock) mx
INNER JOIN xt_yp_crkdj(nolock) c on mx.crkid = c.crkid and c.OrganizeId=@OrganizeId AND c.zt='1'
INNER JOIN NewtouchHIS_Base.dbo.V_S_xt_ypgys g on cast(g.gysCode as varchar) = c.ckbm and g.OrganizeId=@OrganizeId
INNER JOIN xt_yp_kcxx(nolock) kc on kc.ypdm = mx.ypdm and kc.yfbmCode = @yfbmCode and kc.ph = mx.ph AND kc.zt='1'
where c.rkbm = @yfbmCode
AND mx.Fph IS NOT NULL
AND LTRIM(RTRIM(mx.Fph)) LIKE '%'+ @keyword +'%'
AND kc.kcsl > 0
AND kc.kcsl > kc.djsl 
AND c.shzt = @shzt
";
			var param = new DbParameter[] {
				new SqlParameter("@yfbmCode", Constants.CurrentYfbm.yfbmCode),
				new SqlParameter("@OrganizeId", OperatorProvider.GetCurrent().OrganizeId),
				new SqlParameter("@keyword", fph),
				new SqlParameter("@shzt",(int)EnumDjShzt.Approved)
			};
			return FindList<MedicineInvoiceInfoVO>(strSql, param);

		}

		/// <summary>
		/// 查询当前部门药品(出库)
		/// </summary>
		/// <param name="keyword">ypmc、py、spm、ypCode</param>
		/// <param name="fph"></param>
		/// <param name="gyscode"></param>
		/// <returns></returns>
		public List<DepartmentMedicineVO> SelectDepartmentMedicineList2(string keyword, string fph, string gyscode)
		{
			var strSql = new StringBuilder(@"
select sd.dlmc yplb ,
		yp.ypcode ,
		yp.ypmc ,
		ypsx.ypgg gg ,
		(kc.kcsl - kc.djsl) kykc ,
        (select dbo.[f_getYfbmYpComplexYpSlandDw]((kc.kcsl - kc.djsl),@yfbmCode,yp.ypcode,@OrganizeId)) kykcstr ,
		kc.kcsl ,
		kc.zhyz ,
        yp.bzs,
		yp.bzdw ykdw ,
        yp.zxdw ,
		kc.ph ,
		kc.pc ,
		kc.yxq ,
		crkmx.scrq ,
		crkmx.jj ,
		crkmx.pfj ,
		crkmx.lsj ,
		crkmx.kl ,
		yp.ycmc sccj ,
		ypsx.pzwh ,
		kc.kcId
from xt_yp_crkmx(NOLOCK) crkmx
INNER JOIN xt_yp_crkdj(NOLOCK) crkdj on crkdj.crkid = crkmx.crkid
INNER JOIN xt_yp_bmypxx(NOLOCK) bmyp on bmyp.ypdm = crkmx.ypdm and bmyp.yfbmcode = crkdj.Rkbm and bmyp.OrganizeId=crkdj.OrganizeId
INNER JOIN NewtouchHIS_Base.dbo.V_S_xt_yfbm yfbm ON yfbm.yfbmCode=bmyp.yfbmCode AND yfbm.OrganizeId=crkdj.OrganizeId
INNER JOIN newtouchhis_base.dbo.V_S_xt_yp yp on crkmx.ypdm = yp.ypcode and yp.OrganizeId=crkdj.OrganizeId
INNER JOIN newtouchhis_base.dbo.V_S_xt_ypsx ypsx on yp.ypid = ypsx.ypid and ypsx.OrganizeId=crkdj.OrganizeId
LEFT JOIN xt_yp_kcxx(NOLOCK) kc on kc.ypdm = crkmx.ypdm AND kc.yfbmCode=crkdj.Rkbm and kc.pc = crkmx.pc AND kc.ph=crkmx.Ph and kc.OrganizeId=crkdj.OrganizeId
LEFT JOIN newtouchhis_base.dbo.V_S_xt_ypcrkfs fs on fs.crkfsCode = crkdj.crkfsdm 
LEFT JOIN newtouchhis_base.dbo.V_S_xt_sfdl sd on sd.dlcode = yp.dlcode and sd.OrganizeId=crkdj.OrganizeId
where crkdj.Rkbm=@yfbmCode
");
			var parms = new List<SqlParameter>();
			if (!string.IsNullOrWhiteSpace(keyword))
			{
				strSql.AppendLine(
					"AND ( yp.ypcode like @searchkeyValue or yp.ypmc like @searchkeyValue or yp.py like @searchkeyValue or yp.spm like lower(@searchkeyValue) ) ");
				parms.Add(new SqlParameter("@searchkeyValue", "%" + keyword.Trim() + "%"));
			}
			strSql.AppendLine(@" 
AND crkdj.OrganizeId=@OrganizeId
AND bmyp.zt = '1'
AND LTRIM(rtrim(crkmx.fph)) = RTRIM(LTRIM(@txtfph))
AND (kc.kcsl - kc.djsl)>0
AND kc.djsl >= 0
AND kc.tybz = 0  --0：未停用 1：停用
AND fs.crkbz = '0' --0：入库 1：出库
AND crkdj.ckbm = @txtgyscode
order by kc.ypdm , kc.yxq
                        
                        ");
			parms.Add(new SqlParameter("@yfbmCode", Constants.CurrentYfbm.yfbmCode));
			parms.Add(new SqlParameter("@OrganizeId", OperatorProvider.GetCurrent().OrganizeId));
			parms.Add(new SqlParameter("@txtfph", fph));
			parms.Add(new SqlParameter("@txtgyscode", gyscode));
			var list = this.FindList<DepartmentMedicineVO>(strSql.ToString(), parms.ToArray()).ToList();
			return list;

		}

		/// <summary>
		/// 入库
		/// </summary>
		/// <param name="ioReceiptEntity"></param>
		/// <param name="ioReceiptDetailList"></param>
		public void SaveInStorageInfo(SysMedicineStorageIOReceiptEntity ioReceiptEntity,
			List<SysMedicineStorageIOReceiptDetailEntity> ioReceiptDetailList)
		{
			using (var db = new EFDbTransaction(_databaseFactory).BeginTrans())
			{
				db.Insert(ioReceiptEntity); //新增xt_yp_crkdj
				foreach (var item in ioReceiptDetailList)
				{
					db.Insert(item); //新增xt_yp_crkmx
				}
				db.Commit();
			}
		}

		/// <summary>
		/// 出库
		/// </summary>
		/// <param name="ioReceiptEntity"></param>
		/// <param name="ioReceiptDetailList"></param>
		public void SaveOutStorageInfo(SysMedicineStorageIOReceiptEntity ioReceiptEntity,
			List<SysMedicineStorageIOReceiptDetailVO> ioReceiptDetailList)
		{
			using (var db = new EFDbTransaction(this._databaseFactory).BeginTrans())
			{
				//新增xt_yp_crkdj
				db.Insert(ioReceiptEntity);
				foreach (var item in ioReceiptDetailList)
				{
					//冻结库存
					var kcEntity = db.FindEntity<SysMedicineStockInfoEntity>(item.kcId);
					if (kcEntity.kcsl - kcEntity.djsl < item.Sl * item.Ckzhyz)
					{
						throw new FailedCodeException("AVAILABLE_QUANTITY_LESS_THAN_FREEZING_QUANTITY");
					}
					kcEntity.djsl = kcEntity.djsl + item.Sl * item.Ckzhyz;
					kcEntity.Modify();
					db.Update(kcEntity);

					var ioReceiptDetailEntity = new SysMedicineStorageIOReceiptDetailEntity
					{
						crkmxId = item.crkmxId,
						crkId = item.crkId,
						Ypdm = item.Ypdm,
						Fph = item.Fph,
						Kprq = item.Kprq,
						Dprq = item.Dprq,
						Ph = item.Ph,
						Yxq = item.Yxq,
						Pfj = item.Pfj,
						Lsj = item.Lsj,
						Ykpfj = item.Ykpfj,
						Yklsj = item.Yklsj,
						Zje = item.Zje,
						Sl = item.Sl,
						Rkzhyz = item.Rkzhyz,
						Rkbmkc = item.Rkbmkc,
						Ckzhyz = item.Ckzhyz,
						Ckbmkc = item.Ckbmkc,
						Wg = item.Wg,
						zbbz = item.zbbz,
						jkzcz = item.jkzcz,
						hgzm = item.hgzm,
						ysjg = item.ysjg,
						Thyy = item.Thyy,
						Cljg = item.Cljg,
						scrq = item.scrq,
						kl = item.kl,
						jj = item.jj,
						cd = item.cd,
						pc = item.pc,
						zt = item.zt,
						px = item.px
					};
					ioReceiptDetailEntity.Create();

					//新增xt_yp_crkmx
					db.Insert(ioReceiptDetailEntity);
				}

				db.Commit();
			}
		}

		#region 药品调价

		/// <summary>
		/// 调价申请 查询
		/// </summary>
		/// <param name="pagination"></param>
		/// <param name="inputCode"></param>
		/// <returns></returns>
		public IList<AdjustPriceMedicineInfoVO> SelectAdjustPriceMedicineInfoList(Pagination pagination,
			string inputCode)
		{
			var strSql = new StringBuilder(@"
SELECT * FROM (
select yp.ypCode,yp.ypmc,ypsx.ypgg,yp.djdw,yp.pfj,yp.lsj,itemsDetail.Name yptssxmc,
        substring( sfdl.dlmc,0,len(sfdl.dlmc)) yplb,
        yp.jl,yp.jldw,yp.bzs,yp.bzdw,yp.mzcls,yp.mzcldw,yp.zycls,yp.zycldw,yp.zfbl,
        case yp.zfxz when '1' then '自理' when '2' then '分类自负' end zfxz,
        ypjx.jxmc,yp.ycmc,
        case yp.zt when'1' then '启用' when '0' then '停用' end zt,
        case ypsx.jzlx when'0'then'每顿取整'when'1'then'每次取整'end jzlx,
        ypsx.mrbzq
from NewtouchHIS_Base.dbo.V_S_xt_yp yp
left join NewtouchHIS_Base.dbo.V_S_xt_ypsx ypsx on ypsx.ypCode=yp.ypCode and ypsx.OrganizeId=@OrganizeId
left join NewtouchHIS_Base.dbo.V_S_Sys_ItemsDetail itemsDetail on itemsDetail.Code=ypsx.yptssx and (itemsDetail.TopOrganizeId=@TopOrganizeId or itemsDetail.TopOrganizeId='*')
left join NewtouchHIS_Base.dbo.V_S_xt_ypjx ypjx  on  ypjx.jxCode =yp.jx 
left join NewtouchHIS_Base.dbo.V_S_xt_sfdl sfdl on sfdl.dlCode=yp.dlCode and sfdl.OrganizeId=@OrganizeId
where yp.OrganizeId=@OrganizeId 
                         ");
			var inSqlParameterList = new List<SqlParameter>();
			if (!string.IsNullOrEmpty(inputCode))
			{
				strSql.Append(
					" and (yp.ypCode like @inputCode or yp.ypmc like @inputCode or yp.spm like @inputCode or yp.py like @inputCode)");
				inSqlParameterList.Add(new SqlParameter("@inputCode", "%" + inputCode.Trim() + "%"));
			}
			strSql.AppendLine(@"
            ) a
            WHERE a.ypCode NOT IN (
            SELECT ypCode FROM dbo.xt_yptj(NOLOCK) yptj WHERE yptj.zxbz='0' and yptj.shzt NOT IN('2','3') AND yptj.zxsj>GETDATE()
            )
            ");
			inSqlParameterList.Add(new SqlParameter("@yfbmCode", Constants.CurrentYfbm.yfbmCode));
			inSqlParameterList.Add(new SqlParameter("@OrganizeId", OperatorProvider.GetCurrent().OrganizeId));
			inSqlParameterList.Add(new SqlParameter("@TopOrganizeId", Constants.TopOrganizeId));
			return QueryWithPage<AdjustPriceMedicineInfoVO>(strSql.ToString(), pagination,
				inSqlParameterList.ToArray());
		}

		/// <summary>
		/// 调价审核查询
		/// </summary>
		/// <param name="pagination"></param>
		/// <param name="inputCode"></param>
		/// <param name="shzt"></param>
		/// <returns></returns>
		public IList<AdjustPriceMedicineInfoVO> SelectMedicineAdjustPriceApprovalInfoList(Pagination pagination,
			string inputCode, string shzt)
		{
			var strSql = new StringBuilder(@"
							select 
                                yptj.ypfj,yptj.ylsj,yptj.pfj,yptj.lsj,
                                case yptj.shzt when '0' then '未审核' when '1' then '已审核' when '2' then '已拒绝' when '3' then '已撤销' end shzt,
                                case yptj.zxbz when '0' then '待执行' when '1' then '已执行' end zxbz,
                                yptj.ypcode,yp.ypmc,ypsx.ypgg,yp.djdw,itemsdetail.name yptssxmc,
                                substring( sfdl.dlmc,0,len(sfdl.dlmc)) yplb,
                                yp.jl,yp.jldw,yp.bzs,yp.bzdw,yp.mzcls,yp.mzcldw,yp.zycls,yp.zycldw,yp.zxdw,yp.zfbl,
                                case yp.zfxz when '1' then '自理' when '2' then '分类自负' end zfxz,
                                ypjx.jxmc,yp.ycmc,
                                case yp.mzzybz when'1' then '启用' when '0' then '停用' end zt,
                                case ypsx.jzlx when'0'then'每顿取整'when'1'then'每次取整'end jzlx,
                                ypsx.mrbzq,yptj.tzwj,yptj.zxsj,
							    case when yptj.zxsj<=getdate() then 'true' else 'false' end Isgq,
                                yptj.CreateTime
                            from xt_yptj yptj
                            left join newtouchhis_base.dbo.V_S_xt_yp yp on yptj.ypcode = yp.ypcode and yp.OrganizeId=@OrganizeId
                            left join newtouchhis_base.dbo.V_S_xt_ypsx ypsx on ypsx.ypcode=yp.ypcode and ypsx.OrganizeId=@OrganizeId
							left join newtouchhis_base.dbo.V_S_Sys_ItemsDetail itemsdetail on itemsdetail.code=ypsx.yptssx and (itemsDetail.TopOrganizeId=@TopOrganizeId or itemsDetail.TopOrganizeId='*')
                            left join newtouchhis_base.dbo.V_S_xt_ypjx ypjx  on  ypjx.jxcode =yp.jx
                            left join newtouchhis_base.dbo.V_S_xt_sfdl sfdl on sfdl.dlcode=yp.dlcode and sfdl.OrganizeId=@OrganizeId
                            WHERE  1=1  and yptj.yfbmCode=@yfbmCode and yptj.OrganizeId=@OrganizeId

                         ");
			var inSqlParameterList = new List<SqlParameter>();
			if (!string.IsNullOrEmpty(inputCode))
			{
				strSql.Append(
					" and (yp.ypCode like @inputCode or yp.ypmc like @inputCode or yp.spm like @inputCode or yp.py like @inputCode)");
				inSqlParameterList.Add(new SqlParameter("@inputCode", "%" + inputCode.Trim() + "%"));
			}
			if (!string.IsNullOrEmpty(shzt))
			{
				strSql.Append(" and (yptj.shzt =@shzt or @shzt='-1')");
				inSqlParameterList.Add(new SqlParameter("@shzt", shzt));
			}
			inSqlParameterList.Add(new SqlParameter("@yfbmCode", Constants.CurrentYfbm.yfbmCode));
			inSqlParameterList.Add(new SqlParameter("@TopOrganizeId", Constants.TopOrganizeId));
			inSqlParameterList.Add(new SqlParameter("@OrganizeId", OperatorProvider.GetCurrent().OrganizeId));
			return QueryWithPage<AdjustPriceMedicineInfoVO>(strSql.ToString(), pagination,
				inSqlParameterList.ToArray());
		}

		/// <summary>
		/// 药品调价执行
		/// </summary>
		/// <param name="ypCodeList"></param>
		public string ExecteAdjustPrice(ArrayList ypCodeList)
		{
			var executeTime = DateTime.Now;
			foreach (var item in ypCodeList)
			{
				var entity = _sysMedicinePriceAdjustmentRepo.GetMedicinePriceAdjustmentNotExecteEntity(item.ToString());
				if (entity == null)
				{
					return "变更价格执行失败";
				}
				//1.update xt_yp表
				var medicineEntity = _sysMedicineRepo.GetMedicineByCode(OperatorProvider.GetCurrent().OrganizeId, item.ToString());
				const string strSql = @" 
update NewtouchHIS_Base.dbo.V_S_xt_yp 
set pfj=@pfj,lsj=@lsj,LastModifyTime=@LastModifyTime,LastModifierCode=@LastModifierCode 
where ypCode=@ypCode and OrganizeId=@OrganizeId ";
				DbParameter[] param =
				{
					new SqlParameter("@pfj", entity.pfj),
					new SqlParameter("@lsj", entity.lsj),
					new SqlParameter("@ypCode", entity.ypCode),
					new SqlParameter("@LastModifyTime", DateTime.Now),
					new SqlParameter("@LastModifierCode", OperatorProvider.GetCurrent().UserCode),
					new SqlParameter("@OrganizeId", OperatorProvider.GetCurrent().OrganizeId)
				};
				ExecuteSqlCommand(strSql, param);

				try
				{
					using (var db = new EFDbTransaction(_databaseFactory).BeginTrans())
					{
						//1.update xt_yptj表
						entity.zxbz = ((int)EnumPriceAdjustZXStatus.Executed).ToString();
						entity.zxsj = executeTime;
						entity.Modify();
						db.Update(entity);

						//取值，判断异常
						var list = GetNeedColumnValue(item.ToString());
						if (list == null)
						{
							throw new FailedCodeException("INSERTED_INTO_THE_TJSY_TABLE_FAILED_NO_MATCHING_DRUG_INFORMATION_OR_STOCK_INFORMATION");
						}
						//2.insert 到xt_yp_tjsy表
						foreach (var syitem in list)
						{
							var priceAdjustmentProfitLossEntity = new SysMedicinePriceAdjustmentProfitLossEntity
							{
								TjsyId = Guid.NewGuid().ToString(),
								OrganizeId = OperatorProvider.GetCurrent().OrganizeId,
								yfbmCode = syitem.yfbmCode,
								ypCode = syitem.ypCode,
								Ph = string.Empty,
								Tjsj = executeTime,
								Tjwj = syitem.tjwj,
								Dssl = syitem.dssl,
								Ypfj = syitem.ypfj,
								Ylsj = syitem.ylsj,
								Yykpfj = syitem.yykpfj,
								Yyklsj = syitem.yyklsj,
								Xpfj = syitem.xpfj,
								Xlsj = syitem.xlsj,
								Xykpfj = syitem.xykpfj,
								Xyklsj = syitem.xyklsj,
								Zhyz = syitem.zhyz,
								Pfjtjlr = syitem.pfjtjlr,
								Lsjtjlr = syitem.lsjtjlr,
								pc = string.Empty
							};
							priceAdjustmentProfitLossEntity.Create();
							db.Insert(priceAdjustmentProfitLossEntity);
						}
						db.Commit();
					}
				}
				catch (Exception ex)
				{
					AppLogger.Instance.Error("药品调价执行异常", ex, "ExecteAdjustPrice error");
					const string strSql2 = @"
update NewtouchHIS_Base.dbo.V_S_xt_yp set pfj=@pfj,lsj=@lsj where ypCode=@ypCode and OrganizeId=@OrganizeId ";
					DbParameter[] param2 =
					{
						new SqlParameter("@pfj", entity.ypfj),
						new SqlParameter("@lsj", entity.ylsj),
						new SqlParameter("@ypCode", entity.ypCode),
						new SqlParameter("@OrganizeId", OperatorProvider.GetCurrent().OrganizeId)
					};
					ExecuteSqlCommand(strSql2, param2);
					return "变更价格执行失败";
				}
				//保存变更日志老记录
				var oldMedicineEntity = medicineEntity.Clone();
				var newMedicineEntity = _sysMedicineRepo.GetMedicineByCode(OperatorProvider.GetCurrent().OrganizeId, item.ToString());

				if (oldMedicineEntity != null)
				{
					AppLogger.WriteEntityChangeRecordLog(oldMedicineEntity, newMedicineEntity, "xt_yp", oldMedicineEntity.ypId.ToString());
				}
			}
			return "操作成功";
		}

		/// <summary>
		/// 根据ypCode得到调价损益的字段
		/// </summary>
		/// <param name="ypCode"></param>
		/// <returns></returns>
		public List<PriceAdjustmentProfitLossVO> GetNeedColumnValue(string ypCode)
		{
			var strSql = new StringBuilder();
			strSql.Append(@"                           
                            select A.yfbmcode, yptj.ypcode, yptj.zxsj as tjsj, yptj.tzwj as tjwj
	                            ,A.dssl, yptj.ypfj as ypfj, yptj.ylsj as ylsj, ypfj as yykpfj, ylsj as yyklsj
	                            ,yptj.pfj as xpfj, yptj.lsj as xlsj, yptj.pfj as xykpfj, yptj.lsj as xyklsj, A.zhyz
	                            ,(yptj.pfj - yptj.ypfj) * A.dssl as pfjtjlr, (yptj.lsj - yptj.ylsj) * A.dssl as lsjtjlr
                            from xt_yptj yptj
	                            inner join NewtouchHIS_Base.dbo.V_S_xt_yp yp on yptj.ypcode = yp.ypcode and yp.organizeid = @organizeid
	                            inner join                           
				                (select sum(kcxx.kcsl/ kcxx.zhyz) as dssl,kcxx.yfbmcode,kcxx.zhyz,kcxx.ypdm 
                                    from xt_yp_kcxx(nolock) kcxx 
                                    where kcxx.ypdm = @ypCode 
                                    and kcxx.zt = '1'
		                            and isnull(kcxx.kcsl, 0) > 0
		                            and kcxx.organizeid = @organizeid
                                    group by kcxx.yfbmcode,kcxx.zhyz,kcxx.ypdm) A on yptj.ypcode = A.ypdm and yptj.organizeid = @organizeid and yptj.yfbmCode = A.yfbmCode
                            where yptj.zxsj > GETDATE()
                         ");
			DbParameter[] param =
			{
                //new SqlParameter("@yfbmCode",Constants.CurrentYfbm.yfbmCode),
                new SqlParameter("@organizeid", OperatorProvider.GetCurrent().OrganizeId),
				new SqlParameter("@ypCode", ypCode)
			};
			return FindList<PriceAdjustmentProfitLossVO>(strSql.ToString(), param).ToList();
		}

		/// <summary>
		/// 调价历史查询
		/// </summary>
		/// <param name="pagination"></param>
		/// <param name="inputCode"></param>
		/// <param name="startTime"></param>
		/// <param name="endTime"></param>
		/// <returns></returns>
		public IList<AdjustPriceHistoryInfoVO> SelectMedicineAdjustPriceHistoryInfoList(Pagination pagination,
			string inputCode, string startTime, string endTime)
		{
			var strSql = new StringBuilder();
			strSql.Append(@"
                            select yptj.ypcode,yp.ypmc,ypsx.ypgg,yp.djdw,yptj.pfj,yptj.lsj,yptj.ypfj,yptj.ylsj,itemsdetail.name tssxmc,
								  substring( sfdl.dlmc,0,len(sfdl.dlmc)) yplb,
								  yp.jl,yp.jldw,yp.bzs,yp.bzdw,yp.mzcls,yp.mzcldw,yp.zycls,yp.zycldw,yp.zxdw,yp.zfbl,
								  case yp.zfxz when '1' then '自理' when '2' then '分类自负' end zfxz,
								  ypjx.jxmc,yp.ycmc,
								  case yp.mzzybz when'1' then '启用' when '0' then '停用' end mzzybz,
								  case ypsx.jzlx when'0'then'每顿取整'when'1'then'每次取整'end jzlx,
								  ypsx.mrbzq,yptj.tzwj,
								  userStaff.name czy,
                                  yptj.zxsj,yptj.LastModifierCode as zxry,yptj.tzsj
							  from xt_yptj yptj
                              left join newtouchhis_base.dbo.V_S_xt_yp yp on yptj.ypcode = yp.ypcode and yp.OrganizeId=@OrganizeId
                              left join newtouchhis_base.dbo.V_S_xt_ypsx ypsx on ypsx.ypcode=yp.ypcode and ypsx.OrganizeId=@OrganizeId
							  left join newtouchhis_base.dbo.V_S_Sys_ItemsDetail itemsdetail on itemsdetail.code=ypsx.yptssx and (itemsDetail.TopOrganizeId=@TopOrganizeId or itemsDetail.TopOrganizeId='*')
                              left join newtouchhis_base.dbo.V_S_xt_ypjx ypjx on ypjx.jxcode =yp.jx 
                              left join newtouchhis_base.dbo.V_C_Sys_UserStaff userStaff on userStaff.account=yptj.zxczy and userStaff.OrganizeId=@OrganizeId
                              left join newtouchhis_base.dbo.V_S_xt_sfdl sfdl on sfdl.dlcode=yp.dlcode and sfdl.OrganizeId=@OrganizeId
                              where 1=1 
								  and yptj.zxbz='1' and yptj.yfbmCode=@yfbmCode and yptj.OrganizeId=@OrganizeId

                        ");
			var inSqlParameterList = new List<SqlParameter>();
			if (!string.IsNullOrEmpty(inputCode))
			{
				strSql.Append(
					" and (yptj.ypcode like @inputCode or yp.ypmc like @inputCode or yp.spm like @inputCode or yp.py like @inputCode)");
				inSqlParameterList.Add(new SqlParameter("@inputCode", "%" + inputCode.Trim() + "%"));
			}
			if (!string.IsNullOrEmpty(startTime))
			{
				strSql.Append(" and  yptj.zxsj>@startTime");
				inSqlParameterList.Add(new SqlParameter("@startTime", startTime));
			}
			if (!string.IsNullOrEmpty(endTime))
			{
				strSql.Append(" and yptj.zxsj<@endTime");
				inSqlParameterList.Add(new SqlParameter("@endTime", endTime));
			}
			inSqlParameterList.Add(new SqlParameter("@yfbmCode", Constants.CurrentYfbm.yfbmCode));
			inSqlParameterList.Add(new SqlParameter("@TopOrganizeId", Constants.TopOrganizeId));
			inSqlParameterList.Add(new SqlParameter("@OrganizeId", OperatorProvider.GetCurrent().OrganizeId));

			return QueryWithPage<AdjustPriceHistoryInfoVO>(strSql.ToString(), pagination, inSqlParameterList.ToArray());
		}

		#endregion

		#region 申领出库

		/// <summary>
		/// 获取申领单信息(显示未发放和已发放部分状态的信息)
		/// </summary>
		/// <param name="pagination"></param>
		/// <param name="sldh">申领单号</param>
		/// <param name="slbm">申领部门</param>
		/// <param name="ffzt">发药状态</param>
		/// <param name="txtStartDate"></param>
		/// <param name="txtEndDate"></param>
		/// <returns></returns>
		public IList<SysMedicineReInfoVO> GetMedicineRequestInfo(Pagination pagination, string sldh = null,
			string slbm = null, string ffzt = null, string txtStartDate = "", string txtEndDate = "")
		{
			var strSql = new StringBuilder(@"
SELECT DISTINCT a.sldId ,a.Sldh ,a.Slbm ,a.Ckbm ,y.yfbmmc ,a.ffzt,a.CreateTime
FROM dbo.XT_YP_SLD(NOLOCK) a
INNER JOIN NewtouchHIS_Base.dbo.V_S_xt_yfbm y ON yfbmCode = a.Slbm AND y.OrganizeId=a.OrganizeId
INNER JOIN dbo.xt_yp_sldmx(NOLOCK) M ON M.sldId = a.sldId
INNER JOIN NewtouchHIS_Base.dbo.xt_yp(NOLOCK) J ON M.ypCode = J.ypCode AND j.OrganizeId=a.OrganizeId AND J.zt='1'
INNER JOIN NewtouchHIS_Base.dbo.xt_ypsx(NOLOCK) ypsx ON ypsx.ypId=J.ypId AND ypsx.zt='1'
INNER JOIN dbo.xt_yp_kcxx(NOLOCK) kc ON kc.yfbmCode = a.Ckbm AND kc.OrganizeId=a.OrganizeId
WHERE a.zt = 1
AND a.OrganizeId=@OrganizeId
AND a.Sldh LIKE @sldh
AND a.Slbm LIKE @slbm
");
			var par = new List<DbParameter>
			{
				new SqlParameter("@Ckbm", Constants.CurrentYfbm.yfbmCode),
				new SqlParameter("@sldh", "%" + (string.IsNullOrEmpty(sldh) ? "" : sldh.Trim()) + "%"),
				new SqlParameter("@slbm", "%" + (string.IsNullOrEmpty(slbm) ? "" : slbm.Trim()) + "%"),
				new SqlParameter("@OrganizeId", OperatorProvider.GetCurrent().OrganizeId)
			};
			if (!string.IsNullOrWhiteSpace(txtStartDate))
			{
				var startT = Convert.ToDateTime(txtStartDate);
				startT = startT < Constants.MinDateTime ? Constants.MinDateTime : startT;
				strSql.AppendLine("AND a.CreateTime>=@startTime ");
				par.Add(new SqlParameter("@startTime", startT.ToString("yyyy-MM-dd HH:mm:ss")));
			}
			if (!string.IsNullOrWhiteSpace(txtEndDate))
			{
				var endT = Convert.ToDateTime(txtEndDate);
				endT = endT < Constants.MinDateTime ? Constants.MinDateTime : endT;
				strSql.AppendLine("AND a.CreateTime<=@endTime ");
				par.Add(new SqlParameter("@endTime", endT.ToString("yyyy-MM-dd HH:mm:ss")));
			}
			if (ffzt != "-1")
			{
				strSql.Append("AND a.ffzt=@ffzt ");
				par.Add(new SqlParameter("@ffzt", ffzt));
			}
			return QueryWithPage<SysMedicineReInfoVO>(strSql.ToString(), pagination, par.ToArray());
		}

		/// <summary>
		/// 获取申领单明细
		/// </summary>
		/// <param name="sldId"></param>
		/// <returns></returns>
		public List<SysMedicineReDetailVO> GetMedicineDetail(string sldId)
		{
			var strSql = new StringBuilder(@"
SELECT d.Slbm ,M.sldmxId ,m.sldId ,d.sldh ,M.ypcode Ypdm ,J.ypmc , ypsx.ypgg, J.py ,J.ycmc 
,CONVERT(INT,J.bzs) zhyz ,j.bzdw bzdw,M.Slsl,M.yfsl
,CONVERT(INT,(SUM(kc.kcsl-kc.djsl))) kcsl
,dbo.f_getComplexYpSlandDw(SUM(kc.kcsl-kc.djsl), J.bzs, J.bzdw, j.zxdw) kc 
,dbo.f_getComplexYpSlandDw(M.Slsl, J.bzs, J.bzdw, j.zxdw) Sl  
,dbo.f_getComplexYpSlandDw(M.yfsl, J.bzs, J.bzdw, j.zxdw) yfslStr
,CONVERT(INT,(M.Slsl - M.yfsl)/J.bzs) fysl 
,CONVERT(NUMERIC(12,4),J.pfj) Pfj 
,CONVERT(NUMERIC(12,4),J.lsj) Lsj 
,CONVERT(NUMERIC(12,2),J.pfj/J.bzs * M.Slsl) Pjze 
,CONVERT(NUMERIC(12,2),J.lsj/J.bzs * M.Slsl) lsje
FROM XT_YP_SLDMX(NOLOCK) M
INNER JOIN NewtouchHIS_Base.dbo.V_S_xt_yp(NOLOCK) J ON M.ypCode = J.ypCode and J.OrganizeId=@OrganizeId 
INNER JOIN NewtouchHIS_Base.dbo.V_S_xt_ypsx(NOLOCK) ypsx ON ypsx.ypId=J.ypId AND ypsx.OrganizeId=J.OrganizeId
INNER JOIN xt_yp_sld d(NOLOCK) ON d.sldid = m.sldid AND d.OrganizeId=j.OrganizeId
INNER JOIN NewtouchHIS_Base..V_S_xt_yfbm yfbm ON yfbm.yfbmCode = d.Ckbm AND yfbm.OrganizeId=j.OrganizeId
INNER JOIN dbo.xt_yp_kcxx(NOLOCK) kc ON kc.OrganizeId=j.OrganizeId AND kc.yxq > GETDATE() AND kc.ypdm = m.ypCode AND kc.yfbmCode = yfbm.yfbmCode
WHERE M.SldId = @sldId
AND j.OrganizeId=@OrganizeId
GROUP BY d.Slbm ,M.sldmxId ,m.sldId ,d.sldh ,M.ypcode ,J.ypmc, ypsx.ypgg ,J.py ,J.ycmc ,J.bzs, bzdw , m.Zhyz ,j.zxdw ,m.slsl ,m.yfsl ,j.pfj ,j.lsj
"
			);
			DbParameter[] par =
			{
				new SqlParameter("@sldId", sldId),
				new SqlParameter("@yfbmCode", Constants.CurrentYfbm.yfbmCode),
				new SqlParameter("@OrganizeId", OperatorProvider.GetCurrent().OrganizeId)
			};
			return FindList<SysMedicineReDetailVO>(strSql.ToString(), par);
		}

		/// <summary>
		/// 终止申领出库状态
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public void UpgradeStatus(string id)
		{
			var strSql = new StringBuilder();
			strSql.Append("UPDATE  dbo.xt_yp_sld ");
			strSql.Append("SET     ffzt = " + (int)EnumSLDDeliveryStatus.Abandon + " ");
			strSql.Append("WHERE   sldId = @sldId");
			ExecuteSqlCommand(strSql.ToString(), new SqlParameter("@sldId", id));
		}

		#endregion

		#region 进销存统计

		/// <summary>
		/// 进销存统计
		/// </summary>
		/// <param name="pagination"></param>
		/// <param name="searchParam"></param>
		/// <param name="yfbmCode"></param>
		/// <param name="organizeId"></param>
		/// <returns></returns>
		public IList<PsiStatisticsVo> GetPsiStatisticsVo(Pagination pagination, PsiStatisticsParam searchParam, string yfbmCode, string organizeId)
		{
			//            var sql = new StringBuilder(@"
			//SELECT bmypxx.Ypdm, 
			//       yp.ypmc, 
			//       ypsx.ypgg, 
			//       dbo.f_getyfbmDw(@yfbmCode,bmypxx.Ypdm,@Organizeid) dw,
			//	   kcxx.jj,
			//       yp.lsj,
			//       --dbo.f_getYfbmYpComplexYpSlandDw(qc.qckcsl, 'mzyf002', bmypxx.Ypdm, '6d5752a7-234a-403e-aa1c-df8b45d3469f') 
			//	  CONVERT(int,qc.qckcsl) qcsl
			//       ,--dbo.f_getYfbmYpComplexYpSlandDw(rk.rkkcsl, 'mzyf002', bmypxx.Ypdm, '6d5752a7-234a-403e-aa1c-df8b45d3469f') 
			//	    CONVERT(int, rk.rkkcsl) rksl
			//       ,--dbo.f_getYfbmYpComplexYpSlandDw(ck.ckkcsl, 'mzyf002', bmypxx.Ypdm, '6d5752a7-234a-403e-aa1c-df8b45d3469f')
			//	      CONVERT(int,ck.ckkcsl )cksl
			//       ,--dbo.f_getYfbmYpComplexYpSlandDw(sy.sysl, 'mzyf002', bmypxx.Ypdm, '6d5752a7-234a-403e-aa1c-df8b45d3469f') 
			//	     CONVERT(int,sy.sysl) sysl
			//       ,qc.pfze qcpfze,
			//       rk.pfze rkpfze,
			//       ck.pfze ckpfze,
			//       sy.pfze sypfze,
			//      -- qm.pfze qmpfze,
			//       tjsy.tjsyze tjsyze,
			//	   --dbo.f_getYfbmYpComplexYpSlandDw(pd.sysl, 'mzyf002', bmypxx.Ypdm, '6d5752a7-234a-403e-aa1c-df8b45d3469f')
			//       (CASE WHEN pd.sysl>0 then  pd.sysl else '0' end) pysl,
			//	   --dbo.f_getYfbmYpComplexYpSlandDw(pd.sysl, 'mzyf002', bmypxx.Ypdm, '6d5752a7-234a-403e-aa1c-df8b45d3469f') 
			//       (CASE WHEN pd.sysl<0 then pd.sysl else '0' end) pksl,
			//       (CASE WHEN pd.sysl>0 then pd.pfze else '0' end) pyze,(CASE WHEN pd.sysl<0 then pd.pfze else '0' end) pkze
			//	      ,--dbo.f_getYfbmYpComplexYpSlandDw(qm.qmkcsl, 'mzyf002', bmypxx.Ypdm, '6d5752a7-234a-403e-aa1c-df8b45d3469f') 
			//	    CONVERT(int, (qc.qckcsl+ rk.rkkcsl-ck.ckkcsl+sy.sysl+pd.sysl)) qmsl,
			//      CONVERT(DECIMAL(11, 2), CONVERT(int, (qc.qckcsl+ rk.rkkcsl-ck.ckkcsl+sy.sysl+pd.sysl))*qm.pfj, 0) qmpfze
			//FROM dbo.xt_yp_bmypxx(NOLOCK) bmypxx
			//INNER JOIN NewtouchHIS_Base.dbo.V_S_xt_yp yp ON yp.ypCode=bmypxx.Ypdm AND yp.OrganizeId=bmypxx.OrganizeId
			//INNER JOIN NewtouchHIS_Base.dbo.V_S_xt_ypsx ypsx ON ypsx.ypId=yp.ypId AND ypsx.OrganizeId=bmypxx.OrganizeId
			//INNER join(
			// select kcxx.OrganizeId,kcxx.ypdm,max(yfbmCode) yfbmCode,max(kcxx.jj) jj 
			//  from NewtouchHIS_PDS.dbo.xt_yp_kcxx(NOLOCK)  as kcxx 
			//  group by kcxx.OrganizeId,kcxx.ypdm,kcxx.yfbmCode
			//) as kcxx on kcxx.yfbmCode = bmypxx.yfbmCode and kcxx.ypdm = bmypxx.Ypdm and kcxx.OrganizeId = bmypxx.OrganizeId
			//LEFT JOIN 
			//(
			//	SELECT bmypxx.Ypdm, ISNULL(SUM(kcjz.Kcsl/kcjz.Zhyz), 0) qckcsl, CONVERT(DECIMAL(11, 2), ISNULL(SUM(kcjz.Ykpfj/yp2.bzs*kcjz.Kcsl), 0)) pfze 
			//	FROM dbo.xt_yp_bmypxx(NOLOCK) bmypxx
			//	INNER JOIN NewtouchHIS_Base.dbo.V_S_xt_yp yp2 ON yp2.ypCode=bmypxx.Ypdm AND yp2.OrganizeId=bmypxx.OrganizeId
			//	INNER JOIN NewtouchHIS_Base.dbo.V_S_xt_ypsx ypsx2 ON ypsx2.ypId=yp2.ypId AND ypsx2.OrganizeId=bmypxx.OrganizeId
			//	LEFT JOIN dbo.xt_yp_kcjzk(NOLOCK) kcjz ON kcjz.Ypdm=bmypxx.Ypdm AND kcjz.OrganizeId=bmypxx.OrganizeId AND Jzsj =(select top 1 Jzsj   from  xt_yp_kcjzk where yfbmCode=@YfbmCode and OrganizeId=@Organizeid  and jzsj<@ksjzsj order by Jzsj desc) AND kcjz.zt='1' AND kcjz.yfbmCode=bmypxx.yfbmCode
			//	WHERE bmypxx.yfbmCode=@YfbmCode
			//	AND bmypxx.OrganizeId=@Organizeid
			//	GROUP BY bmypxx.Ypdm
			//) qc ON qc.Ypdm=bmypxx.Ypdm
			//LEFT JOIN 
			//(
			//	SELECT bmypxx.Ypdm, ISNULL(SUM(rkdjmx.Sl/rkdjmx.Rkzhyz), 0) rkkcsl, CONVERT(DECIMAL(11, 2), ISNULL(SUM(rkdjmx.Pfj/yp2.bzs*rkdjmx.Rkzhyz*rkdjmx.Sl), 0)) pfze 
			//	FROM dbo.xt_yp_bmypxx(NOLOCK) bmypxx
			//	INNER JOIN NewtouchHIS_Base.dbo.V_S_xt_yp yp2 ON yp2.ypCode=bmypxx.Ypdm AND yp2.OrganizeId=bmypxx.OrganizeId
			//	INNER JOIN NewtouchHIS_Base.dbo.V_S_xt_ypsx ypsx2 ON ypsx2.ypId=yp2.ypId AND ypsx2.OrganizeId=bmypxx.OrganizeId
			//	LEFT JOIN 
			//	(
			//		SELECT Ypdm, Sl, Pfj, (case when Rkzhyz=0 then Ckzhyz else Rkzhyz end) Rkzhyz 
			//        FROM dbo.xt_yp_crkmx(NOLOCK) mx
			//		INNER JOIN dbo.xt_yp_crkdj(NOLOCK) dj ON mx.crkId=dj.crkId AND dj.zt='1'
			//		WHERE dj.shzt =@shzt AND mx.zt='1' and dj.OrganizeId=@Organizeid AND dj.Rkbm=@YfbmCode AND dj.Rksj BETWEEN CONVERT(DATETIME, @ksjzsj) AND CONVERT(DATETIME, @jsjzsj)
			//		AND dj.djlx IN (1,5)	
			//    ) rkdjmx ON rkdjmx.Ypdm=bmypxx.Ypdm
			//	WHERE bmypxx.OrganizeId=@Organizeid
			//	AND bmypxx.yfbmCode=@YfbmCode
			//	GROUP BY bmypxx.Ypdm
			//) rk ON rk.Ypdm=bmypxx.Ypdm
			//LEFT JOIN 
			//(
			//	SELECT bmypxx.Ypdm, ISNULL(SUM(ckdjmx.Sl/ckdjmx.Ckzhyz), 0) ckkcsl, CONVERT(DECIMAL(11, 2), ISNULL(SUM(case when ckdjmx.Pfj=0 then ckdjmx.dj*ckdjmx.Sl else ckdjmx.Pfj/yp2.bzs*ckdjmx.Ckzhyz*ckdjmx.Sl end), 0)) pfze 
			//	FROM dbo.xt_yp_bmypxx(NOLOCK) bmypxx
			//	INNER JOIN NewtouchHIS_Base.dbo.V_S_xt_yp yp2 ON yp2.ypCode=bmypxx.Ypdm AND yp2.OrganizeId=bmypxx.OrganizeId
			//	INNER JOIN NewtouchHIS_Base.dbo.V_S_xt_ypsx ypsx2 ON ypsx2.ypId=yp2.ypId AND ypsx2.OrganizeId=bmypxx.OrganizeId
			//	LEFT JOIN 
			//	(
			//		SELECT Ypdm, Sl, Pfj, Ckzhyz, 0 dj
			//        FROM dbo.xt_yp_crkmx(NOLOCK) mx
			//		INNER JOIN dbo.xt_yp_crkdj(NOLOCK) dj ON mx.crkId=dj.crkId AND dj.zt='1'
			//		WHERE dj.OrganizeId=@Organizeid AND mx.zt='1' AND dj.Ckbm=@YfbmCode AND dj.Cksj BETWEEN CONVERT(DATETIME, @ksjzsj) AND CONVERT(DATETIME, @jsjzsj)
			//	    AND dj.djlx IN (2,3,4,5,6)
			//        --UNION ALL
			//	    --select czjl.ypCode Ypdm, case when czjl.operateType ='1' then czjl.sl else -1*czjl.sl end Sl, 0 Pfj, cfmx.zhyz Ckzhyz,cfmx.dj  
			//        --from dbo.mz_cfypczjl(NOLOCK) czjl
			//		--INNER JOIN dbo.mz_cfmx(NOLOCK) cfmx ON cfmx.Id = czjl.mzcfmxid and cfmx.OrganizeId =@Organizeid
			//		--INNER JOIN dbo.mz_cf(NOLOCK) cf on cf.cfh = cfmx.cfh and cf.OrganizeId = @Organizeid and cf.lyyf = @YfbmCode and cf.zt = '1'
			//       -- WHERE czjl.CreateTime BETWEEN CONVERT(DATETIME, @ksjzsj) AND CONVERT(DATETIME, @jsjzsj)
			//		--UNION ALL
			//        --SELECT c.ypCode Ypdm,case when c.operateType ='1' then c.sl else -1*c.sl end Sl, 0 Pfj, dbo.f_getyfbmZhyz(d.fyyf,c.ypcode,d.OrganizeId) Ckzhyz, d.dj  
			//       -- FROM dbo.zy_ypyzczjl(NOLOCK) c 
			//		--INNER JOIN dbo.zy_ypyzxx(NOLOCK) d ON c.ypyzxxId = d.Id and d.OrganizeId = @Organizeid and d.fyyf = @YfbmCode
			//       -- WHERE c.CreateTime BETWEEN CONVERT(DATETIME, @ksjzsj) AND CONVERT(DATETIME, @jsjzsj)
			//    ) ckdjmx ON ckdjmx.Ypdm=bmypxx.Ypdm
			//	WHERE bmypxx.OrganizeId=@Organizeid
			//	AND bmypxx.yfbmCode=@YfbmCode
			//	GROUP BY bmypxx.Ypdm
			//) ck ON ck.Ypdm=bmypxx.Ypdm
			//LEFT JOIN 
			//(
			//	SELECT bmypxx.Ypdm, ISNULL(SUM(syxx.Sysl/syxx.Zhyz), 0) sysl, CONVERT(DECIMAL(11, 2), ISNULL(SUM(syxx.Pfj/yp2.bzs*syxx.Sysl), 0)) pfze 
			//	FROM dbo.xt_yp_bmypxx(NOLOCK) bmypxx
			//	INNER JOIN NewtouchHIS_Base.dbo.V_S_xt_yp yp2 ON yp2.ypCode=bmypxx.Ypdm AND yp2.OrganizeId=bmypxx.OrganizeId
			//	INNER JOIN NewtouchHIS_Base.dbo.V_S_xt_ypsx ypsx2 ON ypsx2.ypId=yp2.ypId AND ypsx2.OrganizeId=bmypxx.OrganizeId
			//	LEFT JOIN dbo.xt_yp_syxx(NOLOCK) syxx ON syxx.zt='1' AND syxx.Ypdm=bmypxx.Ypdm AND syxx.OrganizeId=bmypxx.OrganizeId AND syxx.yfbmCode=bmypxx.yfbmCode AND syxx.Bgsj BETWEEN CONVERT(DATETIME, @ksjzsj) AND CONVERT(DATETIME, @jsjzsj)
			//	WHERE bmypxx.yfbmCode=@YfbmCode
			//	AND bmypxx.OrganizeId=@Organizeid
			//	GROUP BY bmypxx.Ypdm
			//) sy ON sy.Ypdm=bmypxx.Ypdm
			//LEFT JOIN 
			//(
			//	SELECT bmypxx.Ypdm, ISNULL(SUM(syxx.Pdsl), 0) sysl, CONVERT(DECIMAL(11, 2), ISNULL(SUM(syxx.Pfj/yp2.bzs*syxx.Pdsl), 0)) pfze 
			//	FROM dbo.xt_yp_bmypxx(NOLOCK) bmypxx
			//	INNER JOIN NewtouchHIS_Base.dbo.V_S_xt_yp yp2 ON yp2.ypCode=bmypxx.Ypdm AND yp2.OrganizeId=bmypxx.OrganizeId
			//	INNER JOIN NewtouchHIS_Base.dbo.V_S_xt_ypsx ypsx2 ON ypsx2.ypId=yp2.ypId AND ypsx2.OrganizeId=bmypxx.OrganizeId
			//	LEFT JOIN dbo.xt_yp_pdmx(NOLOCK) syxx ON syxx.zt='1' AND syxx.Ypdm=bmypxx.Ypdm AND syxx.OrganizeId=bmypxx.OrganizeId AND syxx.yfbmCode=bmypxx.yfbmCode AND syxx.Bgsj BETWEEN CONVERT(DATETIME, @ksjzsj) AND CONVERT(DATETIME, @jsjzsj)
			//	WHERE bmypxx.yfbmCode=@YfbmCode
			//	AND bmypxx.OrganizeId=@Organizeid
			//	GROUP BY bmypxx.Ypdm
			//) pd ON pd.Ypdm=bmypxx.Ypdm ");
			//            if (string.IsNullOrWhiteSpace(searchParam.jsjzsj))
			//            {
			//                sql.Append(@"
			//LEFT JOIN 
			//(
			//	SELECT bmypxx.Ypdm, ISNULL(SUM(kcxx.Kcsl), 0) qmkcsl, CONVERT(DECIMAL(11, 2), ISNULL(SUM(yp2.pfj/yp2.bzs*kcxx.Kcsl), 0)) pfze,yp2.pfj,yp2.bzs
			//	FROM dbo.xt_yp_bmypxx(NOLOCK) bmypxx
			//	INNER JOIN NewtouchHIS_Base.dbo.V_S_xt_yp yp2 ON yp2.ypCode=bmypxx.Ypdm AND yp2.OrganizeId=bmypxx.OrganizeId
			//	INNER JOIN NewtouchHIS_Base.dbo.V_S_xt_ypsx ypsx2 ON ypsx2.ypId=yp2.ypId AND ypsx2.OrganizeId=bmypxx.OrganizeId
			//	LEFT JOIN dbo.xt_yp_kcxx(NOLOCK) kcxx ON kcxx.Ypdm=bmypxx.Ypdm AND kcxx.OrganizeId=bmypxx.OrganizeId AND kcxx.yfbmCode=bmypxx.yfbmCode
			//	WHERE bmypxx.yfbmCode=@YfbmCode
			//	AND bmypxx.OrganizeId=@Organizeid
			//	GROUP BY bmypxx.Ypdm,yp2.pfj,yp2.bzs
			//) qm ON qm.Ypdm=bmypxx.Ypdm ");
			//            }
			//            else
			//            {
			//                sql.Append(@"
			//LEFT JOIN 
			//(
			//	SELECT bmypxx.Ypdm, ISNULL(SUM(kcjz.Kcsl), 0) qmkcsl, CONVERT(DECIMAL(11, 2), ISNULL(SUM(kcjz.Ykpfj/yp2.bzs*kcjz.Kcsl), 0)) pfze,yp2.pfj,yp2.bzs
			//	FROM dbo.xt_yp_bmypxx(NOLOCK) bmypxx
			//	INNER JOIN NewtouchHIS_Base.dbo.V_S_xt_yp yp2 ON yp2.ypCode=bmypxx.Ypdm AND yp2.OrganizeId=bmypxx.OrganizeId
			//	INNER JOIN NewtouchHIS_Base.dbo.V_S_xt_ypsx ypsx2 ON ypsx2.ypId=yp2.ypId AND ypsx2.OrganizeId=bmypxx.OrganizeId
			//	LEFT JOIN dbo.xt_yp_kcjzk(NOLOCK) kcjz ON kcjz.Ypdm=bmypxx.Ypdm AND kcjz.OrganizeId=bmypxx.OrganizeId AND CONVERT(VARCHAR(19),kcjz.Jzsj,120)=@jsjzsj AND kcjz.zt='1' AND kcjz.yfbmCode=bmypxx.yfbmCode
			//	WHERE bmypxx.yfbmCode=@YfbmCode
			//	AND bmypxx.OrganizeId=@Organizeid
			//	GROUP BY bmypxx.Ypdm,yp2.pfj,yp2.bzs
			//) qm ON qm.Ypdm=bmypxx.Ypdm ");
			//            }
			//            sql.Append(@"
			//LEFT JOIN 
			//(
			//	SELECT bmypxx.Ypdm, CONVERT(DECIMAL(11, 2), ISNULL(SUM(tjsy.Pfjtjlr), 0)) tjsyze
			//	FROM dbo.xt_yp_bmypxx(NOLOCK) bmypxx
			//	LEFT JOIN dbo.xt_yp_tjsy(NOLOCK) tjsy ON tjsy.ypCode=bmypxx.Ypdm AND tjsy.yfbmCode=bmypxx.yfbmCode AND tjsy.Tjsj BETWEEN CONVERT(DATETIME, @ksjzsj) AND CONVERT(DATETIME, @jsjzsj) AND tjsy.OrganizeId=bmypxx.OrganizeId
			//	WHERE bmypxx.yfbmCode=@YfbmCode
			//	AND bmypxx.OrganizeId=@Organizeid
			//	GROUP BY bmypxx.Ypdm
			//) tjsy ON tjsy.Ypdm=bmypxx.Ypdm
			//WHERE bmypxx.OrganizeId=@Organizeid
			//AND bmypxx.yfbmCode=@YfbmCode
			//AND (yp.zt=@ypzt OR LTRIM(RTRIM(ISNULL(@ypzt, '')))='')
			//AND (yp.ypCode LIKE '%'+@srm+'%' OR yp.ypmc LIKE '%'+@srm+'%' OR yp.py LIKE '%'+@srm+'%')
			//AND (yp.dlCode=@dl OR LTRIM(RTRIM(ISNULL(@dl, '')))='')
			//AND (yp.jx=@jx OR LTRIM(RTRIM(ISNULL(@jx, '')))='')
			//AND ((yp.lsj-yp.pfj)=CONVERT(NUMERIC(11,4),@rate) OR -1=CONVERT(NUMERIC(11,4),ISNULL(@rate, -1))) 

			//");
			//			if (string.IsNullOrWhiteSpace(searchParam.noPSI) || "0".Equals(searchParam.noPSI))
			//			{
			//				sql.AppendLine("AND (rk.rkkcsl<>0 OR ck.ckkcsl<>0 OR sy.sysl<>0 OR pd.sysl<>0) ");
			//			}

			var param = new DbParameter[]
			{
				new SqlParameter("@ksjzsj", searchParam.ksjzsj),
				new SqlParameter("@jsjzsj", string.IsNullOrWhiteSpace(searchParam.jsjzsj)?DateTime.Now.ToString(Constants.DateTimeFormat):searchParam.jsjzsj),
				new SqlParameter("@Organizeid", organizeId),
				new SqlParameter("@YfbmCode", yfbmCode),
				new SqlParameter("@ypzt", searchParam.ypzt ?? ""),
				new SqlParameter("@srm", searchParam.srm ?? ""),
				new SqlParameter("@dl", searchParam.dl ?? ""),
				new SqlParameter("@jx", searchParam.jx ?? ""),
				new SqlParameter("@rate",string.IsNullOrWhiteSpace(searchParam.rate) ? -1 : Convert.ToDecimal(searchParam.rate)),
				new SqlParameter("@shzt",(int)EnumDjShzt.Approved)
			};
			var sql = @" exec sp_yp_jxctj_01 @ksjzsj=@ksjzsj,@jsjzsj=@jsjzsj,@Organizeid=@Organizeid,@YfbmCode=@YfbmCode,@ypzt=@ypzt,@srm=@srm,@dl=@dl,@jx=@jx,@rate=@rate,@shzt=@shzt";
			var result = FindList<PsiStatisticsVo>(sql.ToString(), param);
			pagination.records = result.Count();
			var list = result.Skip((pagination.page-1) * pagination.rows).Take(pagination.rows).ToList();
			return list;
		}

		#endregion

		#region 获取本部门药品和库存信息

		/// <summary>
		/// 获取品，不分批次批号，含有效期
		/// </summary>
		/// <param name="srm">关键字</param>
		/// <param name="yfbmCode">当前部门</param>
		/// <param name="organizeId"></param>
		/// <param name="topCount"></param>
		/// <returns></returns>
		public List<DrugStockInfoVEntity> GetMedicinesrmList(string srm, string yfbmCode, string organizeId, int topCount = 100)
		{
			var sql = new StringBuilder(@"SELECT TOP " + topCount + " bmypxx.Ypdm, yp.ypmc, yp.dlCode, sfdl.dlmc, ypsx.ypgg gg, kcxx.yxq, CONVERT(INT, yp.bzs) bzs, yp.bzdw, yp.zxdw, kcxx.zhyz, yp.ycmc sccj ");
			sql.Append(@"
,CONVERT(INT,ISNULL(SUM(kcxx.kcsl-kcxx.djsl),0)) kcsl
,CONVERT(INT,ISNULL(SUM(kcxx.kcsl-kcxx.djsl)/kcxx.zhyz,0)) sl
,dbo.f_getYfbmYpComplexYpSlandDw(ISNULL(SUM(kcxx.kcsl-kcxx.djsl),0), @YfbmCode, bmypxx.Ypdm, @Organizeid) slStr
,CONVERT(INT,ISNULL(SUM(kcxx.kcsl-kcxx.djsl)/yp.bzs,0)) bzdwsl
,dbo.f_getyfbmDw(@YfbmCode, bmypxx.Ypdm, @Organizeid) dw
,CONVERT(NUMERIC(11,4),ISNULL(yp.pfj/yp.bzs*dbo.f_getyfbmZhyz(@YfbmCode, bmypxx.Ypdm, @Organizeid),0)) pfj
,CONVERT(NUMERIC(11,4),ISNULL(yp.lsj/yp.bzs*dbo.f_getyfbmZhyz(@YfbmCode, bmypxx.Ypdm, @Organizeid),0)) lsj
,CONVERT(NUMERIC(11,4),yp.pfj) ykpfj, CONVERT(NUMERIC(11,4),yp.lsj) yklsj
FROM dbo.xt_yp_bmypxx(NOLOCK) bmypxx
INNER JOIN NewtouchHIS_Base.dbo.V_S_xt_yp yp ON yp.ypCode=bmypxx.Ypdm AND yp.OrganizeId=bmypxx.OrganizeId
INNER JOIN NewtouchHIS_Base.dbo.V_S_xt_ypsx ypsx ON ypsx.ypId=yp.ypId AND ypsx.OrganizeId=bmypxx.OrganizeId
LEFT JOIN dbo.xt_yp_kcxx(NOLOCK) kcxx ON kcxx.ypdm=bmypxx.Ypdm AND kcxx.yfbmCode=bmypxx.yfbmCode AND kcxx.OrganizeId=bmypxx.OrganizeId
LEFT JOIN NewtouchHIS_Base.dbo.V_S_xt_sfdl sfdl ON sfdl.dlCode=yp.dlCode AND sfdl.OrganizeId=bmypxx.OrganizeId
WHERE bmypxx.OrganizeId=@Organizeid
AND bmypxx.yfbmCode=@yfbmCode
AND (yp.ypCode LIKE '%'+@srm+'%' OR yp.ypmc LIKE '%'+@srm+'%' OR yp.py LIKE '%'+@srm+'%')
GROUP BY bmypxx.Ypdm, yp.ypmc, yp.dlCode, sfdl.dlmc, ypsx.ypgg, kcxx.yxq, yp.bzs, yp.bzdw, yp.zxdw, kcxx.zhyz, yp.ycmc, yp.pfj, yp.lsj
ORDER BY yp.ypmc
");
			var param = new DbParameter[]
			{
				new SqlParameter("@Organizeid", organizeId),
				new SqlParameter("@yfbmCode", yfbmCode),
				new SqlParameter("@srm", srm ?? "")
			};
			return FindList<DrugStockInfoVEntity>(sql.ToString(), param);
		}

		/// <summary>
		/// 获取在有效期内的药品，不分批次批号，不含有效期
		/// </summary>
		/// <param name="srm">关键字</param>
		/// <param name="yfbmCode">当前部门</param>
		/// <param name="organizeId"></param>
		/// <param name="topCount"></param>
		/// <returns></returns>
		public List<DrugStockInfoVEntity> GetValidMedicinesrmList(string srm, string yfbmCode, string organizeId, int topCount = 100)
		{
			var sql = new StringBuilder(@"SELECT TOP " + topCount + " bmypxx.Ypdm, yp.ypmc, yp.dlCode, sfdl.dlmc, ypsx.ypgg gg, CONVERT(INT, yp.bzs) bzs, yp.bzdw, yp.zxdw, kcxx.zhyz, yp.ycmc sccj ");
			sql.Append(@"
,CONVERT(INT,ISNULL(SUM(kcxx.kcsl-kcxx.djsl),0)) kcsl
,CONVERT(INT,ISNULL(SUM(kcxx.kcsl-kcxx.djsl)/kcxx.zhyz,0)) sl
,dbo.f_getYfbmYpComplexYpSlandDw(ISNULL(SUM(kcxx.kcsl-kcxx.djsl),0), @YfbmCode, bmypxx.Ypdm, @Organizeid) slStr
,CONVERT(INT,ISNULL(SUM(kcxx.kcsl-kcxx.djsl)/yp.bzs,0)) bzdwsl
,dbo.f_getyfbmDw(@YfbmCode, bmypxx.Ypdm, @Organizeid) dw
,CONVERT(NUMERIC(11,4),ISNULL(yp.pfj/yp.bzs*dbo.f_getyfbmZhyz(@YfbmCode, bmypxx.Ypdm, @Organizeid),0)) pfj
,CONVERT(NUMERIC(11,4),ISNULL(yp.lsj/yp.bzs*dbo.f_getyfbmZhyz(@YfbmCode, bmypxx.Ypdm, @Organizeid),0)) lsj
,CONVERT(NUMERIC(11,4),yp.pfj) ykpfj, CONVERT(NUMERIC(11,4),yp.lsj) yklsj
FROM dbo.xt_yp_bmypxx(NOLOCK) bmypxx
INNER JOIN NewtouchHIS_Base.dbo.V_S_xt_yp yp ON yp.ypCode=bmypxx.Ypdm AND yp.OrganizeId=bmypxx.OrganizeId
INNER JOIN NewtouchHIS_Base.dbo.V_S_xt_ypsx ypsx ON ypsx.ypId=yp.ypId AND ypsx.OrganizeId=bmypxx.OrganizeId
LEFT JOIN dbo.xt_yp_kcxx(NOLOCK) kcxx ON kcxx.ypdm=bmypxx.Ypdm AND kcxx.yfbmCode=bmypxx.yfbmCode AND kcxx.OrganizeId=bmypxx.OrganizeId
LEFT JOIN NewtouchHIS_Base.dbo.V_S_xt_sfdl sfdl ON sfdl.dlCode=yp.dlCode AND sfdl.OrganizeId=bmypxx.OrganizeId
WHERE bmypxx.OrganizeId=@Organizeid
AND bmypxx.yfbmCode=@YfbmCode
AND kcxx.yxq >GETDATE()
AND (yp.ypCode LIKE '%'+@srm+'%' OR yp.ypmc LIKE '%'+@srm+'%' OR yp.py LIKE '%'+@srm+'%')
GROUP BY bmypxx.Ypdm, yp.ypmc, yp.dlCode, sfdl.dlmc, ypsx.ypgg, yp.bzs, yp.bzdw, yp.zxdw, kcxx.zhyz, yp.ycmc, yp.pfj, yp.lsj
ORDER BY yp.ypmc
");
			var param = new DbParameter[]
			{
				new SqlParameter("@Organizeid", organizeId),
				new SqlParameter("@yfbmCode", yfbmCode),
				new SqlParameter("@srm", srm ?? "")
			};
			return FindList<DrugStockInfoVEntity>(sql.ToString(), param);
		}

		/// <summary>
		/// 获取在有效期内的药品，不分批次批号，不含有效期
		/// </summary>
		/// <param name="ypdm">药品代码</param>
		/// <param name="yfbmCode">当前部门</param>
		/// <param name="organizeId"></param>
		/// <param name="topCount"></param>
		/// <returns></returns>
		public List<DrugStockInfoVEntity> GetPcKcList(string ypdm, string yfbmCode, string organizeId, int topCount = 100)
		{
			var sql = new StringBuilder(@"SELECT TOP " + topCount + " bmypxx.Ypdm, kcxx.yxq, kcxx.pc, kcxx.ph ");
			sql.Append(@"
,CONVERT(INT,ISNULL(SUM(kcxx.kcsl-kcxx.djsl),0)) kcsl
,CONVERT(INT,ISNULL(SUM(kcxx.kcsl-kcxx.djsl)/kcxx.zhyz,0)) sl
,dbo.f_getyfbmDw(@YfbmCode, bmypxx.Ypdm, @Organizeid) dw
,dbo.f_getYfbmYpComplexYpSlandDw(ISNULL(SUM(kcxx.kcsl-kcxx.djsl),0), @YfbmCode, bmypxx.Ypdm, @Organizeid) slStr
,CONVERT(INT,ISNULL(SUM(kcxx.kcsl-kcxx.djsl)/yp.bzs,0)) bzdwsl
FROM dbo.xt_yp_bmypxx(NOLOCK) bmypxx
INNER JOIN NewtouchHIS_Base.dbo.V_S_xt_yp yp ON yp.ypCode=bmypxx.Ypdm AND yp.OrganizeId=bmypxx.OrganizeId
INNER JOIN NewtouchHIS_Base.dbo.V_S_xt_ypsx ypsx ON ypsx.ypId=yp.ypId AND ypsx.OrganizeId=bmypxx.OrganizeId
LEFT JOIN dbo.xt_yp_kcxx(NOLOCK) kcxx ON kcxx.ypdm=bmypxx.Ypdm AND kcxx.yfbmCode=bmypxx.yfbmCode AND kcxx.OrganizeId=bmypxx.OrganizeId
WHERE bmypxx.OrganizeId=@Organizeid
AND bmypxx.yfbmCode=@YfbmCode
AND yp.ypCode=@ypdm
GROUP BY bmypxx.Ypdm, yp.bzs, yp.bzdw, yp.zxdw, kcxx.zhyz, kcxx.pc, kcxx.ph, kcxx.yxq
ORDER BY yp.ypmc
");
			var param = new DbParameter[]
			{
				new SqlParameter("@Organizeid", organizeId),
				new SqlParameter("@yfbmCode", yfbmCode),
				new SqlParameter("@ypdm", ypdm)
			};
			return FindList<DrugStockInfoVEntity>(sql.ToString(), param);
		}

		#endregion
	}
}
