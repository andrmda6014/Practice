using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Task10
{
    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class Timer : UserControl
    {
        private List<BitmapImage> _images;
        private int _currentIndex;
        private System.Timers.Timer _timer;

        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(int), typeof(Timer), new PropertyMetadata(0, OnValueChanged));

        public int Value
        {
            get => (int)GetValue(ValueProperty);
            set => SetValue(ValueProperty, value);
        }

        public Timer()
        {
            InitializeComponent();
            _images = new List<BitmapImage>();
            _currentIndex = 0;
            _timer = new System.Timers.Timer();
            _timer.Elapsed += Timer_Elapsed;
        }

        public void AddImage(string imagePath)
        {
            var bitmap = new BitmapImage(new Uri(imagePath, UriKind.RelativeOrAbsolute));
            _images.Add(bitmap);
        }

        private static void OnValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as Timer;
            if (control != null)
            {
                control.UpdateImage();
            }
        }

        private void UpdateImage()
        {
            if (_images.Count == 0)
            {
                // Заглушка
                ImageDisplay.Source = null; // Укажите изображение-заглушку
                return;
            }

            if (Value < 0 || Value >= _images.Count)
            {
                // Заглушка
                ImageDisplay.Source = null; // Укажите изображение-заглушку
            }
            else
            {
                ImageDisplay.Source = _images[Value];
            }
        }

        public void StartAnimation(int interval)
        {
            _timer.Interval = interval;
            _timer.Start();
        }

        public void StopAnimation()
        {
            _timer.Stop();
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                _currentIndex = (_currentIndex + 1) % _images.Count;
                Value = _currentIndex;
            });
        }
    }
}
