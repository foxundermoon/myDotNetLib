using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace FoxundermoonLib.XmppEx.Data
{
   public class Command
    {
        public string Name { get; set; }
        public string Operation { get; set; }
        public bool NeedBroadcast { get; set; }
        public bool NeedResponse { get; set; }
        public string Condition { set; get; }
        public string Sql { set; get; }
       
    }
}
