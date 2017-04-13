using MinecraftRegion.Business.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ionic.Zlib;
using MinecraftRegion.Business.Models.Tags;

namespace MinecraftRegion.Business
{
    public class RegionReader
    {
        public void Read(string path)
        {
            foreach (var file in System.IO.Directory.GetFiles(path, "r.*.*.mca"))
            {
                ReadOne(file);
            }
        }

        private void ReadOne(string file)
        {
            using (BinaryReader binaryReader = new BinaryReader(File.OpenRead(file)))
            {
                byte[] headerLocations = new byte[4096];
                byte[] headerTimestamps = new byte[4096];
                int read = binaryReader.Read(headerLocations, 0, 4096);
                read = binaryReader.Read(headerTimestamps, 0, 4096);
                var locations = ReadLocations(headerLocations);
                var timeStamps = ReadTimeStamps(headerTimestamps);
                for (int iLocation = 0; iLocation < locations.Count; iLocation++)
                {
                    int timestamp = timeStamps[iLocation];
                    ChunkLocation location = locations[iLocation];
                    binaryReader.BaseStream.Position = location.Offset * 4096;
                    byte[] headerChunk = new byte[5];
                    binaryReader.Read(headerChunk, 0, 5);
                    for (int iSector = 0; iSector < location.SectorCount; iSector++)
                    {
                        //byte          0 1 2 3             4                   5...
                        //description   length(in bytes)    compression type    compressed data (length - 1 bytes)
                        int length = (headerChunk[0] << 24) + (headerChunk[1] << 16) + (headerChunk[2] << 8) + headerChunk[3];
                        byte compresionType = headerChunk[4];

                        switch (compresionType)
                        {
                            case 2:
                                var rootTag = ParseByCompressionZLib(binaryReader);
                                break;
                            default:
                                throw new NotImplementedException("Only compression ZLib is implemented");
                        }
                    }
                }
            }
        }

        private BaseTAG ParseByCompressionZLib(BinaryReader binaryReader)
        {
            byte[] compressedChunk = new byte[4091];
            binaryReader.Read(compressedChunk, 0, 4091);
            using (MemoryStream mStream = new MemoryStream(compressedChunk))
            {
                using (Ionic.Zlib.ZlibStream zLibStream = new Ionic.Zlib.ZlibStream(mStream, Ionic.Zlib.CompressionMode.Decompress))
                {
                    //byte[] decompressedChunk = new byte[zLibStream.BufferSize];
                    return GetTag(zLibStream);
                    //zLibStream.ReadAsync (decompressedChunk, 0, zLibStream.BufferSize);
                }
            }
        }

        private BaseTAG GetTag(Stream stream)
        {
            byte[] tagTypeArray = new byte[1];
            stream.Read(tagTypeArray, 0, 1);
            byte tagType = tagTypeArray[0];
            return GetTagByType(stream, tagType);
        }

        private BaseTAG GetTagByType(Stream stream, byte tagType)
        {
            switch (tagType)
            {
                case 0:
                    return new TAG_End();
                case 1:
                    return ParseTAG_Byte(stream);
                case 2:
                    return ParseTAG_Short(stream);
                case 3:
                    return ParseTAG_Int(stream);
                case 4:
                    return ParseTAG_Long(stream);
                case 5:
                    return ParseTAG_Float(stream);
                case 6:
                    return ParseTAG_Double(stream);
                case 7:
                    return ParseTAG_ByteArray(stream);
                case 8:
                    break;
                case 9:
                    return ParseTAG_List(stream);
                case 10:
                    return ParseTAG_Compound(stream);
                case 11:
                    return ParseTAG_IntArray(stream);
                case 12:
                    break;
                default:
                    break;
            }

            throw new NotImplementedException();
        }

        private TAG_ByteArray ParseTAG_ByteArray(Stream stream)
        {
            TAG_ByteArray tag = new TAG_ByteArray();
            tag.Name = GetTAGName(stream);
            int size = GetInt(stream);
            tag.Values = new sbyte[size];
            for (int i = 0; i < size; i++)
            {
                tag.Values[i] = GetSbyte(stream);
            }

            return tag;
        }

