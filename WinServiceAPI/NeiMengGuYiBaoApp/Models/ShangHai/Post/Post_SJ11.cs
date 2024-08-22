using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQYiBaoInterface.Models.ShangHai.Post
{
    public class Post_SJ11:Post_Base
    {
        public string orgId { get; set; }
        //mz:门诊  zy:住院
        public string djlx { get; set; }
    }
}
