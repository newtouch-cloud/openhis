using FrameworkBase.MultiOrg.Web;
using Newtouch.Common;
using Newtouch.Common.Operator;
using Newtouch.Core.Common;
using Newtouch.Herp.Domain.DTO.InputDto.Purchase;
using Newtouch.Herp.Domain.DTO.OutputDto;
using Newtouch.Herp.Domain.DTO.OutputDto.Purchase;
using Newtouch.Herp.Domain.Entity.Purchase;
using Newtouch.Herp.Domain.IDomainServices.Purchase;
using Newtouch.Herp.Domain.IRepository;
using Newtouch.Herp.Domain.IRepository.Purchase;
using Newtouch.Herp.Domain.ValueObjects.Purchase;
using Newtouch.Herp.Infrastructure;
using Newtouch.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using static Newtouch.Herp.Domain.DTO.OutputDto.OutputYY156;

namespace Newtouch.Herp.Web.Areas.PurchaseManage.Controllers
{
    public class PurchaseController :   OrgControllerBase
    {
        // GET: PurchaseManage/Purchase

        private readonly IPurchaseRepo _purchaseRepo;
        private readonly IPurchaseDetailRepo _purchaseDetailRepo;
        private readonly IPurchaseDmnService _purchaseDmnService;
        private readonly ISunPurchaseDmnService _sunPurchaseDmnService;
        private readonly IGysSupplierRepo _gysSupplierRepo;
        private readonly IPurchaseLocationRepo _purchaseLocationRepo;
        private readonly IPurchaseBillRepo _purchaseBillRepo;

        #region 页面
        //采购管理
        public ActionResult Index()
        {
            return View();
        }

        //采购新增修改
        public ActionResult Form()
        {
            return View();
        }
        
        //采购导入列表
        public ActionResult PurchaseView()
        {
            return View();
        }

        //发票查询与验收
        public ActionResult Bill()
        {
            return View();
        }

        //配送单获取验收
        public ActionResult Delivery() {
            return View();
        }
        
        //退货
        public ActionResult ReIndex()
        {
            return View();
        }
        
        //退货详情
        public ActionResult ReForm()
        {
            return View();
        }

        //发票验收
        public ActionResult BillForm()
        {
            return View();
        }
        //配送单验收
        public ActionResult DeliveryForm()
        {
            return View();
        }

        //支付确认
        public ActionResult BillPay() {
            return View();
        }

        //支付确认弹框
        public ActionResult BillPayForm() {
            return View();
        }

        //配送点
        public ActionResult LocationIndex() {
            return View();
        }

        //配送点新增
        public ActionResult LocationForm() {
            return View();
        }

        #endregion

        #region 查询页面

        //YY151 耗材采购明细信息获取(YY151)
        public ActionResult QueryYY151() {
            return View();
        }

        //YY154 耗材配送单获取(YY154)
        public ActionResult QueryYY154()
        {
            return View();
        }

        //YY155 耗材配送明细获取(YY155)
        //public ActionResult QueryYY155()
        //{
        //    return View();
        //}

        //YY157 耗材发票信息获取(YY157)
        public ActionResult QueryYY157()
        {
            return View();
        }

        //YY158 耗材发票明细获取(YY158)
        //public ActionResult QueryYY158()
        //{
        //    return View();
        //}

        //YY152 耗材退货明细信息获取(YY152)
        public ActionResult QueryYY152()
        {
            return View();
        }

        //YY159 耗材按采购单获取采购明细状态(YY159)
        public ActionResult QueryYY159()
        {
            return View();
        }

        //YY160 耗材发票状态获取(YY160)
        public ActionResult QueryYY160()
        {
            return View();
        }

        //YY161 耗材配送单状态获取(YY161)
        public ActionResult QueryYY161()
        {
            return View();
        }

        //YY162 耗材按退货单获取退货明细状态(YY162)
        public ActionResult QueryYY162()
        {
            return View();
        }

        //YY164 企业信息获取(YY164)

        public ActionResult QueryYY164()
        {
            return View();
        }

        #endregion


        #region 采购管理

