using AForge.Video;
using AForge.Video.DirectShow;
using System.Drawing.Imaging;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;

namespace CameraApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private FilterInfoCollection videoDevices;
        private VideoCaptureDevice videoSource;
        private List<object> listImage;

        public MainWindow()
        {
            InitializeComponent();
            listImage = new List<object>();
            lvData.ItemsSource = listImage;
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            textPath.Text = Directory.GetCurrentDirectory();

            videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            foreach (FilterInfo device in videoDevices)
            {
                cboVideoDevices.Items.Add(device.Name);
            }
            cboVideoDevices.SelectedIndex = 0;
            videoSource = new VideoCaptureDevice();
            LoadVideoSource();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var image = picture.Source as BitmapImage;
            if (image != null)
            {
                var pathToSave = CreateNewFile(textPath.Text);
                bitmapImageToBytes(image, pathToSave);
                System.Windows.MessageBox.Show("Image saved successfully");
                listImage.Add(new
                {
                    Preview = pathToSave,
                    FileName = Path.GetFileName(pathToSave),
                    Path = pathToSave
                });
            }

            this.lvData.ItemsSource = null;
            this.lvData.ItemsSource = listImage;
        }

        private void bitmapImageToBytes(BitmapImage bitmapImage, string pathToSave)
        {
            Stream writingStream = new FileStream(pathToSave, FileMode.Create);
            var encoder = new JpegBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(bitmapImage));
            encoder.Save(writingStream);
        }

        private string CreateNewFile(string path, int i = 0)
        {
            string filePath = path + "\\New Image";
            if (i > 0)
            {
                filePath += $" ({i})";
            }
            filePath += ".jpg";

            if (File.Exists(filePath) == false)
            {
                return filePath;
            }
            else
            {
                return CreateNewFile(path, ++i);
            }
        }

        private void LoadVideoSource()
        {
            videoSource = new VideoCaptureDevice(videoDevices[cboVideoDevices.SelectedIndex].MonikerString);
            videoSource.NewFrame += VideoCaptureDevice_NewFrame;
            videoSource.Start();
        }

        private void VideoCaptureDevice_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            picture.Dispatcher.Invoke(() =>
            {
                picture.Source = BitmapToImageSource(eventArgs.Frame);
            });
        }

        private BitmapImage BitmapToImageSource(Bitmap bitmap)
        {
            using (var memory = new System.IO.MemoryStream())
            {
                bitmap.RotateFlip(RotateFlipType.RotateNoneFlipX);
                bitmap.Save(memory, ImageFormat.Bmp);
                memory.Position = 0;

                var bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = memory;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();

                return bitmapImage;
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (videoSource.IsRunning)
            {
                videoSource.SignalToStop();
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();

            DialogResult dialog = folderBrowserDialog.ShowDialog();

            if (dialog == System.Windows.Forms.DialogResult.OK)
            {
                textPath.Text = folderBrowserDialog.SelectedPath;
            }
        }
    }
}