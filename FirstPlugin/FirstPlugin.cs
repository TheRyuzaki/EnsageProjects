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
            Game.PrintMessage("[TheRyuzaki] GameOnOnGcMessageReceive"); 
        }

        private static void GameOnOnFireEvent(FireEventEventArgs args)
        {
            Game.PrintMessage("[TheRyuzaki] GameOnOnFireEvent => " + args.GameEvent.Name);
            switch (args.GameEvent.Name)
            {
                case "dota_game_state_change":
                    switch (Game.GameState)
                    {
                        case GameState.GameInProgress:
                            Game.PrintMessage("[TheRyuzaki] GameInProgress");
                            Game.ExecuteCommand("say Игроки в сессии:");
                            for (var i = 0; i < Players.All.Count; i++)
                            {
                                Game.ExecuteCommand("say [" + i + "] => [" + Players.All[i].PlayerSteamId + " / " + Players.All[i].Name + "]");
                            }
                            break;
                    }
                    break;
                case "dota_portrait_unit_stats_changed":
                    break;
            }
        }

        private static void GameOnOnMessage(MessageEventArgs args)
        {
            Game.PrintMessage("[TheRyuzaki] GameOnOnMessage");
        }

        private static void GameOnOnIngameUpdate(EventArgs args)
        {
            //Game.ExecuteCommand("say GameOnOnIngameUpdate");
        }

        private static void GameOnOnStart(EventArgs args)
        {
            Game.PrintMessage("[TheRyuzaki] GameOnOnStart");
            Game.ExecuteCommand("say Игроки в сессии:");
            for (var i = 0; i < Players.All.Count; i++)
            {
                Game.ExecuteCommand("say [" + i + "] => [" + Players.All[i].PlayerSteamId + " / " + Players.All[i].Name + "]");
            }
        }
    }
}
