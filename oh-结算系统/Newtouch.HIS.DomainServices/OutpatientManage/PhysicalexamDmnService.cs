using FrameworkBase.MultiOrg.DmnService;
using FrameworkBase.MultiOrg.Infrastructure;
using Newtouch.HIS.Domain.DTO;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.HIS.Domain.IDomainServices.OutpatientManage;
using Newtouch.Infrastructure;
using Newtouch.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.DomainServices.OutpatientManage
{
   public class PhysicalexamDmnService : DmnServiceBase, IPhysicalexamDmnService
    {

        private readonly IOutPatientSettleDmnService _outPatientSettleDmnService;
        public PhysicalexamDmnService(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }

        public Tuple<string,int> SubmitPhysicalexamForm(AddPhysicalexamDto dto, string orgId, string userCode)
        {
            SysPatientBasicInfoEntity xt_brjbxx = new SysPatientBasicInfoEntity();
            xt_brjbxx.patid = EFDBBaseFuncHelper.Instance.GetNewPrimaryKeyInt("xt_brjbxx");
            //xt_brjbxx.brxz = dto.brxz;
            xt_brjbxx.blh = dto.blh;
            xt_brjbxx.xm = dto.xm;
            xt_brjbxx.py = dto.py;
            xt_brjbxx.xb = dto.xb;
            xt_brjbxx.OrganizeId = orgId;
            xt_brjbxx.brly = "体检/疫苗";
            xt_brjbxx.Create();
            SysCardEntity newCardEntity = new SysCardEntity();
            newCardEntity.CardNo = dto.kh;
            newCardEntity.CardType = ((int)EnumCardType.XNK).ToString();
            newCardEntity.CardTypeName = ((EnumCardType)(Convert.ToInt32(newCardEntity.CardType))).GetDescription();
            newCardEntity.zt = "1";
            newCardEntity.hzxm = dto.xm;
            newCardEntity.OrganizeId = orgId;
            newCardEntity.patid = xt_brjbxx.patid;
            newCardEntity.Create(true);
            var mzhjzxh = _outPatientSettleDmnService.GetMzhJzxh(xt_brjbxx.patid, dto.ghpbId.ToString(), dto.ks, "", orgId, userCode);
            var jzxh = mzhjzxh.Item1;
            var mzh = mzhjzxh.Item2;
            var gh = new OutpatientRegistEntity
            {
                ghnm = EFDBBaseFuncHelper.Instance.GetNewPrimaryKeyInt(OutpatientRegistEntity.GetTableName()),
                OrganizeId = orgId,
                patid = xt_brjbxx.patid,
                mjzbz = "1",
                ks = dto.ks,
                ys=dto.ys,
                jzbz = ((int)EnumOutpatientJzbz.Jsjz).ToString(),
                jzxh = (short)jzxh,
                //门诊号
                mzh = mzh,
                kh = dto.kh,
                blh=dto.blh,
                xb=dto.xb,
                CardType= ((int)EnumCardType.XNK).ToString(),
                CardTypeName= ((EnumCardType)(Convert.ToInt32(newCardEntity.CardType))).GetDescription(),
                brxz=dto.brxz,
                xm=dto.xm
        };
            gh.Create();
            using (var db = new EFDbTransaction(this._databaseFactory).BeginTrans())
            {
                db.Insert(xt_brjbxx);
                db.Insert(newCardEntity);
                db.Insert(gh);
                try
                {
                    db.Commit();
                }
                catch (Exception e)
                {
                    throw e.InnerException;
                }
               
            }
            return Tuple.Create(mzh, xt_brjbxx.patid);
        }
    }
}
