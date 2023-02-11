using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing;

namespace System
{
    public static class ServicoImagem
    {
        public static Image ToImage(this byte[] valor)
        {
            if (valor == null)
                return null;
            var ms = new MemoryStream(valor);
            return Image.FromStream(ms);
        }

        public static byte[] ToByteArray(this Image valor)
        {
            if (valor == null)
                return null;
            var ms = new MemoryStream();
            valor.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            return ms.ToArray();
        }

        public static string ToStringBase64(this byte[] imagem)
        {
            try
            {
                return System.Convert.ToBase64String(imagem);
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }

        public static Image ResizeImage(this Image originalImage, int newSize)
        {
            if (originalImage.Width <= newSize)
                newSize = originalImage.Width;

            var newHeight = originalImage.Height * newSize / originalImage.Width;

            if (newHeight > newSize)
            {
                // Resize with height instead
                newSize = originalImage.Width * newSize / originalImage.Height;
                newHeight = newSize;
            }

            return Resize(originalImage, newSize, newHeight);
        }

        private static Image Resize(Image image, int width, int height)
        {

            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }


    }
}
