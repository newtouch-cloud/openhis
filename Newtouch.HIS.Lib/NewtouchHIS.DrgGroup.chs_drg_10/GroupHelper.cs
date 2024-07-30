using drg_group;

namespace NewtouchHIS.DrgGroup.chs_drg_11
{
    public class GroupHelper
    {
        public static GroupResult GroupRecord(MedicalRecord record)
        {
            GroupProxy grouper = new GroupProxy();
            GroupResult result = grouper.group_record(record.ToString());
            return result;
        }
        public static GroupResult GroupRecord(string record)
        {
            GroupProxy grouper = new GroupProxy();
            GroupResult result = grouper.group_record(record);
            return result;
        }
    }
}