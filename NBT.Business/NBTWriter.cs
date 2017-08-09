using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NBT.Business.Models.Tags;

namespace NBT.Business
{
    public class NBTWriter
    {
        public byte[] GetBytes(TAG_Byte tag)
        {
            int stringLength = tag.Name.Length;
            int headerLength = 3;//1 byte for type and 2 for string length
            int dataLength = 1;
            byte[] bytes = new byte[headerLength + stringLength + dataLength];

            bytes[0] = (byte)TagType.Byte;
            bytes[1] = (byte)((stringLength & 0xFF00) >> 8);
            bytes[2] = (byte)(stringLength & 0xFF);

            for (int i = 0; i < stringLength; i++)
            {
                bytes[3 + i] = (byte)tag.Name[i];
            }

            bytes[3 + stringLength] = tag.Value;

            return bytes;
        }

        public byte[] GetBytes(TAG_Short tag)
        {
            int stringLength = tag.Name.Length;
            int headerLength = 3;//1 byte for type and 2 for string length
            int dataLength = 2;
            byte[] bytes = new byte[headerLength + stringLength + dataLength];

            bytes[0] = (byte)TagType.Short;
            bytes[1] = (byte)((stringLength & 0xFF00) >> 8);
            bytes[2] = (byte)(stringLength & 0xFF);

            for (int i = 0; i < stringLength; i++)
            {
                bytes[3 + i] = (byte)tag.Name[i];
            }

            bytes[3 + stringLength] = (byte)((tag.Value & 0xFF00) >> 8);
            bytes[4 + stringLength] = (byte)(tag.Value & 0xFF);

            return bytes;
        }

        public byte[] GetBytes(TAG_Int tag)
        {
            int stringLength = tag.Name.Length;
            int headerLength = 3;//1 byte for type and 2 for string length
            int dataLength = 4;
            byte[] bytes = new byte[headerLength + stringLength + dataLength];

            bytes[0] = (byte)TagType.Int;
            bytes[1] = (byte)((stringLength & 0xFF00) >> 8);
            bytes[2] = (byte)(stringLength & 0xFF);

            for (int i = 0; i < stringLength; i++)
            {
                bytes[3 + i] = (byte)tag.Name[i];
            }

            bytes[3 + stringLength] = (byte)((tag.Value & 0xFF000000) >> 24);
            bytes[4 + stringLength] = (byte)((tag.Value & 0xFF0000) >> 16);
            bytes[5 + stringLength] = (byte)((tag.Value & 0xFF00) >> 8);
            bytes[6 + stringLength] = (byte)(tag.Value & 0xFF);

            return bytes;
        }

        public byte[] GetBytes(TAG_Long tag)
        {
            int stringLength = tag.Name.Length;
            int headerLength = 3;//1 byte for type and 2 for string length
            int dataLength = 8;
            byte[] bytes = new byte[headerLength + stringLength + dataLength];

            bytes[0] = (byte)TagType.Long;
            bytes[1] = (byte)((stringLength & 0xFF00) >> 8);
            bytes[2] = (byte)(stringLength & 0xFF);

            for (int i = 0; i < stringLength; i++)
            {
                bytes[3 + i] = (byte)tag.Name[i];
            }

            byte[] numberBytes = BitConverter.GetBytes(tag.Value);
            bytes[3 + stringLength] = numberBytes[7];
            bytes[4 + stringLength] = numberBytes[6];
            bytes[5 + stringLength] = numberBytes[5];
            bytes[6 + stringLength] = numberBytes[4];
            bytes[7 + stringLength] = numberBytes[3];
            bytes[8 + stringLength] = numberBytes[2];
            bytes[9 + stringLength] = numberBytes[1];
            bytes[10 + stringLength] = numberBytes[0];

            return bytes;
        }

        public byte[] GetBytes(TAG_Float tag)
        {
            int stringLength = tag.Name.Length;
            int headerLength = 3;//1 byte for type and 2 for string length
            int dataLength = 4;
            byte[] bytes = new byte[headerLength + stringLength + dataLength];

            bytes[0] = (byte)TagType.Float;
            bytes[1] = (byte)((stringLength & 0xFF00) >> 8);
            bytes[2] = (byte)(stringLength & 0xFF);

            for (int i = 0; i < stringLength; i++)
            {
                bytes[3 + i] = (byte)tag.Name[i];
            }

            byte[] numberBytes = BitConverter.GetBytes(tag.Value);
            bytes[3 + stringLength] = numberBytes[3];
            bytes[4 + stringLength] = numberBytes[2];
            bytes[5 + stringLength] = numberBytes[1];
            bytes[6 + stringLength] = numberBytes[0];

            return bytes;
        }

