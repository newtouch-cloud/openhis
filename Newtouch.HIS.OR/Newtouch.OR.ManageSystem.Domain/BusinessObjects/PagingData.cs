using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.OR.ManageSystem.Domain
{
    public class PagingData<T>
    {
        /// <summary>
        /// 排序方式asc需要你服务器按顺序排序，desc倒序排序
        /// </summary>
        public string order { get; set; }
        /// <summary>
        /// 起始行，比如你数据库有100条数据，offset等于10表示你服务器需要从第10条数据返回结果
        /// </summary>
        public int offset { get; set; }
        /// <summary>
        /// 每次读取多少条数据
        /// </summary>
        public int limit { get; set; }
        /// <summary>
        /// 用户在输入框搜索的关键词
        /// </summary>
        public string search { get; set; }
        /// <summary>
        /// 分页时你可以传入你的自定义参数，例如你想每次往服务器请求时带action="getlist"参数function(params) { return params }
        /// </summary>
        public T queryParams { get; set; }
    }
}
