using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Animation;

namespace WPF_Interface
{
    internal class Utilities
    {
        public static int interval = 1;
        public static double OpInterval = 0.08;

        /*        public static async void WindowStartAnimotion(Window window)
                {
                    window.Opacity = 0;
                    window.Show();
                    for(double i = 0.5; i <= 1; i += OpInterval)
                    {
                        window.Opacity = i;
                        await Task.Delay(interval);
                    }
                    //MessageBox.Show(window.Opacity.ToString()); 
                    window.Opacity = 1;
                }*/


        /*        public static async void WindowHideAnimotion(Window window, bool isClose)
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

        public static double startOp = 0.3;
        public static void WindowStartAnimotion(Window window)
        {
            
            window.Opacity = startOp;
            window.Show();
            Storyboard sto = new Storyboard();
            DoubleAnimation da = new DoubleAnimation();
            da.From = startOp;
            da.To = 1.0;
            //可选属性：是否往返播放
            //da.AutoReverse = true;
            //da.RepeatBehavior = RepeatBehavior.Forever;
            da.RepeatBehavior = new RepeatBehavior(1);
            da.Duration = new Duration(TimeSpan.FromMilliseconds(100));
            Storyboard.SetTarget(da, window);
            Storyboard.SetTargetProperty(da, new PropertyPath("Opacity"));
            sto.Children.Add(da);
            sto.Begin();
        }
        public static void WindowHideAnimotion(Window window,bool isClose)
        {
            Storyboard sto = new Storyboard();
            DoubleAnimation da = new DoubleAnimation();
            da.From = 1;
            da.To = 0;
            //可选属性：是否往返播放
            //da.AutoReverse = true;
            //da.RepeatBehavior = RepeatBehavior.Forever;
            da.RepeatBehavior = new RepeatBehavior(1);
            da.Duration = new Duration(TimeSpan.FromMilliseconds(150));
            Storyboard.SetTarget(da, window);
            Storyboard.SetTargetProperty(da, new PropertyPath("Opacity"));
            sto.Children.Add(da);
            
            if (isClose)
            {
                sto.Completed += (o, e) => {
                    window.Close();
                };
            }
            sto.Begin();
        }
        public delegate void LaterTask();

        public static void WindowHideAnimotion(Window window, LaterTask task)
        {
            Storyboard sto = new Storyboard();
            DoubleAnimation da = new DoubleAnimation();
            da.From = 1;
            da.To = 0;
            //可选属性：是否往返播放
            //da.AutoReverse = true;
            //da.RepeatBehavior = RepeatBehavior.Forever;
            da.RepeatBehavior = new RepeatBehavior(1);
            da.Duration = new Duration(TimeSpan.FromMilliseconds(150));
            Storyboard.SetTarget(da, window);
            Storyboard.SetTargetProperty(da, new PropertyPath("Opacity"));
            sto.Children.Add(da);

            sto.Completed += (o, e) => {
                task();
            };
            sto.Begin();
        }
        public static void CrossThread(LaterTask task)
        {
            Assets.assets.initialWindow.Dispatcher.Invoke(task);
        }
    }
}
