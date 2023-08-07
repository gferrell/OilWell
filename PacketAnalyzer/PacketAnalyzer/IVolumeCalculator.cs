using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacketAnalyzer
{
    public interface IVolumeCalculator
    {
        double CalculateVolume(IEnumerable<Packet> packets);
    }
}