        /// <summary>
        /// 分页查询采购单列表
        /// </summary>
        /// <param name="pagination"></param>
        /// <param name="kssj"></param>
        /// <param name="jssj"></param>
        /// <returns></returns>
        public ActionResult GetPurchaseGridJson(Pagination pagination, DateTime kssj, DateTime jssj, int ddzt)
        {
            var tt = _purchaseRepo.GetPurchaseGridJson(pagination, kssj, jssj, OrganizeId,ddzt);
            var data = new
            {
                rows = tt,
                total = pagination.total,
                page = pagination.page,
                records = pagination.records,
            };
            return Content(data.ToJson()); ;
        }

        public ActionResult QueryPurchasebyId(string cgId)
        {
            var result = _purchaseDmnService.QueryPurchasebyId(cgId, this.OrganizeId);
            return Content(result.ToJson());
        }
        public ActionResult QueryPurchaseDetailbyId(string cgId)
        {
            var result = _purchaseDmnService.QueryPurchaseDetailbyId(cgId, this.OrganizeId);
            return Content(result.ToJson());
        }

        public ActionResult PurchaseDelete(string cgId)
        {
            _purchaseRepo.PurchaseDelete(cgId, this.OrganizeId);
            return Success();
        }
        public ActionResult PurchaseStateUpdate(string cgId, int ddzt)
        {
            _purchaseRepo.PurchaseStateUpdate(cgId, ddzt, this.OrganizeId);
            return Success();
        }
        /// <summary>
        /// 采购订单传报
        /// </summary>
        /// <param name="cgId"></param>
        /// <param name="ddzt"></param>
        /// <returns></returns>
        public ActionResult PurchaseUpload(string cgId, int ddzt)
        {
            //1.上传至阳光采购平台 YY111
            var ddbh=_sunPurchaseDmnService.Purchase_YY111(this.OrganizeId, cgId);
            //2.更新ddbh
            _purchaseRepo.PurchaseDdbhUpdate(cgId, ddbh, this.OrganizeId);
            //3.订单填报确认 YY112
            _sunPurchaseDmnService.Purchase_YY112(this.OrganizeId, cgId);
            //4.更新传报状态
            _purchaseRepo.PurchaseStateUpdate(cgId, ddzt, this.OrganizeId);
            return Success();
        }
        #endregion

        #region 采购订单测试

        /// <summary>
        /// 采购订单传报
        /// </summary>
        /// <param name="cgId"></param>
        /// <param name="ddzt"></param>
        /// <returns></returns>
        public ActionResult PurchaseUploadWriteTest(string cgId, int ddzt)
        {
            //1.上传至阳光采购平台 YY111
            var ddbh = _sunPurchaseDmnService.Purchase_YY111(this.OrganizeId, cgId);
            //2.更新ddbh
            _purchaseRepo.PurchaseDdbhUpdate(cgId, ddbh, this.OrganizeId);
            return Success();
        }

        /// <summary>
        /// 采购订单传报
        /// </summary>
        /// <param name="cgId"></param>
        /// <param name="ddzt"></param>
        /// <returns></returns>
        public ActionResult PurchaseUploadSureTest(string cgId, int ddzt)
        {
            //3.订单填报确认 YY112
            _sunPurchaseDmnService.Purchase_YY112(this.OrganizeId, cgId);
            //4.更新传报状态
            _purchaseRepo.PurchaseStateUpdate(cgId, ddzt, this.OrganizeId);
            return Success();
        }
        #endregion

        #region 采购详情表单
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="cgEntity"></param>
        /// <param name="cgmxList"></param>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public ActionResult SubmitPurchase(PurchaseEntity cgEntity, List<PurchaseDetailEntity> cgmxList, string keyValue)
        {
            OperatorModel user = this.UserIdentity;

            cgEntity.OrganizeId = this.OrganizeId;
            cgEntity.jls = cgmxList.Count;
            // 1.新增或更新采购列表
            var cgId = _purchaseRepo.SubmitForm(cgEntity, keyValue);
            //2.删除采购明细
            if (keyValue != null)
            {
                _purchaseDetailRepo.DeleteItem(cgId, this.OrganizeId);
            }
            //3.添加采购明细
            foreach (var cgmx in cgmxList)
            {
                cgmx.OrganizeId = this.OrganizeId;
                _purchaseDetailRepo.InsertItem(cgmx, cgId);
            }
            return Success();
        }

