using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task5
{
    public class DataInterpreter
    {
        private DataTable _dataTable;

        public DataInterpreter()
        {
            // Инициализация DataTable
            _dataTable = new DataTable();
            _dataTable.Columns.Add("Timestamp", typeof(DateTime));
            _dataTable.Columns.Add("DeviceId", typeof(int));
            _dataTable.Columns.Add("Value", typeof(double));
        }

        public void InterpretData(byte[] data, DateTime timestamp)
        {
            if (data.Length < 2)
            {
                Console.WriteLine("Недостаточно данных для интерпретации.");
                return;
            }

            // Определение ID устройства (второй байт)
            int deviceId = data[1];

            // Пример интерпретации данных в зависимости от ID устройства
            double value = 0;

            // Пример: если ID устройства 1, интерпретируем данные по определенному алгоритму
            if (deviceId == 1)
            {
                // Пример: конвертация байтов в число (например, 2 байта)
                if (data.Length >= 4) // Убедитесь, что есть достаточно байтов
                {
                    value = BitConverter.ToUInt16(data, 2); // Конвертация 2 байтов в число
                }
            }
            else if (deviceId == 2)
            {
                // Другой алгоритм интерпретации для другого устройства
                if (data.Length >= 5)
                {
                    value = BitConverter.ToUInt32(data, 2); // Конвертация 4 байтов в число
                }
            }
            // Добавьте дополнительные условия для других ID устройств по мере необходимости

            // Сохранение интерпретированных данных в DataTable
            _dataTable.Rows.Add(timestamp, deviceId, value);
        }

        public DataTable GetDataTable()
        {
            return _dataTable;
        }
    }
}
