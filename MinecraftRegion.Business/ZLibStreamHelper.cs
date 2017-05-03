using Ionic.Zlib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinecraftRegion.Business
{
    public class ZLibStreamHelper : IDisposable
    {
        ZlibStream innerStream;

        public Stream BaseStream { get { return innerStream; } }

        public static ZLibStreamHelper ForDecompression(Stream stream)
        {
            return new ZLibStreamHelper(stream, CompressionMode.Decompress);
        }
        public static ZLibStreamHelper ForCompression(Stream stream)
        {
            return new ZLibStreamHelper(stream, CompressionMode.Compress);
        }
        private ZLibStreamHelper(Stream stream, CompressionMode mode)
        {
            innerStream = new ZlibStream(stream, mode);
        }
        public void Dispose()
        {
            innerStream.Dispose();
            innerStream = null;
        }
    }
}