        /// <summary>
        /// 保存 + 传报
        /// </summary>
        /// <param name="cgEntity"></param>
        /// <param name="cgmxList"></param>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public ActionResult SubmitAndUploadPurchase(PurchaseEntity cgEntity, List<PurchaseDetailEntity> cgmxList, string keyValue)
        {
            OperatorModel user = this.UserIdentity;

            cgEntity.OrganizeId = this.OrganizeId;
            cgEntity.jls = cgmxList.Count;
            // 1.新增或更新采购列表
            var cgId = _purchaseRepo.SubmitForm(cgEntity, keyValue);
            //2.删除采购明细
            if (keyValue != null)
            {
                _purchaseDetailRepo.DeleteItem(cgId, this.OrganizeId);
            }
            //3.添加采购明细
            foreach (var cgmx in cgmxList)
            {
                cgmx.OrganizeId = this.OrganizeId;
                _purchaseDetailRepo.InsertItem(cgmx, cgId);
            }
            //4.采购订单填报与传报
            PurchaseUpload(cgId, 2);
            return Success();
        }
        #endregion

        #region 出入库
        //public ActionResult QueryPurchaseStorebyId(string cgId, string keyword)
        //{

        //    var result = _purchaseDmnService.QueryPurchaseStorebyId(cgId, this.OrganizeId, Constants.CurrentYfbm.yfbmCode);
        //    return Content(result.ToJson());
        //}
        #endregion

        #region 发票查询与验收
        public ActionResult GetBillGridJson(string qybm, string fpmxbhcxtj)
        {
            var result = _sunPurchaseDmnService.Purchase_YY156(OrganizeId, qybm, fpmxbhcxtj);
            //if (result != null && result.DETAIL != null)
            //{
            //    List<OutputStructYY156> list = result.DETAIL.STRUCT;
            //    return Content(list.ToJson());
            //}
            return Content(result.ToJson());

        }

        public ActionResult BillAccept(PurchaseMainYY132 main)
        {
            //1.阳光采购平台验收
            var result = _sunPurchaseDmnService.Purchase_YY132(main, this.OrganizeId);
            //2.发票数据落地
            _purchaseBillRepo.InsertItem(main, this.OrganizeId);
            return Success();
        }
        #endregion

        #region 配送单获取验收
        public ActionResult GetDeliveryGridJson(string qybm, string psmxbhcxtj)
        {
            var result = _sunPurchaseDmnService.Purchase_YY153(OrganizeId, qybm, psmxbhcxtj);
            return Content(result.ToJson());
        }

        public ActionResult DeliveryAccept(PurchaseDeliveryVO vo)
        {

            PurchaseMainYY131 main = new PurchaseMainYY131();
            main.PSYSLX = vo.PSYSLX;
            main.JLS = 1;
            List<PurchaseStructYY131> list = new List<PurchaseStructYY131>();
            PurchaseStructYY131 st = new PurchaseStructYY131();
            st.HCTBDM = vo.HCTBDM;
            st.PSL = vo.PSL;
            st.PSMXBH = vo.PSMXBH;
            st.SCPH = vo.SCPH;
            st.YSBGS = vo.YSBGS;
            st.YSBZSM = vo.YSBZSM;
            st.YSTGS = vo.YSTGS;
            list.Add(st);
            var result = _sunPurchaseDmnService.Purchase_YY131(main, list, this.OrganizeId);
            return Success();
        }

        //public ActionResult DeliveryAccept(PurchaseMainYY131 main, PurchaseStructYY131 st)
        //{
        //    List<PurchaseStructYY131> list = new List<PurchaseStructYY131>();
        //    list.Add(st);
        //    //var result = _sunPurchaseDmnService.Purchase_YY131(main, st, this.OrganizeId);
        //    return Success();
        //}
        #endregion


