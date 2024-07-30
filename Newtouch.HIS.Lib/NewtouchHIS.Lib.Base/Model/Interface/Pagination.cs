namespace NewtouchHIS.Lib.Base.Model
{
    /// <summary>
    /// 分页参数封装类
    /// </summary>
    public class Pagination
    {
        /// <summary>
        /// 每页行数
        /// </summary>
        public int? rows { get; set; }

        /// <summary>
        /// 当前页
        /// </summary>
        public int? page { get; set; }

        /// <summary>
        /// 排序列
        /// </summary>
        public string? sidx { get; set; }

        /// <summary>
        /// 排序类型
        /// </summary>
        public string? sord { get; set; }

        /// <summary>
        /// 总记录数
        /// </summary>
        public int? records { get; set; }

        /// <summary>
        /// 总页数
        /// </summary>
        public int? total
        {
            get
            {
                if (records > 0)
                {
                    return records % this.rows == 0 ? records / this.rows : records / this.rows + 1;
                }
                else
                {
                    return 0;
                }
            }
        }

        public Pagination()
        {
            rows = 15;
            page = 1;
            sidx = string.Empty;
            sord = string.Empty;
            records = 0;
        }

        /// <summary>
        /// 初始化（克隆）
        /// </summary>
        /// <param name="pagination"></param>
        public Pagination(Pagination pagination)
        {
            rows = pagination.rows;
            page = pagination.page;
            sidx = pagination.sidx;
            sord = pagination.sord;
            records = pagination.records;
        }
    }

    /// <summary>
    /// 分页请求
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Pagination<T> : Pagination
    {
        /// <summary>
        /// 分页条件
        /// </summary>
        public T? filter { get; set; }
    }

    public class PaginationResult<T> : Pagination
    {
        /// <summary>
        /// 分页数据
        /// </summary>
        public T? pagedata { get; set; }
    }

    /// <summary>
    /// 分页 offset/limit
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class OLPagination<T>
    {
        /// <summary>
        /// 排序方式asc需要你服务器按顺序排序，desc倒序排序
        /// </summary>
        public string? order { get; set; }
        /// <summary>
        /// 排序字段
        /// </summary>
        public string? sort { get; set; }
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
        public string? search { get; set; }

        /// <summary>
        /// 分页时你可以传入你的自定义参数，例如你想每次往服务器请求时带action="getlist"参数function(params) { return params }
        /// </summary>
        public T? queryParams { get; set; }

        public int total { get; set; } = 0;
    }




}
