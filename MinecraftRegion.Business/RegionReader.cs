using MinecraftRegion.Business.Models;
using System;
using System.Collections.Generic;
using System.IO;
using NBT.Business.Models.Tags;
using NBT.Business;
using System.Linq;
using System.Diagnostics;

namespace MinecraftRegion.Business
{
    public class RegionReader
    {
        public IEnumerable<Region> ReadFolder(string path)
        {
            List<Region> regions = new List<Region>();
            foreach (var file in System.IO.Directory.GetFiles(path, "r.*.*.mca"))
            {
                Region region = ReadOneRegion(file);
                regions.Add(region);
            }
            return regions;
        }

        public Region ReadOneRegion(string file)
        {
            string[] split = new FileInfo(file).Name.Split('.');
            int x = int.Parse(split[1]);
            int y = int.Parse(split[2]);
            Region region = new Region();
            region.Locations = ReadOneRegionLocations(file).ToList();
            region.X = x;
            region.Z = y;
            region.Path = file;
            return region;
        }

        private IEnumerable<Chunk> ReadOneRegionLocations(string file)
        {
            using (BinaryReader binaryReader = new BinaryReader(File.OpenRead(file)))
            {
                List<Chunk> chunks = new List<Chunk>();
                byte[] headerLocations = new byte[4096];
                byte[] headerTimestamps = new byte[4096];
                int read = binaryReader.Read(headerLocations, 0, 4096);
                read = binaryReader.Read(headerTimestamps, 0, 4096);
                var locations = ReadLocations(headerLocations);
                var timeStamps = ReadTimeStamps(headerTimestamps);
                for (int iLocation = 0; iLocation < locations.Count; iLocation++)
                {
                    int timestamp = timeStamps[iLocation];
                    Chunk location = locations[iLocation];
                    location.Timestamp = timestamp;
                    binaryReader.BaseStream.Position = location.Offset * 4096;
                    byte[] headerChunk = new byte[5];
                    binaryReader.Read(headerChunk, 0, 5);
                    //byte          0 1 2 3             4                   5...
                    //description   length(in bytes)    compression type    compressed data (length - 1 bytes)
                    int length = (headerChunk[0] << 24) + (headerChunk[1] << 16) + (headerChunk[2] << 8) + headerChunk[3];
                    byte compresionType = headerChunk[4];

                    switch (compresionType)
                    {
                        case 2:
                            var sector = ParseByCompressionZLib(binaryReader, length);
                            location.Sector = sector ;
                            int x = sector.Level.XPos, z = sector.Level.ZPos;
                            Debug.WriteLine($"{location.Offset} {x} {z} {(4 * ((x & 31) + (z & 31) * 32))}");
                            break;
                        default:
                            throw new NotImplementedException("Only compression ZLib is implemented");
                    }
                    chunks.Add(location);
                }
                return chunks;
            }
        }

        private ChunkSector ParseByCompressionZLib(BinaryReader binaryReader, int length)
        {
            byte[] compressedChunk = new byte[length];
            binaryReader.Read(compressedChunk, 0, length);
            using (MemoryStream mStream = new MemoryStream(compressedChunk))
            {
                using (ZLibStreamHelper zLibStream = ZLibStreamHelper.ForDecompression(mStream))
                {
                    NBTReader nbtReader = new NBTReader();
                    SectorReader sectorReader = new SectorReader();
                    ChunkSector sector = sectorReader.GetSector(nbtReader.GetTag(zLibStream.BaseStream));
                    return sector;
                }
            }
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

        private List<Chunk> ReadLocations(byte[] headerLocations)
        {
            //Location information for a chunk consists of four bytes split into two fields: the first three bytes 
            //are a (big - endian) offset in 4KiB sectors from the start of the file, 
            //and a remaining byte which gives the length of the chunk (also in 4KiB sectors, rounded up).Chunks will always be less than 1MiB in size.
            //If a chunk isn't present in the region file (e.g. because it hasn't been generated or migrated yet), both fields will be zero.

            //byte          0 1 2   3
            //description   offset  sector count
            //A chunk with an offset of 2 will begin right after the timestamps table.
            List<Chunk> locations = new List<Chunk>();
            for (int iLocation = 0; iLocation < 4096; iLocation += 4)
            {
                byte[] location = new byte[4] { headerLocations[iLocation], headerLocations[iLocation + 1], headerLocations[iLocation + 2], headerLocations[iLocation + 3] };
                int offset = (location[0] << 16) + (location[1] << 8) + location[2];
                byte sectorCount = location[3];
                if (offset != 0 && sectorCount != 0)
                    locations.Add(new Chunk()
                    {
                        Offset = offset,
                        SectorCount = sectorCount
                    });
            }

            return locations;
        }
    }
}
