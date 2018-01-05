using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Ensage;
using Ensage.Common.Objects;
using Ensage.SDK.Service;
using FirstPlugin.Sys;
using SharpDX;
using SharpDX.Multimedia;
using Timer = Ensage.SDK.Helpers.Timer;

namespace FirstPlugin
{
    public class FirstPlugin : Plugin
    {
        public static GameState LastGameState = GameState.Disconnect;
        
        static void Main(string[] args)
        {
            Game.OnIngameUpdate += GameOnOnIngameUpdate;

        }

        private static void GameOnOnIngameUpdate(EventArgs args)
        {
            if (LastGameState != Game.GameState)
            {
                OnGameStatusChange(Game.GameState);
                LastGameState = Game.GameState;
            }
            CustomTimer.Cycle();
        }

        private static void OnGameStatusChange(GameState newGameState)
        {
            if (newGameState == GameState.PreGame)
            {
                CustomTimer.CreateTimer(() =>
                {
                    GameChat.SendMessage("Начинается движуха(By ~ vk.com id223225363)");
                    GameChat.SendMessage("Информация о игроках Radiant: ");
                    for (var i = 0; i < Players.Radiant.Count; i++)
                    {
                        uint steamid = Players.Radiant[i].PlayerSteamId;
                        string name = Players.Radiant[i].Name;
                        if (steamid != 0)
                        {
                            DotaBuffPlayer.Parse(steamid, player =>
                            {
                                GameChat.SendMessage($"[{name}]: Уровень игры: {player.Grade}; Винрейт: {player.WinRate}; Роль: {player.Role}");
                            });
                        }
                    }

                    CustomTimer.CreateTimer(() =>
                    {
                        GameChat.SendMessage("Информация о игроках Dire: ");
                        for (var i = 0; i < Players.Dire.Count; i++)
                        {
                            uint steamid = Players.Dire[i].PlayerSteamId;
                            string name = Players.Dire[i].Name;
                            if (steamid != 0)
                            {
                                DotaBuffPlayer.Parse(steamid, player =>
                                {
                                    GameChat.SendMessage($"[{name}]: Уровень игры: {player.Grade}; Винрейт: {player.WinRate}; Роль: {player.Role}");
                                });
                            }
                        }
                    }, 10f);
                }, 5f);
            }
        }
    }
}
