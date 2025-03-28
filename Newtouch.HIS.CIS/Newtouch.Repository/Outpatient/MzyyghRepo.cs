using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Repository;
using Newtouch.Domain.Entity;
using Newtouch.Domain.IRepository;
using System;

namespace Newtouch.Repository
{
    /// <summary>
    /// 门诊预约挂号
    /// </summary>
    public class MzyyghRepo : RepositoryBase<MzyyghEntity>, IMzyyghRepo
    {
        public MzyyghRepo(IDefaultDatabaseFactory databaseFactory) : base(databaseFactory)
        {
        }

        /// <summary>
        /// 根据ID删除已预约挂号
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int DeleteById(string id)
        {
            if (string.IsNullOrWhiteSpace(id)) return 0;
            var entity = FindEntity(p => p.Id == id);
            if (entity == null) return 0;
            return Delete(entity);
        }

        /// <summary>
        /// 赴约
        /// </summary>
        /// <param name="id"></param>
        /// <param name="arrivalDate"></param>
        /// <param name="arrivalOpereater"></param>
        /// <returns></returns>
        public int KeepAnAppointment(string id, DateTime arrivalDate, string arrivalOpereater)
        {
            var entity = FindEntity(p => p.Id == id);
            if (entity == null) return 0;
            entity.arrivalOpereater = arrivalOpereater;
            entity.arrivalDate = arrivalDate;
            entity.Modify();
            return Update(entity);
        }
    }
}
