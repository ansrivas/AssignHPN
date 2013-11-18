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
        private int TIME_SERVER_UPDATE = 6000;
      
        public void setUpTimer() { 
            
            aTimer = new System.Timers.Timer(10000);
            // Hook up the Elapsed event for the timer.
            aTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            // Set the Interval to (TIME_SERVER_UPDATE x 1000 milliseconds).
            aTimer.Interval = TIME_SERVER_UPDATE;
            //for enabling for disabling the timer.
            aTimer.Enabled = true;
        
        
            return;
        }

        private void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            //disable the timer
            aTimer.Enabled = false;
            //call the required function here.
            Console.WriteLine("The Elapsed event was raised at {0}", e.SignalTime);
        }


    }
}