        public byte[] GetBytes(TAG_ByteArray tag)
        {
            int stringLength = tag.Name.Length;
            int headerLength = 3;//1 byte for type and 2 for string length
            int dataLength = 1 * tag.Value.Length;
            int arrayLengthLength = 4;
            byte[] bytes = new byte[headerLength + stringLength + dataLength + arrayLengthLength];

            bytes[0] = (byte)TagType.ByteArray;
            bytes[1] = (byte)((stringLength & 0xFF00) >> 8);
            bytes[2] = (byte)(stringLength & 0xFF);

            for (int i = 0; i < stringLength; i++)
            {
                bytes[3 + i] = (byte)tag.Name[i];
            }

            byte[] numberBytes = BitConverter.GetBytes(tag.Value.Length);
            bytes[3 + stringLength] = numberBytes[3];
            bytes[4 + stringLength] = numberBytes[2];
            bytes[5 + stringLength] = numberBytes[1];
            bytes[6 + stringLength] = numberBytes[0];

            for (int i = 0; i < tag.Value.Length; i++)
            {
                bytes[7 + stringLength + i] = tag.Value[i];
            }

            return bytes;
        }

        public byte[] GetBytes(TAG_IntArray tag)
        {
            int stringLength = tag.Name.Length;
            int headerLength = 3;//1 byte for type and 2 for string length
            int dataLength = 4 * tag.Value.Length;
            int arrayLengthLength = 4;
            byte[] bytes = new byte[headerLength + stringLength + dataLength + arrayLengthLength];

            bytes[0] = (byte)TagType.IntArray;
            bytes[1] = (byte)((stringLength & 0xFF00) >> 8);
            bytes[2] = (byte)(stringLength & 0xFF);

            for (int i = 0; i < stringLength; i++)
            {
                bytes[3 + i] = (byte)tag.Name[i];
            }

            byte[] numberBytes = BitConverter.GetBytes(tag.Value.Length);
            bytes[3 + stringLength] = numberBytes[3];
            bytes[4 + stringLength] = numberBytes[2];
            bytes[5 + stringLength] = numberBytes[1];
            bytes[6 + stringLength] = numberBytes[0];

            for (int i = 0; i < tag.Value.Length; i++)
            {
                byte[] numberBytesValue = BitConverter.GetBytes(tag.Value[i]);
                bytes[7 + stringLength + 4 * i] = numberBytesValue[3];
                bytes[7 + stringLength + 4 * i + 1] = numberBytesValue[2];
                bytes[7 + stringLength + 4 * i + 2] = numberBytesValue[1];
                bytes[7 + stringLength + 4 * i + 3] = numberBytesValue[0];
            }

            return bytes;
        }

        public byte[] GetBytes(TAG_List tag)
        {
            int stringLength = tag.Name.Length;
            int headerLength = 3;//1 byte for type and 2 for string length
            int oneDataLength = GetDataLength((TagType)tag.TagId);
            int dataLength = oneDataLength * tag.Value.Count;
            int arrayLengthLength = 4;
            int tagIdLength = 1;
            byte[] bytes = new byte[headerLength + stringLength + tagIdLength+ dataLength + arrayLengthLength];

            bytes[0] = (byte)TagType.List;
            bytes[1] = (byte)((stringLength & 0xFF00) >> 8);
            bytes[2] = (byte)(stringLength & 0xFF);

            for (int i = 0; i < stringLength; i++)
            {
                bytes[3 + i] = (byte)tag.Name[i];
            }

            bytes[3 + stringLength] = (byte)tag.TagId;
            byte[] numberBytes = BitConverter.GetBytes(tag.Value.Count);
            bytes[4 + stringLength] = numberBytes[3];
            bytes[5 + stringLength] = numberBytes[2];
            bytes[6 + stringLength] = numberBytes[1];
            bytes[7 + stringLength] = numberBytes[0];

            var getNumberBytes = GetNumberBytes((TagType)tag.TagId);
            for (int i = 0; i < tag.Value.Count; i++)
            {
                byte[] numberBytesValue = getNumberBytes(tag.Value[i]);
                for (int j = 0; j < numberBytesValue.Length; j++)
                {
                    bytes[8 + stringLength + oneDataLength * i + j] = numberBytesValue[numberBytesValue.Length-j-1];
                }
            }

            return bytes;
        }

        public Func<object, byte[]> GetNumberBytes(TagType type)
        {
            switch (type)
            {
                case TagType.End:
                    break;
                case TagType.Byte:
                    return new Func<object, byte[]>((o) => { return BitConverter.GetBytes(byte.Parse(o.ToString())); });
                case TagType.Short:
                    return new Func<object, byte[]>((o) => { return BitConverter.GetBytes(short.Parse(o.ToString())); });
                case TagType.Int:
                    return new Func<object, byte[]>((o) => { return BitConverter.GetBytes(int.Parse(o.ToString())); });
                case TagType.Long:
                    return new Func<object, byte[]>((o) => { return BitConverter.GetBytes(long.Parse(o.ToString())); });
                case TagType.Float:
                    return new Func<object, byte[]>((o) => { return BitConverter.GetBytes(float.Parse(o.ToString())); });
                case TagType.Double:
                    return new Func<object, byte[]>((o) => { return BitConverter.GetBytes(double.Parse(o.ToString())); });
                case TagType.ByteArray:
                    break;
                case TagType.String:
                    break;
                case TagType.List:
                    break;
                case TagType.Compound:
                    return new Func<object, byte[]>((o) => { return GetBytes((TAG_Compound)o); });
                case TagType.IntArray:
                    break;
                default:
                    break;
            }

            throw new ArgumentException("Unable to find how return byte array");
        }

