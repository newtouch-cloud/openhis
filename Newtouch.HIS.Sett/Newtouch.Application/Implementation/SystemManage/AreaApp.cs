using System;
using System.Collections.Generic;
using System.Linq;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;

namespace Newtouch.HIS.Application
{
    /// <summary>
    /// 
    /// </summary>
    public class AreaApp: IAreaApp
    {
        private readonly IAreaRepository _areaRepository;

        public AreaApp(IAreaRepository areaRepository)
        {
            this._areaRepository = areaRepository;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<AreaEntity> GetList()
        {
            return _areaRepository.IQueryable().ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public AreaEntity GetForm(string keyValue)
        {
            return _areaRepository.FindEntity(keyValue);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyValue"></param>
        public void DeleteForm(string keyValue)
        {
            if (_areaRepository.IQueryable().Count(t => t.F_ParentId.Equals(keyValue)) > 0)
            {
                throw new Exception("删除失败！操作的对象包含了下级数据。");
            }
            else
            {
                _areaRepository.Delete(t => t.Id == keyValue);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="areaEntity"></param>
        /// <param name="keyValue"></param>
        public void SubmitForm(AreaEntity areaEntity, string keyValue)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                areaEntity.Modify(keyValue);
                _areaRepository.Update(areaEntity);
            }
            else
            {
                areaEntity.Create(true);
                _areaRepository.Insert(areaEntity);
            }
        }

    }
}
