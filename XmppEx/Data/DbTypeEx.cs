using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FoxundermoonLib.XmppEx.Data
{
    public static class DbTypeEx
    {
        public static bool IsTheseType(this string origin,params string[] gives){
            if (string.IsNullOrEmpty(origin))
                return false;
            foreach (var give in gives)
            {
                if(!string.IsNullOrEmpty(give))
                {
                    if (origin.ToLower().Contains(give))
                        return true;
                }
            }
            return false;
        }
    }
}
