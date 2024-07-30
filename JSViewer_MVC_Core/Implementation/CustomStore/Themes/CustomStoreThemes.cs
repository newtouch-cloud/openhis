using System.Linq;

using GrapeCity.ActiveReports.Aspnetcore.Designer;
using GrapeCity.ActiveReports.Aspnetcore.Designer.Services;

using JSViewer_MVC_Core.Services;


namespace JSViewer_MVC_Core.Implementation.CustomStore
{
    public partial class CustomStoreService : ICustomStoreService
    {
        #region IResourcesService implementation

        public Theme GetTheme(string id)
        {
            var theme = _db.GetTheme(id);

            if (theme is null)
                throw new ThemeNotFoundException();

            return theme;
        }

        public IThemeInfo[] GetThemesList()
        {
            return _db.GetThemesList()
                      .ToArray();
        }

        #endregion
    }
}