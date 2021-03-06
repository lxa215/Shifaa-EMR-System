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
    public partial class CreateNewProviderAccount : Form
    {
        private static readonly IDbConnection con = new System.Data.SqlClient.SqlConnection(Properties.Settings.Default.EMRDatabaseConnectionString);
        private readonly SiteFunctionsDataContext doAction = new SiteFunctionsDataContext(con);

        public CreateNewProviderAccount()
        {
            InitializeComponent();
            PassCodeBox.UseSystemPasswordChar = true;
            ReenterPasscodeBox.UseSystemPasswordChar = true;
         

        }

        private void LoginButton_Click(object sender, EventArgs e)
        {

            try
            {

                if (String.IsNullOrEmpty(FirstNameBox.Text) || String.IsNullOrEmpty(LastNameBox.Text))
                {
                    MessageBox.Show("The First Name and Last Name fields are required");
                }

                string gender = null;

                if (FemaleCheckBox.Checked && !MaleCheckBox.Checked) gender = "Female";
                if (!FemaleCheckBox.Checked && MaleCheckBox.Checked) gender = "Male";
                if (FemaleCheckBox.Checked && MaleCheckBox.Checked) MessageBox.Show("Please pick only one gender");

                if(String.IsNullOrWhiteSpace(UsernameBox.Text) || String.IsNullOrWhiteSpace(PassCodeBox.Text))
                {
                    MessageBox.Show("The username and password fields are required");
                     return;
                }

                var phoneNumberUtil = PhoneNumbers.PhoneNumberUtil.GetInstance();


                if (!String.IsNullOrWhiteSpace(PhoneNumberBox.Text))
                {
                    try
                    {

                        //TODO: FORMAT THE NUMBER USING ASYOUTYPE FORMATER
                        phoneNumberUtil.Parse(PhoneNumberBox.Text, null);

                    }
                    catch
                    {
                        MessageBox.Show("Please enter a valid phone number");
                        PhoneNumberBox.Text = null;
                        return;
                    }
                }

                if(PassCodeBox.Text.Length < 8)
                {
                    MessageBox.Show("Passcodes must be longer than 8 characters");
                    PassCodeBox.Text = null;
                    return;
                }

                //TODO: Configure for email validation.
                if(!EmailBox.Text.Contains("@") || !EmailBox.Text.Contains("."))
                {
                    MessageBox.Show("Please enter a valid email account.");
                }

                else
                {

                   
   

                    int returnValue = doAction.createProviderAccount(FirstNameBox.Text, LastNameBox.Text,
                        TitleBox.Text, PhoneNumberBox.Text , EmailBox.Text , JobTypeBox.Text, gender, UsernameBox.Text , 
                        PassCodeBox.Text, ReenterPasscodeBox.Text);

                    

                    if (returnValue is 1)
                    {
                        MessageBox.Show("That username already exists. Please try another one");
                    }

                    if (returnValue is 2)
                    {
                        MessageBox.Show("The two passcodes don't match. Please try again");
                    }
                    if (returnValue is 3)
                    {
                        MessageBox.Show("Please pick a different passcode. That passcode already exists");
                    }
                    if (returnValue is 0)
                    {

                        string providerName = FirstNameBox.Text + " " + LastNameBox.Text;
                        foreach (DataGridViewRow data in SchedulerTable.SelectedRows)
                        {
                            doAction.createProviderSchedulerRelation(UsernameBox.Text, providerName, 
                                (String)SchedulerTable[0, data.Index].Value);
                           
                        }

                        this.Close();
                        WelcomeHomePage welcome = new WelcomeHomePage();
                        welcome.Show();

                    }
                }


            }
            catch
            {
                MessageBox.Show("Please enter a valid username and passcode combination");
            }

        }

        private void CreateNewProviderAccount_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'eMRDatabaseDataSet.SchedulerInfo' table. You can move, or remove it, as needed.
            this.schedulerInfoTableAdapter.Fill(this.eMRDatabaseDataSet.SchedulerInfo);
            this.SchedulerTable.MultiSelect = true;
            this.SchedulerTable.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.SchedulerTable.DefaultCellStyle.Font = new Font("Bahnschrift Light", 8);
           


        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void PhoneNumberBox_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            WelcomeHomePage welcome = new WelcomeHomePage();
            welcome.Show();
        }

        private void ChooseSchedulerBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void PassCodeBox_TextChanged(object sender, EventArgs e)
        {

        }
    }
}

