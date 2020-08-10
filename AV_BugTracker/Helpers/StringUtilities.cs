using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace AV_BugTracker.Helpers
{
    public class StringUtilities
    {

        /// <summary>
        /// Produces optional, URL-friendly version of a title, "like-this-one". 
        /// hand-tuned for speed, reflects performance refactoring contributed
        /// by John Gietzen (user otac0n) 
        /// </summary>
        public static string URLFriendly(string title)
        {
            if (title == null) return "";
            const int maxlen = 80;
            int len = title.Length;
            bool prevdash = false;
            var sb = new StringBuilder(len);
            char c;
            for (int i = 0; i < len; i++)
            {
                c = title[i];
                if ((c >= 'a' && c <= 'z') || (c >= '0' && c <= '9'))
                {
                    sb.Append(c);
                    prevdash = false;
                }
                else if (c >= 'A' && c <= 'Z')
                {
                    // tricky way to convert to lowercase
                    sb.Append((char)(c | 32));
                    prevdash = false;
                }
                else if (c == ' ' || c == ',' || c == '.' || c == '/' ||
                c == '\\' || c == '-' || c == '_' || c == '=')
                {
                    if (!prevdash && sb.Length > 0)
                    {
                        sb.Append('-');
                        prevdash = true;
                    }
                }
                else if (c == '#')
                {
                    if (i > 0)
                        if (title[i - 1] == 'C' || title[i - 1] == 'F')
                            sb.Append("-sharp");
                }
                else if (c == '+')
                {
                    sb.Append("-plus");
                }
                else if ((int)c >= 128)
                {
                    int prevlen = sb.Length;
                    sb.Append(RemapInternationalCharToAscii(c));
                    if (prevlen != sb.Length) prevdash = false;
                }
                if (sb.Length == maxlen) break;
            }
            if (prevdash)
                return sb.ToString().Substring(0, sb.Length - 1);
            else
                return sb.ToString();
        }
        private static string RemapInternationalCharToAscii(char c)
        {
            string s = c.ToString().ToLowerInvariant();
            if ("Ã Ã¥Ã¡Ã¢Ã¤Ã£Ã¥Ä…".Contains(s))
            {
                return "a";
            }
            else if ("Ã¨Ã©ÃªÃ«Ä™".Contains(s))
            {
                return "e";
            }
            else if ("Ã¬Ã­Ã®Ã¯Ä±".Contains(s))
            {
                return "i";
            }
            else if ("Ã²Ã³Ã´ÃµÃ¶Ã¸Å‘Ã°".Contains(s))
            {
                return "o";
            }
            else if ("Ã¹ÃºÃ»Ã¼Å­Å¯".Contains(s))
            {
                return "u";
            }
            else if ("Ã§Ä‡ÄÄ‰".Contains(s))
            {
                return "c";
            }
            else if ("Å¼ÅºÅ¾".Contains(s))
            {
                return "z";
            }
            else if ("Å›ÅŸÅ¡Å".Contains(s))
            {
                return "s";
            }
            else if ("Ã±Å„".Contains(s))
            {
                return "n";
            }
            else if ("Ã½Ã¿".Contains(s))
            {
                return "y";
            }
            else if ("ÄŸÄ".Contains(s))
            {
                return "g";
            }
            else if (c == 'ř')
            {
                return "r";
            }
            else if (c == 'ł')
            {
                return "l";
            }
            else if (c == 'đ')
            {
                return "d";
            }
            else if (c == 'ß')
            {
                return "ss";
            }
            else if (c == 'Þ')
            {
                return "th";
            }
            else if (c == 'ĥ')
            {
                return "h";
            }
            else if (c == 'ĵ')
            {
                return "j";
            }
            else
            {
                return "";
            }

        }

    }



}