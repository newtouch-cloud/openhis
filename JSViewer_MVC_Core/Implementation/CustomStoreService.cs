using System;

using JSViewer_MVC_Core.Services;
using JSViewer_MVC_Core.Implementation.Storage;

namespace JSViewer_MVC_Core.Implementation.CustomStore
{
    public partial class CustomStoreService : ICustomStoreService
    {
        private ICustomStorage _db;

        public CustomStoreService(ICustomStorage database)
        {
            _db = database;
        }

        public void Dispose()
        {
            _db.Dispose();
        }

        #region IResourcesService implementation

        public Uri GetBaseUri()
        {
            return null;
        }

        #endregion
    }
}
