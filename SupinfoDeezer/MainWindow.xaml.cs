using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;
using System.IO;

namespace SupinfoDeezer
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private Process player;
        public MainWindow()
        {
            InitializeComponent();
            mainFrame.Navigate(new Uri("TrackContainer.xaml", UriKind.RelativeOrAbsolute));
            player = new Process();
            player.StartInfo.WorkingDirectory = Directory.GetCurrentDirectory();
            player.StartInfo.Arguments = "";
            player.StartInfo.FileName = "TestWebPlayer.exe";

            player.StartInfo.UseShellExecute = false;
            player.StartInfo.RedirectStandardInput = true;
            player.StartInfo.RedirectStandardOutput = true;
            player.StartInfo.RedirectStandardError = true;
            player.StartInfo.CreateNoWindow = true;
            player.ErrorDataReceived += Receive;
            player.OutputDataReceived += Receive;
            player.EnableRaisingEvents = true;
            player.Start();
            player.BeginOutputReadLine();
            player.BeginErrorReadLine();
        }
        public void Receive(object sender, DataReceivedEventArgs e)
        {
        }
        private async void BarreRecherche_TextChanged(object sender, TextChangedEventArgs e)
        {
            var textbox = (TextBox)sender;
            if(textbox.Text != null && textbox.Text != "")
            {
                await ((TrackContainer)mainFrame.Content).GetAlbums(textbox.Text);
            }
        }

        private void BAlbums_Click(object sender, RoutedEventArgs e)
        {
            Dispatcher.BeginInvoke(new Action(()=> {
                player.StandardInput.WriteLineAsync("p 4231436").Wait();
                player.StandardInput.FlushAsync().Wait();
            }));

            //PlayerManager.StardThread();
            //PlayerManager.AccesToken = "fr49mph7tV4KY3ukISkFHQysRpdCEbzb958dB320pM15OpFsQs";
        }
    }
}
