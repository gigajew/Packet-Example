using ConsoleApp17.Net;
using ConsoleApp17.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ConsoleApp17
{
    class Program
    {
        static void Main(string[] args)
        {
            // memory for the packet
            MemoryStream memory = new MemoryStream();

            // create a packet
            Packet packet = new Packet(PacketType.Message, Encoding.UTF8.GetBytes("hello, server"));

            // serialize the packet to the memorystream
            PacketSerializer serializer = new PacketSerializer();
            serializer.Serialize(memory, packet);

            // save a copy
            File.WriteAllBytes("TestPacket.bin", memory.ToArray());

            // reset the position of the stream
            memory.Seek(0L, SeekOrigin.Begin);

            // deserialize the packet
            Packet deserialized = serializer.Deserialize(memory);

            // print the packet data
            Console.WriteLine(Encoding.UTF8.GetString(deserialized.Data, 0, (int)deserialized.Length));
            Console.ReadLine();
        }
    }
}
