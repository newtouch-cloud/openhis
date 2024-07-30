using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Repository;
using Newtouch.EMR.Domain.Entity;
using Newtouch.EMR.Domain.IRepository;
using Newtouch.EMR.Domain.ValueObjects.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.EMR.Repository
{
    public class MrBlApplyRecordRepo : RepositoryBase<MrBlApplyRecordEntity>, IMrBlApplyRecordRepo
    {
        public MrBlApplyRecordRepo(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }

        public void SubmitForm(MrApplyWritingVo vo, ApplyWritingResp resp, string czr,string orgId)
        {
            if (vo != null)
            {
                MrBlApplyRecordEntity entity = new MrBlApplyRecordEntity();
                entity.blmc = vo.MedicalName;
                entity.zyh = vo.Zyh;
                entity.ks = vo.ApplyDept;
                entity.xm = vo.PatName;
                entity.sqsj = vo.ApplyDate;
                entity.wcsj = vo.ApplyCompletionDate;
                entity.zkwcsj = vo.CompletionDate;
                entity.bllx = vo.bllx;
                entity.applyStatus = resp.ApplyStatus;
                entity.applyReturnNo = resp.Id;
                entity.ApprovePerson = resp.Approver;
                entity.ApproveDate = resp.ApproveDate;
                entity.ApproveDept = resp.ApproverDept;
                entity.OrganizeId = orgId;
                entity.CreatorCode = czr;

                entity.Id = Guid.NewGuid().ToString();
                entity.CreateTime = DateTime.Now;
                entity.zt = "1";
                entity.Create(true);
                this.Insert(entity);
            }
           
        }
    }
}
