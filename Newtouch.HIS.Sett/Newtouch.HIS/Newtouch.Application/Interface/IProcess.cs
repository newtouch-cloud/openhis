using Newtouch.HIS.Domain.DTO.OutputDto;

namespace Newtouch.HIS.Application.Interface
{
    /// <summary>
    /// 处理方法
    /// </summary>
    public interface IProcess
    {
        /// <summary>
        /// 处理过程
        /// </summary>
        /// <returns></returns>
        ActResult Process();
    }
}
