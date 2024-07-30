using System;
using System.Linq;

using JSViewer_MVC_Core.Services;


namespace JSViewer_MVC_Core.Implementation.CustomStore
{
    public partial class CustomStoreService : ICustomStoreService
    {
        #region IDataSetsService implementation

        public object GetDataSet(string id)
        {
            var dataset = _db.GetDataset(id);

            if (dataset is null)
                throw new ArgumentException();

            return dataset;
        }

        public object[] GetDataSetsList()
        {
            return _db.GetDatasetsList()
                      .ToArray();
        }

        #endregion
    }
}