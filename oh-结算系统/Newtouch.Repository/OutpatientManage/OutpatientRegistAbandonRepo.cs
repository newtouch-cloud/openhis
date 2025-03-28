using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Infrastructure;
using System;

namespace Newtouch.HIS.Repository
{
    /// <summary>
    /// 门诊作废
    /// </summary>
    public class OutpatientRegistAbandonRepo : RepositoryBase<OutpatientRegistAbandonEntity>, IOutpatientRegistAbandonRepo
    {
        public OutpatientRegistAbandonRepo(INewtouchDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }
        ///// <summary>
        /////保存挂号作废记录
        ///// </summary>
        ///// <param name="outPatientRegAbandEntity"></param>
        //public void SaveRegAbandRecord(int ghnm, string yhm, int rybh)
        //{
        //    OutpatientRegistAbandonEntity outPatientRegAbandEntity = new OutpatientRegistAbandonEntity();
        //    outPatientRegAbandEntity.Create();
        //    outPatientRegAbandEntity.ghnm = ghnm;
        //    outPatientRegAbandEntity.zfry = yhm;
        //    outPatientRegAbandEntity.zfrybh = rybh;
        //    outPatientRegAbandEntity.zfrq = DateTime.Now;
        //    this.Insert(outPatientRegAbandEntity);
        //}

    }
}


