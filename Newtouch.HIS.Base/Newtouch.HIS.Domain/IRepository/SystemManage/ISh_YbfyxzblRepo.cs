using Newtouch.HIS.Domain.Entity.SystemManage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.HIS.Domain.IRepository.SystemManage
{
    public interface ISh_YbfyxzblRepo : IRepositoryBase<Sh_YbfyxzblEntity>
    {
        void SubmitForm(Sh_YbfyxzblEntity entity, string ksCode);
    }
}
