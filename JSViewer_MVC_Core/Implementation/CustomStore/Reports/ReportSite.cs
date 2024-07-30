using GrapeCity.ActiveReports;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace JSViewer_MVC_Core.Implementation.CustomStore.Reports
{
	internal class ReportSite : ISite
	{
		private readonly ResourceLocator _resourceLocator;

		public ReportSite(ResourceLocator resourceLocator) =>
			_resourceLocator = resourceLocator;

		public object GetService(Type serviceType) =>
			serviceType == typeof(ResourceLocator) ? _resourceLocator : null;

		public IComponent Component => null;
		public IContainer Container => null;
		public bool DesignMode => false;
		public string Name { get; set; }
	}
}
