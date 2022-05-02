# LatchFileAnalyzer
The latch file analyzer is a binary file checker which determines the actual type of a file by reading
the file byte headers which signifies its precise type.

To determine the type of file, the file analyzer provides a static method `FileChecker.IsValidFileTypeExtension`
which you could pass numerous file extensions which would return a true or false flag which determines if the
provided `Stream` or `byte[]` is a valid type of any of the extensions provided.

The latch file analyzer also contains methods in the [FileChecker](./FileAnalyzer/FileChecker.cs) which can determine the
file type by supplying the bytes read from the file or a stream derived from reading the file.

## Limitations
The `LatchFileAnalyzer` has a large collection of file headers and types which offers the file checking,
nevertheless, it does not contain all the file types and headers for some file types.

The collection is still growing and we add more as we discover them.

## Install

```
PM> Install-Package Latch -Version 1.0.1
```

## Usage

``` C#
using System.IO;
using Latch.FileAnalyzer;
using Microsoft.VisualBasic.FileIO;

namespace FileAnalyzer.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var files = Directory.EnumerateFiles(SpecialDirectories.MyPictures);
            foreach (var item in files)
            {
                var s = new StreamReader(item);
                bool isImage = FileChecker.IsValidFileTypeExtension(s.BaseStream, ".png", ".jpg", ".svg");
                Console.WriteLine($"{isImage}: {Path.GetFileName(item)}");
            }
        }
    }
}
```

Or you could pass in any stream or buffer array for it to determine the file type. If the stream or buffer
is not a valid file (just a random stream) the `FileChecker` will return false.

### Note
This might take longer as the list of headers increases.

``` C#
using System.IO;
using Latch.FileAnalyzer;
using Microsoft.VisualBasic.FileIO;
namespace FileAnalyzer.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var files = Directory.EnumerateFiles(SpecialDirectories.MyPictures);
            foreach (var item in files)
            {
                var s = new StreamReader(item);
                bool r = FileChecker.GetFileTypeExtension(s.BaseStream, out string ext);
                Console.WriteLine($"{r}: {Path.GetFileName(item)}");
            }
        }
    }
}
```