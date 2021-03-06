﻿using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Windows.Forms.Calendar;
using System.Data.Linq;
using System.Drawing;


namespace Shifaa_EMR_System

{


    public partial class NewAppointment : Form
    {


        private static readonly IDbConnection con = new System.Data.SqlClient.SqlConnection(Properties.Settings.Default.EMRDatabaseConnectionString);
        private readonly SiteFunctionsDataContext doAction = new SiteFunctionsDataContext(con);

        private  int  selectedPatientID;
        private readonly CalendarItem calendarItem;
        private readonly string providerID;
        private bool saved = false;
        private SchedulingCalendar scheduleForm;
        bool isUpdated = false;
        private string firstNameFromAppt;
        private string lastNameFromAppt;
        private List<String> providerIDList = new List<String>();


             
        private Calendar calendar1;

        public NewAppointment(CalendarItem item, string providerID, Calendar calendar1, SchedulingCalendar schedulingCalendar)
        {

            InitializeComponent();

            this.calendarItem = item;
            this.providerID = providerID;
            this.calendar1 = calendar1;

            this.scheduleForm = schedulingCalendar;


            if (this.scheduleForm.Type == "Provider")
            {
                ChooseProviderLabel.Hide();
                ProviderForPatientList.Hide();
            }
            if (this.scheduleForm.Type == "Scheduler")
            {
                if (PatientListView.Rows.Count > 0)
                {


                    int rowIndex = PatientListView.CurrentRow.Index;
                    var selectedPatientIDCell = this.PatientListView["PatientID", rowIndex];
                    selectedPatientID = (Int32)selectedPatientIDCell.Value;
                    ProviderForPatientList.DataSource = patientProviderRelationTableAdapter.GetDataByPatientID(selectedPatientID);
                    ProviderForPatientList.ValueMember = "ProviderID";
                    ProviderForPatientList.DisplayMember = "ProviderInfo";
                }

            }
        }


            public NewAppointment(bool isUpdated, CalendarItem item, string providerID, Calendar calendar1, SchedulingCalendar schedulingCalendar)
        {

            InitializeComponent();

            this.calendarItem = item;
            this.providerID = providerID;
            this.calendar1 = calendar1;
            this.scheduleForm = schedulingCalendar;
            this.isUpdated = isUpdated;


            Console.WriteLine(PatientIDNum.Text);
            firstNameFromAppt = calendarItem.PatientFirstName;
            lastNameFromAppt = calendarItem.PatientLastName;

            string[] text = calendarItem.Text.Split('\n');
            AppointmentDetails.Text = text[2];

            if (this.scheduleForm.Type == "Provider")
            {
                ChooseProviderLabel.Hide();
                ProviderForPatientList.Hide();
            }
            if (this.scheduleForm.Type == "Scheduler")
            {
                if (PatientListView.Rows.Count > 0)
                {


                    int rowIndex = PatientListView.CurrentRow.Index;
                    var selectedPatientIDCell = this.PatientListView["PatientID", rowIndex];
                    selectedPatientID = (Int32)selectedPatientIDCell.Value;
                    ProviderForPatientList.DataSource = patientProviderRelationTableAdapter.GetDataByPatientID(selectedPatientID);
                    ProviderForPatientList.ValueMember = "ProviderID";
                    ProviderForPatientList.DisplayMember = "ProviderInfo";
                }
            }
        }







            private void NewAppointment_Load(object sender, EventArgs e)

        {
            // TODO: This line of code loads data into the 'eMRDatabaseDataSet.PatientProviderRelation' table. You can move, or remove it, as needed.
            this.patientProviderRelationTableAdapter.Fill(this.eMRDatabaseDataSet.PatientProviderRelation);

            this.Visible = true;

            this.patientTableAdapter.Fill(this.eMRDatabaseDataSet.Patient);
           

            this.WindowState = FormWindowState.Normal;
            this.PatientListView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.PatientListView.MultiSelect = false;

            PatientIDNum.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            PatientIDNum.AutoCompleteSource = AutoCompleteSource.CustomSource;
            PatientIDNum.AutoCompleteCustomSource = AutoComplete.PatientIDAutoComplete();


          
             FirstName.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
             FirstName.AutoCompleteSource = AutoCompleteSource.CustomSource;
             FirstName.AutoCompleteCustomSource = AutoComplete.PatientFirstNameAutoComplete();
     

         
             LastName.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
             LastName.AutoCompleteSource = AutoCompleteSource.CustomSource;
             LastName.AutoCompleteCustomSource = AutoComplete.PatientLastNameAutoComplete();



            this.dateTimePicker1.Value = calendarItem.StartDate;
            this.dateTimePicker2.Value = calendarItem.EndDate;


            if (isUpdated)
            {

             
            }




        }

     
        public string GetProviderName()
        {
            string name = "";
            ISingleResult<getProviderInfoResult> result = doAction.getProviderInfo(providerID);
            foreach (getProviderInfoResult r in result)
            {
                name =  r.FirstName + " " + r.LastName + " " + r.Title;
            }

            return name;
        }

