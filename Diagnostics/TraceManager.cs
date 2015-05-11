using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace FoxundermoonLib.Diagnostics
{
    public class TraceManager
    {
        public static void StartTrace()
        {
            Trace.Listeners.Add(new MySqlTraceListener());
        }
    }
}
