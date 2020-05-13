using System.Diagnostics;
using System.Windows;


namespace PlaceSearchWorldReverseGeocoding
{

    public partial class ModalWindow : Window
    {

        public ModalWindow()
        {
            InitializeComponent();
        }

        private void NavigateToGisServer(object sender, RoutedEventArgs e)
        {
            Process.Start("https://cloud.thinkgeo.com/");
        }


        private void SetApiKey_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = this.Owner as MainWindow;
            if (mainWindow == null)
            {
                MessageBox.Show("This dialog's owner must be MainWindow");
                return;
            }
            if (string.IsNullOrWhiteSpace(txtApiKey.Text))
            {
                MessageBox.Show("Api Key can't be empty");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtApiSecret.Text))
            {
                MessageBox.Show("Api Sceret can't be empty");
                return;
            }

            mainWindow.SetApiKeyAndSceret(txtApiKey.Text, txtApiSecret.Text);
            this.Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


    }
}