using Newtouch.HIS.Domain.Entity.SystemManage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.IRepository.SystemManage
{
    public interface ISysConsultRepo : IRepositoryBase<SysConsultEntity>
    {
        /// <summary>
        /// 获取诊室列表
        /// </summary>
        /// <param name="orgId"></param>
        /// <returns></returns>
        List<SysConsultEntity> GetConsultList(string orgId);

        /// <summary>
        /// 获取科室下诊室列表
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="ksCode"></param>
        /// <returns></returns>
        List<SysConsultEntity> GetConsultListByDept(string orgId, string ksCode);
        /// <summary>
        /// 获取科室下有效诊室列表
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="ksCode"></param>
        /// <returns></returns>
        List<SysConsultEntity> GetValidConsultListByDept(string orgId, string ksCode);
        /// <summary>
        /// 新增诊室
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="ksCode"></param>
        void SubmitForm(SysConsultEntity entity, string ksCode);
    }
}
