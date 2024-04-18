using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerG5
{
    internal class TrackerAsset : ITrackerAsset
    {
        bool active = false;



        public bool accept(TrackerEvent e)
        {
            return active;
        }
    }
}