        private TAG_IntArray ParseTAG_IntArray(Stream stream)
        {
            TAG_IntArray tag = new TAG_IntArray();
            tag.Name = GetTAGName(stream);
            int size = GetInt(stream);
            tag.Values = new int[size];
            for (int i = 0; i < size; i++)
            {
                tag.Values[i] = GetInt(stream);
            }

            return tag;
        }

        private TAG_Double ParseTAG_Double(Stream stream)
        {
            TAG_Double tag = new TAG_Double();
            tag.Name = GetTAGName(stream);
            double composed = GetDouble(stream);
            tag.Value = composed;
            return tag;
        }

        private TAG_Float ParseTAG_Float(Stream stream)
        {
            TAG_Float tag = new TAG_Float();
            tag.Name = GetTAGName(stream);
            float composed = GetFloat(stream);
            tag.Value = composed;
            return tag;
        }

        private TAG_Long ParseTAG_Long(Stream stream)
        {
            TAG_Long tag = new TAG_Long();
            tag.Name = GetTAGName(stream);
            long composed = GetLong(stream);
            tag.Value = composed;
            return tag;
        }

        private TAG_List ParseTAG_List(Stream stream)
        {
            TAG_List list = new TAG_List();
            list.Name = GetTAGName(stream);
            list.TagId = GetSbyte(stream);
            list.Size = GetInt(stream);
            for (int iList = 0; iList < list.Size; iList++)
            {
#warning là ça fait du gros caca !! Appeller simple value en fait, mais qui doit gérer les tags autre que primitifs (Compound notamment)
                list.Children.Add(GetTagByType(stream, (byte)list.TagId));
            }
            return list;
        }

        private static byte[] GetDebug100(Stream streamIn, out Stream streamOut)
        {
            streamOut = new MemoryStream();
            streamIn.CopyTo(streamOut);
            byte[] debug100 = new byte[100];
            streamOut.Position = 0;
            streamOut.Read(debug100, 0, 100);
            streamOut.Position = 0;
            return debug100;
        }

        private object GetSimpleValue(byte tagId, Stream stream)
        {
            switch (tagId)
            {
                case 1:
                    return GetSbyte(stream);
                case 2:
                    return GetShort(stream);
                case 3:
                    return GetInt(stream);
                case 4:
                    return GetLong(stream);
                case 5:
                    return GetFloat(stream);
                case 6:
                    return GetDouble(stream);
                default:
                    break;
            }
            throw new NotImplementedException();
        }

        private long GetLong(Stream stream)
        {
            byte[] valueByte = new byte[4];
            stream.Read(valueByte, 0, 4);
            int composed1 = (valueByte[0] << 24) + (valueByte[1] << 16) + (valueByte[2] << 8) + (valueByte[3]);
            stream.Read(valueByte, 0, 4);
            int composed2 = (valueByte[0] << 24) + (valueByte[1] << 16) + (valueByte[2] << 8) + (valueByte[3]);
            return (composed1 << 32) + composed2;
        }

        private double GetDouble(Stream stream)
        {
            byte[] doubleBytes = new byte[8];
            stream.Read(doubleBytes, 0, 8);
            if (BitConverter.IsLittleEndian)
            {
                //It's big endian ! we have to invert
                byte temp = doubleBytes[0];
                doubleBytes[0] = doubleBytes[7];
                doubleBytes[7] = temp;
                temp = doubleBytes[1];
                doubleBytes[1] = doubleBytes[6];
                doubleBytes[6] = temp;
                temp = doubleBytes[2];
                doubleBytes[2] = doubleBytes[5];
                doubleBytes[5] = temp;
                temp = doubleBytes[3];
                doubleBytes[3] = doubleBytes[4];
                doubleBytes[4] = temp;
            }
            return BitConverter.ToDouble(doubleBytes, 0);
        }

        private float GetFloat(Stream stream)
        {
            byte[] floatBytes = new byte[4];
            stream.Read(floatBytes, 0, 4);
            if (BitConverter.IsLittleEndian)
            {
                //It's big endian ! we have to invert
                byte temp = floatBytes[0];
                floatBytes[0] = floatBytes[3];
                floatBytes[3] = temp;
                temp = floatBytes[1];
                floatBytes[1] = floatBytes[2];
                floatBytes[2] = temp;
            }
            return BitConverter.ToSingle(floatBytes, 0);
        }

