using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerG5
{
    internal interface ISerializer
    {
        string Serialize(TrackerEvent e);
    }
}
