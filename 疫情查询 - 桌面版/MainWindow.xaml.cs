using Newtonsoft.Json.Linq;
using Panuon.UI.Silver;
using System;
using System.ComponentModel;
using System.IO;
//using System.Net;
using System.Runtime.InteropServices;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Threading;

namespace 疫情查询___桌面版
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : WindowX
    {
        [DllImport("user32.dll")]
        public static extern IntPtr GetDC(IntPtr hwnd);
        [DllImport("user32.dll")]
        public static extern Int32 ReleaseDC(IntPtr hwnd, IntPtr hdc);
        [DllImport("gdi32.dll")]
        public static extern uint GetPixel(IntPtr hdc, int nXPos, int nYPos);

        public static SolidColorBrush brush = new SolidColorBrush();
        public static SolidColorBrush brushfont = new SolidColorBrush();
        public static MainWindow windowMain;
        private delegate void TimerDispatcherDelegate();
        public bool fx = true;
        public static int times = 3600;
        public static string wherefind = "全国";
        public bool endRun = false;
        public static bool top = false;
        Timer runtimer = new Timer();
        private bool fu = true;
        private bool iscountry;

        public class ToolTipInfo : INotifyPropertyChanged
        {
            public event PropertyChangedEventHandler PropertyChanged;
            private string toolTipText;
            public string ToolTipText
            {
                get { return toolTipText; } 
                set
                {
                    toolTipText = value;
                    //通知前台UI更新
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ToolTipText"));
                }
            }
        }
        public ToolTipInfo toolTip = new ToolTipInfo();

        public MainWindow()
        {
            InitializeComponent();

            WindowBlur.SetIsEnabled(this, true);
            windowMain = this;

            //刷新窗口前景色
            updateUI();
            Timer timer = new Timer();
            timer = new Timer(1000);
            timer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            timer.Enabled = true;

            FontFamily font = new FontFamily("方正兰亭简黑");
            this.MGET.FontFamily = font;
            this.MGET.FontSize = 13;
            this.GET.FontFamily = font;
            this.GET.FontSize = 23;
            this.MMAY.FontFamily = font;
            this.MMAY.FontSize = 13;
            this.MAY.FontFamily = font;
            this.MAY.FontSize = 23;
            this.MMAYDIE.FontFamily = font;
            this.MMAYDIE.FontSize = 13;
            this.MAYDIE.FontFamily = font;
            this.MAYDIE.FontSize = 23;
            this.MDIE.FontFamily = font;
            this.MDIE.FontSize = 13;
            this.DIE.FontFamily = font;
            this.DIE.FontSize = 23;
            this.MALIVE.FontFamily = font;
            this.MALIVE.FontSize = 13;
            this.ALIVE.FontFamily = font;
            this.ALIVE.FontSize = 23;

            this.TOOLTIP.DataContext = toolTip;

            //开始运行
            RunXH();
        }

        private void OnTimedEvent(object sender, ElapsedEventArgs e)
        {
            this.Dispatcher.Invoke(DispatcherPriority.Normal, new TimerDispatcherDelegate(updateUI));
        }

        private void updateUI()
        {
            if (top)
            {
                this.Topmost = true;
            }
            else
            {
                this.Topmost = false;
            }
            Point inScreen = new Point(0, 0);
            try
            {
                inScreen = PointToScreen(new System.Windows.Point(0, 0));
            }
            catch { }
            Color color = GetColor((int)inScreen.X - 3, (int)inScreen.Y - 3);
            //MessageBox.Show(color.ToString());
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
            this.MGET.Foreground = brush;
            this.GET.Foreground = brush;
            this.MMAY.Foreground = brush;
            this.MAY.Foreground = brush;
            this.MMAYDIE.Foreground = brush;
            this.MAYDIE.Foreground = brush;
            this.MDIE.Foreground = brush;
            this.DIE.Foreground = brush;
            this.MALIVE.Foreground = brush;
            this.ALIVE.Foreground = brush;

            this.SET.Foreground = brush;
            this.ERR.Foreground = brush;

            this.TTS.Background = brush;
            this.TOOLTIP.Foreground = brushfont;
        }

        public Color GetColor(int x, int y)
        {
            IntPtr hdc = GetDC(IntPtr.Zero); uint pixel = GetPixel(hdc, x, y);
            ReleaseDC(IntPtr.Zero, hdc);
            Color color = Color.FromArgb(byte.Parse(255.ToString()), (byte)(pixel & 0x000000FF), (byte)((int)(pixel & 0x0000FF00) >> 8), (byte)((int)(pixel & 0x00FF0000) >> 16));
            return color;
        }

        private void SET_Click(object sender, RoutedEventArgs e)
        {
            Set about = new Set();
            about.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            about.Owner = this;
            about.ParentWindow = this;
            about.ShowDialog();
        }

        private void Window_StateChanged(object sender, EventArgs e)
        {
            this.WindowState = WindowState.Normal;
        }

        public void RunXH()
        {
            UpdateThings();
            runtimer = new Timer(times * 100);
            runtimer.Elapsed += new ElapsedEventHandler(RunWay);
            runtimer.Enabled = true;
        }

        private void RunWay(object sender, ElapsedEventArgs e)
        {
            this.Dispatcher.Invoke(DispatcherPriority.Normal, new TimerDispatcherDelegate(Runing));
        }

        private void Runing()
        {
            if(endRun)
            {
                endRun = false;
                runtimer.Stop();
                return;
            }
            UpdateThings();
        }

        public void UpdateThings()
        {
            GET.Text = "—";
            DIE.Text = "—";
            ALIVE.Text = "—";
            MAY.Text = "—";
            MAYDIE.Text = "—";
            MGET.Text = "确诊";
            MMAY.Text = "疑似";
            MMAYDIE.Text = "重症";
            MDIE.Text = "死亡";
            MALIVE.Text = "治愈";
            toolTip.ToolTipText = "在 " + DateTime.Now.ToLongTimeString().ToString() + " 数据已刷新。（点击消除）";
            ButtonHelper.SetIcon(this.ERR, "");
            if (fu)
            {
                toolTip.ToolTipText += "\n感谢使用 Stapx Steve 制作的疫情查询！\n做得比较仓促，多多理解。";
                ButtonHelper.SetIcon(this.ERR, "");
                fu = false;
            }
            String ncov;
            Action actionyq = new Action(() => {
                if (File.Exists("Ncov.txt"))
                {
                    File.Delete("Ncov.txt");
                }
                try
                {
                    //尝试下载丁香园界面……
                    string url = "https://ncov.dxy.cn/ncovh5/view/pneumonia?scene=2&clicktime=1579582238&enterid=1579582238&from=timeline&isappinstalled=0";
                    string filepath = "Ncov.txt";
                    System.Net.WebClient mywebclient = new System.Net.WebClient();
                    mywebclient.DownloadFile(url, filepath);
                }
                catch(Exception ex)
                {
                    this.Dispatcher.BeginInvoke(new Action(() =>
                    {
                        ButtonHelper.SetIcon(this.ERR, "");
                        this.TOOLTIP.Text += "\n下载丁香园网页错误。" + ex + "（点击消除）";
                    }), DispatcherPriority.SystemIdle, null);
                    return;
                }
                try
                {
                    ncov = File.ReadAllText("Ncov.txt");
                }
                catch(Exception ex)
                {
                    this.Dispatcher.BeginInvoke(new Action(() =>
                    {
                        ButtonHelper.SetIcon(this.ERR, "");
                        toolTip.ToolTipText += "\n读取网页数据错误。" + ex + "（点击消除）";
                    }), DispatcherPriority.SystemIdle, null);
                    return;
                }
                this.Dispatcher.BeginInvoke(new Action(() =>
                {
                    GET.Text = "无数据";
                    DIE.Text = "无数据";
                    ALIVE.Text = "无数据";
                    MAY.Text = "无数据";
                    MAYDIE.Text = "无数据";
                }), DispatcherPriority.SystemIdle, null);
                if (!String.IsNullOrWhiteSpace(wherefind))
                {
                    if (!wherefind.Equals("全国"))
                    {
                        iscountry = true;
                        this.Dispatcher.BeginInvoke(new Action(() =>
                        {
                            MGET.Text = "确诊";
                            MMAY.Text = "疑似";
                            MMAYDIE.Text = "重症";
                            MDIE.Text = "死亡";
                            MALIVE.Text = "治愈";
                        }), DispatcherPriority.SystemIdle, null);
                        string content = File.ReadAllText("Ncov.txt");
                        string gw = content;
                        int i = gw.IndexOf("getListByCountryTypeService2");
                        if (i == -1)
                        {
                            this.Dispatcher.BeginInvoke(new Action(() =>
                            {
                                ButtonHelper.SetIcon(this.ERR, "");
                                toolTip.ToolTipText += "\n未找到城市数据区域。（点击消除）";
                            }), DispatcherPriority.SystemIdle, null);
                            return;
                        }
                        gw = gw.Substring(i);
                        i = gw.IndexOf("getIndexRecommendList");
                        gw = gw.Substring(0, i);
                        i = gw.IndexOf(wherefind);
                        if (i == -1)
                        {
                            iscountry = false;
                            i = content.IndexOf("getAreaStat");
                            if (i == -1)
                            {
                                this.Dispatcher.BeginInvoke(new Action(() =>
                                {
                                    ButtonHelper.SetIcon(this.ERR, "");
                                    toolTip.ToolTipText += "\n未找到城市数据区域。（点击消除）";
                                }), DispatcherPriority.SystemIdle, null);
                                return;
                            }
                            content = content.Substring(i);
                            i = content.IndexOf(wherefind);
                            if (i == -1)
                            {
                                this.Dispatcher.BeginInvoke(new Action(() =>
                                {
                                    ButtonHelper.SetIcon(this.ERR, "");
                                    toolTip.ToolTipText += "\n没有找到这个城镇。（点击消除）";
                                }), DispatcherPriority.SystemIdle, null);
                                return;
                            }
                        }
                        else
                        {
                            content = gw;
                        }
                        try
                        {
                            if (iscountry)
                            {
                                content = content.Substring(i - "stByCountryTypeService2 = [{\"id\":953,\"createTime\":1580027704000,\"modifyTime\":1581144220000,\"tags\":\"\",\"countryType\":2,\"continents\":\"亚洲\",\"provinceId\":\"6\",\"provinceName\":\"".Length);
                            }
                            else
                            {
                                content = content.Substring(i - 30);
                            }
                            i = content.IndexOf("{");
                            if (i == -1)
                            {
                                this.Dispatcher.BeginInvoke(new Action(() =>
                                {
                                    ButtonHelper.SetIcon(this.ERR, "");
                                    toolTip.ToolTipText += "\n未找到标识区域1。（点击消除）";
                                }), DispatcherPriority.SystemIdle, null);
                                return;
                            }
                            content = content.Substring(i);
                            if (iscountry)
                            {
                                i = content.IndexOf("}");
                                if (i == -1)
                                {
                                    this.Dispatcher.BeginInvoke(new Action(() =>
                                    {
                                        ButtonHelper.SetIcon(this.ERR, "");
                                        toolTip.ToolTipText += "\n未找到标识区域2。（点击消除）";
                                    }), DispatcherPriority.SystemIdle, null);
                                    return;
                                }
                                content = content.Substring(0, i + 1);
                            }
                            else
                            {
                                string type = content;
                                int j = type.IndexOf("\"");
                                if (j == -1)
                                {
                                    this.Dispatcher.BeginInvoke(new Action(() =>
                                    {
                                        ButtonHelper.SetIcon(this.ERR, "");
                                        toolTip.ToolTipText += "\n未找到标识区域3。（点击消除）";
                                    }), DispatcherPriority.SystemIdle, null);
                                    return;
                                }
                                type = type.Substring(j + 1);
                                j = type.IndexOf("\"");
                                if (j == -1)
                                {
                                    this.Dispatcher.BeginInvoke(new Action(() =>
                                    {
                                        ButtonHelper.SetIcon(this.ERR, "");
                                        toolTip.ToolTipText += "\n未找到标识区域4。（点击消除）";
                                    }), DispatcherPriority.SystemIdle, null);
                                    return;
                                }
                                type = type.Substring(0, j);
                                if (type.Equals("cityName"))
                                {
                                    i = content.IndexOf("}");
                                    if (i == -1)
                                    {
                                        this.Dispatcher.BeginInvoke(new Action(() =>
                                        {
                                            ButtonHelper.SetIcon(this.ERR, "");
                                            toolTip.ToolTipText += "\n未找到标识区域5。（点击消除）";
                                        }), DispatcherPriority.SystemIdle, null);
                                        return;
                                    }
                                    content = content.Substring(0, i + 1);
                                }
                                else if (type.Equals("provinceName"))
                                {
                                    i = content.IndexOf("\"cities\"");
                                    if (i == -1)
                                    {
                                        this.Dispatcher.BeginInvoke(new Action(() =>
                                        {
                                            ButtonHelper.SetIcon(this.ERR, "");
                                            toolTip.ToolTipText += "\n未找到标识区域6。（点击消除）";
                                        }), DispatcherPriority.SystemIdle, null);
                                        return;
                                    }
                                    content = content.Substring(0, i - 1);
                                    content = content + "}";
                                }
                                else
                                {
                                    this.Dispatcher.BeginInvoke(new Action(() =>
                                    {
                                        ButtonHelper.SetIcon(this.ERR, "");
                                        toolTip.ToolTipText += "\n未知错误：城市类型 " + type + " 不存在。（点击消除）";
                                    }), DispatcherPriority.SystemIdle, null);
                                    return;
                                }
                            }
                            this.Dispatcher.BeginInvoke(new Action(() =>
                            {
                                GET.Text = 0.ToString();
                                DIE.Text = 0.ToString();
                                ALIVE.Text = 0.ToString();
                                MAY.Text = 0.ToString();
                                MAYDIE.Text = 0.ToString();
                            }), DispatcherPriority.SystemIdle, null);
                            JObject jObject = JObject.Parse(content);
                            this.Dispatcher.BeginInvoke(new Action(() =>
                            {
                                try
                                {
                                    if (iscountry)
                                    {
                                        this.Title = " " + jObject["continents"].ToString() + " - " + wherefind;
                                    }
                                    GET.Text = jObject["confirmedCount"].ToString();
                                    DIE.Text = jObject["deadCount"].ToString();
                                    ALIVE.Text = jObject["curedCount"].ToString();
                                    MAY.Text = jObject["suspectedCount"].ToString();
                                    MAYDIE.Text = "无数据";
                                }
                                catch(Exception ex)
                                {
                                    ButtonHelper.SetIcon(this.ERR, "");
                                    toolTip.ToolTipText += "\n处理丁香园界面错误1。" + ex + "（点击消除）";
                                    return;
                                }
                            }), DispatcherPriority.SystemIdle, null);
                        }
                        catch(Exception ex)
                        {
                            this.Dispatcher.BeginInvoke(new Action(() =>
                            {
                                ButtonHelper.SetIcon(this.ERR, "");
                                toolTip.ToolTipText += "\n处理丁香园界面错误2。" + ex + "（点击消除）";
                            }), DispatcherPriority.SystemIdle, null);
                            return;
                        }
                    }
                    else
                    {
                        try
                        {
                            string content = File.ReadAllText("Ncov.txt");
                            int i = content.IndexOf("getStatisticsService");
                            content = content.Substring(i + "getStatisticsService".Length);
                            i = content.IndexOf("summary");
                            content = content.Substring(i - 1);
                            content = "{" + content;
                            i = content.IndexOf("virus");
                            content = content.Substring(0, i - 2);
                            content = content + "}";
                            JObject jObject = JObject.Parse(content);
                            this.Dispatcher.BeginInvoke(new Action(() =>
                            {
                                try
                                {
                                    GET.Text = jObject["currentConfirmedCount"].ToString();
                                    DIE.Text = jObject["deadCount"].ToString();
                                    ALIVE.Text = jObject["curedCount"].ToString();
                                    MAY.Text = jObject["suspectedCount"].ToString();
                                    MAYDIE.Text = jObject["seriousCount"].ToString();
                                }
                                catch(Exception ex)
                                {
                                    ButtonHelper.SetIcon(this.ERR, "");
                                    toolTip.ToolTipText += "\n处理丁香园界面错误3。" + ex + "（点击消除）";
                                }
                            }), DispatcherPriority.SystemIdle, null);
                        }
                        catch (Exception ex)
                        {
                            this.Dispatcher.BeginInvoke(new Action(() =>
                            {
                                ButtonHelper.SetIcon(this.ERR, "");
                                toolTip.ToolTipText += "\n处理丁香园界面错误4。" + ex + "（点击消除）";
                            }), DispatcherPriority.SystemIdle, null);
                            
                            return;
                        }
                    }
                }
            });
            actionyq.BeginInvoke(null, null);
        }

        private void ERR_Click(object sender, RoutedEventArgs e)
        {
            ButtonHelper.SetIcon(this.ERR, null);
            toolTip.ToolTipText = "";
        }
    }
}
