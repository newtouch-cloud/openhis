namespace Newtouch.HIS.Application
{
    public interface ISysNationApp
    {
        /// <summary>
        /// 获取有效民族下拉框
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        object GetmzList();
    }
}
