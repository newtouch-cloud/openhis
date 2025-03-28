using System.Collections.Generic;

namespace Newtouch.HIS.Domain.BusinessObjects
{
    public class BsTreeViewBO
    {
        public string nodeId { get; set; }
        public string text { get; set; }
        public string href { get; set; }
        public string[] tags { get; set; }
        public string icon { get; set; }
        public bool selected { get; set; } = false;
        public string backColor { get; set; }
        public string color { get; set; }
    }

    public class BsTreeViewNodesBO : BsTreeViewBO
    {
        public List<BsTreeViewBO> nodes { get; set; }
    }
}
