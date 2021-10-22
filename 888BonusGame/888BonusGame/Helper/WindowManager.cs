using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace _888BonusGame.Helper
{
	class WindowManager
	{
        public static void CreateGeneralWindow(Window window, object viewModel, string title, string controlPath)
        {
            window.Title = title;
            var controlAssembly = Assembly.Load(Assembly.GetExecutingAssembly().GetName().Name);
            var controlType = controlAssembly.GetType(controlPath);
            var newControl = Activator.CreateInstance(controlType) as UserControl;
            newControl.DataContext = viewModel;
            window.Content = newControl;
            window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            window.SizeToContent = SizeToContent.WidthAndHeight;
            window.ResizeMode = ResizeMode.NoResize;
        }
    }
}
