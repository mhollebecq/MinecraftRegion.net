using NBT.Business.Models.Tags;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NBT.Business
{
    public class NBTReader
    {
        public BaseTAG GetTag(Stream stream)
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
                    return ParseTAG_String(stream);
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

        private TAG_String ParseTAG_String(Stream stream)
        {
            TAG_String tag = new TAG_String();
            tag.Name = GetString(stream);
            string composed = GetString(stream);
            tag.Value = composed;
            return tag;
        }

        private TAG_ByteArray ParseTAG_ByteArray(Stream stream)
        {
            TAG_ByteArray tag = new TAG_ByteArray();
            tag.Name = GetString(stream);
            int size = GetInt(stream);
            tag.Value = new byte[size];
            for (int i = 0; i < size; i++)
            {
                tag.Value[i] = GetByte(stream);
            }

            return tag;
        }

        private TAG_IntArray ParseTAG_IntArray(Stream stream)
        {
            TAG_IntArray tag = new TAG_IntArray();
            tag.Name = GetString(stream);
            int size = GetInt(stream);
            tag.Value = new int[size];
            for (int i = 0; i < size; i++)
            {
                tag.Value[i] = GetInt(stream);
            }

            return tag;
        }

        private TAG_Double ParseTAG_Double(Stream stream)
        {
            TAG_Double tag = new TAG_Double();
            tag.Name = GetString(stream);
            double composed = GetDouble(stream);
            tag.Value = composed;
            return tag;
        }

        private TAG_Float ParseTAG_Float(Stream stream)
        {
            TAG_Float tag = new TAG_Float();
            tag.Name = GetString(stream);
            float composed = GetFloat(stream);
            tag.Value = composed;
            return tag;
        }

        private TAG_Long ParseTAG_Long(Stream stream)
        {
            TAG_Long tag = new TAG_Long();
            tag.Name = GetString(stream);
            long composed = GetLong(stream);
            tag.Value = composed;
            return tag;
        }

        private TAG_List ParseTAG_List(Stream stream)
        {
            TAG_List list = new TAG_List();
            list.Name = GetString(stream);
            list.TagId = GetSbyte(stream);
            list.Size = GetInt(stream);
            for (int iList = 0; iList < list.Size; iList++)
            {
                list.Value.Add(GetSimpleValue((byte)list.TagId, stream));
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
                case 8:
                    return GetString(stream);
                case 9:
                    return ParseTAG_List(stream);
                case 10:
                    return GetTAG_Compound(stream);
                case 11:
                    return ParseTAG_IntArray(stream);
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
            return (composed1 * 0x100000000) + composed2;
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
            tag.Name = GetString(stream);
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
            tag.Name = GetString(stream);
            short composed = GetShort(stream);
            tag.Value = composed;
            return tag;
        }

        private static short GetShort(Stream stream)
        {
            byte[] valueByte = new byte[2];
            stream.Read(valueByte, 0, 2);
            short composed = (short)((valueByte[0] << 8) + (valueByte[1]));
            return composed;
        }

        private TAG_Byte ParseTAG_Byte(Stream stream)
        {
            TAG_Byte tag = new TAG_Byte();
            tag.Name = GetString(stream);
            byte composed = GetByte(stream);
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

        private static byte GetByte(Stream stream)
        {
            byte[] valueByte = new byte[1];
            stream.Read(valueByte, 0, 1);
            byte composed = valueByte[0];
            return composed;
        }

        private TAG_Compound ParseTAG_Compound(Stream stream)
        {
            TAG_Compound tag = new TAG_Compound();
            tag.Name = GetString(stream);
            BaseTAG currentChild = null;
            while (currentChild == null || !(currentChild is TAG_End))
            {
                currentChild = GetTag(stream);
                tag.Value.Add(currentChild);
            }
            return tag;
        }

        /// <summary>
        /// Does not get TAG Name, only value
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        private TAG_Compound GetTAG_Compound(Stream stream)
        {
            TAG_Compound tag = new TAG_Compound();
            BaseTAG currentChild = null;
            while (currentChild == null || !(currentChild is TAG_End))
            {
                currentChild = GetTag(stream);
                tag.Value.Add(currentChild);
            }
            return tag;
        }

        private string GetString(Stream stream)
        {
            byte[] textLengthArray = new byte[2];
            stream.Read(textLengthArray, 0, 2);
            int textLength = (textLengthArray[0] << 8) + textLengthArray[1];
            byte[] textContentArray = new byte[textLength];
            stream.Read(textContentArray, 0, textLength);
            StringBuilder sb = new StringBuilder();
            foreach (byte c in textContentArray)
            {
                sb.Append((char)c);
            }
            return sb.ToString();
        }

    }
}
