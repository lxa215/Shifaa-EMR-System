﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Data.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.Calendar;

namespace Shifaa_EMR_System
{
    public partial class PatientHomePage : Form
    {

        private static readonly IDbConnection con = new System.Data.SqlClient.SqlConnection(Properties.Settings.Default.EMRDatabaseConnectionString);
        private readonly SiteFunctionsDataContext doAction = new SiteFunctionsDataContext(con);

        readonly int thisPatientID;
        readonly ProviderMain thisProviderMain;
        readonly SchedulingCalendar schedulingCalendar;
        readonly ToolStripMenuItem thisGenerateReport;
        readonly ToolStripMenuItem thisPrintPrescriptions;
        readonly ToolStripMenuItem thisPrintScansProcedures;
        readonly ToolStripMenuItem thisPrintLabs;
        readonly string thisProviderID;
        CalendarItem item;


        public PatientHomePage(string name, string number, string gender, string age, string maritalStatus,  DateTime DOB, int selectedPatientID, ProviderMain providerMain)
        {
            InitializeComponent();
            this.PatientNameLabel.Text = name;
            this.PhoneNumberLabel.Text = "Phone Number: " + number;
            this.PatientGenderLabel.Text = gender;
            this.PatientAgeLabel.Text = "Age: " + age;
            this.DOBLabel.Text = "DOB: " + DOB.ToShortDateString();
            this.MaritalStatusLabel.Text = "Marital Status:" + maritalStatus;

            this.thisProviderID = providerMain.GetProviderID();
            SetVitals(selectedPatientID);

            thisPatientID = selectedPatientID;
            this.thisProviderMain = providerMain;
            this.FinishVisitButton.Hide();

            thisGenerateReport = new ToolStripMenuItem("Generate Report");
            thisGenerateReport.Click += new System.EventHandler(GenerateReportClick);
            thisPrintPrescriptions = new ToolStripMenuItem("Print Prescriptions");
            thisPrintPrescriptions.Click += new System.EventHandler(PrintPrescriptionsClick);
            thisPrintLabs = new ToolStripMenuItem("Print Lab Orders");
            thisPrintLabs.Click += new System.EventHandler(PrintLabs);
            thisPrintScansProcedures = new ToolStripMenuItem("Print Procedure Orders");
            thisPrintScansProcedures.Click += new System.EventHandler(PrintScansProcedures);


            thisProviderMain.menuStrip2.Items.Add(thisGenerateReport);
            thisProviderMain.menuStrip2.Items.Add(thisPrintPrescriptions);
            thisProviderMain.menuStrip2.Items.Add(thisPrintLabs);
            thisProviderMain.menuStrip2.Items.Add(thisPrintScansProcedures);


        }

        public PatientHomePage(string name, string number, string gender, string maritalStatus, string age, DateTime DOB, int selectedPatientID, ProviderMain providerMain,  SchedulingCalendar schedulingCalendar, CalendarItem item)
        {
            InitializeComponent();
            this.PatientNameLabel.Text = name;
            this.PhoneNumberLabel.Text = "Phone Number: " + number;
            this.PatientGenderLabel.Text = gender;
            this.PatientAgeLabel.Text = "Age: " + age;
            this.DOBLabel.Text = "DOB: " + DOB.ToShortDateString();
            this.MaritalStatusLabel.Text = "Marital Status: " + maritalStatus;

            SetVitals(selectedPatientID);

            thisPatientID = selectedPatientID;
            this.thisProviderMain = providerMain;
            this.schedulingCalendar = schedulingCalendar;
            this.FinishVisitButton.Show();

            this.NoteHistoryTable.AutoGenerateColumns = false;
            this.LabsTable.AutoGenerateColumns = false;
            this.AllergiesTable.AutoGenerateColumns = false;
            this.VitalHistoryTable.AutoGenerateColumns = false;
            this.ScansTable.AutoGenerateColumns = false;
            this.ProblemListView.AutoGenerateColumns = false;
            this.MedicationsListDataGridView.AutoGenerateColumns = false;

            thisGenerateReport = new ToolStripMenuItem("Generate Report");
            thisGenerateReport.Click += new System.EventHandler(GenerateReportClick);
            thisPrintPrescriptions = new ToolStripMenuItem("Print Prescriptions");
            thisPrintPrescriptions.Click += new System.EventHandler(PrintPrescriptionsClick);
            thisPrintLabs = new ToolStripMenuItem("Print Lab Orders");
            thisPrintLabs.Click += new System.EventHandler(PrintLabs);
            thisPrintScansProcedures = new ToolStripMenuItem("Print Procedure Orders");
            thisPrintScansProcedures.Click += new System.EventHandler(PrintScansProcedures);


            thisProviderMain.menuStrip2.Items.Add(thisGenerateReport);
            thisProviderMain.menuStrip2.Items.Add(thisPrintPrescriptions);
            thisProviderMain.menuStrip2.Items.Add(thisPrintLabs);
            thisProviderMain.menuStrip2.Items.Add(thisPrintScansProcedures);

            this.thisProviderID = providerMain.GetProviderID();

            this.item = item;

            this.WindowState = FormWindowState.Maximized;
           
        }

