using System;
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
            Game.ExecuteCommand("say GameOnOnGcMessageReceive"); 
        }

        private static void GameOnOnFireEvent(FireEventEventArgs args)
        {
            Game.ExecuteCommand("say GameOnOnFireEvent => " + args.GameEvent.Name);
            switch (args.GameEvent.Name)
            {
                case "dota_game_state_change":
                    Game.ExecuteCommand("say Status: " + Game.GameState);
                    break;
            }
        }

        private static void GameOnOnMessage(MessageEventArgs args)
        {
            Game.ExecuteCommand("say GameOnOnMessage");
        }

        private static void GameOnOnIngameUpdate(EventArgs args)
        {
            //Game.ExecuteCommand("say GameOnOnIngameUpdate");
        }

        private static void GameOnOnStart(EventArgs args)
        {
            Game.ExecuteCommand("say GameOnOnStart");
            Game.ExecuteCommand("say Игроки в сессии:");
            Game.ExecuteCommand("say [0] => [" + Players.All[0].PlayerSteamId + " / " + Players.All[0].Name + "]");
        }
    }
}
