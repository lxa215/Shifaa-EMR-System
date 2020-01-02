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
    public partial class NewProblem : Form
    {

        int thisPatientID;
        string thisProviderName;
        string thisProviderID;
        private SiteFunctionsDataContext doAction = new SiteFunctionsDataContext(@"Data Source=shifaaserver.database.windows.net;Initial Catalog=EMRDatabase;Persist Security Info=True;User ID=shifaaAdmin;Password=qalbeefeemasr194!");

        public NewProblem(int patientID, string providerName, string providerID)
        {
            this.thisPatientID = patientID;
            this.thisProviderName = providerName;
            this.thisProviderID = providerID;
         
            InitializeComponent();

            ProblemBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            ProblemBox.AutoCompleteSource = AutoCompleteSource.CustomSource;
            ProblemBox.AutoCompleteCustomSource = AutoComplete.ProblemAutoComplete();



        }

        private void SubmitButton_Click(object sender, EventArgs e)
        {
            try
            {
                doAction.createNewProblem(thisPatientID, ProblemBox.Text, DescriptionBox.Text, thisProviderID, thisProviderName , DateTime.Today);

                ((PatientHomePage)this.Owner).problemTableAdapter.FillByPatientID(((PatientHomePage)this.Owner).eMRDatabaseDataSet.Problem, thisPatientID);
                this.Close();
            }
            catch
            {
                MessageBox.Show("Please enter a valid problem name");
            }
        }

        private void NewProblem_Load(object sender, EventArgs e)
        {
         
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
