using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Animation;

namespace WPF_Interface
{
    internal class Utilities
    {
        private Utilities()
        {
            throw new NotImplementedException();
        }
        static Utilities()
        {
            da1.From = startOp;
            da1.To = 1.0;
            //可选属性：是否往返播放
            //da.AutoReverse = true;
            //da.RepeatBehavior = RepeatBehavior.Forever;
            da1.RepeatBehavior = new RepeatBehavior(1);
            da1.Duration = new Duration(TimeSpan.FromMilliseconds(150));
            Storyboard.SetTargetProperty(da1, new PropertyPath("Opacity"));
            sto1.Children.Add(da1);


            da2.From = 1;
            da2.To = 0;
            //可选属性：是否往返播放
            //da.AutoReverse = true;
            //da.RepeatBehavior = RepeatBehavior.Forever;
            da2.RepeatBehavior = new RepeatBehavior(1);
            da2.Duration = new Duration(TimeSpan.FromMilliseconds(150));
            Storyboard.SetTargetProperty(da2, new PropertyPath("Opacity"));
        }
        public static double startOp = 0.3;
        private static Storyboard sto1 = new Storyboard();
        private static DoubleAnimation da1 = new DoubleAnimation();
        public static void WindowStartAnimotion(Window window,int milliseconds) {

            if (milliseconds != 100)
            {
                da1.Duration=new Duration(TimeSpan.FromMilliseconds(milliseconds));
            }

            window.Opacity = startOp;
            window.Show();
            Storyboard.SetTarget(da1, window);
            sto1.Begin();
        }
        public static void WindowStartAnimotion(Window window)
        {    
            WindowStartAnimotion(window,100);
        }
        private  static LaterTask Blank_Task = new(() => { });
        public static void WindowHideAnimotion(Window window)
        {
            WindowHideAnimotion(window, false);
        }
        public static void WindowHideAnimotion(Window window,bool isClose)
        {
            if (isClose)
            {
                WindowHideAnimotion(window, () =>
                {
                    window.Close();
                });
            }
            else
                WindowHideAnimotion(window, Blank_Task);
        }
        public delegate void LaterTask();
        private static Storyboard sto2 = new Storyboard();
        private static DoubleAnimation da2 = new();


        public static void WindowHideAnimotion(Window window, LaterTask task)
        {
            
            

            Storyboard.SetTarget(da2, window);
          
            sto2.Children.Add(da2);

            sto2.Completed += (o, e) => {
                task();
            };
            sto2.Begin();
        }
        public static void CrossThread(LaterTask task)
        {
            Assets.assets.initialWindow.Dispatcher.Invoke(task);
        }
        /*        public static int interval = 1;
        public static double OpInterval = 0.08;

        public static async void WindowStartAnimotion(Window window)
        {
            window.Opacity = 0;
            window.Show();
            for (double i = 0.5; i <= 1; i += OpInterval)
            {
                window.Opacity = i;
                await Task.Delay(interval);
            }
            //MessageBox.Show(window.Opacity.ToString()); 
            window.Opacity = 1;
        }


        public static async void WindowHideAnimotion(Window window, bool isClose)
        {
            window.Opacity = 0.5;
            for (double i = 1; i > 0; i -= OpInterval)
            {
                window.Opacity = i;
                await Task.Delay(interval);
            }
            window.Hide();
            window.Opacity = 0;
            if (isClose)
            {
                window.Close();
            }

        }*/
    }
}
