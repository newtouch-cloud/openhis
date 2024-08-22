namespace Newtouch.HIS.Domain.IRepository
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISysGloablConfigRepo
    {
        /// <summary>
        /// 根据Code获取配置Value
        /// </summary>
        /// <param name="code"></param>
        /// <param name="orgId"></param>
        /// <returns></returns>
        string GetValueByCode(string code);

        /// <summary>
        /// 根据Code获取配置Value
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        int GetIntValueByCode(string code, int defaultValue = 0);

        /// <summary>
        /// 根据Code获取配置Value
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        bool? GetBoolValueByCode(string code, bool? defaultValue = null);
    }

}
