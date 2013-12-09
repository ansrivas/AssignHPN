
using System;
using System.ServiceModel;
using Microsoft.Samples.XmlRpc;
using CalendarInterface;
using DBEngine;
using System.IO;
using System.Windows.Forms;



namespace CalendarClient
{
    class Client
    {
        private int PORT_NUMBER;
        private String pathValue;
        private DatabaseCon dbConn;
        public ICalendarAPI calendarAPI;

        public Client() {

        }

        public String initClientConfig(String machineIP="" ,String operation="" ,String param="")
        {
            PORT_NUMBER = 3030;
            pathValue = "";

            dbConn = new DatabaseCon();

            Uri blogAddress = new UriBuilder(Uri.UriSchemeHttp, machineIP, PORT_NUMBER, pathValue).Uri;
            ChannelFactory<ICalendarAPI> calendarAPIFactory = new ChannelFactory<ICalendarAPI>(new WebHttpBinding(WebHttpSecurityMode.None), new EndpointAddress(blogAddress));
            calendarAPIFactory.Endpoint.Behaviors.Add(new XmlRpcEndpointBehavior());
            calendarAPI = calendarAPIFactory.CreateChannel();

            String result = "";
            try
            {
                switch (operation)
                {
                    case "ADD":
                        System.Windows.Forms.MessageBox.Show("Adding to the machine with ip" + machineIP);

                        int i = calendarAPI.addAppointment(param);
                        result = i.ToString();
                        break;
                    case "MODIFY":
                        System.Windows.Forms.MessageBox.Show("Modifying to the machine with ip" + machineIP);

                        i = calendarAPI.modifyAppointment(param);
                        result = i.ToString();
                        break;

                    case "REMOVE":
                        System.Windows.Forms.MessageBox.Show("REMOVING to the machine with ip" + machineIP);

                        i = calendarAPI.removeAppointment(param);
                        result = i.ToString();
                        break;
                    case "REGISTER_ON_NW":
                        result = calendarAPI.registerOnNW(param);
                        break;

                    case "LEAVE_NETWORK":
                        i = calendarAPI.removeIPFromDB(param);
                        result = i.ToString();
                        break;
                    case "SYNC_DB":
                        result = calendarAPI.syncDatabase(param);
                        break;


                    case "INSERT_IP_TO_DB":
                        i = calendarAPI.insertNewIPInDB(param);
                        result = i.ToString();
                        break;
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error in connecting to the server \n" + ex.Message);
                //throw ex;
            }
            return result;
        }

  
    }
}
