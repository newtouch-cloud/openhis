using System;
using System.Collections.Generic;
using System.Text;
using FrameworkBase.MultiOrg.Domain.DBContext.Infrastructure;
using Newtouch.Core.Common.Exceptions;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.HIS.DomainServices;
using Newtouch.Infrastructure;
using Newtouch.Infrastructure.ExException;
using Newtouch.PDS.Requset;
using Newtouch.PDS.Requset.Zyypyz;

namespace Newtouch.HIS.Application.Implementation.Process
{
    /// <summary>
    /// 住院预定资源（冻结库存）
    /// </summary>
    public class YzzxAppV2 : ProcessorFun<ZyypyzzxRequest>
    {
        private List<ZyYpyzxxEntity> _yzEntities = new List<ZyYpyzxxEntity>();
        private string zxId = Guid.NewGuid().ToString();//医嘱执行ID

        private Dictionary<string, FrameworkBase.MultiOrg.Domain.Entity.SysMedicineComplexVEntity> 
            _ypDict = new Dictionary<string, FrameworkBase.MultiOrg.Domain.Entity.SysMedicineComplexVEntity>();

        private readonly IZyYpyzxxRepo _zyYpyzzxRepo;
        private readonly IDispensingDmnService _dispensingDmnService;

        public YzzxAppV2(ZyypyzzxRequest request) : base(request)
        {
        }

        /// <summary>
        /// 验证
        /// </summary>
        /// <returns></returns>
        protected override ActResult Validata()
        {
            if (Request == null) throw new FailedException("请求报文不能我空");
            if (Request.yzList == null || Request.yzList.Count <= 0) throw new FailedException("医嘱列表不能为空");
            if (string.IsNullOrWhiteSpace(Request.OrganizeId)) throw new FailedException("组织机构ID不能为空");
            try
            {
                foreach(var detail in Request.yzList)
                {
                    if (string.IsNullOrWhiteSpace(detail.ypCode)) throw new Exception("请传入有效的药品代码");
                    if (string.IsNullOrWhiteSpace(detail.yzId)) throw new Exception("请传入有效的医嘱ID");
                    if (string.IsNullOrWhiteSpace(detail.bqCode)) throw new Exception("请传入有效的病区编码");
                    if (string.IsNullOrWhiteSpace(detail.fyyf)) throw new Exception("请传入有效的发药药房编码");
                    if (detail.sl <= 0) throw new Exception("请输入有效的医嘱执行数量");

                    var ypxx = GetYpDetails(Request.OrganizeId, detail.ypCode);
                    if (ypxx == null) throw new Exception("未找到药品【" + detail.ypCode + "】。");
                    detail.ypmc = ypxx.ypmc;
                    if (detail.zhyz <= 0)
                    {
                        if (ypxx.zycls == null) throw new Exception("药品【" + ypxx.ypmc + "】住院拆零数为空。");
                        detail.zhyz = (int)ypxx.zycls;
                    }

                    detail.dj = ypxx.lsj / ypxx.bzs * detail.zhyz;
                    detail.je = detail.dj * detail.sl;
                }
            }
            catch (Exception e)
            {
                throw new FailedException("", e.Message + (e.InnerException != null ? e.InnerException.Message : ""));
            }
            return new ActResult();
        }

