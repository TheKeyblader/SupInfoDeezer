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

namespace SupinfoDeezer
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static string Recherche;
        public PageAlbum PageAlbum { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            mainFrame.Navigate(new Uri("PageAlbum.xaml", UriKind.RelativeOrAbsolute));
            PageAlbum = (PageAlbum) mainFrame;
        }

        private void BarreRecherche_TextChanged(object sender, TextChangedEventArgs e)
        {
            var textbox = (TextBox)sender;
            PageAlbum.GetAlbums(textbox.Text);
        }
    }
}
