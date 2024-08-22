/*******************************************************************************
 * Copyright © 2016 Newtouch 版权所有
 * Author: Newtouch
 * Description: 

*********************************************************************************/

using FrameworkBase.MultiOrg.Domain.Entity;
using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Repository;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Infrastructure;

namespace Newtouch.HIS.Repository
{
    public class SysLogRepository : RepositoryBase<SysLogEntity>, ISysLogRepository
    {
        public SysLogRepository(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }


    }
}