        private void IsNotVisible(object sender , FormClosedEventArgs e)
        {
          
                thisProviderMain.menuStrip2.Items.Remove(thisGenerateReport);
                thisProviderMain.menuStrip2.Items.Remove(thisPrintPrescriptions);
                thisProviderMain.menuStrip2.Items.Remove(thisPrintScansProcedures);
                thisProviderMain.menuStrip2.Items.Remove(thisPrintLabs);
            
        }

        private void GenerateReportClick(object sender, EventArgs e)
        {
            if (Application.OpenForms["PrintableReport"] as PrintableReport == null)
            {
                PrintableReport report = new PrintableReport(thisPatientID, thisProviderMain.GetProviderID());
                Center(report);
                report.Show();
            }
        }

       private void PrintPrescriptionsClick(object sender, EventArgs e)
       {
            if (Application.OpenForms["PrintableReport"] as PrintableReport == null)
            {
                PrintPrescriptionsForm printPrescriptions = new PrintPrescriptionsForm(thisPatientID, thisProviderMain.GetProviderID());
                Center(printPrescriptions);
                printPrescriptions.Show();
            }
       }

        private void PrintLabs(object sender, EventArgs e)
        {
            if (Application.OpenForms["PrintLabs"] as PrintLabs == null)
            {
                PrintLabs printLabs = new PrintLabs(thisPatientID, thisProviderMain.GetProviderID());
                Center(printLabs);
                printLabs.Show();
            }
        }

        private void PrintScansProcedures(object sender, EventArgs e)
        {
            if (Application.OpenForms["PrintScanProcedures"] as PrintScanProcedure == null)
            {
                PrintScanProcedure printScan = new PrintScanProcedure(thisPatientID, thisProviderMain.GetProviderID());
                Center(printScan);
                printScan.Show();
            }
        }





            private void SetVitals(int selectedPatientID)
        {
            ISingleResult<getLatestPatientVitalsResult> result = doAction.getLatestPatientVitals(selectedPatientID);

            foreach (getLatestPatientVitalsResult vital in result)
            {
                this.BloodPressureValueLabel.Text = vital.BloodPressure + " mm Hg";
                this.PulseValueLabel.Text = vital.Pulse + " bpm";
                this.TemperatureValueLabel.Text = vital.Temperature + " °C";
                this.WeightValueLabel.Text = vital.Weight + " Kg";
                this.HeightValueLabel.Text = vital.Height + " cm";
                Double BMI = 0.0;
                if (vital.BMI.Contains("-") || String.IsNullOrWhiteSpace(vital.BMI))
                {
                    this.BMIValueLabel.Text = " - " + " Kg/m²";
                }
                else
                {
                    BMI = Double.Parse(vital.BMI);

                    this.BMIValueLabel.Text = Math.Round(BMI, 2).ToString() + " Kg/m²";

                }
                   


             

            }


        }

        private void Panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void ExitButton_Click(object sender, EventArgs e)
        {

        }

        private void NoteHistoryLabel_Click(object sender, EventArgs e)
        {

        }

        

