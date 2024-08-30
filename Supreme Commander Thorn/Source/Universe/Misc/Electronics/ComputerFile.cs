using System;

namespace Supreme_Commander_Thorn
{
    public class ComputerFile
    {
        public String Path { get; set; }
        public String Content { get; set; }

        public ComputerFile() { }
        public ComputerFile(String path) { }
        public ComputerFile(string path, String content) { }
    }
}