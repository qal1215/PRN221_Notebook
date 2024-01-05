using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BMI_App
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private double _height;
        private double _weight;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            double heightResult;
            double weightResult;

            if (string.IsNullOrEmpty(this.txtHeight!.Text))
            {
                MessageBox.Show("Please enter your height!");
                return;
            }

            if (string.IsNullOrEmpty(this.txtWeight!.Text))
            {
                MessageBox.Show("Please enter your weight!");
                return;
            }

            if (double.TryParse(this.txtHeight!.Text, out heightResult))
            {
                this._height = heightResult;
            }
            else
            {
                MessageBox.Show("Height invalid!");
                return;
            }

            if (double.TryParse(this.txtWeight!.Text, out weightResult))
            {
                this._weight = weightResult;
            }
            else
            {
                MessageBox.Show("Weight invalid!");
                return;
            }

            var heightSprt = Math.Pow(this._height / 100, 2);
            var bmiIndex = this._weight / heightSprt;
            bmiIndex = Math.Round(bmiIndex, 1);
            this.BMIResult(bmiIndex);
        }

        private void BMIResult(double bmIndex)
        {
            var bc = new BrushConverter();
            this.lbBMI.Content = bmIndex;
            if (BMINormal(bmIndex))
            {
                this.lbBMI.Background = bc.ConvertFromString("#FF3B8A22") as SolidColorBrush;
                this.lbStatus.Content = BMIStatus.NORMAL;
            }
            else if (bmIndex <= 18.5)
            {
                if (BMIUnderweight_1(bmIndex))
                {
                    this.Color_1();
                }
                else if (BMIUnderweight_2(bmIndex))
                {
                    this.Color_2();
                }
                else
                {
                    this.Color_3();
                }

                this.lbStatus.Content = BMIStatus.UNDER_WEIGHT;
            }
            else if (BMIOverweight(bmIndex))
            {
                this.Color_1();
                this.lbStatus.Content = BMIStatus.OVER_WEIGHT;
            }
            else
            {
                if (BMIObesity_1(bmIndex))
                {
                    this.Color_2();
                } else if (BMIObesity_2(bmIndex))
                {
                    this.Color_3();
                }
                else
                {
                    this.Color_4();
                }
                this.lbStatus.Content = BMIStatus.OBESITY;
            }

            this.lbBMI.Visibility = Visibility.Visible;
            this.lbStatus.Visibility = Visibility.Visible;
        }

        private void Color_1()
        {
            this.lbBMI.Background = new BrushConverter().ConvertFromString("#ffe400") as SolidColorBrush;
        }

        private void Color_2()
        {
            this.lbBMI.Background = new BrushConverter().ConvertFromString("#d38888") as SolidColorBrush;
        }

        private void Color_3()
        {
            this.lbBMI.Background = new BrushConverter().ConvertFromString("#bc2020") as SolidColorBrush;
        }

        private void Color_4()
        {
            this.lbBMI.Background = new BrushConverter().ConvertFromString("#8a0101") as SolidColorBrush;
        }

        private bool BMINormal(double bmIndex) => (bmIndex > 18.5) && (bmIndex <= 25);

        private bool BMIUnderweight_1(double bmIndex) => (bmIndex > 17) && (bmIndex <= 18.5);

        private bool BMIUnderweight_2(double bmIndex) => (bmIndex > 16) && (bmIndex <= 17);

        private bool BMIOverweight(double bmIndex) => (bmIndex > 25) && (bmIndex <= 30);

        private bool BMIObesity_1(double bmIndex) => (bmIndex > 30) && (bmIndex <= 35);

        private bool BMIObesity_2(double bmIndex) => (bmIndex > 35) && (bmIndex <= 40);


        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.txtHeight.Text = "";
            this.txtWeight.Text = "";

            this.lbBMI.Visibility = Visibility.Hidden;
            this.lbStatus.Visibility = Visibility.Hidden;
        }
    }

    public static class BMIStatus
    {
        public static readonly string UNDER_WEIGHT = "Underweight";
        public static readonly string NORMAL = "Normal";
        public static readonly string OVER_WEIGHT = "Overweight";
        public static readonly string OBESITY = "Obesity";
    }
}