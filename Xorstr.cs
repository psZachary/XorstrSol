using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace XorstrSol
{
    enum XorstrError
    {
        None,
        InvalidFile
    }
    class Xorstr
    {
        public byte[] DataBuffer { get; set; }

        public string StringDataBuffer { get; set; }

        public XorstrError Error { get; set; }

        public Xorstr()
        {
            StringDataBuffer = "";
            Error = XorstrError.None;
        }

        public static Tuple<bool, Xorstr> ParseFile(string filePath)
        {
            Xorstr xorstr = new Xorstr();

            byte[] fileData = File.ReadAllBytes(filePath);
            if (fileData.Length <= 0)
            {
                xorstr.Error = XorstrError.InvalidFile;
                return new Tuple<bool, Xorstr>(false, xorstr);
            }

            xorstr.DataBuffer = new byte[fileData.Length];

            var fileText = Encoding.UTF8.GetString(fileData);
            if (fileText == "" || fileText.Length == 0)
            {
                xorstr.Error = XorstrError.InvalidFile;
                return new Tuple<bool, Xorstr>(false, xorstr);
            }

            bool hasFoundString = false;
            bool addEndParentheses = false;

            var lines = fileText.Split('\n');

            for (int z = 0; z < lines.Length; z++)
            {
                var line = lines[z];
                    for (int i = 0; i < line.Length; i++)
                    {
                        xorstr.StringDataBuffer += line[i];
                        if (!line.ToLower().StartsWith("#pragma") && !line.ToLower().StartsWith("#include"))
                        {
                            if (addEndParentheses)
                            {
                                xorstr.StringDataBuffer += ")";
                                addEndParentheses = false;
                                hasFoundString = false;
                            }
                            if (i >= line.Length - 1) continue;

                            if (line[i + 1] == '"' && !hasFoundString)
                            {
                                hasFoundString = true;
                                xorstr.StringDataBuffer += "xorstr_(";
                                continue;
                            }
                            if (line[i + 1] == '"' && hasFoundString)
                            {
                                addEndParentheses = true;
                                continue;
                            }
                        }
                    }

            }

            xorstr.DataBuffer = Encoding.UTF8.GetBytes(xorstr.StringDataBuffer);

            return new Tuple<bool, Xorstr>(true, xorstr);
        }
    }
}
