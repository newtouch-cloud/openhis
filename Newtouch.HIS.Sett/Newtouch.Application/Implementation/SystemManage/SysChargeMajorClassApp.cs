/*******************************************************************************
 * Copyright © 2016 Newtouch 版权所有
 * Author: Newtouch
 * Description: 
*********************************************************************************/
using Newtouch.Tools;
using Newtouch.Tools.DB;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Infrastructure;
using Newtouch.Common;
using System.Data.SqlClient;
using Newtouch.Common.Operator;

namespace Newtouch.HIS.Application
{
    public class SysChargeMajorClassApp : ISysChargeMajorClassApp
    {
        private readonly ISysChargeCategoryRepo _xt_sfdlRepository;

        public SysChargeMajorClassApp(ISysChargeCategoryRepo xt_sfdlRepository)
        {
            this._xt_sfdlRepository = xt_sfdlRepository;
        }

        /// <summary>
        /// A页面带一个对象到B页面
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public SysChargeCategoryEntity GetForm(int keyValue)
        {
            return null;
            //return _xt_sfdlRepository.GetsfdlFirst(keyValue);
        }
        public void DeleteForm(int keyValue)
        {
            _xt_sfdlRepository.Delete(t => t.dlId == keyValue);
        }
        public void SubmitForm(SysChargeCategoryEntity xt_sfdlEntity, string keyValue)
        {
            return;
            //_xt_sfdlRepository.SubmitForm(xt_sfdlEntity, keyValue);
        }

        /// <summary>
        /// 收费大类下拉框
        /// </summary>
        /// <returns></returns>
        public string GetsfdlTreeSelectJson()
        {
            var data = _xt_sfdlRepository.GetdlSelect(OperatorProvider.GetCurrent().OrganizeId);
            var treeList = new List<TreeSelectModel>();
            foreach (var item in data)
            {
                TreeSelectModel treeModel = new TreeSelectModel();
                treeModel.id = item.dlCode;
                treeModel.text = item.dlmc;
                treeModel.parentId = "0";
                treeList.Add(treeModel);
            }
            return treeList.TreeSelectJson();
        }
    }
}
