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
            public static Stack<CustomTimer> listRemove = new Stack<CustomTimer>();
            public static List<CustomTimer> ListTimers = new List<CustomTimer>();
            public static DateTime LastCycle = DateTime.Now;
            
            public float Interval;
            public Action Callback;

            public CustomTimer(float interval, Action callback)
            {
                this.Interval = interval;
                this.Callback = callback;
                ListTimers.Add(this);
            }

            public static CustomTimer CreateTimer(Action callback, float interval)
            {
                return new CustomTimer(interval, callback);
            }

            public static void DestroyTimer(CustomTimer timer)
            {
                if (ListTimers.Contains(timer))
                    ListTimers.Remove(timer);
            }

            public static void Cycle()
            {
                float deltatime = (Single)DateTime.Now.Subtract(LastCycle).TotalSeconds;
                
                for (var i = 0; i < ListTimers.Count; i++)
                {
                    ListTimers[i].Interval -= deltatime;
                    if (ListTimers[i].Interval <= 0)
                    {
                        ListTimers[i]?.Callback();
                        listRemove.Push(ListTimers[i]);
                    }
                }

                while (listRemove.Count != 0)
                {
                    var obj = listRemove.Pop();
                    DestroyTimer(obj);
                }
            }
        }
        
        static void Main(string[] args)
        {
            Game.OnStart += GameOnOnStart;
            //Game.OnIngameUpdate += GameOnOnIngameUpdate;
            Game.OnMessage += GameOnOnMessage;
            Game.OnFireEvent += GameOnOnFireEvent;
            Game.OnGCMessageReceive += GameOnOnGcMessageReceive;
            Game.OnIngameUpdate += GameOnOnIngameUpdate;

        }

        private static void GameOnOnUpdate(EventArgs args)
        {
                
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
            CustomTimer.Cycle();
        }

        private static void GameOnOnStart(EventArgs args)
        {
            Game.PrintMessage("[TheRyuzaki] GameOnOnStart2");

            CustomTimer.CreateTimer(() => { Game.ExecuteCommand("say Игроки в сессии: " + Players.All.Count); }, 0.5f);
            ;
            for (var i = 0; i < Players.All.Count; i++)
            { 
                CustomTimer.CreateTimer(() => { Game.ExecuteCommand("say [" + i + "] => [" + Players.All[i].PlayerSteamId + " / " + Players.All[i].Name + "]"); }, 1f + (0.2f * i));
            }
        }
    }
}
