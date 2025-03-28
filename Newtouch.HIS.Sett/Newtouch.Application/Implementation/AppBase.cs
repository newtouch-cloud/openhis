using Newtouch.Common.Operator;

namespace Newtouch.HIS.Application
{
    /// <summary>
    /// 
    /// </summary>
    public class AppBase
    {
        /// <summary>
        /// 
        /// </summary>
        protected OperatorModel UserIdentity;

        /// <summary>
        /// 
        /// </summary>
        protected string OrganizeId
        {
            get
            {
                if (this.UserIdentity == null)
                {
                    return null;
                }
                return this.UserIdentity.OrganizeId;
            }
        }

        public AppBase()
        {
            this.UserIdentity = OperatorProvider.GetCurrent();
        }

    }
}
