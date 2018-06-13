using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notadesigner.ConwaysLife.Game

{
    public class LevelScreens
    {
        public int index;
        public List<LevelScreen> list = new List<LevelScreen>();

        public LevelScreens()
        {
            for (index =0;index <=3; index++)
            {
                list.Add(new LevelScreen(index));
            }
        }

    }
}
