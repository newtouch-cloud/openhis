namespace NewtouchHIS.Lib.Base.Model
{
    public class DependencyInjectionConfigure
    {
        /// <summary>
        /// 服务程序集名称
        /// </summary>
        public string ServiceAssemblyName { get; set; }

        /// <summary>
        /// 服务结尾名称
        /// </summary>
        public string ServiceEndsWith { get; set; }

        /// <summary>
        /// 仓储程序集名称
        /// </summary>
        public string RepositoryAssemblyName { get; set; }

        /// <summary>
        /// 仓库结尾名称
        /// </summary>
        public string RepositoryEndsWith { get; set; }
    }
}
