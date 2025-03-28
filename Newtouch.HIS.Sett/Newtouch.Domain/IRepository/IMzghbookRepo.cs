using Newtouch.HIS.Domain.Entity;
using Newtouch.Infrastructure.EF;
using System;

namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// 创 建：chl
    /// 日 期：2019-12-16 17:20
    /// 描 述：预约
    /// </summary>
    public interface IMzghbookRepo : IRepositoryBase<MzghbookEntity>
    {
        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="keyValue">主键值</param>
        int SubmitForm(MzghbookEntity entity, decimal? keyValue);

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键值</param>
        int DeleteForm(decimal keyValue);

        /// <summary>
        /// 门诊预约挂号修改状态
        /// </summary>
        /// <param name="jsnm"></param>
        /// <param name="mzh"></param>
        /// <param name="patid"></param>
        /// <param name="ks"></param>
        /// <param name="zfje"></param>
        /// <param name="Appid"></param>
        void UpdateMzGhAppointment(string jsnm,string mzh,int patid,string ks,string zfje,string Appid,string orgId,string outdate,string mzlx,string ghf);

        void SigninAppointment(string mzh,string orgId);

		int FZsjTB(string ghrq, string rygh, string orgid);

	}
}