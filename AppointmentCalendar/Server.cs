//  Copyright (c) Microsoft Corporation.  All Rights Reserved.

using System;
using System.ServiceModel;
using System.ServiceModel.Description;
using Microsoft.Samples.XmlRpc;
using CalendarInterface;


class Server
    {
        public void initiateServer()
        {
            Console.WriteLine("XML-RPC Demo");
            Uri baseAddress = new UriBuilder(Uri.UriSchemeHttp, "localhost"/*Environment.MachineName*/, 8080, "").Uri;
            ServiceHost serviceHost = new ServiceHost(typeof(CalendarAPI));
            var epXmlRpc = serviceHost.AddServiceEndpoint(typeof(ICalendarAPI), new WebHttpBinding(WebHttpSecurityMode.None), new Uri(baseAddress, "./cal"));
            epXmlRpc.Behaviors.Add(new XmlRpcEndpointBehavior());

            serviceHost.Open();

            Console.WriteLine("API endpoint listening at {0}", epXmlRpc.ListenUri);
            Console.Write("Press ENTER to quit");
            Console.ReadLine();

            serviceHost.Close();
        }
    }


