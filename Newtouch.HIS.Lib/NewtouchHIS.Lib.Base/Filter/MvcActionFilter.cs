using Microsoft.AspNetCore.Mvc.Filters;

namespace NewtouchHIS.Lib.Base.Filter
{
    public class MvcActionFilter : Attribute, IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            throw new NotImplementedException();
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            throw new NotImplementedException();
        }
    }
}
