using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Text;
using FrameworkBase.MultiOrg.Infrastructure;
using Newtouch.Common.Operator;
using Newtouch.Core.Common;
using Newtouch.Core.Common.Exceptions;
using Newtouch.Herp.Domain.DTO.InputDto;
using Newtouch.Herp.Domain.Entity;
using Newtouch.Herp.Domain.Entity.VEntity;
using Newtouch.Herp.Domain.IDomainServices;
using Newtouch.Herp.Domain.IRepository;
using Newtouch.Herp.Infrastructure;
using Newtouch.Herp.Infrastructure.Enum;

namespace Newtouch.Herp.DomainServices.Product
{
    /// <summary>
    /// 物资调价信息
    /// </summary>
    public class WzPriceAdjustmentDmnService : DmnServiceBase, IWzPriceAdjustmentDmnService
    {
        private readonly IWzProductDmnService _wzProductDmnService;
        private readonly IWzProductRepo _wzProductRepo;
        private readonly IWzPriceAdjustmentProfitAndLossRepo _wzPriceAdjustmentProfitAndLossRepo;
        private readonly IWzPriceAdjustmentRepo _wzPriceAdjustmentRepo;

        public WzPriceAdjustmentDmnService(IDefaultDatabaseFactory databaseFactory) : base(databaseFactory)
        {
        }

        /// <summary>
        /// 获取调价信息
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="keyWord"></param>
        /// <param name="shzt"></param>
        /// <param name="organizeId"></param>
        /// <param name="zxbz"></param>
        /// <returns></returns>
        public IList<VPriceAdjustmentInfoEntity> GetPriceAdjustmentList(Pagination pagination, string keyWord, string shzt, string organizeId, string zxbz = "")
        {
            const string sql = @"
SELECT pa.wztjId, pa.shzt, pa.zxbz, lb.name lbmc, wz.name wzmc, wz.gg, pa.lsj, pa.ylsj, pa.zhyz, pa.dwmc, gys.name supplierName, pa.zt, pa.tzwj, pa.tzsj, pa.zxsj
,CASE WHEN pa.zxsj<=getdate() then 'true' else 'false' end Isgq  
FROM dbo.wz_priceAdjustment(NOLOCK) pa
INNER JOIN dbo.wz_product(NOLOCK) wz ON wz.Id=pa.productId AND wz.OrganizeId=pa.OrganizeId AND wz.zt='1'
LEFT JOIN NewtouchHIS_Base.dbo.wz_type(NOLOCK) lb ON lb.Id=wz.typeId AND lb.zt='1' 
LEFT JOIN dbo.gys_supplier(NOLOCK) gys ON gys.Id=wz.supplierId  AND gys.zt='1'
WHERE pa.OrganizeId=@OrganizeId
AND (pa.shzt=@shzt OR ''=@shzt)
AND (wz.py LIKE '%'+ @keyWord +'%' OR wz.name LIKE '%'+@keyWord+'%')
AND (pa.zxbz=@zxbz OR ''=@zxbz) 
";
            var param = new DbParameter[]
            {
                new SqlParameter("@keyWord", keyWord??""),
                new SqlParameter("@OrganizeId", organizeId),
                new SqlParameter("@shzt", shzt??""),
                new SqlParameter("@zxbz", zxbz)
            };
            return QueryWithPage<VPriceAdjustmentInfoEntity>(sql, pagination, param);
        }

