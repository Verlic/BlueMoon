namespace BlueMoon.UI.Components
{
    using System.Drawing;
    using System.IO;
    using System.Windows.Media.Imaging;

    public static class DrawingImageExtension
    {
        public static BitmapImage ToWpfImage(this Image img)
        {
            var ms = new MemoryStream();  // no using here! BitmapImage will dispose the stream after loading
            img.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);

            var ix = new BitmapImage();
            ix.BeginInit();
            ix.CacheOption = BitmapCacheOption.OnLoad;
            ix.StreamSource = ms;
            ix.EndInit();
            return ix;
        }
    }
}
