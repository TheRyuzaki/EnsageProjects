using System;
using System.Collections.Generic;
using Ensage;

namespace FirstPlugin.System
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
                DateTime tickTick = DateTime.Now;
                float deltatime = (Single)tickTick.Subtract(LastCycle).TotalSeconds;
                LastCycle = tickTick;
                
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
                        DestroyTimer(listRemove[i]);
                    listRemove.Clear();
                }
            }
        }
}