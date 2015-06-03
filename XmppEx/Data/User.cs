using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoxundermoonLib.XmppEx.Data
{
    public class User
    {
        public string Name { get; set; }
        public string Resource { get; set; }
        public User(string name, string resource)
        {
            Name = name;
            Resource = resource;
        }
    }
}
