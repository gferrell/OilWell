using PacketAnalyzer;
using System.Net.Sockets;

namespace PacketAnalyzerTest
{
    public class BoreholeTests
    {
        [Fact]
        public void Test_CompensatePacket()
        {
            var temperatureCompensation = new TemperatureCompensationAlgorithm();
            var constants = new CompensationConstants { A = 0.0287, B = 0.0486, C = 0.0321 };
            var packet = new Packet { Depth = 30, Temperature = 21, A = 0.16, B = 0.18, C = 0.15 };

            var compensatedPacket = temperatureCompensation.Compensate(packet, 21,constants);

            double precision = 0.00001;
            Assert.InRange((double)compensatedPacket.A, 0.096431 - precision, 0.096431 + precision);
            Assert.InRange((double)compensatedPacket.B, 0.183707 - precision, 0.183707 + precision);
            Assert.InRange((double)compensatedPacket.C, 0.101114 - precision, 0.101114 + precision);
        }

        [Fact]
        public void Test_AverageCaliperReading()
        {
            var temperatureCompensation = new TemperatureCompensationAlgorithm();
            var constants = new CompensationConstants { A = 0.0287, B = 0.0486, C = 0.0321 };
            var previousPacket = new Packet { Depth = 20, Temperature = 21, A = 0.15, B = 0.16, C = 0.17 };
            var currentPacket = new Packet { Depth = 30, Temperature = null, A = 0.16, B = 0.18, C = 0.15 };
            var nextPacket = new Packet { Depth = 40, Temperature = 22, A = 0.17, B = null, C = null };

            var averageA = temperatureCompensation.AverageCaliperReading(previousPacket, currentPacket, nextPacket, constants.A, 21, p => p.A);
            var averageB = temperatureCompensation.AverageCaliperReading(previousPacket, currentPacket, nextPacket, constants.B, 21, p => p.B);

            Assert.Equal(0.096432, averageA, 5);
            Assert.Equal(0.173502, averageB, 5);
        }

        [Fact]
        public void Test_CalculateVolume()
        {
            var volumeCalculator = new SimpleVolumeCalculator();
            var packets = new List<Packet>
        {
            new Packet { Depth = 10, Temperature = 21, A = 0.15, B = 0.15, C = 0.17 },
            new Packet { Depth = 20, Temperature = 21, A = 0.15, B = 0.16, C = 0.17 },
            new Packet { Depth = 30, Temperature = 21, A = 0.16, B = 0.18, C = 0.15 },
            new Packet { Depth = 40, Temperature = 22, A = 0.17, B = 0.16, C = 0.15 },
            new Packet { Depth = 50, Temperature = 22, A = 0.18, B = 0.15, C = 0.15 },
            new Packet { Depth = 60, Temperature = 23, A = 0.15, B = 0.15, C = 0.15 },
            new Packet { Depth = 70, Temperature = 24, A = 0.15, B = 0.16, C = 0.16 }
        };

            var volume = volumeCalculator.CalculateVolume(packets);

            Assert.Equal(4.7287950753534354, volume, 9); 
        }

        // ... Add more tests here for other methods
    }
}