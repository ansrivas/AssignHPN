
using System;
using System.ServiceModel;
using Microsoft.Samples.XmlRpc;
using Calendar;

namespace CalendarClient
{
    class Program
    {
        private int PORT_NUMBER;
        private String pathValue;
        public ICalendarAPI calendarAPI;

        public Program() {
            PORT_NUMBER = 5678;
            pathValue = "statename.rem";// "/calendardDemo/cal";
            init();
        }

        private void init()
        {
            Uri blogAddress = new UriBuilder(Uri.UriSchemeHttp, Environment.MachineName, PORT_NUMBER, pathValue).Uri;
       
            ChannelFactory<ICalendarAPI> calendarAPIFactory = new ChannelFactory<ICalendarAPI>(new WebHttpBinding(WebHttpSecurityMode.None), new EndpointAddress(blogAddress));
            calendarAPIFactory.Endpoint.Behaviors.Add(new XmlRpcEndpointBehavior());

            calendarAPI = calendarAPIFactory.CreateChannel();
        }

        static void Main(string[] args)
        {
            Program prg = new Program();

            int i = prg.calendarAPI.calendarTest(1, 2);
            Console.WriteLine(i + "\n");
            Console.WriteLine("Please enter date and time in this format-> 13/02/07,16:05");
            String datetime = Console.ReadLine();
          //  String str = "hello leute";
          //  int k = prg.calendarAPI.addAppointment(datetime);
            Console.ReadLine();
        }

        
    }
}
