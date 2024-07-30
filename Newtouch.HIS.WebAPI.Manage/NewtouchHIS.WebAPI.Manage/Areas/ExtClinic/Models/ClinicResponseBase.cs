namespace NewtouchHIS.WebAPI.Manage.Areas.ExtClinic
{
    public class ClinicResponseBase<T>
    {
        /// <summary>
        /// 100请求成功 
        /// 20011 授权失败
        /// </summary>
        public string code { get; set; }
        public string msg { get; set; }
        public string type { get; set; }
        public T data { get; set; }
    }

    public enum ClinicResponseCode
    {
        success = 100,
        tokenfailed = 20011
    }
}
