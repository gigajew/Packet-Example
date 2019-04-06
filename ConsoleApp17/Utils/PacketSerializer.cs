using ConsoleApp17.Net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp17.Utils
{
    public class PacketSerializer
    {

        public void Serialize(Stream stream, Packet packet)
        {
            stream.WriteByte((byte)( packet.Magic >> 24));
            stream.WriteByte((byte)(packet.Magic >> 16));
            stream.WriteByte((byte)(packet.Magic >> 8));
            stream.WriteByte((byte)(packet.Magic));

            stream.WriteByte((byte)((int)packet.Type >> 24));
            stream.WriteByte((byte)((int)packet.Type >> 16));
            stream.WriteByte((byte)((int)packet.Type >> 8));
            stream.WriteByte((byte)((int)packet.Type));
            
            stream.WriteByte((byte)(packet.Length >> 24));
            stream.WriteByte((byte)(packet.Length >> 16));
            stream.WriteByte((byte)(packet.Length >> 8));
            stream.WriteByte((byte)(packet.Length));

            stream.Write(packet.Data, 0, packet.Data.Length);
        }

#pragma warning disable CS0675
        public Packet Deserialize(Stream stream )
        {
            Packet packet = new Packet();

            int magic = stream.ReadByte() << 24 | stream.ReadByte() << 16 | stream.ReadByte() << 8 | stream.ReadByte();
            if (magic != Constants.Magic )
                throw new InvalidDataException("magic");

            packet.Magic = magic;

            int type = stream.ReadByte() << 24 | stream.ReadByte() << 16 | stream.ReadByte() << 8 | stream.ReadByte();
            if (type < 0)
                throw new ArgumentOutOfRangeException("type");

            packet.Type = (PacketType)type;

            int length =  stream.ReadByte() << 24 | stream.ReadByte() << 16 | stream.ReadByte() << 8 | stream.ReadByte();

            packet.Length = length;

            if (length != 0)
            {
                byte[] buffer = new byte[length];
                if (stream.Read(buffer, 0, buffer.Length) != length)
                    throw new InvalidDataException("data");

                packet.Data = buffer;
            }

            return packet;
        }
#pragma warning restore CS0675
    }
}
