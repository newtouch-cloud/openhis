using FrameworkBase.MultiOrg.Domain.Entity;
using FrameworkBase.MultiOrg.Domain.IDomainServices;
using FrameworkBase.MultiOrg.Infrastructure;
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

namespace FrameworkBase.MultiOrg.Web.Controllers
{
    /// <summary>
    /// Home/Index加载时 默认加载的 缓存数据
    /// </summary>
    public abstract class ClientsDataController : OrgControllerBase
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
            var _itemDmnService = DependencyDefaultInstanceResolver.GetInstance<IItemDmnService>();
            var itemdata = _itemDmnService.GetItemsDetailListByOrgId(this.OrganizeId).ToList();
            Dictionary<string, object> dictionaryItem = new Dictionary<string, object>();
            foreach (var item in _itemDmnService.GetValidItemTypeList())
            {
                var dataItemList = itemdata.Where(t => t.ItemId.Equals(item.Id)).OrderByDescending(p => p.zt == "1").ThenBy(p => p.px).ToList();
                Dictionary<string, string> dictionaryItemList = new Dictionary<string, string>();
                foreach (var itemList in dataItemList)
                {
                    dictionaryItemList.Add(itemList.Code, itemList.Name);
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
            var _itemDmnService = DependencyDefaultInstanceResolver.GetInstance<IItemDmnService>();
            var itemdata = _itemDmnService.GetItemsDetailListByOrgId(this.OrganizeId).ToList();

            var result = _itemDmnService.GetValidItemTypeList().Select(k => new
            {
                Type = k.Code,
                Items = itemdata.Where(t => t.ItemId.Equals(k.Id)).OrderByDescending(p => p.zt == "1")
                    .ThenBy(p => p.px)
                    .Select(p => new { Name = p.Name, Code = p.Code, zt = p.zt })
                    .ToList(),
            }).ToList();

            return result;
        }

        /// <summary>
        /// 获取三方目录对照信息
        /// </summary>
        /// <returns></returns>
        [NonAction]
        public virtual object GetCataloguesComparisonList()
        {
            if (string.IsNullOrWhiteSpace(this.OrganizeId))
            {
                return null;
            }
            var _ttCataloguesComparisonDmnService = DependencyDefaultInstanceResolver.GetInstance<ITTCataloguesComparisonDmnService>();
            var itemdata = _ttCataloguesComparisonDmnService.GetDetailListByOrgId(this.OrganizeId).ToList();

            var result = _ttCataloguesComparisonDmnService.GetValidMainList(this.OrganizeId).Select(k => new
            {
                Code = k.Code,
                TTCode = k.TTCode,
                TTMark = k.TTMark,
                Items = itemdata.Where(t => t.MainId.Equals(k.Id)).OrderByDescending(p => p.zt == "1")
                    .ThenBy(p => p.px)
                    .Select(p => new { Code = p.Code, Name = p.Name, TTCode = p.TTCode, TTName = p.TTName, TTExplain = p.TTExplain })
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
            return ToMenuJson(_sysModuleDmnService.GetMenuList(opr.OrganizeId, opr.RoleIdList, opr.IsRoot, opr.IsAdministrator), null);
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
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="parentId"></param>
        /// <returns></returns>
        private string ToMenuJson(IList<SysModuleEntity> data, string parentId)
        {
            StringBuilder sbJson = new StringBuilder();
            sbJson.Append("[");
            List<SysModuleEntity> entitys = data.Where(t => t.ParentId == parentId).ToList();
            if (entitys.Count > 0)
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
