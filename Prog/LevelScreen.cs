using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notadesigner.ConwaysLife.Game
{
    public class LevelScreen
    {
        public int screenRef;
        public int[] tileRef = new int[256];

        public LevelScreen(int ScreenRef)
        {
            this.screenRef = ScreenRef;
            
        }

        public LevelScreen()
        { }
        public int ScreenRef
        {
            get
            {
                return screenRef;
            }
            set { screenRef = value; }
                    
        }

        public int[] TileRef
        {
            get { return tileRef; }
            set
            {

                tileRef = value;
            }
        }

    }
}
