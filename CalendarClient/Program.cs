
using System;
using System.ServiceModel;
using Microsoft.Samples.XmlRpc;
using Calendar;

namespace CalendarClient
{
    class Program
    {
        static void Main(string[] args)
        {
           
            Uri blogAddress = new UriBuilder(Uri.UriSchemeHttp, Environment.MachineName, 3030, "/calendardDemo/cal").Uri;

            ChannelFactory<ICalendarAPI> calendarAPIFactory = new ChannelFactory<ICalendarAPI>(new WebHttpBinding(WebHttpSecurityMode.None), new EndpointAddress(blogAddress));
            calendarAPIFactory.Endpoint.Behaviors.Add(new XmlRpcEndpointBehavior());

            ICalendarAPI bloggerAPI = calendarAPIFactory.CreateChannel();

            int i = bloggerAPI.calendarTest(1, 2);
            Console.WriteLine(i);


        }
    }
}
