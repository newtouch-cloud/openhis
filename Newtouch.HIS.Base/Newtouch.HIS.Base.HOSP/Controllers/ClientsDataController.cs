using Newtouch.Common.Operator;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.Tools;
using System.Collections.Generic;
using System.Text;
using System.Web.Mvc;
using System.Linq;
using Newtouch.Infrastructure;

namespace Newtouch.HIS.Base.HOSP.Controllers
{
    [HandlerLogin]
    public class ClientsDataController : Controller
    {
        private readonly ISysItemsRepository _sysItemsRepository;
        private readonly ISysItemsDetailRepository _sysItemsDetailRepository;
        private readonly ISysModuleDmnService _sysModuleDmnService;
        private readonly ISysOrganizeRepository _sysOrganizeRepository;
        private readonly ISysDepartmentRepository _sysDepartRepo;

        public ClientsDataController(ISysItemsRepository sysItemsRepository
            , ISysItemsDetailRepository sysItemsDetailRepository, ISysModuleDmnService sysModuleDmnService, ISysOrganizeRepository sysOrganizeRepository, ISysDepartmentRepository sysDepartRepo)
        {
            this._sysItemsRepository = sysItemsRepository;
            this._sysItemsDetailRepository = sysItemsDetailRepository;
            this._sysModuleDmnService = sysModuleDmnService;
            this._sysOrganizeRepository = sysOrganizeRepository;
            this._sysDepartRepo = sysDepartRepo;
        }

        [HttpGet]
        [HandlerAjaxOnly]
        public ActionResult GetClientsDataJson()
        {
            var data = new
            {
                //字典 包括无效的
                dataItems = OperatorProvider.GetCurrent().IsRoot==true?this.GetDataItemListRoot(): this.GetDataItemList(),
                ////获取所有组织 包括无效的
                //organize = _sysOrganizeRepository.GetListByTopOrg(Constants.TopOrganizeId).ToList(),
                ////获取所有科室 包括无效的
                //department = _sysDepartRepo.GetListByTopOrg(Constants.TopOrganizeId),
                authorizeMenu = this.GetMenuList(),
                //authorizeButton = this.GetMenuButtonList(),
            };
            return Content(data.ToJson());
        }

        /// <summary>
        /// 字典 包括无效的 (非root)
        /// </summary>
        /// <returns></returns>
        private object GetDataItemList()
        {
            var opr = OperatorProvider.GetCurrent();
            string orgId = null;
            if (!opr.IsRoot && !opr.IsAdministrator)
            {
                orgId = opr.OrganizeId;
            }
            var itemdata = _sysItemsDetailRepository.GetListByOrgId(orgId).ToList();
            Dictionary<string, object> dictionaryItem = new Dictionary<string, object>();
            foreach (var item in _sysItemsRepository.GetList())
            {
                var dataItemList = itemdata.Where(t => t.ItemId.Equals(item.Id)).ToList();
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
        /// 字典 root
        /// </summary>
        /// <returns></returns>
        private object GetDataItemListRoot()
        {
            var itemdata = _sysItemsDetailRepository.GetCommonList();
            Dictionary<string, object> dictionaryItem = new Dictionary<string, object>();
            foreach (var item in _sysItemsRepository.GetList())
            {
                var dataItemList = itemdata.Where(t => t.ItemId.Equals(item.Id)).ToList();
                Dictionary<string, string> dictionaryItemList = new Dictionary<string, string>();
                foreach (var itemList in dataItemList)
                {
                    dictionaryItemList.Add(itemList.Code, itemList.Name);
                }
                dictionaryItem.Add(item.Code, dictionaryItemList);
            }
            return dictionaryItem;
        }

        private object GetMenuList()
        {
            var opr = OperatorProvider.GetCurrent();
            if (opr.IsRoot)
            {
                return null;
            }
            else
            {
                return ToMenuJson(_sysModuleDmnService.GetMenuListByTopOrg(opr.OrganizeId, opr.RoleIdList, opr.IsRoot, opr.IsAdministrator), null);

            }
        }

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
