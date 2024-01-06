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
            directories = DirectoriesLookUp(path);
            files = FilesLookUp(path);

            foreach (var directory in directories)
            {
                directoryMetaData.Add(new DirectoryMetaData
                {
                    Type = "Folder",
                    Name = Path.GetFileName(directory),
                    Path = directory
                });
            }

            foreach (var file in files)
            {
                directoryMetaData.Add(new DirectoryMetaData
                {
                    Type = "File",
                    Name = Path.GetFileName(file),
                    Path = file
                });
            }

            this.lvDicrector.ItemsSource = directoryMetaData;
        }

        private static ICollection<string> DirectoriesLookUp(string path)
        {
            List<string> directories = new List<string>();

            foreach (var directory in Directory.GetDirectories(path))
            {
                directories.Add(directory);
            }
            return directories;
        }

        private static ICollection<string> FilesLookUp(string path)
        {
            List<string> files = new List<string>();

            foreach (var file in Directory.GetFiles(path))
            {
                files.Add(file);
            }
            return files;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var path = this.txtFolderPath.Text;
            string folderPath = path + "\\New Folder";

            CreateNewFolder(folderPath);
            LoadData(path);
        }

        private void CreateNewFolder(string path, int i = 0)
        {
            string folderPath = path + "\\New Folder";
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
    }


}