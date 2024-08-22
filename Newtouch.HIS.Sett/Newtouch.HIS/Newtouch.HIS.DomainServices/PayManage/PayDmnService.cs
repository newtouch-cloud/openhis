using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrameworkBase.MultiOrg.DmnService;
using FrameworkBase.MultiOrg.Infrastructure;
using Newtouch.Core.Common;
using Newtouch.HIS.Domain.DTO.PayDto;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IDomainServices.PayManage;


namespace Newtouch.HIS.DomainServices.PayManage
{
    public class PayDmnService: DmnServiceBase, IPayDmnService
    {

        public PayDmnService(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }
        /// <summary>
        /// 支付记录
        /// </summary>
        /// <param name="ksrq"></param>
        /// <param name="jsrq"></param>
        /// <param name="payType"></param>
        /// <param name="payStatus"></param>
        /// <param name="keywords"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public IList<MicroPayTradeInfoDTO> TradePayLsit(Pagination pagination, string ksrq,string jsrq,int? payType,int? payStatus,string keywords,string orgId)
        {
            IList<SqlParameter> para = new List<SqlParameter>();
            string sql = @"select PayType,OutTradeNo,TradeNo,OrderDate, PayAmount,PayStatus, PayMemo,PayAccount,TradeDesc, OutTradeTime,
                sum(RefundAmount) RefundAmount,fph,xm,kh,patno
                from (
               	select a.PayType,a.OutTradeNo,a.TradeNo,a.OrderDate,a.Amount PayAmount,PayStatus,a.Memo PayMemo,
                a.PayAccount,a.TradeDesc,a.CreateTime OutTradeTime,b.id RefundId,
               b.Amount RefundAmount,'' fph,isnull(d.xm,f.xm)xm,isnull(d.kh,f.kh)kh,isnull(d.mzh,f.zyh) patno
                from order_payinfo a with(nolock)
                left join order_refundinfo b with(nolock) on a.outtradeno=b.outtradeno and b.zt=1  and b.refundstatus=1
                left join mz_js c with(nolock) on a.outtradeno=c.outtradeno and c.zt=1
                left join mz_gh d with(nolock) on c.organizeid=d.organizeid and c.ghnm=d.ghnm and d.zt=1
				left join zy_js e with(nolock) on a.outtradeno=e.outtradeno and e.zt=1
				left join zy_brjbxx f with(nolock) on e.zyh=f.zyh and e.organizeid=f.organizeid and f.zt=1                
                where a.zt=1 and OrderDate between @ksrq and @jsrq ";

            para.Add(new SqlParameter("@ksrq", Convert.ToDateTime(ksrq)));
            para.Add(new SqlParameter("@jsrq", Convert.ToDateTime(jsrq).AddDays(1).AddSeconds(-1)));
            
            if (payType != null)
            {
                sql += " and PayType=@payType ";
                para.Add(new SqlParameter("@payType", payType));
            }

            if (payStatus != null)
            {
                sql += " and payStatus=@payStatus ";
                para.Add(new SqlParameter("@payStatus", payStatus));
            }

            if (!string.IsNullOrWhiteSpace(keywords))
            {
                sql += " and (d.xm=@keywords or d.kh=@keywords)";
                para.Add(new SqlParameter("@keywords", keywords));
            }

            if (!string.IsNullOrWhiteSpace(orgId))
            {
                sql += " and a.organizeid=@orgId ";
                para.Add(new SqlParameter("@orgId", orgId));
            }

            sql += @"
               group by a.PayType,a.OutTradeNo,a.TradeNo,a.OrderDate,a.Amount,PayStatus,a.Memo ,
                a.PayAccount,a.TradeDesc,a.CreateTime,d.xm,d.kh ,d.mzh,b.Amount,b.id,f.xm,f.kh ,f.zyh
                ) a
            group by PayType,OutTradeNo,TradeNo,OrderDate, PayAmount,PayStatus, PayMemo,PayAccount,TradeDesc, OutTradeTime,
            fph,xm,kh,patno 
            ";

            return this.QueryWithPage<MicroPayTradeInfoDTO>(sql,pagination,para.ToArray()).ToList();
        }

