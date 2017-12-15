using Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SupinfoDeezer
{
    public enum TODO
    {
        Load,
        Play,
        Pause,
        Resume,
        Stop,
    }
    public class Command
    {
        public TODO command;
        public string song;
        public DZPlayerCommand dZCommand;
        public uint idx;
    }

    public static class PlayerManager
    {
        private static Connect connect;
        private static Player.Player player;

        private static bool canPlay;
        public static bool CanPlay => canPlay;
        public static bool Playing => player.Playing;
        public static int Volume { get { return player.Volume; } set { player.SetVolume(value); } }

        public static Command commandToDo;

        public static string AccesToken;
        public static Thread thread;

        public static event Player.Player.EventPlayerArgs EventPlayer;
        public static event Connect.ConnectEventArgs EventConnect;
        public static void StardThread()
        {
            thread = new Thread(new ThreadStart(ThreadingLoop));
            thread.Start();
        }
        public static void ThreadingLoop()
        {
            Connect_Config config = new Connect_Config()
            {
                AppId = "263102",
                ProductId = "Test",
                ProductBuildId = "00001",
                UserProfilePath = "c:\\dzr\\dzrcache_NDK_SAMPLE"
            };
            connect = new Connect(config, ConnectionEvent);
            player = new Player.Player(connect);
            player.Event += new Player.Player.EventPlayerArgs(PlayerEvent);
            while (true)
            {
                if (!string.IsNullOrEmpty(AccesToken))
                {
                    connect.SetAccesToken(AccesToken);
                    AccesToken = null;
                    connect.SetOfflineMode(false);
                }
                if (canPlay)
                {
                    if (commandToDo != null)
                    {
                        lock (commandToDo)
                        {


                            switch (commandToDo.command)
                            {
                                case TODO.Pause:
                                    player.Pause();
                                    break;
                                case TODO.Resume:
                                    player.Resume();
                                    break;
                                case TODO.Stop:
                                    player.Stop();
                                    break;
                                case TODO.Load:
                                    player.Load(commandToDo.song);
                                    break;
                                case TODO.Play:
                                    player.Play(commandToDo.dZCommand, commandToDo.idx);
                                    break;
                            }
                            commandToDo = null;
                        }
                    }
                }
            }
        }
        private static void ConnectionEvent(DZConnectionEvent @event)
        {
            if (@event == DZConnectionEvent.USER_LOGIN_OK)
            {
                canPlay = true;
            }
            EventConnect?.Invoke(@event);
        }

        private static void PlayerEvent(DZPlayerEvent @event)
        {
            if (@event == DZPlayerEvent.QUEUELIST_LOADED)
            {
                player.Play(DZPlayerCommand.START_TRACKLIST, (uint)DZPlayerIndex32.CURRENT);
            }
            EventPlayer?.Invoke(@event);
        }
    }
}
