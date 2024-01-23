using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        public MainWindow()
        {
            InitializeComponent();

            dispatcherTimer.Tick += new EventHandler(dt_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 1);

            List<int> years = new List<int>();
            years.Add(2017);
            years.Add(2018);
            years.Add(2019);
            years.Add(2020);
            years.Add(2021);
            years.Add(2022);
            years.Add(2023);
            years.Add(2024);

            this.cbSchoolYear.ItemsSource = years;
            this.cbSchoolYear.SelectedItem = years[0];

            this.txtPathFile.Text = "C:\\Users\\Aka Bom\\Desktop\\2017.csv";

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
            await Repository.Repository.CommitAsync(filePath, yearNow);
            stopWatch.Stop();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            stopWatch.Reset();
            timer.Content = "00:00:00";
        }
    }
}
