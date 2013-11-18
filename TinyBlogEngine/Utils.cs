using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;

namespace Utils
{
    class CUtils
    {
        private System.Timers.Timer aTimer ;


        public void setUpTimer() { 
            
            aTimer = new System.Timers.Timer(10000);
            // Hook up the Elapsed event for the timer.
            aTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            // Set the Interval to 6 seconds (6000 milliseconds).
            aTimer.Interval = 6000;
            //for enabling for disabling the timer.
            aTimer.Enabled = true;
        
        
            return;
        }

        private void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            //disable the timer
            aTimer.Enabled = false;
            Console.WriteLine("The Elapsed event was raised at {0}", e.SignalTime);
        }


    }
}
