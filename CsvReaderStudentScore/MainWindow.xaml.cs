using Microsoft.Win32;
using System.Windows;

namespace CsvReaderStudentScore
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<Model.StudentRecord> records;

        public MainWindow()
        {
            InitializeComponent();
            records = new List<Model.StudentRecord>();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            FileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "CSV Files (*.csv)|*.csv";
            fileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            if (fileDialog.ShowDialog() == true)
            {
                string path = fileDialog.FileName;
                var records = Helper.Helper.ReaderCsv<Model.StudentRecord>(path);
                this.records.AddRange(records.ToList());
                double totalMath = 0f;
                List<MathScore> math = new List<MathScore>();

                this.records
                .ForEach(x =>
                    {
                        math.Add(new MathScore
                        {
                            Province = x.Province,
                            Math = x.Mathematics ?? 0,
                            English = x.English ?? 0
                        });

                    });

                var result = math
                    .GroupBy(x => x.Province)
                    .Select(x => new
                    {
                        Province = x.First().Province,
                        Math = x.Average(y => y.Math),
                        English = x.Average(y => y.English),
                    })
                    .OrderByDescending(x => x.Math)
                    .ToList();

                dataGrid.ItemsSource = result;
            }
        }
    }

    class MathScore
    {
        public string Province { get; set; }
        public double Math { get; set; }
        public double English { get; set; }

    }
}