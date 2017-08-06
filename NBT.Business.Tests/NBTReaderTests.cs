using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using NBT.Business.Models.Tags;
using System.Collections.Generic;

namespace NBT.Business.Tests
{
    [TestClass]
    public class NBTReaderTests
    {
        [TestMethod]
        public void ReadTAG_Byte()
        {
            byte[] sourceBytes = new byte[] { 1,0,4, 66, 121, 116, 101, 12 };
            MemoryStream mStream = new MemoryStream(sourceBytes);

            NBTReader reader = new NBTReader();
            TAG_Byte tag = reader.GetTag(mStream) as TAG_Byte;
            Assert.IsNotNull(tag, "tag is not TAG_Byte");
            Assert.AreEqual(TagType.Byte, tag.TagType);
            Assert.AreEqual("Byte", tag.Name);
            Assert.AreEqual(12, tag.Value);
        }

        [TestMethod]
        public void ReadTAG_Short()
        {
            byte[] sourceBytes = new byte[] { 2, 0, 5, 83, 104, 111, 114, 116, 11, 12 };
            MemoryStream mStream = new MemoryStream(sourceBytes);

            NBTReader reader = new NBTReader();
            TAG_Short tag = reader.GetTag(mStream) as TAG_Short;
            Assert.IsNotNull(tag, "tag is not TAG_Short");
            Assert.AreEqual(TagType.Short, tag.TagType);
            Assert.AreEqual("Short", tag.Name);
            Assert.AreEqual(11*256+12, tag.Value);
        }

        [TestMethod]
        public void ReadTAG_Int()
        {
            byte[] sourceBytes = new byte[] { 3, 0, 3, 73, 110, 116, 9,10,11, 12 };
            MemoryStream mStream = new MemoryStream(sourceBytes);

            NBTReader reader = new NBTReader();
            TAG_Int tag = reader.GetTag(mStream) as TAG_Int;
            Assert.IsNotNull(tag, "tag is not TAG_Int");
            Assert.AreEqual(TagType.Int, tag.TagType);
            Assert.AreEqual("Int", tag.Name);
            Assert.AreEqual(0x090A0B0C, tag.Value);
        }

        [TestMethod]
        public void ReadTAG_Long()
        {
            byte[] sourceBytes = new byte[] { 4, 0, 4, 76, 111, 110, 103,
                1,2,3,4,
                5,6,7,8};
            MemoryStream mStream = new MemoryStream(sourceBytes);

            NBTReader reader = new NBTReader();
            TAG_Long tag = reader.GetTag(mStream) as TAG_Long;
            Assert.IsNotNull(tag, "tag is not TAG_Long");
            Assert.AreEqual(TagType.Long, tag.TagType);
            Assert.AreEqual("Long", tag.Name);
            Assert.AreEqual(0x0102030405060708, tag.Value);
        }

        [TestMethod]
        public void ReadTAG_Float()
        {
            byte[] sourceBytes = new byte[] { 5, 0, 5, 70, 108, 111, 97, 116, 64,32,0,0 };
            MemoryStream mStream = new MemoryStream(sourceBytes);

            NBTReader reader = new NBTReader();
            TAG_Float tag = reader.GetTag(mStream) as TAG_Float;
            Assert.IsNotNull(tag, "tag is not TAG_Float");
            Assert.AreEqual(TagType.Float, tag.TagType);
            Assert.AreEqual("Float", tag.Name);
            Assert.AreEqual(2.5, tag.Value);
        }

        [TestMethod]
        public void ReadTAG_Double()
        {
            byte[] sourceBytes = new byte[] { 6, 0, 6, 68,111,117,98,108,101,
                64,32,0,0,
                0,0,0,0};
            MemoryStream mStream = new MemoryStream(sourceBytes);

            NBTReader reader = new NBTReader();
            TAG_Double tag = reader.GetTag(mStream) as TAG_Double;
            Assert.IsNotNull(tag, "tag is not TAG_Double");
            Assert.AreEqual(TagType.Double, tag.TagType);
            Assert.AreEqual("Double", tag.Name);
            Assert.AreEqual(8, tag.Value);
        }

