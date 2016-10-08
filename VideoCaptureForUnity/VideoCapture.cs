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
                _process.StandardInput.WriteLine("getDirectory");
                return _process.StandardOutput.ReadLine().Trim();
            }

            set
            {
                _process.StandardInput.WriteLine("setDirectory=" + value);
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
                _process.StandardInput.WriteLine("getName");
                return _process.StandardOutput.ReadLine().Trim();
            }

            set
            {
                _process.StandardInput.WriteLine("setName=" + value);
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
                _process.StandardInput.WriteLine("getResolutionWidth");
                return Convert.ToInt32(_process.StandardOutput.ReadLine().Trim());
            }

            set
            {
                _process.StandardInput.WriteLine("setResolutionWidth=" + value);
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
                _process.StandardInput.WriteLine("getResolutionHeight");
                return Convert.ToInt32(_process.StandardOutput.ReadLine().Trim());
            }

            set
            {
                _process.StandardInput.WriteLine("setResolutionHeight=" + value);
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
                _process.StandardInput.WriteLine("getFormat");
                return _process.StandardOutput.ReadLine().Trim();
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
                _process.StandardInput.WriteLine("getFrameRate");
                return Convert.ToInt32(_process.StandardOutput.ReadLine().Trim());
            }

            set
            {
                _process.StandardInput.WriteLine("setFrameRate=" + value);
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
                _process.StandardInput.WriteLine("getBitRate");
                return Convert.ToInt32(_process.StandardOutput.ReadLine().Trim());
            }

            set
            {
                _process.StandardInput.WriteLine("setBitRate=" + value);
            }
        }

        private string _videoCaptureExeFile = @".\VideoCapture\VideoCaptureCmd.exe";
        private Process _process = new Process();
        private ProcessStartInfo _info = new ProcessStartInfo();
        private bool _isRecording = false;

        /// <summary>
        /// 初始化
        /// 默认目录为当前目录
        /// 默认视频名称为"video"
        /// </summary>
        public VideoCapture()
        {
            _info.FileName = _videoCaptureExeFile;
            _info.UseShellExecute = false;
            _info.RedirectStandardInput = true;
            _info.RedirectStandardOutput = true;
            _info.CreateNoWindow = true;
            _process.StartInfo = _info;
            _process.Start();
            _process.StandardInput.WriteLine("init");
        }
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="videoCaptureCmdPath">VideoCaptureCmd.exe所在目录,末尾不需要路径分隔符，例如C:\VideoCaputureFolder</param>
        public VideoCapture(string videoCaptureCmdPath)
        {
            _videoCaptureExeFile = videoCaptureCmdPath + @"\VideoCaptureCmd.exe";
            _info.FileName = _videoCaptureExeFile;
            _info.UseShellExecute = false;
            _info.RedirectStandardInput = true;
            _info.RedirectStandardOutput = false;
            _info.CreateNoWindow = true;
            _process.StartInfo = _info;
            _process.Start();
            _process.StandardInput.WriteLine("init");
        }
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="videoDirectory">视频存放目录，末尾不需要路径分隔符</param>
        /// <param name="videoName">视频名字，末尾不包含视频格式</param>
        public VideoCapture(string videoDirectory, string videoName) : this()
        {
            _process.StandardInput.WriteLine("setDirectory=" + videoDirectory);
            _process.StandardInput.WriteLine("setName=" + videoName);
        }
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="videoDirectory">视频存放目录，末尾不需要路径分隔符</param>
        /// <param name="videoName">视频名字，末尾不包含视频格式</param>
        /// <param name="videoCaptureCmdPath">VideoCaptureCmd.exe所在目录,末尾不需要路径分隔符，例如C:\VideoCaputureFolder</param>
        public VideoCapture(string videoDirectory, string videoName, string videoCaptureCmdPath) : this(videoCaptureCmdPath)
        {
            _process.StandardInput.WriteLine("setDirectory=" + videoDirectory);
            _process.StandardInput.WriteLine("setName=" + videoName);
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
            _process.StandardInput.WriteLine("setResolutionWidth=" + resolutionWidth);
            _process.StandardInput.WriteLine("setResolutionHeight=" + resolutionHeight);
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
            _process.StandardInput.WriteLine("setResolutionWidth=" + resolutionWidth);
            _process.StandardInput.WriteLine("setResolutionHeight=" + resolutionHeight);
        }

        ~VideoCapture()
        {
            if (_process != null)
            {
                if (_isRecording)
                {
                    StopRecord();
                }
                _process.Kill();
                _process.Dispose();
                _process = null;
            }
        }
        /// <summary>
        /// 开始录制
        /// 暂停后再继续用ContinueRecord方法
        /// </summary>
        public void StartRecord()
        {
            if (_isRecording)
            {
                return;
            }
            _process.StandardInput.WriteLine("start");
            _isRecording = true;
        }
        /// <summary>
        /// 暂停录制
        /// </summary>
        public void PauseRecord()
        {
            if (!_isRecording)
            {
                return;
            }
            _process.StandardInput.WriteLine("pause");
        }
        /// <summary>
        /// 继续录制
        /// </summary>
        public void ContinueRecord()
        {
            if (!_isRecording)
            {
                return;
            }
            _process.StandardInput.WriteLine("continue");
        }
        /// <summary>
        /// 结束录制，并且保存视频
        /// </summary>
        public void StopRecord()
        {
            if (!_isRecording)
            {
                return;
            }
            _process.StandardInput.WriteLine("stop");
            _isRecording = false;
        }
    }
}
