using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacketAnalyzer
{
    public interface ICompensationAlgorithm
    {
        Packet Compensate(Packet packet, double? lastValidTemperature, CompensationConstants constants, Packet previousPacket = null, Packet nextPacket = null);
    }
}
