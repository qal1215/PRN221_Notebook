using System.IO;
using System.Windows;
using Path = System.IO.Path;

namespace CloneWindowsBrowser
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static ICollection<string> directories = new List<string>();
        private static ICollection<string> files = new List<string>();
        private static ICollection<DirectoryMetaData> directoryMetaData = new List<DirectoryMetaData>();

        private static string selectedFolderPath = string.Empty;

        public MainWindow()
        {
            InitializeComponent();
            //directories = new List<string>();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            folderBrowserDialog.InitialDirectory = Directory.GetCurrentDirectory();
            DialogResult dialogResult = folderBrowserDialog.ShowDialog();

            if (dialogResult == System.Windows.Forms.DialogResult.OK)
            {
                string path = folderBrowserDialog.SelectedPath;
                this.txtFolderPath.Text = path;
                LoadData(path);
            }
        }

        private void LoadData(string path)
        {
            directories.Clear();
            files.Clear();
            directoryMetaData.Clear();
            directories = DirectoriesLookUp(path);
            files = FilesLookUp(path);

            foreach (var directory in directories)
            {
                directoryMetaData.Add(new DirectoryMetaData
                {
                    Type = "📁",
                    Name = Path.GetFileName(directory),
                    Path = directory
                });
            }

            foreach (var file in files)
            {
                directoryMetaData.Add(new DirectoryMetaData
                {
                    Type = "🗄️",
                    Name = Path.GetFileName(file),
                    Path = file
                });
            }

            this.lvDicrector.ItemsSource = null;
            this.lvDicrector.ItemsSource = directoryMetaData;
        }

        private static ICollection<string> DirectoriesLookUp(string path)
        {
            List<string> directories = new List<string>();

            if (Directory.GetDirectories(path).Length <= 0)
            {
                return directories;
            }

            foreach (var directory in Directory.GetDirectories(path))
            {
                directories.Add(directory);
            }
            return directories;
        }

        private static ICollection<string> FilesLookUp(string path)
        {
            List<string> files = new List<string>();

            if (Directory.GetFiles(path).Length <= 0)
            {
                return files;
            }

            foreach (var file in Directory.GetFiles(path))
            {
                files.Add(file);
            }
            return files;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var path = this.txtFolderPath.Text;

            CreateNewFolder(path);
            LoadData(path);
        }

        private void CreateNewFolder(string path, int i = 0)
        {
            string folderPath = path + "\\New Folder ";
            if (i > 0)
            {
                folderPath += $" ({i})";
            }

            if (Directory.Exists(folderPath) == false)
            {
                Directory.CreateDirectory(folderPath);
            }
            else
            {
                CreateNewFolder(path, ++i);
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            var path = selectedFolderPath;

            TriggerDeleteFolder(path);
            LoadData(this.txtFolderPath.Text);
        }

        private void TriggerDeleteFolder(string path)
        {
            if (IsEmptyFolder(path))
            {
                MessageBoxResult result = System.Windows.MessageBox.Show("You are sure about that?");
                if (result == MessageBoxResult.OK)
                {
                    DeleteFolder(path);
                }
            }
            else
            {
                MessageBoxResult result = System.Windows.MessageBox.Show("Folder is not empty, you are sure about that?");
                if (result == MessageBoxResult.OK)
                {
                    DeleteFolder(path);
                }
            }
        }

        private void DeleteFolder(string path)
        {
            Directory.Delete(path);
        }

        private bool IsEmptyFolder(string path)
        {
            return Directory.GetFiles(path).Length == 0 && Directory.GetDirectories(path).Length == 0;
        }

        private void lvDicrector_Selected(object sender, RoutedEventArgs e)
        {
            var item = (DirectoryMetaData)this.lvDicrector.SelectedItem;
            if (item != null)
            {
                selectedFolderPath = item.Path;
            }
        }
    }
}