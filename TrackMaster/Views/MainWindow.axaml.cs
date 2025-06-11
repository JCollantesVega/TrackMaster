using Avalonia.Controls;

namespace TrackMaster.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        protected override void OnClosing(WindowClosingEventArgs e)
        {
            base.OnClosing(e);
            e.Cancel = true;
            this.Hide();
        }
    }
}