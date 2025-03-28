namespace Newtouch.HIS.Application
{
    public interface ISysStaffApp
    {

        /// <summary>
        /// 有效员工自动提示
        /// </summary>
        /// <returns></returns>
        object GetValidStaffList();

        /// <summary>
        /// 根据Account（工号）获取人员编号
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        int GetSysRybhByAccont(string account);

    }
}
