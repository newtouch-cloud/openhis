using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Repository;
using Newtouch.Core.Common;
using Newtouch.HIS.Domain.DTO.PharmacyDrugStorage;
using Newtouch.HIS.Domain.Entity.Purchase;
using Newtouch.HIS.Domain.IRepository.Purchase;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Repository.Purchase
{
    public class PurchaseBillDetailRepo : RepositoryBase<PurchaseBillDetailEntity>, IPurchaseBillDetailRepo
    {
        public PurchaseBillDetailRepo(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }

        public int InsertItem(OutputStructYY004 entity, string orgId)
        {
            var sfwpsfp = "";
            var splx = "";
            var sfch = "";
            if (entity.SFWPSFP == "否") sfwpsfp = "0";
            if (entity.SFWPSFP == "是") sfwpsfp = "1";
            if (entity.SPLX=="药品类") splx = "1";
            if (entity.SPLX == "医用耗材类") splx = "2";
            if (entity.SPLX == "医疗器械类") splx = "3";
            if (entity.SPLX == "其他") splx = "9";
            if (entity.SFCH == "否") sfch = "0";
            if (entity.SFCH == "是") sfch = "1";
            PurchaseBillDetailEntity dbEntity = new PurchaseBillDetailEntity();
            dbEntity.Id = Guid.NewGuid().ToString();
            dbEntity.OrganizeId = orgId;
            dbEntity.fph = entity.FPH;
            dbEntity.fprq = entity.FPRQ;
            dbEntity.fphszje = entity.FPHSZJE;
            dbEntity.yqbm = entity.YQBM;
            dbEntity.yybm = entity.YYBM;
            dbEntity.psdbm = entity.PSDBM;
            dbEntity.dlsgbz = entity.DLSGBZ;
            dbEntity.fpbz = entity.FPBZ;
            dbEntity.sfwpsfp = sfwpsfp;
            dbEntity.wpsfpsm = entity.WPSFPSM;
            dbEntity.fpmxbh = entity.FPMXBH;
            dbEntity.splx = splx;
            dbEntity.sfch = sfch;
            dbEntity.zxspbm = entity.ZXSPBM;
            dbEntity.scph = entity.SCPH;
            dbEntity.scrq = entity.SCRQ;
            dbEntity.spsl = entity.SPSL;
            dbEntity.glmxbh = entity.GLMXBH;
            dbEntity.xsddh = entity.XSDDH;
            dbEntity.sxh = entity.SXH;
            dbEntity.yxrq = entity.YXRQ;
            dbEntity.wsdj = entity.WSDJ;
            dbEntity.hsdj = entity.HSDJ;
            dbEntity.sl = entity.SL;
            dbEntity.se = entity.SE;
            dbEntity.hsje = entity.HSJE;
            dbEntity.pfj = entity.PFJ;
            dbEntity.lsj = entity.LSJ;
            dbEntity.pzwh = entity.PZWH;

            dbEntity.Create(true);
            return this.Insert(dbEntity);
        }

      
    }
}
