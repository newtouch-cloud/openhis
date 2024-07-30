using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrameworkBase.MultiOrg.Domain.Entity;
using Newtouch.Common.Web;
using Newtouch.HIS.Application.Interface;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.HIS.Domain.ValueObjects;
using Newtouch.Infrastructure;
using Newtouch.PDS.Requset.PharmacyDepartment;
using Newtouch.Tools;

namespace Newtouch.HIS.Application
{
    /// <summary>
    /// 系统药房部门
    /// </summary>
    public class SysPharmacyDepartmentApp : AppBase, ISysPharmacyDepartmentApp
    {
        private readonly ISysMedicineDmnService _sysMedicineDmnService;
        private readonly IPharmacyDmnService _pharmacyDmnService;
        private readonly ISysPharmacyDrugRepo _sysPharmacyDrugRepo;

        public SysPharmacyDepartmentApp(ISysMedicineDmnService sysMedicineDmnService, IPharmacyDmnService pharmacyDmnService, ISysPharmacyDrugRepo sysPharmacyDrugRepo)
        {
            _sysMedicineDmnService = sysMedicineDmnService;
            _pharmacyDmnService = pharmacyDmnService;
            _sysPharmacyDrugRepo = sysPharmacyDrugRepo;
        }

        /// <summary>
        /// 获取该药品授权的组织机构
        /// </summary>
        /// <param name="ypId"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        public List<SysPharmacyDepartmentVEntity> EmpowermentPharmacyDepartmentQuery(string ypId, string organizeId)
        {
            var reqObj = new
            {
                ypId = ypId,
                organizeId = organizeId,
                TimeStamp = DateTime.Now
            };
            var apiResp = SitePdsApiHelper.Request<APIRequestHelper.DefaultResponse>("api/PharmacyDepartment/EmpowermentPharmacyDepartmentQuery", reqObj);
            var response = apiResp.data.ToString();
            return response.ToObject<List<SysPharmacyDepartmentVEntity>>();
        }

        /// <summary>
        /// 授权药房部门
        /// </summary>
        /// <param name="ypId"></param>
        /// <param name="ypCode"></param>
        /// <param name="organizeId"></param>
        /// <param name="userCode"></param>
        /// <param name="epds"></param>
        /// <returns></returns>
        public string SubmitEmpowermentPharmacyDepartment(int? ypId, string ypCode, string organizeId, string userCode, List<string> epds)
        {
            if (epds == null || epds.Count == 0) return "";

            var ypxx = _sysMedicineDmnService.SelectMedicineInfo(ypCode, organizeId);
            if (ypxx == null) return string.Format("授权药房失败，根据药品代码【{0}】未找到药品信息", ypCode);
            var yfbmyp = _pharmacyDmnService.SelectDepartmentMedicine(ypxx.dlCode, organizeId);

            return ypId == null ? NewEpd(yfbmyp, ypxx, epds) : UpdateEpd(yfbmyp, ypxx, epds);
        }

        /// <summary>
        /// 新增药房部门授权
        /// </summary>
        /// <param name="yfbmyp"></param>
        /// <param name="ypxx"></param>
        /// <param name="epds"></param>
        /// <returns></returns>
        private string NewEpd(IList<PharmacyDepartmentOpenMedicineRepoVO> yfbmyp, SysMedicineVO ypxx, List<string> epds)
        {
            var result = new StringBuilder();
            epds.Distinct().ToList().ForEach(p =>
            {
                if (yfbmyp == null || yfbmyp.Count == 0)
                {
                    result.Append(InsertSysPharmacyDrug(p, ypxx));
                }
                else if (yfbmyp.All(o => o.yfbmCode != p))
                {
                    result.Append(InsertSysPharmacyDrug(p, ypxx));
                }
                var reqObj = new EmpowermentPharmacyDepartmentRequestDto
                {
                    bmypId = Guid.NewGuid().ToString(),
                    yfbmCode = p,
                    Ypdm = ypxx.ypCode,
                    OrganizeId = ypxx.OrganizeId,
                    Ypkw = "",
                    Zcxh = "",
                    px = null,
                    Pxfs1 = "",
                    Pxfs2 = "",
                    Kcsx = 0,
                    Kcxx = 0,
                    Jhd = 0,
                    Jhl = 0,
                    Ypsxdm = null,
                    Sysx = 0,
                    Ylsx = 0,
                    zt = "1",
                    CreateTime = DateTime.Now,
                    CreatorCode = ypxx.CreatorCode,
                    LastModifierCode = "",
                    LastModifyTime = null,
                    Timestamp = DateTime.Now
                };
                var apiResp = SitePdsApiHelper.Request<APIRequestHelper.DefaultResponse>("api/PharmacyDepartment/EmpowermentPharmacyDepartment", reqObj);
                var response = apiResp.data.ToString();
                if (!string.IsNullOrWhiteSpace(response)) result.Append(response);
            });
            return result.ToString();
        }