        #region 外部入库 导入采购单
        public ActionResult QueryPurchaseStorebyId(string cgId, string keyword)
        {

            var result = _purchaseDmnService.QueryPurchaseStorebyId(cgId, this.OrganizeId, Constants.CurrentKf.currentKfId);
            return Content(result.ToJson());
        }
        #endregion

        #region 发票支付确认
        public ActionResult GetBillPayGridJson(PurchaseMainYY157 main)
        {
            var sfwj = "0";//是否完结  
            var bh = "";//编号查询条件 初次调用不填写
            List<PurchaseYY157VO> result = new List<PurchaseYY157VO>();
            while (sfwj == "0")//未完结
            {
                //1.调用接口
                OutputYY157 entity = _sunPurchaseDmnService.Purchase_YY157(main, this.OrganizeId);
                //2.插入列表
                if (entity.DETAIL.STRUCT != null)
                {
                    var structList = entity.DETAIL.STRUCT;
                    foreach (var obj in structList)
                    {
                        if (obj.FPZT == "20" || obj.FPZT == "30")//过滤发票状态:20已验收 30已支付
                        {
                            PurchaseYY157VO vo = new PurchaseYY157VO();
                            vo.ZFBZ = obj.FPZT == "30" ? "已支付" : "未支付";
                            vo.QYMC = _gysSupplierRepo.IQueryable(p => p.py == obj.QYBM && p.OrganizeId == this.OrganizeId && p.zt == "1").FirstOrDefault().name;
                            vo.FPBH = obj.FPBH;
                            vo.FPDM = obj.FPDM;
                            vo.FPH = obj.FPH;
                            vo.FPRQ = obj.FPRQ;
                            vo.FPHSZJE = obj.FPHSZJE;
                            vo.QYBM = obj.QYBM;
                            vo.YYBM = obj.YYBM;
                            vo.PSDBM = obj.PSDBM;
                            vo.FPZT = obj.FPZT;
                            result.Add(vo);
                        }
                    }

                }
                //3.更新是否完结和当次最后发票编号
                sfwj = entity.MAIN.SFWJ;
                bh = entity.MAIN.DCZHFPBH;
                main.FPBHCXTJ = bh;
            }
            return Content(result.ToJson());
        }


        public ActionResult BillPayAccept(PurchaseMainYY133 main)
        {
            var result = _sunPurchaseDmnService.Purchase_YY133(main, this.OrganizeId);
            return Success();
        }
        #endregion

        #region 配送点

        //查询配送点
        public ActionResult QueryLocation()
        {
            var result = _purchaseLocationRepo.IQueryable().Where(p => p.OrganizeId == this.OrganizeId && p.zt == "1" && p.psdzt != 3).OrderByDescending(p => p.CreateTime).ToList();


            return Content(result.ToJson());
        }

        //分页查询配送点
        public ActionResult GetLocationGridJson(Pagination pagination)
        {
            var tt = _purchaseLocationRepo.IQueryable().Where(p => p.OrganizeId == this.OrganizeId && p.zt=="1").OrderByDescending(p => p.CreateTime).ToList();

            var data = new
            {
                rows = tt,
                total = pagination.total,
                page = pagination.page,
                records = pagination.records,
            };
            return Content(data.ToJson()); ;
        }

        //配送点详情 (浮层)
        public ActionResult QueryLocationbyId(string Id)
        {
            var result = _purchaseLocationRepo.IQueryable().Where(p => p.Id==Id && p.OrganizeId == this.OrganizeId && p.zt=="1" && p.psdzt != 3).OrderByDescending(p => p.CreateTime).FirstOrDefault();
            return Content(result.ToJson());
        }

        public ActionResult SubmitLocation(PurchaseLocationEntity entity)
        {
            _purchaseLocationRepo.SubmitForm(entity, this.OrganizeId,entity.Id);
            return Success();
        }

