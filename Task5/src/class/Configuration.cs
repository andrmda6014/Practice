using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task5
{
    public class Device
    {
        public string Name { get; set; } // Имя устройства
    }
    public class Configuration
    {
        public Dictionary<string, Device> Devices { get; set; } // Словарь для хранения устройств
    }
}
