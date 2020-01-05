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
    public partial class CreateNewSchedulerAccount : Form
    {
        private readonly SiteFunctionsDataContext doAction = new SiteFunctionsDataContext(@"Data Source=shifaaserver.database.windows.net;Initial Catalog=EMRDatabase;Persist Security Info=True;User ID=shifaaAdmin;Password=qalbeefeemasr194!");

        public CreateNewSchedulerAccount()
        {
            InitializeComponent();
            PassCodeBox.UseSystemPasswordChar = true;
            ReenterPasscodeBox.UseSystemPasswordChar = true;
        }

        private void LoginButton_Click(object sender, EventArgs e)
        {

            try
            {

                string gender = null;

                if (FemaleCheckBox.Checked && !MaleCheckBox.Checked) gender = "Female";
                if (!FemaleCheckBox.Checked && MaleCheckBox.Checked) gender = "Male";
                if (FemaleCheckBox.Checked && MaleCheckBox.Checked) MessageBox.Show("Please pick only one gender");


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
                        MessageBox.Show("Please enter a valid phone number in the format\n Ex: +20-2-1234-1234");
                        PhoneNumberBox.Text = null;
                    }
                }

                //TODO: Configure for email validation.
                if (!EmailBox.Text.Contains("@") || !EmailBox.Text.Contains("."))
                {
                    MessageBox.Show("Please enter a valid email account.");
                }

                else
                {
                    int returnValue = doAction.createSchedulerAccount(FirstNameBox.Text, LastNameBox.Text, PhoneNumberBox.Text,
                        EmailBox.Text, gender, UsernameBox.Text, PassCodeBox.Text, ReenterPasscodeBox.Text);

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
                }



            }
            catch
            {
                MessageBox.Show("Please enter a valid username and passcode combination");
            }

        }

        private void CreateNewSchedulerAccount_Load(object sender, EventArgs e)
        {

        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CreateNewSchedulerAccount_Load_1(object sender, EventArgs e)
        {

        }

        private void PhoneNumberBox_TextChanged(object sender, EventArgs e)
        {
            var phoneNumberUtil = PhoneNumbers.PhoneNumberUtil.GetInstance();


            if (!String.IsNullOrWhiteSpace(PhoneNumberBox.Text))
            {
                try
                {

                    //TODO: FORMAT THE NUMBER USING ASYOUTYPE FORMATER
                    phoneNumberUtil.Parse(PhoneNumberBox.Text, null);

                }
                catch (FormatException)
                {
                    MessageBox.Show("Please enter a valid phone number");
                    PhoneNumberBox.Text = null;
                }
            }
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            WelcomeHomePage welcome = new WelcomeHomePage();
            welcome.Show();
        }

        private void UsernameBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void FemaleCheckBox_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void ReenterPasscodeBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void PassCodeBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void MaleCheckBox_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void PassCodeLabel_Click(object sender, EventArgs e)
        {

        }

        private void UsernameLabel_Click(object sender, EventArgs e)
        {

        }
    }
}
