using System.Collections.Generic;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;

namespace Newtouch.HIS.Application.SystemManage
{
    public class SysDepartmentApp : ISysDepartmentApp
    {
        private readonly ISysDepartmentRepo _sysDepartResposity;

        public SysDepartmentApp(ISysDepartmentRepo sysDepartResposity)
        {
            this._sysDepartResposity = sysDepartResposity;
        }

        public List<SysDepartmentEntity> GetList()
        {
            return _sysDepartResposity.GetList();
        }

        public object GetksList()
        {
            return _sysDepartResposity.GetksList();
        }
    }
}
