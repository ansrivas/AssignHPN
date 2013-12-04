
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


namespace CalendarInterface
{
    [ServiceContract]
    public interface ICalendarAPI {

        
        [OperationContract(Action = "sum")]
        int sum(int a,int b); 
     
       
        [OperationContract(Action = "updateServers")]
        String updateServers();

        [OperationContract(Action = "addAppointment")]
        int addAppointment(String dateTime);

        [OperationContract(Action = "removeAppointment")]
        void removeAppointment(String dateTime);

        [OperationContract(Action = "modAppointment")]
        void modAppointment(DateTime dateTime, String msg);

      

    };

    [ServiceBehavior(IncludeExceptionDetailInFaults = true)]
    public class CalendarAPI : ICalendarAPI{

        private DatabaseCon con;
        private int DELAYED_UPDATE ;
        System.Threading.Timer timer;


        public CalendarAPI()
		{
            DELAYED_UPDATE = 4000;
            initConnections();
       	}

        private void initConnections() {

            con = new DatabaseCon();
        }



        int ICalendarAPI.sum(int a, int b) {

          //calling the update other servers after DELAYED_UPDATE seconds
         //   timer = new System.Threading.Timer(obj => { updateOtherServers(); }, null, DELAYED_UPDATE, System.Threading.Timeout.Infinite);
         //   con.initCon();
            return (a + b);
        }

        String ICalendarAPI.updateServers()
        {
            String response = "Server Updated Bro";

            return response;
        }


        int ICalendarAPI.addAppointment(String date)
        {
            String str = "INSERT INTO calendar (aptdate, starttime, endtime, aptheader, aptcomment) VALUES ('2013-12-25','11:11','12:12', 'Zagreb', 'I love you');";
            Console.WriteLine("inside add appt");
            
            DateTime dt = DateTime.ParseExact(date, "yy/MM/dd,HH:mm:ss+00",System.Globalization.CultureInfo.InvariantCulture);
            Console.WriteLine(dt);
            return 1;
        }


        void ICalendarAPI.removeAppointment(String dateTime) {

            return;
        }


        void ICalendarAPI.modAppointment(DateTime dateTime, String msg) {

            return;
        }



        private void updateOtherServers()
        {
            ICalendarAPI calAPI = createChannelFactory();

            String str = calAPI.updateServers();
            Console.WriteLine(str);

            Console.ReadLine();


        }

        public ICalendarAPI createChannelFactory()
        {

            Uri blogAddress = new UriBuilder(Uri.UriSchemeHttp, Environment.MachineName, 8080, "cal").Uri;

            ChannelFactory<ICalendarAPI> calendarAPIFactory = new ChannelFactory<ICalendarAPI>(new WebHttpBinding(WebHttpSecurityMode.None), new EndpointAddress(blogAddress));
            calendarAPIFactory.Endpoint.Behaviors.Add(new XmlRpcEndpointBehavior());

            ICalendarAPI calAPI = calendarAPIFactory.CreateChannel();
            return calAPI;   
        }
       
    }
}
