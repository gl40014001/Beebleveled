﻿using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Collections.Generic;

namespace Notadesigner.ConwaysLife.Game
{
    public class TilePanelView : FrameworkElement
    {
        public delegate void ClickHandler(object sender, ClickEventArgs e);

        public event ClickHandler Click;

        private const double OUTLINE_WIDTH = 0;

        private DrawingVisual[] visuals;

        private DrawingVisual cells = new DrawingVisual();

        private bool isMouseDown;


        private byte[] values;
        private List<Tile> TpvTilesList;
        private Pen outline = new Pen(Brushes.LightGray, OUTLINE_WIDTH);

        public Constants.Colour[] defaultPalette;


        public TilePanelView()
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

      

            this.AddVisualChild(this.cells);
            this.AddLogicalChild(this.cells);

            this.visuals = new DrawingVisual[] {this.cells };
                  
        }

        public void Update(List<Tile> TpTiles, Constants.Colour[] Palette)
        {
            
            TpvTilesList = TpTiles;
            values = TpvTilesList[0].Pixels;

            DefaultPalette = Palette;

          // this.drawGrid();
            this.DrawCells();
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
            //this.drawGrid();
            this.DrawCells();
        }



        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            base.OnMouseDown(e);

            this.isMouseDown = true;
            Point pt = e.GetPosition(this);
            int x = (int)((pt.X) / (Constants.TPCELL_SIZE * Constants.CELLS_X+1));
            int y = (int)((pt.Y) / (Constants.TPCELL_SIZE * Constants.CELLS_Y+1));

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
      

      

        private void DrawCells()
        {

            if (null == values)
               return;
     
    
            using (DrawingContext dc = this.cells.RenderOpen())
            {
                Rect rect = new Rect(
                    OUTLINE_WIDTH, 
                    OUTLINE_WIDTH, 
                    Constants.TPCELL_SIZE - OUTLINE_WIDTH, 
                    Constants.TPCELL_SIZE - OUTLINE_WIDTH);
    
                int yOffset = 0;
                int xOffset = 0;
                   
                foreach (Tile tile in TpvTilesList)
                {
                    values = tile.Pixels; 
                    int i = 0;

                    for (int y = yOffset; y < Constants.CELLS_Y+yOffset; y++)
                    {
                        for (int x = xOffset; x < Constants.CELLS_X+xOffset; x++)
                        {
                            rect.Location = new Point(
                                (x * Constants.TPCELL_SIZE) + OUTLINE_WIDTH, 
                                (y * Constants.TPCELL_SIZE) + OUTLINE_WIDTH);

                            dc.DrawRectangle(
                                Constants.GetWindowsColour(DefaultPalette[values[i]]), 
                                null, 
                                rect);
                            i++;
                        }
                    }

                    xOffset = xOffset + Constants.CELLS_X+1;

                    if (xOffset > ((Constants.CELLS_X * 3)+3))
                    {
                        yOffset = yOffset + Constants.CELLS_Y+1;
                        xOffset = 0;
                    }
                  
                }
                dc.Close();
            }
        }


    }
}