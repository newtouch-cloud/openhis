using Newtouch.HIS.Domain.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Newtouch.HIS.Domain.ValueObjects
{
    [NotMapped]
    public class SysWardRoomVO : SysWardRoomEntity
    {
        public string bqmc { get; set; }
        public string ksmc { get; set; }

    }
}
