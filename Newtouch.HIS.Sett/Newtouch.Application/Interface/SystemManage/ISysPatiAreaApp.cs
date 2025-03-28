namespace Newtouch.HIS.Application
{
    public interface ISysPatiAreaApp
    {
        /// <summary>
        /// 获取病区自动提示
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        object GetbqList();
    }
}
