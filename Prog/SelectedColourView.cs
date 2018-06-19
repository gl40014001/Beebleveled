using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Collections.Generic;

namespace Notadesigner.ConwaysLife.Game
{
    public class SelectedColourView : FrameworkElement 
    {
        public delegate void ClickHandler(object sender, ClickEventArgs e);

        public event ClickHandler Click;

        private const double OUTLINE_WIDTH = 1;

        private DrawingVisual[] visuals;

        private DrawingVisual grid = new DrawingVisual();


        private bool isMouseDown;
        private Pen outline = new Pen(Brushes.LightGray, OUTLINE_WIDTH);

        public Constants.Colour[] defaultPalette;
        public int NumColours = 2;

        public SelectedColourView()
            : base()
        {

            Current = new Constants.Colour[NumColours];

            switch (NumColours)
            {
                case 2:
                    Current[0] = Constants.defaultPalette[3];
                    Current[1] = Constants.defaultPalette[0];
              
                    break;
            }


            this.AddVisualChild(this.grid);
            this.AddLogicalChild(this.grid);
            this.visuals = new DrawingVisual[] { this.grid };
            this.DrawGrid();

        }

        public void Update(Constants.Colour[] palette)
        {
            Current = palette;

            this.DrawGrid();

        }

        public void Clear()
        {
            this.DrawGrid();
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
            this.DrawGrid();

        }



        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            base.OnMouseDown(e);

            this.isMouseDown = true;
            Point pt = e.GetPosition(this);
            int x = (int)((pt.X) / (Constants.TPCELL_SIZE * Constants.CELLS_X));
            int y = (int)((pt.Y) / (Constants.TPCELL_SIZE * Constants.CELLS_Y));


            if (null != Click)
                Click(this, new ClickEventArgs(x, y));
        }



        protected override void OnMouseUp(MouseButtonEventArgs e)
        {
            base.OnMouseUp(e);
            this.isMouseDown = false;
        }


        protected override void OnMouseMove(MouseEventArgs e)
        {

        }


        public Constants.Colour[] Current
        {
            get { return this.defaultPalette; }
            set { this.defaultPalette = value; }
        }


        private void DrawGrid()
        {
            using (DrawingContext dc = this.grid.RenderOpen())
            {
                Rect selectedColoursBox = new Rect(
                                    0,
                                    0,
                                    Constants.TPCELL_SIZE * Constants.CELLS_X,
                                    Constants.TPCELL_SIZE * Constants.CELLS_Y);

                int yOffset = 0;
                int xOffset = 0;


                for (int colour = 0; colour < NumColours; colour++)
                {
                    selectedColoursBox.Location = new Point(xOffset, yOffset);

                    dc.DrawRectangle(
                        Constants.GetWindowsColour(Current[colour]),
                        null,
                        selectedColoursBox);

                    xOffset = xOffset + ((int)Constants.TPCELL_SIZE * Constants.CELLS_X);

                    if (xOffset > (((int)Constants.TPCELL_SIZE * Constants.CELLS_X)) * 3)
                        yOffset = yOffset + ((int)Constants.TPCELL_SIZE * Constants.CELLS_Y);

                }
            }
        }
    }
}
