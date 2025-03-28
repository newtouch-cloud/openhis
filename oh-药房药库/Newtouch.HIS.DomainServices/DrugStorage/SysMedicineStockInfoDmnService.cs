using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using FrameworkBase.MultiOrg.Domain.DBContext.Infrastructure;
using FrameworkBase.MultiOrg.Infrastructure;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.Entity.V;
using Newtouch.HIS.Domain.IDomainServices.DrugStorage;
using Newtouch.HIS.Repository;
using Newtouch.Infrastructure;
using Newtouch.Infrastructure.TSQL;

namespace Newtouch.HIS.DomainServices.DrugStorage
{
    /// <summary>
    /// 库存操作
    /// </summary>
    public class SysMedicineStockInfoDmnService : DmnServiceBase, ISysMedicineStockInfoDmnService
    {
        public SysMedicineStockInfoDmnService(IDefaultDatabaseFactory databaseFactory, bool needIoc = true) : base(databaseFactory, needIoc)
        {
        }

        /// <summary>
        /// 外部入库 扣库存(作废)
        /// </summary>
        /// <param name="sl">最小单位数量</param>
        /// <param name="pc"></param>
        /// <param name="ph"></param>
        /// <param name="ypdm"></param>
        /// <param name="organizeId"></param>
        /// <param name="yfbmCode"></param>
        /// <param name="userCode"></param>
        /// <returns></returns>
        public string WbrkKkc(int sl, string pc, string ph, string ypdm, string organizeId, string yfbmCode, string userCode)
        {
            var param = new DbParameter[]
            {
                new SqlParameter("@pc", pc),
                new SqlParameter("@ph", ph),
                new SqlParameter("@OrganizeId", organizeId),
                new SqlParameter("@yfbmCode", yfbmCode),
                new SqlParameter("@ypdm", ypdm),
                new SqlParameter("@zt", ((int) EnumKCZT.Enabled).ToString()),
                new SqlParameter("@sl", sl),
                new SqlParameter("@userCode", userCode)
            };
            return FindList<string>(TSqlStock.cancel_subtract_kcsl, param.ToArray()).FirstOrDefault();
        }

        /// <summary>
        /// 冻结库存，并返回被冻结的批次信息
        /// </summary>
        /// <param name="kcsl"></param>
        /// <param name="ypdm"></param>
        /// <param name="yfbmCode"></param>
        /// <param name="organizeId"></param>
        /// <param name="userCode"></param>
        /// <returns></returns>
        public string FrozenStork(int kcsl, string ypdm, string yfbmCode, string organizeId, string userCode, out List<FrozenBatchesDetailVEntity> frozenInfo)
        {
            var param = new DbParameter[] {
                new SqlParameter("@ypdm",ypdm),
                new SqlParameter("@djsl",kcsl),
                new SqlParameter("@yfbmCode",yfbmCode),
                new SqlParameter("@OrganizeId",organizeId),
                new SqlParameter("@userCode",userCode),
            };
            frozenInfo = FindList<FrozenBatchesDetailVEntity>(TSqlStock.frozen_stork_and_return_frozenInfo, param);
            if (frozenInfo == null || frozenInfo.Count <= 0) return "冻结库存失败";
            if (frozenInfo.Count == 1 && !string.IsNullOrWhiteSpace(frozenInfo[0].errorMsg))
            {
                return frozenInfo[0].errorMsg;
            }
            return "";
        }

        /// <summary>
        /// 提交报损报益
        /// </summary>
        /// <param name="syxx"></param>
        /// <returns></returns>
        public string SubmitReportLossAndProfit(SysMedicineProfitLossEntity syxx)
        {
            try
            {
                using (var db = new EFDbTransaction(_databaseFactory).BeginTrans())
                {
                    var kcxxRepo = new SysMedicineStockInfoRepo(new DefaultDatabaseFactory());
                    var kcxx = kcxxRepo.SelectData(syxx.Ypdm, syxx.Ph, syxx.pc, syxx.yfbmCode, syxx.OrganizeId);
                    var curKcsl = kcxx.Sum(p => p.kcsl);
                    if (syxx.Sysl < 0)//报损  盘库存是否充足
                    {
                        if (kcxx.Sum(p => p.kcsl) + syxx.Sysl < 0)
                            return string.Format("代码为{0}、批号{1}、批次{2}的药品库存不足", syxx.Ypdm, syxx.Ph, syxx.pc);
                    }

                    kcxxRepo.UpdateKcslWithTrans(syxx.Sysl, syxx.pc, syxx.Ph, syxx.Ypdm, syxx.yfbmCode, syxx.OrganizeId,
                        syxx.CreatorCode, db);

                    syxx.Sykc = curKcsl + syxx.Sysl;
                    db.Insert(syxx);

                    db.Commit();
                    return "";
                }
            }
            catch (Exception e)
            {
                return e.Message + (e.InnerException == null ? "" : e.InnerException.Message);
            }
        }
    }
}
