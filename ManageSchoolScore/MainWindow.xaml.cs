using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Windows;

namespace ManageSchoolScore
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();

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
            this.cbSchoolYear.SelectedItem = years[7];
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
    }
}
