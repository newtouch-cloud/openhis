using FrameworkBase.Domain.Entity;
using FrameworkBase.Domain.IRepository;
using FrameworkBase.Infrastructure;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace FrameworkBase.Repository
{
    /// <summary>
    /// 创 建：张冬林
    /// 日 期：2017-11-17 16:15
    /// 描 述：人员岗位对照
    /// </summary>
    public sealed class SysStaffDutyRepo : RepositoryBase<SysStaffDutyEntity>, ISysStaffDutyRepo
    {
        /// <summary>
        /// 默认构造函数
        /// </summary>
        /// <param name="databaseFactory">EF上下文工厂</param>
        public SysStaffDutyRepo(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
            
        }

        /// <summary>
        /// 获取人员 已 关联 岗位 关联关系列表
        /// </summary>
        /// <param name="staffId"></param>
        /// <returns></returns>
        public IList<SysStaffDutyEntity> GetListByStaffId(string staffId)
        {
            if (string.IsNullOrWhiteSpace(staffId))
            {
                return null;
            }
            var sql = "select * from Sys_StaffDuty(nolock) where StaffId = @staffId and zt = '1'";
            return this.FindList<SysStaffDutyEntity>(sql, new[] { new SqlParameter("@staffId", staffId) });
        }

    }
}