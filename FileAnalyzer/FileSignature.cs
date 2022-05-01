using System.Collections;
using System.Security.Cryptography;

namespace Latch.FileAnalyzer
{
    internal sealed class FileSignature
	{
		public int Offset { get; set; }

		public byte[] Bytes { get; set; }
	}
}
