using GrapeCity.ActiveReports.Aspnetcore.Designer.Services;


namespace JSViewer_MVC_Core.Implementation.CustomStore.Themes
{
	internal class ThemeInfo : IThemeInfo
	{
		public string Id { get; set; }
		public string Title { get; set; }
		public string Dark1 { get; set; }
		public string Dark2 { get; set; }
		public string Light1 { get; set; }
		public string Light2 { get; set; }
		public string Accent1 { get; set; }
		public string Accent2 { get; set; }
		public string Accent3 { get; set; }
		public string Accent4 { get; set; }
		public string Accent5 { get; set; }
		public string Accent6 { get; set; }
		public string MajorFontFamily { get; set; }
		public string MinorFontFamily { get; set; }

		public ThemeInfo() { }

		public ThemeInfo(Theme theme)
		{
			Dark1 = theme.Colors.Dark1;
			Dark2 = theme.Colors.Dark2;

			Light1 = theme.Colors.Light1;
			Light2 = theme.Colors.Light2;

			Accent1 = theme.Colors.Accent1;
			Accent2 = theme.Colors.Accent2;
			Accent3 = theme.Colors.Accent3;
			Accent4 = theme.Colors.Accent4;
			Accent5 = theme.Colors.Accent5;
			Accent6 = theme.Colors.Accent6;

			MajorFontFamily = theme.Fonts.MajorFont.Family;
			MinorFontFamily = theme.Fonts.MinorFont.Family;
		}
	}
}
