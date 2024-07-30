using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrameworkBase.MultiOrg.Domain.DBContext.Infrastructure;
using Newtouch.Core.Common.Exceptions;
using Newtouch.HIS.Domain.DO;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.HIS.DomainServices;
using Newtouch.Infrastructure;
using Newtouch.PDS.Requset;
using Newtouch.PDS.Requset.ResourcesOperate;

namespace Newtouch.HIS.Application.Implementation.Process
{
    /// <summary>
    /// 门诊排药V2.0 2019-09-18
    /// </summary>
    public class OutpatientBookAppV2 : ProcessorFun<OutpatientBookRequestDTO>
    {
        private List<MzCfEntity> mzcfxx = new List<MzCfEntity>();
        private List<MzCfmxEntity> mzcfmx = new List<MzCfmxEntity>();
        private readonly IPyDmnService _pyDmnService;
        private readonly IDispensingDmnService _dispensingDmnService;

        public OutpatientBookAppV2(OutpatientBookRequestDTO request) : base(request)
        {
        }

        /// <summary>
        /// 验证
        /// </summary>
        /// <returns></returns>
        protected override ActResult Validata()
        {
            if (Request.Items == null || Request.Items.Count == 0) throw new FailedException("项目/药品不能为空");
            if (string.IsNullOrWhiteSpace(Request.OrganizeId)) throw new FailedException("OrganizeId(组织结构代码) 必传");
            try
            {
                Parallel.ForEach(Request.Items, p =>
                {
                    if (string.IsNullOrWhiteSpace(p.ItemCode)) throw new FailedException("ItemCode(项目编号) 必传。");
                    if (string.IsNullOrWhiteSpace(p.Cfh)) throw new FailedException("Cfh(处方号) 必填。");
                    if (string.IsNullOrWhiteSpace(p.CardNo)) throw new FailedException("CardNo(卡号) 必填。");
                    var rpCount = new fyDmnService(new DefaultDatabaseFactory(), false).ExistEffectiveRp(p.Cfh, p.Yfbm, Request.OrganizeId);
                    if (rpCount > 0) throw new Exception("已存在处方【" + p.Cfh + "】，不可重复提交。");
                    var ypxx = new SysMedicineExDmnService(new DefaultDatabaseFactory(), false).GetYpDetails(Request.OrganizeId,
                        p.ItemCode);
                    if (ypxx == null) throw new Exception("未找到项目【" + (string.IsNullOrWhiteSpace(p.ItemName) ? p.ItemCode : p.ItemName) + "】。");
                    p.ItemName = string.IsNullOrWhiteSpace(p.ItemName) ? ypxx.ypmc : p.ItemName;
                    if (p.Zhyz <= 0)
                    {
                        if (ypxx.mzcls == null) throw new Exception("项目【" + p.ItemName + "】门诊拆零数为空。");
                        p.Zhyz = (int)ypxx.mzcls;
                    }

                    p.ItemSpecifications = string.IsNullOrWhiteSpace(p.ItemSpecifications) ? ypxx.ypgg : p.ItemSpecifications;
                    p.ItemManufacturer = string.IsNullOrWhiteSpace(p.ItemManufacturer) ? ypxx.ycmc : p.ItemManufacturer;
                    p.Dosage = p.Dosage ?? 0;
                });
            }
            catch (Exception e)
            {
                throw new FailedException(e.Message + (e.InnerException != null ? e.InnerException.Message : ""));
            }
            return new ActResult();
        }

        /// <summary>
        /// 预处理 组装mz_cf和mz_cfmx数据
        /// </summary>
        /// <param name="actResult"></param>
        protected override void BeforeAction(ActResult actResult)
        {
            Parallel.For(0, 2, i => //组装数据
            {
                switch (i)
                {
                    case 0:
                        mzcfxx = AssembleMzCfEntity();
                        break;
                    case 1:
                        mzcfmx = AssembleMzCfmxEntity();
                        break;
                }
            });
        }

        protected override void Action(ActResult actResult)
        {
            var errorMsg = new StringBuilder();
            errorMsg.Append(_pyDmnService.InsertOutpatientRpInfo(mzcfxx, mzcfmx));
            if (!string.IsNullOrWhiteSpace(errorMsg.ToString()))
            {
                actResult.IsSucceed = false;
                actResult.ResultMsg = errorMsg.ToString();
                return;
            }

            foreach (var p in mzcfxx)
            {
                var bookRequest = new BookItemDo
                {
                    Yfbm = p.lyyf,
                    Cfh = p.cfh,
                    OrganizeId = p.OrganizeId,
                    CreatorCode = p.CreatorCode
                };
                errorMsg.Append(_dispensingDmnService.OutPatientBookV2(bookRequest));
                if (!string.IsNullOrWhiteSpace(errorMsg.ToString())) _pyDmnService.DeleteRpInfo(p.cfh, p.OrganizeId, p.lyyf);
            }

            if (string.IsNullOrWhiteSpace(errorMsg.ToString())) return;
            actResult.IsSucceed = false;
            actResult.ResultMsg = errorMsg.ToString();
        }

        /// <summary>
        /// 组装门诊处方信息
        /// </summary>
        /// <returns></returns>
        private List<MzCfEntity> AssembleMzCfEntity()
        {
            var cfhs = Request.Items.Select(p => p.Cfh).Distinct();
            return (from cfh in cfhs
                    let cfxx = Request.Items.Find(p => p.Cfh == cfh)
                    select new MzCfEntity
                    {
                        cfh = cfh,
                        jsnm = 0,
                        CardNo = cfxx.CardNo ?? "",
                        xm = cfxx.xm ?? "",
                        Fph = "",
                        nl = cfxx.nl,
                        brxzmc = cfxx.brxzmc,
                        ysmc = cfxx.ysmc,
                        ksmc = cfxx.ksmc,
                        je = Request.Items.FindAll(p => p.Cfh == cfh).Sum(s => s.ItemCount * s.ItemUnitPrice),
                        OrganizeId = Request.OrganizeId,
                        lyyf = cfxx.Yfbm,
                        fybz = ((int)EnumFybz.Yp).ToString(),
                        zt = "1",
                        CreateTime = DateTime.Now,
                        CreatorCode = Request.CreatorCode
                    }).ToList();
        }

        /// <summary>
        /// 组装门诊处方明细信息
        /// </summary>
        /// <returns></returns>
        private List<MzCfmxEntity> AssembleMzCfmxEntity()
        {
            var result = new List<MzCfmxEntity>();
            Request.Items.ForEach(p =>
            {
                result.Add(new MzCfmxEntity
                {
                    cfh = p.Cfh,
                    ypCode = p.ItemCode,
                    ypmc = p.ItemName ?? "",
                    gg = p.ItemSpecifications,
                    sl = (int)p.ItemCount,
                    dw = p.ItemUnitName ?? "",
                    dj = p.ItemUnitPrice / p.Zhyz,
                    ycmc = p.ItemManufacturer,
                    je = p.ItemCount * p.ItemUnitPrice,
                    jl = p.Dosage,
                    jldw = p.DosageUnit,
                    yfmc = p.UsageName,
                    bz = p.Remark,
                    zhyz = p.Zhyz,
                    czh = p.GroupNum,
                    OrganizeId = Request.OrganizeId,
                    CreatorCode = Request.CreatorCode,
                    CreateTime = DateTime.Now,
                    zt = "1"
                });
            });
            return result;
        }
    }
}