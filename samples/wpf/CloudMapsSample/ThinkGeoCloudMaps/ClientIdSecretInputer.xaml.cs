using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ThinkGeo.Cloud;

namespace ThinkGeoCloudMapsSample
{
    /// <summary>
    /// Interaction logic for ClientIdSecretInput.xaml
    /// </summary>
    public partial class ClientIdSecretInputer : Window
    {
        private readonly AuthenticationTestClient authenticationTestClient;


        public string ClientId { get; set; }

        public string ClientSecret { get; set; }


        public ClientIdSecretInputer()
        {
            InitializeComponent();
            DataContext = this;
            authenticationTestClient = new AuthenticationTestClient();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            authenticationTestClient.Dispose();
            base.OnClosing(e);
        }

        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            Hyperlink link = sender as Hyperlink;
            Process.Start(new ProcessStartInfo(link.NavigateUri.AbsoluteUri));
        }

        private void BtnOk_Click(object sender, RoutedEventArgs e)
        {
            ClientId = ClientId?.Trim();
            ClientSecret = ClientSecret?.Trim();

            if (!string.IsNullOrWhiteSpace(ClientId) || !string.IsNullOrWhiteSpace(ClientSecret))
            {
                authenticationTestClient.ClientId = ClientId;
                authenticationTestClient.ClientSecret = ClientSecret;
                if (!authenticationTestClient.TryAuthenticate(out var errorMessage))
                {
                    MessageBox.Show(errorMessage, "Error", MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
                    return;
                }

                SaveIdSecretToConfig(ClientId, ClientSecret);
                this.DialogResult = true;
            }
            else
            {
                this.DialogResult = false;
            }
        }

        private void SaveIdSecretToConfig(string id, string secret)
        {
            var config = ConfigurationManager.OpenExeConfiguration(System.Configuration.ConfigurationUserLevel.None);
            config.AppSettings.Settings.Remove("ClientId");
            config.AppSettings.Settings.Remove("ClientSecret");
            config.AppSettings.Settings.Add("ClientId", id);
            config.AppSettings.Settings.Add("ClientSecret", secret);
            config.Save(ConfigurationSaveMode.Modified);
        }

        private class AuthenticationTestClient : BaseClient
        {
            public AuthenticationTestClient() : base()
            {
                BaseUris.Add(new Uri("https://gisserver1.thinkgeo.com"));
            }

            public bool TryAuthenticate(out string errorMessage)
            {
                errorMessage = null;
                try
                {
                    base.GetToken();
                    return true;
                }
                catch (Exception ex)
                {
                    errorMessage = ex.Message;
                    return false;
                }
            }
        }
    }
}
