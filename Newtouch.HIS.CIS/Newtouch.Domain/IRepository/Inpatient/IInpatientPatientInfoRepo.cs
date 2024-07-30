using Newtouch.Domain.Entity;
using System.Collections.Generic;
using Newtouch.Core.Common;
using Newtouch.Infrastructure.EF;
using Newtouch.Domain.ValueObjects.Outpatient;

namespace Newtouch.Domain.IRepository
{
    /// <summary>
    /// 创 建：张冬林
    /// 日 期：2018-06-28 13:27
    /// 描 述：住院病人信息
    /// </summary>
    public interface IInpatientPatientInfoRepo : IRepositoryBase<InpatientPatientInfoEntity>
    {
        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="keyValue">主键值</param>
        void SubmitForm(InpatientPatientInfoEntity entity, string keyValue);

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键值</param>
        void DeleteForm(string keyValue);
        /// <summary>
        /// 更新住院病人在院标志
        /// </summary>
        /// <param name="orgId"></param>
        /// <param name="zyh"></param>
        /// <param name="zybz"></param>
        void UpdateInpatientStatus(string orgId, string zyh, string zybz, string ryzd, string ryzdmc, string brxz, string brxzmc, string lxr, string lxrgx, string lxrdh);

        /// <summary>
        /// 通过住院号获取住院病人信息
        /// </summary>
        /// <param name="OrganizeId"></param>
        /// <param name="zyh"></param>
        /// <returns></returns>
        InpatientPatientInfoEntity GetByZyh(string OrganizeId, string zyh);

        /// <summary>
        /// 获取病人基本信息
        /// </summary>
        /// <param name="zyh">住院号</param>
        /// <param name="xm">姓名</param>
        /// <param name="organizeId">组织机构ID</param>
        /// <returns></returns>
        InpatientPatientInfoEntity SelectData(string zyh, string xm, string organizeId);


        List<InpatientAreaVO> TreeViewdata(string orgId);


    }
}