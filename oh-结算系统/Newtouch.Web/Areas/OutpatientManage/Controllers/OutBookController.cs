using FrameworkBase.MultiOrg.Domain.IRepository;
using Newtouch.Core.Common;
using Newtouch.HIS.Domain.Entity.OutpatientManage;
using Newtouch.HIS.Domain.IDomainServices.OutpatientManage;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.HIS.Domain.IRepository.OutpatientManage;
using Newtouch.HIS.Domain.ValueObjects.OutpatientManage;
using Newtouch.Infrastructure;
using Newtouch.Tools;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Newtouch.HIS.Web.Areas.OutpatientManage.Controllers
{
    public class OutBookController : ControllerBase
    {
        private readonly IOutBookDmnService _OutBookDmnService;
        private readonly IOutBookRepo _OutBookRepo;
        private readonly IOutBookDateRepo _OutBookDateRepo;
        private readonly IOutBookScheduleRepo _OutBookScheduleRepo;
        private readonly IOutBookScheduleService _OutBookScheduleService;
        private readonly ISysConfigRepo _sysConfigRepo;

        // GET: OutpatientManage/OutBook
        public override ActionResult Index()
        {
            ViewBag.OrganizeId = this.OrganizeId;
            return View();
        }
        public ActionResult RosterCalendar()
        {
            return View();
        }
        public ActionResult RosterCalendarForm()
        {
            return View();
        }
        public ActionResult Doctor()
        {
            return View();
        }

        public ActionResult TimeForm()
        {
            return View();
        }

        public ActionResult Create() {
            return View();
        }
        public ActionResult PatOutBookManage()
        {
            return View();
        }

        public ActionResult GetPagintionList(Pagination pagination,string orgId, string keyword)
        {
            if (orgId == null)
            {
                orgId = OrganizeId;
            }
            var oplist = _OutBookDmnService.GetPagintionList(pagination, orgId,keyword);
            //for (int i = 0; i < oplist.Count; i++)
            //{
            //    var ghList = _OutBookRepo.getStaffByKs(oplist[i].ks);//根据科室获取医生工号列表
            //    oplist[i].ys = getStaffGh(ghList);
            //    oplist[i].ysxm = getStaffName(ghList);//将医生工号列表转换成以逗号分割的医生姓名
            //}
            //for (int i = 0; i < oplist.Count; i++) {
            //    oplist[i].ysxm = getStaffName(oplist[i].ys);
            //}
            var data = new
            {
                rows = oplist,
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(data.ToJson());
        }
        public ActionResult GetStaffList(string orgId) {
            if (orgId == null)
            {
                orgId = OrganizeId;
            }
            var result = _OutBookDmnService.GetStaffList(orgId);
            return Content(result.ToJson());
        }
        public ActionResult GetMzpbZlxmzhList(string zhcode,string keyword) {
            var result = _OutBookDmnService.GetMzpbZlxmzh(zhcode,keyword,OrganizeId);
            return Content(result.ToJson());
        }

        public ActionResult UpdateForm(int keyValue,string ks, string gh) {
            var orgId = OrganizeId;
            var CreateCode=UserIdentity.UserName;
            var CreateTime = DateTime.Now;
            _OutBookRepo.UpdateForm(keyValue, orgId,ks, gh, CreateCode, CreateTime);
            return Success("操作成功。");
        }

        public ActionResult SubmitForm(string ks, List<string> ghList)
        {
            var orgId = OrganizeId;
            var CreateCode = UserIdentity.UserName;
            var CreateTime = DateTime.Now;
            //设置该科室下状态为0
            _OutBookRepo.updateZt(ks, CreateCode, CreateTime,OrganizeId);
            foreach (var gh in ghList)
            {
                //新增 更新
                _OutBookRepo.SubmitForm(orgId, ks, gh, CreateCode, CreateTime);
            }
            return Success("操作成功。");
        }

        public ActionResult getArrangeInfo(int ghpbId) {
            if (ghpbId == 0)
            {
                //新增
                return Content(null);
            }
            else
            {
                var result = _OutBookDmnService.getArrangeInfo(ghpbId,OrganizeId);
                var ghList = _OutBookRepo.getStaffByKs(result.ks,OrganizeId);//根据科室获取医生工号列表
                //if (result != null && result.mjzbz != ((int)EnumOutPatientType.expertOutpat).ToString())
                //{
                //    result.ys = getStaffGh(ghList);//拼接以逗号分割的医生工号
                //    result.ysxm = getStaffName(ghList);//将医生工号列表转换成以逗号分割的医生姓名
                //}
                return Content(result.ToJson());
            }
        }
        public ActionResult getDateInfo(int ghpbId)
        {
            var orgId = OrganizeId;
            var result = _OutBookDmnService.getDateInfo(orgId, ghpbId);
            return Content(result.ToJson());
        }
        public ActionResult getDateTimeInfo(int ghpbId,string timeslot)
        {
            var orgId = OrganizeId;
            var result = _OutBookDmnService.getDateTimeInfo(orgId, ghpbId, timeslot);
            return Content(result.ToJson());
        }
        //将医生工号列表转换成以逗号分割的医生姓名
        public string getStaffName(IList<string> ghList) {
            if (ghList == null) {
                return null;
            }
            var strName = "";
            foreach (var gh in ghList) {
                strName += _OutBookDmnService.getStaffName(gh) + ",";
            }
            if (strName.Contains(","))
            {
                strName = strName.Substring(0, strName.Length - 1);
            }
            return strName;
        }

        //将医生工号列表转换成以逗号分割的字符串
        public string getStaffGh(IList<string> ghList) {
            if (ghList == null)
            {
                return null;
            }
            var strGh = "";
            foreach (var gh in ghList)
            {
                strGh += gh+",";
            }
            if (strGh.Contains(","))
            {
                strGh = strGh.Substring(0, strGh.Length - 1);
            }
            return strGh;
        }
        public ActionResult DeleteArrange(int ghpbId)
        {
            _OutBookDmnService.DeleteArrange(ghpbId, this.OrganizeId);
            return Success("操作成功。");
        }
            public ActionResult SubmitArrange(OutBookArrangeVO entity, int ghpbId)
        {
            var orgId = OrganizeId;
            var User = UserIdentity.UserName;
            var Time = DateTime.Now;
            if (ghpbId == 0)
            {//新增
                //获取排班编号
                var ghpbIdNew = _OutBookDmnService.getghpbId();
                //新增排班信息[mz_ghpb_config]
                _OutBookDmnService.InsertArrange(entity, orgId, User, Time, ghpbIdNew);
                //新增医生关系[mz_ghpb_rel_doc]
                //新增排班时间[mz_ghpb_date]
                for (int i = 1; i <= 7; i++)
                {
                    //新增 更新排班时间 [mz_ghpb_date]
                    _OutBookDateRepo.UpdateDate(entity, ghpbId, i.ToString(), orgId, User, Time, ghpbIdNew);
                }
                return Success("操作成功。");
            }
            else
            {
                //更新信息[mz_ghpb_config]
                _OutBookDmnService.UpdateArrange(entity, ghpbId, orgId, User, Time);
                for (int i = 1; i <= 7; i++)
                {
                    //新增 更新排班时间 [mz_ghpb_date]
                    _OutBookDateRepo.UpdateDate(entity, ghpbId, i.ToString(), orgId, User, Time,0);
                }
                return Success("操作成功。");
            }
        }
        public ActionResult SubmitArrangeADD(OutBookArrangeVO entity, int ghpbId)
        {
            var orgId = OrganizeId;
            var User = UserIdentity.UserName;
            var Time = DateTime.Now;
            var ghpbIdNew = _OutBookDmnService.getghpbId();
            _OutBookDmnService.InsertArrange(entity, orgId, User, Time, ghpbIdNew);
            return Success(ghpbIdNew.ToString());
        }
        /// <summary>
        /// 保存
        /// </summary>
        /// <returns></returns>
        public ActionResult SaveDataPbhy(List<OutBookScheduleEntity> pbList)
        {
            string orgId = this.OrganizeId;
            _OutBookDmnService.SaveDatapb(pbList, orgId);
            return Success();
        }
        /// <summary>
        /// 修改出诊或者停诊状态
        /// </summary>
        /// <returns></returns>
        public ActionResult SaveDatatzcz(Decimal ScheduId,string czzt,string tzyy)
        {
            string orgId = this.OrganizeId;
            _OutBookDmnService.SaveDatatzcz(ScheduId, czzt, orgId,tzyy);
            return Success();
        }
        public ActionResult getDateInfosjd()
        {
            string orgId = OrganizeId;
            IList<string> result = _OutBookDmnService.getDateInfosjd(orgId);
            return Content(result.ToJson());
            //return Content(staffName);
        }
        public ActionResult getDateInfosjdcount(int ghpbId)
        {
            string orgId = OrganizeId;
            IList<string> result = _OutBookDmnService.getDateInfosjdcount(ghpbId,orgId);
            return Content(result.ToJson());
        }

        public ActionResult SubmitghpbTime(string timestr)
        {
            var orgId = OrganizeId;
            var User = UserIdentity.UserName;
            var Time = DateTime.Now;
            for (int i = 0; i < timestr.Split(',').Length; i++)
            {
                _OutBookDmnService.InsertghpbTime(timestr.Split(',')[i].Split('-')[0], timestr.Split(',')[i].Split('-')[1], orgId, User, Time,i+1);
            }
            return Success("操作成功。");
        }

        public ActionResult getStaffByKs(string ks) {
            string orgId = OrganizeId;
            IList<string> result= _OutBookDmnService.getStaffListByKs(ks, orgId);
            return Content(result.ToJson());
            //return Content(staffName);
        }

        public ActionResult CreateSchedule(Pagination pagination,string time) {
           
            var orgId = OrganizeId;
            var Time = Convert.ToDateTime(time);
            //执行存储过程
            string zlxmzhconf = _sysConfigRepo.GetValueByCode("EnableZlxmGroup", orgId);
            if (!string.IsNullOrWhiteSpace(zlxmzhconf) && zlxmzhconf == "1")
            {
                _OutBookScheduleRepo.ExecSchedulebyGroup();
            }
            else
            {
                _OutBookScheduleRepo.ExecSchedule();
            }
                
            //获取列表
            var list= _OutBookScheduleRepo.GetPagintionList(pagination,orgId, Time);
            var oplist = new List<OutBookScheduleVO>();
            for (int i = 0; i < list.Count; i++)
            {
                var ghList = _OutBookRepo.getStaffByKs(list[i].czks,OrganizeId);//根据科室获取医生工号列表
                list[i].ysgh = getStaffGh(ghList);
                list[i].ysxm = getStaffName(ghList);//将医生工号列表转换成以逗号分割的医生姓名
            }

            var data = new
            {
                rows = list,
                total = pagination.total,
                page = pagination.page,
                records = pagination.records
            };
            return Content(data.ToJson());
        }
        public ActionResult GetSchedule(string kssj,string jssj,string ys,string czztcx,string ScheduId,string ks,string lx)
        {

            var orgId = OrganizeId;
            //获取列表
            var list = _OutBookScheduleRepo.GetPagintionListTime(orgId, kssj, jssj, ys, czztcx, ScheduId, ks, lx);
            var oplist = new List<OutBookScheduleVO>();
            var data = new
            {
                rows = list
            };
            return Content(data.ToJson());
        }

        #region HIS 预约/取消预约
        public ActionResult PatBookGh(string cardNo,int ScheduId, string brxz, string Doctor, DateTime OutDate)
        {
            var bookId = _OutBookDmnService.PatBookGh(cardNo,ScheduId, brxz, Doctor, OutDate,this.OrganizeId);
            return Success();
        }

        public ActionResult CancalBook(string BookId)
        {
            var bookId = _OutBookDmnService.CancalBook(BookId);
            return Success();
        }
        #endregion
    }
}