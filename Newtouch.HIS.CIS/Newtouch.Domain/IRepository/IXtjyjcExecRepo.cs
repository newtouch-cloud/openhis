using Newtouch.Domain.Entity;
using Newtouch.Infrastructure.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Domain.IRepository
{
    public interface IXtjyjcExecRepo : IRepositoryBase<XtjyjcExecEntity>
    {

        void SubmitForm(XtjyjcExecEntity entity, string keyValue);
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="keyValue"></param>
        void DeleteForm(string keyValue);
    }
}
