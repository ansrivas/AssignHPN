
using System;
using System.ServiceModel;
using Microsoft.Samples.XmlRpc;
using CalendarInterface;
using DBEngine;
using System.IO;
using System.Windows.Forms;
using Utils;



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
            PORT_NUMBER = 8080;
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
                    case CUtils.ADD_APPOINTMENTS:
                        System.Windows.Forms.MessageBox.Show("Adding to the machine with ip : " + machineIP);

                        int i = calendarAPI.addAppointment(param);
                        result = i.ToString();
                        break;

                    case CUtils.MODIFY_APPOINTMENTS:
                        System.Windows.Forms.MessageBox.Show("Modifying to the machine with ip : " + machineIP);

                        i = calendarAPI.modifyAppointment(param);
                        result = i.ToString();
                        break;

                    case CUtils.REMOVE_APPOINTMENTS:
                        System.Windows.Forms.MessageBox.Show("REMOVING to the machine with ip" + machineIP);

                        i = calendarAPI.removeAppointment(param);
                        result = i.ToString();
                        break;
                    case CUtils.REGISTER_ON_NETWORK:
                        result = calendarAPI.registerOnNW(param);
                        break;

                    case CUtils.LEAVE_NETWORK:
                        i = calendarAPI.removeIPFromDB(param);
                        result = i.ToString();
                        break;
                    case CUtils.SYNCHRONIZE_DATABASE:
                        result = calendarAPI.syncDatabase(param);
                        break;


                    case CUtils.SEND_IP_TO_MACHINES:
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
