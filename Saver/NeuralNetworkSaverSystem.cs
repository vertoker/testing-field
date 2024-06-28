using System;
using System.Collections.Generic;
using System.IO;

namespace NeuralNetworkPipeline.Saver
{
    public static class NeuralNetworkSaverSystem
    {
        public static void Save(NeuralNetwork nn, params string[] paths)
        {
            Save(nn, Path.Combine(paths));
        }
        public static void Save(NeuralNetwork nn, string path)
        {
            var bytes = Serializer(nn);
            File.WriteAllBytes(path, bytes);
        }
        public static NeuralNetwork Load(string path)
        {
            var bytes = File.ReadAllBytes(path);
            return Deserializer(bytes);
        }
        public static bool TryLoad(string path, out NeuralNetwork nn)
        {
            if (!File.Exists(path))
            {
                nn = null;
                return false;
            }
            try
            {
                var bytes = File.ReadAllBytes(path);
                nn = Deserializer(bytes, out var counter);
                return counter == bytes.Length;
            }
            catch (Exception)
            {
                nn = null;
                return false;
            }
        }

        public static byte[] Serializer(NeuralNetwork nn)
        {
            var bytes = new List<byte>();
            var topology = nn.Topology;
            var weights = nn.Weights;
            
            bytes.AddRange(BitConverter.GetBytes(topology.Length));
            foreach (var t in topology)
                bytes.AddRange(BitConverter.GetBytes(t));
            foreach (var layer in weights)
                foreach (var weight in layer)
                    bytes.AddRange(BitConverter.GetBytes(weight));
            
            return bytes.ToArray();
        }

        public static NeuralNetwork Deserializer(byte[] bytes)
        {
            return Deserializer(bytes, out _);
        }
        private static NeuralNetwork Deserializer(byte[] bytes, out int counter)
        {
            counter = 0;
            var length = BitConverter.ToInt32(bytes, counter);
            counter += 4;
            
            var topology = new int[length];
            for (int i = 0; i < length; i++)
            {
                topology[i] = BitConverter.ToInt32(bytes, counter);
                counter += 4;
            }

            length--;
            var weights = new float[length][];
            for (int i = 0; i < length; i++)
            {
                var count = (topology[i] + 1) * topology[i + 1];

                weights[i] = new float[count];

                for (int j = 0; j < count; j++)
                {
                    weights[i][j] = BitConverter.ToSingle(bytes, counter);
                    counter += 4;
                }
            }

            return new NeuralNetwork(topology, weights);
        }
    }
}
