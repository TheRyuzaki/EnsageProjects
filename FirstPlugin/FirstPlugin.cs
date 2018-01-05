using System;
using System.Threading;
using Ensage;
using Ensage.Common.Objects;
using Ensage.SDK.Service;

using SharpDX;

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
            Game.PrintMessage("[TheRyuzaki] GameOnOnGcMessageReceive2"); 
        }

        private static void GameOnOnFireEvent(FireEventEventArgs args)
        {
            Game.PrintMessage("[TheRyuzaki] GameOnOnFireEvent2 => " + args.GameEvent.Name);
            switch (args.GameEvent.Name)
            {

            }
        }

        private static void GameOnOnMessage(MessageEventArgs args)
        {
            Game.PrintMessage("[TheRyuzaki] GameOnOnMessage2");
        }

        private static void GameOnOnIngameUpdate(EventArgs args)
        {
            //Game.PrintMessage("[TheRyuzaki] GameOnOnIngameUpdate");
        }

        private static void GameOnOnStart(EventArgs args)
        {
            Game.PrintMessage("[TheRyuzaki] GameOnOnStart2");
            Game.PrintMessage("[TheRyuzaki] Игроки в сессии3:");
            Thread.Sleep(200);
            for (var i = 0; i < Players.All.Count; i++)
            {
                Game.PrintMessage("[TheRyuzaki] [" + i + "] => [" + Players.All[i].PlayerSteamId + " / " + Players.All[i].Name + "]");
                Thread.Sleep(200);
            }
        }
    }
}
