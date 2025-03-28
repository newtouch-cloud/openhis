using FrameworkBase.MultiOrg.Web;
using Newtouch.Common.Operator;
using Newtouch.Core.Common;
using Newtouch.Core.Common.Utils;
using Newtouch.EMR.Domain.BusinessObjects;
using Newtouch.EMR.Domain.Entity;
using Newtouch.EMR.Domain.IDomainServices;
using Newtouch.EMR.Domain.IRepository;
using Newtouch.EMR.Domain.ValueObjects.MedicalRecord;
using Newtouch.Infrastructure;
using Newtouch.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Newtouch.EMR.Web.Areas.MedicalRecordManage.Controllers
{
    public class TemplateDesignController : OrgControllerBase
    {
        
        private readonly IHljlDataRepo _HljlDataRepo;
        private readonly IBlmblbRepo _bl_mblbRepo;
        private readonly Ibl_hljlRepo _HljlRepo;
        public static string BlFilePath = ConfigurationHelper.GetAppConfigValue("BlFilePath");
        private readonly IMedicalRecordDmnService _medicalRecordDmnService;

        public TemplateDesignController(IHljlDataRepo HljlDataRepo)
        {
            this._HljlDataRepo = HljlDataRepo;
        }


        // GET: MedicalRecordManage/TemplateDesign
        //public ActionResult Index()
        //{
        //    return View();
        //}
        /// <summary>
        /// 临床护理记录
        /// </summary>
        /// <returns></returns>
        public ActionResult HljlGraph()
        {
            ViewBag.OrgId = this.OrganizeId;
            ViewBag.ReportServerHOST = ConfigurationHelper.GetAppConfigValue("ReportServer.HOST");
            return View();
        }

        public ActionResult BtnSubmit(IList<HljldataVO> data,string mbbh,string zyh,string totals) {
            OperatorModel user = this.UserIdentity;
            
            string DataSource = "";
            string blId = "";
            if (!string.IsNullOrWhiteSpace(mbbh))
            {

                if (int.Parse(totals.ToString()) > 0)
                {
                    var bl = _HljlDataRepo.blIdselect(zyh, mbbh, this.OrganizeId);
                    foreach (var item in bl)
                    {
                        blId = item.Id;
                    }
                    if (!string.IsNullOrWhiteSpace(blId))
                    {
                        _HljlDataRepo.BtnSubmit(user, data, blId,zyh);
                    }
                }
                if (blId=="")
                {
                    if (!string.IsNullOrWhiteSpace(zyh))
                    {
                        var ety = _HljlRepo.FindEntity(p =>  p.zyh == zyh && p.mbbh == mbbh &&p.OrganizeId == OrganizeId && p.zt == "1");
                        if (ety != null)
                        {
                            blId = ety.Id;
                        }
                    }
                    if (string.IsNullOrWhiteSpace(blId))
                    {
                        string ywid = zyh.Trim();
                        var mb = _bl_mblbRepo.GetTemplateById(mbbh);
                        string BLMC = DateTime.Now.ToString("ddHHmm") + mb.mbmc.Replace("模板", "");
                        string path = BlFilePath + DateTime.Now.ToString("yyyy/MM/dd/") + ywid + "/";
                        var mbety = _bl_mblbRepo.FindEntity(mbbh);
                        if (mbety != null && mbety.EnableDataLoad == "1" && !string.IsNullOrWhiteSpace(mbety.DataSource))
                        {
                            DataSource = mbety.DataSource;
                        }
                        blId = BL_Save(mb.bllx, mbbh, zyh, path, BLMC, DataSource);

                    }

                    _HljlDataRepo.BtnSubmit(user, data, blId, zyh);
                }
            }
            else {
                _HljlDataRepo.BtnSubmit(user, data, blId,zyh);
            }
            
            return Success(blId);
        }

        //页面加载数据
        public ActionResult HljlLoadData(Pagination pagination,string zyh,string blId,string kssj,string jssj,string mbbh) {

            IList<HljldataVO> list = new List<HljldataVO>();
            if (!string.IsNullOrWhiteSpace(mbbh))
            {
                list = _HljlDataRepo.mbbhselect(zyh, mbbh, this.OrganizeId,kssj,jssj);
                var data = new
                {
                    rows = list,
                    total = pagination.total,
                    page = pagination.page,
                    records = pagination.records,
                };
                return Content(data.ToJson());
            }
            else {
                list = _HljlDataRepo.HljlLoadData(pagination, zyh, blId, this.OrganizeId, kssj, jssj);

                var data = new
                {
                    rows = list,
                    total = pagination.total,
                    page = pagination.page,
                    records = pagination.records,
                };
                return Content(data.ToJson());
            }
            
        }




        public string BL_Save(string bllx, string mbbh, string zyh, string path, string BLMC, string DataSource = null)
        {
            var medicalRecord = new medicalRecordVO();
            medicalRecord.ID = Guid.NewGuid().ToString();
            medicalRecord.blxtml = path;
            medicalRecord.mbbh = mbbh;
            medicalRecord.zyh = zyh;
            medicalRecord.blxtmc_yj = BLMC;
            medicalRecord.CreatorCode = this.UserIdentity.UserCode;
            medicalRecord.ksdm = this.UserIdentity.DepartmentCode;
            medicalRecord.ksmc = this.UserIdentity.DepartmentName;
            medicalRecord.OrganizeId = OrganizeId;
            medicalRecord.bllx = bllx;
            medicalRecord.dzbl_id = "R" + EFDBBaseFuncHelper.Instance.GetNewFieldUniqueValue("YB_Inp_PatRegInfo.dzbl_id", OrganizeId, "{0:D10}", false);
            //插入病人病历关系表
            var Entity = new ZymeddocsrelationEntity();
            Entity.Id = Guid.NewGuid().ToString();
            Entity.zyh = zyh;
            Entity.blmc = BLMC;
            Entity.bllx = bllx;
            Entity.blzt = 0;
            Entity.mbId = mbbh;
            Entity.IsParent = 0;
            Entity.blId = medicalRecord.ID;
            Entity.OrganizeId = OrganizeId;
            Entity.blrq = DateTime.Now;
            Entity.CreatorCode = this.UserIdentity.UserCode;
            Entity.ysxm = this.UserIdentity.UserName;
            Entity.ysgh = this.UserIdentity.rygh;
            Entity.CreateTime = DateTime.Now;
            Entity.zt = "1";
            Entity.DataSource = DataSource;
            _medicalRecordDmnService.MedicalRecordSave(medicalRecord, Entity);
            return medicalRecord.ID;
        }

        
        public ActionResult HljlCtrlQx(string blId,string mbbh, string zyh) {

            var data= _HljlDataRepo.HljlCtrlQx(blId,mbbh,OrganizeId,UserIdentity.rygh);
            return Content(data.ToJson());
        }

        public ActionResult Infodiv(string zyh) {
            var data = _HljlDataRepo.Infodiv(zyh, this.OrganizeId);
            return Content(data.ToJson());
        }


        public ActionResult DelRecords(string Id)
        {
            if (!string.IsNullOrWhiteSpace(Id))
            {
                var ety = _HljlDataRepo.FindEntity(p => p.Id == Id);
                if (ety != null)
                {
                    ety.zt = "0";
                    ety.Modify(Id);
                    _HljlDataRepo.Update(ety);
                    return Success("删除成功");
                }
            }
            return Error("请刷新重试");
        }
    }
}