using Newtouch.Domain.Entity;
using Newtouch.Infrastructure.EF;
using System;

namespace Newtouch.Domain.IRepository
{
    /// <summary>
    /// 门诊预约挂号
    /// </summary>
    public interface IMzyyghRepo : IRepositoryBase<MzyyghEntity>
    {
        /// <summary>
        /// 根据ID删除已预约挂号
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        int DeleteById(string id);

        /// <summary>
        /// 赴约
        /// </summary>
        /// <param name="id"></param>
        /// <param name="arrivalDate"></param>
        /// <param name="arrivalOpereater"></param>
        /// <returns></returns>
        int KeepAnAppointment(string id, DateTime arrivalDate, string arrivalOpereater);
    }
}