        /// <summary>
        /// 新增药房部门授权
        /// </summary>
        /// <param name="yfbmyp"></param>
        /// <param name="ypxx"></param>
        /// <param name="epds"></param>
        /// <returns></returns>
        private string UpdateEpd(IList<PharmacyDepartmentOpenMedicineRepoVO> yfbmyp, SysMedicineVO ypxx, List<string> epds)
        {
            var result = new StringBuilder();
            var request = new EmpowermentPharmacyDepartmentAndRemoveOldRequestDto
            {
                epds = new List<EmpowermentPharmacyDepartment>(),
                Timestamp = DateTime.Now
            };
            epds.Distinct().ToList().ForEach(p =>
            {
                if (yfbmyp == null || yfbmyp.Count == 0)
                {
                    result.Append(InsertSysPharmacyDrug(p, ypxx));
                }
                else if (yfbmyp.All(o => o.yfbmCode != p))
                {
                    result.Append(InsertSysPharmacyDrug(p, ypxx));
                }
                request.epds.Add(new EmpowermentPharmacyDepartment
                {
                    bmypId = Guid.NewGuid().ToString(),
                    yfbmCode = p,
                    Ypdm = ypxx.ypCode,
                    OrganizeId = ypxx.OrganizeId,
                    Ypkw = "",
                    Zcxh = "",
                    px = null,
                    Pxfs1 = "",
                    Pxfs2 = "",
                    Kcsx = 0,
                    Kcxx = 0,
                    Jhd = 0,
                    Jhl = 0,
                    Ypsxdm = null,
                    Sysx = 0,
                    Ylsx = 0,
                    zt = "1",
                    CreateTime = DateTime.Now,
                    CreatorCode = ypxx.CreatorCode,
                    LastModifierCode = "",
                    LastModifyTime = null
                });
            });
            var apiResp = SitePdsApiHelper.Request<APIRequestHelper.DefaultResponse>("api/PharmacyDepartment/EmpowermentPharmacyDepartmentAndRemoveOld", request);
            var response = apiResp.data.ToString();
            if (!string.IsNullOrWhiteSpace(response)) result.Append(response);
            return result.ToString();
        }

        /// <summary>
        /// 插入药房部门药品
        /// </summary>
        /// <param name="p"></param>
        /// <param name="ypxx"></param>
        /// <returns></returns>
        private string InsertSysPharmacyDrug(string p, SysMedicineVO ypxx)
        {
            var yfbmypEntity = new SysPharmacyDepartmentOpenMedicineEntity
            {
                Id = Guid.NewGuid().ToString(),
                yfbmCode = p,
                dlCode = ypxx.dlCode,
                OrganizeId = OrganizeId,
                CreatorCode = ypxx.CreatorCode,
                CreateTime = DateTime.Now,
                zt = "1",
                px = null
            };
            var r1 = _sysPharmacyDrugRepo.Insert(yfbmypEntity);
            return r1 <= 0 ? string.Format("药房部门{0}赋予药品大类{1}权限失败;", p, ypxx.dlCode) : "";
        }
    }
}