﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Shifaa_EMR_System
{
    public partial class NewLabOrder : Form
    {

        private readonly SiteFunctionsDataContext doAction = new SiteFunctionsDataContext(@"Data Source=shifaaserver.database.windows.net;Initial Catalog=EMRDatabase;Persist Security Info=True;User ID=shifaaAdmin;Password=qalbeefeemasr194!");
        readonly int thisPatientID;
        readonly string thisProviderID;
        readonly string thisProviderName;

        public NewLabOrder(int patientID, string providerID , string providerName)
        {
            InitializeComponent();
            this.thisPatientID = patientID;
            this.thisProviderID = providerID;
            this.thisProviderName = providerName;

            LabNameBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            LabNameBox.AutoCompleteSource = AutoCompleteSource.CustomSource;
            LabNameBox.AutoCompleteCustomSource = AutoComplete.LabNameCollection();


        }

        private void SubmitButton_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(LabNameBox.Text)) MessageBox.Show("Please enter a valid lab order");

            else
            {
                try
                {
                    doAction.createNewLabOrder(LabNameBox.Text, thisProviderName, thisProviderID, ScheduledDatePicker.Value, thisPatientID, DateTime.Today);
                    ((PatientHomePage)this.Owner).patientLabTableAdapter.FillByPatientID(((PatientHomePage)this.Owner).eMRDatabaseDataSet.PatientLab, thisPatientID);
                    this.Close();

                }
                catch
                {
                    MessageBox.Show("Please enter a lab order or close the page");
                }
            }
          
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void NewLabOrder_Load(object sender, EventArgs e)
        {

        }

        private void LabNameBox_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
