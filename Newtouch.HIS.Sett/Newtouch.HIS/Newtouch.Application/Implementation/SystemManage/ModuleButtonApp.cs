using Newtouch.HIS.Domain.IRepository;
using Newtouch.HIS.Domain.Entity;

namespace Newtouch.HIS.Application
{
    /// <summary>
    /// 按钮App
    /// </summary>
    public class ModuleButtonApp : IModuleButtonApp
    {
        private readonly ISysModuleButtonRepo _moduleButtonRepository;

        public ModuleButtonApp(ISysModuleButtonRepo moduleButtonRepository)
        {
            this._moduleButtonRepository = moduleButtonRepository;
        }

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
