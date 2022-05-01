using Latch.FileAnalyzer;
using Microsoft.VisualBasic.FileIO;
using System;
using System.IO;

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
                Console.WriteLine($"{Path.GetFileName(item)}: {r}");
            }
        }
    }
}
