using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Newtouch.Core.Common;
using Newtouch.HIS.Application.Interface;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.HIS.Domain.IDomainServices.SystemManage;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.HIS.DomainServices.SystemManage;
using Newtouch.HIS.Repository;
using Newtouch.Tools;

namespace Newtouch.HIS.Base.HOSP.Controllers
{
    public class CommonLibraryController : ControllerBase
    {
        
        
        private readonly ISysOrganizeDmnService _sysOrganizeDmnService;
        private readonly ISysMedicineRepo _SysMedicineRepository;
        private readonly ISysMedicineDmnService _sysMedicineDmnService;
        private readonly ISysMedicalOrderFrequencyRepo _sysMedicalOrderFrequencyRepo;
        private readonly ISysMedicineAntibioticInfoRepo _sysMedicineAntibioticInfoRepo;
        private readonly ISysPharmacyDepartmentRepo _sysPharmacyDepartmentRepo;
        private readonly ISysPharmacyDepartmentApp _sysPharmacyDepartmentApp;
        private readonly ISysMedicineUsageRepo _sysMedicineUsageRepo;
        private readonly ICommonLibraryDmnService _iCommonLibraryDmnService;
        private readonly ISysMedicineBaseRepo _sysMedicineBaseRepo;
        private readonly ISysMedicinePropertyBaseRepo _sysMedicinePropertyBaseRepo;
        private readonly ISysChargeCategoryRepo _sysChargeCategoryRepo;
        private readonly ISysChargeCategoryBaseRepo _sysChargeCategoryBaseRepo;
        private readonly ISysChargeItemRepo _sysChargeItemRepo;
        private readonly ISysChargeItemBaseRepo _sysChargeItemBaseRepo;
        
        


         public CommonLibraryController(ISysOrganizeDmnService sysOrganizeDmnService
             , ISysMedicineRepo SysMedicineRepository, ISysMedicineDmnService sysMedicineDmnService
             , ISysMedicalOrderFrequencyRepo SysMedicalOrderFrequencyRepo
             , ISysMedicineAntibioticInfoRepo MedicineAntibioticInfoRepo,
             ISysPharmacyDepartmentRepo sysPharmacyDepartmentRepo,
             ISysPharmacyDepartmentApp sysPharmacyDepartmentApp, ICommonLibraryDmnService iCommonLibraryDmnService,
             ISysMedicineBaseRepo SysMedicineBaseRepo, ISysMedicinePropertyBaseRepo SysMedicinePropertyBaseRepo,ISysChargeCategoryRepo SysChargeCategoryRepo
             ,ISysChargeCategoryBaseRepo SysChargeCategoryBaseRepo,
             ISysChargeItemRepo SysChargeItemRepo,
             ISysChargeItemBaseRepo SysChargeItemBaseRepo
             )
         {
             _sysOrganizeDmnService = sysOrganizeDmnService;
             _SysMedicineRepository = SysMedicineRepository;
             _sysMedicineDmnService = sysMedicineDmnService;
             _sysMedicalOrderFrequencyRepo = SysMedicalOrderFrequencyRepo;
             _sysMedicineAntibioticInfoRepo = MedicineAntibioticInfoRepo;
             _sysPharmacyDepartmentRepo = sysPharmacyDepartmentRepo;
             _sysPharmacyDepartmentApp = sysPharmacyDepartmentApp;
             _iCommonLibraryDmnService = iCommonLibraryDmnService;
             _sysMedicineBaseRepo = SysMedicineBaseRepo;
             _sysMedicinePropertyBaseRepo = SysMedicinePropertyBaseRepo;
             _sysChargeCategoryRepo = SysChargeCategoryRepo;
             _sysChargeCategoryBaseRepo = SysChargeCategoryBaseRepo;
             _sysChargeItemRepo = SysChargeItemRepo;
             _sysChargeItemBaseRepo = SysChargeItemBaseRepo;
         }
        
        
        
