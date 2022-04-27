using System;
using System.IO;
using System.Collections.Generic;

namespace XorstrSol
{
    class Program
    {
        private static readonly string[] XorstrFileExtensions = { ".cpp", ".h", ".c", ".hpp" };

        static void Main(string[] args)
        {

            bool foundValidFiles = false;
            int fileParsedCount = 0;

            Directory.CreateDirectory("XORSTR_OUTPUT");

            List<string[]> fileEnumerations = new List<string[]>();

            foreach (string fileExtension in XorstrFileExtensions)
            {
                var fileEnumeration = Directory.GetFiles(Directory.GetCurrentDirectory(), "*" + fileExtension, SearchOption.AllDirectories);

                if (fileEnumeration.Length != 0)
                    foundValidFiles = true;

                foreach (string file in fileEnumeration)
                {
                    if (Path.GetFullPath(file).Contains("XORSTR_OUTPUT")) continue;

                    var result = Xorstr.ParseFile(file);

                    if (!result.Item1)
                    {
                        Console.WriteLine($"[Error - {result.Item2.Error}] - Parsing file: \"{Path.GetFullPath(file)}\"");
                    }
                    else
                    {
                        Console.WriteLine($"[Success] - Parsing file: \"{Path.GetFullPath(file)}\"");
                    }
                    
                    File.WriteAllText($"XORSTR_OUTPUT\\{Path.GetFileName(file)}", result.Item2.StringDataBuffer);
                    fileParsedCount += 1;
                }
            }

            if (!foundValidFiles)
            {
                Console.WriteLine("[Error] - No files found with C++ / C header or source extensions");
            }

            if (fileParsedCount != 0 && foundValidFiles)
            {
                Console.WriteLine($"[Success] - Outputted {fileParsedCount} file(s) to {Path.GetFullPath("..\\XORSTR_OUTPUT")}");
            }

            Console.ReadLine();
        }
    }
}
