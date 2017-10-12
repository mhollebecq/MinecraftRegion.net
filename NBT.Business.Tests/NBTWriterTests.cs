using Microsoft.VisualStudio.TestTools.UnitTesting;
using NBT.Business.Models.Tags;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NBT.Business.Tests
{
    [TestClass]
    public class NBTWriterTests
    {
        [TestMethod]
        public void Write_TAG_Byte()
        {
            TAG_Byte tag = new TAG_Byte()
            {
                Name = "Byte",
                Value = 12
            };
            NBTWriter writer = new NBTWriter();
            byte[] bytes = writer.GetBytes(tag);
            byte[] expected = new byte[] { 1, 0, 4, 66, 121, 116, 101, 12 };
            Assert.AreEqual(expected.Length, bytes.Length, "Arrays do not have same length");
            for (int i = 0; i < expected.Length; i++)
            {
                Assert.AreEqual(expected[i], bytes[i]);
            }
        }

        [TestMethod]
        public void Write_TAG_Short()
        {
            TAG_Short tag = new TAG_Short()
            {
                Name = "Short",
                Value = 0xB0C
            };
            NBTWriter writer = new NBTWriter();
            byte[] bytes = writer.GetBytes(tag);
            byte[] expected = new byte[] { 2, 0, 5, 83, 104, 111, 114, 116, 11, 12 };
            Assert.AreEqual(expected.Length, bytes.Length, "Arrays do not have same length");
            for (int i = 0; i < expected.Length; i++)
            {
                Assert.AreEqual(expected[i], bytes[i]);
            }
        }

        [TestMethod]
        public void Write_TAG_Int()
        {
            TAG_Int tag = new TAG_Int()
            {
                Name = "Int",
                Value = 0x090A0B0C
            };
            NBTWriter writer = new NBTWriter();
            byte[] bytes = writer.GetBytes(tag);
            byte[] expected = new byte[] { 3, 0, 3, 73, 110, 116, 9, 10, 11, 12 };
            Assert.AreEqual(expected.Length, bytes.Length, "Arrays do not have same length");
            for (int i = 0; i < expected.Length; i++)
            {
                Assert.AreEqual(expected[i], bytes[i]);
            }
        }

        [TestMethod]
        public void Write_TAG_Long()
        {
            TAG_Long tag = new TAG_Long()
            {
                Name = "Long",
                Value = 0x0102030405060708
            };
            NBTWriter writer = new NBTWriter();
            byte[] bytes = writer.GetBytes(tag);
            byte[] expected = new byte[] { 4, 0, 4, 76, 111, 110, 103,
                1,2,3,4,
                5,6,7,8};
            Assert.AreEqual(expected.Length, bytes.Length, "Arrays do not have same length");
            for (int i = 0; i < expected.Length; i++)
            {
                Assert.AreEqual(expected[i], bytes[i]);
            }
        }

        [TestMethod]
        public void Write_TAG_Float()
        {
            TAG_Float tag = new TAG_Float()
            {
                Name = "Float",
                Value = 2.5f
            };
            NBTWriter writer = new NBTWriter();
            byte[] bytes = writer.GetBytes(tag);
            byte[] expected = new byte[] { 5, 0, 5, 70, 108, 111, 97, 116, 64, 32, 0, 0 };
            Assert.AreEqual(expected.Length, bytes.Length, "Arrays do not have same length");
            for (int i = 0; i < expected.Length; i++)
            {
                Assert.AreEqual(expected[i], bytes[i]);
            }
        }

        [TestMethod]
        public void Write_TAG_Double()
        {
            TAG_Double tag = new TAG_Double()
            {
                Name = "Double",
                Value = 8
            };
            NBTWriter writer = new NBTWriter();
            byte[] bytes = writer.GetBytes(tag);
            byte[] expected = new byte[] { 6, 0, 6, 68,111,117,98,108,101,
                64,32,0,0,
                0,0,0,0};
            Assert.AreEqual(expected.Length, bytes.Length, "Arrays do not have same length");
            for (int i = 0; i < expected.Length; i++)
            {
                Assert.AreEqual(expected[i], bytes[i]);
            }
        }

        [TestMethod]
        public void Write_TAG_ByteArray()
        {
            TAG_ByteArray tag = new TAG_ByteArray()
            {
                Name = "ByteArray",
                Value = new byte[] { 12, 10, 8 }
            };
            NBTWriter writer = new NBTWriter();
            byte[] bytes = writer.GetBytes(tag);
            byte[] expected = new byte[]{ 7, 0, 9, 66,121,116,101,65,114,114,97,121,
                0,0,0,3,
                12, 10, 8 };
            Assert.AreEqual(expected.Length, bytes.Length, "Arrays do not have same length");
            for (int i = 0; i < expected.Length; i++)
            {
                Assert.AreEqual(expected[i], bytes[i]);
            }
        }

        [TestMethod]
        public void Write_TAG_IntArray()
        {
            TAG_IntArray tag = new TAG_IntArray()
            {
                Name = "IntArray",
                Value = new int[] { 12, 10, 8 }
            };
            NBTWriter writer = new NBTWriter();
            byte[] bytes = writer.GetBytes(tag);
            byte[] expected = new byte[]{ 11, 0, 8, 73,110,116,65,114,114,97,121,
                0,0,0,3,
                0,0,0,12,
                0,0,0,10,
                0,0,0,8 };
            Assert.AreEqual(expected.Length, bytes.Length, "Arrays do not have same length");
            for (int i = 0; i < expected.Length; i++)
            {
                Assert.AreEqual(expected[i], bytes[i]);
            }
        }

        [TestMethod]
        public void Write_TAG_String()
        {
            TAG_String tag = new TAG_String()
            {
                Name = "String",
                Value = "String1"
            };
            NBTWriter writer = new NBTWriter();
            byte[] bytes = writer.GetBytes(tag);
            byte[] expected = new byte[]  { 8, 0, 6, 83, 116, 114, 105, 110, 103,
                0,7,
                83,116,114,105,110,103,49};
            Assert.AreEqual(expected.Length, bytes.Length, "Arrays do not have same length");
            for (int i = 0; i < expected.Length; i++)
            {
                Assert.AreEqual(expected[i], bytes[i]);
            }
        }

        [TestMethod]
        public void Write_TAG_List()
        {
            TAG_List tag = new TAG_List()
            {
                Name = "List",
                TagId = 2,
                Value = new List<object>()
                {
                    1,2,3
                }
            };
            NBTWriter writer = new NBTWriter();
            byte[] bytes = writer.GetBytes(tag);
            byte[] expected = new byte[]  { 9, 0, 4, 76, 105, 115, 116,
                2,
                0, 0, 0, 3,
                0, 1, 0, 2, 0, 3};
            Assert.AreEqual(expected.Length, bytes.Length, "Arrays do not have same length");
            for (int i = 0; i < expected.Length; i++)
            {
                Assert.AreEqual(expected[i], bytes[i]);
            }
        }

        [TestMethod]
        public void Write_TAG_List_Of_Compound()
        {
            TAG_List tag = new TAG_List()
            {
                Name = "List",
                TagId = 10,
                Value = new List<object>()
                {
                    new TAG_Compound()
                    {
                        Value = new List<BaseTAG>()
                        {
                            new TAG_ByteArray()
                            {
                                Name="Blocks",
                                Value = new byte[] {1,2,3}
                            },
                            new TAG_ByteArray()
                            {
                                Name="Data",
                                Value = new byte[] {4,5,6}
                            },

                        }
                    },
                    new TAG_Compound()
                    {
                        Value = new List<BaseTAG>()
                        {
                            new TAG_ByteArray()
                            {
                                Name="Blocks",
                                Value = new byte[] {7,8,9}
                            },
                            new TAG_ByteArray()
                            {
                                Name="Data",
                                Value = new byte[] {10,11,12}
                            },

                        }
                    }
                }
            };
            NBTWriter writer = new NBTWriter();
            byte[] bytes = writer.GetBytes(tag);
            byte[] expected = new byte[]  { 9, 0, 4, 76, 105, 115, 116,
                10,
                0, 0, 0, 2,
                //First coumpound
                    7, 0, 6, 66, 108, 111, 99, 107, 115,//Tag Byte Blocks
                    0,0,0,3, 1, 2, 3,
                    7, 0, 4, 68, 97, 116, 97,//Tag Byte Data
                    0,0,0,3, 4, 5, 6,
                0,
                //Second coumpound
                    7, 0, 6, 66, 108, 111, 99, 107, 115,//Tag Byte Blocks
                    0,0,0,3, 7, 8, 9,
                    7, 0, 4, 68, 97, 116, 97,//Tag Byte Data
                    0,0,0,3, 10, 11, 12,
                0
            };
            Assert.AreEqual(expected.Length, bytes.Length, "Arrays do not have same length");
            for (int i = 0; i < expected.Length; i++)
            {
                Assert.AreEqual(expected[i], bytes[i]);
            }
        }


        [TestMethod]
        public void Write_TAG_Compound()
        {
            TAG_Compound tag = new TAG_Compound()
            {
                Name = "Compound",
                Value = new List<BaseTAG>()
                {
                    new TAG_Byte() { Value=12, Name="Byte" },
                    new TAG_Short() { Value=0x0B0C, Name="Short" },
                    new TAG_End() 
                }
            };
            NBTWriter writer = new NBTWriter();
            byte[] bytes = writer.GetBytes(tag);
            byte[] expected = new byte[]  { 10, 0, 8, 67, 111, 109, 112, 111, 117, 110, 100,
                1,0,4, 66, 121, 116, 101, 12, //TAG_Byte
                2, 0, 5, 83, 104, 111, 114, 116, 11, 12,//TAG_Short
                0};//TAG_End
            Assert.AreEqual(expected.Length, bytes.Length, "Arrays do not have same length");
            for (int i = 0; i < expected.Length; i++)
            {
                Assert.AreEqual(expected[i], bytes[i]);
            }
        }

    }
}
