using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml.Linq;
using Newtonsoft.Json.Linq;

namespace Task7
{
    /// <summary>
    /// Логика взаимодействия для SecondWindow.xaml
    /// </summary>
    public partial class SecondWindow : Window
    {
        private const string XmlFilePath = "Таблица.xml";

        public SecondWindow(string name, string weatherData)
        {
            InitializeComponent();
            GreetingLabel.Content = $"Привет, {name}!";
            DisplayWeather(weatherData);
            LoadNameHistory();
            SaveNameHistory(name);
            
        }

        public SecondWindow(string name)
        {
            Name = name;
        }

        private void DisplayWeather(string weatherData)
        {
            try
            {
                var weatherJson = JObject.Parse(weatherData);
                var weatherDescription = weatherJson["weather"]?.FirstOrDefault()?["description"]?.ToString();

                if (!string.IsNullOrEmpty(weatherDescription))
                {
                    SetWindowBackground(weatherDescription);
                }
                else
                {
                    MessageBox.Show("Не удалось получить описание погоды.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при получении данных о погоде: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SetWindowBackground(string weatherCondition)
        {

            switch (weatherCondition.ToLower())
            {
                case "clear":
                case "clear sky":
                case "ясно":
                    this.Background = new SolidColorBrush(Colors.Yellow); 
                    break;
                case "clouds":
                case "облачно":
                    this.Background = new SolidColorBrush(Colors.LightGray); 
                    break;
                case "rain":
                case "дождь":
                    this.Background = new SolidColorBrush(Colors.Blue); 
                    break;
                case "snow":
                case "снег":
                    this.Background = new SolidColorBrush(Colors.White); 
                    break;
                case "thunderstorm":
                case "гроза":
                    this.Background = new SolidColorBrush(Colors.DarkGray); 
                    break;
                default:
                    this.Background = new SolidColorBrush(Colors.White); 
                    break;
            }
        }

        private void LoadNameHistory()
        {
            if (File.Exists(XmlFilePath))
            {
                XDocument doc = XDocument.Load(XmlFilePath);
                var history = new List<NameEntry>();

                foreach (var entry in doc.Descendants("Entry"))
                {
                    var dateTimeElement = entry.Element("DateTime");
                    var nameElement = entry.Element("Name");

                    if (dateTimeElement != null && nameElement != null)
                    {
                        string dateTime = dateTimeElement.Value;
                        string name = nameElement.Value;
                        history.Add(new NameEntry(dateTime, name));
                    }
                }

                HistoryDataGrid.ItemsSource = history;
            }
        }

        private void SaveNameHistory(string name)
        {
            var entry = new XElement("Entry",
                new XElement("DateTime", DateTime.Now.ToString()),
                new XElement("Name", name));

            if (!File.Exists(XmlFilePath))
            {
                new XDocument(new XElement("History", entry)).Save(XmlFilePath);
            }
            else
            {
                var doc = XDocument.Load(XmlFilePath);
                if (doc.Root != null)
                {
                    doc.Root.Add(entry);
                    doc.Save(XmlFilePath);
                }
                else
                {
                    throw new InvalidOperationException("Корневой элемент XML-документа отсутствует.");
                }
            }
        }
    }
    public class NameEntry
    {
        public string DateTime { get; set; }
        public string Name { get; set; }
        public NameEntry(string dateTime, string name)
        {
            DateTime = dateTime ?? throw new ArgumentNullException(nameof(dateTime));
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }
    }
}
