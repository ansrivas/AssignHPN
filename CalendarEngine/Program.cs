//  Copyright (c) Microsoft Corporation.  All Rights Reserved.

using System;
using System.ServiceModel;
using System.ServiceModel.Description;
using Microsoft.Samples.XmlRpc;


namespace Calendar.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("XML-RPC Demo");
            Uri baseAddress = new UriBuilder(Uri.UriSchemeHttp, Environment.MachineName, 3030, "/calendardDemo/").Uri;
            ServiceHost serviceHost = new ServiceHost(typeof(CalendarAPI));
            var epXmlRpc = serviceHost.AddServiceEndpoint(typeof(ICalendarAPI), new WebHttpBinding(WebHttpSecurityMode.None), new Uri(baseAddress, "./cal"));
            epXmlRpc.Behaviors.Add(new XmlRpcEndpointBehavior());

          /*  
           * if I want it to listen to some other URI just append whatever you want
           * 
           * var webBinding = new WebHttpBinding(WebHttpSecurityMode.None);
            var epFeed = serviceHost.AddServiceEndpoint(typeof(IFeed), webBinding, new Uri(baseAddress, "./feed"));
            epFeed.Behaviors.Add(new WebHttpBehavior());
            */

            serviceHost.Open();

            Console.WriteLine("API endpoint listening at {0}", epXmlRpc.ListenUri);
            Console.Write("Press ENTER to quit");
            Console.ReadLine();

            serviceHost.Close();
        }
    }
}
