
//  Copyright (c) Microsoft Corporation.  All Rights Reserved.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.ServiceModel;
using System.ServiceModel.Syndication;
using System.ServiceModel.Web;
using Microsoft.Samples.XmlRpc;
using DBEngine;
using Utils;

namespace Calendar
{
    [ServiceContract]
    public interface ICalendarAPI {

        [OperationContract(Action = "calendar.returnSum")]
        int calendarTest(
            int a,
            int b);


        [OperationContract(Action = "calendar.updateServers")]
        String updateServers();

    };

    [ServiceBehavior(IncludeExceptionDetailInFaults = true)]
    public class CalendarAPI : ICalendarAPI{

        private DatabaseCon con;
        private CUtils utility;

        public CalendarAPI()
		{
            initConnections();
       	}

        private void initConnections() {

            con = new DatabaseCon();
            utility = new CUtils();

            return;
        }
        private void updateOtherServers(){
        
            Uri blogAddress = new UriBuilder(Uri.UriSchemeHttp, Environment.MachineName, 3030, "/calendardDemo/cal").Uri;

            ChannelFactory<ICalendarAPI> calendarAPIFactory = new ChannelFactory<ICalendarAPI>(new WebHttpBinding(WebHttpSecurityMode.None), new EndpointAddress(blogAddress));
            calendarAPIFactory.Endpoint.Behaviors.Add(new XmlRpcEndpointBehavior());

            ICalendarAPI bloggerAPI = calendarAPIFactory.CreateChannel();

            String str = bloggerAPI.updateServers();
            Console.WriteLine(str);

            Console.ReadLine();
        
        }
        int ICalendarAPI.calendarTest(int a, int b) {

            con.initCon();
            utility.setUpTimer();

           // updateOtherServers();
            return (a + b);
        }

        String ICalendarAPI.updateServers()
        {
            String response = "Server Updated Bro";
            return response;
        }
    }
}