        //配送点传报
        public ActionResult LocationUpload(string Id,int psdzt, string czlx)
        {
            //1.上传至阳光采购平台 YY101
            var result=_sunPurchaseDmnService.Purchase_YY101(this.OrganizeId, Id,czlx);
            //2.更新传报状态 (2 已传报)
            if (result == "Success")
            {
                _purchaseLocationRepo.LocationStateUpdate(Id, this.OrganizeId, psdzt);
            }
            return Success();
        }

        //配送点作废
        public ActionResult LocationDelete(string Id, int psdzt, string czlx)
        {
            //1.阳光采购平台作废
            var result=_sunPurchaseDmnService.Purchase_YY101(this.OrganizeId, Id, czlx);
            //2.HIS作废配送点 ( 更新配送点状态 psdzt 3已作废 )
            if (result == "Success")
            {
                _purchaseLocationRepo.LocationStateUpdate(Id, this.OrganizeId, psdzt);
            }
            return Success();
        }
        #endregion

        #region 查询页面
        //YY154 耗材配送单获取(YY154)
        public ActionResult GetGridJsonYY154(PurchaseMainYY154 main)
        {
            var sfwj = "0";//是否完结  
            var bh = "";//编号查询条件 初次调用不填写
            List<OutputStructYY154> result = new List<OutputStructYY154>();
            while (sfwj == "0")//未完结
            {
                //1.调用接口
                OutputYY154 entity = _sunPurchaseDmnService.Purchase_YY154(main, this.OrganizeId);
                //2.插入列表
                if (entity.DETAIL.STRUCT != null)
                {
                    var structList = entity.DETAIL.STRUCT;
                    foreach (var obj in structList)
                    {
                        obj.QYBM = _gysSupplierRepo.IQueryable().Where(p => p.py == obj.QYBM && p.OrganizeId == this.OrganizeId && p.zt == "1").FirstOrDefault().name;
                        result.Add(obj);
                    }
                }
                //3.更新是否完结和当次最后发票编号
                sfwj = entity.MAIN.SFWJ;
                bh = entity.MAIN.DCZHPSDBH;
                main.PSDBHCXTJ = bh;
            }
            return Content(result.ToJson());
        }

        //YY155 耗材配送明细获取(YY155)
        public ActionResult GetGridJsonYY155(PurchaseMainYY155 main)
        {
            var sfwj = "0";//是否完结  
            var bh = "";//编号查询条件 初次调用不填写
            List<OutputStructYY155> result = new List<OutputStructYY155>();
            while (sfwj == "0")//未完结
            {
                //1.调用接口
                OutputYY155 entity = _sunPurchaseDmnService.Purchase_YY155(main, this.OrganizeId);
                //2.插入列表
                if (entity.DETAIL.STRUCT != null)
                {
                    var structList = entity.DETAIL.STRUCT;
                    foreach (var obj in structList)
                    {
                        result.Add(obj);
                    }
                }
                //3.更新是否完结和当次最后发票编号
                sfwj = entity.MAIN.SFWJ;
                bh = entity.MAIN.DCZHPSMXBH;
                main.PSMXBHCXTJ = bh;
            }
            return Content(result.ToJson());
        }

        //YY151 耗材采购明细信息获取(YY151)
        public ActionResult GetGridJsonYY151(PurchaseMainYY151 main)
        {
            var sfwj = "0";//是否完结  
            var bh = "";//编号查询条件 初次调用不填写
            List<OutputStructYY151> result = new List<OutputStructYY151>();
            while (sfwj == "0")//未完结
            {
                //1.调用接口
                OutputYY151 entity = _sunPurchaseDmnService.Purchase_YY151(main, this.OrganizeId);
                //2.插入列表
                if (entity.DETAIL.STRUCT != null)
                {
                    var structList = entity.DETAIL.STRUCT;
                    foreach (var obj in structList)
                    {
                        result.Add(obj);
                    }
                }
                //3.更新是否完结和当次最后发票编号
                sfwj = entity.MAIN.SFWJ;
                bh = entity.MAIN.DCZHDDMXBH;
                main.DDMXBHCXTJ = bh;
            }
            return Content(result.ToJson());
        }
        
