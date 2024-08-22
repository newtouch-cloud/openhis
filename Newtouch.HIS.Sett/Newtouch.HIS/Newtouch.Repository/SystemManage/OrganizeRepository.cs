/*******************************************************************************
 * Copyright © 2016 Newtouch 版权所有
 * Author: Newtouch
 * Description: 

*********************************************************************************/

using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Infrastructure;

namespace Newtouch.HIS.Repository
{
    public class OrganizeRepository : RepositoryBase<SysOrganizeVEntity>, IOrganizeRepository
    {
        public OrganizeRepository(IBaseDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }

    }
}
