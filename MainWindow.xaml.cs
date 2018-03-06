using System.Windows;
using System.Windows.Controls;
using Notadesigner.ConwaysLife.Game;

namespace Notadesigner.ConwaysLife
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
       
     
        public TileModel model = new TileModel();
        private TilePaletteModel TPmodel = new TilePaletteModel();
        public MainWindow()
        {
            InitializeComponent();

            //OnUpdate is a delegate which points to a function that is really an event handler.
            //model.Update is the event.
            //Upon event "Update" occurring
            //delegate in BoardModel calls MainWindow.model_update
            this.model.Update += new TileModel.OnUpdate(model_Update);
            this.TPmodel.Update += new TilePaletteModel.OnUpdate(TPmodel_Update);
        }

		void view_Click(object sender, ClickEventArgs e)
		{
			this.model.ToggleCell(e.X, e.Y);
		}

        void model_Update(object sender)
        {
            //This is the event handler called by Boardmodel when it fires the Update event.
            //i.e model_Update handler now calls BoardView.Update method and passes an array of bytes
            //(returned by the BoardModel.Cells method) as a paremeter.
            this.view.Update(this.model.Cells);
            TPmodel.UpdateTilePaletteModelList(model);
        }

        void TPmodel_Update(object sender)
        {
            tpview.Update(TPmodel.TPMList);
        }

        private void toggleStart_Click(object sender, RoutedEventArgs e)
        {
            if (this.model.IsActive)
            {
                this.btnAdd.IsEnabled = true;
                ((Button)sender).Content = "Start";
                this.model.Stop();
            }
            else
            {
                this.btnAdd.IsEnabled = false;
                ((Button)sender).Content = "Stop";
                this.model.Start();
            }
        }

        private void tpview_Click(object sender, ClickEventArgs e)
        {

        }
        private void addTile_Click(object sender, RoutedEventArgs e)
        {
            if (this.model.IsActive)
            {
                return;
            }

            this.model.AddTile();
        }

        private void clear_Click(object sender, RoutedEventArgs e)
        {
            this.btnAdd.IsEnabled = true;
            this.btnStart.Content = "Start";

            this.model.Clear();
        }

        private void nextTile_Click(object sender, RoutedEventArgs e)
        {
            this.btnAdd.IsEnabled = true;
            this.btnStart.Content = "Start";

            this.model.NextTile();
        }
    }
}