        protected override void BeforeAction(ActResult actResult)
        {
            foreach (var p in Request.yzList)
            {
                decimal p_ts = 0m;
                if (p.ts == null)
                {
                    p_ts = 0;
                }
                else
                {
                    p_ts = (decimal)p.ts;
                }

                var item = new ZyYpyzxxEntity
                {
                    bqCode = p.bqCode,
                    CreatorCode = p.yzzxsqr,
                    CreateTime = DateTime.Now,
                    cw = p.cw,
                    dj = p.dj,
                    dl = p.dl,
                    fybz = ((int)EnumFybz.Yp).ToString(),
                    fyyf = p.fyyf,
                    je = p.je,
                    jsrq = p.jsrq < Convert.ToDateTime("1971-01-01") ? Constants.MinDateTime : p.jsrq,
                    ksCode = p.ksCode,
                    ksrq = p.ksrq < Convert.ToDateTime("1971-01-01") ? Constants.MinDateTime : p.ksrq,
                    lyxh = p.lyxh,
                    OrganizeId = Request.OrganizeId,
                    pcmc = p.pcmc,
                    sjap = p.sjap,
                    sl = p.sl,
                    zhyz = p.zhyz,
                    yl = p.yl,
                    yldw = p.yldw,
                    ypCode = p.ypCode,
                    ysgh = p.ysgh,
                    yzbz = p.yzbz,
                    yzxz = p.yzxz,
                    yzzxsqr = p.yzzxsqr,
                    zfbl = p.zfbl,
                    zfxz = p.zfxz,
                    zlff = p.zlff,
                    zxsl = p.zxsl,
                    zyh = p.zyh,
                    patientName = p.patientName,
                    zxrq = p.zxrq < Convert.ToDateTime("1971-01-01") ? Constants.MinDateTime : p.zxrq,
                    zh = p.zh,
                    zxId = zxId,
                    yzId = p.yzId,
                    ts = p_ts,
                    yzlx = p.yzlx
                };

                _yzEntities.Add(item);
            }
        }

        protected override void Action(ActResult actResult)
        {
            if (_yzEntities == null || _yzEntities.Count <= 0) throw new ProcessException("收集住院药品医嘱执行数据失败。");

            int t = 0;
            try
            {
                t = _zyYpyzzxRepo.Insert(_yzEntities);
            }
            catch(Exception ex)
            {
                actResult.IsSucceed = false;
                actResult.ResultMsg = "保存住院药品医嘱执行数据异常：" + ex.Message;
                return;
            }
            if (t <= 0)
            {
                actResult.IsSucceed = false;
                actResult.ResultMsg = "保存住院药品医嘱执行数据失败：无成功记录数。";
                return;
            }

            var successList = new List<ZyYpyzxxEntity>();
            var errorMsg = new StringBuilder();
            foreach (var p in _yzEntities)
            {
                string phResult = "";
                try
                {
                    phResult = _dispensingDmnService.HospitalizationBook(p.yzId, p.zxId, Request.OrganizeId, p.fyyf, p.yzzxsqr, p.patientName);
                }
                catch(Exception ex)
                {
                    phResult = "HospitalizationBook异常：" + ex.Message;
                }
                if (!string.IsNullOrWhiteSpace(phResult))
                {
                    errorMsg.Append(phResult + "; ");
                    CancelForzen(successList);
                    break;
                }
                successList.Add(p);
            }

            if (string.IsNullOrWhiteSpace(errorMsg.ToString()))
            {
                actResult.Data = Request.yzList;//由于事务控制，所以要么全部失败，否则全部成功，Data返回成功队列
                return;
            }

            _zyYpyzzxRepo.DeleteByZxId(zxId);

            actResult.IsSucceed = false;
            actResult.ResultMsg = errorMsg.ToString();
        }

        /// <summary>
        /// 重写父类方法(空实现)
        /// </summary>
        /// <param name="result"></param>
        protected override void AsyncProcess(ActResult result)
        {
        }

        /// <summary>
        /// 重写父类方法(释放内存)
        /// </summary>
        /// <param name="result"></param>
        protected override void Dispose(ActResult result)
        {
            _ypDict.Clear();
        }

        /// <summary>
        /// 取消冻结，删除医嘱明细
        /// </summary>
        /// <param name="tg"></param>
        private void CancelForzen(List<ZyYpyzxxEntity> tg)
        {
            foreach (var p in tg)
            {
                var t = _dispensingDmnService.CancelForzenAndDeleteYz(p.yzId, p.zxId);
            }
        }

        /// <summary>
        /// 获取药品信息(基于缓存)
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="ypCode"></param>
        /// <returns></returns>
        private FrameworkBase.MultiOrg.Domain.Entity.SysMedicineComplexVEntity GetYpDetails(string orgId, string ypCode)
        {
            if (_ypDict.ContainsKey(ypCode))
            {
                return _ypDict[ypCode];
            }

            var ypxx = new SysMedicineExDmnService(new DefaultDatabaseFactory(), false).GetYpDetails(orgId, ypCode);
            if (ypxx != null)
            {
                _ypDict[ypCode] = ypxx;
            }

            return ypxx;
        }
    }
}