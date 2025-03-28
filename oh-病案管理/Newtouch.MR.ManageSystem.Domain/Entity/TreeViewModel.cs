using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.MR.ManageSystem.Domain.Entity
{
	public class TreeViewModel
	{

	
		//
		// 摘要:
		//     扩展字段4
		public string Ex4 { get; set; }
		//
		// 摘要:
		//     扩展字段3
		public string Ex3 { get; set; }
		//
		// 摘要:
		//     扩展字段2
		public string Ex2 { get; set; }
		//
		// 摘要:
		//     扩展字段1
		public string Ex1 { get; set; }
		//
		// 摘要:
		//     编码
		public string Code { get; set; }
		//
		// 摘要:
		//     title
		public string title { get; set; }
		//
		public string img { get; set; }
		//
		// 摘要:
		//     扩展字段5
		public string Ex5 { get; set; }
		//
		// 摘要:
		//     是否有下级
		public bool hasChildren { get; set; }
		//
		// 摘要:
		//     是否已加载子节点
		public bool complete { get; set; }
		//
		// 摘要:
		//     是否show check
		public bool showcheck { get; set; }
		//
		// 摘要:
		//     选中状态
		public int? checkstate { get; set; }
		//
		// 摘要:
		//     value
		public string value { get; set; }
		//
		// 摘要:
		//     显示文本
		public string text { get; set; }
		//
		// 摘要:
		//     parentId 对应树形上级的id
		public string parentId { get; set; }
		//
		// 摘要:
		//     id
		public string id { get; set; }
		//
		// 摘要:
		//     是否 展开（if has child）
		public bool isexpand { get; set; }
		//
		// 摘要:
		//     扩展字段6
		public string Ex6 { get; set; }
	}
}
