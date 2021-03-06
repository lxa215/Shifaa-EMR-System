﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Data.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Shifaa_EMR_System
{
    public partial class NewNote : Form
    {

        private static readonly IDbConnection con = new System.Data.SqlClient.SqlConnection(Properties.Settings.Default.EMRDatabaseConnectionString);
        private readonly SiteFunctionsDataContext doAction = new SiteFunctionsDataContext(con);
        
        readonly int thisPatientID;
        readonly string thisProviderName;
        readonly string thisProviderID;
        readonly PatientHomePage thisPatientHome;
        private int DraftNoteID;
        private int ExistingNoteID;

        public NewNote(int patientID , string providerName , string providerID, PatientHomePage patientHomePage)
        {
            this.thisPatientID = patientID;
            this.thisProviderName = providerName;
            this.thisProviderID = providerID;
            this.thisPatientHome = patientHomePage;
            InitializeComponent();
        }


        private void NewNote_Load(object sender, EventArgs e)
        {
     
            // TODO: This line of code loads data into the 'eMRDatabaseDataSet.PatientNote' table. You can move, or remove it, as needed.
            this.patientNoteTableAdapter.FillByPatientIDIgnoreStatus(eMRDatabaseDataSet.PatientNote, thisPatientID);
            this.NoteHistoryTable.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.NoteHistoryTable.MultiSelect = false;
            this.NewNoteDateValue.Text = DateTime.Today.ToShortDateString();

            string status = "New";
            DraftNoteID = doAction.createNewPatientNote(thisPatientID, thisProviderName, thisProviderID, "", "" , status, DateTime.Now, DateTime.Today);
            this.patientNoteTableAdapter.FillByPatientIDIgnoreStatus(this.eMRDatabaseDataSet.PatientNote, thisPatientID);

            Console.WriteLine("NoteD is " + DraftNoteID);


        }

        private void AddendumBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void NoteHistoryTable_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }


        
        private void NoteHistoryTable_DoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if(NoteHistoryTable.Rows.Count > 0)
            {
                try
                {
                    DateTime existingNoteDate = (DateTime)NoteHistoryTable["NoteDate", e.RowIndex].Value;
                    String noteTitle = (String)NoteHistoryTable["NoteTitle", e.RowIndex].Value;
                    ExistingNoteID = (int)NoteHistoryTable["ColumnNumber", e.RowIndex].Value;
                    String noteContent = "";
                    String providerName = "";
                    String noteHeader = "";

                    String date = existingNoteDate.ToShortDateString();
                    existingNoteDate = Convert.ToDateTime(date);
                    ISingleResult<getPatientNoteResult> result = doAction.getPatientNote(thisPatientID, ExistingNoteID, existingNoteDate);

                    foreach (getPatientNoteResult r in result)
                    {
                        Console.WriteLine("Result");
                        noteContent = r.NoteContent;
                        providerName = r.ProviderName;
                        noteHeader = r.NoteHeader;

                        if (r.Status == "Signed") AddExistingNoteTab(existingNoteDate, noteTitle, noteContent, providerName, noteHeader);
                        if (r.Status == "Draft") AddNoteDraft(noteTitle, noteContent, noteHeader, thisProviderName, ExistingNoteID);


                    }
                }
                catch(Exception ex)
                {
                    
                }
               



            }





        }


        private void AddNoteDraft(String noteTitle, String noteContent, String noteheader,  String providerName , int noteDraftID)
        {
            Console.WriteLine(noteContent);
            string[] soapNoteContent = noteContent.Split(';');
            this.tabPage1.Text = noteTitle;
            this.SubjectiveNoteBox.Text = soapNoteContent[0];
            this.ObjectiveNoteBox.Text = soapNoteContent[1];
            this.AssesmentBox.Text = soapNoteContent[2];
            this.PlanBox.Text = soapNoteContent[3];
            this.DraftNoteID = noteDraftID;

        }

        private void AddExistingNoteTab(DateTime date, String noteTitle, String noteContent, String providerName , String noteheader)
        {
            TabPage newTab = new TabPage();


            Button newSignButton = new System.Windows.Forms.Button();
            Label newDateValueLabel = new System.Windows.Forms.Label();
            RichTextBox newAddendumBox = new System.Windows.Forms.RichTextBox();
            Label newTitleValueNameLabel = new System.Windows.Forms.Label();
            Button newAddendumButton = new System.Windows.Forms.Button();
            Label newNoteLabel = new System.Windows.Forms.Label();
            RichTextBox newNoteBox = new System.Windows.Forms.RichTextBox();
            Label newDateLabel = new System.Windows.Forms.Label();
            Label newNoteTitleLabel = new System.Windows.Forms.Label();
            Label newAddendumLabel = new System.Windows.Forms.Label();
            Button newExitButton = new System.Windows.Forms.Button();

            // SignButton1
            // 
            newSignButton.BackColor = System.Drawing.SystemColors.ActiveCaption;
            newSignButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            newSignButton.FlatAppearance.BorderSize = 0;
            newSignButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            newSignButton.Font = new System.Drawing.Font("Bahnschrift Light", 10F);
            newSignButton.Location = new System.Drawing.Point(437, 49);
            newSignButton.Name = "newSignButton";
            newSignButton.Size = new System.Drawing.Size(94, 26);
            newSignButton.TabIndex = 66;
            newSignButton.Text = "Sign";
            newSignButton.UseVisualStyleBackColor = false;
            newSignButton.Hide();
            newSignButton.Click += new System.EventHandler(ExistingSignButtonClick);
          
            // 
            // DateValueLabel1
            // 
            newDateValueLabel.AutoSize = true;
            newDateValueLabel.Font = new System.Drawing.Font("Bahnschrift Light", 10F);
            newDateValueLabel.Location = new System.Drawing.Point(101, 12);
            newDateValueLabel.Name = "newDateValueLabel";
            newDateValueLabel.Size = new System.Drawing.Size(82, 17);
            newDateValueLabel.TabIndex = 57;
            newDateValueLabel.Text = date.ToShortDateString();
            newDateValueLabel.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // AddendumBox1
            // 
            newAddendumBox.Font = new System.Drawing.Font("Bahnschrift Light", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            newAddendumBox.Location = new System.Drawing.Point(7, 314);
            newAddendumBox.Multiline = true;
            newAddendumBox.Name = "newAddendumBox";
            newAddendumBox.Size = new System.Drawing.Size(650, 254);
            newAddendumBox.TabIndex = 56;
            newAddendumBox.TextChanged += new System.EventHandler(this.AddendumBox_TextChanged);
            newAddendumBox.ScrollBars = RichTextBoxScrollBars.Vertical;
            newAddendumBox.BorderStyle = BorderStyle.FixedSingle;

            newAddendumBox.Hide();
            // 
            // TitleNameLabel
            // 
            newTitleValueNameLabel.AutoSize = true;
            newTitleValueNameLabel.Font = new System.Drawing.Font("Bahnschrift Light", 10F);
            newTitleValueNameLabel.Location = new System.Drawing.Point(101, 38);
            newTitleValueNameLabel.Name = "newTitleValueNameLabel";
            newTitleValueNameLabel.Size = new System.Drawing.Size(36, 17);
            newTitleValueNameLabel.TabIndex = 53;
            newTitleValueNameLabel.Text = noteTitle;
            newTitleValueNameLabel.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // AddendumButton
            // 
            newAddendumButton.BackColor = System.Drawing.SystemColors.ActiveCaption;
            newAddendumButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            newAddendumButton.FlatAppearance.BorderSize = 0;
            newAddendumButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            newAddendumButton.Font = new System.Drawing.Font("Bahnschrift Light", 10F);
            newAddendumButton.Location = new System.Drawing.Point(437, 49);
            newAddendumButton.Name = "newAddendumButton";
            newAddendumButton.Size = new System.Drawing.Size(94, 26);
            newAddendumButton.TabIndex = 52;
            newAddendumButton.Text = "Addendum";
            newAddendumButton.UseVisualStyleBackColor = false;
            newAddendumButton.Click += new System.EventHandler(AddendumButtonClick);
            newAddendumButton.Show();
            // 
            // NoteLabel1
            // 
            newNoteLabel.AutoSize = true;
            newNoteLabel.Font = new System.Drawing.Font("Bahnschrift Light", 10F);
            newNoteLabel.Location = new System.Drawing.Point(26, 64);
            newNoteLabel.Name = "newNoteLabel";
            newNoteLabel.Size = new System.Drawing.Size(42, 17);
            newNoteLabel.TabIndex = 51;
            newNoteLabel.Text = "Note:";
            newNoteLabel.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // NoteBox1
            // 
            newNoteBox.Font = new System.Drawing.Font("Bahnschrift Light", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            newNoteBox.Location = new System.Drawing.Point(6, 100);
            newNoteBox.Multiline = true;
            newNoteBox.Name = "newNoteBox";
            if(noteContent.Contains("wrote on: "))
            {
                newNoteBox.Text = noteContent;
            }
            else
            {
                newNoteBox.Text = noteheader + "\n" + noteContent.Replace(noteheader, "") + "\n";

            }
            newNoteBox.Size = new System.Drawing.Size(738, 623);
            newNoteBox.TabIndex = 50;
            newNoteBox.ScrollBars = RichTextBoxScrollBars.Vertical;
            newNoteBox.BorderStyle = BorderStyle.FixedSingle;
           
            // 

            // 
            // DateLabel1
            // 
            newDateLabel.AutoSize = true;
            newDateLabel.Font = new System.Drawing.Font("Bahnschrift Light", 10F);
            newDateLabel.Location = new System.Drawing.Point(26, 12);
            newDateLabel.Name = "newDateLabel";
            newDateLabel.Size = new System.Drawing.Size(40, 17);
            newDateLabel.TabIndex = 47;
            newDateLabel.Text = "Date: ";
            newDateLabel.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // NoteTitleLabel1
            // 
            newNoteTitleLabel.AutoSize = true;
            newNoteTitleLabel.Font = new System.Drawing.Font("Bahnschrift Light", 10F);
            newNoteTitleLabel.Location = new System.Drawing.Point(26, 38);
            newNoteTitleLabel.Name = "newNoteTitleLabel";
            newNoteTitleLabel.Size = new System.Drawing.Size(72, 17);
            newNoteTitleLabel.TabIndex = 46;
            newNoteTitleLabel.Text = "Note Title: ";
            newNoteTitleLabel.TextAlign = System.Drawing.ContentAlignment.BottomRight;


            // newAddendumLabel
            // 
            newAddendumLabel.AutoSize = true;
            newAddendumLabel.Font = new System.Drawing.Font("Bahnschrift Light", 10F);
            newAddendumLabel.Location = new System.Drawing.Point(24, 293);
            newAddendumLabel.Name = "newAddendumLabel";
            newAddendumLabel.Size = new System.Drawing.Size(77, 17);
            newAddendumLabel.TabIndex = 67;
            newAddendumLabel.Text = "Addendum: ";
            newAddendumLabel.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            newAddendumLabel.Hide();

            //newExitButton

            newExitButton.BackColor = System.Drawing.SystemColors.ActiveCaption;
            newExitButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            newExitButton.FlatAppearance.BorderSize = 0;
            newExitButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            newExitButton.Font = new System.Drawing.Font("Bahnschrift Light", 10F);
            newExitButton.Location = new System.Drawing.Point(550, 49);
            newExitButton.Name = "ExitButton";
            newExitButton.Size = new System.Drawing.Size(94, 26);
            newExitButton.TabIndex = 52;
            newExitButton.Text = "Exit";
            newExitButton.UseVisualStyleBackColor = false;
            newExitButton.Click += new System.EventHandler(ExitButtonClick);



            newTab.Controls.Add(newExitButton);
            newTab.Controls.Add(newSignButton);
            newTab.Controls.Add(newDateValueLabel);
            newTab.Controls.Add(newAddendumBox);
            newTab.Controls.Add(newTitleValueNameLabel);
            newTab.Controls.Add(newAddendumButton);
            newTab.Controls.Add(newNoteLabel);
            newTab.Controls.Add(newNoteBox);
            newTab.Controls.Add(newDateLabel);
            newTab.Controls.Add(newNoteTitleLabel);
            newTab.Controls.Add(newAddendumLabel);
            newTab.Location = new System.Drawing.Point(4, 22);
            newTab.Padding = new System.Windows.Forms.Padding(3);
            newTab.Size = new System.Drawing.Size(669, 584);
            newTab.TabIndex = NewNoteTabControl.TabCount;
            newTab.Text = noteTitle;
            newTab.UseVisualStyleBackColor = true;

            NewNoteTabControl.TabPages.Add(newTab);
            NewNoteTabControl.SelectedTab = newTab;

         

        }

        private void AddendumButtonClick(object sender, EventArgs e)
        {
            //Hide the Addendum Button 
            Button thisAddendumButton = (Button)NewNoteTabControl.SelectedTab.Controls.Find("newAddendumButton", false)[0];
            thisAddendumButton.Hide();
           


            //Show Sign Button
            Button thisSignButton = (Button)NewNoteTabControl.SelectedTab.Controls.Find("newSignButton", false)[0];
            thisSignButton.Show();

            //Change the dimensions of the addendum box
            RichTextBox thisAddendumBox = (RichTextBox)NewNoteTabControl.SelectedTab.Controls.Find("newAddendumBox", false)[0];
            thisAddendumBox.Size = new System.Drawing.Size(720, 283);
            thisAddendumBox.Location = new Point(7, 450);
            thisAddendumBox.Show();

            //Change dimensions of original notebox
            RichTextBox thisOriginalNoteBox = (RichTextBox)NewNoteTabControl.SelectedTab.Controls.Find("newNoteBox", false)[0];
            thisOriginalNoteBox.Size = new System.Drawing.Size(720, 308);
            thisOriginalNoteBox.ReadOnly = true;

            //Add Addendum Label
            Label thisAddendumLabel = (Label)NewNoteTabControl.SelectedTab.Controls.Find("newAddendumLabel", false)[0];
            thisAddendumLabel.Location = new Point(10, 425);
            thisAddendumLabel.Show();

            
        }

        private void ExistingSignButtonClick(object sender, EventArgs e)
        {


            //Get AddendumTextBox Content 
            RichTextBox thisAddendumBox = (RichTextBox)NewNoteTabControl.SelectedTab.Controls.Find("newAddendumBox", false)[0];
            String addendumText = thisAddendumBox.Text;

            //Add it to the Old Note TextBox Content (with provider name name and the current date)

            //TO DO: ADD THE PROVIDER NAME 
            RichTextBox thisOldNoteBox = (RichTextBox)NewNoteTabControl.SelectedTab.Controls.Find("newNoteBox", false)[0];
            _ = thisOldNoteBox.Text;
            thisOldNoteBox.ReadOnly = true;
            thisOldNoteBox.ScrollBars = RichTextBoxScrollBars.Vertical;
            String currentDate = DateTime.Now.ToString();
            thisOldNoteBox.AppendText("\n\n");
            thisOldNoteBox.Font = new Font("Bahnschrift Light", 10F, FontStyle.Regular);
            thisOldNoteBox.AppendText(thisProviderName + " wrote on: " + DateTime.Now.ToString() + "\n");
            thisOldNoteBox.AppendText(addendumText);


            //Restore the Old Note Box the original size
            thisOldNoteBox.Size = new System.Drawing.Size(650, 483);

            //Clear the Addendum TextBox Content 
            thisAddendumBox.Clear();

            //Hide the Addendum TextBox 
            thisAddendumBox.Hide();

            //Hide the Addendum Label
            Label thisAddendumLabel = (Label)NewNoteTabControl.SelectedTab.Controls.Find("newAddendumLabel", false)[0];
            thisAddendumLabel.Hide();


            //Hide the Sign Button 
            Button thisSignButton = (Button)NewNoteTabControl.SelectedTab.Controls.Find("newSignButton", false)[0];
            thisSignButton.Hide();

            //Show the Addendum Button 
            Button thisAddendumButton = (Button)NewNoteTabControl.SelectedTab.Controls.Find("newAddendumButton", false)[0];
            thisAddendumButton.Show();


            Label thisNoteTitleValueLabel = (Label)NewNoteTabControl.SelectedTab.Controls.Find("newTitleValueNameLabel", false)[0];
            Label thisNoteValueDate = (Label)NewNoteTabControl.SelectedTab.Controls.Find("newDateValueLabel", false)[0];

            string noteheader = thisProviderName + " wrote on " + currentDate + "\n";

            try
            {
                doAction.updateNoteDraft(ExistingNoteID, thisNoteTitleValueLabel.Text, thisOldNoteBox.Text, "Signed",
                    DateTime.Today , DateTime.Now , noteheader);
                this.patientNoteTableAdapter.FillByPatientIDIgnoreStatus(this.eMRDatabaseDataSet.PatientNote, thisPatientID);
                thisPatientHome.patientNoteTableAdapter.FillByPatientID(thisPatientHome.eMRDatabaseDataSet.PatientNote, thisPatientID);
                
             


            }
            catch (FieldAccessException)
            {
                MessageBox.Show("Please update the note or exit the page");
            }

            

        }

        private void ExitButtonClick(object sender, EventArgs e)
        {
            //Remove the selected tab when the exit Button is clicked
            NewNoteTabControl.TabPages.Remove(NewNoteTabControl.SelectedTab);

            
        }

        private void Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void SignButton1_Click(object sender, EventArgs e)
        {

        }


        private void CancelButton_Click(object sender, EventArgs e)       
        {
            for (int i = 0; i < NoteHistoryTable.Rows.Count; i++)
            {
                if((String)NoteHistoryTable["Status" , i].Value == "New")
                {
                    doAction.deletePatientNote((int)NoteHistoryTable["ColumnNumber", i].Value,
                        DateTime.Today, thisProviderID);
                }
            }
            this.Close();
        }

        private void SignButton_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(NewNoteTitleBox.Text))  NewNoteTitleBox.Text = "Untitled";

            string noteTitle = NewNoteTitleBox.Text;


            String noteContent ="Subjective: " +  SubjectiveNoteBox.Text + 
                "\n"  + "Objective: " +  ObjectiveNoteBox.Text +
                "\n" +"Assessment: " +  AssesmentBox.Text +
                "\n"  + "Plan: " +  PlanBox.Text;


            try
            {

                string status = "Signed";
                string noteHeader = thisProviderName + " wrote on: " + DateTime.Now.ToString(); 
                doAction.updateNoteDraft(DraftNoteID , noteTitle , noteContent , status, DateTime.Today , DateTime.Now , noteHeader);
                this.patientNoteTableAdapter.FillByPatientIDIgnoreStatus(this.eMRDatabaseDataSet.PatientNote, thisPatientID);
                thisPatientHome.patientNoteTableAdapter.FillByPatientID(thisPatientHome.eMRDatabaseDataSet.PatientNote, thisPatientID);
                
                this.Close();
            }
            catch
            {
                MessageBox.Show("Please enter a valid note entry");
            }
          


           
        }

        private void TabPage1_Click(object sender, EventArgs e)
        {

        }

        private void NewNotePanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void NoteContentBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void SubjectiveBox_Click(object sender, EventArgs e)
        {
            if (SubjectiveNoteBox.Text == "Subjective:") SubjectiveNoteBox.Text = null;
        }

        private void ObjectiveNoteBox_Click(object sender, EventArgs e)
        {
            if (ObjectiveNoteBox.Text == "Objective:") ObjectiveNoteBox.Text = null;
        }

        private void AssesmentBox_Click(object sender, EventArgs e)
        {
            if (AssesmentBox.Text == "Assessment:") AssesmentBox.Text = null;
        }

        private void PlanBox_Click(object sender, EventArgs e)
        {
            if (PlanBox.Text == "Plan:") PlanBox.Text = null;
        }

        private void ObjectiveNoteBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void SaveDraftButton_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(NewNoteTitleBox.Text)) NewNoteTitleBox.Text = "Untitled";
            string noteTitle = NewNoteTitleBox.Text;

            Console.WriteLine("NoteD is " + DraftNoteID);
            Console.WriteLine("NoteTItel:" + noteTitle);


            String noteContent = SubjectiveNoteBox.Text +
                ";" + ObjectiveNoteBox.Text +
                ";" + AssesmentBox.Text +
                ";" + PlanBox.Text + ";";


            try
            {
                string noteheader = thisProviderName + " wrote on: " + DateTime.Now.ToString();
                string status = "Draft";
                doAction.updateNoteDraft(DraftNoteID, noteTitle, noteContent, status, DateTime.Today , DateTime.Now , noteheader);
                this.patientNoteTableAdapter.FillByPatientIDIgnoreStatus(this.eMRDatabaseDataSet.PatientNote, thisPatientID);
                thisPatientHome.patientNoteTableAdapter.FillByPatientID(thisPatientHome.eMRDatabaseDataSet.PatientNote, thisPatientID);



            }
            catch
            {
                MessageBox.Show("Please enter a valid note entry");
            }

        }

        private void DeleteMessageButton_Click(object sender, EventArgs e)
        {
            doAction.deletePatientNote(DraftNoteID, DateTime.Today, thisProviderID);
            this.patientNoteTableAdapter.FillByPatientIDIgnoreStatus(this.eMRDatabaseDataSet.PatientNote, thisPatientID);
            thisPatientHome.patientNoteTableAdapter.FillByPatientID(thisPatientHome.eMRDatabaseDataSet.PatientNote, thisPatientID);
            this.Close();

        }
    }
}
