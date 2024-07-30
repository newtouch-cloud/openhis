using Newtouch.HIS.Application.Interface;
using System.Collections.Generic;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;

namespace Newtouch.HIS.Application.Implementation
{
    /// <summary>
    /// （下拉）选择 数据源
    /// </summary>
    public class SelectDataSourceApp : ISelectDataSourceApp
    {
        private readonly ISysPatientNatureRepo _sysPatiNatureRepo;
        private readonly ISysChargeCategoryRepo _sysChargeMajorClassRepo;
        public SelectDataSourceApp(ISysPatientNatureRepo sysPatiNatureRepo, ISysChargeCategoryRepo sysChargeMajorClassRepo)
        {
            this._sysPatiNatureRepo = sysPatiNatureRepo;
            this._sysChargeMajorClassRepo = sysChargeMajorClassRepo;
        }

        /// <summary>
        /// 获取病人性质有效列表
        /// </summary>
        /// <returns></returns>
        public string getEffectPatiNatureList()
        {
            return _sysPatiNatureRepo.getEffectPatiNatureList();
        }


    }
}
