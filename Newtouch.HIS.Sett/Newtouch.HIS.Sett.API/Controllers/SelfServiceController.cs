using Autofac;
using Newtouch.HIS.API.Common;
using Newtouch.HIS.Domain.DTO;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.HIS.Domain.IDomainServices.OutpatientManage;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.HIS.Domain.IRepository.OutpatientManage;
using Newtouch.HIS.Sett.Request.SelfService;
using System;
using System.Net.Http;
using System.Web.Http;

namespace Newtouch.HIS.Sett.API.Controllers
{
    /// <summary>
    /// 自助机相关(弃用)
    /// </summary>
    [RoutePrefix("api/SelfService")]
	public class SelfServiceController : ApiControllerBase<SelfServiceController>
	{
		//private readonly IOutpatientRegApp _outpatientRegApp;
		private readonly IOutpatientRegistScheduleRepo _outpatientRegistScheduleRepo;
		private readonly IOutPatientSettleDmnService _outPatientSettleDmnService;
		private readonly ISysCardRepo _sysCardRepo;
		private readonly IOutPatientDmnService _outPatientDmnService;
		private readonly ISelfServiceDmnService _selfServiceDmnService;
        public SelfServiceController(IComponentContext com)
            : base(com)
        {
		}
		/// <summary>
		/// 确认挂号
		/// </summary>
		/// <param name="oData"></param>
		/// <returns></returns>
		[HttpPost]
		[Route("OutpatientRegistration")]
		public HttpResponseMessage OutpatientRegistration(OutpatientRegistrationReqDTO oData)
		{
			DefaultResponse resp=new DefaultResponse();
            try
			{
				OutpatientSettFeeRelatedDTO feeRelated = new OutpatientSettFeeRelatedDTO();
				if (oData == null || string.IsNullOrWhiteSpace(oData.kh) || string.IsNullOrWhiteSpace(oData.mjzbz) || string.IsNullOrWhiteSpace(oData.ks) || string.IsNullOrWhiteSpace(oData.ksmc)
				    || string.IsNullOrWhiteSpace(oData.brxz) || (feeRelated != null && feeRelated.zje < 0))
				{
					resp.msg = "接收到的传参对象为null或请求参数不完整，请检查接口传参！";
					resp.code = ResponseResultCode.FAIL;
				}
				else
				{
					var cardEntity = _sysCardRepo.GetCardEntity(oData.kh, oData.OrganizeId);
                    OutpatientRegistScheduleEntity entity = _outpatientRegistScheduleRepo.FindEntity(p => p.ghpbId.ToString() == oData.ghpbId);
					if (entity == null)
					{
						resp.msg = "该排班无效，请重试！";
						resp.code = ResponseResultCode.FAIL;
					}
					else if (cardEntity==null || string.IsNullOrEmpty(cardEntity.patid.ToString()))
					{
						resp.msg = "当前卡号【"+ oData.kh + "】在HIS中不存在！";
						resp.code = ResponseResultCode.FAIL;
					}
					else
					{
						object newJszbInfo;
						short? qzjzxh = null;
						string qzmzh = null;
						if ((qzjzxh ?? 0) <= 0 || string.IsNullOrWhiteSpace(qzmzh))
						{
							var mzhjzxh = _outPatientSettleDmnService.GetMzhJzxh(cardEntity.patid, oData.ghpbId.ToString(), oData.ks, "", oData.OrganizeId, "zzjadmin");
							qzjzxh = mzhjzxh.Item1;
							qzmzh = mzhjzxh.Item2;
						}
						//保存
						_outPatientSettleDmnService.Save(oData.OrganizeId, cardEntity.patid, oData.kh, "0", oData.mjzbz,
							oData.ks, "", oData.ksmc, "", entity.ghlx, entity.zlxm, null, null, false, false, (int)qzjzxh.Value, entity.ghpbId, feeRelated, oData.brxz, null, qzmzh, "0", null,null,null,null,null, out newJszbInfo);
						resp.data = new { mzh = qzmzh };
						resp.msg = "";
						resp.code = ResponseResultCode.SUCCESS;
					}

				}
			}
			catch (Exception ex)
			{
				resp.msg = ex.Message;
				resp.code = ResponseResultCode.FAIL;
			};

			return base.CreateResponse(resp);
		}
		/// <summary>
		/// 获取科室排班(弃用)
		/// </summary>
		/// <param name="req"></param>
		/// <returns></returns>
		[HttpPost]
		[Route("queryRegistDeptMark")]
		public HttpResponseMessage queryRegistDeptMark(queryRegistDeptMarkReqDTO req)
		{
			DefaultResponse resp = new DefaultResponse();
			try
			{
				if (req==null ||string.IsNullOrEmpty(req.VisitDate) || string.IsNullOrEmpty(req.OrganizeId))
				{
					resp.msg = "接收到的传参对象为null或请求参数不完整，请检查接口传参！";
					resp.code = ResponseResultCode.FAIL;
				}
				else
				{
					//DateTime pbrq = DateTime.Parse(req.VisitDate);
					//var pbList= _outPatientDmnService.GetZzjRegScheduleList(pbrq, req.OrganizeId);
					//if (pbList==null || pbList.Count<1)
					//{
					//	resp.msg = "日期："+ pbrq.ToString()+"暂无排班！";
					//	resp.code = ResponseResultCode.FAIL;
					//}
					//else
					//{
					//	resp.data = pbList;
					//	resp.msg = "";
					//	resp.code = ResponseResultCode.SUCCESS;
					//}
					
				}
				
            }
			catch (Exception ex)
			{
				resp.msg = ex.Message;
				resp.code = ResponseResultCode.FAIL;
			}
			return base.CreateResponse(resp);
		}
		/// <summary>
		/// 查询就诊卡信息
		/// </summary>
		/// <param name="req"></param>
		/// <returns></returns>
		[HttpPost]
		[Route("queryAppCardInfo")]
		public HttpResponseMessage queryAppCardInfo(queryAppCardInfoReqDTO req)
		{
			DefaultResponse resp = new DefaultResponse();
			try
			{
				if (req == null || string.IsNullOrEmpty(req.OrganizeId) || string.IsNullOrEmpty(req.CARD_NO))
				{
					resp.msg = "接收到的传参对象为null或请求参数不完整，请检查接口传参！";
					resp.code = ResponseResultCode.FAIL;
				}
				else
				{
					var cardInfo = _selfServiceDmnService.queryAppCardInfo(req);
					if (cardInfo==null)
					{
						resp.msg = "his中无该卡信息，患者第一次就诊，请在挂号窗口人工挂号！";
						resp.code = ResponseResultCode.FAIL;
					}
					else
					{
						cardInfo.QUERY_TIME = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
						resp.data = cardInfo;
						resp.msg = "";
						resp.code = ResponseResultCode.SUCCESS;
					}
					
				}

			}
			catch (Exception ex)
			{
				resp.msg = ex.Message;
				resp.code = ResponseResultCode.FAIL;
			}
			return base.CreateResponse(resp);
		}
		/// <summary>
		/// 查取门诊费用主表
		/// </summary>
		/// <param name="req"></param>
		/// <returns></returns>
		[HttpPost]
		[Route("queryOutpfeeMasterInfo")]
		public HttpResponseMessage queryOutpfeeMasterInfo(queryOutpfeeMasterInfoReqDTO req)
		{
			DefaultResponse resp = new DefaultResponse();
			try
			{
				if (req == null || string.IsNullOrEmpty(req.OrganizeId) || string.IsNullOrEmpty(req.CARD_NO) || string.IsNullOrEmpty(req.START_DATE) || string.IsNullOrEmpty(req.END_DATE))
				{
					resp.msg = "接收到的传参对象为null或请求参数不完整，请检查接口传参！";
					resp.code = ResponseResultCode.FAIL;
				}
				else
				{
					DateTime ksrq = DateTime.Parse(req.START_DATE);
					DateTime jsrq = DateTime.Parse(req.END_DATE);
					var cardInfo = _selfServiceDmnService.queryOutpfeeMasterInfo(req);
					if (cardInfo==null || cardInfo.ITEMS == null || cardInfo.ITEMS.Count==0)
					{
						resp.msg = "这段时间内，未找到该卡号的门诊结算记录";
						resp.code = ResponseResultCode.FAIL;
					}
					else
					{
						resp.data = cardInfo;
						resp.msg = "";
						resp.code = ResponseResultCode.SUCCESS;
					}
					
				}

			}
			catch (Exception ex)
			{
				resp.msg = ex.Message;
				resp.code = ResponseResultCode.FAIL;
			}
			return base.CreateResponse(resp);
		}
		[HttpPost]
		[Route("queryOutpfeeDetailInfo")]
		public HttpResponseMessage queryOutpfeeDetailInfo(queryOutpfeeDetailInfoReqDTO req)
		{
			DefaultResponse resp = new DefaultResponse();
			try
			{
				if (req == null || string.IsNullOrEmpty(req.OrganizeId) || string.IsNullOrEmpty(req.RECEIPT_NO))
				{
					resp.msg = "接收到的传参对象为null或请求参数不完整，请检查接口传参！";
					resp.code = ResponseResultCode.FAIL;
				}
				else
				{
					var cardInfo = _selfServiceDmnService.queryOutpfeeDetailInfo(req);
					if (cardInfo == null || cardInfo.ITEMS == null || cardInfo.ITEMS.Count == 0)
					{
						resp.msg = "该票据号未找到结算明细";
						resp.code = ResponseResultCode.FAIL;
					}
					else
					{
						resp.data = cardInfo;
						resp.msg = "";
						resp.code = ResponseResultCode.SUCCESS;
					}

				}

			}
			catch (Exception ex)
			{
				resp.msg = ex.Message;
				resp.code = ResponseResultCode.FAIL;
			}
			return base.CreateResponse(resp);
		}

		[HttpPost]
		[Route("QueryInPatientInfo")]
		public HttpResponseMessage QueryInPatientInfo(queryAppCardInfoReqDTO req)
		{
			DefaultResponse resp = new DefaultResponse();
			try
			{
				if (req == null || string.IsNullOrEmpty(req.OrganizeId) || string.IsNullOrEmpty(req.CARD_NO))
				{
					resp.msg = "接收到的传参对象为null或请求参数不完整，请检查接口传参！";
					resp.code = ResponseResultCode.FAIL;
				}
				else
				{
					var cardInfo = _selfServiceDmnService.QueryInPatientInfo(req);
					if (cardInfo == null || cardInfo.ITEMS == null || cardInfo.ITEMS.Count == 0)
					{
						resp.msg = "这段时间内，未找到该卡号的门诊结算记录";
						resp.code = ResponseResultCode.FAIL;
					}
					else
					{
						resp.data = cardInfo;
						resp.msg = "";
						resp.code = ResponseResultCode.SUCCESS;
					}

				}

			}
			catch (Exception ex)
			{
				resp.msg = ex.Message;
				resp.code = ResponseResultCode.FAIL;
			}
			return base.CreateResponse(resp);
		}
	}
}
