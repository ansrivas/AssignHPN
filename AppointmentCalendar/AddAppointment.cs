using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Utils;
using DBEngine;
using CalendarClient;

namespace AppointmentCalendar
{
    public partial class AddAppointment : Form
    {
        private DatabaseCon dbConn;
        private Client clientObject;

        public AddAppointment()
        {
            InitializeComponent();
            dbConn = CUtils.getDBConn();
            clientObject = new Client();
            setupFormatting();

            
        }

        private void setupFormatting() {

            datePicker.Format = DateTimePickerFormat.Custom;
            datePicker.CustomFormat = "yyyy-MM-dd";
            fromTimePicker.Format = DateTimePickerFormat.Custom;
            fromTimePicker.CustomFormat = "HH:mm";
            toTimePicker.Format = DateTimePickerFormat.Custom;
            toTimePicker.CustomFormat = "HH:mm";
        }


        private void saveAppointment_button_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(headerTextBox.Text) && !String.IsNullOrEmpty(commentsTextBox.Text))
            {

                String header = headerTextBox.Text;
                String comments = commentsTextBox.Text;
                String date = datePicker.Text;
                String fromTime = fromTimePicker.Text;
                String toTime = toTimePicker.Text;

                String sql = @"INSERT INTO calendar (aptdate, starttime, endtime, aptheader, aptcomment,author) VALUES ('" + date + "','" + fromTime + "','" + toTime + "','" + header + "','" + comments + "','Ankur');";

                dbConn.queryDB(sql);

                String getIpAndPort = "select * from user";

                String listOfIPs = dbConn.getDataSet(getIpAndPort);
                String []hosts = CUtils.parse(listOfIPs);


                //Now create channel factory and call others
                //Fetch the IP from, loop through it and conn
                
                foreach (String ip in hosts) {
                    if (!ip.Equals(""))
                    {
                        clientObject.initClientConfig(ip, "", "ADD", sql);
                    }
                }
                
                this.Close();
            }
            else
            {

                MessageBox.Show("Please fill in all the values");
            }
        }


    }
}