        public int GetPatientID()
        {
            return selectedPatientID;
        }

       


        public string GetAppointmentDetails()
        {
            return this.AppointmentDetails.Text;
        }

        public string GetFirstName()
        {
            if (PatientListView.Rows.Count > 0)
            {
                int rowIndex = this.PatientListView.CurrentRow.Index;
                return (String)this.PatientListView["FirstName1", rowIndex].Value;

            }

            return null;
        }

        public string GetLastName()
        {

            if (PatientListView.Rows.Count > 0)
            {
                int rowIndex = this.PatientListView.CurrentRow.Index;
                return (String)this.PatientListView["LastName1", rowIndex].Value;
            }

            return null;

        }

        private void FirstNameClick(object sender, EventArgs e)
        {
            while (FirstName.Text == "First Name")
            {
                FirstName.Text = null;
            }
        }

        private void LastNameClick(object sender, EventArgs e)
        {
            while (LastName.Text == "Last Name")
            {
                LastName.Text = null;
            }
        }

        private void AppointmentDetailsClick(object sender, EventArgs e)
        {
            while (AppointmentDetails.Text == "Appointment Details")
            {
                AppointmentDetails.Text = null;
            }
        }


   

        public bool isSaved()
        {
            return saved;
        }

        private void TextBox1_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void Save_Click(object sender, EventArgs e)
        {
            if (PatientListView.Rows.Count > 0)
            {
                if (!isUpdated)
                {
                    try
                    {




                        int rowIndex = PatientListView.CurrentRow.Index;
                        var selectedPatientIDCell = this.PatientListView["PatientID", rowIndex];
                        selectedPatientID = (Int32)selectedPatientIDCell.Value;

                


                        string firstName = (String)this.PatientListView["FirstName1", rowIndex].Value;
                        string lastName = (String)this.PatientListView["LastName1", rowIndex].Value;

                        doAction.CreateAppointment(firstName, lastName, AppointmentDetails.Text, dateTimePicker1.Value, dateTimePicker2.Value, selectedPatientID, System.DateTime.Now,
                            providerID);

                        string calText = firstName + " " + lastName + "\n" +
                            GetProviderName() + "\n" + AppointmentDetails.Text;

                        calendarItem.StartDate = dateTimePicker1.Value;
                        calendarItem.EndDate = dateTimePicker2.Value;

                        ISingleResult<getJustCreatedAppointmentIDResult> result = doAction.getJustCreatedAppointmentID();

                        int apptID = 0;

                        foreach (getJustCreatedAppointmentIDResult r in result)
                        {
                            apptID = r.appointmentID;

                        }

          
                        CalendarItem cal = new CalendarItem(calendar1, dateTimePicker1.Value, dateTimePicker2.Value, calText,
                            firstName , lastName, apptID, selectedPatientID, providerID, "Scheduled");

                        cal.BackgroundColor = Color.Blue;

                        Console.WriteLine("this item text on main screen: " + calendarItem.Text);


                        scheduleForm.GetCalendarItems().Add(cal);


                        scheduleForm.PlaceItems(scheduleForm.GetCalendarItems());

                        saved = true;
                        this.Close();

                    }
                    catch (Exception ex)
                    {

                        System.Windows.Forms.MessageBox.Show("This patient is not in the system. Please add them as a new patient");
                        this.Close();

                        Exception ex2 = ex;
                        while (ex2.InnerException != null)
                        {
                            ex2 = ex2.InnerException;
                        }
                        Console.WriteLine(ex.InnerException);
                        throw;



                    }
                }

                else
                {
                    try
                    {

                       

                        int rowIndex = PatientListView.CurrentRow.Index;
                        var selectedPatientIDCell = this.PatientListView["PatientID", rowIndex];
                        selectedPatientID = (Int32)selectedPatientIDCell.Value;


                        string firstName = (String)this.PatientListView["FirstName1", rowIndex].Value;
                        string lastName = (String)this.PatientListView["LastName1", rowIndex].Value;

                        doAction.editAppointment(calendarItem.AppointmentID , firstName, lastName, AppointmentDetails.Text, dateTimePicker1.Value, dateTimePicker2.Value, selectedPatientID,
                            providerID);

                        string calText = firstName + " " + lastName + "\n" +
                            GetProviderName() + "\n" + AppointmentDetails.Text;

                        calendarItem.StartDate = dateTimePicker1.Value;
                        calendarItem.EndDate = dateTimePicker2.Value;


                        scheduleForm.GetCalendarItems().Remove(calendarItem);

                        CalendarItem cal = new CalendarItem(calendar1, dateTimePicker1.Value, dateTimePicker2.Value, calText,
                            firstName , lastName, calendarItem.AppointmentID, selectedPatientID, providerID, "Scheduled");


                        cal.BackgroundColor = calendarItem.BackgroundColor;

                        scheduleForm.GetCalendarItems().Add(cal);

                        calendar1.Items.Clear();

                        scheduleForm.PlaceItems(scheduleForm.GetCalendarItems());

                        saved = true;

                        this.Close();

                    }
                    catch (Exception ex)
                    {

                        System.Windows.Forms.MessageBox.Show("This patient is not in the system. Please add them as a new patient");
                        this.Close();

                        Exception ex2 = ex;
                        while (ex2.InnerException != null)
                        {
                            ex2 = ex2.InnerException;
                        }
                        Console.WriteLine(ex.InnerException);
                        throw;



                    }

                }
            }
            


        }


