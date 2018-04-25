using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace makefile
{
    class Program
    {
        static byte[] byteArray;

        static void Main(string[] args)
        {
            
            if (args == null || args.Length < 2)
            {
                Console.WriteLine("No parameters found !\nuse makefile.exe {filename} {file size in KB}.");
            }
            else
            {
                string fName = args[0];
                long fSize = long.Parse(args[1]);

                byteArray = new byte[1024];
                
                for (int i = 0; i < 1023; i++)
                {
                    byteArray[i] = byte.MaxValue;
                }

                Console.WriteLine("Generating file, stand by...");

                DoDaFile(fName, fSize);

                Console.WriteLine("\nProgram done the job.");
            }
        }

        static private bool DoDaFile(string FileName, long Size)
        {
            bool _ret = false;
            
            for (long i = 0; i < Size; i++)
            {
                try
                {
                    using (var fs = new FileStream(FileName, FileMode.Append, FileAccess.Write))
                    {
                        if ((i+1) % 512 == 0)
                        {
                            Console.WriteLine(string.Format("{0} kb / {1} kb", i + 1, Size));
                        }

                        fs.Write(byteArray, 0, byteArray.Length);
                        _ret = true;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception caught in process: {0}", ex);
                    _ret = false;
                }
            }
            return _ret;
        }
    }
}
