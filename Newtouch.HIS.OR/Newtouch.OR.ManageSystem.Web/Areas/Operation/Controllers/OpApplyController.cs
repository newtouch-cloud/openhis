using FrameworkBase.MultiOrg.Web;
using Newtouch.Core.Common;
using Newtouch.Core.Common.Exceptions;
using Newtouch.OR.ManageSystem.Domain;
using Newtouch.OR.ManageSystem.Domain.DTO;
using Newtouch.OR.ManageSystem.Domain.DTO.InputDto;
using Newtouch.OR.ManageSystem.Domain.Entity;
using Newtouch.OR.ManageSystem.Domain.IDomainServices;
using Newtouch.OR.ManageSystem.Domain.IRepository;
using Newtouch.OR.ManageSystem.Domain.ValueObjects;
using Newtouch.OR.ManageSystem.Infrastructure;
using Newtouch.Tools;
using System.Data;
using System.Web.Mvc;

namespace Newtouch.OR.ManageSystem.Web.Areas.Operation.Controllers
{
    public class OpApplyController : OrgControllerBase
    {
        //private readonly ISysDepartmentRepo _SysDepartmentRepo;
        //private readonly ISysStaffRepo _SysStaffRepo;      
        private readonly IOPApplyDmnService _applyDmnService;
        private readonly ICommonDmnService _CommonDmnService;
        private readonly IORApplyInfoRepo _ORApplyInfoRepo;
        private readonly IORApplyInfoExpandRepo _ORApplyInfoExpandRepo;
        private readonly IORStaffRepo _ORStaffRepo;
        private readonly ITemporary_ordersERepo _ITemporary_ordersERepo;
        // GET: Operation/OpApply
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 手术申请列表
        /// </summary>
        /// <returns></returns>
        public ActionResult ApplyCard()
        {
            return View();
        }
        /// <summary>
        /// 手术申请明细
        /// </summary>
        /// <returns></returns>
        public ActionResult ApplyOPDetail()
        {
            return View();
        }
        public ActionResult ApplyIndex()
        {
            return View();
        }
        public ActionResult OpApplyCard()
        {
            return View();
        }

        public ActionResult Getbqlist(string bqcode)
        {
            var bq = _CommonDmnService.GetBqlist(bqcode, OrganizeId, null);
            return Content(bq.ToJson());
        }

        public ActionResult GetOplist(string ssdm)
        {
            var op = _CommonDmnService.GetOplist(ssdm, OrganizeId);
            return Content(op.ToJson());
        }

        public ActionResult GetStafflist(string rygh)
        {
            var ys = _CommonDmnService.GetStafflist(rygh, OrganizeId);
            return Content(ys.ToJson());
        }
        public ActionResult GetAneslist(string code)
        {
            var ys = _CommonDmnService.GetAneslistlist(code, OrganizeId);
            return Content(ys.ToJson());
        }

        public ActionResult GetPatGridList(Pagination pagination, string keyword, string bq)
        {
            var pat = _applyDmnService.GetPatList(keyword, null, bq, this.UserIdentity.rygh, OrganizeId, 1);

            var data = new
            {
                rows = pat,
                total = pat.Count,
                page = 1,
                records = pat.Count
            };
            return Content(data.ToJson());
        }
        [HttpPost]
        public ActionResult GetPatGridList(PagingData<QueryBaseDTO> paging)
        {
            #region 测试数据
            //object[] pat = new[]
            //{ new{
            //     ID = 1,
            //    hzxm = "张三",
            //    zyh = "001",
            //    ryzd = "阑尾炎",
            //    rysj="2019-10-1",
            //    sqzt="未申请"
            //    },
            //   new{
            //     ID = 2,
            //    hzxm = "张四",
            //    zyh = "002",
            //    ryzd = "肝硬化",
            //    rysj="2019-10-5",
            //    sqzt="已审核"
            //    }
            //};
            #endregion

            var pat = _applyDmnService.GetPatList(paging.search ?? paging.queryParams.keyword, null, paging.queryParams.bq, this.UserIdentity.rygh, OrganizeId, 1);

            var data = new
            {
                rows = pat,
                total = pat.Count,
                //page = pagination.page,
                //records = pagination.records
            };
            return Content(data.ToJson());
        }
        [HttpPost]
        public ActionResult GetPatInfo(QueryBaseDTO request)
        {
            var pat = _applyDmnService.GetPatList(request.keyword, request.zyh, request.bq, this.UserIdentity.rygh, OrganizeId, 1);
            return Content(pat.Count > 0 ? pat[0].ToJson() : "");
        }

