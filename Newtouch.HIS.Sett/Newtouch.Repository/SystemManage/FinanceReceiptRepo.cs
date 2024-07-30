/**********************************************************/
// Copyright (C) 2016 Newtouch 版权所有。 
// Description： 财务票据-预交金收据凭证号
// Author：HLF
// CreateDate： 2016/12/15 12:36:16 
//**********************************************************/

using FrameworkBase.MultiOrg.Infrastructure;
using FrameworkBase.MultiOrg.Repository;
using Newtouch.Core.Common.Exceptions;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Tools;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Newtouch.HIS.Repository
{
    public class FinanceReceiptRepo : RepositoryBase<FinanceReceiptEntity>, IFinanceReceiptRepo
    {
        public FinanceReceiptRepo(IDefaultDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }

        /// <summary>
        /// 获取收据号Entity
        /// </summary>
        /// <param name="gh"></param>
        /// <returns></returns>
        public FinanceReceiptEntity getSJKEntity(string gh, string orgId)
        {
            FinanceReceiptEntity sjEntity = new FinanceReceiptEntity();
            var SJList = this.IQueryable().Where(p => p.ry == gh && p.OrganizeId == orgId).OrderByDescending(p => p.CreateTime).ToList();
            if (SJList.Count > 0)
            {
                sjEntity = SJList.FirstOrDefault();
            }
            return sjEntity;
        }


        /// <summary>
        /// 根据工号获取可使用的收据号
        /// </summary>
        /// <param name="gh"></param>
        /// <returns></returns>
        public string getDQSJH(string gh, string orgId)
        {
            string result = string.Empty;
            FinanceReceiptEntity outEntity = new FinanceReceiptEntity();
            string type = string.Empty;

            if (string.IsNullOrEmpty(gh))
            {
                throw new FailedException("参数不正确！");
            }
            var SJList = this.IQueryable().Where(p => p.ry == gh && p.OrganizeId == orgId).OrderByDescending(p => p.CreateTime).ToList();
            if (SJList.Count < 1)
            {
                throw new FailedException("没有分配收据号，请填写起始收据号！");
            }
            else
            {
                foreach (var sj in SJList)
                {
                    if (sj.dqsjh < 1)
                    {
                        result = string.Format("{0}{1}", sj.szm, sj.qssjh);
                        break;
                    }
                    else if (sj.dqsjh > 0)
                    {
                        result = string.Format("{0}{1}", sj.szm, sj.dqsjh + 1);
                        if (checkSJIsUsedForUser(gh, sj.szm, sj.dqsjh + 1, out outEntity, out type, orgId))
                        {
                            break;
                        }
                    }
                }
            }

            return result;
        }
        /// <summary>
        /// 根据工号获取可使用的收据号(不累加pzh)
        /// </summary>
        /// <param name="gh"></param>
        /// <returns></returns>
        public string getDQSJHNew(string gh, string orgId, out FinanceReceiptEntity outsjhEntity, out string outsjhType)
        {
            string result = string.Empty;
            FinanceReceiptEntity outEntity = new FinanceReceiptEntity();
            string type = string.Empty;

            if (string.IsNullOrEmpty(gh))
            {
                throw new FailedException("参数不正确！");
            }
            var SJList = this.IQueryable().Where(p => p.ry == gh && p.OrganizeId == orgId).OrderByDescending(p => p.CreateTime).ToList();
            if (SJList.Count < 1)
            {
                throw new FailedException("没有分配收据号，请填写起始收据号！");
            }
            else
            {
                foreach (var sj in SJList)
                {
                    if (sj.dqsjh < 1)
                    {
                        result = string.Format("{0}{1}", sj.szm, sj.qssjh);
                        break;
                    }
                    else if (sj.dqsjh > 0)
                    {
                        result = string.Format("{0}{1}", sj.szm, sj.dqsjh + 1);
                        if (checkSJIsUsedForUser(gh, sj.szm, sj.dqsjh + 1, out outEntity, out type, orgId))
                        {
                            break;
                        }
                    }
                }
            }
            outsjhEntity = outEntity;
            outsjhType = type;
            return result;
        }



        /// <summary>
        /// 根据工号获取可使用的收据号(不累加pzh)
        /// </summary>
        /// <param name="gh"></param>
        /// <returns></returns>
        public string getDQSJHForpzh(string gh, string orgId)
        {
            string result = string.Empty;
            FinanceReceiptEntity outEntity = new FinanceReceiptEntity();
            string type = string.Empty;

            if (string.IsNullOrEmpty(gh))
            {
                throw new FailedException("参数不正确！");
            }
            var SJList = this.IQueryable().Where(p => p.ry == gh && p.OrganizeId == orgId).OrderByDescending(p => p.CreateTime).ToList();
            if (SJList.Count < 1)
            {
                throw new FailedException("没有分配收据号，请填写起始收据号！");
            }
            else
            {
                foreach (var sj in SJList)
                {
                    if (sj.dqsjh < 1)
                    {
                        result = string.Format("{0}{1}", sj.szm, sj.qssjh);
                        break;
                    }
                    else if (sj.dqsjh > 0)
                    {
                        result = string.Format("{0}{1}", sj.szm, sj.dqsjh + 1);
                        //if (checkSJIsUsedForUser(gh, sj.szm, sj.dqsjh + 1, out outEntity, out type, orgId))
                        //{
                        break;
                        //}
                    }
                }
            }
            return result;
        }
        /// <summary>
        /// 验证收据号
        /// 第一位为字母
        /// 或都为数字
        /// </summary>
        /// <param name="sjhfull">完整的收据号</param> 
        /// <returns></returns>
        public bool checkSJH(string sjhfull, string gh, out FinanceReceiptEntity outEntity, out string type, string orgId)
        {
            string szm = string.Empty;
            type = string.Empty;
            long value = 0;
            int nTemp = 0;
            outEntity = new FinanceReceiptEntity();

            if (string.IsNullOrEmpty(sjhfull))
            {
                throw new Exception("没有取到数据号。");
            }

            szm = sjhfull.Substring(0, 1);
            //如果收据号第一个字符不是数字，则为首字母
            if (!int.TryParse(szm, out nTemp))
            {
                szm = sjhfull.Substring(0, 1);
                if (!long.TryParse(sjhfull.Substring(1, sjhfull.Length - 1), out value))
                {
                    throw new Exception("收据号除首字母外必须为数字。");
                }
            }
            else
            {
                szm = string.Empty;
                if (!long.TryParse(sjhfull, out value))
                {
                    throw new Exception("收据号必须为数字。");
                }
            }

            return checkSJIsUsedForUser(gh, szm, value, out outEntity, out type, orgId);
        }



        /// <summary>
        /// 判断当前收据号是否已被使用
        /// 返回收据号校验结果（true ：校验通过，false：已使用的收据号或者未分配的收据号不能使用）
        /// </summary>
        /// <param name="gh">使用者工号</param>
        /// <param name="szm">首字母</param>
        /// <param name="value">收据号</param> 
        /// <returns></returns>
        public bool checkSJIsUsedForUser(string gh, string szm, long sjh, out FinanceReceiptEntity outEntity, out string type, string orgId)
        {
            type = string.Empty;
            outEntity = new FinanceReceiptEntity();
            bool result = false;
            if (sjh < 0 || string.IsNullOrEmpty(gh))
            {
                throw new FailedException("参数不正确！");
            }
            var SJForSjList = this.IQueryable().Where(p => p.qssjh <= sjh && p.szm == szm && sjh <= p.dqsjh && p.OrganizeId == orgId).OrderBy(p => p.qssjh).ToList();

            if (SJForSjList.Count > 0)
            {
                throw new FailedException("当前收据号已使用！");
            }

            var SJList = this.IQueryable().Where(p => p.szm == szm && p.OrganizeId == orgId).OrderBy(p => p.qssjh).ToList();
            foreach (var sj in SJList)
            {
                var intervalData = new
                {
                    Start = sj.qssjh,
                    End = sj.dqsjh + 1
                };
                if (sjh >= intervalData.Start && sjh <= intervalData.End)
                {
                    if (sj.dqsjh > 0)
                    {
                        if (sj.dqsjh == (sjh - 1)) //连着上一次使用的收据号
                        {
                            //修改原来记录的当前收据号
                            FinanceReceiptEntity sjentity = sj;
                            sjentity.dqsjh = sjh;
                            sjentity.ry = gh;
                            sjentity.LastModifyTime = DateTime.Now;
                            sjentity.LastModifierCode = gh;
                            //this.Update(sjentity);// 在加预交金的同时 才修改收据号
                            outEntity = sjentity;
                            type = "update";
                        }
                    }
                    else
                    {
                        FinanceReceiptEntity sjentity = sj;
                        sjentity.dqsjh = sjh;
                        sjentity.ry = gh;
                        sjentity.LastModifyTime = DateTime.Now;
                        sjentity.LastModifierCode = gh;
                        //this.Update(sjentity); //无当前号，修改原来的记录的当前收据号
                        outEntity = sjentity;
                        type = "update";
                    }
                    result = true;
                    break;
                }
            }
            if (result == false)
            {
                FinanceReceiptEntity sjentity = new FinanceReceiptEntity();
                sjentity.cwsjId = Comm.GuId();
                sjentity.ry = gh;
                sjentity.szm = szm;
                sjentity.qssjh = sjh;
                sjentity.dqsjh = sjh;
                sjentity.jssjh = 0;       //结束收据号暂时都不填
                sjentity.CreateTime = DateTime.Now;
                sjentity.CreatorCode = gh;
                sjentity.ry = gh;
                sjentity.zt = "1";
                sjentity.OrganizeId = orgId;
                //this.Insert(sjEntity); 
                outEntity = sjentity;
                type = "insert";
                result = true;
            }
            return result;
        }


        /// <summary>
        /// 添加收据凭证号
        /// </summary>
        public void AddReceiptInfo(FinanceReceiptEntity sjEntity, string orgId)
        {
            sjEntity.Create();
            this.Insert(sjEntity);
        }

        /// <summary>
        /// 修改当前收据凭证号
        /// </summary>
        public void UpdateReceiptInfo(FinanceReceiptEntity sjEntity, string cwsjId)
        {
            sjEntity.Modify(cwsjId);
            this.Update(sjEntity);
        }

        #region 分配收据号
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IList<FinanceReceiptEntity> GetListByUserCode(string lyry, string orgId)
        {
            return this.IQueryable().Where(p => p.ry == lyry && p.zt == "1" && p.OrganizeId == orgId)
                .OrderByDescending(p => p.LastModifyTime).ToList();
        }

        /// <summary>
        /// 修改页面，根据主键获取实体
        /// </summary>
        /// <param name="ypdm"></param>
        /// <returns></returns>
        public FinanceReceiptEntity GetFinancialInvoiceEntity(string Id, string orgId)
        {
            var entity = this.IQueryable().Where(a => a.cwsjId == Id && a.OrganizeId == orgId).FirstOrDefault();
            return entity;
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="financialInvoiceEntity"></param>
        /// <param name="fpdm"></param>
        public void SubmitForm(FinanceReceiptEntity financialInvoiceEntity, string Id)
        {
            if (!string.IsNullOrEmpty(Id))
            {
                financialInvoiceEntity.Modify(Id);
                this.Update(financialInvoiceEntity);
            }
            else
            {
                financialInvoiceEntity.Create();
                financialInvoiceEntity.cwsjId = Guid.NewGuid().ToString();
                this.Insert(financialInvoiceEntity);
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="keyValue"></param>
        public void DeleteForm(string Id, string orgId)
        {
            this.Delete(a => a.cwsjId == Id && a.OrganizeId == orgId);
        }

        //y1:entity.qsfph,y2:entity.jsfph
        //x1:item.qsfph,x2:item.jsfph
        public void VlidateRepeat(FinanceReceiptEntity entity, string orgId,string Id)
        {
            var e = this.IQueryable().Where(p => p.zt == "1" && p.OrganizeId == orgId);
            if (!string.IsNullOrWhiteSpace(Id))
            {
                e = e.Where(p=>p.cwsjId!= Id);
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
                        if ((entity.qssjh < item.qssjh && entity.jssjh > item.jssjh) ||
                            (entity.qssjh < item.qssjh && entity.jssjh > item.qssjh) ||
                            (entity.qssjh > item.qssjh && entity.jssjh < item.jssjh) ||
                            (entity.qssjh > item.qssjh && entity.jssjh > item.jssjh && entity.qssjh < item.jssjh)||
                            (entity.qssjh==item.qssjh)||(entity.jssjh==item.jssjh)
                            )
                        {
                            throw new FailedException("收据号段重复！");
                        }
                    }
                }
            }
            #endregion
        }
    }
}
