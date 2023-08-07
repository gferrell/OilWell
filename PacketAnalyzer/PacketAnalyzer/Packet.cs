using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacketAnalyzer
{
    public class Packet
    {
        public double Depth { get; set; }
        public double? Temperature { get; set; }
        public double? A { get; set; }
        public double? B { get; set; }
        public double? C { get; set; }
    }
}
