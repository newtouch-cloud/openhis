using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FrameworkBase.MultiOrg.Application;
using Newtouch.Common.Operator;
using Newtouch.HIS.Application.Interface;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.HIS.Domain.IDomainServices.PharmacyDrugStorage;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.HIS.Domain.ValueObjects.PharmacyDrugStorage;
using Newtouch.PDS.Requset.PharmacyDepartment;
using Newtouch.Tools;

namespace Newtouch.HIS.Application.Implementation
{
    /// <summary>
    /// 
    /// </summary>
    public class SysPharmacyDepartmentApp : AppBase, ISysPharmacyDepartmentApp
    {
        private readonly IPharmacyDrugStorageDmnService _pharmacyDrugStorageDmnService;
        private readonly ISysPharmacyDepartmentMedicineRepo _sysPharmacyDepartmentMedicineRepo;

        /// <summary>
        /// 根据药房代码获取药库List（药房可向目标药库申领）
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="yfbmCode"></param>
        /// <param name="organizeId"></param>
        /// <returns></returns>
        public IList<SysPharmacyDepartmentVEntity> GetTheUpperYkbmCodeList(string keyword, string yfbmCode, string organizeId)
        {
            return _pharmacyDrugStorageDmnService.GetTheUpperYkbmCodeList(keyword, yfbmCode, organizeId);
        }

        /// <summary>
        /// 根据药库代码获取药房List（药库可向目标药房发药）
        /// </summary>
        /// <param name="ykbmCode"></param>
        /// <returns></returns>
        public IList<SysPharmacyDepartmentVEntity> GetTheLowerYfbmCodeList(string ykbmCode)
        {
            return _pharmacyDrugStorageDmnService.GetTheLowerYfbmCodeList("", ykbmCode, OrganizeId);
        }

        /// <summary>
        /// 根据药房代码获取科室List（药房可向目标科室发药）
        /// </summary>
        /// <param name="yfbmCode"></param>
        /// <returns></returns>
        public IList<SysDepartmentVEntity> GetTheLowerKsCodeList(string yfbmCode)
        {
            return _pharmacyDrugStorageDmnService.GetTheLowerKsCodeList(yfbmCode, OperatorProvider.GetCurrent().OrganizeId);
        }

        /// <summary>
        /// 药品授权药房部门
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public string EmpowermentPharmacyDepartment(EmpowermentPharmacyDepartmentRequestDto req)
        {
            if (req == null || string.IsNullOrWhiteSpace(req.bmypId)) return "请传入有效的本部门医药品信息；";
            var oldData = _sysPharmacyDepartmentMedicineRepo.SelectData(req.Ypdm, req.OrganizeId, req.yfbmCode);
            if (oldData != null && oldData.Count > 0)
            {
                if (oldData.Any(p => p.zt == "1")) return "";
                var item = oldData.FirstOrDefault();
                if (item != null)
                {
                    var t = _sysPharmacyDepartmentMedicineRepo.UpdateZt(item.bmypId, "1", req.CreatorCode, req.CreateTime);
                    return t > 0 ? "" : string.Format("修改授权部门药品为【{0}】的药品【{1}】失败；", req.yfbmCode, req.Ypdm);
                }
            }
            var bmypxx = new SysPharmacyDepartmentMedicineEntity
            {
                bmypId = req.bmypId,
                yfbmCode = req.yfbmCode,
                Ypdm = req.Ypdm,
                OrganizeId = req.OrganizeId,
                Ypkw = req.Ypkw,
                Zcxh = req.Zcxh,
                px = req.px,
                Pxfs1 = req.Pxfs1,
                Pxfs2 = req.Pxfs2,
                Kcsx = req.Kcsx,
                Kcxx = req.Kcxx,
                Jhd = req.Jhd,
                Jhl = req.Jhl,
                Ypsxdm = req.Ypsxdm,
                Sysx = req.Sysx,
                Ylsx = req.Ylsx,
                zt = req.zt,
                CreateTime = req.CreateTime,
                CreatorCode = req.CreatorCode,
                LastModifierCode = req.LastModifierCode,
                LastModifyTime = req.LastModifyTime
            };
            var rt = _sysPharmacyDepartmentMedicineRepo.Insert(bmypxx);
            return rt > 0 ? "" : string.Format("新增授权部门药品为【{0}】的药品【{1}】失败；", req.yfbmCode, req.Ypdm);
        }

        /// <summary>
        /// 药品授权药房部门 与EmpowermentPharmacyDepartment不同，该方法会先取消该药品所有药房的授权，在重新赋予权限
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public string EmpowermentPharmacyDepartmentAndRemoveOld(EmpowermentPharmacyDepartmentAndRemoveOldRequestDto req)
        {
            var result = new StringBuilder();
            if (req == null || req.epds == null || req.epds.Count == 0) return "请传入有效的本部门医药品信息；";
            req.epds.Select(p => p.Ypdm).Distinct().ToList().ForEach(o =>
            {
                _sysPharmacyDepartmentMedicineRepo.DeleteItem(o, req.epds[0].OrganizeId);
            });
            req.epds.ForEach(p =>
            {
                var tp = p.ToJson().ToObject<EmpowermentPharmacyDepartmentRequestDto>();
                result.Append(EmpowermentPharmacyDepartment(tp));
            });
            return result.ToString();
        }


    }
}