        private void PatientHomePage_Load(object sender, EventArgs e)
        {
  

            this.AllergiesTable.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.NoteHistoryTable.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.VitalHistoryTable.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.ScansTable.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.MedicationsListDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.AppointmentListView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.LabsTable.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.AllergiesTable.MultiSelect = false;
            this.NoteHistoryTable.MultiSelect = false;
            this.VitalHistoryTable.MultiSelect = false;
            this.ScansTable.MultiSelect = false;
            this.MedicationsListDataGridView.MultiSelect = false;
            this.AppointmentListView1.MultiSelect = false;
            this.LabsTable.MultiSelect = false;


            this.WindowState = FormWindowState.Maximized;




            // TODO: This line of code loads data into the 'eMRDatabaseDataSet.Appointment' table. You can move, or remove it, as needed
            this.patientNoteTableAdapter.FillByPatientID(this.eMRDatabaseDataSet.PatientNote, thisPatientID);
            this.vitalSignsTableAdapter.FillByPatientID(this.eMRDatabaseDataSet.VitalSigns, thisPatientID);
            this.allergieTableAdapter.FillByPatientID(this.eMRDatabaseDataSet.Allergie, thisPatientID);
            this.patientLabTableAdapter.FillByPatientID(this.eMRDatabaseDataSet.PatientLab, thisPatientID);
            this.prescriptionTableAdapter.FillByPatientID(this.eMRDatabaseDataSet.Prescription, thisPatientID);
            this.patientScanTableAdapter.FillByPatientID(this.eMRDatabaseDataSet.PatientScan, thisPatientID);
            this.problemTableAdapter.FillByPatientID(this.eMRDatabaseDataSet.Problem, thisPatientID);
            this.appointmentTableAdapter.FillByPatientID(this.eMRDatabaseDataSet.Appointment, thisPatientID);

            this.AutoScroll = false;

            // disable horizontal scrollbar
            this.HorizontalScroll.Enabled = false;

            // restore AutoScroll
            this.AutoScroll = true;


        }

        private void FormGotFocused(object sender, EventArgs e)
        {
            Console.WriteLine("hi im focused");
            this.patientNoteTableAdapter.FillByPatientID(this.eMRDatabaseDataSet.PatientNote, thisPatientID);
            this.vitalSignsTableAdapter.FillByPatientID(this.eMRDatabaseDataSet.VitalSigns, thisPatientID);
            this.allergieTableAdapter.FillByPatientID(this.eMRDatabaseDataSet.Allergie, thisPatientID);
            this.patientLabTableAdapter.FillByPatientID(this.eMRDatabaseDataSet.PatientLab, thisPatientID);
            this.prescriptionTableAdapter.FillByPatientID(this.eMRDatabaseDataSet.Prescription, thisPatientID);
            this.patientScanTableAdapter.FillByPatientID(this.eMRDatabaseDataSet.PatientScan, thisPatientID);
            this.problemTableAdapter.FillByPatientID(this.eMRDatabaseDataSet.Problem, thisPatientID);
            this.appointmentTableAdapter.FillByPatientID(this.eMRDatabaseDataSet.Appointment, thisPatientID);

        }


        private void PatientNameLabel_Click(object sender, EventArgs e)
        {

        }

        private void PhoneNumberLabel_Click(object sender, EventArgs e)
        {

        }

        private void PatientGenderLabel_Click(object sender, EventArgs e)
        {

        }

        private void PatientAgeLabel_Click(object sender, EventArgs e)
        {

        }



     

     

        private void Center(Form form)
        {
            form.Location = new Point((Screen.PrimaryScreen.Bounds.Size.Width / 2) - (form.Size.Width / 2), (Screen.PrimaryScreen.Bounds.Size.Height / 2) - (form.Size.Height / 2));
        }