        public ActionResult GetGridListOP(string zyh, string sqlx, Pagination pagination)
        {
            if (string.IsNullOrWhiteSpace(zyh))
            {
                return Content("");
            }
            var oplist = _applyDmnService.GetOpApplybyzyh(zyh, sqlx, OrganizeId);



            var data = new
            {
                rows = oplist,
                page = 1,
                records = 1,
                total = oplist.Count
            };
            return Content(data.ToJson());
        }
        [HttpPost]
        public ActionResult GetGridListOP(PagingData<QueryBaseDTO> reqdata)
        {
            #region 测试数据
            //object[] oplist = new[]
            //{ new{
            //     ID = 1,
            //    ssmc = "阑尾切除术1",
            //    sssj = "2019-10-1 10:00",
            //    zdys = "医生1",
            //    sszt="已审核"
            //    },
            //   new{
            //    ID = 2,
            //    ssmc = "阑尾切除术2",
            //    sssj = "2019-10-1 15:30",
            //    zdys = "医生1",
            //    sszt="待审核"
            //    }
            //};
            #endregion

            if (string.IsNullOrWhiteSpace(reqdata.queryParams.zyh))
            {
                return Content("");
            }
            var oplist = _applyDmnService.GetOpApplybyzyh(reqdata.queryParams.zyh, reqdata.queryParams.sqlx, OrganizeId);



            var data = new
            {
                rows = oplist,
                total = oplist.Count
            };
            return Content(data.ToJson());
        }

        /// <summary>
        /// 获取申请详情实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public ActionResult GetFormJson(string keyValue)
        {
            if (string.IsNullOrWhiteSpace(keyValue))
            {
                return Content(new OpApplySubmitDTO().ToJson());
            }
            var entity = _applyDmnService.GetOpApplybyApplyNo(keyValue, OrganizeId);
            return Content(entity.ToJson());
        }
        public ActionResult SubmitForm(OpApplySubmitDTO entity, string keyValue)
        {
            if (string.IsNullOrWhiteSpace(entity.zyh))
            {
                throw new FailedException("患者信息不可为空");
            }
            entity.staffgh = this.UserIdentity.rygh;
            if (string.IsNullOrWhiteSpace(entity.OrganizeId))
            {
                entity.OrganizeId = OrganizeId;
            }
            //提交申请信息
            string applyno = _applyDmnService.SubmitApplyForm(entity, keyValue);
            return Success("申请成功", new { applyno });
        }
        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public ActionResult SubmitForm1(OpApplyDTO entity, string keyValue, string datas)
        {
            if (entity != null)
            {
                //提交申请信息
                string applyno = _applyDmnService.SubmitApplyForm(entity, keyValue, datas);

                //清空手术辅助
                if (keyValue != "")
                {
                    DeleteApplyExtend(keyValue);
                }

                ////添加手术辅助
                for (var i = 0; i < entity.ss.Count; i++)
                {
                    var ssObj = new ORApplyInfoExpandEntity();
                    ssObj.OrganizeId = entity.organizeid;
                    ssObj.Applyno = applyno;
                    ssObj.zyh = entity.sq_zyh;
                    ssObj.ssmc = entity.ss[i].ssmc;
                    ssObj.ssdm = entity.ss[i].ssdm;
                    ssObj.px = i + 1;
                    _ORApplyInfoExpandRepo.SubmitForm(ssObj, null);
                }

                return Success($"申请成功，手术申请编号：{applyno}", new { applyno });
            }

            return Error("信息不完整，申请失败。");


        }
        /// <summary>
        /// 撤销申请
        /// </summary>
        /// <param name="keyValue">手术申请编号</param>
        /// <returns></returns>
        public ActionResult DeleteApply(string keyValue, string ssmcn)
        {
            var ety = _ORApplyInfoRepo.FindEntity(p => p.Applyno == keyValue && p.OrganizeId == OrganizeId && p.zt == "1");
            if (ety == null)
            {
                return Error("未找到有效手术申请");
            }
            ety.sqzt = ((int)EnumSqzt.yqx).ToString();
            ety.Modify(ety.Id);
            _ORApplyInfoRepo.Update(ety);
            return Success("撤销申请操作成功");
        }

        /// <summary>
        /// 删除手术辅助
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public ActionResult DeleteApplyExtend(string keyValue)
        {
            _ORApplyInfoExpandRepo.DeleteForm(keyValue, OrganizeId);
            return Success();
        }
    }
}