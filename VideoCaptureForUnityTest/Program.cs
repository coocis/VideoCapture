using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoCaptureForUnity;

namespace VideoCaptureForUnityTest
{
    class Program
    {
        static void Main(string[] args)
        {
            VideoCapture vc = new VideoCapture();
            string s;
            s = Console.ReadLine();
            if (s.Contains("start"))
            {
                vc.StartRecord();
            }
            s = Console.ReadLine();
            if (s.Contains("stop"))
            {
                vc.StopRecord();
            }
        }
    }
}
