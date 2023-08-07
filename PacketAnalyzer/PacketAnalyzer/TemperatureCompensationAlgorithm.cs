using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacketAnalyzer
{
    public class TemperatureCompensationAlgorithm : ICompensationAlgorithm
    {
        public Packet Compensate(Packet packet, double? lastValidTemperature, CompensationConstants constants, Packet previousPacket = null, Packet nextPacket = null)
        {
            var temperature = packet.Temperature ?? lastValidTemperature ?? 20;
            packet.A = packet.A.HasValue ? packet.A * temperature * constants.A : AverageCaliperReading(previousPacket, packet, nextPacket, constants.A, temperature, p => p.A);
            packet.B = packet.B.HasValue ? packet.B * temperature * constants.B : AverageCaliperReading(previousPacket, packet, nextPacket, constants.B, temperature, p => p.B);
            packet.C = packet.C.HasValue ? packet.C * temperature * constants.C : AverageCaliperReading(previousPacket, packet, nextPacket, constants.C, temperature, p => p.C);
            ;
            return packet;
        }

        public double AverageCaliperReading(Packet previousPacket, Packet currentPacket, Packet nextPacket, double compensationConstant, double temperature, Func<Packet, double?> selector)
        {
            // Get the current, previous and next readings
            var previousValue = previousPacket != null && selector(previousPacket).HasValue ? (selector(previousPacket) ?? 0)  * compensationConstant * temperature : 0; //Should this be compensated again?
            var currentValue = currentPacket != null && selector(currentPacket).HasValue ? (selector(currentPacket) ?? 0) * compensationConstant * temperature : 0;
            var nextValue = nextPacket != null && selector(nextPacket).HasValue ? (selector(nextPacket) ?? 0) * compensationConstant * temperature : 0;

            // Count only valid readings
            var count = (previousValue > 0 ? 1 : 0) + (currentValue > 0 ? 1 : 0) + (nextValue > 0 ? 1 : 0);
           
            // Return the average of valid readings or zero if no valid values
            double average = count > 0 ? (previousValue + currentValue + nextValue) / count : 0;
            return average;
        }


    }
}
