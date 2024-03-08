using ManageSchoolScore.Models;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Threading;

namespace ManageSchoolScore
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer dispatcherTimer = new DispatcherTimer();
        Stopwatch stopWatch = new Stopwatch();
        string currentTime = string.Empty;
        List<Statistics> statistics = new List<Statistics>();
        List<int> years = new List<int>();


        public MainWindow()
        {
            InitializeComponent();

            dispatcherTimer.Tick += new EventHandler(dt_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 1);

            years.Add(2017);
            years.Add(2018);
            years.Add(2019);
            years.Add(2020);
            years.Add(2021);

            this.cbSchoolYear.ItemsSource = years;
            this.cbSchoolYear.SelectedItem = 2017;

            this.cbSchoolYear_2.ItemsSource = years;
            this.cbSchoolYear_2.SelectedItem = 2017;

            this.txtPathFile.Text = "C:\\Users\\Aka Bom\\Desktop\\2017-2021.csv\\2017-2021.csv";

        }

        void dt_Tick(object sender, EventArgs e)
        {
            if (stopWatch.IsRunning)
            {
                TimeSpan ts = stopWatch.Elapsed;
                currentTime = String.Format("{0:00}:{1:00}:{2:00}",
                ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
                timer.Content = currentTime;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = false;
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            var result = openFileDialog.ShowDialog();

            if (result!.Value)
            {
                this.txtPathFile.Text = openFileDialog.FileName;
            }
        }

        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            stopWatch.Start();
            dispatcherTimer.Start();

            var filePath = this.txtPathFile.Text;
            var yearNow = (int)this.cbSchoolYear.SelectedItem;
            //await Repository.Repository.SetUp();
            await Repository.Repository.CommitAsync(filePath);
            stopWatch.Stop();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            stopWatch.Reset();
            timer.Content = "00:00:00";
        }

        private async void Button_Click_3(object sender, RoutedEventArgs e)
        {
            stopWatch.Start();
            dispatcherTimer.Start();
            await Repository.Repository.SetUp();
            this.statistics = await Repository.Repository.Statistics();
            dataGrid.ItemsSource = null;
            dataGrid.ItemsSource = this.statistics;
            stopWatch.Stop();
        }

        private async void Button_Click_4(object sender, RoutedEventArgs e)
        {
            stopWatch.Start();
            dispatcherTimer.Start();
            await Repository.Repository.SetUp();
            int year2 = (int)this.cbSchoolYear_2.SelectedItem;
            var topScores = await Repository.Repository.GetTopScores(year2);
            if (topScores != null || topScores!.Count == 0)
            {
                dgTopScore.ItemsSource = null;
                dgTopScore.ItemsSource = topScores;

                var topScoresStatistics = new List<object>();
                topScoresStatistics.Add(new
                {
                    Năm = year2,
                    A00 = topScores.Where(ts => ts.KhoiThi == "A00").Select(ts => ts.Score).First(),
                    B00 = topScores.Where(ts => ts.KhoiThi == "B00").Select(ts => ts.Score).First(),
                    C00 = topScores.Where(ts => ts.KhoiThi == "C00").Select(ts => ts.Score).First(),
                    D01 = topScores.Where(ts => ts.KhoiThi == "D01").Select(ts => ts.Score).First(),
                    A01 = topScores.Where(ts => ts.KhoiThi == "A01").Select(ts => ts.Score).First(),
                });
                dgTopScore2.ItemsSource = null;
                dgTopScore2.ItemsSource = topScoresStatistics;
            }


            stopWatch.Stop();
        }
    }
}
