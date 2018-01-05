using System;
using System.Threading;
using Ensage;
using Ensage.Common.Objects;
using Ensage.SDK.Service;

using SharpDX;
using Timer = Ensage.SDK.Helpers.Timer;

namespace FirstPlugin
{
    public class FirstPlugin : Plugin
    {
        static void Main(string[] args)
        {
            Game.OnStart += GameOnOnStart;
            //Game.OnIngameUpdate += GameOnOnIngameUpdate;
            Game.OnMessage += GameOnOnMessage;
            Game.OnFireEvent += GameOnOnFireEvent;
            Game.OnGCMessageReceive += GameOnOnGcMessageReceive;

        }

        private static void GameOnOnGcMessageReceive(GCMessageEventArgs args)
        {
            //Game.PrintMessage("[TheRyuzaki] GameOnOnGcMessageReceive2"); 
        }

        private static void GameOnOnFireEvent(FireEventEventArgs args)
        {
            //Game.PrintMessage("[TheRyuzaki] GameOnOnFireEvent2 => " + args.GameEvent.Name);
            switch (args.GameEvent.Name)
            {

            }
        }

        private static void GameOnOnMessage(MessageEventArgs args)
        {
            ///Game.PrintMessage("[TheRyuzaki] GameOnOnMessage2");
        }

        private static void GameOnOnIngameUpdate(EventArgs args)
        {
            //Game.PrintMessage("[TheRyuzaki] GameOnOnIngameUpdate");
        }

        private static void GameOnOnStart(EventArgs args)
        {
            Game.PrintMessage("[TheRyuzaki] GameOnOnStart2");
            new Timer(100).Elapsed += (sender, eventArgs) => Game.ExecuteCommand("say Игроки в сессии:");
            for (var i = 0; i < Players.All.Count; i++)
            {
                new Timer(100 * (i + 2)).Elapsed += (sender, eventArgs) => Game.ExecuteCommand("say [" + i + "] => [" + Players.All[i].PlayerSteamId + " / " + Players.All[i].Name + "]");
            }
        }
    }
}
