using Microsoft.AspNetCore.Mvc.Filters;

namespace NewtouchHIS.Lib.Framework.Filter
{
    /// <summary>
    /// MVC过滤器基类
    /// </summary>
    public abstract class ActionFilterAttributeBase : ActionFilterAttribute
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public ActionFilterAttributeBase()
        {

        }

        /// <summary>
        /// 请求对象
        /// </summary>
        //public HttpRequest request
        //{
        //    get
        //    {
        //        return request;
        //    }
        //}

        /// <summary>
        /// 路由值
        /// </summary>
        //public RouteValueDictionary routeValues
        //{
        //    get
        //    {
        //        return this.routeValues;

        //    }
        //}

        /// <summary>
        /// 在路由中获取Int值
        /// </summary>
        /// <param name="name"></param>
        /// <param name="dftVal">默认值0</param>
        /// <returns></returns>
        public int GetRouteIntVal(string name, int dftVal = 0)
        {
            if (string.IsNullOrEmpty(name)) return dftVal;
            return this.GetRouteIntVal(name);
            //object rVal = null;
            //if (rVal == null)
            //    rVal = this.request.Params[name];
            //if (rVal == null)
            //    rVal = this.GetRouteIntVal[name];
            //if (rVal != null)
            //{
            //    int.TryParse(rVal.ToString(), out dftVal);
            //}
            //return dftVal;
        }

        /// <summary>
        /// 在路由中获取String值
        /// </summary>
        /// <param name="name"></param>
        /// <param name="dftVal">默认值null</param>
        /// <returns></returns>
        public string GetRouteStringVal(string name, string dftVal = null)
        {
            return this.GetRouteStringVal(name);
            //if (string.IsNullOrEmpty(name)) return (dftVal ?? string.Empty);
            //object rVal = null;
            //if (rVal == null)
            //    rVal = request.Params[name];
            //if (rVal == null)
            //    rVal = routeValues[name];
            //if (rVal != null)
            //{
            //    return rVal.ToString();
            //}
            //return dftVal;
        }

    }
}
