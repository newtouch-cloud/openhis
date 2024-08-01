using FrameworkBase.Domain.Entity;
using FrameworkBase.Domain.IDomainServices;
using FrameworkBase.Domain.IRepository;
using FrameworkBase.Infrastructure;
using Newtouch.Common.Operator;
using Newtouch.Core.Common;
using Newtouch.HIS.Web.Core.Extensions;
using Newtouch.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web.Mvc;
using static Newtouch.HIS.Web.Core.Extensions.EnumExtensions;

namespace FrameworkBase.Web.Controllers
{
    /// <summary>
    /// Home/Index加载时 默认加载的 缓存数据
    /// </summary>
    public abstract class ClientsDataController : BaseController
    {
        /// <summary>
        /// 字典 分类仅有效的，字典项包括无效的（仅用于在列表中Code切换成Name显示）
        /// {"OrganizeType":{"001":"集团","002":"区域","Hospital":"医院","Clinic":"诊所"}}
        /// </summary>
        /// <returns></returns>
        [NonAction]
        [Obsolete("不建议再使用，用GetItemDetailsList的格式代替")]
        public virtual object GetDataItemList()
        {
            var _sysItemsTypeRepo = DependencyDefaultInstanceResolver.GetInstance<ISysItemsTypeRepo>();
            var _sysItemsDataRepo = DependencyDefaultInstanceResolver.GetInstance<ISysItemsDataRepo>();

            var itemdata = _sysItemsDataRepo.IQueryable().ToList();
            Dictionary<string, object> dictionaryItem = new Dictionary<string, object>();
            foreach (var item in _sysItemsTypeRepo.IQueryable().Where(p => p.zt == "1").ToList())
            {
                var dataItemList = itemdata.Where(t => t.ItemId.Equals(item.Id)).OrderByDescending(p => p.zt == "1").ThenBy(p => p.px).ToList();
                Dictionary<string, string> dictionaryItemList = new Dictionary<string, string>();
                foreach (var itemDetail in dataItemList)
                {
                    if (!dictionaryItemList.ContainsKey(itemDetail.Code))
                    {
                        dictionaryItemList.Add(itemDetail.Code, itemDetail.Name);
                    }
                }
                dictionaryItem.Add(item.Code, dictionaryItemList);
            }
            return dictionaryItem;
        }

        /// <summary>
        /// 字典 分类仅有效的，字典项包括无效的（可用于显示成select）
        /// {Type:"OrganizeType",Items:[{Code:"001",Name:"集团",zt:"1"},{Code:"002",Name:"区域",zt:"1"}]}
        /// </summary>
        /// <returns></returns>
        [NonAction]
        public virtual object GetItemDetailsList()
        {
            var _sysItemsTypeRepo = DependencyDefaultInstanceResolver.GetInstance<ISysItemsTypeRepo>();
            var _sysItemsDataRepo = DependencyDefaultInstanceResolver.GetInstance<ISysItemsDataRepo>();

            var itemdata = _sysItemsDataRepo.IQueryable().ToList();

            var result = _sysItemsTypeRepo.IQueryable().Where(p => p.zt == "1").ToList()
                .Select(k => new {
                Type = k.Code,
                Items = itemdata.Where(t => t.ItemId.Equals(k.Id)).OrderByDescending(p => p.zt == "1").ThenBy(p => p.px)
             .Select(p => new { Name = p.Name, Code = p.Code, zt = p.zt })
             .ToList(),
            }).ToList();

            return result;
        }

        /// <summary>
        /// 获取菜单
        /// </summary>
        /// <returns></returns>
        [NonAction]
        public virtual object GetMenuList()
        {
            var _sysModuleDmnService = DependencyDefaultInstanceResolver.GetInstance<ISysModuleDmnService>();

            var opr = OperatorProvider.GetCurrent();
            return ToMenuJson(_sysModuleDmnService.GetMenuList(opr.RoleIdList, opr.IsRoot), null);
        }

        /// <summary>
        /// 获取系统所有定义的枚举
        /// </summary>
        /// <returns></returns>
        [NonAction]
        public virtual object GetEnumList(params string[] namespaceList)
        {
            List<Assembly> asmList = new List<Assembly>();
            asmList.Add(typeof(DbLogType).Assembly);
            if (namespaceList != null)
            {
                foreach (var namespaceItem in namespaceList)
                {
                    asmList.Add(Assembly.Load(namespaceItem));
                }
            }
            asmList.Add(Assembly.Load("Newtouch.Core.Common"));

            var result = new Dictionary<string, List<EnumItemInfo>>();

            foreach (var asm in asmList)
            {
                var enmList = asm.GetTypes().Where(p => p.BaseType != null && p.BaseType.FullName == "System.Enum"
                && p.DeclaringType == null).ToList();

                foreach (var enm in enmList)
                {
                    //DbLogType DbLogType_Ex视为同一个枚举
                    var typeName = enm.Name.EndsWith("_Ex") ? enm.Name.Substring(0, enm.Name.Length - 3) : enm.Name;

                    var items = enm.getEnumNameValueDescInfo().ToList();
                    if (!result.ContainsKey(typeName))
                    {
                        result.Add(typeName, items);
                    }
                    else
                    {
                        result[typeName].AddRange(items);
                    }
                }
            }

            return result.Select(p => new { Type = p.Key, Items = p.Value });
        }

        /// <summary>
        /// 将菜单列表 解析成json串
        /// </summary>
        /// <param name="data"></param>
        /// <param name="parentId"></param>
        /// <returns></returns>
        private string ToMenuJson(IList<SysModuleEntity> data, string parentId)
        {
            StringBuilder sbJson = new StringBuilder();
            sbJson.Append("[");
            List<SysModuleEntity> entitys = data != null ? data.Where(t => t.ParentId == parentId).ToList() : null;
            if (entitys != null && entitys.Count > 0)
            {
                foreach (var item in entitys)
                {
                    string strJson = item.ToJson();
                    strJson = strJson.Insert(strJson.Length - 1, ",\"ChildNodes\":" + ToMenuJson(data, item.Id) + "");
                    sbJson.Append(strJson + ",");
                }
                sbJson = sbJson.Remove(sbJson.Length - 1, 1);
            }
            sbJson.Append("]");
            return sbJson.ToString();
        }

    }
}