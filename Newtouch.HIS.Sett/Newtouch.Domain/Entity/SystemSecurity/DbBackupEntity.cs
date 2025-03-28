using FrameworkBase.MultiOrg.Domain.Entity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.Entity
{
    public class DbBackupEntity
    {
        public string Id { get; set; }
        public string F_BackupType { get; set; }
        public string F_DbName { get; set; }
        public string F_FileName { get; set; }
        public string F_FileSize { get; set; }
        public string F_FilePath { get; set; }
        public DateTime? F_BackupTime { get; set; }
        public string F_Description { get; set; }
        public DateTime CreateTime { get; set; }
        public int? px { get; set; }
    }
}
