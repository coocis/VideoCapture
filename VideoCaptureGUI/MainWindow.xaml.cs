using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Interop;
using VideoCapture;

namespace VideoCaptureGUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private VideoCapture.VideoCapture _videoCapturer;
        private bool _isRecording = false;
        private IntPtr _windowHandle;
        private HwndSource _source;

        [DllImport("user32.dll")]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, uint vk);
        [DllImport("user32.dll")]
        private static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        private const int F8_HOTKEY_ID = 8000;
        private const int F9_HOTKEY_ID = 9000;
        //Modifiers:
        private const uint MOD_NONE = 0x0000; //[NONE]
        private const uint MOD_ALT = 0x0001; //ALT
        private const uint MOD_CONTROL = 0x0002; //CTRL
        private const uint MOD_SHIFT = 0x0004; //SHIFT
        private const uint MOD_WIN = 0x0008; //WINDOWS
                                             //CAPS LOCK:
        private const uint VK_CAPITAL = 0x14;
        private const uint VK_F8 = 0x77;
        private const uint VK_F9 = 0x78;

        public MainWindow()
        {
            InitializeComponent();
            label.Content = "录制未开始";
        }

        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);

            _windowHandle = new WindowInteropHelper(this).Handle;
            _source = HwndSource.FromHwnd(_windowHandle);
            _source.AddHook(HwndHook);

            RegisterHotKey(_windowHandle, F8_HOTKEY_ID, MOD_NONE, VK_F8);
            RegisterHotKey(_windowHandle, F9_HOTKEY_ID, MOD_NONE, VK_F9);
        }

        private IntPtr HwndHook(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            const int WM_HOTKEY = 0x0312;
            switch (msg)
            {
                case WM_HOTKEY:
                    switch (wParam.ToInt32())
                    {
                        case F8_HOTKEY_ID:
                            int vkey = (((int)lParam >> 16) & 0xFFFF);
                            if (vkey == VK_F8)
                            {
                                StartRecord();
                            }
                            handled = true;
                            break;
                        case F9_HOTKEY_ID:
                            int key = (((int)lParam >> 16) & 0xFFFF);
                            if (key == VK_F9)
                            {
                                StopRecord();
                            }
                            handled = true;
                            break;
                    }
                    break;
            }
            return IntPtr.Zero;
        }

        protected override void OnClosed(EventArgs e)
        {
            _source.RemoveHook(HwndHook);
            UnregisterHotKey(_windowHandle, F8_HOTKEY_ID);
            if (_isRecording)
            {
                StopRecord();
            }
            base.OnClosed(e);
        }

        private void startRecordButton_Click(object sender, RoutedEventArgs e)
        {
            StartRecord();
        }

        private void stopRecordButton_Click(object sender, RoutedEventArgs e)
        {
            StopRecord();
        }

        private void StartRecord()
        {
            if (_isRecording)
            {
                return;
            }
            _isRecording = true;
            _videoCapturer = new VideoCapture.VideoCapture();
            _videoCapturer.StartRecord();
            label.Content = "录制中。。。";
        }

        private void StopRecord()
        {
            if (!_isRecording)
            {
                return;
            }
            _videoCapturer.StopRecord();
            _isRecording = false;
            label.Content = "录制结束";
        }
    }
}
