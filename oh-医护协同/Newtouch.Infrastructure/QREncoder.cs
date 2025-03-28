using System.Drawing;

namespace Newtouch.Infrastructure
{
    /// <summary>
    /// 二维码编码器
    /// </summary>
    public static class QrEncoder
    {
        /// <summary>
        /// 生成二维码
        /// </summary>
        /// <param name="msg">信息</param>
        /// <param name="version">版本 1 ~ 40</param>
        /// <param name="pixel">像素点大小</param>
        /// <param name="iconPath">图标路径</param>
        /// <param name="iconSize">图标尺寸</param>
        /// <param name="iconBorder">图标边框厚度</param>
        /// <param name="whiteEdge">二维码白边</param>
        /// <returns>位图</returns>
        public static Bitmap Code(string msg, int version, int pixel, string iconPath, int iconSize, int iconBorder, bool whiteEdge)
        {
            var codeGenerator = new QRCoder.QRCodeGenerator();
            var codeData = codeGenerator.CreateQrCode(msg, QRCoder.QRCodeGenerator.ECCLevel.M/* 这里设置容错率的一个级别 */, true, true, QRCoder.QRCodeGenerator.EciMode.Utf8, version);
            var code = new QRCoder.QRCode(codeData);
            var icon = new Bitmap(iconPath);
            var bmp = code.GetGraphic(pixel, Color.Black, Color.White, icon, iconSize, iconBorder, whiteEdge);
            return bmp;
        }
    }
}
