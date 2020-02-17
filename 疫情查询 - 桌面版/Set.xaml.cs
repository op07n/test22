using Panuon.UI.Silver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace 疫情查询___桌面版
{
    /// <summary>
    /// Set.xaml 的交互逻辑
    /// </summary>
    public partial class Set : WindowX
    {
        public MainWindow ParentWindow { get; set; }

        [DllImport("user32.dll")]
        public static extern IntPtr GetDC(IntPtr hwnd);
        [DllImport("user32.dll")]
        public static extern Int32 ReleaseDC(IntPtr hwnd, IntPtr hdc);
        [DllImport("gdi32.dll")]
        public static extern uint GetPixel(IntPtr hdc, int nXPos, int nYPos);

        public static SolidColorBrush brush = new SolidColorBrush();
        public static SolidColorBrush brushfont = new SolidColorBrush();
        private delegate void TimerDispatcherDelegate();

        public Set()
        {
            InitializeComponent();

            WindowBlur.SetIsEnabled(this, true);
            WindowXCaption.SetForeground(this, MainWindow.brush);

            //刷新窗口前景色
            updateUI();
            Timer timer = new Timer();
            timer = new Timer(1000);
            timer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            timer.Enabled = true;


            if(WindowXCaption.GetHeight(MainWindow.windowMain) == 0)
            {
                GD.IsChecked = true;
            }
            if (MainWindow.windowMain.fx)
            {
                GT.IsChecked = true;
            }
            if(MainWindow.wherefind != "全国")
            {
                WHERE.Text = MainWindow.wherefind;
            }
            TIME.Text = MainWindow.times.ToString();
            TOP.IsChecked = MainWindow.top;
        }

        private void OnTimedEvent(object sender, ElapsedEventArgs e)
        {
            this.Dispatcher.Invoke(DispatcherPriority.Normal, new TimerDispatcherDelegate(updateUI));
        }

        private void updateUI()
        {
            Point inScreen = new Point(0, 0);
            try
            {
                inScreen = PointToScreen(new System.Windows.Point(0, 0));
            }
            catch { }
            Color color = GetColor((int)inScreen.X - 3, (int)inScreen.Y - 3);
            if (color.R * 0.299 + color.G * 0.578 + color.B * 0.114 >= 192)
            { //浅色
                brush.Color = Color.FromArgb(255, 0, 0, 0);
                brushfont.Color = Color.FromArgb(255, 255, 255, 255);
                WindowXCaption.SetForeground(this, brush);
            }
            else
            {  //深色
                brush.Color = Color.FromArgb(255, 255, 255, 255);
                brushfont.Color = Color.FromArgb(255, 0, 0, 0);
                WindowXCaption.SetForeground(this, brush);
            }
            FontFamily font = new FontFamily("方正兰亭简黑");

            this.T1.Foreground = brush;
            this.T1.FontFamily = font;
            this.T1.FontSize = 13;
            this.T2.Foreground = brush;
            this.T2.FontFamily = font;
            this.T2.FontSize = 13;
            this.T3.Foreground = brush;
            this.T3.FontFamily = font;
            this.T3.FontSize = 13;
            this.T4.Foreground = brush;
            this.T4.FontFamily = font;
            this.T4.FontSize = 13;
            this.T5.Foreground = brush;
            this.T5.FontFamily = font;
            this.T5.FontSize = 13;

            this.B1.Foreground = brush;
            this.B2.Foreground = brush;
            this.B3.Foreground = brush;
            this.B4.Foreground = brush;
            this.B5.Foreground = brush;

            this.WHEREOK.Foreground = brush;
            this.TIMEOK.Foreground = brush;
            ButtonHelper.SetHoverBrush(WHEREOK, brush);
            ButtonHelper.SetHoverBrush(TIMEOK, brush);

            this.WHERE.Foreground = brush;
            this.TIME.Foreground = brush;
        }

        public Color GetColor(int x, int y)
        {
            IntPtr hdc = GetDC(IntPtr.Zero); uint pixel = GetPixel(hdc, x, y);
            ReleaseDC(IntPtr.Zero, hdc);
            Color color = Color.FromArgb(byte.Parse(255.ToString()), (byte)(pixel & 0x000000FF), (byte)((int)(pixel & 0x0000FF00) >> 8), (byte)((int)(pixel & 0x00FF0000) >> 16));
            return color;
        }

        private void GD_Checked(object sender, RoutedEventArgs e)
        {
            if(GD.IsChecked == true)
            {
                WindowXCaption.SetHeight(ParentWindow, 0);
                ParentWindow.Main.Margin = new Thickness(0, 10, 0, 0);
                ParentWindow.Height = ParentWindow.Height - 25;
            }
            else
            {
                WindowXCaption.SetHeight(ParentWindow, 35);
                ParentWindow.Main.Margin = new Thickness(0, 0, 0, 0);
                ParentWindow.Height = ParentWindow.Height + 25;
            }
        }

        private void GT_Click(object sender, RoutedEventArgs e)
        {
            if (GT.IsChecked == true)
            {
                MainWindow.windowMain.fx = true;
                ParentWindow.GET.HorizontalAlignment =     HorizontalAlignment.Right;
                ParentWindow.MGET.HorizontalAlignment =    HorizontalAlignment.Right;
                ParentWindow.MAY.HorizontalAlignment =     HorizontalAlignment.Right;
                ParentWindow.MMAY.HorizontalAlignment =    HorizontalAlignment.Right;
                ParentWindow.MAYDIE.HorizontalAlignment =  HorizontalAlignment.Right;
                ParentWindow.MMAYDIE.HorizontalAlignment = HorizontalAlignment.Right;
                ParentWindow.DIE.HorizontalAlignment =     HorizontalAlignment.Right;
                ParentWindow.MDIE.HorizontalAlignment =    HorizontalAlignment.Right;
                ParentWindow.ALIVE.HorizontalAlignment =   HorizontalAlignment.Right;
                ParentWindow.MALIVE.HorizontalAlignment =  HorizontalAlignment.Right;
                ParentWindow.WP.FlowDirection = FlowDirection.RightToLeft;
            }
            else
            {
                MainWindow.windowMain.fx = false;
                ParentWindow.GET.HorizontalAlignment =     HorizontalAlignment.Left;
                ParentWindow.MGET.HorizontalAlignment =    HorizontalAlignment.Left;
                ParentWindow.MAY.HorizontalAlignment =     HorizontalAlignment.Left;
                ParentWindow.MMAY.HorizontalAlignment =    HorizontalAlignment.Left;
                ParentWindow.MAYDIE.HorizontalAlignment =  HorizontalAlignment.Left;
                ParentWindow.MMAYDIE.HorizontalAlignment = HorizontalAlignment.Left;
                ParentWindow.DIE.HorizontalAlignment =     HorizontalAlignment.Left;
                ParentWindow.MDIE.HorizontalAlignment =    HorizontalAlignment.Left;
                ParentWindow.ALIVE.HorizontalAlignment =   HorizontalAlignment.Left;
                ParentWindow.MALIVE.HorizontalAlignment =  HorizontalAlignment.Left;
                ParentWindow.SET.Margin = new Thickness(10, 0, 93, 0);
                ParentWindow.WP.FlowDirection = FlowDirection.LeftToRight;
            }
        }

        private void WHEREOK_Click(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrWhiteSpace(WHERE.Text))
            {
                MainWindow.wherefind = WHERE.Text;
                ParentWindow.UpdateThings();
                ParentWindow.Title = " " + WHERE.Text;
            }
            else
            {
                MainWindow.wherefind = "全国";
                ParentWindow.UpdateThings();
                ParentWindow.Title = " " + WHERE.Text;
            }
        }

        private void TIMEOK_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!String.IsNullOrWhiteSpace(TIME.Text))
                {
                    MainWindow.times = int.Parse(TIME.Text);
                    this.Dispatcher.BeginInvoke(new Action(() =>
                    {
                        ParentWindow.RunXH();
                    }), DispatcherPriority.SystemIdle, null);
                }
            }
            catch
            {
                this.Dispatcher.BeginInvoke(new Action(() =>
                {
                    ParentWindow.toolTip.ToolTipText += "\n这不是个数字。（点击消除）";
                    ButtonHelper.SetIcon(ParentWindow.ERR, "");
                }), DispatcherPriority.SystemIdle, null);
                
            }
        }

        private void TOP_Click(object sender, RoutedEventArgs e)
        {
            if (TOP.IsChecked == true)
            {
                MainWindow.top = true;
            }
            else
            {
                MainWindow.top = false;
            }
        }
    }
}
