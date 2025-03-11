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
using System.Timers;

namespace Task10
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool _isAnimating;

        public MainWindow()
        {
            InitializeComponent();
            // Добавьте изображения в контрол
            ImageTimer.AddImage("pack://application:,,,/Resources/image1.png");
            ImageTimer.AddImage("pack://application:,,,/Resources/image2.png");
            ImageTimer.AddImage("pack://application:,,,/Resources/image3.png");
        }

        private void NextImage_Click(object sender, RoutedEventArgs e)
        {
            ImageTimer.Value = (ImageTimer.Value + 1) % 3; // 3 - количество изображений
        }

        private void Apply_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(IntervalTextBox.Text, out int interval))
            {
                ImageTimer.StartAnimation(interval);
            }
        }

        private void StartStop_Click(object sender, RoutedEventArgs e)
        {
            if (_isAnimating)
            {
                ImageTimer.StopAnimation();
            }
            else
            {
                ImageTimer.StartAnimation(1000); // Используйте значение по умолчанию
            }
            _isAnimating = !_isAnimating;
        }
    }
}