        private TAG_Int ParseTAG_Int(Stream stream)
        {
            TAG_Int tag = new TAG_Int();
            tag.Name = GetTAGName(stream);
            int composed = GetInt(stream);
            tag.Value = composed;
            return tag;
        }

        private static int GetInt(Stream stream)
        {
            byte[] valueByte = new byte[4];
            stream.Read(valueByte, 0, 4);
            int composed = (valueByte[0] << 24) + (valueByte[1] << 16) + (valueByte[2] << 8) + (valueByte[3]);
            return composed;
        }

        private TAG_Short ParseTAG_Short(Stream stream)
        {
            TAG_Short tag = new TAG_Short();
            tag.Name = GetTAGName(stream);
            short composed = GetShort(stream);
            tag.Value = composed;
            return tag;
        }

        private static short GetShort(Stream stream)
        {
            byte[] valueByte = new byte[4];
            stream.Read(valueByte, 0, 4);
            short composed = (short)((valueByte[0] << 8) + (valueByte[1]));
            return composed;
        }

        private TAG_Byte ParseTAG_Byte(Stream stream)
        {
            TAG_Byte tag = new TAG_Byte();
            tag.Name = GetTAGName(stream);
            sbyte composed = GetSbyte(stream);
            tag.Value = composed;
            return tag;
        }

        private static sbyte GetSbyte(Stream stream)
        {
            byte[] valueByte = new byte[1];
            stream.Read(valueByte, 0, 1);
            sbyte composed = (sbyte)valueByte[0];
            return composed;
        }

        private TAG_Compound ParseTAG_Compound(Stream stream)
        {
            TAG_Compound tag = new TAG_Compound();
            tag.Name = GetTAGName(stream);
            BaseTAG currentChild = null;
            while (currentChild == null || !(currentChild is TAG_End))
            {
                currentChild = GetTag(stream);
                tag.Children.Add(currentChild);
            }
            return tag;
        }

        private string GetTAGName(Stream zLibStream)
        {
            byte[] textLengthArray = new byte[2];
            zLibStream.Read(textLengthArray, 0, 2);
            int textLength = (textLengthArray[0] << 8) + textLengthArray[1];
            byte[] textContentArray = new byte[textLength];
            zLibStream.Read(textContentArray, 0, textLength);
            StringBuilder sb = new StringBuilder();
            foreach (byte c in textContentArray)
            {
                sb.Append((char)c);
            }
            return sb.ToString();
        }

        private List<int> ReadTimeStamps(byte[] headerTimestamps)
        {
            List<int> timestamps = new List<int>();
            for (int iTimeStamp = 0; iTimeStamp < 4096; iTimeStamp += 4)
            {
                timestamps.Add((headerTimestamps[iTimeStamp + 0] << 24) + (headerTimestamps[iTimeStamp + 1] << 16) + (headerTimestamps[iTimeStamp + 2] << 8) + (headerTimestamps[iTimeStamp + 3]));
            }
            return timestamps;
        }

        private List<ChunkLocation> ReadLocations(byte[] headerLocations)
        {
            //Location information for a chunk consists of four bytes split into two fields: the first three bytes 
            //are a (big - endian) offset in 4KiB sectors from the start of the file, 
            //and a remaining byte which gives the length of the chunk (also in 4KiB sectors, rounded up).Chunks will always be less than 1MiB in size.
            //If a chunk isn't present in the region file (e.g. because it hasn't been generated or migrated yet), both fields will be zero.

            //byte          0 1 2   3
            //description   offset  sector count
            //A chunk with an offset of 2 will begin right after the timestamps table.
            List<ChunkLocation> locations = new List<ChunkLocation>();
            for (int iLocation = 0; iLocation < 4096; iLocation += 4)
            {
                byte[] location = new byte[4] { headerLocations[iLocation], headerLocations[iLocation + 1], headerLocations[iLocation + 2], headerLocations[iLocation + 3] };
                int offset = (location[0] << 16) + (location[1] << 8) + location[2];
                byte sectorCount = location[3];
                if (offset != 0 && sectorCount != 0)
                    locations.Add(new ChunkLocation()
                    {
                        Offset = offset,
                        SectorCount = sectorCount
                    });
            }

            return locations;
        }
    }
}
