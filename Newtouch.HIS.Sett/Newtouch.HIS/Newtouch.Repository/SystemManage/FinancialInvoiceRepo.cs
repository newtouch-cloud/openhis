using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Repository;
using Newtouch.Core.Common.Exceptions;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Infrastructure;
using Newtouch.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Newtouch.HIS.Repository
{
    /// <summary>
    /// 
    /// </summary>
    public class FinancialInvoiceRepo : RepositoryBase<FinancialInvoiceEntity>, IFinancialInvoiceRepo
    {
        public FinancialInvoiceRepo(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IList<FinancialInvoiceEntity> GetListByUserCode(string lyry, string orgId)
        {
            return this.IQueryable().Where(p => p.lyry == lyry && p.zt == "1" && p.OrganizeId == orgId)
                .OrderByDescending(p => p.LastModifyTime).ToList();
        }

        /// <summary>
        /// 使用发票，数据库变更 同步当前发票号
        /// </summary>
        /// <param name="updateEntity"></param>
        /// <param name="insertEntity"></param>
        public void UpdateCurrentGetEntitys(string fph, string lyry, out FinancialInvoiceEntity updateEntity, out FinancialInvoiceEntity insertEntity, string orgId)
        {
            updateEntity = insertEntity = null;
            if (string.IsNullOrWhiteSpace(fph) || string.IsNullOrWhiteSpace(lyry) || string.IsNullOrWhiteSpace(orgId))
            {
                return;
            }
            var fphszm = "";
            long fphnum = 0;
            if (Regex.IsMatch(fph, "^[a-z][0-9]+$", RegexOptions.IgnoreCase))
            {
                fphszm = fph.Substring(0, 1);
                fphnum = Convert.ToInt64(fph.Substring(1));
            }
            else if (Regex.IsMatch(fph, "^[0-9]+$", RegexOptions.IgnoreCase))
            {
                fphszm = "";
                fphnum = Convert.ToInt64(fph);
            }
            if (fphnum <= 0)
            {
                throw new FailedCodeException("FINANCIAL_INVOICENO_IS_ERROR");
            }
            var cwfpList = GetListByUserCode(lyry, orgId);
            foreach (var cwfp in cwfpList)
            {
                if (fphnum >= cwfp.qsfph && fphnum <= cwfp.jsfph)
                {
                    if (cwfp.dqfph == 0)
                    {
                        //更新原cw_fp记录
                        cwfp.dqfph = fphnum;
                        cwfp.LastModifyTime = DateTime.Now;
                        updateEntity = cwfp;
                    }
                    //else if (fphnum <= cwfp.dqfph)
                    //{
                    //    throw new FailedException("发票号已被使用，请重新选择");
                    //}
                    else if (fphnum - 1 == cwfp.dqfph)
                    {
                        //更新原cw_fp记录
                        cwfp.dqfph = fphnum;
                        cwfp.LastModifyTime = DateTime.Now;
                        updateEntity = cwfp;
                    }
                    else
                    {
                        //更新原cw_fp记录
                        cwfp.dqfph = fphnum;
                        cwfp.LastModifyTime = DateTime.Now;
                        updateEntity = cwfp;
                        #region 不需自动生成发票段
                        //var oldJsfph = cwfp.jsfph;

                        ////分段
                        //cwfp.jsfph = fphnum - 1;
                        ////cwfp.ty = ((int)EnumCWFPTY.YTY).ToString();
                        ////cwfp.LastModifyUserId = gh;
                        //cwfp.LastModifyTime = DateTime.Now;
                        //updateEntity = cwfp;

                        //insertEntity = new FinancialInvoiceEntity()
                        //{
                        //    fpdm = EFDBBaseFuncHelper.Instance.GetNewPrimaryKeyString("cw_fp"),
                        //    OrganizeId = orgId,
                        //    qsfph = fphnum,
                        //    dqfph = fphnum,
                        //    jsfph = oldJsfph,
                        //    szm = fphszm,
                        //    zt = "1",
                        //    lyry = lyry,
                        //    lyrq = DateTime.Now,
                        //    LastModifyTime = DateTime.Now,  //用于排序
                        //    CreatorCode = "",
                        //    CreateTime = DateTime.Now,
                        //};
                        #endregion
                    }
                    return;
                }
            }
            throw new FailedCodeException("SYS_THIS_INVOICE_NO_NOT_ASSIGNED_PLEASE_CONTACT_THE_ADMINISTRATOR");
        }

        /// <summary>
        /// 获取所有发票列表
        /// </summary>
        /// <param name="keyValue"></param>
        /// <param name="fpId"></param>
        /// <returns></returns>
        public IList<FinancialInvoiceEntity> GetFinancialInvoiceList(string keyValue, string OrganizeId)
        {
            IList<FinancialInvoiceEntity> list = null;
            if (!string.IsNullOrEmpty(keyValue))
            {
                list = this.IQueryable().Where(a => a.lyry == keyValue && a.OrganizeId == OrganizeId).ToList();
            }
            else
            {
                list = this.IQueryable().Where(a => a.OrganizeId == OrganizeId).ToList();
            }
            return list;
        }

        /// <summary>
        /// 修改页面，根据主键获取实体
        /// </summary>
        /// <param name="ypdm"></param>
        /// <returns></returns>
        public FinancialInvoiceEntity GetFinancialInvoiceEntity(string fpdm, string orgId)
        {
            var entity = this.IQueryable().Where(a => a.fpdm == fpdm && a.OrganizeId == orgId).FirstOrDefault();
            return entity;
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="financialInvoiceEntity"></param>
        /// <param name="fpdm"></param>
        public void SubmitForm(FinancialInvoiceEntity financialInvoiceEntity, string fpdm)
        {
            if (!string.IsNullOrEmpty(fpdm))
            {
                financialInvoiceEntity.Modify(fpdm);
                this.Update(financialInvoiceEntity);
            }
            else
            {
                financialInvoiceEntity.Create();
                financialInvoiceEntity.fpdm = Guid.NewGuid().ToString();
                this.Insert(financialInvoiceEntity);
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="keyValue"></param>
        public void DeleteForm(string fpdm, string orgId)
        {
            this.Delete(a => a.fpdm == fpdm && a.OrganizeId == orgId);
        }

        //y1:entity.qsfph,y2:entity.jsfph
        //x1:item.qsfph,x2:item.jsfph
        public void VlidateRepeat(FinancialInvoiceEntity entity, string orgId, string Id)
        {
            var e = this.IQueryable().Where(p => p.zt == "1" && p.OrganizeId == orgId);
            if (!string.IsNullOrWhiteSpace(Id))
            {
                e = e.Where(p => p.fpdm != Id);
            }
            if (e != null && e.Count() > 0)
            {
                foreach (var item in e)
                {
                    if (item.szm == entity.szm)
                    {
                        //A) y1 < x1 并且 y2 > x2;
                        //B) y1 < x1 并且 y2 > x1;
                        //C) y1 > x1 并且 y2<x2;
                        //D) y1 > x1 , y2 > x2 并且 y1<x2;
                        if ((entity.qsfph < item.qsfph && entity.jsfph > item.jsfph) ||
                            (entity.qsfph < item.qsfph && entity.jsfph > item.qsfph) ||
                            (entity.qsfph > item.qsfph && entity.jsfph < item.jsfph) ||
                            (entity.qsfph > item.qsfph && entity.jsfph > item.jsfph && entity.qsfph < item.jsfph) ||
                            (entity.qsfph == item.qsfph) || (entity.jsfph == item.jsfph)
                            )
                        {
                            throw new FailedException("发票号段重复！");
                        }
                    }
                }
            }
        }
    }
}


