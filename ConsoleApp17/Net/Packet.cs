using ConsoleApp17.Utils;
using System;

namespace ConsoleApp17.Net
{
    [Serializable]
    public class Packet
    {
        public int Magic;
        public PacketType Type;
        public int Length;
        public byte[] Data;

        public Packet() { }

        public Packet(PacketType type, byte[] data)
        {
            Magic = Constants.Magic;
            Type = type;
            Length = data.Length;
            Data = data;
        }
    }
}
