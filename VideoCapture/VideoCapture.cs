using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using System.IO;
using System.Drawing;
using System.Windows.Forms;
using AForge.Video;
using AForge.Video.FFMPEG;
using MediaToolkit;
using MediaToolkit.Model;
using MediaToolkit.Options;

namespace VideoCapture
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
                return _videoDirectory;
            }

            set
            {
                _videoDirectory = value;
            }
        }
        /// <summary>
        /// 视频名字，末尾不包含视频格式
        /// 默认为"video"
        /// </summary>
        public string VideoName
        {
            get
            {
                return _videoName;
            }

            set
            {
                _videoName = value;
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
                return _resolutionWidth;
            }

            set
            {
                _resolutionWidth = value;
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
                return _resolutionHeight;
            }

            set
            {
                _resolutionHeight = value;
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
                return _videoFormatString;
            }

            private set
            {
                _videoFormatString = value;
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
                return _frameRate;
            }

            set
            {
                _frameRate = value;
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
                return _bitRate;
            }

            set
            {
                _bitRate = value;
            }
        }
        #region private
        private string _videoDirectory = Environment.CurrentDirectory;
        private string _videoName = "video";
        private int _resolutionWidth = 1920;
        private int _resolutionHeight = 1080;
        private string _videoFormatString = ".mp4";
        private int _frameRate = 20;
        private int _bitRate = 2000000;
        private ScreenCaptureStream _stream;
        private VideoFileWriter writer;
        private Rectangle screenArea = Rectangle.Empty;
        private bool _isPause = false;
        private bool _isRecording = false;
        #endregion //private
        /// <summary>
        /// 初始化
        /// 默认目录为当前目录
        /// 默认视频名称为"video"
        /// </summary>
        public VideoCapture()
        {
        }
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="videoDirectory">视频存放目录，末尾不需要路径分隔符</param>
        /// <param name="videoName">视频名字，末尾不包含视频格式</param>
        public VideoCapture(string videoDirectory, string videoName)
        {
            _videoDirectory = videoDirectory;
            _videoName = videoName;
        }
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="videoDirectory">视频存放目录，末尾不需要路径分隔符</param>
        /// <param name="videoName">视频名字，末尾不包含视频格式</param>
        /// <param name="resolutionWidth">视频分辨率的宽度</param>
        /// <param name="resolutionHeight">视频分辨率的高度></param>
        public VideoCapture(string videoDirectory, string videoName, int resolutionWidth, int resolutionHeight)
        {
            _videoDirectory = videoDirectory;
            _videoName = videoName;
            _resolutionWidth = resolutionWidth;
            _resolutionHeight = resolutionHeight;
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
            _isRecording = true;
            foreach (Screen screen in
                      Screen.AllScreens)
            {
                screenArea = Rectangle.Union(screenArea, screen.Bounds);
            }
            string file = _videoDirectory + Path.DirectorySeparatorChar + _videoName + "tmp" + _videoFormatString;
            writer = new VideoFileWriter();
            writer.Open(file, _resolutionWidth, _resolutionHeight, _frameRate, VideoCodec.MPEG4, _bitRate);
            // create screen capture video source
            _stream = new ScreenCaptureStream(screenArea);
            _stream.FrameInterval = (1000 / _frameRate);
            // set NewFrame event handler
            _stream.NewFrame += new NewFrameEventHandler(video_NewFrame);

            // start the video source
            _stream.Start();

        }
        /// <summary>
        /// 暂停录制
        /// </summary>
        public void PauseRecord()
        {
            _isPause = true;
        }
        /// <summary>
        /// 继续录制
        /// </summary>
        public void ContinueRecord()
        {
            _isPause = false;
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
            _isRecording = false;
            _isPause = false;
            writer.Close();
            _stream.SignalToStop();
            //转成H.264（AVC/MPEG P10)格式
            var inputFile = new MediaFile { Filename = _videoDirectory + Path.DirectorySeparatorChar + _videoName + "tmp" + _videoFormatString };
            var outputFile = new MediaFile { Filename = _videoDirectory + Path.DirectorySeparatorChar + _videoName + _videoFormatString };
            using (var engine = new Engine())
            {
                ConversionOptions opt = new ConversionOptions()
                {
                };
                engine.Convert(inputFile, outputFile);
            }
            File.Delete(_videoDirectory + Path.DirectorySeparatorChar + _videoName + "tmp" + _videoFormatString);
        }

        private void video_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            Bitmap origin = eventArgs.Frame;
            Bitmap resize = origin;
            using (Graphics g = Graphics.FromImage(origin))
            {
                Rectangle cursor = new Rectangle(Control.MousePosition, new Size(18, 13));
                Cursors.Default.Draw(g, cursor);
            }
            resize = new Bitmap(origin, new Size(_resolutionWidth, _resolutionHeight));
            if (writer.IsOpen && !_isPause)
            {
                try
                {
                    writer.WriteVideoFrame(resize);
                }
                catch (Exception)
                {
                }
            }
            resize.Dispose();
        }
    }
}
