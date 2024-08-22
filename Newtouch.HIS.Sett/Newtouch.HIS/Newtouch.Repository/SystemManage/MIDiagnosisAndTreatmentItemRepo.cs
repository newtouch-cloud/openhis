/*******************************************************************************
 * Copyright © 2016 Newtouch 版权所有
 * Author: Newtouch
 * Description: 

*********************************************************************************/

using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Infrastructure;
using System.Linq;

namespace Newtouch.HIS.Repository
{
    public class MIDiagnosisAndTreatmentItemRepo : RepositoryBase<MIDiagnosisAndTreatmentItemEntity>, IMIDiagnosisAndTreatmentItemRepo
    {

        public MIDiagnosisAndTreatmentItemRepo(INewtouchDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }
        public object GetzlxmSelect()
        {
            var list = this.IQueryable().Where(p => p.zt == "1").Select(p => new
            {
                zlxmbh = p.setup_zlxmbh,
                ybdm = p.ybdm,
                wjdm = p.wjdm,
                xmmc = p.xmmc,
                py = p.py
            }).ToList();
            return list;
        }
    }
}
