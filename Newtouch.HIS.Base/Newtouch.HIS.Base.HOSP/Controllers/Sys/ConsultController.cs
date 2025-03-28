using System.Collections.Generic;
using System.Web.Mvc;
using Newtouch.HIS.Domain.Entity;
using Newtouch.Tools;
using System.Linq;
using Newtouch.Infrastructure;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Common;
using Newtouch.Core.Common.Exceptions;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.HIS.Domain.IDomainServices.SystemManage;
using Newtouch.HIS.Domain.IRepository.SystemManage;
using Newtouch.HIS.Domain.Entity.SystemManage;

namespace Newtouch.HIS.Base.HOSP.Controllers.Sys
{
    public class ConsultController : ControllerBase
    {
        private readonly ISysDepartmentRepository _sysDepartmentRepository;
        private readonly ISysOrganizeDmnService _sysOrganizeDmnService;
        private readonly ISysConsultDmnService _sysConsultDmnService;
        private readonly ISysConsultRepo _sysConsultRepo;
        private readonly ISysStaffRepo _sysStaffRepo;

        public ConsultController(ISysDepartmentRepository sysDepartmentRepository
            , ISysOrganizeDmnService sysOrganizeDmnService
            , ISysConsultDmnService sysConsultDmnService
            , ISysConsultRepo sysConsultRepo
            , ISysStaffRepo sysStaffRepo
            ) {
            this._sysDepartmentRepository = sysDepartmentRepository;
            this._sysOrganizeDmnService = sysOrganizeDmnService;
            this._sysConsultDmnService = sysConsultDmnService;
            this._sysConsultRepo = sysConsultRepo;
			this._sysStaffRepo = sysStaffRepo;
        }

        // GET: Consult
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ExpertForm()
        {
            return View();
        }
        public ActionResult NormalForm()
        {
            return View();
        }

        /// <summary>
        /// 获取科室列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetDeptList(string keyValue)
        {
            var entity = _sysDepartmentRepository.GetDeptListByKeyValue(this.OrganizeId,keyValue);
            return Content(entity.ToJson());
        }


        /// <summary>
        /// 获取部门的诊室列表
        /// </summary>
        /// <param name="ksCode">科室编号</param>
        /// <returns></returns>
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetConsultListByDept(string ksCode)
        {
            var list = _sysConsultRepo.GetConsultListByDept(this.OrganizeId,ksCode);

            return Content(list.ToJson());
        }


        /// <summary>
        /// 普通诊室：生成普通诊室时初始化诊室内容
        /// </summary>
        /// <param name="ksCode"></param>
        /// <returns></returns>
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult InitConsultInfo(string ksCode)
        {
            var entity = new
            {
                orgId = this.OrganizeId,
                orgName = _sysOrganizeDmnService.GetNameByOrgId(this.OrganizeId),
                ksCode = ksCode,
                ksmc = _sysDepartmentRepository.GetNameByCode(ksCode, this.OrganizeId)
            };
            return Content(entity.ToJson());
        }


        /// <summary>
        /// 专家诊室：获取科室下的专家列表
        /// </summary>
        /// <param name="ksCode">科室编号</param>
        /// <returns></returns>
        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetExpertListByDept(string ksCode)
        {
            var treeList = new List<TreeViewModel>();
            //获取专家医生
            var list = _sysConsultDmnService.GetExpertListByDept(this.OrganizeId, ksCode);
            if (list == null)
            {
                return Content(treeList.TreeViewJson(null));
            }
            foreach (var item in list)
            {
                TreeViewModel tree = new TreeViewModel();
                int i = list.Count(t => t.gh == item.gh);

                tree.id = item.gh;
                tree.text = item.name;
                tree.value = item.gh;
                tree.parentId = null;
                tree.isexpand = true;
                tree.complete = true;
                tree.showcheck = true;
                tree.checkstate = i;
                tree.hasChildren = false;
                treeList.Add(tree);
            }
            return Content(treeList.TreeViewJson(null));
        }

        /// <summary>
        /// 生成专家诊室
        /// </summary>
        /// <param name="expert"></param>
        /// <param name="ksCode"></param>
        /// <returns></returns>
        public ActionResult CreateExpertConsult(string expert, string ksCode)
        {
            var expertList = expert.Split(',');
            foreach (var gh in expertList) {
                var staff = _sysStaffRepo.IQueryable().Where(p => p.gh == gh && p.OrganizeId == this.OrganizeId && p.zt == "1").FirstOrDefault();
                SysConsultEntity entity= new SysConsultEntity();
                entity.zsmc = staff.Name + "专家诊室";
                entity.zsCode = staff.py + "ZJZS";
                entity.ksCode = ksCode;
                entity.ys = gh;
                entity.OrganizeId = this.OrganizeId;
                entity.py = staff.py + "ZJZS";
                entity.xh = 0;//专家诊室xh为0
                
                //诊室已存在则不再插入
                if (_sysConsultRepo.IQueryable().Any(p => p.zsCode == entity.zsCode && p.zt == "1"))
                {
                    continue;
                }

                entity.Create();
                _sysConsultRepo.Insert(entity);
            }
            return Success("操作成功。");
        }

        /// <summary>
        /// 生成普通诊室
        /// </summary>
        /// <param name="ksCode"></param>
        /// <param name="zssl"></param>
        /// <returns></returns>
        public ActionResult CreateNormalConsult( string ksCode,int zssl)
        {
            //科室名称
            var ksmc=_sysDepartmentRepository.IQueryable().Where(p => p.Code == ksCode && p.OrganizeId == this.OrganizeId && p.zt == "1").FirstOrDefault().Name;
            //科室下诊室最大序号
            var maxConsult = _sysConsultRepo.IQueryable().Where(p => p.ksCode == ksCode && p.OrganizeId == this.OrganizeId && p.zt == "1").OrderByDescending(p => p.xh).FirstOrDefault();
            var num = maxConsult == null ? 0 : maxConsult.xh;
            //自动生成多个诊室
            for (var i=0;i<zssl;i++)
            {
                num++;
                SysConsultEntity entity = new SysConsultEntity();
                entity.zsmc = ksmc + "诊室" + num;
                entity.zsCode = ksCode + "zs"+num;
                entity.ksCode = ksCode;
                entity.ys = null;
                entity.OrganizeId = this.OrganizeId;
                entity.py = ksCode + "zs" + num;
                entity.xh = num;

                //诊室已存在则不再插入
                if (_sysConsultRepo.IQueryable().Any(p => p.zsCode == entity.zsCode && p.zt == "1"))
                {
                    continue;
                }

                entity.Create();
                _sysConsultRepo.Insert(entity);
            }
            return Success("操作成功。");
        }
    }
}