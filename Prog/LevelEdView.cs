using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Collections.Generic;
using System.Windows.Media.Imaging;

namespace Notadesigner.ConwaysLife.Game
{
    public class LevelEdView : FrameworkElement
    {
        public delegate void ClickHandler(object sender, ClickEventArgs e);

        public event ClickHandler Click;



        private DrawingVisual[] visuals;

        private DrawingVisual cells = new DrawingVisual();

        private bool isMouseDown;
        const double OUTLINE_WIDTH = 1;
        const double TILES_PER_ROW = 15;

        private byte[] pixels;
        private List<Tile> Tiles;
        private Pen outline = new Pen(Brushes.LightGray, OUTLINE_WIDTH);

        public Constants.Colour[] defaultPalette;
        private byte SelectedTile;




        public LevelEdView()
            : base()
        {
            int NumColours = 4;
            DefaultPalette = new Constants.Colour[NumColours];

            switch (NumColours)
            {
                case 4:
                    DefaultPalette[0] = Constants.Colour.Yellow;
                    DefaultPalette[1] = Constants.Colour.Red;
                    DefaultPalette[2] = Constants.Colour.Green;
                    DefaultPalette[3] = Constants.Colour.Black;
                    break;
            }



            this.AddVisualChild(this.cells);
            this.AddLogicalChild(this.cells);

            this.visuals = new DrawingVisual[] { this.cells };

        }

        public void Update(List<Tile> TpTiles, Constants.Colour[] Palette, byte selectedTile)
        {
            Tiles = TpTiles;
            pixels = Tiles[0].Pixels;

            DefaultPalette = Palette;

            DisplayScreen(screenNo); 
            SelectedTile = selectedTile;
            // this.drawGrid();
            this.drawTiles();
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
            return new Size(64 * Constants.LVL_TILE_SIZE, 64 * Constants.LVL_TILE_SIZE);
        }



        protected override void OnRender(DrawingContext drawingContext)
        {
            //this.drawGrid();
            this.drawTiles();
        }



        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            base.OnMouseDown(e);

            this.isMouseDown = true;
            Point pt = e.GetPosition(this);
            int x = (int)((pt.X) / (Constants.LVL_TILE_SIZE * Constants.CELLS_X + 1));
            int y = (int)((pt.Y) / (Constants.LVL_TILE_SIZE * Constants.CELLS_Y + 1));

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

        
        public LevelScreens screens = new LevelScreens();
        private List<LevelScreen> screen;
        public int[] LevelMap = new int[256];
        public int screenNo = 0;
        public void DisplayScreen(int ScreenNo)
        {
            screenNo = ScreenNo;
            screen = screens.list;

            LevelMap = screen[ScreenNo].TileRef;
            drawTiles();

        }

        public void PutTileHere(int x, int y)
        {
            int index = (x + (y * ((int)TILES_PER_ROW + 1)));
            LevelMap[index] = (int)SelectedTile;
            drawTiles();

        }
        private void AddTileToLevel()
        {

        }


        private void drawTiles()
        {
            if (null == pixels)
               return;




            double TileWidth = Constants.CELLS_X * Constants.LVL_TILE_SIZE;
            double TileHeight = Constants.CELLS_Y * Constants.LVL_TILE_SIZE;

            using (DrawingContext dc = this.cells.RenderOpen())
            {
                Rect rect = new Rect(0, 0, Constants.LVL_TILE_SIZE, Constants.LVL_TILE_SIZE);

                double yOffset = 0;
                double xOffset = 0;
                int xOutlineCount = 0;
                int yOutlineCount = 0;

                foreach (int TileRef in LevelMap)
                {

                    pixels = Tiles[TileRef].Pixels;
                    int index = 0;

                    Point point = new Point(xOffset, yOffset);
                    point.Offset(xOutlineCount * OUTLINE_WIDTH, yOutlineCount * OUTLINE_WIDTH);

                    for (int y = 0; y < Constants.CELLS_Y; y++)
                    {
                        for (int x = 0; x < Constants.CELLS_X; x++)
                        {
                            point.Offset(Constants.LVL_TILE_SIZE, 0);
                            rect.Location = point;

                            dc.DrawRectangle(
                                Constants.GetWindowsColour(DefaultPalette[pixels[index]]),
                                null,
                                rect);
                            index++;
                        }

                        point.Offset(-(TileWidth), Constants.LVL_TILE_SIZE);
                    }

                    xOffset = xOffset + (TileWidth);
                    xOutlineCount += 1;

                    if (xOffset > (TileWidth) * TILES_PER_ROW)
                    {
                        yOffset = yOffset + (TileHeight);
                        xOffset = 0;
                        xOutlineCount = 0;
                        yOutlineCount += 1;
                    }
                }
                dc.Close();
                
            }




        }
    }
}