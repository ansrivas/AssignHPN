
using System;
using System.ServiceModel;
using Microsoft.Samples.XmlRpc;
using CalendarInterface;
using DBEngine;

namespace CalendarClient
{
    class Client
    {
        private int PORT_NUMBER;
        private String pathValue;
        private DatabaseCon dbConn;
        public ICalendarAPI calendarAPI;

        public Client() {

            init();
        }

        private void init()
        {
            PORT_NUMBER = 8080;
            pathValue = "cal";

            dbConn = new DatabaseCon();

            Uri blogAddress = new UriBuilder(Uri.UriSchemeHttp, Environment.MachineName, PORT_NUMBER, pathValue).Uri;
            ChannelFactory<ICalendarAPI> calendarAPIFactory = new ChannelFactory<ICalendarAPI>(new WebHttpBinding(WebHttpSecurityMode.None), new EndpointAddress(blogAddress));
            calendarAPIFactory.Endpoint.Behaviors.Add(new XmlRpcEndpointBehavior());
            calendarAPI = calendarAPIFactory.CreateChannel();
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Sending request to Server");
            Client prg = new Client();
            int i = prg.calendarAPI.sum(1,5);
            Console.WriteLine(i + "\n");
            prg.dbConn.initCon();
            Console.WriteLine("Please enter date and time in this format-> 13/02/07,16:05");
            String datetime = Console.ReadLine();

            

            Console.ReadLine();
        }

        
    }
}
