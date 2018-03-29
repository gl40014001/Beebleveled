using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Notadesigner.ConwaysLife.Game
{
    public class TileView : FrameworkElement
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

		private Pen outline = new Pen(Brushes.LightGray, OUTLINE_WIDTH);

        public Constants.Colour[] defaultPalette;
        public int NumColours = 4;
        Constants.Mousebutton mbpressed;
        public TileView()
            : base()
        {

            Current = new Constants.Colour[NumColours];
            switch (NumColours)
            {
                case 4:
                    Current[0] = Constants.defaultPalette[0];
                    Current[1] = Constants.defaultPalette[1];
                    Current[2] = Constants.defaultPalette[2];
                    Current[3] = Constants.defaultPalette[3];
                    break;
            }

             values = new byte[Constants.CELLS_X * Constants.CELLS_Y];
            this.AddVisualChild(this.grid);
			this.AddLogicalChild(this.grid);

			this.AddVisualChild(this.cells);
			this.AddLogicalChild(this.cells);

			this.visuals = new DrawingVisual[] { this.grid, this.cells };

			this.drawGrid();
			this.drawCells();
        }

        public void Update(byte[] values)
        {
			this.values = values;
			this.drawGrid();
			this.drawCells();
        }

        public void Update(byte[] values, Constants.Colour[] palette)
        {

            defaultPalette = palette;
            this.values = values;
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
			return new Size(Constants.CELLS_X * Constants.CELL_SIZE, Constants.CELLS_Y * Constants.CELL_SIZE);
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
            MouseButtonState mb = e.RightButton;
            MouseButtonState mbl = e.LeftButton;
            

            if (mb == MouseButtonState.Pressed)
            {
                mbpressed = Constants.Mousebutton.Right;
            }

            if (mbl == MouseButtonState.Pressed)
            {
                mbpressed = Constants.Mousebutton.Left;
            }


            int x = (int)((pt.X) / Constants.CELL_SIZE);
			int y = (int)((pt.Y) / Constants.CELL_SIZE);

			if (null != this.Click)
			{
				this.Click(this, new ClickEventArgs(x, y, mbpressed));
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
				this.Click(this, new ClickEventArgs(x, y, mbpressed));
			}
		}

		private void drawGrid()
		{
			using (DrawingContext dc = this.grid.RenderOpen())
			{
				Rect background = new Rect(0, 0, Constants.CELL_SIZE * Constants.CELLS_X, Constants.CELL_SIZE * Constants.CELLS_Y);
				dc.DrawRectangle(Brushes.Gray, null, background);

				Point start = new Point(0, 0);
				Point end = new Point(0, background.Bottom);
				for (int i = 0; i < Constants.CELLS_X; i++)
				{
					dc.DrawLine(outline, start, end);
					start.Offset(Constants.CELL_SIZE, 0);
					end.Offset(Constants.CELL_SIZE, 0);
				}

				start = new Point(0, 0);
				end = new Point(background.Right, 0);
				for (int i = 0; i < Constants.CELLS_X; i++)
				{
					dc.DrawLine(outline, start, end);
					start.Offset(0, Constants.CELL_SIZE);
					end.Offset(0, Constants.CELL_SIZE);
				}
			}
		}

		private void drawCells()
		{
			if (null == this.values)
			{
				return;
			}

           


            using (DrawingContext dc = this.cells.RenderOpen())
			{
				int x = 0;
				int y = 0;
				Rect rect = new Rect(OUTLINE_WIDTH, OUTLINE_WIDTH, Constants.CELL_SIZE - OUTLINE_WIDTH, Constants.CELL_SIZE - OUTLINE_WIDTH);
				
				for (int i = 0; i < values.Length; i++)
				{
					x = (i % Constants.CELLS_X);
					y = (i / Constants.CELLS_X);
					rect.Location = new Point((x * Constants.CELL_SIZE) + OUTLINE_WIDTH, (y * Constants.CELL_SIZE) + OUTLINE_WIDTH);
					
						dc.DrawRectangle(Constants.GetWindowsColour(Current[values[i]]), null, rect);
					
				}
			}
		}

        public Constants.Colour[] Current
        {
            get { return this.defaultPalette; }
            set { this.defaultPalette = value; }
        }
    }
}
