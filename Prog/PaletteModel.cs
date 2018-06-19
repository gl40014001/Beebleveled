using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Notadesigner.ConwaysLife.Game
{
    public class PaletteModel
    {

        public delegate void OnUpdate(object sender);
        public event OnUpdate Update;
        public int NumColours = 4;
        public int[] pal = { 0, 1, 2, 3 };
        public Constants.Colour[] currentPalette;
        public int ChangedPaletteIndex;


        public PaletteModel(Constants.Colour[] defaultpalette)
        {
            

            CurrentPalette = new Constants.Colour[NumColours];
            switch (NumColours)
            {
                case 4:
                    CurrentPalette[0] = defaultpalette[pal[3]];
                    CurrentPalette[1] = defaultpalette[pal[1]];
                    CurrentPalette[2] = defaultpalette[pal[2]];
                    CurrentPalette[3] = defaultpalette[pal[0]];
                    break;
            }

        }

        public void ChangeColour(int x, int y)
        {
            int index = (x + (y * 4));
            ++pal[index];
            
            if (pal[index] > 7)
              pal[index] = 0;

            CurrentPalette[index] = Constants.defaultPalette[pal[index]];

            ChangedPaletteIndex = index;
            if (null != Update)
                Update(this);
        }
    

     

        public Constants.Colour[] CurrentPalette
        {
            get { return this.currentPalette; }
            set { this.currentPalette = value; }
        }

    }
}
