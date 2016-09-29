using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace VideoCaptureForUnity
{
    public class VideoCapture
    {
        /// <summary>
        /// 视频存放目录，末尾不需要路径分隔符
        /// 默认为当前目录
        /// </summary>
        public string VideoDirectory
        {
            get
            {
                p.StandardInput.WriteLine("getDirectory");
                return p.StandardOutput.ReadLine().Trim();
            }

            set
            {
                p.StandardInput.WriteLine("setDirectory=" + value);
            }
        }
        /// <summary>
        /// 视频名字，末尾不包含视频格式
        /// 默认为video
        /// </summary>
        public string VideoName
        {
            get
            {
                p.StandardInput.WriteLine("getName");
                return p.StandardOutput.ReadLine().Trim();
            }

            set
            {
                p.StandardInput.WriteLine("setName=" + value);
            }
        }
        /// <summary>
        /// 视频分辨率的宽度
        /// 默认为1920
        /// </summary>
        public int ResolutionWidth
        {
            get
            {
                p.StandardInput.WriteLine("getResolutionWidth");
                return Convert.ToInt32(p.StandardOutput.ReadLine().Trim());
            }

            set
            {
                p.StandardInput.WriteLine("setResolutionWidth=" + value);
            }
        }
        /// <summary>
        /// 视频分辨率的高度
        /// 默认为1080
        /// </summary>
        public int ResolutionHeight
        {
            get
            {
                p.StandardInput.WriteLine("getResolutionHeight");
                return Convert.ToInt32(p.StandardOutput.ReadLine().Trim());
            }

            set
            {
                p.StandardInput.WriteLine("setResolutionHeight=" + value);
            }
        }
        /// <summary>
        /// （只读）视频格式的字符串
        /// 默认".mp4"
        /// </summary>
        public string VideoFormatString
        {
            get
            {
                p.StandardInput.WriteLine("getFormat");
                return p.StandardOutput.ReadLine().Trim();
            }

            private set
            {
            }
        }
        /// <summary>
        /// 帧率，每秒播放的帧的数量
        /// 默认20
        /// </summary>
        public int FrameRate
        {
            get
            {
                p.StandardInput.WriteLine("getFrameRate");
                return Convert.ToInt32(p.StandardOutput.ReadLine().Trim());
            }

            set
            {
                p.StandardInput.WriteLine("setFrameRate=" + value);
            }
        }
        /// <summary>
        /// 码率
        /// 默认2000000
        /// </summary>
        public int BitRate
        {
            get
            {
                p.StandardInput.WriteLine("getBitRate");
                return Convert.ToInt32(p.StandardOutput.ReadLine().Trim());
            }

            set
            {
                p.StandardInput.WriteLine("setBitRate=" + value);
            }
        }

        private string _videoCaptureExeFile = @".\VideoCapture\VideoCaptureCmd.exe";
        private Process p = new Process();
        private ProcessStartInfo info = new ProcessStartInfo();


        /// <summary>
        /// 初始化
        /// 默认目录为当前目录
        /// 默认视频名称为"video"
        /// </summary>
        public VideoCapture()
        {
            info.FileName = _videoCaptureExeFile;
            info.UseShellExecute = false;
            info.RedirectStandardInput = true;
            info.RedirectStandardOutput = false;
            info.CreateNoWindow = true;
            p.StartInfo = info;
            p.Start();
            p.StandardInput.WriteLine("init");
        }
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="videoCaptureCmdPath">VideoCaptureCmd.exe所在目录,末尾不需要路径分隔符，例如C:\VideoCaputureFolder</param>
        public VideoCapture(string videoCaptureCmdPath)
        {
            _videoCaptureExeFile = videoCaptureCmdPath + @"\VideoCaptureCmd.exe";
            info.FileName = _videoCaptureExeFile;
            info.UseShellExecute = false;
            info.RedirectStandardInput = true;
            info.RedirectStandardOutput = false;
            info.CreateNoWindow = true;
            p.StartInfo = info;
            p.Start();
            p.StandardInput.WriteLine("init");
        }


        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="videoDirectory">视频存放目录，末尾不需要路径分隔符</param>
        /// <param name="videoName">视频名字，末尾不包含视频格式</param>
        public VideoCapture(string videoDirectory, string videoName) : this()
        {
            p.StandardInput.WriteLine("setDirectory=" + videoDirectory);
            p.StandardInput.WriteLine("setName=" + videoName);
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="videoDirectory">视频存放目录，末尾不需要路径分隔符</param>
        /// <param name="videoName">视频名字，末尾不包含视频格式</param>
        /// <param name="videoCaptureCmdPath">VideoCaptureCmd.exe所在目录,末尾不需要路径分隔符，例如C:\VideoCaputureFolder</param>
        public VideoCapture(string videoDirectory, string videoName, string videoCaptureCmdPath) : this(videoCaptureCmdPath)
        {
            p.StandardInput.WriteLine("setDirectory=" + videoDirectory);
            p.StandardInput.WriteLine("setName=" + videoName);
        }
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="videoDirectory">视频存放目录，末尾不需要路径分隔符</param>
        /// <param name="videoName">视频名字，末尾不包含视频格式</param>
        /// <param name="resolutionWidth">视频分辨率的宽度</param>
        /// <param name="resolutionHeight">视频分辨率的高度></param>
        public VideoCapture(string videoDirectory, string videoName, int resolutionWidth, int resolutionHeight) :
            this(videoDirectory, videoName)
        {
            p.StandardInput.WriteLine("setResolutionWidth=" + resolutionWidth);
            p.StandardInput.WriteLine("setResolutionHeight=" + resolutionHeight);
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="videoDirectory">视频存放目录，末尾不需要路径分隔符</param>
        /// <param name="videoName">视频名字，末尾不包含视频格式</param>
        /// <param name="resolutionWidth">视频分辨率的宽度</param>
        /// <param name="resolutionHeight">视频分辨率的高度></param>
        /// <param name="videoCaptureCmdPath">VideoCaptureCmd.exe所在目录,末尾不需要路径分隔符，例如C:\VideoCaputureFolder</param>
        public VideoCapture(string videoDirectory, string videoName, int resolutionWidth, int resolutionHeight, string videoCaptureCmdPath) :
            this(videoDirectory, videoName, videoCaptureCmdPath)
        {
            p.StandardInput.WriteLine("setResolutionWidth=" + resolutionWidth);
            p.StandardInput.WriteLine("setResolutionHeight=" + resolutionHeight);
        }

        ~VideoCapture()
        {
            if (p != null)
            {
                p.StandardInput.WriteLine("quit");
                p.Close();
                p.Dispose();
            }
        }

        /// <summary>
        /// 开始录制
        /// 暂停后再继续用ContinueRecord方法
        /// </summary>
        public void StartRecord()
        {
            p.StandardInput.WriteLine("start");
        }
        /// <summary>
        /// 暂停录制
        /// </summary>
        public void PauseRecord()
        {
            p.StandardInput.WriteLine("pause");
        }


        /// <summary>
        /// 继续录制
        /// </summary>
        public void ContinueRecord()
        {
            p.StandardInput.WriteLine("continue");
        }

        /// <summary>
        /// 结束录制，并且保存视频
        /// </summary>
        public void StopRecord()
        {
            p.StandardInput.WriteLine("stop");
            p.StandardInput.WriteLine("quit");
            p.Close();
            p.Dispose();
            p = null;
        }
    }

}
