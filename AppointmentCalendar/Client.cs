
using System;
using System.ServiceModel;
using Microsoft.Samples.XmlRpc;
using CalendarInterface;
using DBEngine;

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

        public void initClientConfig(String machineIP="", String machinePort="",String operation="",String param="")
        {
            PORT_NUMBER = 8080;
            pathValue = "cal";

            dbConn = new DatabaseCon();

            Uri blogAddress = new UriBuilder(Uri.UriSchemeHttp, Environment.MachineName, PORT_NUMBER, pathValue).Uri;
            ChannelFactory<ICalendarAPI> calendarAPIFactory = new ChannelFactory<ICalendarAPI>(new WebHttpBinding(WebHttpSecurityMode.None), new EndpointAddress(blogAddress));
            calendarAPIFactory.Endpoint.Behaviors.Add(new XmlRpcEndpointBehavior());
            calendarAPI = calendarAPIFactory.CreateChannel();

            switch (operation)
            {
                case "ADD":
                    System.Windows.Forms.MessageBox.Show("Adding to the machine with ip" + machineIP);
                    return;
                    int i = calendarAPI.addAppointment(param);
                    Console.WriteLine(i + "\n");

                    Console.ReadLine();
                    break;
                case "MODIFY":
                    System.Windows.Forms.MessageBox.Show("Modifying to the machine with ip" + machineIP);
                    return;
                    calendarAPI.modifyAppointment(param);
                    break;

                case "REMOVE":
                    System.Windows.Forms.MessageBox.Show("REMOVING to the machine with ip" + machineIP);
                    return;
                    calendarAPI.removeAppointment(param);
                    break;
            }
        }

  
    }
}
