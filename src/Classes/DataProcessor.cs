using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using OfficeOpenXml;


namespace Task5.Classes
{
    public class DataProcessor
    {
        private Configuration _configuration; // Хранит конфигурацию
        public List<DataRecord> DataRecords { get; private set; } = new List<DataRecord>(); // Хранит данные

        private const string ConfigFilePath = "configuration.json"; // Путь к файлу конфигурации
        private const string DataDirectoryPath = "data"; // Папка с данными

        public void LoadConfiguration()
        {
            try
            {
                var json = File.ReadAllText(ConfigFilePath);
                _configuration = JsonConvert.DeserializeObject<Configuration>(json);

                if (_configuration == null || _configuration.Devices == null || !_configuration.Devices.Any())
                {
                    Console.WriteLine("Конфигурация не загружена или не содержит устройств.");
                }
                else
                {
                    Console.WriteLine("Конфигурация загружена успешно.");
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Файл конфигурации не найден.");
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"Ошибка при десериализации JSON: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при загрузке конфигурации: {ex.Message}");
            }
        }

        public void ShowConfiguration()
        {
            if (_configuration != null && _configuration.Devices != null)
            {
                foreach (var device in _configuration.Devices)
                {
                    Console.WriteLine($"\"{device.Key}\": {{ \"name\": \"{device.Value.Name}\" }}");
                }
            }
            else
            {
                Console.WriteLine("Конфигурация не загружена или не содержит устройств.");
            }
        }

        public void LoadData()
        {
            try
            {
                var csvFiles = Directory.GetFiles(DataDirectoryPath, "*.csv");
                if (csvFiles.Length == 0)
                {
                    Console.WriteLine("Нет CSV файлов в папке данных.");
                    return;
                }

                Console.WriteLine("Выберите файл для загрузки данных:");
                for (int i = 0; i < csvFiles.Length; i++)
                {
                    Console.WriteLine($"{i + 1}. {Path.GetFileName(csvFiles[i])}");
                }

                int fileChoice = int.Parse(Console.ReadLine()) - 1;
                if (fileChoice < 0 || fileChoice >= csvFiles.Length)
                {
                    Console.WriteLine("Неверный выбор файла.");
                    return;
                }

                string selectedFile = csvFiles[fileChoice];
                DataRecords = new List<DataRecord>(); // Инициализация списка

                using (var reader = new StreamReader(selectedFile))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        var parts = line.Split(',');
                        var record = new DataRecord
                        {
                            Timestamp = DateTime.Now, // Здесь можно использовать реальное время
                            DeviceName = "DefaultDevice" // Устанавливаем фиксированное имя устройства
                        };

                        // Создаем фиксированные элементы данных
                        var dataItem = new DataItem
                        {
                            Variable = "Temperature",
                            Byte = new List<byte> { 0, 1, 2 },
                            Format = "float",
                            ByteOrder = "little-endian",
                            Unit = "Celsius",
                            Coefficient = 1.0,
                            Offset = 0.0
                        };

                        record.DataItems.Add(dataItem);
                        DataRecords.Add(record);
                    }
                }
                Console.WriteLine("Данные загружены.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при загрузке данных: {ex.Message}");
            }
        }

        public void ShowData(int start, int end)
        {
            if (DataRecords == null || !DataRecords.Any())
            {
                Console.WriteLine("Нет данных для отображения.");
                return;
            }

            for (int i = start; i < end && i < DataRecords.Count; i++)
            {
                var record = DataRecords[i];
                Console.WriteLine($"Запись {i + 1}: {record.Timestamp}, Устройство: {record.DeviceName}, Данные: {string.Join(", ", record.DataItems.Select(d => d.Variable + ": " + d.Byte[0]))}");
            }
        }
    }
}
