using System.Text.Json.Serialization;
using System.Collections.Generic;

namespace Task5
{

    public class DataItem
    {
        public List<byte> Byte { get; set; }
        public string Variable { get; set; }
        public string Format { get; set; }
        public string ByteOrder { get; set; }
        public string Unit { get; set; }
        public double? Coefficient { get; set; }
        public double? Offset { get; set; }
    }
}
