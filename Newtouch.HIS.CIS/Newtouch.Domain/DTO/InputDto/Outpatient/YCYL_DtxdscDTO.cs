using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.Domain.DTO.InputDto.Outpatient
{
    public class YCYL_DtxdscDTO
    {
        public string PatientID { get; set; }
        public string PlaceerOrderNo { get; set; }
        public string FileType { get; set; }
        public string FileNo { get; set; }
        public string FileTotalNum { get; set; }
        public string FileStreams { get; set; }
        public string FileHash { get; set; }
        public string CheckNumber { get; set; }
        public string FileDesc { get; set; }
        /// <summary>
        /// 远程医疗上传，压缩文件前缀（his机构标志）
        /// </summary>
        public string Sjscjgmc { get; set; }
    }
}
