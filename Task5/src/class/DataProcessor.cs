using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using OfficeOpenXml;

namespace Task5
{
    public class DataProcessor
    {
        private Configuration _configuration; // Хранит конфигурацию
        public List<DataRecord> DataRecords { get; private set; } = new List<DataRecord>(); // Хранит данные

        private const string ConfigFilePath = "configuration.json"; // Путь к файлу конфигурации
        private const string DataDirectoryPath = "data"; // Папка с данными

        private DataInterpreter _dataInterpreter;

    public DataProcessor()
    {
        _dataInterpreter = new DataInterpreter();
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

                using (var reader = new StreamReader(selectedFile))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        // Преобразование строки в массив байтов
                        byte[] data = ConvertStringToByteArray(line);
                        if (data.Length == 0) // Пропускаем пустые массивы
                        {
                            continue;
                        }

                        DateTime timestamp = DateTime.Now; // Здесь можно использовать реальное время

                        // Интерпретация данных
                        _dataInterpreter.InterpretData(data, timestamp);
                    }
                }

                // Получение DataTable с интерпретированными данными
                DataTable dataTable = _dataInterpreter.GetDataTable();

                // Вывод данных для проверки
                foreach (DataRow row in dataTable.Rows)
                {
                    Console.WriteLine($"Timestamp: {row["Timestamp"]}, DeviceId: {row["DeviceId"]}, Value: {row["Value"]}");
                }

                Console.WriteLine("Данные загружены и интерпретированы.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при загрузке данных: {ex.Message}");
            }
        }

        // Метод для преобразования строки в массив байтов
        private byte[] ConvertStringToByteArray(string line)
        {
            // Удаляем лишние пробелы и символы новой строки
            line = line.Trim();

            // Проверяем, пустая ли строка
            if (string.IsNullOrEmpty(line))
            {
                Console.WriteLine("Пустая строка, пропускаем.");
                return Array.Empty<byte>(); // Возвращаем пустой массив
            }

            // Преобразование строки в массив байтов
            // Предполагается, что строка содержит значения, разделенные точками с запятой
            string[] byteStrings = line.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            List<byte> bytes = new List<byte>();

            foreach (var byteString in byteStrings)
            {
                // Пробуем преобразовать каждую часть в байт
                if (byte.TryParse(byteString, out byte value))
                {
                    bytes.Add(value);
                }
                else
                {
                    Console.WriteLine($"Неверный формат байта: {byteString}, пропускаем.");
                }
            }

            return bytes.ToArray();
        }
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
            catch (System.Text.Json.JsonException ex)
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

        public void ShowData(int start, int end)
        {
            // Проверяем, есть ли данные для отображения
            if (_dataInterpreter.GetDataTable().Rows.Count == 0)
            {
                Console.WriteLine("Нет данных для отображения.");
                return;
            }

            // Проверяем, что индексы находятся в допустимом диапазоне
            if (start < 0 || end < 0 || start >= _dataInterpreter.GetDataTable().Rows.Count || end > _dataInterpreter.GetDataTable().Rows.Count)
            {
                Console.WriteLine("Неверные индексы. Пожалуйста, введите значения в допустимом диапазоне.");
                return;
            }

            // Отображаем данные в заданном диапазоне
            for (int i = start; i < end && i < _dataInterpreter.GetDataTable().Rows.Count; i++)
            {
                DataRow row = _dataInterpreter.GetDataTable().Rows[i];
                Console.WriteLine($"Timestamp: {row["Timestamp"]}, DeviceId: {row["DeviceId"]}, Value: {row["Value"]}");
            }
        }
    }
}
