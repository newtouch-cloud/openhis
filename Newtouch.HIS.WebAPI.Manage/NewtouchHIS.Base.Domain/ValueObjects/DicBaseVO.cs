namespace NewtouchHIS.Base.Domain.ValueObjects
{
    public class DicBaseVO
    {
        public string? Code { get; set; }
        public string? Name { get; set; }
    }

    public class DicBaseEnumVO
    {
        public int Value { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
    }
}
