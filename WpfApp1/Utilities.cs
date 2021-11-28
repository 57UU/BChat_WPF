using System.Threading.Tasks;
using System.Windows;

namespace WpfApp1
{
    internal class Utilities
    {
        public static int interval = 1;
        public static double OpInterval = 0.08;

        public static async void WindowStartAnimotion(Window window)
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
            
        }
    }
}
