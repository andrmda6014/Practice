using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task5.Classes
{
    public class DataRecord
    {
        public DateTime Timestamp { get; set; }
        public string DeviceName { get; set; }
        public List<DataItem> DataItems { get; set; } = new List<DataItem>();
    }
}