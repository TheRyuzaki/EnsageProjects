using System;
using System.Collections.Generic;
using System.Net;
using System.Text.RegularExpressions;

namespace FirstPlugin.Sys
{
    public class CustomWebClient : WebClient
    {
        public CustomWebClient(){}

        protected override WebRequest GetWebRequest(Uri address)
        {
            this.Credentials = CredentialCache.DefaultCredentials;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12 | SecurityProtocolType.Ssl3;
            var request = base.GetWebRequest(address) as HttpWebRequest;
            request.UserAgent="Mozilla/5.0 (Windows NT 6.1) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/41.0.2228.0 Safari/537.36";
            return request;
        }
        
        public static List<string> GetContents(string input, string pattern)
        {
            MatchCollection matches = Regex.Matches(input, pattern, RegexOptions.Singleline);
            List<string> contents = new List<string>();
            foreach (Match match in matches)
                contents.Add(match.Value);
            return contents;

        }
    }
}