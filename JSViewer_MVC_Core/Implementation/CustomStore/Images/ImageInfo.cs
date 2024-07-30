using LiteDB;
using GrapeCity.ActiveReports.Aspnetcore.Designer.Services;


namespace JSViewer_MVC_Core.Implementation.CustomStore.Images
{
	internal class ImageInfo : IImageInfo
	{
		[BsonField("_id")]
		public string Id { get; set; }

		public string Name { get; set; }

		[BsonField("Mime")]
		public string ContentType { get; set; }

		[BsonField("Image")]
		public byte[] Content { get; set; }

		public byte[] Thumbnail { get; set; }
	}
}
