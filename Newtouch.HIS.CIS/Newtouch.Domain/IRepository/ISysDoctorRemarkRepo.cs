using Newtouch.Domain.Entity;
using Newtouch.Domain.ValueObjects;
using Newtouch.Infrastructure.EF;
using System.Collections.Generic;

namespace Newtouch.Domain.IRepository
{
    public interface ISysDoctorRemarkRepo : IRepositoryBase<SysDoctorRemarkEntity>
    {
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns></returns>
        List<SysDoctorRemarkVO> GetListByOrg(string orgId);

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="keyValue"></param>
        void SubmitForm(SysDoctorRemarkEntity entity, string keyValue);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="keyValue"></param>
        void DeleteForm(string keyValue);



        #region 医生站--》文字录入--》指示文本框按钮

        //List<SyssurgeryTextVO> surgery_record(string OrganizeId);

        #endregion




    }
}
