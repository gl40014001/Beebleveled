using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Notadesigner.ConwaysLife.Game;

namespace Notadesigner.ConwaysLife
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class LevelWindow : Window
    {


       // int[] Level bob = = new int[256];
        public LevelWindow()
        {

            InitializeComponent();               
        }

    


        private void LevelEdView_Click(object sender, ClickEventArgs e)
        {

            levelEdView.PutTileHere(e.X, e.Y);
      

        }

        private void Scr1_Click(object sender, RoutedEventArgs e)
        {
            levelEdView.DisplayScreen(0);
            btnScr1.Background = Brushes.Aquamarine;
            btnScr2.Background = Brushes.LightGray;
            btnScr3.Background = Brushes.LightGray;
            btnScr4.Background = Brushes.LightGray;


        }


        private void Scr2_Click(object sender, RoutedEventArgs e)
        {
            levelEdView.DisplayScreen(1);
            btnScr1.Background = Brushes.LightGray;
            btnScr2.Background = Brushes.Aquamarine;
            btnScr3.Background = Brushes.LightGray;
            btnScr4.Background = Brushes.LightGray;
        }

        private void Scr3_Click(object sender, RoutedEventArgs e)
        {
            levelEdView.DisplayScreen(2);
            btnScr1.Background = Brushes.LightGray;
            btnScr2.Background = Brushes.LightGray;
            btnScr3.Background = Brushes.Aquamarine;
            btnScr4.Background = Brushes.LightGray;
        }

        private void Scr4_Click(object sender, RoutedEventArgs e)
        {
            levelEdView.DisplayScreen(3);
            btnScr1.Background = Brushes.LightGray;
            btnScr2.Background = Brushes.LightGray;
            btnScr3.Background = Brushes.LightGray;
            btnScr4.Background = Brushes.Aquamarine;

        }


    }
}
