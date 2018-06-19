using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Notadesigner.ConwaysLife.Game
{
    public class UndoRedo
    {
        public Stack<byte[]> Undo = new Stack<byte[]>();
        public Stack<byte[]> Redo = new Stack<byte[]>();


        public byte[] CmdUndo(byte[] pixels)
        {
            byte[] myPixels = pixels;
           
            if (Undo.Count > 0)
            {
                Redo.Push(myPixels);

                myPixels = Undo.Pop();
            }
           
            
            return myPixels;
        }

        public void CmdDo(byte[] pixels)
        {


            byte[] myPixels = new byte[Constants.CELLS_X * Constants.CELLS_Y];  
            pixels.CopyTo(myPixels,0);
            
         

            Undo.Push(myPixels);
            Redo.Clear();
        }

        public byte[] CmdRedo(byte[] pixels)
        {
            byte[] myPixels = new byte[Constants.CELLS_X * Constants.CELLS_Y];

            if (Redo.Count >0)
            {
               
                Undo.Push(pixels);
                myPixels = Redo.Pop();
                return myPixels;

            }
            else
            {
                return pixels;
            }
        }


    }
}
