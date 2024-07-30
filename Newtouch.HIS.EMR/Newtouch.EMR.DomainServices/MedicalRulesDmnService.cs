using FrameworkBase.MultiOrg.DmnService;
using FrameworkBase.MultiOrg.Infrastructure;
using Newtouch.Common.Operator;
using Newtouch.EMR.Domain.Entity;
using Newtouch.EMR.Domain.IDomainServices;
using Newtouch.EMR.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.EMR.DomainServices
{
    public class MedicalRulesDmnService : DmnServiceBase, IMedicalRulesDmnService
    {
        public MedicalRulesDmnService(IDefaultDatabaseFactory databaseFactory) : base(databaseFactory)
        {
        }

        public void SubmitForm(RulesEntityVo entityvo)
        {
            string OrganizeId = OperatorProvider.GetCurrent().OrganizeId;
            using (var db = new EFDbTransaction(this._databaseFactory).BeginTrans())
            {
                if (entityvo.czlx == "update")
                {
                    var entity = db.IQueryable<MrWritingRulesEntity>().FirstOrDefault(p => p.Id == entityvo.Id);
                    entity.Bllx = entityvo.Bllx;
                    entity.RulesDay = entityvo.RulesDay;
                    entity.RulesType = entityvo.RulesType;
                    entity.Remark = entityvo.Remark;
                    entity.Px = entityvo.Px;
                    entity.LastModifyTime = DateTime.Now;
                    entity.LastModifierCode = OperatorProvider.GetCurrent().UserCode;
                    db.Update(entity);
                }
                else
                {
                    MrWritingRulesEntity entity = new MrWritingRulesEntity();
                    entity.Id = Guid.NewGuid().ToString();
                    entity.Bllx = entityvo.Bllx;
                    entity.RulesType = entity.RulesType;
                    entity.RulesType = entityvo.RulesType;
                    entity.RulesDay = entityvo.RulesDay;
                    entity.Remark = entityvo.Remark;
                    entity.Px = entityvo.Px;
                    entity.OrganizeId = OrganizeId;
                    entity.CreateTime = DateTime.Now;
                    entity.CreatorCode = OperatorProvider.GetCurrent().UserCode;
                    entity.zt = "1";
                    db.Insert(entity);
                }


                db.Commit();
            }
        }
    }
}
