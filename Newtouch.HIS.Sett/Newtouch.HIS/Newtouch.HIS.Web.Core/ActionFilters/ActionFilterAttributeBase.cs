using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Newtouch.HIS.Web.Core.ActionFilters
{
    /// <summary>
    /// 
    /// </summary>
    public class ActionFilterAttributeBase : ActionFilterAttribute
    {

        public ActionFilterAttributeBase()
        {
            ;
        }

        /// <summary>
        /// 
        /// </summary>
        public HttpRequest request
        {
            get
            {
                return HttpContext.Current.Request;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public RouteValueDictionary routeValues
        {
            get
            {
                return HttpContext.Current.Request.RequestContext.RouteData.Values;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="dftVal"></param>
        /// <returns></returns>
        public int GetRouteIntVal(string name, int dftVal = 0)
        {
            if (string.IsNullOrEmpty(name)) return dftVal;
            object rVal = null;
            //if (request.HttpMethod.ToLower() == "post")
            //    rVal = request.Form[name];
            if (rVal == null)
                rVal = request.Params[name];
            if (rVal == null)
                rVal = routeValues[name];
            if (rVal != null)
            {
                int.TryParse(rVal.ToString(), out dftVal);
            }
            return dftVal;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="dftVal"></param>
        /// <returns></returns>
        public string GetRouteStringVal(string name, string dftVal = null)
        {
            if (string.IsNullOrEmpty(name)) return (dftVal ?? string.Empty);
            object rVal = null;
            //if(request.HttpMethod.ToLower() == "post")
            //    rVal = request.Form[name];
            if (rVal == null)
                rVal = request.Params[name];
            if (rVal == null)
                rVal = routeValues[name];
            if (rVal != null)
            {
                return rVal.ToString();
            }
            return dftVal;
        }

    }
}