        //YY152 耗材退货明细信息获取(YY152)
        public ActionResult GetGridJsonYY152(PurchaseMainYY152 main)
        {
            var sfwj = "0";//是否完结  
            var bh = "";//编号查询条件 初次调用不填写
            List<OutputStructYY152> result = new List<OutputStructYY152>();
            while (sfwj == "0")//未完结
            {
                //1.调用接口
                OutputYY152 entity = _sunPurchaseDmnService.Purchase_YY152(main, this.OrganizeId);
                //2.插入列表
                if (entity.DETAIL.STRUCT != null)
                {
                    var structList = entity.DETAIL.STRUCT;
                    foreach (var obj in structList)
                    {
                        result.Add(obj);
                    }
                }
                //3.更新是否完结和当次最后发票编号
                sfwj = entity.MAIN.SFWJ;
                bh = entity.MAIN.DCZHTHMXBH;
                main.THMXBHCXTJ = bh;
            }
            return Content(result.ToJson());
        }

        //YY157 耗材发票状态获取(YY157)
        public ActionResult GetGridJsonYY157(PurchaseMainYY157 main)
        {
            var sfwj = "0";//是否完结  
            var bh = "";//编号查询条件 初次调用不填写
            List<OutputStructYY157> result = new List<OutputStructYY157>();
            while (sfwj == "0")//未完结
            {
                //1.调用接口
                OutputYY157 entity = _sunPurchaseDmnService.Purchase_YY157(main, this.OrganizeId);
                //2.插入列表
                if (entity.DETAIL.STRUCT != null)
                {
                    var structList = entity.DETAIL.STRUCT;
                    foreach (var obj in structList)
                    {
                        obj.QYBM = _gysSupplierRepo.IQueryable().Where(p => p.py == obj.QYBM && p.OrganizeId == this.OrganizeId && p.zt == "1").FirstOrDefault().name;
                        result.Add(obj);
                    }
                }
                //3.更新是否完结和当次最后发票编号
                sfwj = entity.MAIN.SFWJ;
                bh = entity.MAIN.DCZHFPBH;
                main.FPBHCXTJ = bh;
            }
            return Content(result.ToJson());
        }

        //YY158 耗材发票明细获取(YY158)
        public ActionResult GetGridJsonYY158(PurchaseMainYY158 main)
        {
            var sfwj = "0";//是否完结  
            var bh = "";//编号查询条件 初次调用不填写
            List<OutputStructYY158> result = new List<OutputStructYY158>();
            while (sfwj == "0")//未完结
            {
                //1.调用接口
                OutputYY158 entity = _sunPurchaseDmnService.Purchase_YY158(main, this.OrganizeId);
                //2.插入列表
                if (entity.DETAIL.STRUCT != null)
                {
                    var structList = entity.DETAIL.STRUCT;
                    foreach (var obj in structList)
                    {
                        result.Add(obj);
                    }
                }
                //3.更新是否完结和当次最后发票编号
                sfwj = entity.MAIN.SFWJ;
                bh = entity.MAIN.DCZHFPMXBH;
                main.FPMXBHCXTJ = bh;
            }
            return Content(result.ToJson());
        }

        //YY159 耗材按采购单获取采购明细状态(YY159)
        public ActionResult GetGridJsonYY159(PurchaseMainYY159 main)
        {
            var sfwj = "0";//是否完结  
            var bh = "";//编号查询条件 初次调用不填写
            List<OutputStructYY159> result = new List<OutputStructYY159>();
            while (sfwj == "0")//未完结
            {
                //1.调用接口
                OutputYY159 entity = _sunPurchaseDmnService.Purchase_YY159(main, this.OrganizeId);
                //2.插入列表
                if (entity.DETAIL.STRUCT != null)
                {
                    var structList = entity.DETAIL.STRUCT;
                    foreach (var obj in structList)
                    {
                        result.Add(obj);
                    }
                }
                //3.更新是否完结和当次最后发票编号
                sfwj = entity.MAIN.SFWJ;
                bh = entity.MAIN.DCZHDDMXBH;
                main.DDMXBHCXTJ = bh;
            }
            return Content(result.ToJson());
        }

