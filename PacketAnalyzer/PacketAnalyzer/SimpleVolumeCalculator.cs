using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacketAnalyzer
{
    public class SimpleVolumeCalculator : IVolumeCalculator
    {
        public double CalculateVolume(IEnumerable<Packet> packets)
        {
            var sortedPackets = packets.OrderBy(p => p.Depth).ToList();
            double totalVolume = 0;

            for (int i = 0; i < sortedPackets.Count; i++)
            {
                var previousPacket = i > 0 ? sortedPackets[i - 1] : null;
                var currentPacket = sortedPackets[i];

                double height = previousPacket != null ? currentPacket.Depth - previousPacket.Depth : 0;
                double radius = (currentPacket.A.GetValueOrDefault() + currentPacket.B.GetValueOrDefault() + currentPacket.C.GetValueOrDefault()) / 3;
                
                totalVolume += Math.PI * Math.Pow(radius, 2) * height;
            }

            return totalVolume;
        }
    }
}
