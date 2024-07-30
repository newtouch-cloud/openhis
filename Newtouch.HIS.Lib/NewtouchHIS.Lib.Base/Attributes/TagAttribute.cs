namespace NewtouchHIS.Lib.Base.Attributes
{
    public class TagAttribute : Attribute
    {
    }

    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter,
AllowMultiple = false)]
    public class DbTagAttribute : TagAttribute
    {
        public object _dbId { get; set; }

        public DbTagAttribute(object dbId)
        {
            this._dbId = dbId;
        }
    }

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
    public class ClientDataTagAttribute : TagAttribute
    {
        public string _clientId { get; set; }

        public ClientDataTagAttribute(string clientId)
        {
            this._clientId = clientId;
        }
    }
}
