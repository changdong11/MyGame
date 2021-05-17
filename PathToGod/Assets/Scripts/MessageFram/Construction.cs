using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectPratice
{
    class Construction : IConstruction
    {
        public string Name { get; }

        public object Body { get ; set ; }
        public Construction(string name,object body)
        {
            this.Name = name;
            this.Body = body;
        }
        public override string ToString()
        {
            var msg = "Notification Name:" + Name;
            msg += "\nBody:" + ((Body == null) ? "null" : Body.ToString());
            return msg;
        }
    }
}
