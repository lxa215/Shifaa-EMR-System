﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Shifaa_EMR_System
{
	using System.Data.Linq;
	using System.Data.Linq.Mapping;
	using System.Data;
	using System.Collections.Generic;
	using System.Reflection;
	using System.Linq;
	using System.Linq.Expressions;
	using System.ComponentModel;
	using System;
	
	
	[global::System.Data.Linq.Mapping.DatabaseAttribute(Name="EMRDatabase")]
	public partial class SiteFunctionsDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Extensibility Method Definitions
    partial void OnCreated();
    partial void InsertAppointment(Appointment instance);
    partial void UpdateAppointment(Appointment instance);
    partial void DeleteAppointment(Appointment instance);
    partial void InsertPatient(Patient instance);
    partial void UpdatePatient(Patient instance);
    partial void DeletePatient(Patient instance);
    #endregion
		
		public SiteFunctionsDataContext() : 
				base(global::Shifaa_EMR_System.Properties.Settings.Default.EMRDatabaseConnectionString1, mappingSource)
		{
			OnCreated();
		}
		
		public SiteFunctionsDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public SiteFunctionsDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public SiteFunctionsDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public SiteFunctionsDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public System.Data.Linq.Table<Appointment> Appointments
		{
			get
			{
				return this.GetTable<Appointment>();
			}
		}
		
		public System.Data.Linq.Table<Patient> Patients
		{
			get
			{
				return this.GetTable<Patient>();
			}
		}
		
		[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.CreateAppointment")]
		public int CreateAppointment([global::System.Data.Linq.Mapping.ParameterAttribute(Name="PatientName", DbType="NVarChar(50)")] string patientName, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="Details", DbType="NVarChar(MAX)")] string details, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="DateAppointment", DbType="Date")] System.Nullable<System.DateTime> dateAppointment, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="TimeAppointment", DbType="NVarChar(10)")] string timeAppointment, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="DurationAppointment", DbType="NVarChar(10)")] string durationAppointment)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), patientName, details, dateAppointment, timeAppointment, durationAppointment);
			return ((int)(result.ReturnValue));
		}
		
		[global::System.Data.Linq.Mapping.FunctionAttribute(Name="dbo.createNewPatient")]
		public int createNewPatient([global::System.Data.Linq.Mapping.ParameterAttribute(Name="FirstName", DbType="VarChar(50)")] string firstName, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="LastName", DbType="VarChar(50)")] string lastName, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="DOB", DbType="Date")] System.Nullable<System.DateTime> dOB, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="Age", DbType="Int")] System.Nullable<int> age, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="Gender", DbType="Char(10)")] string gender, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="Weight", DbType="Float")] System.Nullable<double> weight, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="Height", DbType="Float")] System.Nullable<double> height, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="BMI", DbType="Float")] System.Nullable<double> bMI, [global::System.Data.Linq.Mapping.ParameterAttribute(Name="Nationality", DbType="VarChar(50)")] string nationality)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), firstName, lastName, dOB, age, gender, weight, height, bMI, nationality);
			return ((int)(result.ReturnValue));
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.Appointment")]
	public partial class Appointment : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _appID;
		
		private string _PatientName;
		
		private string _Details;
		
		private System.Nullable<System.DateTime> _DateAppointment;
		
		private string _TimeAppointment;
		
		private string _DurationAppointment;
		
		private string _Status;
		
		private string _Created;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnappIDChanging(int value);
    partial void OnappIDChanged();
    partial void OnPatientNameChanging(string value);
    partial void OnPatientNameChanged();
    partial void OnDetailsChanging(string value);
    partial void OnDetailsChanged();
    partial void OnDateAppointmentChanging(System.Nullable<System.DateTime> value);
    partial void OnDateAppointmentChanged();
    partial void OnTimeAppointmentChanging(string value);
    partial void OnTimeAppointmentChanged();
    partial void OnDurationAppointmentChanging(string value);
    partial void OnDurationAppointmentChanged();
    partial void OnStatusChanging(string value);
    partial void OnStatusChanged();
    partial void OnCreatedChanging(string value);
    partial void OnCreatedChanged();
    #endregion
		
		public Appointment()
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_appID", DbType="Int NOT NULL", IsPrimaryKey=true)]
		public int appID
		{
			get
			{
				return this._appID;
			}
			set
			{
				if ((this._appID != value))
				{
					this.OnappIDChanging(value);
					this.SendPropertyChanging();
					this._appID = value;
					this.SendPropertyChanged("appID");
					this.OnappIDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_PatientName", DbType="NVarChar(50)")]
		public string PatientName
		{
			get
			{
				return this._PatientName;
			}
			set
			{
				if ((this._PatientName != value))
				{
					this.OnPatientNameChanging(value);
					this.SendPropertyChanging();
					this._PatientName = value;
					this.SendPropertyChanged("PatientName");
					this.OnPatientNameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Details", DbType="NVarChar(MAX)")]
		public string Details
		{
			get
			{
				return this._Details;
			}
			set
			{
				if ((this._Details != value))
				{
					this.OnDetailsChanging(value);
					this.SendPropertyChanging();
					this._Details = value;
					this.SendPropertyChanged("Details");
					this.OnDetailsChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_DateAppointment", DbType="Date")]
		public System.Nullable<System.DateTime> DateAppointment
		{
			get
			{
				return this._DateAppointment;
			}
			set
			{
				if ((this._DateAppointment != value))
				{
					this.OnDateAppointmentChanging(value);
					this.SendPropertyChanging();
					this._DateAppointment = value;
					this.SendPropertyChanged("DateAppointment");
					this.OnDateAppointmentChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_TimeAppointment", DbType="NVarChar(10)")]
		public string TimeAppointment
		{
			get
			{
				return this._TimeAppointment;
			}
			set
			{
				if ((this._TimeAppointment != value))
				{
					this.OnTimeAppointmentChanging(value);
					this.SendPropertyChanging();
					this._TimeAppointment = value;
					this.SendPropertyChanged("TimeAppointment");
					this.OnTimeAppointmentChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_DurationAppointment", DbType="NVarChar(10)")]
		public string DurationAppointment
		{
			get
			{
				return this._DurationAppointment;
			}
			set
			{
				if ((this._DurationAppointment != value))
				{
					this.OnDurationAppointmentChanging(value);
					this.SendPropertyChanging();
					this._DurationAppointment = value;
					this.SendPropertyChanged("DurationAppointment");
					this.OnDurationAppointmentChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Status", DbType="NVarChar(50)")]
		public string Status
		{
			get
			{
				return this._Status;
			}
			set
			{
				if ((this._Status != value))
				{
					this.OnStatusChanging(value);
					this.SendPropertyChanging();
					this._Status = value;
					this.SendPropertyChanged("Status");
					this.OnStatusChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Created", DbType="NVarChar(50)")]
		public string Created
		{
			get
			{
				return this._Created;
			}
			set
			{
				if ((this._Created != value))
				{
					this.OnCreatedChanging(value);
					this.SendPropertyChanging();
					this._Created = value;
					this.SendPropertyChanged("Created");
					this.OnCreatedChanged();
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.Patient")]
	public partial class Patient : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _PatientID;
		
		private string _FirstName;
		
		private string _LastName;
		
		private System.Nullable<System.DateTime> _DOB;
		
		private string _Age;
		
		private string _Gender;
		
		private System.Nullable<double> _Weight;
		
		private System.Nullable<double> _Height;
		
		private System.Nullable<double> _BMI;
		
		private string _Nationality;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnPatientIDChanging(int value);
    partial void OnPatientIDChanged();
    partial void OnFirstNameChanging(string value);
    partial void OnFirstNameChanged();
    partial void OnLastNameChanging(string value);
    partial void OnLastNameChanged();
    partial void OnDOBChanging(System.Nullable<System.DateTime> value);
    partial void OnDOBChanged();
    partial void OnAgeChanging(string value);
    partial void OnAgeChanged();
    partial void OnGenderChanging(string value);
    partial void OnGenderChanged();
    partial void OnWeightChanging(System.Nullable<double> value);
    partial void OnWeightChanged();
    partial void OnHeightChanging(System.Nullable<double> value);
    partial void OnHeightChanged();
    partial void OnBMIChanging(System.Nullable<double> value);
    partial void OnBMIChanged();
    partial void OnNationalityChanging(string value);
    partial void OnNationalityChanged();
    #endregion
		
		public Patient()
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_PatientID", DbType="Int NOT NULL", IsPrimaryKey=true)]
		public int PatientID
		{
			get
			{
				return this._PatientID;
			}
			set
			{
				if ((this._PatientID != value))
				{
					this.OnPatientIDChanging(value);
					this.SendPropertyChanging();
					this._PatientID = value;
					this.SendPropertyChanged("PatientID");
					this.OnPatientIDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_FirstName", DbType="VarChar(50)")]
		public string FirstName
		{
			get
			{
				return this._FirstName;
			}
			set
			{
				if ((this._FirstName != value))
				{
					this.OnFirstNameChanging(value);
					this.SendPropertyChanging();
					this._FirstName = value;
					this.SendPropertyChanged("FirstName");
					this.OnFirstNameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_LastName", DbType="VarChar(50)")]
		public string LastName
		{
			get
			{
				return this._LastName;
			}
			set
			{
				if ((this._LastName != value))
				{
					this.OnLastNameChanging(value);
					this.SendPropertyChanging();
					this._LastName = value;
					this.SendPropertyChanged("LastName");
					this.OnLastNameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_DOB", DbType="Date")]
		public System.Nullable<System.DateTime> DOB
		{
			get
			{
				return this._DOB;
			}
			set
			{
				if ((this._DOB != value))
				{
					this.OnDOBChanging(value);
					this.SendPropertyChanging();
					this._DOB = value;
					this.SendPropertyChanged("DOB");
					this.OnDOBChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Age", DbType="NChar(10)")]
		public string Age
		{
			get
			{
				return this._Age;
			}
			set
			{
				if ((this._Age != value))
				{
					this.OnAgeChanging(value);
					this.SendPropertyChanging();
					this._Age = value;
					this.SendPropertyChanged("Age");
					this.OnAgeChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Gender", DbType="Char(10)")]
		public string Gender
		{
			get
			{
				return this._Gender;
			}
			set
			{
				if ((this._Gender != value))
				{
					this.OnGenderChanging(value);
					this.SendPropertyChanging();
					this._Gender = value;
					this.SendPropertyChanged("Gender");
					this.OnGenderChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Weight", DbType="Float")]
		public System.Nullable<double> Weight
		{
			get
			{
				return this._Weight;
			}
			set
			{
				if ((this._Weight != value))
				{
					this.OnWeightChanging(value);
					this.SendPropertyChanging();
					this._Weight = value;
					this.SendPropertyChanged("Weight");
					this.OnWeightChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Height", DbType="Float")]
		public System.Nullable<double> Height
		{
			get
			{
				return this._Height;
			}
			set
			{
				if ((this._Height != value))
				{
					this.OnHeightChanging(value);
					this.SendPropertyChanging();
					this._Height = value;
					this.SendPropertyChanged("Height");
					this.OnHeightChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_BMI", DbType="Float")]
		public System.Nullable<double> BMI
		{
			get
			{
				return this._BMI;
			}
			set
			{
				if ((this._BMI != value))
				{
					this.OnBMIChanging(value);
					this.SendPropertyChanging();
					this._BMI = value;
					this.SendPropertyChanged("BMI");
					this.OnBMIChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Nationality", DbType="VarChar(50)")]
		public string Nationality
		{
			get
			{
				return this._Nationality;
			}
			set
			{
				if ((this._Nationality != value))
				{
					this.OnNationalityChanging(value);
					this.SendPropertyChanging();
					this._Nationality = value;
					this.SendPropertyChanged("Nationality");
					this.OnNationalityChanged();
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
}
#pragma warning restore 1591
