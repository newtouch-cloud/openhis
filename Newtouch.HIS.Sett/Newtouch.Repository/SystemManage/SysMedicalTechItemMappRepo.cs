/*******************************************************************************
 * Copyright © 2016 Newtouch 版权所有
 * Author: Newtouch
 * Description: 

*********************************************************************************/

using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Repository;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;

namespace Newtouch.HIS.Repository
{
    public class SysMedicalTechItemMappRepo : RepositoryBase<SysMedicalTechItemMappEntity>, ISysMedicalTechItemMappRepo
    {
        public SysMedicalTechItemMappRepo(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }

    }
}
