using System.Net.Sockets;

namespace PacketAnalyzer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var packets = new List<Packet>()
            {
                new Packet { Depth = 10, Temperature = null, A = 0.15, B = 0.15, C = 0.17 },
                new Packet { Depth = 20, Temperature = 21, A = 0.15, B = 0.16, C = null },
                new Packet { Depth = 30, Temperature = null, A = 0.16, B = 0.18, C = 0.15 },
                new Packet { Depth = 40, Temperature = 22, A = 0.17, B = null, C = null },
                new Packet { Depth = 50, Temperature = 22, A = null, B = null, C = null },
                new Packet { Depth = 60, Temperature = 23, A = 0.18, B = 0.18, C = 0.15 },
                new Packet { Depth = 70, Temperature = 24, A = 0.15, B = 0.16, C = 0.16 }
            };

            var constants = new CompensationConstants { A = 0.0495, B = 0.0486, C = 0.0440 };
            var compensationAlgorithm = new TemperatureCompensationAlgorithm();
            var volumeCalculator = new SimpleVolumeCalculator();

            var engine = new AnalysisEngine(compensationAlgorithm, volumeCalculator);
            var volume = engine.Analyze(packets, constants);

            Console.WriteLine($"Borehole Volume = {volume} m3");
        }
    }
}