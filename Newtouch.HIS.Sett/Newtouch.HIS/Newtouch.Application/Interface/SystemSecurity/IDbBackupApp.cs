using System.Collections.Generic;
using Newtouch.HIS.Domain.Entity;

namespace Newtouch.HIS.Application
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDbBackupApp
    {
        List<DbBackupEntity> GetList(string queryJson);

        DbBackupEntity GetForm(string keyValue);
        
        void DeleteForm(string keyValue);

        void SubmitForm(DbBackupEntity dbBackupEntity);

    }
}
