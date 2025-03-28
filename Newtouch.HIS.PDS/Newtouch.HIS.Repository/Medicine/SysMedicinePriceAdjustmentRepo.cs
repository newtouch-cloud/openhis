using System;
using Newtouch.Common.Operator;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Infrastructure;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Repository;
using Newtouch.Core.Common.Exceptions;


namespace Newtouch.HIS.Repository
{
    /// <summary>
    /// 药品调价 xt_yptj
    /// </summary>
    public class SysMedicinePriceAdjustmentRepo : RepositoryBase<SysMedicinePriceAdjustmentEntity>, ISysMedicinePriceAdjustmentRepo
    {
        /// <summary>
        /// 构造函数初始化
        /// </summary>
        /// <param name="databaseFactory"></param>
        public SysMedicinePriceAdjustmentRepo(IDefaultDatabaseFactory databaseFactory) : base(databaseFactory)
        {
        }

        /// <summary>
        /// 获取一个调价的entity 未审核 未执行
        /// </summary>
        /// <param name="ypCode"></param>
        /// <returns></returns>
        public SysMedicinePriceAdjustmentEntity GetMedicinePriceAdjustmentNotApproveEntity(string ypCode)
        {
            var organizeId = OperatorProvider.GetCurrent().OrganizeId;
            return IQueryable().FirstOrDefault(a => a.ypCode == ypCode && a.zxbz == "0" && (a.shzt == "0" || a.shzt == "1") && a.zxsj > DateTime.Now && a.OrganizeId == organizeId);   //未审核 未执行
        }

        /// <summary>
        /// 获取一个调价的entity 已审核 未执行
        /// </summary>
        /// <param name="ypCode"></param>
        /// <returns></returns>
        public SysMedicinePriceAdjustmentEntity GetMedicinePriceAdjustmentNotExecteEntity(string ypCode)
        {
            var organizeId = OperatorProvider.GetCurrent().OrganizeId;
            return IQueryable().FirstOrDefault(a => a.ypCode == ypCode && a.zxbz == "0" && a.shzt == "1" && a.OrganizeId == organizeId);   //已审核 未执行
        }

        /// <summary>
        /// 添加药品调价记录(申请)
        /// </summary>
        /// <param name="entity"></param>
        public void AddPriceAdjustmentRecord(SysMedicinePriceAdjustmentEntity entity)
        {
            Insert(entity);
        }

        /// <summary>
        /// 检查是否有未处理的数据
        /// </summary>
        /// <param name="ypCodeList"></param>
        public string CheckStatus(ArrayList ypCodeList)
        {
            if (ypCodeList == null || ypCodeList.Count == 0)
            {
                return "请求数据为空,请联系管理员";
            }
            foreach (var item in ypCodeList)
            {
                var entity = IQueryable().OrderByDescending(o=>o.CreateTime).FirstOrDefault(a => a.ypCode == item.ToString() && a.zxsj > DateTime.Now);
                if (entity == null)
                {
                    return string.Format(@"查无药品代码为{0}的药品！", item);
                }
                if (entity.shzt.Equals("0") || entity.shzt.Equals("2") || entity.shzt.Equals("3"))  //未审核 拒绝 撤销
                {
                    return entity.ypCode + " 状态处于未审核或拒绝或撤销的状态，不可执行";
                }
                if ("1".Equals(entity.zxbz))   //执行
                {
                    return entity.ypCode + " 已执行过的数据，不可重复执行";
                }
                if (entity.zxsj <= DateTime.Now)
                {
                    return entity.ypCode + " 已过执行日期";
                }
            }
            return string.Empty;
        }

        ///  <summary>
        ///  药品调价审核
        ///  </summary>
        ///  --shzt:0:未审核 1:已审核 2:已拒绝 3.已撤销
        ///  --zxbz:0:未执行 1:已执行
        /// <param name="ypCodeList"></param>
        /// <param name="operationType"></param>
        public string MedicinePriceAdjustment(ArrayList ypCodeList, int operationType)
        {
            var expireApply = new List<string>();
            using (var db = new EFDbTransaction(_databaseFactory).BeginTrans())
            {
                foreach (var item in ypCodeList)
                {
                    var entity = db.IQueryable<SysMedicinePriceAdjustmentEntity>().FirstOrDefault(a => a.ypCode == item.ToString() && a.zxbz == "0" && a.shzt == "0");
                    if (entity == null) continue;
                    if (entity.zxsj < DateTime.Now)
                    {
                        expireApply.Add(entity.ypCode);
                        continue;
                    }
                    entity.zxbz = ((int)EnumPriceAdjustZXStatus.Not).ToString();
                    switch (operationType)
                    {
                        case (int)EnumPriceAdjustOperationType.Approval:   //审核
                            entity.shzt = operationType.ToString();
                            entity.shczy = OperatorProvider.GetCurrent().UserCode;
                            break;
                        case (int)EnumPriceAdjustOperationType.Refuse:   //拒绝
                            entity.shzt = operationType.ToString();
                            entity.shczy = OperatorProvider.GetCurrent().UserCode;
                            break;
                        case (int)EnumPriceAdjustOperationType.Withdraw:  //撤销
                            entity.shzt = operationType.ToString();
                            entity.pfj = entity.ypfj;
                            entity.lsj = entity.ylsj;
                            break;
                    }
                    entity.Modify();
                    db.Update(entity);
                }
                db.Commit();
            }
            return expireApply.Count > 0 ? string.Format("药品代码为{0}的药品操作失败，已过最晚执行时间。", string.Join(",", expireApply)) : "操作成功";
        }
    }
}
