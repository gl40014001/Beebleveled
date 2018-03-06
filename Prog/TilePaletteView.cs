using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Collections.Generic;

namespace Notadesigner.ConwaysLife.Game
{
    public class TilePaletteView : FrameworkElement
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


        public TilePaletteView()
            : base()
        {
            int NumColours = 4; 
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

            this.AddVisualChild(this.cells);
            this.AddLogicalChild(this.cells);

            this.visuals = new DrawingVisual[] { this.grid, this.cells };
            //this.visuals = new DrawingVisual[] { this.grid };

            this.drawGrid();
            // this.drawCells();

           
          
        }

        public void Update(List<Tile> TpTiles)
        {
            
            TpvTilesList = TpTiles;
            values = TpvTilesList[0].Pixels;


            this.drawGrid();
            this.drawCells();
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
            {
                throw new ArgumentOutOfRangeException("index");
            }

            return this.visuals[index];
        }

        protected override Size MeasureOverride(Size availableSize)
        {
            return new Size(64 * Constants.CELL_SIZE, 64 * Constants.CELL_SIZE);
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            this.drawGrid();
            this.drawCells();
        }

        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            base.OnMouseDown(e);

            this.isMouseDown = true;
            Point pt = e.GetPosition(this);
            int x = (int)((pt.X) / Constants.CELL_SIZE);
            int y = (int)((pt.Y) / Constants.CELL_SIZE);

            if (null != this.Click)
            {
                this.Click(this, new ClickEventArgs(x, y));
            }
        }

        protected override void OnMouseUp(MouseButtonEventArgs e)
        {
            base.OnMouseUp(e);
            this.isMouseDown = false;
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            Point pt = e.GetPosition(this);
            int x = (int)((pt.X) / Constants.CELL_SIZE);
            int y = (int)((pt.Y) / Constants.CELL_SIZE);
            if (!this.isMouseDown || (this.previous.X == x && this.previous.Y == y))
            {
                return;
            }

            this.previous.X = x;
            this.previous.Y = y;

            if (null != this.Click)
            {
                this.Click(this, new ClickEventArgs(x, y));
            }
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
                Rect background = new Rect(0, 0, Constants.CELL_SIZE * Constants.CELLS_X, Constants.CELL_SIZE * Constants.CELLS_Y);
                dc.DrawRectangle(Brushes.Gray, null, background);

                Rect anotherbackground = new Rect(Constants.CELL_SIZE * Constants.CELLS_X, 0, Constants.CELL_SIZE * Constants.CELLS_X, Constants.CELL_SIZE * Constants.CELLS_Y);
                dc.DrawRectangle(Brushes.Red, null, anotherbackground);


                //  Point start = new Point(0, 0);
                //Point end = new Point(0, background.Bottom);
                //for (int i = 0; i < Constants.CELLS_X; i++)
                //{
                //    dc.DrawLine(outline, start, end);
                //    start.Offset(Constants.CELL_SIZE, 0);
                //    end.Offset(Constants.CELL_SIZE, 0);
                //}

                // start = new Point(0, 0);
                // end = new Point(background.Right, 0);
                // for (int i = 0; i < Constants.CELLS_X; i++)
                // {
                //     dc.DrawLine(outline, start, end);
                //     start.Offset(0, Constants.CELL_SIZE);
                //     end.Offset(0, Constants.CELL_SIZE);
                // }
            }
        }

        private void drawCells()
        {
           if (null == values)
           {
                return;
           }

            


            using (DrawingContext dc = this.cells.RenderOpen())
            {
                Rect rect = new Rect(OUTLINE_WIDTH, OUTLINE_WIDTH, Constants.CELL_SIZE - OUTLINE_WIDTH, Constants.CELL_SIZE - OUTLINE_WIDTH);

               
                int yOffset = 0;
                int xOffset = 0;

                
                

                foreach (Tile tilenumber in TpvTilesList)
                {
                    values = tilenumber.Pixels;
    

                    int i = 0;
                    for (int y = yOffset; y < Constants.CELLS_Y+yOffset; y++)
                    {
                        for (int x = xOffset; x < Constants.CELLS_X+xOffset; x++)
                        {
                            rect.Location = new Point((x * Constants.CELL_SIZE) + OUTLINE_WIDTH, (y * Constants.CELL_SIZE) + OUTLINE_WIDTH);

                            if (values[i] == 0)
                            {
                                dc.DrawRectangle(Constants.GetWindowsColour(Constants.Colour.Black), null, rect);
                            }
                            else
                            { dc.DrawRectangle(Constants.GetWindowsColour(DefaultPalette[values[i]]), null, rect); }

                            i++;
                        }
                    }
                    xOffset = xOffset + Constants.CELLS_X;

                    if (xOffset > (Constants.CELLS_X * 3))
                    {
                        yOffset = yOffset + Constants.CELLS_Y;
                        xOffset = 0;
                    }
                }
            }
        }
    }
}