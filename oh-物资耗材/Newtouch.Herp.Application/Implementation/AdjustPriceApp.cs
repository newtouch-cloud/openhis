using System.Linq;
using System.Text;
using Newtouch.Herp.Application.Interface;

namespace Newtouch.Herp.Application.Implementation
{
    /// <summary>
    /// 调价
    /// </summary>
    public class AdjustPriceApp : AppBase, IAdjustPriceApp
    {
        /// <summary>
        /// 执行调价损益
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public string AdjustPriceExecute(string ids)
        {
            var result = new StringBuilder();
            var idList = ids.TrimEnd(',').Split(',').ToList();
            if (idList.Count > 0)
            {
                idList.ForEach(p =>
                {
                    var exRes = new AdjustPriceExecuteProcess(p).Process();
                    if (!exRes.IsSucceed) result.AppendLine(exRes.ResultMsg);
                });
            }
            return result.ToString();
        }
    }
}
