namespace Newtouch.HIS.Domain.ValueObjects
{
    /// <summary>
    /// 根据药房部门汇总发药总数
    /// </summary>
    public class FyCountVoByYfbm
    {
        /// <summary>
        /// 发药总数
        /// </summary>
        public long fyCount { get; set; }

        /// <summary>
        /// 发药事件 yyyy-MM
        /// </summary>
        public string fysj { get; set; }
    }
}
