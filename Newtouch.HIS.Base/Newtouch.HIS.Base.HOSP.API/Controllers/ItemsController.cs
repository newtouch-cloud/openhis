using Autofac;
using Newtouch.HIS.Domain.IDomainServices;
using Newtouch.HIS.Domain.IRepository;
using System.Web.Http;

namespace Newtouch.HIS.Base.HOSP.API.Configs
{
    /// <summary>
    /// 字典
    /// </summary>
    [RoutePrefix("api/Items")]
    public class ItemsController : ApiControllerBase<ItemsController>
    {
        private readonly ISysItemsRepository _sysItemsRepo;
        private readonly ISysItemsDetailRepository _sysItemsDetailRepo;
        private readonly IItemDmnService _itemDmnService;

        /// <summary>
        /// 
        /// </summary>
        public ItemsController(IComponentContext com)
            : base(com)
        {

        }



    }
}
