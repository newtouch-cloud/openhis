using Newtouch.Herp.Domain.DTO.OutputDto;

namespace Newtouch.Herp.Application.Interface
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