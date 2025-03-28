using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using FrameworkBase.MultiOrg.Domain.DBContext.Infrastructure;
using Newtouch.Herp.Application.Interface;
using Newtouch.Herp.Domain.Entity;
using Newtouch.Herp.Domain.IRepository;
using Newtouch.Herp.Infrastructure.Enum;
using Newtouch.Herp.Infrastructure.Log;
using Newtouch.Infrastructure.EF;
using Newtouch.Tools;

namespace Newtouch.Herp.Application.Implementation
{
    /// <summary>
    /// 采购计划
    /// </summary>
    public class PurchasingPlanApp : AppBase, IPurchasingPlanApp
    {
        private readonly ICgPurchaseOrderRepo _cgPurchaseOrderRepo;
        private readonly ICgPurchaseOrderDetailRepo _cgPurchaseOrderDetailRepo;

        /// <summary>
        /// 提交采购计划
        /// </summary>
        /// <param name="pp"></param>
        /// <param name="ppmx"></param>
        /// <param name="userCode"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        public string SubmitFillPurchasingPlan(CgPurchaseOrderEntity pp, List<CgPurchaseOrderDetailEntity> ppmx, string userCode, string organizeId)
        {
            if (pp == null) return "采购主信息不能为空";
            if (ppmx == null || ppmx.Count == 0) return "采购计划明细不能为空";
            try
            {
                var oldpp = _cgPurchaseOrderRepo.SelectData(pp.cgdh, organizeId);
                if (oldpp != null && oldpp.Id > 0)
                {
                    var checkResult = CheckIsAuditRefuseOrTemporaryBill(oldpp.cgdh, organizeId, ref oldpp);
                    if (!string.IsNullOrWhiteSpace(checkResult)) return checkResult;

                    UpdatePurchasingPlan(pp, ppmx, oldpp, userCode, organizeId);
                    return "";
                }

                //新增
                if (!string.IsNullOrWhiteSpace(pp.remark) && System.Text.Encoding.Default.GetBytes(pp.remark).Length > 200)
                {
                    throw new Exception("批语长度不得超过2000个字符");
                }
                var operateTime = DateTime.Now;
                pp.OrganizeId = organizeId;
                pp.CreatorCode = userCode;
                pp.CreateTime = operateTime;
                if (_cgPurchaseOrderRepo.Insert(pp) <= 0) throw new Exception("保存采购计划主信息失败");
                using (var db = new EFDbTransaction(new DefaultDatabaseFactory()).BeginTrans())
                {
                    InsertPurchasingPlanDetail(pp.Id, ppmx, operateTime, userCode, db);
                    db.Commit();
                    return "";
                }
            }
            catch (Exception e)
            {
                LogCore.Error("SubmitFillPurchasingPlan error", e, string.Format("pp:{0}; ppmx:{1}", pp.ToJson(), ppmx.ToJson()));
                _cgPurchaseOrderRepo.Delete(pp);
                return "保存采购计划失败";
            }
        }

        /// <summary>
        /// 修改采购计划
        /// </summary>
        /// <param name="pp"></param>
        /// <param name="ppmx"></param>
        /// <param name="oldpp"></param>
        /// <param name="userCode"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        private void UpdatePurchasingPlan(CgPurchaseOrderEntity pp, List<CgPurchaseOrderDetailEntity> ppmx, CgPurchaseOrderEntity oldpp, string userCode, string organizeId)
        {
            using (var db = new EFDbTransaction(new DefaultDatabaseFactory()).BeginTrans())
            {
                if (!string.IsNullOrWhiteSpace(pp.remark) && System.Text.Encoding.Default.GetBytes(pp.remark).Length > 200)
                {
                    throw new Exception("批语长度不得超过2000个字符");
                }

                var operateTime = DateTime.Now;
                oldpp.auditState = pp.auditState;
                oldpp.LastModifierCode = userCode;
                oldpp.LastModifyTime = operateTime;
                oldpp.remark = pp.remark;
                oldpp.rkbmCode = pp.rkbmCode;
                db.Update(oldpp);
                const string sql = @"DELETE FROM dbo.cg_purchaseOrderDetail WHERE purchaseId=@purchaseId";
                db.ExecuteSqlCommand(sql, new SqlParameter("@purchaseId", oldpp.Id));
                InsertPurchasingPlanDetail(oldpp.Id, ppmx, operateTime, userCode, db);

                db.Commit();
            }
        }

        /// <summary>
        /// 保存采购计划明细
        /// </summary>
        /// <param name="purchaseId"></param>
        /// <param name="ppmx"></param>
        /// <param name="operateTime"></param>
        /// <param name="userCode"></param>
        /// <param name="db"></param>
        private void InsertPurchasingPlanDetail(long purchaseId, List<CgPurchaseOrderDetailEntity> ppmx, DateTime operateTime, string userCode, EFDbTransaction db)
        {
            ppmx.ForEach(p =>
            {
                p.purchaseId = purchaseId;
                p.zt = ((int)Enumzt.Enable).ToString();
                p.CreatorCode = userCode;
                p.CreateTime = operateTime;
                db.Insert(p);
            });
        }

