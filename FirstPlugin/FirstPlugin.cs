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
            Game.PrintMessage("[TheRyuzaki] GameOnOnGcMessageReceive2"); 
        }

        private static void GameOnOnFireEvent(FireEventEventArgs args)
        {
            Game.PrintMessage("[TheRyuzaki] GameOnOnFireEvent2 => " + args.GameEvent.Name);
            switch (args.GameEvent.Name)
            {
                case "dota_game_state_change":
                    switch (Game.GameState)
                    {
                        case GameState.GameInProgress:
                            Game.PrintMessage("[TheRyuzaki] GameInProgress2");
                            Game.ExecuteCommand("say Игроки в сессии2: " + Players.All.Count);
                            Game.ExecuteCommand("say Игроки в сессии2: " + Players.Dire.Count);
                            Game.ExecuteCommand("say Игроки в сессии2: " + Players.Radiant.Count);
                            
                            Game.ExecuteCommand("say Героев2: " + Heroes.All.Count);
                            Game.ExecuteCommand("say Героев2: " + Heroes.Dire.Count);
                            Game.ExecuteCommand("say Героев2: " + Heroes.Radiant.Count);
                            break;
                    }
                    break;
                case "dota_portrait_unit_stats_changed":
                    break;
            }
        }

        private static void GameOnOnMessage(MessageEventArgs args)
        {
            Game.PrintMessage("[TheRyuzaki] GameOnOnMessage2");
        }

        private static void GameOnOnIngameUpdate(EventArgs args)
        {
            //Game.ExecuteCommand("say GameOnOnIngameUpdate");
        }

        private static void GameOnOnStart(EventArgs args)
        {
            Game.PrintMessage("[TheRyuzaki] GameOnOnStart2");
            Game.ExecuteCommand("say Игроки в сессии3: " + Players.All.Count);
            Game.ExecuteCommand("say Игроки в сессии3: " + Players.Dire.Count);
            Game.ExecuteCommand("say Игроки в сессии3: " + Players.Radiant.Count);
                            
            Game.ExecuteCommand("say Героев3: " + Heroes.All.Count);
            Game.ExecuteCommand("say Героев3: " + Heroes.Dire.Count);
            Game.ExecuteCommand("say Героев3: " + Heroes.Radiant.Count);
        }
    }
}
