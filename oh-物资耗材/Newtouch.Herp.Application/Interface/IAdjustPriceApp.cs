namespace Newtouch.Herp.Application.Interface
{
    /// <summary>
    /// 调价
    /// </summary>
    public interface IAdjustPriceApp
    {
        /// <summary>
        /// 执行调价损益
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        string AdjustPriceExecute(string ids);
    }
}