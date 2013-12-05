
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

        [OperationContract(Action = "addAppointment")]
        int addAppointment(String sql);

        [OperationContract(Action = "removeAppointment")]
        void removeAppointment(String sql);

        [OperationContract(Action = "modAppointment")]
        void modifyAppointment(String sql);


        [OperationContract(Action = "sum")]
        int sum(int a, int b );

    };

    [ServiceBehavior(IncludeExceptionDetailInFaults = true)]
    public class CalendarAPI : ICalendarAPI{

        private DatabaseCon dbConn;
        private int DELAYED_UPDATE ;
        System.Threading.Timer timer;


        public CalendarAPI()
		{
            DELAYED_UPDATE = 4000;
            initConnections();
       	}

        private void initConnections() {

            dbConn = new DatabaseCon();
        }


        int ICalendarAPI.addAppointment(String param)
        {
            return dbConn.queryDB(param);
           
        }


        void ICalendarAPI.removeAppointment(String param) {

            dbConn.queryDB(param);
            return;
        }


        void ICalendarAPI.modifyAppointment(String param) {

            dbConn.queryDB(param);
            return;
        }

        int ICalendarAPI.sum(int a, int b) {
            return (a + b);
        }
    }
}
