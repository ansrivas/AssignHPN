
//  Copyright (c) Microsoft Corporation.  All Rights Reserved.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.ServiceModel;
using System.ServiceModel.Syndication;
using System.ServiceModel.Web;

using DBEngine;

namespace Calendar
{
    [ServiceContract]
    public interface ICalendarAPI {

        [OperationContract(Action = "calendar.returnSum")]
        int calendarTest(
            int a,
            int b);
    };

    [ServiceBehavior(IncludeExceptionDetailInFaults = true)]
    public class CalendarAPI : ICalendarAPI{
     
        public CalendarAPI()
		{

       	}

        int ICalendarAPI.calendarTest(int a, int b) {

            DatabaseCon con = new DatabaseCon();
            con.initCon();
            return (a + b);
        }
    }
}
