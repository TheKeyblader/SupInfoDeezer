using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Player;
namespace TestWebPlayer
{
    class Program
    {
        static Player.Player player;
        static void Main(string[] args)
        {
            Connect_Config connectConfig = new Connect_Config()
            {
                AppId = "263102",
                ProductId = "Test",
                ProductBuildId = "00001",
                UserProfilePath = "c:\\dzr\\dzrcache_NDK_SAMPLE"
            };
            Connect connect = new Connect(connectConfig,Connection);
            connect.OnConnection += new Connect.ConnectEventArgs(Connection);
            player = new Player.Player(connect);
            player.Event += Player;
            connect.SetAccesToken("fr49mph7tV4KY3ukISkFHQysRpdCEbzb958dB320pM15OpFsQs");
            connect.SetOfflineMode(false);
            while (true)
            {
                var k = Console.ReadKey().KeyChar;

                if(k == 'q') { return; }
                if(k == 'p') { player.Load("dzmedia:///track/95629060"); }
                if(k == ' ') { if (player.Playing) { player.Pause(); } else { player.Resume(); } }
                if(k == '+') { if(player.Volume +10 > 100) { player.SetVolume(100);} else { player.SetVolume(player.Volume + 10); } }
                if(k == '-') { if (player.Volume - 10 < 0) { player.SetVolume(0); } else { player.SetVolume(player.Volume - 10); } }
            }

        }

        static void Connection(DZConnectionEvent connectionEvent)
        {
            Console.WriteLine(connectionEvent);
        }
        static void Player(DZPlayerEvent @event)
        {
            if(@event == DZPlayerEvent.QUEUELIST_LOADED)
            {
                player.Play(DZPlayerCommand.START_TRACKLIST, (uint)DZPlayerIndex32.NEXT);
            }
            if(@event == DZPlayerEvent.QUEUELIST_TRACK_RIGHTS_AFTER_AUDIOADS)
            {
                player.PlayAds();
            }
            Console.WriteLine(@event);
        }
    }
}
