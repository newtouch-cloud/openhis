using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.EMR.Domain.ValueObjects.MedicalRecord
{
    public class FileComparer : IComparer<FileInfo>
    {
        public int Compare(FileInfo x, FileInfo y)
        {
            //FileInfo fi1 = o1 as FileInfo;
            //FileInfo fi2 = o2 as FileInfo;
            return y.LastWriteTime.CompareTo(x.LastWriteTime);//递减
        }
    }
}
