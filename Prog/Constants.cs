using System;
using System.Windows.Media;

namespace Notadesigner.ConwaysLife.Game
{
    public static  class Constants
    {
        public static int NUM_OF_TILES = 31;
        public static double CELL_SIZE = 20;
        public static double TPCELL_SIZE = 5;
        public static double LVL_TILE_SIZE = 3;
        public static int CELLS_X = 16;

        public static int CELLS_Y = 16;

        public static int LEVEL_X = 16;
        public static int LEVEL_Y = 16;
        public static string SEPARATOR = "SPLIT";

        public static Colour[] defaultPalette =
        {
            Colour.Black,
            Colour.Red,
            Colour.Green,
            Colour.Yellow,
            Colour.Blue,
            Colour.Magenta,
            Colour.Cyan,
            Colour.White
        };
    
        public enum Colour
        {
            Black = 0,
            Red = 1,
            Green = 2,
            Yellow = 3,
            Blue = 4,
            Magenta = 5,
            Cyan = 6,
            White = 7
        }

        public enum Mousebutton
        {
            Left = 0,
            Right = 1
        }
        public static SolidColorBrush GetWindowsColour(Colour beebColour)
        {
            
            switch (beebColour)
            {
                case Colour.Black: return Brushes.Black;
                case Colour.Red: return Brushes.Red;
                case Colour.Green: return Brushes.Green;
                case Colour.Yellow: return Brushes.Yellow;
                case Colour.Blue: return Brushes.Blue;
                case Colour.Magenta: return Brushes.Magenta;
                case Colour.Cyan: return Brushes.Cyan;
                case Colour.White: return Brushes.White;
                default: throw new Exception("Unknown colour");
            }
        }



        public static Colour[] DefaultPalette
        {
            get { return defaultPalette; }
            set { defaultPalette = value; }
        }

    }
}
