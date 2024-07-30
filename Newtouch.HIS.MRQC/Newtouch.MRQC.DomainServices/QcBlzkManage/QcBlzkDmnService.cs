using FrameworkBase.MultiOrg.DmnService;
using FrameworkBase.MultiOrg.Infrastructure;
using Newtouch.Common.Operator;
using Newtouch.MRQC.Domain.Entity.QcItemManage;
using Newtouch.MRQC.Domain.IDomainServices.QcBlzkManage;
using Newtouch.MRQC.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.MRQC.DomainServices.QcBlzkManage
{
    public class QcBlzkDmnService : DmnServiceBase, IQcBlzkDmnService
    {
        public QcBlzkDmnService(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
        /// <summary>
        /// 质控项目维护
        /// </summary>
        /// <param name="entityvo"></param>
        public void SubmitForm(ZkxmEntityVo entityvo)
        {
            string OrganizeId= OperatorProvider.GetCurrent().OrganizeId;
            using (var db = new EFDbTransaction(this._databaseFactory).BeginTrans())
            {
                if (entityvo.czlx == "update")
                {
                    var entity = db.IQueryable<QcItemDataEntity>().FirstOrDefault(p => p.Id == entityvo.Id);
                    entity.Bllmbmc = entityvo.Bllmbmc;
                    entity.Name = entityvo.Name;
                    entity.Score = entityvo.Score;
                    entity.Remark = entityvo.Remark;
                    entity.Px = entityvo.Px;
                    entity.LastModifyTime = DateTime.Now;
                    entity.LastModifierCode = OperatorProvider.GetCurrent().UserCode;
                    db.Update(entity);
                }
                else {
                    int cnt = 0;
                    var list = db.IQueryable<QcItemDataEntity>().Where(p => p.BlmbId == entityvo.BlmbId && p.OrganizeId == OrganizeId && p.zt == "1").OrderByDescending(p=>p.Code).ToList().FirstOrDefault();
                    if (list != null)
                    {
                        cnt = Convert.ToInt32(list.Code);
                    }
                    QcItemDataEntity entity = new QcItemDataEntity();
                    entity.Bllmbmc = entityvo.Bllmbmc;
                    entity.Code = (cnt + 1).ToString();
                    entity.BlmbId = entity.BlmbId;
                    entity.Name = entityvo.Name;
                    entity.BlmbId = entityvo.BlmbId;
                    entity.Score = entityvo.Score;
                    entity.Remark = entityvo.Remark;
                    entity.zklx = entityvo.zklx;
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


        public IList<ScoreItemVo> Getybbxbldata(string bllx, string orgId,string zyh)
        {
            string sqlstr = @"SELECT b.Zyh,a.Score,a.BlmbId,a.Code,a.Name,a.Remark,isnull(b.sl,0.00) qxs,NULL ysc
FROM [MR_Qc_ItemData] a with(nolock)
LEFT JOIN [Mr_Qc_Score] b with(nolock) on a.BlmbId = b.BllxId and a.Code=b.ScoreCode and a.OrganizeId = b.OrganizeId and  Zyh=@zyh
WHERE a.BlmbId = @bllx  and a.OrganizeId =@orgId  order by a.px 
";
            var pars = new List<SqlParameter>() {
                new SqlParameter("@zyh", zyh),
                new SqlParameter("@orgId", orgId),
                new SqlParameter("@bllx", bllx)
             };
            return this.FindList<ScoreItemVo>(sqlstr, pars.ToArray());
        }

        public void SaveScoreDate(List<ScoreEntity> entity, string orgId, string CreatorCode,string bllx,string blmc,string zyh)
        {
            using (var db = new EFDbTransaction(this._databaseFactory).BeginTrans())
            {
                //var blmbid = entity.FirstOrDefault().BlmbId;
                var isczList = db.IQueryable<MrQcScoreEntity>().Where(p =>p.Zyh==zyh && p.BllxId == bllx && p.zt == "1" && p.OrganizeId == orgId).ToList();
                if (isczList.Count > 0)
                {
                    foreach (var item in isczList)
                    {
                        db.Delete<MrQcScoreEntity>(p => p.Id == item.Id);
                    };
                }
                foreach (var item in entity)
                {
                    MrQcScoreEntity data = new MrQcScoreEntity();
                    data.Id = Guid.NewGuid().ToString();
                    data.Zyh = zyh;
                    //data.WritId = blId;
                    data.BllxId = bllx;
                    //data.BlmbId = item.BlmbId;
                    data.Blmc = blmc;
                    data.ScoreCode = item.Code;
                    data.ScoreMc = item.Name;
                    data.ScoreMcValue = item.Score;
                    data.sl = item.qxs;
                    data.OrganizeId = orgId;
                    data.zt = "1";
                    data.CreateTime = DateTime.Now;
                    data.CreatorCode = CreatorCode;
                    db.Insert(data);
                };
                db.Commit();
            }
        }
    }
}
