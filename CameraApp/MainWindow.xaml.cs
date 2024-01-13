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
        private List<ImageInfo> listImage;

        private object currentImageChoice;

        public MainWindow()
        {
            InitializeComponent();
            listImage = new List<ImageInfo>();
            lvData.ItemsSource = listImage;
            Window_Loaded();
        }

        private void Window_Loaded()
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
                listImage.Add(new ImageInfo
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
            Bitmap bitmap = BitmapImage2Bitmap(bitmapImage);
            BitmapSource bitmapSource = ImageToBitmapSource(resizeImage(bitmap, 100, 100, true));
            encoder.Frames.Add(BitmapFrame.Create(bitmapImage));
            encoder.Save(writingStream);
        }

        private BitmapSource ImageToBitmapSource(Image image)
        {
            var bitmap = new Bitmap(image);
            var bitSrc = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
                               bitmap.GetHbitmap(),
                                              IntPtr.Zero,
                                                             Int32Rect.Empty,
                                                                            BitmapSizeOptions.FromEmptyOptions());
            return bitSrc;
        }

        private Bitmap BitmapImage2Bitmap(BitmapImage bitmapImage)
        {
            using (MemoryStream outStream = new MemoryStream())
            {
                BitmapEncoder enc = new BmpBitmapEncoder();
                enc.Frames.Add(BitmapFrame.Create(bitmapImage));
                enc.Save(outStream);
                Bitmap bitmap = new Bitmap(outStream);

                return new Bitmap(bitmap);
            }
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

        private Image resizeImage(Bitmap imgToResize, int width, int height, bool keepRatio = false)
        {
            if (keepRatio)
            {
                int sourceWidth = imgToResize.Width;
                int sourceHeight = imgToResize.Height;

                float nPercent = 0;
                float nPercentW = 0;
                float nPercentH = 0;

                nPercentW = ((float)width / (float)sourceWidth);
                nPercentH = ((float)height / (float)sourceHeight);

                if (nPercentH < nPercentW)
                    nPercent = nPercentH;
                else
                    nPercent = nPercentW;

                width = (int)(sourceWidth * nPercent);
                height = (int)(sourceHeight * nPercent);
            }

            Image img = (Image)imgToResize;
            Image b = new Bitmap(width, height);
            Graphics g = Graphics.FromImage(b);

            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            g.DrawImage(img, 0, 0, width, height);
            g.Dispose();

            return b;
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

        private void lvData_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var item = (ImageInfo)this.lvData.SelectedItem;
            Window1 window1 = new Window1(item);
            window1.ShowDialog();
        }
    }
}