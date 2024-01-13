using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;

namespace CameraApp
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        private ImageInfo currentImageChoice;

        public Window1(ImageInfo imageInfo)
        {
            InitializeComponent();
            currentImageChoice = imageInfo;
            var imageSource = GetBitmapImage(imageInfo.Path);
            image.Source = imageSource;
            thumbnail.Source = imageSource;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            File.Delete(currentImageChoice.Path);
            this.Close();
        }

        public BitmapImage GetBitmapImage(string path)
        {
            using (var stream = File.OpenRead(path))
            {
                var bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.StreamSource = stream;
                bitmapImage.EndInit();
                bitmapImage.Freeze();
                return bitmapImage;
            }
        }
    }
}