        private void LostFormFocus(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Exit_Click(object sender, EventArgs e)
        {

            Console.WriteLine("closeblah");

            this.Close();
        }

    
       
        private void AppointmentDescriptionLabel_Click(object sender, EventArgs e)
        {

        }



        private void Label5_Click(object sender, EventArgs e)
        {

        }

        private void LastName_TextChanged(object sender, EventArgs e)
        {
            while (LastName.Text.Any(Char.IsDigit))
            {
                LastName.Text = null;
                System.Windows.Forms.MessageBox.Show("Please enter a valid last name");
            }

            int PatientIDNumInt = 0;

            while (!(String.IsNullOrEmpty(PatientIDNum.Text)) && PatientIDNum.Text != "Patient ID #")
            {
                PatientIDNumInt = Int32.Parse(PatientIDNum.Text);
            }

            try
            {

                this.patientTableAdapter.FillBySearch(this.eMRDatabaseDataSet.Patient, PatientIDNumInt, FirstName.Text, LastName.Text);
            }
            catch (FormatException ex)
            {

                //TO DO: Get rid of this when functional
                MessageBox.Show(ex.Message);

                Console.WriteLine("error");
            }


        }

        private void FirstName_TextChanged(object sender, EventArgs e)
        {
            while (FirstName.Text.Any(Char.IsDigit))
            {
                FirstName.Text = null;
                System.Windows.Forms.MessageBox.Show("Please enter a valid first name");
            }

            int PatientIDNumInt = 0;

            while (!(String.IsNullOrEmpty(PatientIDNum.Text)) && PatientIDNum.Text != "Patient ID #")
            {
                PatientIDNumInt = Int32.Parse(PatientIDNum.Text);
            }



            try
            {

                this.patientTableAdapter.FillBySearch(this.eMRDatabaseDataSet.Patient, PatientIDNumInt, FirstName.Text, LastName.Text);

            }
            catch (FieldAccessException ex)
            {
                //TO DO: Get rid of this when functional
                MessageBox.Show(ex.Message);
            }


        }

        private void PatientIDNum_TextChanged(object sender, EventArgs e)
        {

            int patientIDNumInt;

            if (!(String.IsNullOrEmpty(PatientIDNum.Text)) && PatientIDNum.Text != "Patient ID #")
            {

                try
                {
                    patientIDNumInt = Int32.Parse(PatientIDNum.Text);
                    Console.WriteLine("parsed");
                }
                catch (FormatException)
                {
                    PatientIDNum.Text = null;
                    patientIDNumInt = 0;
                    MessageBox.Show("Please enter a valid patient ID number integer");
                }

                try
                {

                    this.patientTableAdapter.FillBySearch(this.eMRDatabaseDataSet.Patient, patientIDNumInt, FirstName.Text, LastName.Text);
                    Console.WriteLine("search tried");

                }
                catch (FieldAccessException ex)
                {
                    //TO DO: Get rid of this when functional
                    MessageBox.Show(ex.Message);
                }
            }

        }

        private void PatientIDNum_Click(object sender, EventArgs e)
        {
            if (PatientIDNum.Text == "Patient ID #") PatientIDNum.Text = null;
        }


   




      


        private void PatientListView_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            if (e.StateChanged != DataGridViewElementStates.Selected) return;


            if (this.scheduleForm.Type == "Scheduler")
            { 
                int rowIndex = PatientListView.CurrentRow.Index;
                var selectedPatientIDCell = this.PatientListView["PatientID", rowIndex];
                selectedPatientID = (Int32)selectedPatientIDCell.Value;
                ProviderForPatientList.DataSource = patientProviderRelationTableAdapter.GetDataByPatientID(selectedPatientID);
                ProviderForPatientList.ValueMember = "ProviderID";
                ProviderForPatientList.DisplayMember = "ProviderInfo";

            }


        }


      
        private void PatientListView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
          
        }


   
        private void Exit_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ProviderForPatientList_SelectedIndexChanged(object sender, EventArgs e)
        {

            string selectedValue = (String)ProviderForPatientList.SelectedValue;
            if (!providerIDList.Contains(selectedValue))
            {
                providerIDList.Add(selectedValue);
            }
            else
            {
                providerIDList.Remove(selectedValue);
            }
        }
    }
}

