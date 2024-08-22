using FrameworkBase.MultiOrg.Domain.Entity;
using FrameworkBase.MultiOrg.Domain.IRepository;
using Newtouch.Common;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Newtouch.HIS.Web.Areas.SystemManage.Controllers
{
    /// <summary>
    /// 收费大类
    /// </summary>
    public class SysChargeCategoryController : ControllerBase
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly ISysChargeCategoryRepo _sysChargeCategoryRepo;
        private readonly ISysConfigRepo _sysConfigRepo;

        /// <summary>
        /// 获取收费大类列表
        /// </summary>
        /// <param name="dllbs"></param>
        /// <returns></returns>
        public ActionResult GetListJson(string dllbs)
        {
            var list = _sysChargeCategoryRepo.GetList(this.OrganizeId, dllbs, zt: "1");
            var treeList = new List<TreeSelectModel>();
            foreach (var item in list)
            {
                TreeSelectModel treeModel = new TreeSelectModel();
                treeModel.id = item.dlCode;
                treeModel.text = item.dlmc;
                treeModel.parentId = item.ParentId == null ? null :
                    list.Where(p => p.dlId == item.ParentId).Select(p => p.dlCode).FirstOrDefault();
                treeList.Add(treeModel);
            }
            return Content(treeList.TreeSelectJson(null));
        }
        /// <summary>
        /// 获取收费大类浮层list
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public ActionResult GetsfdlJson(string keyword)
        {
            var iszytfyp = _sysConfigRepo.GetBoolValueByCode("YXZ_SETT_1010", OrganizeId);//动态参数：住院退费是否退药品费用
            var sysDls = new List<SysChargeCategoryVEntity>();
            if (iszytfyp.Value == false)
            {//显示药品
                sysDls = _sysChargeCategoryRepo.GetLazyList(this.OrganizeId).ToList();
            }
            else {//不显示药品
                sysDls = _sysChargeCategoryRepo.GetLazyList(this.OrganizeId).Where(p => p.dlCode != "01" && p.dlCode != "02" && p.dlCode != "03").ToList();
            } 
            //sysDls = sysDls.Where(p=>p.py.Contains(keyword)||p.dlmc.Contains(keyword)).ToList();
            sysDls = sysDls.Where(p => p.py.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0
            || p.dlmc.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
            return Content(sysDls.ToJson());
        }
        
    }
}