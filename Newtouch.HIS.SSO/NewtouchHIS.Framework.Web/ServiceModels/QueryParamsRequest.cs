using NewtouchHIS.Base.Domain.Model;

namespace NewtouchHIS.Framework.Web.ServiceModels
{
    public class QueryParamsRequest : QueryParamsBase
    {
        /// <summary>
        /// 仅限有效
        /// </summary>
        public bool? validLimit { get; set; }

    }
}
