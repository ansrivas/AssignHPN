using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AppointmentCalendar
{
    public partial class ModifyAppointment : Form
    {
        public ModifyAppointment()
        {
            InitializeComponent();
        }

        public void setupValues(String header, String comments, String date, String starttime, String endtime) {

            this.headerTextBox.Text = header;
            this.commentsTextBox.Text = comments;
            this.datePicker.Text = date;
            this.fromTimePicker.Text = starttime;
            this.toTimePicker.Text = endtime;
        
        }
        private void updateAppointment_button_Click(object sender, EventArgs e)
        {
            String sql = "Update calendar set starttime='' where author='Ankur' AND ...";
        }
    }
}
