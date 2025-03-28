/*******************************************************************************
 * Copyright © 2016 Newtouch 版权所有
 * Author: Newtouch
 * Description: 
*********************************************************************************/
using Newtouch.Tools;
using System.Collections.Generic;
using System.Linq;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.HIS.Domain.IDomainServices.SystemManage;
using Newtouch.HIS.Domain.ValueObjects.SystemManage;
using Newtouch.Common;

namespace Newtouch.HIS.Application
{
    public class SysCIRemindContentApp : ISysCIRemindContentApp
    {
        private readonly ISysChargeItemWarningContentRepo _SysCIRemindContentApp;
        private readonly ISysCIRemaindContentDmnService _SysCIRemaindContentDmnService;

        public SysCIRemindContentApp(ISysChargeItemWarningContentRepo SysCIRemindContentApp, ISysCIRemaindContentDmnService SysCIRemaindContentDmnService)
        {
            this._SysCIRemindContentApp = SysCIRemindContentApp;
            this._SysCIRemaindContentDmnService = SysCIRemaindContentDmnService;
        }


        /// <summary>
        /// 收费大类所有数据
        /// </summary>
        /// <returns></returns>
        public List<SysChargeItemWarningContentEntity> GetList()
        {
            return _SysCIRemindContentApp.IQueryable().ToList();
        }

        /// <summary>
        /// A页面带一个对象到B页面
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public SysCIRemindContentVO GetForm(int keyValue)
        {
            return _SysCIRemaindContentDmnService.GetnrFormJson(keyValue);
        }
        public void DeleteForm(int keyValue)
        {
            _SysCIRemindContentApp.Delete(t => t.sfxjsnrbh == keyValue);
        }
        public void SubmitForm(SysChargeItemWarningContentEntity xt_sfxmjsnrEntity, string keyValue)
        {
            _SysCIRemindContentApp.SubmitForm(xt_sfxmjsnrEntity, keyValue);
        }

        /// <summary>
        /// 获取所有收费大类下拉框
        /// </summary>
        /// <returns></returns>
        public string GetdlSelect()
        {
            //var data = this.GetList();
            //var treeList = new List<TreeSelectModel>();
            //foreach (xt_sfxmjsnrEntity item in data)
            //{
            //    TreeSelectModel treeModel = new TreeSelectModel();
            //    treeModel.id = item.sfxm;
            //    treeModel.text = item.sfxm;
            //    treeModel.parentId = "0";
            //    treeList.Add(treeModel);
            //}
            //return treeList.TreeSelectJson();
            return "";
        }

        /// <summary>
        /// Grid筛选查询显示
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public List<SysCIRemindContentVO> GetListBySearch(Pagination pagination, string keyword)
        {
            return _SysCIRemaindContentDmnService.GetGridBySearch(pagination, keyword);
            //var list = ExtLinq.True<SysChargeItemWarningContentEntity>();
            //if (!string.IsNullOrWhiteSpace(keyword))
            //{
            //    list = list.And(p => (p.sfxjsnrbh.ToString().Contains(keyword)));

            //}
            //return _SysCIRemindContentApp.FindList(list, pagination);
        }
    }
}
