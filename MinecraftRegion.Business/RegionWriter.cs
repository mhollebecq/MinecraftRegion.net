using MinecraftRegion.Business.Models;
using NBT.Business;
using NBT.Business.Models.Tags;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinecraftRegion.Business
{
    public class RegionWriter
    {
        public void WriteRegionsToFiles(string basePath, IEnumerable<Region> regions)
        {
            foreach (Region region in regions)
            {
                string fileName = string.Format("r.{0}.{1}.mca", region.X, region.Z);
                using (FileStream fileStream = new FileStream(Path.Combine(basePath, fileName), FileMode.Create))
                {
                    WriteRegionToStream(region, fileStream);
                }
            }

        }
        public void WriteRegionToStream(Region regionToWrite, Stream stream)
        {
            using (BinaryWriter binaryWriter = new BinaryWriter(stream))
            {
                byte[] headerLocations = new byte[4096];
                byte[] headerTimestamps = new byte[4096];

                NBTWriter nbtWriter = new NBTWriter();
                int total4096words = 2;
                int iLocationAndTimestamp = 0;
                List<byte[]> sectorsList = new List<byte[]>();
                foreach (Chunk location in regionToWrite.Locations)
                {
                    TAG_Compound chunkTag = GetChunkTag(location);

                    byte[] chunkBytes = nbtWriter.GetBytes(chunkTag);
                    using (MemoryStream mStream = new MemoryStream())
                    {
                        using (ZLibStreamHelper zLibStream = ZLibStreamHelper.ForCompression(mStream, true))
                        {
                            zLibStream.BaseStream.Write(chunkBytes, 0, chunkBytes.Length);
                            zLibStream.BaseStream.Flush();
                        }
                        int compressedDataLengthPlusHeader = (int)mStream.Length + 5;
                        int sectorCount = (int)(compressedDataLengthPlusHeader / 4046) +
                            (compressedDataLengthPlusHeader % 4096 == 0 ? 0 : 1);

                        headerLocations[iLocationAndTimestamp] = (byte)((total4096words & 0xFF0000) >> 16);
                        headerLocations[iLocationAndTimestamp + 1] = (byte)((total4096words & 0xFF00) >> 8);
                        headerLocations[iLocationAndTimestamp + 2] = (byte)((total4096words & 0xFF) >> 0);
                        headerLocations[iLocationAndTimestamp + 3] = (byte)(sectorCount);

                        headerTimestamps[iLocationAndTimestamp] = (byte)((location.Timestamp & 0xFF000000) >> 24);
                        headerTimestamps[iLocationAndTimestamp + 1] = (byte)((location.Timestamp & 0xFF0000) >> 16);
                        headerTimestamps[iLocationAndTimestamp + 2] = (byte)((location.Timestamp & 0xFF00) >> 8);
                        headerTimestamps[iLocationAndTimestamp + 3] = (byte)(location.Timestamp & 0xFF);

                        mStream.Position = 0;
                        byte[] sectorBytes = new byte[4096 * sectorCount];
                        sectorBytes[0] = (byte)((mStream.Length & 0xFF000000) >> 24);
                        sectorBytes[1] = (byte)((mStream.Length & 0xFF0000) >> 16);
                        sectorBytes[2] = (byte)((mStream.Length & 0xFF00) >> 8);
                        sectorBytes[3] = (byte)((mStream.Length & 0xFF) >> 0);
                        sectorBytes[4] = 2;
                        mStream.Read(sectorBytes,
                            5,
                            (int)mStream.Length);
                        sectorsList.Add(sectorBytes);
                        total4096words += sectorCount;
                        iLocationAndTimestamp += 4;
                    }
                }
                binaryWriter.Write(headerLocations);

                binaryWriter.Write(headerTimestamps);

                foreach (byte[] sectorBytes in sectorsList)
                {
                    binaryWriter.Write(sectorBytes);
                }

                binaryWriter.Flush();
            }
        }

        private static TAG_Compound GetChunkTag(Chunk location)
        {
            TAG_Compound chunkTag = new TAG_Compound();
            chunkTag.Value.Add(new TAG_Int()
            {
                Name = "DataVersion",
                Value = location.Sector.DataVersion
            });
            TAG_Compound levelTag = GetLevelTag(location);
            chunkTag.Value.Add(levelTag);
            return chunkTag;
        }

        private static TAG_Compound GetLevelTag(Chunk location)
        {
            TAG_Compound levelTag = new TAG_Compound();
            levelTag.Name = "Level";
            levelTag.Value.Add(new TAG_Int()
            {
                Name = "xPos",
                Value = location.Sector.Level.XPos
            });
            levelTag.Value.Add(new TAG_Int()
            {
                Name = "zPos",
                Value = location.Sector.Level.ZPos
            });
            return levelTag;
        }
    }
}
