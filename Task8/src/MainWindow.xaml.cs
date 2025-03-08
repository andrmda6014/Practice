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

namespace Task8
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool isDragging = false;
        private Point clickPosition;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button)
            {
                textBox.Text = button.Content.ToString();
            }
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            textBox.FontSize = e.NewValue;
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (comboBox.SelectedItem is ComboBoxItem selectedItem)
            {
                switch (selectedItem.Content.ToString())
                {
                    case "Красный":
                        this.Background = Brushes.Red;
                        break;
                    case "Зеленый":
                        this.Background = Brushes.Green;
                        break;
                    case "Синий":
                        this.Background = Brushes.Blue;
                        break;
                }
            }
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listBox.SelectedItem is ListBoxItem selectedItem)
            {
                textBox.Text = selectedItem.Content.ToString();
            }
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            progressBar.IsIndeterminate = true;
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            progressBar.IsIndeterminate = false;
        }

        private void DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (datePicker.SelectedDate.HasValue)
            {
                textBox.Text = datePicker.SelectedDate.Value.ToShortDateString();
            }
        }

        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                // Используйте имя переменной canvas, а не тип Canvas
                Point currentPosition = e.GetPosition(canvas);
                Canvas.SetLeft(ellipse, currentPosition.X - clickPosition.X);
                Canvas.SetTop(ellipse, currentPosition.Y - clickPosition.Y);
            }
        }

        private void Ellipse_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            isDragging = true;
            clickPosition = e.GetPosition(canvas); // Используйте имя переменной canvas
            ellipse.CaptureMouse();
        }

        private void Ellipse_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            isDragging = false;
            ellipse.ReleaseMouseCapture();
        }
    }
}