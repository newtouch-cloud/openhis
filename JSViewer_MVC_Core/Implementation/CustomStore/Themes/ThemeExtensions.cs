using System.IO;
using System.Xml.Serialization;

using GrapeCity.ActiveReports.Aspnetcore.Designer.Services;


namespace JSViewer_MVC_Core.Implementation.CustomStore.Themes
{
	public static class ThemeExtensions
	{
		/// <summary>
		/// Serialize <see cref="Theme" /> object to XML.
		/// </summary>
		/// <param name="theme"><see cref="Theme" /> object to serialize.</param>
		/// <returns>Serialized theme <see cref="Stream"/>.</returns>
		public static Stream ToStream(this Theme theme)
		{
			var stream = new MemoryStream();

			var xml = new XmlSerializer(typeof(Theme));
			xml.Serialize(stream, theme);

			stream.Seek(0, SeekOrigin.Begin);
			return stream;
		}
	}
}
