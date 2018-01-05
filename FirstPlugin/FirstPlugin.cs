using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Ensage;
using Ensage.Common.Objects;
using Ensage.SDK.Service;
using FirstPlugin.System;
using SharpDX;
using SharpDX.Multimedia;
using Timer = Ensage.SDK.Helpers.Timer;

namespace FirstPlugin
{
    public class FirstPlugin : Plugin
    {
        
        
        static void Main(string[] args)
        {
            Game.OnStart += GameOnOnStart;
            Game.OnIngameUpdate += GameOnOnIngameUpdate;

        }

        private static void GameOnOnIngameUpdate(EventArgs args)
        {
            CustomTimer.Cycle();
        }

        private static void GameOnOnStart(EventArgs args)
        {
            CustomTimer.CreateTimer(() =>
            {
                GameChat.SendMessage("Игра начата");
                GameChat.SendMessage("Начинаем поиск лузеров!");
                GameChat.SendMessage("Игроки в сессии: " + Players.All.Count);
                for (var i = 0; i < Players.All.Count; i++)
                {
                    uint steamid = Players.All[i].PlayerSteamId;
                    string name = Players.All[i].Name;
                    string tick = i.ToString();
                    GameChat.SendMessage($"[{i}] => [{steamid} / {name}]");
                }
            }, 5f);
        }
    }
}
