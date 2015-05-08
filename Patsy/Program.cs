using System;
using System.IO;
using System.Threading;
using WMPLib;

namespace Patsy
{
    class Program
    {
        private const int START_HOUR = 9;
        private const int FINISH_HOUR = 17;

        private const int MIN_RANDOM_TIME = 1000 * 30;
        private const int MAX_RANDOM_TIME = 1000 * 120;

        private const string DIRECTORY = @"C:\Users\fw\Music\patsy-ljud";

        static void Main(string[] args)
        {
            WindowsMediaPlayer player = new WindowsMediaPlayer();
            string[] files = Directory.GetFiles(DIRECTORY);

            Random rand = new Random();
            PictureHandling pictureHandling = new PictureHandling();
            string fileFormat = string.Empty;

            while (true)
            {
                if (DateTime.Now.Hour >= START_HOUR && DateTime.Now.Hour <= FINISH_HOUR)
                {
                    foreach (var file in files)
                    {
                        Console.Clear();

                        var next = rand.Next(MIN_RANDOM_TIME, MAX_RANDOM_TIME);
                        Console.Write("Sleeping for: {0}s\n", (next / 1000));
                        Thread.Sleep(next);

                        Console.Write("Playing {0}\n", file);
                        player.URL = file;
                        player.controls.play();

                        // wait for patsy to show up.
                        Thread.Sleep(1000 * 10);
                        fileFormat = string.Format("{0}{1}", DateTime.Now.ToString().Replace(":", "-"), ".bmp");
                        pictureHandling.TakePicture(fileFormat);
                    }
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Waiting for the time to be between {0} and {1}", START_HOUR, FINISH_HOUR);
                }

                Thread.Sleep(1000 * 10);
            }
        }
    }
}
