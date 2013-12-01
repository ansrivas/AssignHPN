
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

        [OperationContract(Action = "calendarTest")]
        int calendarTest(
            int a,
            int b);


        [OperationContract(Action = "updateServers")]
        String updateServers();


        [OperationContract(Action = "addAppointment")]
        int addAppointment(String dateTime);

    /*    [OperationContract(Action = "calendar.removeAppointment")]
        void removeAppointment(DateTime dateTime);

        [OperationContract(Action = "calendar.modAppointment")]
        void modAppointment(DateTime dateTime, String msg);
*/
      

    };

    [ServiceBehavior(IncludeExceptionDetailInFaults = true)]
    public class CalendarAPI : ICalendarAPI{

        private DatabaseCon con;
        private CUtils utility;
        private int DELAYED_UPDATE ;
        System.Threading.Timer timer;


        public CalendarAPI()
		{
            DELAYED_UPDATE = 4000;
            initConnections();
       	}

        private void initConnections() {

            con = new DatabaseCon();
            utility = new CUtils();

            return;
        }


        private void updateOtherServers(){

            Console.WriteLine("returning from updateOtherServers");
            return;
            Uri blogAddress = new UriBuilder(Uri.UriSchemeHttp, Environment.MachineName, 8080, "/calendardDemo/cal").Uri;

            ChannelFactory<ICalendarAPI> calendarAPIFactory = new ChannelFactory<ICalendarAPI>(new WebHttpBinding(WebHttpSecurityMode.None), new EndpointAddress(blogAddress));
            calendarAPIFactory.Endpoint.Behaviors.Add(new XmlRpcEndpointBehavior());

            ICalendarAPI bloggerAPI = calendarAPIFactory.CreateChannel();

            String str = bloggerAPI.updateServers();
            Console.WriteLine(str);

            Console.ReadLine();
        
        }
        int ICalendarAPI.calendarTest(int a, int b) {

          //  con.initCon();

            //calling the update other servers after DELAYED_UPDATE seconds
         //   timer = new System.Threading.Timer(obj => { updateOtherServers(); }, null, DELAYED_UPDATE, System.Threading.Timeout.Infinite);
            
            return (a + b);
        }

        String ICalendarAPI.updateServers()
        {
            String response = "Server Updated Bro";

            return response;
        }


        int ICalendarAPI.addAppointment(String dateTime)
        {
            Console.WriteLine("inside add appt");
            string date = dateTime; // "13/02/07,16:05:13+00";
            DateTime dt = DateTime.ParseExact(date, "yy/MM/dd,HH:mm:ss+00",System.Globalization.CultureInfo.InvariantCulture);
            Console.WriteLine(dt);
            return 1;
        }
    }
}
