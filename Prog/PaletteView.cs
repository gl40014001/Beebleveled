using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Collections.Generic;

namespace Notadesigner.ConwaysLife.Game
{
    public class PaletteView : FrameworkElement
    {
        public delegate void ClickHandler(object sender, ClickEventArgs e);

        public event ClickHandler Click;

        private const double OUTLINE_WIDTH = 1;

        private DrawingVisual[] visuals;

        private DrawingVisual grid = new DrawingVisual();

        private DrawingVisual cells = new DrawingVisual();

        private bool isMouseDown;

        private Point previous = new Point();

        private byte[] values;
        private List<Tile> TpvTilesList;
        private Pen outline = new Pen(Brushes.LightGray, OUTLINE_WIDTH);

        public Constants.Colour[] defaultPalette;
        public int NumColours = 4;

        public PaletteView()
            : base()
        {
           
            DefaultPalette = new Constants.Colour[NumColours];

            switch (NumColours)
            {
                case 4:
                    DefaultPalette[0] = Constants.Colour.Yellow;
                    DefaultPalette[1] = Constants.Colour.Green;
                    DefaultPalette[2] = Constants.Colour.Red;
                    DefaultPalette[3] = Constants.Colour.Black;
                    break;
            }


            this.AddVisualChild(this.grid);
            this.AddLogicalChild(this.grid);

          

            this.visuals = new DrawingVisual[] { this.grid };
            //this.visuals = new DrawingVisual[] { this.grid };

            this.drawGrid();
            // this.drawCells();



        }

        public void Update(Constants.Colour[] Palette)
        {
            DefaultPalette = Palette;
         
            this.drawGrid();
          
        }

        public void Clear()
        {
            this.drawGrid();
        }

        protected override int VisualChildrenCount
        {
            get
            {
                return this.visuals.Length;
            }
        }



        protected override Visual GetVisualChild(int index)
        {
            if (index < 0 || index > this.visuals.Length)
                throw new ArgumentOutOfRangeException("index");

            return this.visuals[index];
        }



        protected override Size MeasureOverride(Size availableSize)
        {
            return new Size(64 * Constants.TPCELL_SIZE, 64 * Constants.TPCELL_SIZE);
        }



        protected override void OnRender(DrawingContext drawingContext)
        {
            this.drawGrid();
            
        }



        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            base.OnMouseDown(e);
      
            this.isMouseDown = true;
            Point pt = e.GetPosition(this);
            int x = (int)((pt.X) / (Constants.TPCELL_SIZE * Constants.CELLS_X));
            int y = (int)((pt.Y) / (Constants.TPCELL_SIZE * Constants.CELLS_Y));

     
            if (null != this.Click)
                this.Click(this, new ClickEventArgs(x, y));
        }



        protected override void OnMouseUp(MouseButtonEventArgs e)
        {
            base.OnMouseUp(e);
            this.isMouseDown = false;
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
           
        }


        public Constants.Colour[] DefaultPalette
        {
            get { return this.defaultPalette; }
            set { this.defaultPalette = value; }
        }





        private void drawGrid()
        {
            using (DrawingContext dc = this.grid.RenderOpen())
            {
                Rect palettebox = new Rect(
                                    0,
                                    0,
                                    Constants.TPCELL_SIZE * Constants.CELLS_X,
                                    Constants.TPCELL_SIZE * Constants.CELLS_Y);

                int yOffset = 0;
                int xOffset = 0;

             
                for (int colour = 0; colour < NumColours; colour++)
                {
                    palettebox.Location = new Point(xOffset, yOffset);

                    dc.DrawRectangle(
                        Constants.GetWindowsColour(DefaultPalette[colour]),
                        null,
                        palettebox);

                    xOffset = xOffset +( (int)Constants.TPCELL_SIZE * Constants.CELLS_X);

                    if (xOffset > ( ((int)Constants.TPCELL_SIZE * Constants.CELLS_X)) *3)
                        yOffset = yOffset + ((int)Constants.TPCELL_SIZE * Constants.CELLS_Y);

                }
            }
        }


      
        
    }
}
