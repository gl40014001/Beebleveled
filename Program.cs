using System;
using System.Windows;
using Notadesigner.ConwaysLife.Game;

namespace Notadesigner.ConwaysLife
{
    class Program
    {
        [STAThread]
        static void Main()
        {
            new Application();
            Window wnd = new MainWindow();
            wnd.Show();
            Application.Current.Run();
        }
    }
}
