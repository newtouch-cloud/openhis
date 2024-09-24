using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NeiMengGuYiBaoApp
{
	public class LogHelper
	{
		public static void LogError(string strError)
		{
            var filePath = "C:\\HISLog\\log_yibao\\LOG.log";
			if (!File.Exists(filePath))//如果不存在就创建文件
			{
				File.Create(filePath);
			}
			using (System.IO.StreamWriter file = new System.IO.StreamWriter(filePath, true))
			{
				file.WriteLine(strError);// 直接追加文件末尾，换行 
			}
		}
	}
}