        /// <summary>
        /// 审核调价
        /// </summary>
        /// <param name="idList"></param>
        /// <param name="operationType"></param>
        /// <param name="userCode"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        public string Approval(List<string> idList, string operationType, string userCode, string organizeId)
        {
            if (idList == null || idList.Count <= 0) return "";
            var expireApply = new List<string>();
            const string sql = @"
DECLARE @zxsj DATETIME;
SELECT @zxsj=zxsj FROM dbo.wz_priceAdjustment WHERE wztjId=@Id AND OrganizeId=@OrganizeId AND zxbz=@zxbz
IF @zxsj>GETDATE()
BEGIN
	UPDATE dbo.wz_priceAdjustment 
	SET shzt=@shzt, shczy=@userCode, LastModifierCode=@userCode, LastModifyTime=GETDATE() 
	WHERE wztjId=@Id AND OrganizeId=@OrganizeId AND zxbz=@zxbz
END
SELECT wz.name wzmc, pa.ylsj, pa.lsj, pa.zhyz, pa.zxsj 
FROM dbo.wz_priceAdjustment pa
INNER JOIN dbo.wz_product(NOLOCK) wz ON wz.Id=pa.productId AND wz.OrganizeId=pa.OrganizeId AND wz.zt='1'
WHERE pa.wztjId=@Id AND pa.zxbz=@zxbz AND pa.OrganizeId=@OrganizeId 
";
            DbParameter[] param;
            idList.ForEach(p =>
            {
                if (operationType == ((int)EnumTjShzt.Revoke).ToString())
                {
                    operationType = ((int)EnumTjShzt.Waiting).ToString();
                    userCode = "";
                }
                param = new DbParameter[]
                {
                    new SqlParameter("@shzt", operationType),
                    new SqlParameter("@userCode", userCode),
                    new SqlParameter("@Id", p),
                    new SqlParameter("@OrganizeId", organizeId),
                    new SqlParameter("@zxbz", ((int)EnumTjZxbz.Not).ToString())
                };
                var ret = FirstOrDefault<VPriceAdjustmentInfoEntity>(sql, param);
                if (ret != null && ret.zxsj < DateTime.Now && ret.zxsj > Constants.MinDateTime) expireApply.Add(ret.wzmc);
            });
            return expireApply.Count > 0 ? string.Format("药品代码为{0}的药品操作失败，已过最晚执行时间。", string.Join(",", expireApply)) : "";
        }

        /// <summary>
        /// 获取调价损益信息
        /// </summary>
        /// <param name="wztjId"></param>
        /// <param name="productId"></param>
        /// <param name="organizeid"></param>
        /// <returns></returns>
        public List<VPriceAdjustmentProfitLossEntity> GetPriceAdjustmentProfitLoss(string wztjId, string productId, string organizeid)
        {
            const string sql = @"
IF EXISTS(SELECT 1 FROM tempdb..sysobjects where id=object_id(N'tempdb..#kcxx') and type='U')
BEGIN
	DROP TABLE #kcxx;
END
SELECT SUM(kcsl/zhyz) AS dssl, warehouseId, zhyz, productId
INTO #kcxx 
FROM dbo.kf_kcxx 
WHERE productId=@proId AND ISNULL(kcsl, 0)>0 AND OrganizeId=@OrganizeId AND zt='1' 
GROUP BY warehouseId, zhyz, productId

SELECT tj.productId, kcxx.warehouseId, tj.zxsj tjsj, tj.tzwj tjwj, kcxx.dssl, tj.ylsj, tj.lsj xlsj, kcxx.zhyz
,(tj.lsj-tj.ylsj)*kcxx.dssl lsjtjlr
FROM dbo.wz_priceAdjustment(NOLOCK) tj
INNER JOIN dbo.wz_product(NOLOCK) wz ON wz.Id=tj.productId AND wz.OrganizeId=tj.OrganizeId
INNER JOIN #kcxx kcxx ON tj.productId=kcxx.productId 
WHERE tj.OrganizeId=@OrganizeId
AND tj.wztjId=@Id
";
            DbParameter[] param =
            {
                new SqlParameter("@organizeid", organizeid),
                new SqlParameter("@Id", wztjId),
                new SqlParameter("@proId", productId)
            };
            return FindList<VPriceAdjustmentProfitLossEntity>(sql, param);
        }

