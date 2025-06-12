using System;

namespace Barfas_1.Models
{
    public class RfidTag
    {
        public string TagId { get; set; }
        public string AntennaPort { get; set; }
        public int Rssi { get; set; }
        public DateTime ReadTime { get; set; }
    }
} 