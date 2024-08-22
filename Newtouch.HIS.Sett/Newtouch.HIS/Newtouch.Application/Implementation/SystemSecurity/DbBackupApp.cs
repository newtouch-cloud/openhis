using Newtouch.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using Newtouch.HIS.Domain.Entity;
using Newtouch.HIS.Domain.IRepository;
using FrameworkBase.MultiOrg.Application;

namespace Newtouch.HIS.Application.SystemSecurity
{
    /// <summary>
    /// 
    /// </summary>
    public class DbBackupApp : AppBase, IDbBackupApp
    {
        private readonly IDbBackupRepository _dbBackupRepository;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="queryJson"></param>
        /// <returns></returns>
        public List<DbBackupEntity> GetList(string queryJson)
        {
            var expression = ExtLinq.True<DbBackupEntity>();
            var queryParam = queryJson.ToJObject();
            if (!queryParam["condition"].IsEmpty() && !queryParam["keyword"].IsEmpty())
            {
                string condition = queryParam["condition"].ToString();
                string keyword = queryParam["keyword"].ToString();
                switch (condition)
                {
                    case "DbName":
                        expression = expression.And(t => t.F_DbName.Contains(keyword));
                        break;
                    case "FileName":
                        expression = expression.And(t => t.F_FileName.Contains(keyword));
                        break;
                }
            }
            return _dbBackupRepository.IQueryable(expression).OrderByDescending(t => t.F_BackupTime).ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        public DbBackupEntity GetForm(string keyValue)
        {
            return _dbBackupRepository.FindEntity(keyValue);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyValue"></param>
        public void DeleteForm(string keyValue)
        {
            _dbBackupRepository.DeleteForm(keyValue);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbBackupEntity"></param>
        public void SubmitForm(DbBackupEntity dbBackupEntity)
        {
            dbBackupEntity.Id = Comm.GuId();
            dbBackupEntity.F_BackupTime = DateTime.Now;
            _dbBackupRepository.ExecuteDbBackup(dbBackupEntity);
        }

    }
}
