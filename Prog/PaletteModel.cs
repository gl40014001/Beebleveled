using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Notadesigner.ConwaysLife.Game
{
    class PaletteModel
    {

        public delegate void OnUpdate(object sender);
        public event OnUpdate Update;
        public int NumColours = 4;
        public int[] pal = { 3, 2, 1, 0 };
        private Constants.Colour[] defaultPalette;
        public Constants.Colour[] currentPalette;


        public PaletteModel()
        {
            DefaultPalette = new Constants.Colour[8];

            DefaultPalette[0] = Constants.Colour.Black;
            DefaultPalette[1] = Constants.Colour.Red;
            DefaultPalette[2] = Constants.Colour.Green;
            DefaultPalette[3] = Constants.Colour.Yellow;
            DefaultPalette[4] = Constants.Colour.Blue;
            DefaultPalette[5] = Constants.Colour.Magenta;
            DefaultPalette[6] = Constants.Colour.Cyan;
            DefaultPalette[7] = Constants.Colour.White;

            CurrentPalette = new Constants.Colour[NumColours];
            switch (NumColours)
            {
                case 4:
                    CurrentPalette[0] = defaultPalette[pal[0]];
                    CurrentPalette[1] = defaultPalette[pal[1]];
                    CurrentPalette[2] = defaultPalette[pal[2]];
                    CurrentPalette[3] = defaultPalette[pal[3]];
                    break;
            }

        }

        public void ChangeColour(int x, int y)
        {
            int index = (x + (y * 4));
            ++pal[index];
            
            if (pal[index] > 7)
              pal[index] = 0;


            CurrentPalette[index] = defaultPalette[pal[index]];
            if (null != this.Update)
            {
                this.Update(this);
            }
        }
    

        public Constants.Colour[] DefaultPalette
        {
            get { return this.defaultPalette; }
            set { this.defaultPalette = value; }
        }

        public Constants.Colour[] CurrentPalette
        {
            get { return this.currentPalette; }
            set { this.currentPalette = value; }
        }

    }
}