        //grid json 数据源
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetGridJson(Pagination pagination, string keyword, string organizeId,string zt,string ypflCode,string ypgg ,string ycmc,string dlCode)
        {
            pagination.sidx = "CreateTime desc";
            pagination.sord = "asc";
            var data = new
            {
                rows = _iCommonLibraryDmnService.GetPaginationList(organizeId, pagination, zt, ypflCode, keyword,ypgg, ycmc, dlCode),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(data.ToJson());
        }
        
        
        
        
        
        
        
        
        // /**
        //  * 同步全部药品
        //  */
        // [HttpGet]
        // [HandlerAjaxOnly]
        // public ActionResult SyncAllDrug(string organizeId)
        // {
        //     //默认获取当前登录用户的机构
        //     var userIdentityOrganizeId = UserIdentity.OrganizeId;
        //     var validListByOrg = _sysMedicineBaseRepo.GetValidListByOrg("*");
        //     
        //     foreach (var item in validListByOrg)
        //     {
        //         //查看是否同步过
        //         var sysMedicineEntity = _SysMedicineRepository.FindEntity(p => p.ypCode == item.ypCode && p.OrganizeId == userIdentityOrganizeId && p.ypmc == item.ypmc);
        //         if (sysMedicineEntity != null)
        //         {
        //             //同步过则跳过
        //             continue;
        //         }
        //         _iCommonLibraryDmnService.SyncCommonDrug(item,userIdentityOrganizeId);
        //     }
        //     return Success("同步成功!");
        // }
        
        
        
        [HttpPost]
        [HandlerAjaxOnly]
        public ActionResult Sync(List<int> idList,string organizeId,string Type)
        {
            //默认获取当前登录用户的机构
            // var userIdentityOrganizeId = UserIdentity.OrganizeId;
                //药品
                foreach (var id in idList)
                {
                    if ("yp".Equals(Type))
                    {   
                        //获取药品信息
                        var s = _sysMedicineBaseRepo.FindEntity(p=>p.ypId == id);
                        //查看是否同步过
                        var sysMedicineEntity = _SysMedicineRepository.FindEntity(p => p.ycmc == s.ycmc && p.OrganizeId == organizeId && p.ypmc == s.ypmc);
                        if (sysMedicineEntity == null)
                        {
                            _iCommonLibraryDmnService.SyncCommonDrug(s,organizeId);
                        }
                        else
                        {
                            return Error("["+s.ypmc+"]已经同步过！请勿勾选");
                        }  
                    }else if ("sfdl".Equals(Type))
                    {
                        var s = _sysChargeCategoryBaseRepo.FindEntity(p => p.dlId == id);
                        var sysChargeCategoryEntity = _sysChargeCategoryRepo.FindEntity(p => p.dlCode == s.dlCode && p.OrganizeId == organizeId && p.dlmc == s.dlmc);
                        if (sysChargeCategoryEntity == null)
                        {
                            _iCommonLibraryDmnService.SyncCommonSfdl(s,organizeId);
                            
                        }else
                        {
                            return Error("["+s.dlmc+"]已经同步过！请勿勾选");
                        }
                        //收费大类
                        
                    }else if ("hc".Equals(Type))
                    {
                        //暂用公用药品材料费
                        //获取材料信息
                        var s = _sysMedicineBaseRepo.FindEntity(p=>p.ypId == id);
                        //查看是否同步过
                        var sysMedicineEntity = _SysMedicineRepository.FindEntity(p => p.ycmc == s.ycmc && p.OrganizeId == organizeId && p.ypmc == s.ypmc);
                        if (sysMedicineEntity == null)
                        {
                            _iCommonLibraryDmnService.SyncCommonDrug(s,organizeId);
                        }
                        else
                        {
                            return Error("["+s.ypmc+"]已经同步过！请勿勾选");
                        }  
                        
                    }else if ("sfxm".Equals(Type))
                    {
                        //sfxm 
                        var s = _sysChargeItemBaseRepo.FindEntity(p => p.sfxmId == id);
                        //查看是否同步过
                        var sysChargeItem = _sysChargeItemRepo.FindEntity(p => p.sfxmmc == s.sfxmmc && p.OrganizeId == organizeId && p.sfdlCode == s.sfdlCode);
                        if (sysChargeItem == null)
                        {
                            _iCommonLibraryDmnService.SyncCommonSfxm(s,organizeId);
                        }
                        else
                        {
                            return Error("["+s.sfxmmc+"]已经同步过！请勿勾选");
                        }  
                    }
                    
                }
            
            return Success("同步成功!");
        }
        /**
         * 同步医保药品目录
         */
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult SyncYbyp()
        {
            _iCommonLibraryDmnService.SyncYbyp(UserIdentity.OrganizeId);
            
            return Success("医保药品目录同步成功!");
        }
        
        
        public ActionResult SysCommonMedicineAdd()
        {
            return View();
        }
        
        public ActionResult SysCommonStuffAdd()
        {
            return View();
        }
        
        public ActionResult ChargeItemForm()
        {
            return View();
        }

        
        
        
        
        public ActionResult GetPcSelectJson()
        {
            var data = new List<object>();
            var entityList = _sysMedicalOrderFrequencyRepo.GetOrderFrequencyList(this.OrganizeId);
            foreach (var item in entityList)
            {
                var obj = new
                {
                    id = item.yzpcCode,
                    text = item.yzpcmc
                };
                data.Add(obj);
            }
            return Content(data.ToJson());
        }
        
        
        /// <summary>
        /// 修改信息时，把信息带到新页面
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetFormJson(int keyValue)
        {
            var data = _iCommonLibraryDmnService.GetFormJson(keyValue);
            return Content(data.ToJson());
        }
        
        
    }
}