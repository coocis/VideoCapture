﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoCaptureForUnity;
using System.IO;

namespace VideoCaptureForUnityTest
{
    class Program
    {
        static void Main(string[] args)
        {
            VideoCapture vc = new VideoCapture();
            string s = "";
            while (!(s = Console.ReadLine()).Contains("quit"))
            {
                try
                {
                    if (s.Contains("init"))
                    {
                        vc = new VideoCapture();
                    }

                    if (s.Contains("start"))
                    {
                        vc.StartRecord();
                    }

                    if (s.Contains("stop"))
                    {
                        vc.StopRecord();
                    }

                    if (s.Contains("pause"))
                    {
                        vc.PauseRecord();
                    }

                    if (s.Contains("continue"))
                    {
                        vc.ContinueRecord();
                    }

                    if (s.Contains("setDirectory"))
                    {
                        vc.VideoDirectory = s.Split('=')[1];
                    }

                    if (s.Contains("getDirectory"))
                    {
                        Console.WriteLine(vc.VideoDirectory);
                    }

                    if (s.Contains("setName"))
                    {
                        vc.VideoName = s.Split('=')[1];
                    }

                    if (s.Contains("getName"))
                    {
                        Console.WriteLine(vc.VideoName);
                    }

                    if (s.Contains("getFormat"))
                    {
                        Console.WriteLine(vc.VideoFormatString);
                    }

                    if (s.Contains("setResolutionWidth"))
                    {
                        vc.ResolutionWidth = Convert.ToInt32(s.Split('=')[1]);
                    }
                    if (s.Contains("getResolutionWidth"))
                    {
                        Console.WriteLine(vc.ResolutionWidth);
                    }

                    if (s.Contains("setResolutionHeight"))
                    {
                        vc.ResolutionHeight = Convert.ToInt32(s.Split('=')[1]);
                    }
                    if (s.Contains("getResolutionHeight"))
                    {
                        Console.WriteLine(vc.ResolutionHeight);
                    }
                    if (s.Contains("setFrameRate"))
                    {
                        vc.FrameRate = Convert.ToInt32(s.Split('=')[1]);
                    }
                    if (s.Contains("getFrameRate"))
                    {
                        Console.WriteLine(vc.FrameRate);
                    }
                    if (s.Contains("setBitRate"))
                    {
                        vc.BitRate = Convert.ToInt32(s.Split('=')[1]);
                    }
                    if (s.Contains("getBitRate"))
                    {
                        Console.WriteLine(vc.BitRate);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
        }
    }
}
