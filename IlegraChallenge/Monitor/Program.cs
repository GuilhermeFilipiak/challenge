using IlegraChallenge.Business;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IlegraChallenge.Monitor
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Back End Technical Challenge");
            Console.WriteLine("=======================================");

            string sPathIn = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "data", "in");
            string sPathOut = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "data", "out");

            if (!Directory.Exists(sPathIn))
            {
                Directory.CreateDirectory(sPathIn);
            }

            if (!Directory.Exists(sPathOut))
            {
                Directory.CreateDirectory(sPathOut);
            }

            Console.WriteLine("Press any key to start...");
            Console.ReadKey();

            Console.WriteLine("");
            Console.WriteLine("\r> Monitoring...");
            Console.WriteLine("");

            try
            {
                InputLoopProcessor(sPathIn, sPathOut);
            }
            catch (Exception)
            {
                //generate log here
            }

            Console.SetCursorPosition(0, 13);
            Console.WriteLine("Press any key to cancel...");
            Console.ReadKey();
        }

        /// <summary>
        /// Monitors data entry files within 1 second interval
        /// </summary>
        /// <param name="sPathIn"></param>
        /// <param name="sPathOut"></param>
        static async void InputLoopProcessor(string sPathIn, string sPathOut)
        {
            Report report = new Report(sPathIn);

            while (true)
            {
                await Task.Run(() =>
                {
                    System.Threading.Thread.Sleep(1000);

                    report.GenerateDatFilesSummary(sPathOut);

                    var path = Path.Combine(sPathOut, "summary.done.dat");
                    if (File.Exists(path))
                    {
                        Console.SetCursorPosition(0, 5);
                        Console.WriteLine("\r\n================Summary================");

                        foreach (var line in File.ReadAllLines(path))
                        {
                            Console.WriteLine(line + new string(' ', Console.WindowWidth - line.Length - 1));
                        }

                        Console.WriteLine("\r=======================================");
                    }
                });
            }
        }
    }
}
