using System;
using Ensage;

namespace FirstPlugin.Sys
{
    public class GameChat
    {
        static Int32 lastChatMessageTime = (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;

        public static void SendMessage(string message)
        {
            int currentTime = (Int32) (DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
            float offsetTime = 1f;
            if (currentTime <= lastChatMessageTime)
            {
                offsetTime = lastChatMessageTime - currentTime + offsetTime;
                lastChatMessageTime = currentTime + (int) offsetTime;
            }
            else
                lastChatMessageTime = currentTime + (int)offsetTime;
            CustomTimer.CreateTimer(() => Game.ExecuteCommand("say " + message), offsetTime);
        }
    }
}