        public byte[] GetBytes(TAG_Compound tag)
        {
            int stringLength = !string.IsNullOrEmpty(tag.Name)? tag.Name.Length:0;
            int headerLength = 3;//1 byte for type and 2 for string length
            byte[] bytes = new byte[headerLength + stringLength];

            bytes[0] = (byte)TagType.Compound;
            bytes[1] = (byte)((stringLength & 0xFF00) >> 8);
            bytes[2] = (byte)(stringLength & 0xFF);

            for (int i = 0; i < stringLength; i++)
            {
                bytes[3 + i] = (byte)tag.Name[i];
            }

            TagType lastTag = TagType.Byte;
            foreach (BaseTAG innerTag in tag.Value)
            {
                lastTag = innerTag.TagType;
                bytes = bytes.Concat(GetBytes(innerTag)).ToArray();
            }
            if (lastTag != TagType.End)
                bytes = bytes.Concat(new byte[] { 0 }).ToArray();

            return bytes;
        }

        private byte[] GetBytes(BaseTAG innerTag)
        {
            switch (innerTag.TagType)
            {
                case TagType.End:
                    return new byte[] { 0 };
                case TagType.Byte:
                    return GetBytes((TAG_Byte)innerTag);
                case TagType.Short:
                    return GetBytes((TAG_Short)innerTag);
                case TagType.Int:
                    return GetBytes((TAG_Int)innerTag);
                case TagType.Long:
                    return GetBytes((TAG_Long)innerTag);
                case TagType.Float:
                    return GetBytes((TAG_Float)innerTag);
                case TagType.Double:
                    return GetBytes((TAG_Double)innerTag);
                case TagType.ByteArray:
                    return GetBytes((TAG_ByteArray)innerTag);
                case TagType.String:
                    return GetBytes((TAG_String)innerTag);
                case TagType.List:
                    return GetBytes((TAG_List)innerTag);
                case TagType.Compound:
                    return GetBytes((TAG_Compound)innerTag);
                case TagType.IntArray:
                    return GetBytes((TAG_IntArray)innerTag);
                default:
                    throw new ArgumentException("Cannot get bytes for " + innerTag.TagType);
            }
        }

        public int GetDataLength(TagType type)
        {
            switch (type)
            {
                case TagType.End:
                    break;
                case TagType.Byte:
                    return 1;
                case TagType.Short:
                    return 2;
                case TagType.Int:
                    return 4;
                case TagType.Long:
                    return 8;
                case TagType.Float:
                    return 4;
                case TagType.Double:
                    return 8;
                case TagType.ByteArray:
                    return 1;
                case TagType.String:
                    return 1;
                case TagType.List:
                    break;
                case TagType.Compound:
                    break;
                case TagType.IntArray:
                    return 4;
                default:
                    break;
            }

            throw new ArgumentException("Unable to find DataLength");
        }

        public byte[] GetBytes(TAG_String tag)
        {
            int stringLength = tag.Name.Length;
            int headerLength = 3;//1 byte for type and 2 for string length
            int dataLength = 1 * tag.Value.Length;
            int arrayLengthLength = 2;
            byte[] bytes = new byte[headerLength + stringLength + dataLength + arrayLengthLength];

            bytes[0] = (byte)TagType.String;
            bytes[1] = (byte)((stringLength & 0xFF00) >> 8);
            bytes[2] = (byte)(stringLength & 0xFF);

            for (int i = 0; i < stringLength; i++)
            {
                bytes[3 + i] = (byte)tag.Name[i];
            }

            byte[] numberBytes = BitConverter.GetBytes(tag.Value.Length);
            bytes[3 + stringLength] = numberBytes[1];
            bytes[4 + stringLength] = numberBytes[0];

            for (int i = 0; i < tag.Value.Length; i++)
            {
                bytes[5 + stringLength + i] = (byte)tag.Value[i];
            }

            return bytes;
        }

        public byte[] GetBytes(TAG_Double tag)
        {
            int stringLength = tag.Name.Length;
            int headerLength = 3;//1 byte for type and 2 for string length
            int dataLength = 8;
            byte[] bytes = new byte[headerLength + stringLength + dataLength];

            bytes[0] = (byte)TagType.Double;
            bytes[1] = (byte)((stringLength & 0xFF00) >> 8);
            bytes[2] = (byte)(stringLength & 0xFF);

            for (int i = 0; i < stringLength; i++)
            {
                bytes[3 + i] = (byte)tag.Name[i];
            }

            byte[] numberBytes = BitConverter.GetBytes(tag.Value);
            bytes[3 + stringLength] = numberBytes[7];
            bytes[4 + stringLength] = numberBytes[6];
            bytes[5 + stringLength] = numberBytes[5];
            bytes[6 + stringLength] = numberBytes[4];
            bytes[7 + stringLength] = numberBytes[3];
            bytes[8 + stringLength] = numberBytes[2];
            bytes[9 + stringLength] = numberBytes[1];
            bytes[10 + stringLength] = numberBytes[0];

            return bytes;
        }
    }
}
