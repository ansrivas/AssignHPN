
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

        [OperationContract(Action = "ICalendarAPI.addAppointment")]
        int addAppointment(String sql);

        [OperationContract(Action = "ICalendarAPI.removeAppointment")]
        int removeAppointment(String sql);

        [OperationContract(Action = "ICalendarAPI.modAppointment")]
        int modifyAppointment(String sql);

        [OperationContract(Action = "ICalendarAPI.registerOnNW")]
        String registerOnNW(String sql);

        [OperationContract(Action = "ICalendarAPI.sum")]
        int sum(int a, int b );

        [OperationContract(Action = "ICalendarAPI.syncDatabase")]
        String syncDatabase(String str);

        [OperationContract(Action = "ICalendarAPI.insertNewIPInDB")]
        int insertNewIPInDB(String ip);

        [OperationContract(Action = "ICalendarAPI.removeIPFromDB")]
        int removeIPFromDB(String ip);


         [OperationContract(Action = "ICalendarAPI.returnString")]
        String returnString(String ip);

    };

    [ServiceBehavior(IncludeExceptionDetailInFaults = true)]
    public class CalendarAPI : ICalendarAPI{

        private DatabaseCon dbConn;

        // Delegate
        public delegate void PropertyChangeHandler(object sender, PropertyChangeEventArgs data);
        
        // The event
        public static event PropertyChangeHandler PropertyChange;


        public CalendarAPI()
		{
            initConnections();
       	}

        private void initConnections() {

            dbConn = new DatabaseCon();
        }


        private void generateEvent() {

            try
            {
                // Check for null, in case no other forms have registered with this event.
               // PropertyChangeEventArgs e = new PropertyChangeEventArgs();
                if (PropertyChange != null)
                {
                    // Call the Event
                    PropertyChange(this, new PropertyChangeEventArgs());
                }

            }
            catch (Exception exception)
            {
                System.Windows.Forms.MessageBox.Show(exception.Message);
            }
        
        
        
        
        }

        int ICalendarAPI.addAppointment(String param)
        {

        //    System.Windows.Forms.MessageBox.Show("addAppointment param : " + param);
            int result = dbConn.queryDB(param);
            generateEvent();
            return result;
           
        }


        int ICalendarAPI.removeAppointment(String param) {

         //   System.Windows.Forms.MessageBox.Show("removeAppointment param : " + param);

            int i = dbConn.queryDB(param);
            generateEvent();
            return i;
        }

        

        int ICalendarAPI.modifyAppointment(String param) {

          //  System.Windows.Forms.MessageBox.Show("modifyAppointment param :  " + param);
            int i = dbConn.queryDB(param);
            generateEvent();
            return i; 
        }


        //Return the list of machines on the network
        string ICalendarAPI.registerOnNW(String sql) { 
        
            String sqlQueryIPTable = "select ipAddr from user";
            String dataSet = dbConn.getDataSet(sqlQueryIPTable);
           // System.Windows.Forms.MessageBox.Show(dataSet);
            return dataSet;
        }

        string ICalendarAPI.syncDatabase(String sql) {

            String sqlQueryGetDB = "select * from calendar";
            String dataSet = dbConn.getDataSet(sqlQueryGetDB);
           // System.Windows.Forms.MessageBox.Show(dataSet);
            return dataSet;

        }



        int ICalendarAPI.insertNewIPInDB(String ip)
        { 
            String query = "INSERT INTO user (ipAddr) VALUES ('" + ip +"');";
            return dbConn.queryDB(query);
        
        }


        int ICalendarAPI.removeIPFromDB(String ip) {

            String query = "DELETE FROM user WHERE ipAddr='" + ip + "'";
            return dbConn.queryDB(query);
        }



        int ICalendarAPI.sum(int a, int b) {
            return (a + b);
        }


        //Test debug API
        String ICalendarAPI.returnString(String str) { 
            
            string ret = "Ankur";
            System.Windows.Forms.MessageBox.Show(ret);

            return ret;
        }


    }

    public class PropertyChangeEventArgs : EventArgs
    {
        public PropertyChangeEventArgs()
        {
        }
    }

   
}

