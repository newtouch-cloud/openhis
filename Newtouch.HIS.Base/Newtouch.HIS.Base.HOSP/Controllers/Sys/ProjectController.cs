using Newtouch.Common;
using Newtouch.Core.Common;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.HIS.Domain.ValueObjects;
using Newtouch.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Newtouch.HIS.Base.HOSP.Controllers
{
    public class ProjectController : ControllerBase
    {
        private readonly ISysItemsDetailRepository _sysItemsDetailRepository;
        public ProjectController(ISysItemsDetailRepository sysItemsDetailRepository)
        {
            this._sysItemsDetailRepository = sysItemsDetailRepository;
        }
        public ActionResult ProjectType()
        {
            return View();
        }
        public ActionResult ProjectTypeForm()
        {
            return View();
        }
        public ActionResult ProjectTypeChildForm()
        {
            return View();
        }

        public ActionResult TreeViewdata()
        {
            var wardTree = _sysItemsDetailRepository.GetTreeViewdata(this.OrganizeId);
            var wardonly = wardTree.GroupBy(p => new { p.kmdm, p.kmmc,p.sjkmdm}).Select(p => new { p.Key.kmdm, p.Key.kmmc,p.Key.sjkmdm});


            var treeList = new List<TreeViewModel>();
            foreach (var item in wardonly)
            {
                var patInfo = wardTree.Where(p => p.sjkmdm == item.kmdm).ToList();

                if (item.sjkmdm == null || item.sjkmdm == "")
                {
                    foreach (var itempat in patInfo)
                    {
                        TreeViewModel treepat = new TreeViewModel();
                        treepat.id = itempat.kmdm;
                        treepat.text = itempat.kmdm + "_" + itempat.kmmc;
                        treepat.value = itempat.kmdm;
                        treepat.parentId = itempat.sjkmdm;
                        treepat.isexpand = false;
                        treepat.complete = true;
                        treepat.showcheck = true;
                        treepat.checkstate = 0;
                        treepat.hasChildren = false;
                        treepat.Ex1 = "c";
                        treeList.Add(treepat);
                    }

                    TreeViewModel tree = new TreeViewModel();
                    bool hasChildren = patInfo.Count == 0 ? false : true;
                    tree.id = item.kmdm;
                    tree.text = item.kmdm + "_" + item.kmmc;
                    tree.value = item.kmdm;
                    tree.parentId = null;
                    tree.isexpand = true;
                    tree.complete = true;
                    tree.showcheck = true;
                    tree.checkstate = 0;
                    tree.hasChildren = hasChildren;
                    tree.Ex1 = "p";
                    treeList.Add(tree);
                }
                else
                {
                    foreach (var itempat in patInfo)
                    {
                        TreeViewModel treepat = new TreeViewModel();
                        treepat.id = itempat.kmdm;
                        treepat.text = itempat.kmdm + "_" + itempat.kmmc;
                        treepat.value = itempat.kmdm;
                        treepat.parentId = itempat.sjkmdm;
                        treepat.isexpand = false;
                        treepat.complete = true;
                        treepat.showcheck = true;
                        treepat.checkstate = 0;
                        treepat.hasChildren = false;
                        treepat.Ex1 = "c";
                        treeList.Add(treepat);
                    }
                }
            }
            var a = treeList.TreeViewJson(null);
            return Content(treeList.TreeViewJson(null));
        }

        public ActionResult GetProjectMx(Pagination pagination, string kmdm) {
            pagination.sidx = "Id asc";
            pagination.sord = "asc";
            var data = new
            {
                rows = _sysItemsDetailRepository.GetProjectMxVO(this.OrganizeId, kmdm),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records,
            };
            return Content(data.ToJson());
        }
        public ActionResult GetXmandYp(Pagination pagination,string kmdm, int? xmlx ) {
            pagination.sidx = "Id asc";
            pagination.sord = "asc";
            var data = new
            {
                rows = _sysItemsDetailRepository.GetXmandYp(this.OrganizeId, kmdm, xmlx),
                total = pagination.total,
                page = pagination.page,
                records = pagination.records,
            };
            return Content(data.ToJson());
        }
        public ActionResult UpdateProjectMx(string kmdm, string xmdm) {
            try
            {
                int i = _sysItemsDetailRepository.UpdateMx(this.OrganizeId, kmdm, xmdm, UserIdentity.rygh);
                if (i == 0) {
                    return Error("移除失败，请稍后再试！");
                }
                else
                {
                    return Success("移除成功！");
                }
            }
            catch (Exception e) {
                return Error(e.Message);
            }
        }

        public ActionResult InsertProjectMx(string kmdm,string xmdm)
        {
            try
            {
                int i = _sysItemsDetailRepository.InsertMx(this.OrganizeId, kmdm, xmdm, UserIdentity.rygh);
                if (i == 0)
                {
                    return Error("添加失败，请稍后再试！");
                }
                else
                {
                    return Success("添加成功！");
                }
            }
            catch (Exception e)
            {
                return Error(e.Message);
            }
        }
        public ActionResult deleteMl (string kmdm)
        {
            try
            {
                int i = _sysItemsDetailRepository.DeleteMl(this.OrganizeId, kmdm);
                if (i == 0)
                {
                    return Error("目录下还有子项，无法删除该目录！");
                }
                else
                {
                    return Success("删除成功！");
                }
            }
            catch (Exception e)
            {
                return Error(e.Message);
            }
        }

        public ActionResult getmlbykmdm(string kmdm)
        {
            var list = _sysItemsDetailRepository.GetMlbyKmdm(kmdm, this.OrganizeId);
            return Content(list.ToJson());
        }
        public ActionResult getsjdm()
        {
            var list = _sysItemsDetailRepository.Getsjdm(this.OrganizeId);
            var data = new List<object>();
            foreach (ProjectZbVO item in list)
            {
                data.Add(new { Id = item.kmdm, Name = item.kmmc });
            }
            return Content(data.ToJson());
        }
        public ActionResult insertMl(string kmmc,string kmdm,string sjkmdm,int xmlx,int gmlbz,string  sl,int zt)
        {
            try
            {
                decimal s = decimal.Parse(sl);
                var list = _sysItemsDetailRepository.GetMlbyKmdm(kmdm, this.OrganizeId);
                if (list.Count>0)
                {
                    return Error("已经存在该目录，请重新输入！");
                }
                else
                {
                    int i = _sysItemsDetailRepository.InsertMl(kmmc, kmdm, sjkmdm, xmlx, gmlbz, s, this.OrganizeId, UserIdentity.rygh,zt);
                    if (i == 0)
                    {
                        return Error("添加失败，请稍后再试！");
                    }else
                    {
                        return Success("添加成功！");
                    }
                }

            }
            catch(Exception e)
            {
                return Error(e.Message);
            }
        }
        public ActionResult updateMl(string kmmc, string kmdm, string sjkmdm, int xmlx, int gmlbz, decimal sl, int zt)
        {
            try
            {
                    int i = _sysItemsDetailRepository.UpdateMl(kmmc, kmdm, sjkmdm, xmlx, gmlbz, sl, this.OrganizeId, UserIdentity.rygh, zt);
                    if (i == 0)
                    {
                        return Error("修改失败，请稍后再试！");
                    }
                    else
                    {
                        return Success("添加成功！");
                    }

            }
            catch (Exception e)
            {
                return Error(e.Message);
            }
        }
    }
}