using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using FrameworkBase.MultiOrg.Domain.DBContext.Infrastructure;
using Newtouch.Core.Common.Exceptions;
using Newtouch.Herp.Domain.DTO.InputDto;
using Newtouch.Herp.Domain.DTO.OutputDto;
using Newtouch.Herp.Domain.IDomainServices;
using Newtouch.Herp.Domain.IRepository;
using Newtouch.Herp.Infrastructure.Enum;
using Newtouch.Infrastructure.EF;

namespace Newtouch.Herp.Application.Implementation
{
    /// <summary>
    /// 科室申领出库
    /// </summary>
    public class DeptApplyOutStockProcess : ProcessorFun<DjInfDTO>
    {
        private readonly IKfCrkdjRepo _crkdjRepo;
        private readonly IKfCrkmxRepo _crkmxRepo;
        private readonly IKfKcxxDmnService iKfKcxxDmnService;
        private readonly IKfApplyOrderRepo _kfApplyOrderRepo;
        private readonly IKfApplyOrderDetailRepo _kfApplyOrderDetailRepo;

        public DeptApplyOutStockProcess(DjInfDTO request) : base(request)
        {
        }

        /// <summary>
        /// 效验
        /// </summary>
        /// <returns></returns>
        protected override ActResult Validata()
        {
            if (Request == null) throw new FailedException("单据内容不能为空");
            if (Request.crkdj == null) throw new FailedException("单据主表信息不能为空");
            if (Request.crkdjmx == null || Request.crkdjmx.Count == 0) throw new FailedException("单据明细信息不能为空");
            if (Request.applyBillDetail == null || Request.applyBillDetail.Count == 0) throw new FailedException("申领信息不能为空");
            return base.Validata();
        }

        /// <summary>
        /// 执行
        /// </summary>
        /// <param name="actResult"></param>
        protected override void Action(ActResult actResult)
        {
            try
            {
                if (_crkdjRepo.Insert(Request.crkdj) <= 0) throw new FailedException("生成出入库单失败");
                using (var db = new EFDbTransaction(new DefaultDatabaseFactory()).BeginTrans())
                {
                    #region 保存出入库单
                    Request.crkdjmx.ForEach(p =>
                    {
                        p.crkId = Request.crkdj.Id;
                        db.Insert(p);
                    });
                    #endregion

                    #region 库存变更
                    var updateKcslParam = AssembleUpdateKcslDTO();
                    var subtractKcslResult = iKfKcxxDmnService.JustSubtractKcslNoTrans(updateKcslParam, db);
                    if (!string.IsNullOrWhiteSpace(subtractKcslResult)) throw new Exception(subtractKcslResult);
                    #endregion

                    #region 变更申领单状态
                    foreach (var sldh in Request.applyBillDetail.Select(p => p.sldh).Distinct())
                    {
                        var abd = Request.applyBillDetail.FindAll(p => p.sldh == sldh);
                        foreach (var mx in abd)
                        {
                            const string updateYfslSql = @"
UPDATE dbo.kf_applyOrderDetail 
SET yfsl=ISNULL(yfsl,0)+@sl, LastModifyTime=GETDATE(), LastModifierCode=@userCode 
WHERE Id=@sldmxId AND zt='1'
";
                            var param = new DbParameter[]
                            {
                                new SqlParameter("@sldmxId", mx.sldmxId),
                                new SqlParameter("@sl", mx.xfsl),
                                new SqlParameter("@userCode", Request.crkdj.CreatorCode)
                            };
                            db.ExecuteSqlCommand(updateYfslSql, param);
                        }

                        var appBillDetail = _kfApplyOrderDetailRepo.SelectData(sldh, Request.crkdj.OrganizeId);
                        if (appBillDetail == null || appBillDetail.Count == 0) throw new Exception(string.Format("申领单【{0}】未找到有效的申领明细", sldh));

                        var applyBill = _kfApplyOrderRepo.SelectData(sldh, Request.crkdj.OrganizeId);
                        if (applyBill == null) throw new Exception(string.Format("申领单号【{0}】无效", sldh));

                        applyBill.applyProcess = (int)(appBillDetail.Exists(p => p.sl > p.yfsl)
                            ? EnumApplyProcess.PartialCompletion
                            : EnumApplyProcess.Completion);
                        applyBill.LastModifierCode = Request.crkdj.CreatorCode;
                        applyBill.LastModifyTime = Request.crkdj.CreateTime;
                        db.Update(applyBill);
                    }
                    #endregion

                    db.Commit();
                }
            }
            catch (Exception e)
            {
                _crkdjRepo.Delete(Request.crkdj);
                actResult.IsSucceed = false;
                actResult.ResultMsg = e.Message;
            }
        }


        /// <summary>
        /// 组装修改库存DTO
        /// </summary>
        /// <returns></returns>
        private List<UpdateKcslDTO> AssembleUpdateKcslDTO()
        {
            return Request.crkdjmx.Select(p => new UpdateKcslDTO
            {
                productId = p.productId,
                pc = p.pc,
                ph = p.ph,
                warehouseId = Request.crkdj.ckbm,
                sl = p.sl * p.zhyz,
                organizeId = Request.crkdj.OrganizeId,
                userCode = Request.crkdj.CreatorCode
            }).ToList();
        }
    }
}