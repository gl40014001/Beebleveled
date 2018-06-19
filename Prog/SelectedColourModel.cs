using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Notadesigner.ConwaysLife.Game
{
    public class SelectedColourModel
    {
        public delegate void OnUpdate(object sender);
        public event OnUpdate Update;
        public int NumColours = 2;
        public int[] pal = { 0, 1  };
        public Constants.Colour[] currentPalette;
        public SelectedColourModel(Constants.Colour[] defaultpalette)
        {
            


          Current = new Constants.Colour[NumColours];
            switch (NumColours)
            {

                case 2:
                    Current[0] = defaultpalette[3];
                    Current[1] = defaultpalette[0];
 
                    break;
            }

    
        }

        public void PaletteUpdated(int ChangedPaletteIndex, Constants.Colour[] palette)
        {

            if (ChangedPaletteIndex == pal[0])
                Current[0] = palette[pal[0]];

            if (ChangedPaletteIndex == pal[1])
                Current[1] = palette[pal[1]];


        }

        public void PaletteLoad(Constants.Colour[] palette)
        {
           

           // currentPalette = palette;
            Current[0] = palette[pal[0]];
            Current[1] = palette[pal[1]];

            Update(this);
        }

        public void ChangeColour(int x, int y, Constants.Colour[] palette)
        {
            int index = (x + (y * 4));
            ++pal[index];

            if (pal[index] > 3)
                pal[index] = 0;

            Current[index] = palette[pal[index]];

            if (null != Update)
                Update(this);
        }


        

        public Constants.Colour[] Current
        {
            get { return this.currentPalette; }
            set { this.currentPalette = value; }
        }
    }
}
