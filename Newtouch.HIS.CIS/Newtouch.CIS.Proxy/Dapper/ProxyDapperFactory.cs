using Newtouch.CIS.Proxy.CMMPlatform.DTO.TCM_HIS_01;
using Newtouch.CIS.Proxy.Dapper.CMMPlatform;

namespace Newtouch.CIS.Proxy.Dapper
{
    /// <summary>
    /// 适配工厂
    /// </summary>
    public class ProxyDapperFactory
    {
        /// <summary>
        /// 推送患者信息
        /// </summary>
        /// <param name="patient"></param>
        /// <returns></returns>
        public static IProxyDapperExecutable CreateTcmhis01ProxyDapper(Patient patient)
        {
            return new TcmHis01(patient);
        }
    }
}