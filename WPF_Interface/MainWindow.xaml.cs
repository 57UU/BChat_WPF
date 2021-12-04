using System;
using System.Windows;
using System.Windows.Input;
using System.Threading.Tasks;
using WPF_Interface;
using BChatKernel;

namespace WPF_Interface
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            Assets.assets.initialWindow = this;
            InitializeComponent();
            this.Opacity = 0;
            this.Activated += initialAnimotion;

            void initialAnimotion(object sender,EventArgs e)
            {
                this.Activated -= initialAnimotion;
                Utilities.WindowStartAnimotion(this);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (verify())
            {
                status.Content = "Password or account can't br null";
                return;
            }
            status.Content = "Logging in";
            loginBtn.IsEnabled = false;
            /*            Assets.assets.kernel = new BChatService(new BChatBuildConfig()
                        {
                            ip = "127.0.0.1",
                            port = 1234,
                            password = password.Password,
                            username = account.Text,
                            bChatInterface = Assets.assets.chatInterface
                        });*/
            Assets.assets.buildConfig.password = password.Password;
            Assets.assets.buildConfig.username = account.Text;
            Assets.assets.kernel.Connect();
        }
        private bool verify()
        {
            if (account.Text == "" ||password.Password=="")
            {
/*                Utilities.WindowStartAnimotion(
                    new MessageWindow("Error", "Password or account can't br null",this));*/
                return true;
            }
            return false;

        }
        #region 标题栏事件

        /// <summary>
        /// 窗口移动事件
        /// </summary>
        private void TitleBar_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }

        int i = 0;
        /// <summary>
        /// 标题栏双击事件
        /// </summary>
        private void TitleBar_MouseDown(object sender, MouseButtonEventArgs e)
        {
/*            i += 1;
            System.Windows.Threading.DispatcherTimer timer = new System.Windows.Threading.DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 0, 0, 300);
            timer.Tick += (s, e1) => { timer.IsEnabled = false; i = 0; };
            timer.IsEnabled = true;

            if (i % 2 == 0)
            {
                timer.IsEnabled = false;
                i = 0;
                this.WindowState = this.WindowState == WindowState.Maximized ?
                              WindowState.Normal : WindowState.Maximized;
            }*/
        }

        /// <summary>
        /// 窗口最小化
        /// </summary>
        private void btn_min_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized; //设置窗口最小化
        }

        /// <summary>
        /// 窗口最大化与还原
        /// </summary>
        private void btn_max_Click(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == WindowState.Maximized)
            {
                this.WindowState = WindowState.Normal; //设置窗口还原
            }
            else
            {
                this.WindowState = WindowState.Maximized; //设置窗口最大化
            }
        }

        /// <summary>
        /// 窗口关闭
        /// </summary>
        private void btn_close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        #endregion 标题栏事件

        private void Label_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Dispose();
        }
        private void Dispose()
        {
            Utilities.WindowHideAnimotion(this, () =>
            {
                //await Task.Delay(1000);
                this.Close();
            });
        }
    }
}
