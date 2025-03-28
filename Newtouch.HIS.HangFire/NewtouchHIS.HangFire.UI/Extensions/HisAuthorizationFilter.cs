using Hangfire.Dashboard;

namespace NewtouchHIS.HangFire.UI.Extensions
{
    public class HisAuthorizationFilter:IDashboardAuthorizationFilter
    {
        public bool Authorize(DashboardContext context)
        {
            var httpContext = context.GetHttpContext();
            return true;
            // Allow all authenticated users to see the Dashboard (potentially dangerous).
            return httpContext.User.Identity?.IsAuthenticated ?? false;
        }
    }
}
