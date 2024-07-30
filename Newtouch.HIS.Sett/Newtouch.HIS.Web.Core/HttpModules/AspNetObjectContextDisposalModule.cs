using System;
using System.Web;

namespace Newtouch.HIS.Web.Core.HttpModules
{
    /// <summary>
    /// 
    /// </summary>
    public class AspNetObjectContextDisposalModule : IHttpModule
    {
        private static Action _ac;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="func"></param>
        public static void Register(Action ac)
        {
            _ac = ac;
        }

        public void Dispose()
        {
            //throw new NotImplementedException();
        }

        public void Init(HttpApplication context)
        {
            context.EndRequest += new EventHandler(EndOfHttpRequest);
        }

        /// <summary>
        /// Is invoked at the end of a HTTP request. Disposes the shared ObjectContext instance. 
        /// </summary>
        private void EndOfHttpRequest(object sender, EventArgs e)
        {
            DisposeObjectContext();
        }

        /// <summary>
        /// Disposes the shared ObjectContext instance.
        /// </summary>
        public static void DisposeObjectContext()
        {
            //_ac?.Invoke();
            if (_ac != null) 
            {
                _ac.Invoke();
            }
        }

    }

}
