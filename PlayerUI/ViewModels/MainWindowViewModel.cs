using Player;
using Prism.Commands;
using Prism.Mvvm;
using System.Windows.Input;

namespace PlayerUI.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private string _title = "Lol";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        private string _recherche = "lol";
        public string Recherche { get { return _recherche; } set { Textchanged(value); SetProperty(ref _recherche, value); } }

        private bool canPlay;
        private Connect connect;
        private Player.Player player;
        private void Textchanged(string text)
        {
            
        }
        public ICommand command { get; private set; }

        public void Click()
        {
            player.Load("dzmedia:///track/4231436");
        }
        public bool CanClick()
        {
            return canPlay;
        }

        public MainWindowViewModel(Connect connect,Player.Player player)
        {
            command = new DelegateCommand(Click, CanClick);
            this.connect = connect;
            this.player = player;
            connect.OnConnection += new Connect.ConnectEventArgs(Connection);
            player.Event += new Player.Player.EventPlayerArgs(PlayerEvent);
            connect.SetAccesToken("fr49mph7tV4KY3ukISkFHQysRpdCEbzb958dB320pM15OpFsQs");
            connect.SetOfflineMode(false);
        }

        private void Connection(DZConnectionEvent @event)
        {
            if(@event == DZConnectionEvent.USER_LOGIN_OK) { canPlay = true; }
        }

        private void PlayerEvent(DZPlayerEvent @event)
        {
            if (@event == DZPlayerEvent.QUEUELIST_LOADED)
            {
                player.Play(DZPlayerCommand.START_TRACKLIST, (uint)DZPlayerIndex32.NEXT);
            }
            if (@event == DZPlayerEvent.QUEUELIST_TRACK_RIGHTS_AFTER_AUDIOADS)
            {
                player.PlayAds();
            }
        }
    }
}
