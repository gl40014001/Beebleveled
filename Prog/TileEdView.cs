using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Notadesigner.ConwaysLife.Game
{
    public class TileEdView : FrameworkElement
    {
		public delegate void ClickHandler(object sender, ClickEventArgs e);

		public event ClickHandler Click;

		private const double OUTLINE_WIDTH = 1;

		private DrawingVisual[] visuals;

		private DrawingVisual dvGrid = new DrawingVisual();

		private DrawingVisual dvPixels = new DrawingVisual();

		private bool isMouseDown;

		private Point previous = new Point();

		private byte[] pixels;

		private Pen gridoutline = new Pen(Brushes.DarkGray, OUTLINE_WIDTH + 1);

        public Constants.Colour[] palette;
        public int NumColours = 4;
        Constants.Mousebutton mbpressed;

        public Draw draw = new Draw();

        public TileEdView()
            : base()
        {

            CurrentPalette = new Constants.Colour[NumColours];
            switch (NumColours)
            {
                case 4:
                    CurrentPalette[0] = Constants.defaultPalette[3];
                    CurrentPalette[1] = Constants.defaultPalette[1];
                    CurrentPalette[2] = Constants.defaultPalette[2];
                    CurrentPalette[3] = Constants.defaultPalette[0];
                    break;
            }

            pixels = new byte[Constants.CELLS_X * Constants.CELLS_Y];

            this.AddVisualChild(this.dvGrid);
			this.AddLogicalChild(this.dvGrid);

			this.AddVisualChild(this.dvPixels);
			this.AddLogicalChild(this.dvPixels);

			this.visuals = new DrawingVisual[] { this.dvGrid, this.dvPixels };

			this.DrawGrid();
			this.DrawTile();

        }


        public void Update(byte[] pixels)
        {
			this.pixels = pixels;
			this.DrawGrid();
			this.DrawTile();
        }


        public void Update(byte[] pixels, Constants.Colour[] palette)
        {

            this.palette = palette;
            this.pixels = pixels;
            DrawGrid();
            DrawTile();
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
			return new Size(Constants.CELLS_X * Constants.CELL_SIZE, 
                Constants.CELLS_Y * Constants.CELL_SIZE);
		}


		protected override void OnRender(DrawingContext drawingContext)
		{
			this.DrawGrid();
			this.DrawTile();
		}


		protected override void OnMouseDown(MouseButtonEventArgs e)
		{
			base.OnMouseDown(e);
            
			this.isMouseDown = true;
           
			Point pt = e.GetPosition(this);
            MouseButtonState mb = e.RightButton;
            MouseButtonState mbl = e.LeftButton;
            

            if (mb == MouseButtonState.Pressed)
                mbpressed = Constants.Mousebutton.Right;
            

            if (mbl == MouseButtonState.Pressed)
                mbpressed = Constants.Mousebutton.Left;
            

            int x = (int)((pt.X) / Constants.CELL_SIZE);
			int y = (int)((pt.Y) / Constants.CELL_SIZE);

			if (null != this.Click)
				Click(this, new ClickEventArgs(x, y, mbpressed));
			
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
				this.Click(this, new ClickEventArgs(x, y, mbpressed));
			}
		}

		private void DrawGrid()
		{
			using (DrawingContext dc = this.dvGrid.RenderOpen())
			{
                draw.TileGrid(dc, gridoutline);
                dc.Close();
            }
		}

		private void DrawTile()
		{
			if (null == this.pixels)
				return;
			

            using (DrawingContext dc = this.dvPixels.RenderOpen())
			{
                draw.Tile(dc, pixels, CurrentPalette, OUTLINE_WIDTH);
                dc.Close();
            }
            
		}

        public Constants.Colour[] CurrentPalette
        {
            get { return this.palette; }
            set { this.palette = value; }
        }
    }
}