        /// <summary>
        /// 作废采购计划
        /// </summary>
        /// <param name="cgdh"></param>
        /// <param name="organizeId"></param>
        /// <param name="userCode"></param>
        /// <returns></returns>
        public string CancelPurchasingPlan(string cgdh, string organizeId, string userCode)
        {
            var entity = new CgPurchaseOrderEntity();
            var checkResult = CheckIsTemporaryBill(cgdh, organizeId, ref entity);
            if (!string.IsNullOrWhiteSpace(checkResult)) return checkResult;
            return _cgPurchaseOrderRepo.CancelPurchasingPlan(cgdh, organizeId, userCode) > 0 ? "" : "作废失败";
        }

        /// <summary>
        /// 检查是否是暂存单
        /// </summary>
        /// <param name="cgdh"></param>
        /// <param name="organizeId"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public string CheckIsTemporaryBill(string cgdh, string organizeId, ref CgPurchaseOrderEntity entity)
        {
            if (entity == null || entity.Id <= 0) entity = _cgPurchaseOrderRepo.SelectData(cgdh, organizeId);
            if (entity == null || entity.Id <= 0) return "未找到符合要求的采购计划";
            return entity.auditState != (int)EnumAuditState.Temporary ? "本操作只针对暂存的采购计划单" : "";
        }

        /// <summary>
        /// 检查是否是审核不通过
        /// </summary>
        /// <param name="cgdh"></param>
        /// <param name="organizeId"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public string CheckIsAuditRefuseBill(string cgdh, string organizeId, ref CgPurchaseOrderEntity entity)
        {
            if (entity == null || entity.Id <= 0) entity = _cgPurchaseOrderRepo.SelectData(cgdh, organizeId);
            if (entity == null || entity.Id <= 0) return "未找到符合要求的采购计划";
            return entity.auditState != (int)EnumAuditState.Refuse ? "本操作只针对审核不通过的采购计划单" : "";
        }

        /// <summary>
        /// 检查是否是审核不通过或暂存采购计划单
        /// </summary>
        /// <param name="cgdh"></param>
        /// <param name="organizeId"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public string CheckIsAuditRefuseOrTemporaryBill(string cgdh, string organizeId, ref CgPurchaseOrderEntity entity)
        {
            if (entity == null || entity.Id <= 0) entity = _cgPurchaseOrderRepo.SelectData(cgdh, organizeId);
            if (entity == null || entity.Id <= 0) return "未找到符合要求的采购计划";
            return entity.auditState != (int)EnumAuditState.Temporary && entity.auditState != (int)EnumAuditState.Refuse ?
                "该操作只针对暂存采购计划单或审核不通过的采购计划单" : "";
        }

        /// <summary>
        /// 将暂存采购计划提交审核
        /// </summary>
        /// <param name="cgdh"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        public string SubmitPurchasingPlan(string cgdh, string organizeId)
        {
            var entity = new CgPurchaseOrderEntity();
            var checkResult = CheckIsTemporaryBill(cgdh, organizeId, ref entity);
            if (!string.IsNullOrWhiteSpace(checkResult)) return checkResult;
            entity.auditState = (int)EnumAuditState.Waiting;
            entity.Modify();
            return _cgPurchaseOrderRepo.Update(entity) > 0 ? "" : "提交审核失败";
        }

        /// <summary>
        /// 审核采购计划
        /// </summary>
        /// <param name="cgdh"></param>
        /// <param name="auditState"></param>
        /// <param name="remarks">批语</param>
        /// <param name="organizeId"></param>
        /// <param name="userCode"></param>
        /// <returns></returns>
        public string SubmitAuditPurchasingPlan(string cgdh, int auditState, string remarks, string organizeId, string userCode)
        {
            if (string.IsNullOrWhiteSpace(cgdh)) return "采购计划单号不能为空";
            var entity = _cgPurchaseOrderRepo.SelectData(cgdh, organizeId);
            if (entity == null || entity.Id <= 0) return "根据采购单号未找到有效的采购计划单";
            if (entity.auditState != (int)EnumAuditState.Waiting) return "审核操作只针对待审核采购计划单";
            switch (auditState)
            {
                case (int)EnumAuditState.Adopt:
                    entity.auditState = (int)EnumAuditState.Adopt;
                    break;
                case (int)EnumAuditState.Refuse:
                    entity.auditState = (int)EnumAuditState.Refuse;
                    break;
                default:
                    return "该操作无效";
            }
            if (!string.IsNullOrWhiteSpace(remarks) && System.Text.Encoding.Default.GetBytes(remarks).Length > 200) return "批语长度不得超过2000个字符";
            entity.remark = remarks;
            entity.LastModifierCode = userCode;
            entity.LastModifyTime = DateTime.Now;
            return _cgPurchaseOrderRepo.Update(entity) > 0 ? "" : "审核失败";
        }
    }
}