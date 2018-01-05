using System;
using System.Collections.Generic;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading;
using Ensage;

namespace FirstPlugin.Sys
{
    public class DotaBuffPlayer
    {
        public UInt32 SteamID;
        public string Role;
        public string Grade;
        public string WinRate;
        
        public static void Parse(uint steamid, Action<DotaBuffPlayer> callback)
        {
            ThreadPool.QueueUserWorkItem(_ => SendRequestFromDotaBuff(steamid, callback));
        }

        private static void SendRequestFromDotaBuff(uint steamid, Action<DotaBuffPlayer> callback)
        {
            string result = string.Empty;
            using (CustomWebClient webClient = new CustomWebClient())
            {
                try
                {
                    result = webClient.DownloadString("https://ru.dotabuff.com/players/" + steamid);
                }
                catch (Exception ex)
                {
                    Game.PrintMessage("[TheRyuzaki]: Exception from request dotabuff: " + ex.Message);
                }
            }

            var player = new DotaBuffPlayer
            {
                SteamID = steamid,
                Role = GetRole(result),
                Grade = GetGrate(result),
                WinRate = GetWinRate(result)
            };
            callback?.Invoke(player);
        }
        
        private static string GetWinRate(string htmlDotabuff)
        {
            string winRate = "N/A";
            string pattern = "<dd>(.*?)</dd>";
            var result = CustomWebClient.GetContents(htmlDotabuff, pattern);
            
            for (var i = 0; i < result.Count; i++)
            {
                if (result[i].Contains("%"))
                {
                    winRate = result[i].Replace("<dd>", "").Replace("</dd>", "");
                    break;
                }
            }

            return winRate;
        }

        private static string GetGrate(string htmlDotabuff)
        {
            
            string grate = "N/A";
            string pattern = "<div class=\"grade\"(.*?)>(.*?)</div>";
            var result = CustomWebClient.GetContents(htmlDotabuff, pattern);
            
            for (var i = 0; i < result.Count; i++)
            {
                if (result[i].StartsWith("<div class=\"grade\"") && result[i].EndsWith("</div>"))
                {
                    char symbol = result[i][result[i].Length - 7];
                    grate = (symbol == '-' || symbol == '+') ? result[i].Substring(result[i].Length-8, 2) : symbol.ToString();
                    break;
                }
            }

            return grate;
        }
        
        private static string GetRole(string htmlDotabuff)
        {
            
            string role = "N/A";
            string pattern = "<div class=\"sector role index-0\"(.*?)>(.*?)</div>";
            var result = CustomWebClient.GetContents(htmlDotabuff, pattern);
            
            for (var i = 0; i < result.Count; i++)
            {
                pattern = "</i>(.*?)</div>";
                var result2 = CustomWebClient.GetContents(result[i], pattern);
                for (var i2 = 0; i2 < result2.Count; i2++)
                {
                    if (result2[i2].StartsWith("</i> ") && result2[i2].EndsWith("</div>"))
                    {
                        role = result2[i2].Replace("</i> ", "").Replace("</div>", "");
                        return role;
                    }
                }
            }

            return role;
        }
        
        
    }
}