using Newtouch.MR.ManageSystem.Domain.Entity;
using Newtouch.MR.ManageSystem.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Newtouch.MR.ManageSystem.Domain.IDomainServices
{
    public interface IRecordDmnService
    {
        medicalRecordVO GetMedicalRecord(string blid, string bllx,string organizeId);
        void LockRecord(string blid, string bllx, string OrganizeId, string UserCode, int isLock);
        bl_ryblVO bl_ryblGetByID(string ID, string organizeId);
        bl_bcjlVO bl_bcjlGetByID(string ID, string organizeId);
        bl_zqwsVO bl_zqwsGetByID(string ID, string organizeId);
        bl_hljlVO bl_hljlGetByID(string ID, string organizeId);
        BlbasyVO bl_basyGetByID(string ID, string organizeId);
        ZymeddocsrelationVO GetZymeddocsrelation(string blId, string organizeId);
        int updateRecordStu(string id, string organizeId, string LastModifierCode, DateTime LastModifyTime,string blzt);
    }
}
