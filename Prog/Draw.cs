using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Notadesigner.ConwaysLife.Game
{
    public class Draw
    {
  //      private const double OUTLINE_WIDTH = 1;

        public void Tile(DrawingContext dc, 
                          byte[] pixels, 
                          Constants.Colour[] Current, 
                          double OUTLINE_WIDTH)
            //x
            //y
            //cell size
            //
        {
            int x = 0;
            int y = 0;
            Rect rect = new Rect(OUTLINE_WIDTH, OUTLINE_WIDTH, Constants.CELL_SIZE - OUTLINE_WIDTH, Constants.CELL_SIZE - OUTLINE_WIDTH);
            for (int i = 0; i < pixels.Length; i++)
            {
                x = (i % Constants.CELLS_X);
                y = (i / Constants.CELLS_X);
                rect.Location = new Point((x * Constants.CELL_SIZE) , (y * Constants.CELL_SIZE) );

                dc.DrawRectangle(Constants.GetWindowsColour(Current[pixels[i]]), null, rect);

            }
        }

        public void TileGrid(DrawingContext dc,
                             Pen gridoutline)
        {


            Rect background = new Rect(0, 0, Constants.CELL_SIZE * Constants.CELLS_X, Constants.CELL_SIZE * Constants.CELLS_Y);
            // dc.DrawRectangle(Brushes.Red, null, background);
          
            Point start = new Point(0, 0);
            Point end = new Point(0, background.Bottom);
            for (int i = 0; i <= Constants.CELLS_X; i++)
            {
                dc.DrawLine(gridoutline, start, end);
                start.Offset(Constants.CELL_SIZE, 0);
                end.Offset(Constants.CELL_SIZE, 0);
            }

            start = new Point(0, 0);
            end = new Point(background.Right, 0);
            for (int i = 0; i <= Constants.CELLS_X; i++)
            {
                dc.DrawLine(gridoutline, start, end);
                start.Offset(0, Constants.CELL_SIZE);
                end.Offset(0, Constants.CELL_SIZE);
            }
        }
    }


}
