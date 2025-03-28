namespace Newtouch.CIS.Proxy
{
    /// <summary>
    /// 贵安远程医疗接口代理
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TV"></typeparam>
    public abstract class GuiAnTelemedicinePlatformProxy<T, TV> where T : class
    {
        protected string Url = Core.Common.Utils.ConfigurationHelper.GetAppConfigValue("GuiAnTelemedicinePlatform_his_01");
        protected string IntegratedModuleUrl = Core.Common.Utils.ConfigurationHelper.GetAppConfigValue("IntegratedModuleUrl");

        /// <summary>
        /// 执行
        /// </summary>
        /// <param name="requestDto"></param>
        /// <returns></returns>
        public abstract TV Execute(T requestDto);


    }
}