        [TestMethod]
        public void ReadTAG_ByteArray()
        {
            byte[] sourceBytes = new byte[] { 7, 0, 9, 66,121,116,101,65,114,114,97,121,
                0,0,0,3,
                12, 10, 8 };
            MemoryStream mStream = new MemoryStream(sourceBytes);

            NBTReader reader = new NBTReader();
            TAG_ByteArray tag = reader.GetTag(mStream) as TAG_ByteArray;
            Assert.IsNotNull(tag, "tag is not TAG_ByteArray");
            Assert.AreEqual(TagType.ByteArray, tag.TagType);
            Assert.AreEqual("ByteArray", tag.Name);
            var expectedArray = new byte[] { 12, 10, 8 };
            Assert.AreEqual(expectedArray.Length, tag.Value.Length);
            for (int i = 0; i < expectedArray.Length; i++)
            {
                Assert.AreEqual(expectedArray[i], tag.Value[i]);
            }
        }

        [TestMethod]
        public void ReadTAG_String()
        {
            byte[] sourceBytes = new byte[] { 8, 0, 6, 83, 116, 114, 105, 110, 103,
                0,7,
                83,116,114,105,110,103,49};
            MemoryStream mStream = new MemoryStream(sourceBytes);

            NBTReader reader = new NBTReader();
            TAG_String tag = reader.GetTag(mStream) as TAG_String;
            Assert.IsNotNull(tag, "tag is not TAG_String");
            Assert.AreEqual(TagType.String, tag.TagType);
            Assert.AreEqual("String", tag.Name);
            Assert.AreEqual("String1", tag.Value);
        }

        [TestMethod]
        public void ReadTAG_List()
        {
            byte[] sourceBytes = new byte[] { 9, 0, 4, 76, 105, 115, 116,
                2,
                0, 0, 0, 3,
                0, 1, 0, 2, 0, 3};
            MemoryStream mStream = new MemoryStream(sourceBytes);

            NBTReader reader = new NBTReader();
            TAG_List tag = reader.GetTag(mStream) as TAG_List;
            Assert.IsNotNull(tag, "tag is not TAG_List");
            Assert.AreEqual(TagType.List, tag.TagType);
            Assert.AreEqual("List", tag.Name);
            var expectedArray = new short[] { 1, 2, 3 };
            Assert.AreEqual(expectedArray.Length, tag.Value.Count);
            for (int i = 0; i < expectedArray.Length; i++)
            {
                Assert.AreEqual(expectedArray[i], tag.Value[i]);
            }
        }

        [TestMethod]
        public void ReadTAG_Compound()
        {
            byte[] sourceBytes = new byte[] { 10, 0, 8, 67, 111, 109, 112, 111, 117, 110, 100,
                1,0,4, 66, 121, 116, 101, 12, //TAG_Byte
                2, 0, 5, 83, 104, 111, 114, 116, 11, 12,//TAG_Short
                0};//TAG_End
            MemoryStream mStream = new MemoryStream(sourceBytes);

            NBTReader reader = new NBTReader();
            TAG_Compound tag = reader.GetTag(mStream) as TAG_Compound;
            Assert.IsNotNull(tag, "tag is not TAG_Compound");
            Assert.AreEqual(TagType.Compound, tag.TagType);
            Assert.AreEqual("Compound", tag.Name);
            Assert.AreEqual(TagType.Byte, tag.Value[0].TagType);
            Assert.AreEqual(TagType.Short, tag.Value[1].TagType);
            Assert.AreEqual(TagType.End, tag.Value[2].TagType);
        }

        [TestMethod]
        public void ReadTAG_IntArray()
        {
            byte[] sourceBytes = new byte[] { 11, 0, 8, 73,110,116,65,114,114,97,121,
                0,0,0,3,
                0,0,0,12,
                0,0,0,10,
                0,0,0,8 };
            MemoryStream mStream = new MemoryStream(sourceBytes);

            NBTReader reader = new NBTReader();
            TAG_IntArray tag = reader.GetTag(mStream) as TAG_IntArray;
            Assert.IsNotNull(tag, "tag is not TAG_IntArray");
            Assert.AreEqual(TagType.IntArray, tag.TagType);
            Assert.AreEqual("IntArray", tag.Name);
            var expectedArray = new int[] { 12, 10, 8 };
            Assert.AreEqual(expectedArray.Length, tag.Value.Length);
            for (int i = 0; i < expectedArray.Length; i++)
            {
                Assert.AreEqual(expectedArray[i], tag.Value[i]);
            }
        }
    }
}
