using System;
using System.IO;


namespace XorstrSol
{
    class Program
    {
        private static readonly string[] XorstrFileExtensions = { ".cpp", ".h", ".c", ".hpp" };

        static void Main(string[] args)
        {
            foreach (string fileExtension in XorstrFileExtensions)
            {
                foreach (string file in Directory.EnumerateFiles("..\\", "*" + fileExtension, SearchOption.AllDirectories))
                {
                    var result = Xorstr.ParseFile(file);

                    if (!result.Item1)
                    {
                        Console.WriteLine(result.Item2.Error);
                    }

                    Console.WriteLine(result.Item2.StringDataBuffer);
                }
            }

        }
    }
}
