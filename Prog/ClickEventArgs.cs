using System;

namespace Notadesigner.ConwaysLife.Game
{
	public class ClickEventArgs : EventArgs
	{
		public ClickEventArgs(int x, int y, Constants.Mousebutton button)
			: base()
		{
           
            this.X = x;
			this.Y = y;
            this.BUTTON = button;
		}

        public ClickEventArgs(int x, int y)
            : base()
        {

            this.X = x;
            this.Y = y;
        }
        public int X
		{
			get;
			private set;
		}

		public int Y
		{
			get;
			private set;
		}

        public Constants.Mousebutton BUTTON
        {
            get;
            private set;
        }
	}
}
