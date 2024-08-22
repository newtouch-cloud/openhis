using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeiMengGuYiBaoApp.Models.Post.YiBao
{
    public class Post_D002 : PostBase
    {
        public string cfh { get; set; }
        public string kfys { get; set; }
        public string shks { get; set; }
        public string shys { get; set; }
        public string rxFile { get; set; }
        public string signDigest { get; set; }//电子签名返回的处方信息签名值
    }
}
