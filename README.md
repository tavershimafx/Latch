# LatchFileAnalyzer
The latch file analyzer is a binary file checker which determines the actual type of a file by reading
the file byte headers which signifies its precise type.

To determine the type of file, the file analyzer provides a static method `FileChecker.IsValidFileTypeExtension`
which you could pass numerous file extensions which would return a true or false flag which determines if the
provided `Stream` or `byte[]` is a valid type of any of the extensions provided.

The latch file analyzer also contains methods in the [FileChecker](./FileChecker.cs) which can determine the
file type by supplying the bytes read from the file or a stream derived from reading the file.

## Limitations
The `LatchFileAnalyzer` has a large collection of file headers and types which offers the file checking,
nevertheless, it does not contain all the file types and headers for some file types.

The collection is still growing and we add more as we discover them.

## Install

```
PM> Install-Package Latch -Version 1.0.0
```

## Usage

``` C#
using system.IO;
using Latch.FileAnalyzer;

using namespace MyProgram
{
	public static class Program
	{
		public static Main()
		{
			var s = new StreamReader("C:\myfile.png")
			bool isImage = FileChecker.IsValidFileTypeExtension(s, ".png", ".jpg", ".svg")
		}
	}
}
```