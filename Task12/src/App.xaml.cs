using System.Configuration;
using System.Data;
using System.Windows;
using Task12.Classes;

namespace Task12
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            var mainWindow = new MainWindow
            {
                DataContext = new TaskViewModel()
            };
            mainWindow.Show();
        }
    }

}
