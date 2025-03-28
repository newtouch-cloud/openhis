using FrameworkBase.MultiOrg.Domain.IRepository;
using FrameworkBase.MultiOrg.Web;
using Newtouch.Core.Common;
using Newtouch.OR.ManageSystem.Domain;
using Newtouch.OR.ManageSystem.Domain.DTO;
using Newtouch.OR.ManageSystem.Domain.DTO.InputDto;
using Newtouch.OR.ManageSystem.Domain.Entity;
using Newtouch.OR.ManageSystem.Domain.IDomainServices;
using Newtouch.OR.ManageSystem.Domain.IRepository;
using Newtouch.OR.ManageSystem.Domain.ValueObjects;
using Newtouch.OR.ManageSystem.Infrastructure;
using Newtouch.OR.ManageSystem.Repository;
using Newtouch.Tools;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Newtouch.OR.ManageSystem.Web.Areas.Operation.Controllers
{
    public class OpArrangementController : OrgControllerBase
    {
        private readonly IOpArrangementDmnService _OpArrangementDmnService;
        private readonly IORArrangementRepo _ORArrangementRepo;
        private readonly IORApplyInfoRepo _ORApplyInfoRepo;
        private readonly IORApplyInfoExpandRepo _ORApplyInfoExpandRepo;
        private readonly ISysStaffRepo _sysStaff;

        // GET: Operation/OpArrangement
        public ActionResult Index()
        {
            return View();
        }
        //手术排班安排
        public ActionResult ArrangementDetail()
        {
            return View();
        }
        public ActionResult OperationProcDetail()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GetGridList(PagingData<QueryArrangeDto> reqdata)
        {
            var dto = reqdata.queryParams;
            if (dto.ksrq != null)
            {
                dto.ksrq = string.Format("{0:yyyy-MM-dd HH:mm:ss.fff}", dto.ksrq).ToDate();
            }
            if (dto.jsrq != null)
            {
                dto.jsrq = string.Format("{0:yyyy-MM-dd HH:mm:ss.fff}", dto.jsrq).ToDate();
            }
            if (string.IsNullOrWhiteSpace(dto.orgId))
            {
                dto.orgId = OrganizeId;
            }
            var list = _OpArrangementDmnService.GetArrangeList(dto);

            var data = new
            {
                rows = list,
                total = list.Count
            };
            return Content(data.ToJson());
        }
        public ActionResult GetGridList(Pagination pagination, QueryArrangeDto dto)
        {
            #region 测试数据
            //object[] list = new[]{
            //    new{
            //        applyno="ss0000001",
            //        oparrangeno="20191001002",
            //        xm="张三",
            //        xb="男",
            //        nl="26",
            //        zyh="001",
            //        patid="12345",
            //        bq="一病区",
            //        ch="C098",
            //        rysj="2019-10-1",
            //        ryzd="阑尾炎",
            //        sqssrq="2019-10-1 10:00",
            //        sqssmc="阑尾切除术1",
            //        sqzdys="医生1",
            //        sqzt="已审核",
            //        sssj="2019-10-1 10:00",
            //        ssmc="阑尾切除术1",
            //        zdys="医生1",
            //        yszs1="ys01",
            //        yszs2="ys02",
            //        xshs="hs01",  //洗手护士
            //        xhhs="hs02",  //巡回护士
            //        ssroom="手术室1",
            //        ssno="2"
            //    },
            //    new{
            //        applyno="ss0000002",
            //        oparrangeno="",
            //        xm="张三",
            //        xb="男",
            //        nl="26",
            //        zyh="001",
            //        patid="12345",
            //        bq="一病区",
            //        ch="C098",
            //        rysj="2019-10-1",
            //        ryzd="阑尾炎",
            //        sqssrq="2019-10-1 15:30",
            //        sqssmc="阑尾切除术2",
            //        sqzdys="医生1",
            //        sqzt="待审核",
            //        sssj="",
            //        ssmc="",
            //        zdys="",
            //        yszs1="",
            //        yszs2="",
            //        xshs="",  //洗手护士
            //        xhhs="",  //巡回护士
            //        ssroom="",
            //        ssno=""
            //    }
            //};
            //return Content(list.ToJson());
            #endregion

            //时间格式转换
            if (dto.ksrq != null)
            {
                dto.ksrq = string.Format("{0:yyyy-MM-dd HH:mm:ss.fff}", dto.ksrq).ToDate();
            }
            if (dto.jsrq != null)
            {
                dto.jsrq = string.Format("{0:yyyy-MM-dd HH:mm:ss.fff}", dto.jsrq).ToDate();
            }
            if (string.IsNullOrWhiteSpace(dto.orgId))
            {
                dto.orgId = OrganizeId;
            }
            var list = _OpArrangementDmnService.GetArrangeList(dto);

            var data = new
            {
                rows = list,
                total = list.Count,
                page = 1,
                records = list.Count
            };
            return Content(data.ToJson());
        }

        public ActionResult submitForm(ORArrangementEntity entity, string keyValue)
        {
            if (string.IsNullOrWhiteSpace(entity.OrganizeId))
            {
                entity.OrganizeId = OrganizeId;
            }
            var ApplyId = keyValue;
            //申请编号转换为手术安排Id
            var ArrangeId = getArrangeIdByApplyId(ApplyId);
            //安排表申请状态改为已审核
            _ORApplyInfoRepo.UpdateSqzt(ApplyId, "2");
            //申请表申请状态改为已排班
            _ORArrangementRepo.SubmitForm(entity, ApplyId, ArrangeId);
            return Success("操作成功。");
        }

        public string getArrangeIdByApplyId(string ApplyId)
        {
            var result = _OpArrangementDmnService.getArrangeIdByApplyId(ApplyId, OrganizeId);
            return result;
        }

        public ActionResult GetArrangeObjByApplyId(string ApplyId)
        {
            var result = _OpArrangementDmnService.GetArrangeObjByApplyId(ApplyId, OrganizeId);
            if (!string.IsNullOrWhiteSpace(result.zlys1) && string.IsNullOrWhiteSpace(result.zlys1name))
            {
                result.zlys1name = _sysStaff.GetNameByGh(result.zlys1, OrganizeId);
            }
            if (!string.IsNullOrWhiteSpace(result.zlys2) && string.IsNullOrWhiteSpace(result.zlys2name))
            {
                result.zlys2name = _sysStaff.GetNameByGh(result.zlys2, OrganizeId);
            }
            if (!string.IsNullOrWhiteSpace(result.xshs) && string.IsNullOrWhiteSpace(result.xshsname))
            {
                result.xshsname = _sysStaff.GetNameByGh(result.xshs, OrganizeId);
            }
            if (!string.IsNullOrWhiteSpace(result.xhhs) && string.IsNullOrWhiteSpace(result.xhhsname))
            {
                result.xhhsname = _sysStaff.GetNameByGh(result.xhhs, OrganizeId);
            }
            return Content(result.ToJson());
        }
        /// <summary>
        /// 获取手术申请详情(手术排班页)
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public ActionResult GetApplyInfoByKey(string keyValue)
        {
            ApplyVO applyVO = new ApplyVO();
            var entity = _OpArrangementDmnService.GetApplyInfoByKey(keyValue, OrganizeId);
            if (entity.sqzt == null)
            {
                entity.sqzt = "1";

            }
            entity.sqzt = ((EnumSqzt)int.Parse(entity.sqzt)).GetDescription();

            applyVO.AnesCode = entity.AnesCode;
            applyVO.Applyno = entity.Applyno;
            applyVO.bq = entity.bq;
            applyVO.ch = entity.ch;
            applyVO.CreateTime = entity.CreateTime;
            applyVO.CreatorCode = entity.CreatorCode;
            applyVO.csrq = entity.csrq;
            applyVO.Id = entity.Id;
            applyVO.isgl = entity.isgl;
            applyVO.ks = entity.ks;
            applyVO.LastModifierCode = entity.LastModifierCode;
            applyVO.LastModifyTime = entity.LastModifyTime;
            applyVO.mzys = entity.mzys;
            applyVO.OrganizeId = entity.OrganizeId;
            applyVO.ryrq = entity.ryrq;
            applyVO.sfz = entity.sfz;
            applyVO.sqzt = entity.sqzt;
            applyVO.ss = _ORApplyInfoExpandRepo.getApplyExtendByApplyno(OrganizeId, entity.Applyno);
            applyVO.ssbw = entity.ssbw;
            applyVO.sssj = entity.sssj;
            applyVO.xb = entity.xb;
            applyVO.xm = entity.xm;
            applyVO.ysgh = entity.ysgh;
            applyVO.ysxm = entity.ysxm;
            applyVO.zd = entity.zd;
            applyVO.zt = entity.zt;
            applyVO.zyh = entity.zyh;
            applyVO.ssmcn = entity.ssmcn;
            applyVO.ssdm = entity.ssdm;
            return Content(applyVO.ToJson());
        }

        public ActionResult DeleteForm(string keyValue)
        {
            //更新排班手术状态
            var ArrangeId = getArrangeIdByApplyId(keyValue);
            _ORArrangementRepo.DeleteForm(ArrangeId);
            //取消排班更新手术申请状态(1.已申请)
            _ORApplyInfoRepo.UpdateSqzt(keyValue, "1");
            return Success("操作成功。");
        }
    }
}