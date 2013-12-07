//  Copyright (c) Microsoft Corporation.  All Rights Reserved.

using System;
using System.ServiceModel;
using System.ServiceModel.Description;
using Microsoft.Samples.XmlRpc;
using CalendarInterface;


namespace CalendarEngine
{
    class Server
    {
        private ServiceHost serviceHost;
        public Server() { 
        
        }

        ~Server() {

            //cleanup in the destructor
            terminateServer();
        }

        public void initiateServer()
        {
            //Console.WriteLine("XML-RPC Demo");
            Uri baseAddress = new UriBuilder(Uri.UriSchemeHttp, "localhost"/*Environment.MachineName*/, 8080, "").Uri;
            serviceHost = new ServiceHost(typeof(CalendarAPI));
            var epXmlRpc = serviceHost.AddServiceEndpoint(typeof(ICalendarAPI), new WebHttpBinding(WebHttpSecurityMode.None), new Uri(baseAddress, "./cal"));
            epXmlRpc.Behaviors.Add(new XmlRpcEndpointBehavior());

            serviceHost.Open();
            
            //For debugging purposes
            System.Windows.Forms.MessageBox.Show("Server Started listening at: " + epXmlRpc.ListenUri);
    
            
        }

        public void terminateServer() {

            serviceHost.Close();
        }
    }


}