        private void AddNoteButton_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms["NewNote"] as NewNote == null)
            {
                NewNote newNoteForm = new NewNote(thisPatientID, thisProviderMain.GetProviderName(),
                    thisProviderMain.GetProviderID() , this);
                Center(newNoteForm);
                newNoteForm.Show();

            }
        }

        private void AddMedicationButton_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms["NewPrescriptionForm"] as NewPrescriptionForm == null)
            {
                NewPrescriptionForm newPrescription = new NewPrescriptionForm(thisPatientID, thisProviderMain.GetProviderName(),
                thisProviderMain.GetProviderID())
                {
                    Owner = this
                };
                Center(newPrescription);
                newPrescription.Show();
            }
        }

        private void AddAllergieButton_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms["NewAllergie"] as NewAllergie == null)
            {
                NewAllergie newAllergie = new NewAllergie(thisPatientID, thisProviderMain.GetProviderID(),
                    thisProviderMain.GetProviderName())
                {
                    Owner = this
                };
                Center(newAllergie);
                newAllergie.Show();
            }
        }

        private void AddNewScanButton_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms["NewScanOrder"] as NewScanOrder == null)
            {
                NewScanOrder newScan = new NewScanOrder(thisPatientID, thisProviderMain.GetProviderName(),
                    thisProviderMain.GetProviderID())
                {
                    Owner = this
                };
                Center(newScan);
                newScan.Show();
            }
        }

        private void AddLabButton_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms["NewLabOrder"] as NewLabOrder == null)
            {
                NewLabOrder newLabOrder = new NewLabOrder(thisPatientID, thisProviderMain.GetProviderID(),
                    thisProviderMain.GetProviderName())
                {
                    Owner = this
                };
                Center(newLabOrder);
                newLabOrder.Show();
            }

        }

        private void AddNewVitalButton_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms["NewVital"] as NewVital == null)
            {
                NewVital newVital = new NewVital(thisPatientID, this)
                {
                    Owner = this
                };
                Center(newVital);
                newVital.Show();
            }

        }

        private void Label2_Click(object sender, EventArgs e)
        {

        }

        private void AddNewAppointmentButton_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms["SchedulingCalendar"] as SchedulingCalendar == null)
            {
                SchedulingCalendar schedulingCalendar = new SchedulingCalendar(thisPatientID , (ProviderMain)this.MdiParent , this)
                {
                  
                };
                schedulingCalendar.WindowState = FormWindowState.Normal;
                Center(schedulingCalendar);
                schedulingCalendar.Show();
            }

            InvokeLostFocus(this, e);
        }

       

        private void CancelButton_Click(object sender, EventArgs e)
        {
            thisProviderMain.menuStrip2.Items.Remove(thisGenerateReport);
            thisProviderMain.menuStrip2.Items.Remove(thisPrintPrescriptions);
            thisProviderMain.menuStrip2.Items.Remove(thisPrintScansProcedures);
            thisProviderMain.menuStrip2.Items.Remove(thisPrintLabs);
            thisProviderMain.AutoScroll = false;
            this.Close();
        }

        
        private void NoteHistoryTable_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void DeleteAppointmentButton_Click(object sender, EventArgs e)
        {
            if (AppointmentListView1.SelectedRows.Count > 0)
            {
                int currentRow = AppointmentListView1.CurrentRow.Index;

                int selectedAppointmentID = (Int32)AppointmentListView1["appointmentID", currentRow].Value;
                if (MessageBox.Show(string.Format("Are you sure you want to delete this appointment?"), "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    try
                    {
                        AppointmentListView1.Rows.RemoveAt(currentRow);
                        doAction.deleteAppointment(selectedAppointmentID);
                    }
                    catch
                    {
                        MessageBox.Show("Error");
                    }
                }
            }

        }

        private void DeletePrescriptionButton_Click(object sender, EventArgs e)
        {
            if (MedicationsListDataGridView.SelectedRows.Count > 0)
            {


                int currentRow = MedicationsListDataGridView.CurrentRow.Index;

                int selectedPrescriptionID = (int)MedicationsListDataGridView["prescriptionID", currentRow].Value;
                if (MessageBox.Show(string.Format("Are you sure you want to delete this Prescription?"), "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    try
                    {
                        MedicationsListDataGridView.Rows.RemoveAt(currentRow);
                        if (doAction.deletePatientPrescription(selectedPrescriptionID, DateTime.Today, thisProviderID) == 1)
                        {
                            MessageBox.Show("Unable to delete prescription. You can only delete prescriptions that you have written today");
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Error");
                    }
                }

                deletePrescriptionButton.Show();
            }
        }

        private void DeleteAllergyButton_Click(object sender, EventArgs e)
        {
            if (AllergiesTable.SelectedRows.Count > 0)
            {


                int currentRow = AllergiesTable.CurrentRow.Index;

                int selectedAllergyID = (int)AllergiesTable["PatientAllergieID", currentRow].Value;
                if (MessageBox.Show(string.Format("Are you sure you want to delete this Allergy?"), "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    try
                    {
                        AllergiesTable.Rows.RemoveAt(currentRow);
                        if((doAction.deletePatientAllergy(selectedAllergyID , DateTime.Today , thisProviderID) == 1))
                        {
                            MessageBox.Show("Unable to delete allergy. You can only delete allergies that you have written today");

                        }
                    }
                    catch
                    {
                        MessageBox.Show("Error");
                    }
                }
            }
        }

        private void DeleteScanButton_Click(object sender, EventArgs e)
        {
            int currentRow = ScansTable.CurrentRow.Index;

            int selectedScanID = (Int32)ScansTable["PatientScanID", currentRow].Value;
            if (MessageBox.Show(string.Format("Are you sure you want to delete this scan or procedure?"), "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    ScansTable.Rows.RemoveAt(currentRow);
                    doAction.deletePatientScan(selectedScanID , thisProviderID , DateTime.Today);
                }
                catch
                {
                    MessageBox.Show("Error");
                }
            }
        }

        private void DeleteLabButton_Click(object sender, EventArgs e)
        {
            if (LabsTable.SelectedRows.Count > 0)
            {

                int currentRow = LabsTable.CurrentRow.Index;

                int selectedLabID = (Int32)LabsTable["PatientLabID", currentRow].Value;
                if (MessageBox.Show(string.Format("Are you sure you want to delete this lab?"), "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    try
                    {
                        LabsTable.Rows.RemoveAt(currentRow);
                        doAction.deletePatientLab(selectedLabID , thisProviderID , DateTime.Today);
                    }
                    catch
                    {
                        MessageBox.Show("Error");
                    }
                }
            }
        }

        private void DeleteNoteButton_Click(object sender, EventArgs e)
        {
            if (NoteHistoryTable.SelectedRows.Count > 0)
            {

                int currentRow = NoteHistoryTable.CurrentRow.Index;

                int selectedNoteID = (Int32)NoteHistoryTable["NoteID", currentRow].Value;

                DateTime noteDate = (DateTime)NoteHistoryTable["Date", currentRow].Value;
                string noteDateString = noteDate.ToShortDateString();


                if(noteDateString != DateTime.Today.ToShortDateString())
                {
                    MessageBox.Show("You can only delete notes from today.");

                    return; 
                }

                if (MessageBox.Show(string.Format("Are you sure you want to delete this note?"), "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    int i = doAction.deletePatientNote(selectedNoteID, DateTime.Today, thisProviderID);
                    try
                    {
                        if (i == 1)
                        {
                            MessageBox.Show("Unable to delete note. You can only delete notes you have written on this date.");
                        }
                        else
                        {
                            NoteHistoryTable.Rows.RemoveAt(currentRow);
                        }
                      
                    }
                    catch
                    {
                        MessageBox.Show("Error");
                    }
                }
            }
        }

        private void DeleteVitalButton_Click(object sender, EventArgs e)
        {
            if (VitalHistoryTable.SelectedRows.Count > 0)
            {

                int currentRow = VitalHistoryTable.CurrentRow.Index;

                int selectedVitalID = (Int32)VitalHistoryTable["VitalSignID", currentRow].Value;
                if (MessageBox.Show(string.Format("Are you sure you want to delete this vital sign entry?"), "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    try
                    {
                        VitalHistoryTable.Rows.RemoveAt(currentRow);
                        doAction.deleteVitalSigns(selectedVitalID , DateTime.Today);
                    }
                    catch
                    {
                        MessageBox.Show("Error");
                    }
                }
            }
        }

        private void AddProblemButton_Click(object sender, EventArgs e)
        {
            if (Application.OpenForms["NewProblem"] as NewProblem == null)
            {
                NewProblem newProblem = new NewProblem(thisPatientID, thisProviderMain.GetProviderName(),
                    thisProviderMain.GetProviderID())
                {
                    Owner = this
                };
                Center(newProblem);
                newProblem.Show();
            }
        }

        private void RemoveProblemButton_Click(object sender, EventArgs e)
        {
            if (ProblemListView.SelectedRows.Count > 0)
            {

                int currentRow = ProblemListView.CurrentRow.Index;

                int selectedProblemID = (Int32)ProblemListView["ProblemID", currentRow].Value;
                if (MessageBox.Show(string.Format("Are you sure you want to delete this problem entry?"), "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    try
                    {
                        ProblemListView.Rows.RemoveAt(currentRow);

                        if (doAction.deleteProblem(selectedProblemID, thisProviderID, DateTime.Today) == 1)
                        {
                            MessageBox.Show("Unable to delete problem. You can only delete problems that you have written today");

                        }

                    }
                    catch
                    {
                        MessageBox.Show("Error");
                    }
                }
            }
        }

        private void UpdateProblemButton_Click(object sender, EventArgs e)
        {

            if (ProblemListView.SelectedRows.Count > 0) {
                int currentRow = ProblemListView.CurrentRow.Index;

                int selectedProblemID = (Int32)ProblemListView["ProblemID", currentRow].Value;
                string problemName = (String)ProblemListView["ProblemName", currentRow].Value;
                string problemDescription = (String)ProblemListView["ProblemDescription", currentRow].Value;

                if (Application.OpenForms["UpdateProblem"] as UpdateProblem == null)
                {
                    UpdateProblem updateProblem = new UpdateProblem(selectedProblemID, problemName, problemDescription, thisPatientID)
                    {
                        Owner = this
                    };
                    Center(updateProblem);
                    updateProblem.Show();
                }
            }

        }

        private void FinishVisitButton_Click(object sender, EventArgs e)
        {
            // TO DO: Complete the Checked Out Button
            string checkedOut = "Complete at: " + DateTime.Now.ToString("HH:mm");
            doAction.updateAppointmentStatus(checkedOut, schedulingCalendar.GetAppointmentID() , "Green" , item.Pattern.ToString() , item.PatternColor.ToString());
            item.BackgroundColor = Color.Green;
            thisProviderMain.menuStrip2.Items.Remove(thisGenerateReport);
            thisProviderMain.menuStrip2.Items.Remove(thisPrintPrescriptions);
            thisProviderMain.menuStrip2.Items.Remove(thisPrintLabs);
            thisProviderMain.menuStrip2.Items.Remove(thisPrintScansProcedures);
            thisProviderMain.AutoScroll = false;
            this.Close();

        }

        private void ProblemListView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void EditMedicationButton_Click(object sender, EventArgs e)
        {
            if (MedicationsListDataGridView.SelectedRows.Count > 0)
            {
                int currentRow = MedicationsListDataGridView.CurrentRow.Index;

                int selectedPrescriptionID = (Int32)MedicationsListDataGridView["PrescriptionID", currentRow].Value;
                string medicationName = (String)MedicationsListDataGridView["MedicationName", currentRow].Value;
                string amount = (String)MedicationsListDataGridView["Amount", currentRow].Value;
                string strength = (String)MedicationsListDataGridView["Strength", currentRow].Value;
                string route = (String)MedicationsListDataGridView["Route", currentRow].Value;
                string frequency = (String)MedicationsListDataGridView["Frequency", currentRow].Value;
                double? doubleRefills = null; 
                //TODO: Fix the refills 


                if (Application.OpenForms["UpdatePrescription"] as UpdatePrescription == null)
                {
                    UpdatePrescription updatePrescription = new UpdatePrescription(medicationName, amount, strength, frequency, doubleRefills, route, selectedPrescriptionID, thisPatientID);
                    Center(updatePrescription);
                    updatePrescription.Owner = this;
                    updatePrescription.Show();
                }
            }
        }

       


        private void ProblemListView_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void AddMedicationButton_Click_1(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void VitalHistoryTable_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
