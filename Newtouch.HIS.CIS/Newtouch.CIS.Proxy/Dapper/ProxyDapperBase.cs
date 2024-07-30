namespace Newtouch.CIS.Proxy.Dapper
{
    /// <summary>
    /// 适配
    /// </summary>
    /// <typeparam name="TProxyRequest"></typeparam>
    /// <typeparam name="TProxyResponse"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    public abstract class ProxyDapperBase<TProxyRequest, TProxyResponse, TResult> : IProxyDapperExecutable
    {
        public TProxyRequest Request;
        public TProxyResponse Response;
        /// <summary>
        /// 组建请求报文
        /// </summary>
        /// <returns></returns>
        public abstract void BuildProxyRequest();

        /// <summary>
        /// 执行代理接口
        /// </summary>
        /// <returns></returns>
        public abstract void ExecuteProxy();

        /// <summary>
        /// 提取返回数据
        /// </summary>
        /// <returns></returns>
        public abstract TResult ExtractProxyResponse();

        /// <summary>
        /// 操作
        /// </summary>
        /// <returns></returns>
        public dynamic Execute()
        {
            BuildProxyRequest();
            ExecuteProxy();
            return ExtractProxyResponse();
        }
    }
}