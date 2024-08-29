using System;
using System.Data;
using System.Data.SqlClient;
using Primary2020;
using System.Diagnostics;

	public class CustomerItem : PrimaryItem
	{
		#region private variables
		private long _lngCustomerId;
		private string _strCustomerName;
		private string _strPhone;
		private string _strAddress;
		private string _strDeleted;
		private long _lngCreatedById;
		private DateTime _dtCreatedDate;
		private long _lngUpdatedById;
		private DateTime _dtUpdatedDate;
		//PrivateVariables Next
		#endregion

		#region public properties
		public long CustomerId
		{
			get { return _lngCustomerId; }
			set { _lngCustomerId = value; }
		}
		public string CustomerName
		{
			get { return _strCustomerName; }
			set { _strCustomerName = value; }
		}
		public string Phone
		{
			get { return _strPhone; }
			set { _strPhone = value; }
		}
		public string Address
		{
			get { return _strAddress; }
			set { _strAddress = value; }
		}
		public string Deleted
		{
			get { return _strDeleted; }
			set { _strDeleted = value; }
		}
		public long CreatedById
		{
			get { return _lngCreatedById; }
			set { _lngCreatedById = value; }
		}
		public DateTime CreatedDate
		{
			get { return _dtCreatedDate; }
			set { _dtCreatedDate = value; }
		}
		public long UpdatedById
		{
			get { return _lngUpdatedById; }
			set { _lngUpdatedById = value; }
		}
		public DateTime UpdatedDate
		{
			get { return _dtUpdatedDate; }
			set { _dtUpdatedDate = value; }
		}
		//PublicProperties Next
		#endregion

		#region public enums
		public enum SaveOption
		{
			SaveRow = 1,
			DeleteRow = 2
			//SaveOption Next
		}
		public enum LoadOption
		{
			LoadAll = 1,
			LoadWithNoneSelected = 2,
			LoadById = 3
			//LoadOption Next
		}
		#endregion

		#region constructors
		public CustomerItem()
		{
			base.ClassName = "CustomerItem";
		}
		#endregion

		#region save code
		public void Save(SaveOption sOption)
		{
			try
			{
				switch (sOption)
				{
					case SaveOption.SaveRow:
						SqlCommand cmd = getSaveRowCmd(sOption);
						this.CustomerId = SaveItem(cmd, true);
						break;

					case SaveOption.DeleteRow:
						SqlCommand cmdDelete = getDeleteRowCmd(sOption);
						this.CustomerId = SaveItem(cmdDelete, true);
						break;

						//SaveCase Next1
					default:
						break;
				}
			}
			catch (Exception ex)
			{
				GSACaughtException.GSACaughtException ce = new GSACaughtException.GSACaughtException(ex, new StackFrame().GetMethod().Name, (long)GSACaughtException.GSACaughtException.GlobalSeverityLevel.HighSeverity);
				throw ce;
			}
		}

		public void Save(SaveOption sOption, ref long id)
		{
			try
			{
				switch (sOption)
				{
					case SaveOption.SaveRow:
						SqlCommand cmd = getSaveRowCmd(sOption);
						id = SaveItem(cmd, true);
						break;

						//SaveCase Next2
					default:
						break;
				}
			}
			catch (Exception ex)
			{
				GSACaughtException.GSACaughtException ce = new GSACaughtException.GSACaughtException(ex, new StackFrame().GetMethod().Name, (long)GSACaughtException.GSACaughtException.GlobalSeverityLevel.HighSeverity);
				throw ce;
			}
		}

		public void Save(SaveOption sOption, SqlConnection cnn)
		{
			try
			{
				switch (sOption)
				{
					case SaveOption.SaveRow:
						SqlCommand cmd = getSaveRowCmd(sOption);
						this.CustomerId = SaveItem(cnn, cmd, true);
						break;

						//SaveCase Next3
					default:
						break;
				}
			}
			catch (Exception ex)
			{
				GSACaughtException.GSACaughtException ce = new GSACaughtException.GSACaughtException(ex, new StackFrame().GetMethod().Name, (long)GSACaughtException.GSACaughtException.GlobalSeverityLevel.HighSeverity);
				throw ce;
			}
		}

		public override void Save(object sOption, ref long id)
		{
			this.Save((SaveOption)sOption, ref id);
		}

		private SqlCommand getSaveRowCmd(SaveOption sOption)
		{
			try
			{
				SqlCommand cmd = new SqlCommand("spSetCustomer");
				cmd.CommandType = CommandType.StoredProcedure;

				SqlParameter param1 = new SqlParameter("@SaveOption", SqlDbType.Int);
				param1.Value = sOption;
				cmd.Parameters.Add(param1);

				SqlParameter param2 = new SqlParameter("@CustomerId", SqlDbType.Int);
				param2.Value = this.@CustomerId;
				cmd.Parameters.Add(param2);

				SqlParameter param3 = new SqlParameter("@CustomerName", SqlDbType.VarChar);
				param3.Value = this.@CustomerName;
				cmd.Parameters.Add(param3);

				SqlParameter param4 = new SqlParameter("@Phone", SqlDbType.VarChar);
				param4.Value = this.@Phone;
				cmd.Parameters.Add(param4);

				SqlParameter param5 = new SqlParameter("@Address", SqlDbType.VarChar);
				param5.Value = this.@Address;
				cmd.Parameters.Add(param5);

				SqlParameter param6 = new SqlParameter("@Deleted", SqlDbType.VarChar);
				param6.Value = this.@Deleted;
				cmd.Parameters.Add(param6);

				SqlParameter param7 = new SqlParameter("@CreatedById", SqlDbType.Int);
				param7.Value = this.@CreatedById;
				cmd.Parameters.Add(param7);

				SqlParameter param8 = new SqlParameter("@CreatedDate", SqlDbType.DateTime);
				param8.Value = this.@CreatedDate.Equals(DateTime.Parse("1/1/0001")) ? DateTime.Parse("1/1/1900") : this.CreatedDate;
				cmd.Parameters.Add(param8);

				SqlParameter param9 = new SqlParameter("@UpdatedById", SqlDbType.Int);
				param9.Value = this.@UpdatedById;
				cmd.Parameters.Add(param9);

				SqlParameter param10 = new SqlParameter("@UpdatedDate", SqlDbType.DateTime);
				param10.Value = this.@UpdatedDate.Equals(DateTime.Parse("1/1/0001")) ? DateTime.Parse("1/1/1900") : this.UpdatedDate;
				cmd.Parameters.Add(param10);

				//CmdParameters Next
				SqlParameter paramIdentityValue = new SqlParameter("@IdentityValue", SqlDbType.Int, 0, ParameterDirection.Output, false, 0, 0, "", DataRowVersion.Default, null);
				cmd.Parameters.Add(paramIdentityValue);

				return cmd;
			}
			catch (Exception ex)
			{
				GSACaughtException.GSACaughtException ce = new GSACaughtException.GSACaughtException(ex, new StackFrame().GetMethod().Name, (long)GSACaughtException.GSACaughtException.GlobalSeverityLevel.HighSeverity);
				throw ce;
			}
		}

		private SqlCommand getDeleteRowCmd(SaveOption sOption)
		{
			try
			{
				SqlCommand cmd = new SqlCommand("spSetCustomer");
				cmd.CommandType = CommandType.StoredProcedure;

				SqlParameter param1 = new SqlParameter("@SaveOption", SqlDbType.Int);
				param1.Value = sOption;
				cmd.Parameters.Add(param1);

				SqlParameter param2 = new SqlParameter("@CustomerId", SqlDbType.Int);
				param2.Value = this.CustomerId;
				cmd.Parameters.Add(param2);

				SqlParameter paramIdentityValue = new SqlParameter("@IdentityValue", SqlDbType.Int, 0, ParameterDirection.Output, false, 0, 0, "", DataRowVersion.Default, null);
				cmd.Parameters.Add(paramIdentityValue);

				return cmd;
			}
			catch (Exception ex)
			{
				GSACaughtException.GSACaughtException ce = new GSACaughtException.GSACaughtException(ex, new StackFrame().GetMethod().Name, (long)GSACaughtException.GSACaughtException.GlobalSeverityLevel.HighSeverity);
				throw ce;
			}
		}

		//SaveCommand Next
		#endregion

		#region load code
		public void Load(LoadOption lOption)
		{
			try
			{
				this.Found = false;
				SqlDataReader dr;
				AppSettings app = new AppSettings();
				app.LoadSettings();
				SqlConnection cnn = new SqlConnection(app.DBCnnString);
				SqlCommand cmd = getLoadCmd(lOption);
				cmd.Connection = cnn;
				cnn.Open();
				dr = cmd.ExecuteReader();

				if (dr.Read())
				{
					System.Collections.ArrayList ar = GetColumnList(dr);
					this.Found = true;
					this.CustomerId = ar.Contains("CustomerId") ? (dr["CustomerId"].Equals(System.DBNull.Value) ? this.CustomerId : long.Parse(dr["CustomerId"].ToString())) : this.CustomerId;
					this.CustomerName = ar.Contains("CustomerName") ? (dr["CustomerName"].ToString()) : this.CustomerName;
					this.Phone = ar.Contains("Phone") ? (dr["Phone"].ToString()) : this.Phone;
					this.Address = ar.Contains("Address") ? (dr["Address"].ToString()) : this.Address;
					this.Deleted = ar.Contains("Deleted") ? (dr["Deleted"].ToString()) : this.Deleted;
					this.CreatedById = ar.Contains("CreatedById") ? (dr["CreatedById"].Equals(System.DBNull.Value) ? this.CreatedById : long.Parse(dr["CreatedById"].ToString())) : this.CreatedById;
					this.CreatedDate = ar.Contains("CreatedDate") ? (dr["CreatedDate"].Equals(System.DBNull.Value) ? this.CreatedDate : DateTime.Parse(dr["CreatedDate"].ToString())) : this.CreatedDate;
					this.UpdatedById = ar.Contains("UpdatedById") ? (dr["UpdatedById"].Equals(System.DBNull.Value) ? this.UpdatedById : long.Parse(dr["UpdatedById"].ToString())) : this.UpdatedById;
					this.UpdatedDate = ar.Contains("UpdatedDate") ? (dr["UpdatedDate"].Equals(System.DBNull.Value) ? this.UpdatedDate : DateTime.Parse(dr["UpdatedDate"].ToString())) : this.UpdatedDate;
					//DataReader NextItem
				}

				dr.Close();
				dr = null;
				cnn.Close();
				cmd = null;
				cnn = null;
			}
			catch (Exception ex)
			{
				GSACaughtException.GSACaughtException ce = new GSACaughtException.GSACaughtException(ex, new StackFrame().GetMethod().Name, (long)GSACaughtException.GSACaughtException.GlobalSeverityLevel.HighSeverity);
				throw ce;
			}
		}

		private static System.Collections.ArrayList GetColumnList(SqlDataReader dr)
		{
			System.Collections.ArrayList ar = new System.Collections.ArrayList();
			for (int i = 0; i < dr.FieldCount; i++)
			{
				ar.Add(dr.GetName(i));
			}
			return ar;
		}

		private SqlCommand getLoadCmd(LoadOption lOption)
		{
			switch (lOption)
			{
				case LoadOption.LoadById:
					return getLoadByIdCmd(lOption);

					//LoadCase Next
				default:
					return null;
			}
		}

		private SqlCommand getLoadByIdCmd(LoadOption lOption)
		{
			try
			{
				SqlCommand cmd = new SqlCommand("spGetCustomer");
				cmd.CommandType = CommandType.StoredProcedure;

				SqlParameter param1 = new SqlParameter("@QryOption", SqlDbType.Int);
				param1.Value = lOption;
				cmd.Parameters.Add(param1);

				SqlParameter param2 = new SqlParameter("@CustomerId", SqlDbType.Int);
				param2.Value = this.CustomerId;
				cmd.Parameters.Add(param2);

				return cmd;
			}
			catch (Exception ex)
			{
				GSACaughtException.GSACaughtException ce = new GSACaughtException.GSACaughtException(ex, new StackFrame().GetMethod().Name, (long)GSACaughtException.GSACaughtException.GlobalSeverityLevel.HighSeverity);
				throw ce;
			}
		}

		//LoadCommand Next
		#endregion
	}
