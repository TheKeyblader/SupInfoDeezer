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
        static void Main(string[] args)
        {
            Connect_Config connectConfig = new Connect_Config()
            {
                AppId = "263102",
                ProductId = "Test",
                ProductBuildId = "00001",
                UserProfilePath = "c:\\dzr\\dzrcache_NDK_SAMPLE"
            };
            Connect connect = new Connect(connectConfig, "fr49mph7tV4KY3ukISkFHQysRpdCEbzb958dB320pM15OpFsQs",false,Connection);
            connect.OnConnection += new Connect.ConnectEventArgs(Connection);
            Player.Player player = new Player.Player(connect);
            player.Event += Player;
            player.Load("dzmedia:///track/433164552");
            player.Play(DZPlayerCommand.START_TRACKLIST, (uint)DZPlayerIndex32.NEXT);
        }

        static void Connection(DZConnectionEvent connectionEvent)
        {
            Console.WriteLine(connectionEvent);
        }
        static void Player(DZPlayerEvent @event)
        {
            Console.WriteLine(@event);
        }
    }
}