        //YY160 耗材发票状态获取(YY160)
        public ActionResult GetGridJsonYY160(PurchaseMainYY160 main)
        {
            var sfwj = "0";//是否完结  
            var bh = "";//编号查询条件 初次调用不填写
            List<OutputStructYY160> result = new List<OutputStructYY160>();
            while (sfwj == "0")//未完结
            {
                //1.调用接口
                OutputYY160 entity = _sunPurchaseDmnService.Purchase_YY160(main, this.OrganizeId);
                //2.插入列表
                if (entity.DETAIL.STRUCT != null)
                {
                    var structList = entity.DETAIL.STRUCT;
                    foreach (var obj in structList)
                    {
                        result.Add(obj);
                    }
                }
                //3.更新是否完结和当次最后发票编号
                sfwj = entity.MAIN.SFWJ;
                bh = entity.MAIN.DCZHFPBH;
                main.FPBHCXTJ = bh;
            }
            return Content(result.ToJson());
        }

        //YY161 耗材配送单状态获取(YY161)
        public ActionResult GetGridJsonYY161(PurchaseMainYY161 main)
        {
            var sfwj = "0";//是否完结  
            var bh = "";//编号查询条件 初次调用不填写
            List<OutputStructYY161> result = new List<OutputStructYY161>();
            while (sfwj == "0")//未完结
            {
                //1.调用接口
                OutputYY161 entity = _sunPurchaseDmnService.Purchase_YY161(main, this.OrganizeId);
                //2.插入列表
                if (entity.DETAIL.STRUCT != null)
                {
                    var structList = entity.DETAIL.STRUCT;
                    foreach (var obj in structList)
                    {
                        result.Add(obj);
                    }
                }
                //3.更新是否完结和当次最后发票编号
                sfwj = entity.MAIN.SFWJ;
                bh = entity.MAIN.DCZHPSMXBH;
                main.PSMXBHCXTJ = bh;
            }
            return Content(result.ToJson());
        }
 
        //YY162 耗材按退货单获取退货明细状态(YY162)
        public ActionResult GetGridJsonYY162(PurchaseMainYY162 main)
        {
            var sfwj = "0";//是否完结  
            var bh = "";//编号查询条件 初次调用不填写
            List<OutputStructYY162> result = new List<OutputStructYY162>();
            while (sfwj == "0")//未完结
            {
                //1.调用接口
                OutputYY162 entity = _sunPurchaseDmnService.Purchase_YY162(main, this.OrganizeId);
                //2.插入列表
                if (entity.DETAIL.STRUCT != null)
                {
                    var structList = entity.DETAIL.STRUCT;
                    foreach (var obj in structList)
                    {
                        result.Add(obj);
                    }
                }
                //3.更新是否完结和当次最后发票编号
                sfwj = entity.MAIN.SFWJ;
                bh = entity.MAIN.DCZHTHMXBH;
                main.THMXBHCXTJ = bh;
            }
            return Content(result.ToJson());
        }
 
        //YY164 企业信息获取(YY164)
        public ActionResult GetGridJsonYY164(PurchaseMainYY164 main)
        {
            var sfwj = "0";//是否完结  
            var bh = "";//编号查询条件 初次调用不填写
            List<OutputStructYY164> result = new List<OutputStructYY164>();
            while (sfwj == "0")//未完结
            {
                //1.调用接口
                OutputYY164 entity = _sunPurchaseDmnService.Purchase_YY164(main, this.OrganizeId);
                //2.插入列表
                if (entity.DETAIL.STRUCT != null)
                {
                    var structList = entity.DETAIL.STRUCT;
                    foreach (var obj in structList)
                    {
                        result.Add(obj);
                    }
                }
                //3.更新是否完结和当次最后编号
                sfwj = entity.MAIN.SFWJ;
                bh = entity.MAIN.DCZHQYBM;
                main.QYBMCXTJ = bh;
            }
            return Content(result.ToJson());
        }

        #endregion
    }
}