        /// <summary>
        /// 调价执行
        /// </summary>
        /// <param name="tjInfo"></param>
        /// <returns></returns>
        public string AdjustPriceExecute(WzPriceAdjustmentEntity tjInfo)
        {
            var oldzxsj = DateTime.Now.AddDays(1);
            try
            {
                if (_wzProductDmnService.UpdateProductPrice(tjInfo.productId, tjInfo.lsj, tjInfo.tzczy, tjInfo.OrganizeId) > 0)// 第一步 修改物资零售价
                {
                    #region  第二步 修改调价执行标志
                    var executeTime = DateTime.Now;
                    tjInfo.zxbz = ((int)EnumTjZxbz.Already).ToString();
                    oldzxsj = tjInfo.zxsj;
                    tjInfo.zxsj = executeTime;
                    tjInfo.zxczy = OperatorProvider.GetCurrent().UserCode;
                    tjInfo.Modify();
                    if (_wzPriceAdjustmentRepo.Update(tjInfo) <= 0) throw new FailedException("2", "修改调价执行标志失败");
                    #endregion

                    #region 第三步 新增调价利润
                    var lrList = GetPriceAdjustmentProfitLoss(tjInfo.wztjId, tjInfo.productId, tjInfo.OrganizeId);
                    var tjlrList = new List<WzPriceAdjustmentProfitAndLossEntity>();
                    if (lrList != null && lrList.Count > 0)
                    {
                        lrList.ForEach(p =>
                        {
                            var tjlr = new WzPriceAdjustmentProfitAndLossEntity
                            {
                                dssl = p.dssl,
                                lsjtjlr = p.lsjtjlr,
                                OrganizeId = tjInfo.OrganizeId,
                                pc = "",
                                ph = "",
                                productId = p.productId,
                                tjsj = p.tjsj,
                                tjwj = p.tjwj,
                                warehouseId = p.warehouseId,
                                xlsj = p.xlsj,
                                ylsj = p.ylsj,
                                zhyz = p.zhyz,
                                zt = ((int)Enumzt.Enable).ToString()
                            };
                            tjlr.Create(true);
                            tjlrList.Add(tjlr);
                        });
                    }
                    if (tjlrList.Count <= 0) return "";
                    if (_wzPriceAdjustmentProfitAndLossRepo.Insert(tjlrList) <= 0)
                    {
                        throw new FailedException("3", "插入调价利润失败失败");
                    }
                    #endregion
                }
                else
                {
                    var wz1 = _wzProductRepo.FindEntity(p => p.Id == tjInfo.productId && p.OrganizeId == tjInfo.OrganizeId);
                    return string.Format("修改物资【{0}】零售价失败!", wz1 != null ? wz1.name : "");
                }
                return "";
            }
            catch (FailedException ex)
            {
                return CancelAdjustPriceExecute(ex, oldzxsj, tjInfo);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        /// <summary>
        /// 取消调价执行
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="oldzxsj"></param>
        /// <param name="tjInfo"></param>
        /// <returns></returns>
        private string CancelAdjustPriceExecute(FailedException ex, DateTime oldzxsj, WzPriceAdjustmentEntity tjInfo)
        {
            switch (ex.Code)
            {
                case "2":
                    _wzProductDmnService.UpdateProductPrice(tjInfo.productId, tjInfo.ylsj, tjInfo.tzczy, tjInfo.OrganizeId);
                    break;
                case "3":
                    _wzProductDmnService.UpdateProductPrice(tjInfo.productId, tjInfo.ylsj, tjInfo.tzczy, tjInfo.OrganizeId);
                    tjInfo.zxsj = oldzxsj;
                    tjInfo.zxczy = "";
                    tjInfo.zxbz = ((int)EnumTjZxbz.Not).ToString();
                    tjInfo.Modify();
                    _wzPriceAdjustmentRepo.Update(tjInfo);
                    break;
            }
            var wz2 = _wzProductRepo.FindEntity(p => p.Id == tjInfo.productId && p.OrganizeId == tjInfo.OrganizeId);
            return string.Format("物资【{0}】调价失败,{1}", wz2 != null ? wz2.name : "", ex.Msg);
        }

        /// <summary>
        /// 获取调价历史信息
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="keyWord"></param>
        /// <param name="organizeId"></param>
        /// <param name="bt">开始时间</param>
        /// <param name="et">结束时间</param>
        /// <param name="warehouseId"></param>
        /// <returns></returns>
        public IList<VPriceAdjustmentInfoEntity> GetPriceAdjustmentHistoryList(Pagination pagination, string keyWord, string organizeId, DateTime bt, DateTime et, string warehouseId)
        {
            const string sql = @"
SELECT pa.wztjId, pa.shzt, pa.zxbz, lb.name lbmc, wz.name wzmc, wz.gg, pa.lsj, pa.ylsj, pa.zhyz, pa.dwmc, gys.name supplierName, pa.zt, pa.tzwj, pa.tzsj, pa.zxsj, pa.CreatorCode
FROM dbo.wz_priceAdjustment(NOLOCK) pa
INNER JOIN dbo.wz_product(NOLOCK) wz ON wz.Id=pa.productId AND wz.OrganizeId=pa.OrganizeId AND wz.zt='1'
LEFT JOIN NewtouchHIS_Base.dbo.wz_type(NOLOCK) lb ON lb.Id=wz.typeId AND lb.zt='1' 
LEFT JOIN dbo.gys_supplier(NOLOCK) gys ON gys.Id=wz.supplierId  AND gys.zt='1'
WHERE pa.OrganizeId=@OrganizeId
AND (pa.shzt=@shzt)
AND (wz.py LIKE '%'+ @keyWord +'%' OR wz.name LIKE '%'+@keyWord+'%')
AND pa.zxbz=@zxbz
AND pa.zxsj BETWEEN @kssj AND @jssj 
AND pa.warehouseId=@warehouseId
";
            if (bt < Constants.MinDateTime) bt = Constants.MinDateTime;
            if (et < Constants.MinDateTime) et = Constants.MinDateTime;
            if (bt > et) et = bt.AddMinutes(1);

            var param = new DbParameter[]
            {
                new SqlParameter("@keyWord", keyWord??""),
                new SqlParameter("@OrganizeId", organizeId),
                new SqlParameter("@shzt", ((int)EnumTjShzt.Audited).ToString()),
                new SqlParameter("@zxbz", ((int)EnumTjZxbz.Already).ToString()),
                new SqlParameter("@kssj", bt),
                new SqlParameter("@jssj", et),
                new SqlParameter("@warehouseId", warehouseId)
            };
            return QueryWithPage<VPriceAdjustmentInfoEntity>(sql, pagination, param);
        }

        /// <summary>
        /// 获取调价损益信息
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="searchDto"></param>
        /// <returns></returns>
        public IList<VPriceAdjustmentProfitLossEntity> GetPriceAdjustmentProfitLoss(Pagination pagination, PriceAdjustmentProfitLossDTO searchDto)
        {
            var sql = new StringBuilder(@"
SELECT tjlr.TjsyId, tjlr.productId, lb.name lbmc, wz.name wzmc, wz.gg, gys.name supplierName
,ISNULL(CONVERT(INT,tjlr.dssl/tjlr.zhyz*rpu.zhyz),0) dssl,  rpu.unit dwmc, tjlr.tjwj
,ISNULL(CONVERT(NUMERIC(11,4),tjlr.ylsj/tjlr.zhyz*rpu.zhyz), 0) ylsj, ISNULL(CONVERT(NUMERIC(11,4),tjlr.xlsj/tjlr.zhyz*rpu.zhyz), 0) xlsj
,tjlr.lsjtjlr, tjlr.zt, tjlr.CreatorCode, tjlr.tjsj
FROM dbo.wz_priceAdjustmentProfitAndLoss(NOLOCK) tjlr
INNER JOIN dbo.wz_product(NOLOCK) wz ON wz.Id=tjlr.productId AND wz.OrganizeId=tjlr.OrganizeId AND wz.zt='1'
INNER JOIN dbo.rel_productWarehouse(NOLOCK) rpw ON rpw.productId=tjlr.productId AND rpw.warehouseId=tjlr.warehouseId AND rpw.OrganizeId=tjlr.OrganizeId AND rpw.zt='1'
LEFT JOIN dbo.rel_productUnit(NOLOCK) rpu ON rpu.unitId=rpw.unitId AND rpu.productId=rpw.productId AND rpu.OrganizeId=tjlr.OrganizeId AND rpu.zt='1'
LEFT JOIN NewtouchHIS_Base.dbo.wz_type(NOLOCK) lb ON lb.Id=wz.typeId AND lb.zt='1' 
LEFT JOIN dbo.gys_supplier(NOLOCK) gys ON gys.Id=wz.supplierId  AND gys.zt='1'
WHERE tjlr.OrganizeId=@OrganizeId
AND (tjlr.warehouseId=@warehouseId OR ''=@warehouseId)
AND (wz.py LIKE '%'+ @keyWord +'%' OR wz.name LIKE '%'+@keyWord+'%')
AND tjlr.tjsj BETWEEN @kssj AND @jssj  ");
            if (searchDto.lkc == "0")
            {
                sql.AppendLine("AND tjlr.dssl > 0 ");
            }
            sql.AppendLine(string.Format("AND tjlr.warehouseId IN ({0})", string.Join(",", searchDto.kfList)));
            if (searchDto.startTime < Constants.MinDateTime) searchDto.startTime = Constants.MinDateTime;
            if (searchDto.endTime < Constants.MinDateTime) searchDto.endTime = Constants.MinDateTime;
            if (searchDto.startTime > searchDto.endTime) searchDto.endTime = searchDto.startTime.AddMinutes(1);
            var param = new DbParameter[]
            {
                new SqlParameter("@OrganizeId", searchDto.organizeId),
                new SqlParameter("@warehouseId", searchDto.kfbm??""),
                new SqlParameter("@keyWord", searchDto.keyWord??""),
                new SqlParameter("@kssj", searchDto.startTime),
                new SqlParameter("@jssj", searchDto.endTime)
            };
            return QueryWithPage<VPriceAdjustmentProfitLossEntity>(sql.ToString(), pagination, param);
        }
    }
}
