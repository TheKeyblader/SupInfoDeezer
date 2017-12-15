using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Threading;

namespace SupinfoDeezer
{
    /// <summary>
    /// Logique d'interaction pour App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {

            App.Current.Exit += new ExitEventHandler(ExitEvent);
        }

        private void ExitEvent(object sender, ExitEventArgs exit)
        {
            if (PlayerManager.thread != null)
            {
                PlayerManager.thread.Abort();
            }
        }
    }
}
