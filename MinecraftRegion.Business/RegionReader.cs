﻿using MinecraftRegion.Business.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

                byte[] headerChunk = new byte[5];
                int count = 0;
                while (binaryReader.Read(headerChunk, 0, 5) != 0)
                {
                    count++;
                    //byte          0 1 2 3             4                   5...
                    //description   length(in bytes)    compression type    compressed data (length - 1 bytes)
                    int length = (headerChunk[0] << 24) + (headerChunk[1] << 16) + (headerChunk[2] << 8) + headerChunk[3];
                    byte compresionType = headerChunk[4];
                    
                    switch (compresionType)
                    {
                        case 2:
                            byte[] compressedChunk = new byte[4091];
                            binaryReader.Read(compressedChunk, 0, 4091);
                            using (MemoryStream mStream = new MemoryStream(compressedChunk))
                            {
                                using (Ionic.Zlib.ZlibStream zLibStream = new Ionic.Zlib.ZlibStream(mStream, Ionic.Zlib.CompressionMode.Decompress))
                                {
                                    byte[] decompressedChunk = new byte[zLibStream.BufferSize];
                                    zLibStream.Read(decompressedChunk, 0, zLibStream.BufferSize);
                                }
                            }
                            break;
                        default:
                            throw new NotImplementedException("Only compression ZLib is implemented");
                    }
                }
            }
        }

        private IEnumerable<int> ReadTimeStamps(byte[] headerTimestamps)
        {
            for (int iTimeStamp = 0; iTimeStamp < 4096; iTimeStamp += 4)
            {
                yield return (headerTimestamps[iTimeStamp + 0] << 24) + (headerTimestamps[iTimeStamp + 1] << 16) + (headerTimestamps[iTimeStamp + 2] << 8) + (headerTimestamps[iTimeStamp + 3]);
            }
        }

        private IEnumerable<ChunkLocation> ReadLocations(byte[] headerLocations)
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
                locations.Add(new ChunkLocation()
                {
                    Offset = (location[0] << 16) + (location[1] << 8) + location[2],
                    SectorCount = location[3]
                });
            }

            return locations;
        }
    }
}
