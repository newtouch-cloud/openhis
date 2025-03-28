using FrameworkBase.MultiOrg.Domain.IRepository;
using FrameworkBase.MultiOrg.Web;
using Newtouch.Common;
using Newtouch.Core.Common.Exceptions;
using Newtouch.Domain.Entity;
using Newtouch.Domain.IDomainServices;
using Newtouch.Domain.IRepository;
using Newtouch.Domain.ValueObjects.Inpatient;
using Newtouch.Infrastructure;
using Newtouch.Tools;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Newtouch.CIS.Web.Areas.TemplateManage.Controllers
{
    public class DoctorServiceTemplateController : OrgControllerBase
    {
        private readonly IInpatientOrderPackageRepo _inpatientOrderPackageRepo;
        private readonly ISysConfigRepo _sysConfigRepo;
        private readonly IInpatientOrderPackageDmnService _inpatientOrderPackageDmnService;
        private readonly FrameworkBase.MultiOrg.Domain.IDomainServices.IBaseDataDmnService _iBaseDataDmnService;
        public override ActionResult Index() {
            ViewBag.bhbq = _sysConfigRepo.GetBoolValueByCode("DoctorServiceTemplate", OrganizeId, true);//住院医嘱套餐是否包含病区
            return View();
        }
        public ActionResult ItemForm()
        {
            ViewBag.bwhide = _sysConfigRepo.GetValueByCode("bwhide", OrganizeId) ?? "false";//住院项目录入部位显示隐藏配置    
            return View();
        }
        public ActionResult MedicineForm()
        {
            ViewBag.qzfs = _sysConfigRepo.GetValueByCode("QZFS", OrganizeId) ?? "day";
            return View();
        }
        public ActionResult TakeMedicineForm()
        {
            ViewBag.qzfs = _sysConfigRepo.GetValueByCode("QZFS", OrganizeId) ?? "day";
            return View();
        }
        public ActionResult WordForm()
        {
            return View();
        }

        public ActionResult FoodForm()
        {
            return View();
        }

        /// <summary>
        /// 处方模板树
        /// </summary>
        /// <param name="mblx"></param>
        /// <param name="cflx"></param>
        /// <returns></returns>
        public ActionResult GetTreeList(int tclx, int tcfw, int? expandyzlx)
        {
            var treeList = new List<TreeViewModel>();

            if (tclx == 0)
            {
                treeList.AddRange(GetStaticTreeList(tcfw, (int)EnumYzlx.Yp, expandyzlx));
                treeList.AddRange(GetStaticTreeList(tcfw, (int)EnumYzlx.Wz, expandyzlx));
                treeList.AddRange(GetStaticTreeList(tcfw, (int)EnumYzlx.sfxm, expandyzlx));
                treeList.AddRange(GetStaticTreeList(tcfw, (int)EnumYzlx.Cydy, expandyzlx));
                treeList.AddRange(GetStaticTreeList(tcfw, (int)EnumYzlx.ssyz, expandyzlx));
            }
            else
            {
                treeList.AddRange(GetStaticTreeList(tcfw, tclx));
            }
            return Content(treeList.TreeViewJson(null));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mblx"></param>
        /// <param name="cflx"></param>
        /// <returns></returns>
        private List<TreeViewModel> GetStaticTreeList(int tcfw, int tclx, int? expandyzlx = null)
        {
            var yzlxmc = ((EnumYzlx)tclx).GetDescription();

            var treeList = new List<TreeViewModel>();

            //套餐
            treeList.Add(new TreeViewModel()
            {
                id = tclx.ToString(),
                value = tclx.ToString(),
                text = yzlxmc,
                parentId = null,
                hasChildren = true,
                isexpand = (expandyzlx ?? 0) != 0 ? (tclx == expandyzlx ? true : false) : true,
                complete = true,
            });

            //套餐明细
            var data = _inpatientOrderPackageRepo.IQueryable().Where(a => a.OrganizeId == this.OrganizeId && a.tclx == tclx && a.tcfw == tcfw && a.zt == "1").Select(a => new { a.tcmc, a.tclx, a.Id, a.CreateTime, a.LastModifyTime }).OrderByDescending(a => a.CreateTime).OrderByDescending(a => a.LastModifyTime).ToList();
            foreach (var item in data)
            {
                treeList.Add(new TreeViewModel()
                {
                    id = "",   //模板Id
                    value = item.tclx.ToString(),
                    text = item.tcmc,
                    parentId = item.tclx.ToString(),
                    hasChildren = false,
                    isexpand = false,
                    complete = true,
                    Ex1 = item.Id
                });
            }
            return treeList;
        }

        public ActionResult Submit(InpatientOrderPackageEntity mbObj, List<InpatientOrderPackageVO> mbDetailList) {
            if (mbObj == null|| string.IsNullOrWhiteSpace(mbObj.tcfw.ToString()))
            {
                throw new FailedException("缺少套餐数据");
            }
            switch (mbObj.tcfw) {
                case (int)EnumTcfw.Person:
                    mbObj.ysgh = UserIdentity.rygh;
                    break; //个人时，医生赋值
                case (int)EnumTcfw.Dept:
                    mbObj.DeptCode = UserIdentity.DepartmentCode;
                    break; //科室时
                case (int)EnumTcfw.Ward:
                    var bq = _iBaseDataDmnService.GetWardListByStaffGh(UserIdentity.rygh, OrganizeId).FirstOrDefault().bqCode;
                    mbObj.WardCode = bq;
                    break; //病区时
                case (int)EnumTcfw.Hosp:
                    mbObj.ysgh = "*";
                    mbObj.DeptCode = "*";
                    mbObj.ysgh = "*";
                    break;
                    //全院时
            }
          var mbId=  _inpatientOrderPackageDmnService.saveAsTemplate(mbObj, mbDetailList, OrganizeId);
            return Success(null, mbId);
        }

        public ActionResult Delete(string mbId) {
            try
            {
                _inpatientOrderPackageDmnService.DeleteTemplate(mbId, OrganizeId);
                
                return Success();
            }
            catch (System.Exception e)
            {

                return Error("删除失败");
            }
        }
    }
}