using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Ensage;
using Ensage.Common.Objects;
using Ensage.SDK.Service;

using SharpDX;
using SharpDX.Multimedia;
using Timer = Ensage.SDK.Helpers.Timer;

namespace FirstPlugin
{
    public class FirstPlugin : Plugin
    {
        public class CustomTimer
        {
            public static List<CustomTimer> listRemove = new List<CustomTimer>();
            public static List<CustomTimer> ListTimers = new List<CustomTimer>();
            public static DateTime LastCycle = DateTime.Now;
            
            public float Interval;
            public Action Callback;

            public CustomTimer(float interval, Action callback)
            {
                this.Interval = interval;
                this.Callback = callback;
            }

            public static CustomTimer CreateTimer(Action callback, float interval)
            {
                CustomTimer timer = new CustomTimer(interval, callback);
                ListTimers.Add(timer);
                return timer;
            }

            public static void DestroyTimer(CustomTimer timer, bool execute = false)
            {
                if (ListTimers.Contains(timer))
                {
                    if (execute == true)
                        timer.Callback();
                    ListTimers.Remove(timer);
                }
            }

            public static void Cycle()
            {
                float deltatime = (Single)DateTime.Now.Subtract(LastCycle).TotalSeconds;
                for (var i = 0; i < ListTimers.Count; i++)
                {
                    ListTimers[i].Interval -= deltatime;
                    if (ListTimers[i].Interval <= 0)
                    {
                        try
                        {
                            ListTimers[i].Callback();
                        }
                        catch (Exception ex)
                        {
                            Game.PrintMessage("[TheRyuzaki]: Error from timer: " + ex.Message);
                        }
                        listRemove.Add(ListTimers[i]);
                    }
                }

                if (listRemove.Count > 0)
                {
                    for (var i = 0; i < listRemove.Count; i++)
                    {
                        DestroyTimer(listRemove[i]);
                    }

                    listRemove.Clear();
                }
            }
        }
        
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
            Game.PrintMessage("[TheRyuzaki] GameOnOnStart2");

            CustomTimer.CreateTimer(() => { Game.ExecuteCommand("say Игра начата"); }, 0.5f);
            CustomTimer.CreateTimer(() => { Game.ExecuteCommand("say Начинаем поиск лузеров!"); }, 1.5f);
            CustomTimer.CreateTimer(() => { Game.ExecuteCommand("say Игроки в сессии: " + Players.All.Count); }, 2.0f);
            for (var i = 0; i < Players.All.Count; i++)
            {
                uint streamid = Players.All[i].PlayerSteamId;
                string name = Players.All[i].Name;
                CustomTimer.CreateTimer(() => { Game.ExecuteCommand("say [" + i + "] => [" + streamid + " / " + name + "]"); }, 3f + (0.2f * i));
            }
        }
    }
}