        /// <summary>
        /// 查询具体订单详情
        /// </summary>
        /// <param name="OutTradeNo"></param>
        /// <param name="TradeNo"></param>
        /// <returns></returns>
        public MicroPayTradeInfoDTO TradePayInfobyNo(string OutTradeNo,string TradeNo)
        {
            IList<SqlParameter> para = new List<SqlParameter>();
            string sql = @"select PayType,OutTradeNo,TradeNo,OrderDate, PayAmount,PayStatus, PayMemo,PayAccount,TradeDesc, OutTradeTime,
                isnull(sum(RefundAmount),0.00) RefundAmount,fph,xm,kh,patno
                from (
               	select a.PayType,a.OutTradeNo,a.TradeNo,a.OrderDate,a.Amount PayAmount,PayStatus,a.Memo PayMemo,
                a.PayAccount,a.TradeDesc,a.CreateTime OutTradeTime,b.id RefundId,
               b.Amount RefundAmount,'' fph,isnull(d.xm,f.xm)xm,isnull(d.kh,f.kh)kh,isnull(d.mzh,f.zyh) patno
                from order_payinfo a with(nolock)
                left join order_refundinfo b with(nolock) on a.outtradeno=b.outtradeno and b.zt=1  and b.refundstatus=1
                left join mz_js c with(nolock) on a.outtradeno=c.outtradeno and c.zt=1
                left join mz_gh d with(nolock) on c.organizeid=d.organizeid and c.ghnm=d.ghnm and d.zt=1
				left join zy_js e with(nolock) on a.outtradeno=e.outtradeno and e.zt=1
				left join zy_brjbxx f with(nolock) on e.zyh=f.zyh and e.organizeid=f.organizeid and f.zt=1                
                where a.zt=1 ";

            if (!string.IsNullOrWhiteSpace(OutTradeNo))
            {
                sql += " and a.OutTradeNo=@OutTradeNo ";
                para.Add(new SqlParameter("@OutTradeNo", OutTradeNo));
            }

            if (!string.IsNullOrWhiteSpace(TradeNo))
            {
                sql += " and a.TradeNo=@TradeNo ";
                para.Add(new SqlParameter("@TradeNo", TradeNo));
            }
            

            sql += @"
               group by a.PayType,a.OutTradeNo,a.TradeNo,a.OrderDate,a.Amount,PayStatus,a.Memo ,
                a.PayAccount,a.TradeDesc,a.CreateTime,d.xm,d.kh ,d.mzh,b.Amount,b.id,f.xm,f.kh ,f.zyh
                ) a
            group by PayType,OutTradeNo,TradeNo,OrderDate, PayAmount,PayStatus, PayMemo,PayAccount,TradeDesc, OutTradeTime,
            fph,xm,kh,patno ";

            return this.FirstOrDefault<MicroPayTradeInfoDTO>(sql, para.ToArray());
        }


        /// <summary>
        /// 退款历史
        /// </summary>
        /// <param name="OutTradeNo"></param>
        /// <param name="TradeNo"></param>
        /// <returns></returns>
        public IList<OrderRefundInfoEntity> TradeRefundList(string OutTradeNo,string TradeNo)
        {
            IList<SqlParameter> para = new List<SqlParameter>();
            string sql = @"SELECT [Id]
                      ,[OutTradeNo]
                      ,[TradeNo]
                      ,[Amount]
                      ,[RefundStatus]
                      ,[Memo]
                      ,[CreatorCode]
                      ,[CreateTime]
                      ,[LastModifyTime]
                      ,[LastModifierCode]
                      ,[zt]
                      ,[RefundReason]
                      ,[RefundDate]
                      ,[OutRequestNo]
                  FROM [dbo].[Order_RefundInfo]
                where zt=1";
            if (!string.IsNullOrWhiteSpace(OutTradeNo))
            {
                sql += " and OutTradeNo=@OutTradeNo ";
                para.Add(new SqlParameter("@OutTradeNo", OutTradeNo));
            }
            if (!string.IsNullOrWhiteSpace(TradeNo))
            {
                sql += " and TradeNo=@TradeNo ";
                para.Add(new SqlParameter("@TradeNo", TradeNo));
            }

            sql += " order by CreateTime ";
            return this.FindList<OrderRefundInfoEntity>(sql, para.ToArray()).ToList();
        }

        /// <summary>
        /// 交易详细信息
        /// </summary>
        /// <param name="ksrq"></param>
        /// <param name="jsrq"></param>
        /// <param name="payType"></param>
        /// <param name="payStatus"></param>
        /// <param name="keywords"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        public IList<MicroPayTradeInfoDTO> MicroPayTradeQuery(string ksrq, string jsrq, int? payType, int? payStatus, string keywords, string orgId)
        {
            IList<SqlParameter> para = new List<SqlParameter>();
            string sql = @"select a.PayType,a.OutTradeNo,a.TradeNo,a.OrderDate,a.Amount PayAmount,PayStatus,a.Memo PayMemo,
                a.PayAccount,a.TradeDesc,a.CreateTime OutTradeTime,
                b.Amount RefundAmount,RefundDate,RefundReason,RefundStatus,b.Memo RefundMemo,b.CreateTime OutRefundTime,c.fph,
                d.xm,d.kh,b.OutRequestNo
                from order_payinfo a with(nolock)
                left join order_refundinfo b with(nolock) on a.outtradeno=b.outtradeno
                left join mz_js c with(nolock) on a.outtradeno=c.outtradeno
                left join mz_gh d with(nolock) on c.organizeid=d.organizeid and c.ghnm=d.ghnm
                where a.zt=1 and OrderDate between @ksrq and @jsrq ";

            para.Add(new SqlParameter("@ksrq", Convert.ToDateTime(ksrq)));
            para.Add(new SqlParameter("@jsrq", Convert.ToDateTime(jsrq).AddDays(1).AddSeconds(-1)));

            if (payType != null)
            {
                sql += " and PayType=@payType ";
                para.Add(new SqlParameter("@payType", payType));
            }

            if (payStatus != null)
            {
                sql += " and payStatus=@payStatus ";
                para.Add(new SqlParameter("@payStatus", payStatus));
            }

            if (!string.IsNullOrWhiteSpace(keywords))
            {
                sql += " and (d.xm=@keywords or d.kh=@keywords)";
                para.Add(new SqlParameter("@keywords", keywords));
            }

            if (!string.IsNullOrWhiteSpace(orgId))
            {
                sql += " and c.organizeid=@orgId ";
                para.Add(new SqlParameter("@orgId", orgId));
            }

            return this.FindList<MicroPayTradeInfoDTO>(sql, para.ToArray()).ToList();
        }

    }
}
