namespace NewtouchHIS.Lib.Base
{
    //用于定义这三种生命周期的标识接口

    public interface IDependency
    {
    }

    /// <summary>
    /// 瞬时（每次都重新实例）
    /// </summary>
    public interface ITransientDependency : IDependency
    {
    }

    /// <summary>
    /// 单例（全局唯一）
    /// </summary>
    public interface ISingletonDependency : IDependency
    {

    }

    /// <summary>
    /// 一个请求内唯一（线程内唯一）
    /// </summary>
    public interface IScopedDependency : IDependency
    {
    }
}
