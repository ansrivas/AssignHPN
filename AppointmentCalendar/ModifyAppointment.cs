using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DBEngine;
using Utils;
using CalendarClient;

namespace AppointmentCalendar
{
    public partial class ModifyAppointment : Form
    {
        private String author;
        private String primaryKey;
        private DatabaseCon dbConn;
        private Client clientObject;

        public ModifyAppointment()
        {
            InitializeComponent();
            setupFormatting();
            dbConn = CUtils.getDBConn();
            clientObject = new Client();
        }

        private void setupFormatting()
        {

            datePicker.Format = DateTimePickerFormat.Custom;
            datePicker.CustomFormat = "yyyy-MM-dd";
            fromTimePicker.Format = DateTimePickerFormat.Custom;
            fromTimePicker.CustomFormat = "HH:mm";
            toTimePicker.Format = DateTimePickerFormat.Custom;
            toTimePicker.CustomFormat = "HH:mm";
        }


        public void setupValues( String header, String comments, String date, String starttime, String endtime, String author) {

            this.headerTextBox.Text = header;
            this.commentsTextBox.Text = comments;
            this.datePicker.Text = date;
            this.fromTimePicker.Text = starttime;
            this.toTimePicker.Text = endtime;
            this.author = author;

            String fetchPrimaryKey  = "Select aptid from calendar where aptdate='" + date + "' AND starttime='" + starttime + "' AND endtime='" + endtime + "' AND aptheader='" + header + "' AND aptcomment='" + comments + "' AND author='" + author + "';";
            String primaryKey = dbConn.queryPrimeKey(fetchPrimaryKey);

            this.primaryKey = primaryKey;
        }


        private void updateAppointment_button_Click(object sender, EventArgs e)
        {
            String header = headerTextBox.Text;
            String comments = commentsTextBox.Text;
            String date = datePicker.Text;
            String starttime = fromTimePicker.Text;
            String endtime = toTimePicker.Text;


            String updateAppointmentSql = "Update calendar set aptdate='"+date+ "', starttime='" + starttime+ "', endtime ='"+ endtime + "', aptheader='"+ header + "', aptcomment ='" +comments + "',author='"+ author +"'  where aptid='"+ primaryKey +"';";
            
            dbConn.queryDB(updateAppointmentSql);


            //Now create channel factory and call others
            String getIpAndPort = "select * from user";
            String listOfIPs = dbConn.getDataSet(getIpAndPort);
            String[] hosts = CUtils.parse(listOfIPs);

            foreach (String ip in hosts)
            {
                clientObject.initClientConfig(ip, "", "MODIFY", updateAppointmentSql);

            }
                


            MessageBox.Show("Successfully Modified Appointment, Update Other Servers!!", "Important Message", MessageBoxButtons.OK,
                               MessageBoxIcon.Exclamation,
                               MessageBoxDefaultButton.Button1);
            this.Close();
        }
    }
}
