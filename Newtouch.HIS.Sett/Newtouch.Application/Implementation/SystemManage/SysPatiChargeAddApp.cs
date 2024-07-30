using Newtouch.HIS.Domain.Entity;
using System.Collections.Generic;
using Newtouch.HIS.Domain.ValueObjects;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.HIS.Domain.IDomainServices;
using System.Linq;
using FrameworkBase.MultiOrg.Application;

namespace Newtouch.HIS.Application.SystemManage
{
    /// <summary>
    /// 
    /// </summary>
    public class SysPatiChargeAddApp : AppBase, ISysPatiChargeAddApp
    {
        private readonly ISysPatientChargeAdditionalRepo _SysPatiChargeAddRepo;
        private readonly ISysPatiChargeAddDmnService _sysPatiChargeAddDmnService;
        private readonly ISysChargeAdditionalCategoryRepo _sysChargeAdditionalCategoryRepo;

        /// <summary>
        /// 获取有效列表
        /// </summary>
        /// <returns></returns>
        public List<SysPatiChargeAddVo> GetSysPatiChargeAddVoList(string keyword, int? bh = null)
        {
            return _sysPatiChargeAddDmnService.GetSysPatiChargeAddVoList(keyword, bh);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="keyValue"></param>
        public void DeleteForm(SysPatientChargeAdditionalEntity sysPatiChargeAddEntity)
        {
            _SysPatiChargeAddRepo.DeleteForm(sysPatiChargeAddEntity, this.OrganizeId);
        }
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sysPatiChargeAddEntity"></param>
        /// <param name="keyValue"></param>
        public void SubmitForm(SysPatientChargeAdditionalEntity sysPatiChargeAddEntity, int? keyValue)
        {
            _SysPatiChargeAddRepo.SubmitForm(sysPatiChargeAddEntity, keyValue, this.OrganizeId);
        }

        /// <summary>
        /// 附加显示大类下拉框
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public List<SysChargeAdditionalCategoryEntity> GetfjsfdlList(string keyValue)
        {
            List<SysChargeAdditionalCategoryEntity> data = null;
            if (!string.IsNullOrWhiteSpace(keyValue))
            {
                data= _sysChargeAdditionalCategoryRepo.IQueryable().Where(a => a.dlmc.Contains(keyValue) || a.py.Contains(keyValue)).ToList();
            }
            else
            {
                data= _sysChargeAdditionalCategoryRepo.IQueryable().ToList();
            }
            return data;
        }

        /// <summary>
        /// 获取服务费比例
        /// </summary>
        /// <param name="brxz"></param>
        /// <param name="sfxm"></param>
        /// <returns></returns>
        public decimal? GetFWFBL(string brxz, string dl, string sfxm, decimal dj)
        {
           
            var entity = _SysPatiChargeAddRepo.GetFWFBL(brxz, null, sfxm, this.OrganizeId);//_SysPatiChargeAddRepo.GetFWFBL(" and brxz='" + brxz + "' and sfxm='" + sfxm + "'");
            decimal fwfbl = 0;
            if (entity != null)
            {
                fwfbl = dj * entity.fwfbl;
            }
            else
            {
                var entity2 = _SysPatiChargeAddRepo.GetFWFBL(brxz, dl, sfxm, this.OrganizeId);//_SysPatiChargeAddRepo.GetFWFBL(" and brxz='" + brxz + "' and dl='" + dl + "' and sfxm='" + sfxm + "'");
                if (entity2 != null)
                {
                    fwfbl = dj * entity2.fwfbl;
                }
            }
            return fwfbl;
        }


    }
}
