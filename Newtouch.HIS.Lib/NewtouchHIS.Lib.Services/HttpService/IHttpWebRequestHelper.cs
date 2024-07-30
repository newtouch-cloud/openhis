namespace NewtouchHIS.Lib.Services.HttpService
{
    public interface IHttpWebRequestHelper
    {
        /// <summary>
        /// 通用方法
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <param name="method">默认POST</param>
        /// <returns></returns>
        T Request<T>(string url, string data, string method = "POST");
    }


}
