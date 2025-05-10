using System.Net.Http;
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
using Newtonsoft.Json.Linq;

namespace Task7
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            SetWindowBackground();
        }
        private async void SetWindowBackground()
        {
            string weather = await GetWeatherAsync("Anapa");
            switch (weather)
            {
                case "Clear":
                    this.Background = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(255, 255, 0));
                    break;
                case "Clouds":
                    this.Background = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(211, 211, 211));
                    break;
                case "Rain":
                    this.Background = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(0, 0, 255));
                    break;
                case "Snow":
                    this.Background = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(255, 255, 255));
                    break;
                case "Thunderstorm":
                    this.Background = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(169, 169, 169));
                    break;
                default:
                    this.Background = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Color.FromRgb(255, 255, 255));
                    break;
            }
        }

        private async void VerifyButton_Click(object sender, RoutedEventArgs e)
        {
            string name = Name.Text;

            if (IsValidName(name))
            {
                try
                {
                    string weatherData = await GetWeatherAsync("Anapa");
                    SetWindowBackground(); 

                    SecondWindow secondWindow = new SecondWindow(name, weatherData);
                    secondWindow.Show();
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при получении данных о погоде: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Введите корректное имя! Имя не должно быть пустым и не должно содержать цифр.",
                                "Ошибка ввода",
                                MessageBoxButton.OK,
                                MessageBoxImage.Warning);

                Name.BorderBrush = new SolidColorBrush(Colors.Red);
                Name.Background = new SolidColorBrush(Colors.LightPink);
            }
        }

        private bool IsValidName(string name)
        {
            return !string.IsNullOrWhiteSpace(name) && !name.Any(char.IsDigit);
        }

        private async Task<string> GetWeatherAsync(string city)
        {
            string apiKey = "357e91787af57d8e9cef450552f7df20";
            string url = $"http://api.openweathermap.org/data/2.5/weather?q={city}&appid={apiKey}&units=metric";

            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetStringAsync(url);
                var weatherData = JObject.Parse(response);
                return weatherData["weather"][0]["main"].ToString();
            }
        }
    }
}