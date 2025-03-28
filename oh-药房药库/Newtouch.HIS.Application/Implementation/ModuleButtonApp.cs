using FrameworkBase.MultiOrg.Application;
using Newtouch.HIS.Domain.IRepository;
using Newtouch.HIS.Domain.Entity;

namespace Newtouch.HIS.Application
{
    /// <summary>
    /// 按钮App
    /// </summary>
    public class ModuleButtonApp : AppBase, IModuleButtonApp
    {
        private readonly ISysModuleButtonRepo _moduleButtonRepository;


        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public SysModuleButtonEntity GetForm(string keyValue)
        {
            return _moduleButtonRepository.FindEntity(keyValue);
        }

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="keyValue"></param>
        public void DeleteForm(string keyValue)
        {
            _moduleButtonRepository.Delete(t => t.Id == keyValue);
        }

    }
}
