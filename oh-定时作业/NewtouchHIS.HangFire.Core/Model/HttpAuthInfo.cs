using System.Text;

namespace NewtouchHIS.HangFire.Core.Model
{
    public class HttpAuthInfo
    {
        /// <summary>
        /// Redirects all non-SSL requests to SSL URL
        /// </summary>
        public bool SslRedirect { get; set; } = false;

        /// <summary>
        /// Requires SSL connection to access Hangfire dahsboard. It's strongly recommended to use SSL when you're using basic authentication.
        /// </summary>
        public bool RequireSsl { get; set; } = false;

        /// <summary>
        /// Whether or not login checking is case sensitive.
        /// </summary>
        public bool LoginCaseSensitive { get; set; } = true;

        public bool IsOpenLogin { get; set; } = true;

        public List<UserInfo> Users { get; set; } = new List<UserInfo>();
    }

    public class UserInfo
    {
        public string Login { get; set; }
        public string PasswordClear { get; set; }

        public byte[] Password => Encoding.UTF8.GetBytes(PasswordClear);
    }

}
