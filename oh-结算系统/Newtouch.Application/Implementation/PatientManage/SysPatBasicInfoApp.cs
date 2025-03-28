using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FrameworkBase.MultiOrg.Application;
using FrameworkBase.MultiOrg.Domain.DBContext.Infrastructure;
using Newtouch.Core.Common.Exceptions;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.HIS.Domain.PrintDto;
using Newtouch.HIS.Domain.ReportTemplateVO;
using Newtouch.HIS.Sett.Request;
using Newtouch.Infrastructure;
using Newtouch.Infrastructure.EF;
using Newtouch.Tools;

namespace Newtouch.HIS.Application
{
    /// <summary>
    /// 
    /// </summary>
    public class SysPatBasicInfoApp : AppBase, ISysPatBasicInfoApp
    {

        private readonly ISysPatientBasicInfoRepo _sysPatBasicInfoRepository;
        private readonly ISysPatientNatureRepo _SysPatiNatureRepo;
        private readonly IPatientBasicInfoDmnService _PatientBasicInfoDmnService;
        private readonly IOutPatientDmnService _outPatientDmnService;
        private readonly IHospMultiDiagnosisRepo _hospMultiDiagnosisRepo;
        //string OrganizeId = OperatorProvider.GetCurrent().OrganizeId;

        /// <summary>
        /// （病历号由系统自动生成）获取最新病历号
        /// </summary>
        /// <returns></returns>
        public string Getblh()
        {
            return _sysPatBasicInfoRepository.Getblh(this.OrganizeId);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public object GetBRXZList(string orgId)
        {
            var list = _SysPatiNatureRepo.IQueryable().Where(p => p.zt == "1" && p.OrganizeId == orgId).Select(p => new
            {
                brxz = p.brxz,
                brxzlb=p.brxzlb,
                brxzbh = p.brxzbh,
                brxzmc = p.brxzmc,
                py = p.py
            }).ToList();
            return list;
        }

        public void PrinZYInfo(ZYInfoVO info)
        {
            var obj = info.MapperTo<ZYInfoVO, PrintZYInfoVO>();
            obj.lxdz = info.xian_sheng + "省（区、市）" + info.xian_shi
                        + "市" + info.xian_xian + "县" + info.xian_dz;
            obj.zd = "";
            if (!string.IsNullOrWhiteSpace(info.ryzd1))
            {
                string[] zdmc1 = info.ryzd1.Split('-');
                obj.zd += zdmc1[1] + ",";
            }
            if (!string.IsNullOrWhiteSpace(info.ryzd2))
            {
                string[] zdmc2 = info.ryzd2.Split('-');
                obj.zd += zdmc2[1] + ",";
            }
            if (!string.IsNullOrWhiteSpace(info.ryzd3))
            {
                string[] zdmc3 = info.ryzd3.Split('-');
                obj.zd += zdmc3[1] + ",";
            }
            if (!string.IsNullOrWhiteSpace(obj.zd))
            {
                obj.zd = obj.zd.Substring(0, obj.zd.Length - 1);
            }
            string fpPath = string.Format(@"{0}\住院信息记录.grf", Constants.ReportTemplateDirUrl);
            //GridppReportHelper.Print<PrintZYInfoVO>(obj, fpPath, null);
        }

        /// <summary>
        /// 根据patid获取病人基本信息
        /// </summary>
        /// <param name="patid"></param>
        /// <returns></returns>
        public SysPatientBasicInfoEntity GetInfoByPatid(string patid)
        {
            return _sysPatBasicInfoRepository.GetInfoByPatid(patid, this.OrganizeId);
        }

        /// <summary>
        /// 打印腕带
        /// </summary>
        /// <param name="info"></param>
        public void PrintWDInfo(WDInfoVO info)
        {
            string fpPath = string.Empty;

            if (info.Flag == "1")
            {
                var obj = info.MapperTo<WDInfoVO, PrintChiWDInfoVO>();
                fpPath = string.Format(@"{0}\儿童标签.grf", Constants.ReportTemplateDirUrl);
                //GridppReportHelper.Print<PrintChiWDInfoVO>(obj, fpPath, info.zyh);
            }
            else if (info.Flag == "2")
            {
                var obj = info.MapperTo<WDInfoVO, PrintAduWDInfoVO>();
                fpPath = string.Format(@"{0}\成人标签.grf", Constants.ReportTemplateDirUrl);
                //GridppReportHelper.Print<PrintAduWDInfoVO>(obj, fpPath, info.zyh);
            }
        }

        /// <summary>
        /// 修改入院诊断
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public void ModifyRyDiagnosis(ModifyRyDiagnosisRequestDTO request)
        {
            if (request == null) throw new FailedException("请求报文不能为空！");
            if (string.IsNullOrWhiteSpace(request.zyh)) throw new FailedException("住院号必填！");
            if (string.IsNullOrWhiteSpace(request.OrganizeId)) throw new FailedException("组织机构不能为空！");
            if (request.RyDiagnosisDetails == null || request.RyDiagnosisDetails.Count == 0) throw new FailedException("诊断内容不能为空！");

            var zdEntities = _hospMultiDiagnosisRepo.SelectData(request.zyh, request.OrganizeId);
            var locker = new object();
            var operateTime = DateTime.Now;
            using (var db = new EFDbTransaction(new DefaultDatabaseFactory()).BeginTrans())
            {
                if (zdEntities != null && zdEntities.Count > 0)
                {
                    //作废原诊断
                    zdEntities.ForEach(p =>
                    {
                        p.zt = "0";
                        p.LastModifyTime = operateTime;
                        p.LastModifierCode = request.userCode;
                        db.Update(p);
                    });
                }
                //新增
                var li = new List<HospMultiDiagnosisEntity>();
                Parallel.ForEach(request.RyDiagnosisDetails, p =>
                {
                    if (string.IsNullOrWhiteSpace(p.ryzddm) || string.IsNullOrWhiteSpace(p.ryzdmc)) return;
                    var item = new HospMultiDiagnosisEntity
                    {
                        rydzdId = Guid.NewGuid().ToString(),
                        OrganizeId = request.OrganizeId,
                        zyh = request.zyh,
                        zdCode = p.ryzddm,
                        zdmc = p.ryzdmc,
                        zdpx = p.zdpx,
                        icd10 = p.ryzdICD10,
                        CreateTime = operateTime,
                        CreatorCode = request.userCode,
                        zt = "1",
                        px = null
                    };
                    lock (locker)
                    {
                        li.Add(item);
                    }
                });
                db.Insert(li);
                db.Commit();
            }
        }
    }
}
