using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacketAnalyzer
{
    public class AnalysisEngine
    {
        private readonly ICompensationAlgorithm _compensationAlgorithm;
        private readonly IVolumeCalculator _volumeCalculator;

        public AnalysisEngine(ICompensationAlgorithm compensationAlgorithm, IVolumeCalculator volumeCalculator)
        {
            _compensationAlgorithm = compensationAlgorithm;
            _volumeCalculator = volumeCalculator;
        }

        public double Analyze(IEnumerable<Packet> packets, CompensationConstants constants)
        {
            double? lastValidTemperature = null;
            var sortedPackets = packets.OrderBy(p => p.Depth).ToList();

            for (int i = 0; i < sortedPackets.Count; i++)
            {
                var previousPacket = i > 0 ? sortedPackets[i - 1] : null;
                var nextPacket = i < sortedPackets.Count - 1 ? sortedPackets[i + 1] : null;

                sortedPackets[i] = _compensationAlgorithm.Compensate(sortedPackets[i], lastValidTemperature, constants, previousPacket, nextPacket);

                if (sortedPackets[i].Temperature.HasValue) //Save the last valid temperature if there is one.
                {
                    lastValidTemperature = sortedPackets[i].Temperature;
                }
            }

            return _volumeCalculator.CalculateVolume(sortedPackets);
        }
    }
}
