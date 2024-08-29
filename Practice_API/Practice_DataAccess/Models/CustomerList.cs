using System;
using System.Data;
using System.Data.SqlClient;
using Primary2020;
using System.Diagnostics;

	public class CustomerList : PrimaryList
	{
		#region private variables
		private CustomerItem _cusItem;
		#endregion

		#region public properties
		public CustomerItem Item
		{
			set{this._cusItem = value;}
			get{return this._cusItem;}
		}
		#endregion

		#region public enums
		public enum LoadOption
		{
			LoadAll = 1,
			LoadWithNoneSelected = 2,
			LoadById = 3,
			LoadActive = 4
			//LoadOption Next
		}
		#endregion

		#region constructors
		public CustomerList()
		{
			base.ClassName = "CustomerList";
			this._cusItem = new CustomerItem();
		}
		#endregion

		#region override public functions
		public override DataSet GetDataSet(Object lOption)
		{
			AppSettings app = new AppSettings();
			app.LoadSettings();
			SqlConnection cnn = new SqlConnection(app.DBCnnString);
			SqlCommand cmd = this.getLoadCmd((LoadOption)lOption);
			cmd.Connection = cnn;
			cnn.Open();

			DataSet ds = new DataSet();
			SqlDataAdapter adapter = new SqlDataAdapter(cmd);
			adapter.Fill(ds, "tbl");
			cnn.Close();
			return ds;
		}
		#endregion

		#region load code
		public void Load(LoadOption lOption)
		{
			try
			{
				SqlDataReader dr;
				AppSettings app = new AppSettings();
				app.LoadSettings();
				SqlConnection cnn = new SqlConnection(app.DBCnnString);
				SqlCommand cmd = getLoadCmd(lOption);
				cmd.Connection = cnn;
				cnn.Open();
				dr = cmd.ExecuteReader();
				CustomerItem item;

				while (dr.Read())
				{
					System.Collections.ArrayList ar = GetColumnList(dr);
					item = new CustomerItem();
					item.CustomerId = ar.Contains("CustomerId") ? (dr["CustomerId"].Equals(System.DBNull.Value) ? item.CustomerId : long.Parse(dr["CustomerId"].ToString())) : item.CustomerId;
					item.CustomerName = ar.Contains("CustomerName") ? (dr["CustomerName"].ToString()) : item.CustomerName;
					item.Phone = ar.Contains("Phone") ? (dr["Phone"].ToString()) : item.Phone;
					item.Address = ar.Contains("Address") ? (dr["Address"].ToString()) : item.Address;
					item.Deleted = ar.Contains("Deleted") ? (dr["Deleted"].ToString()) : item.Deleted;
					item.CreatedById = ar.Contains("CreatedById") ? (dr["CreatedById"].Equals(System.DBNull.Value) ? item.CreatedById : long.Parse(dr["CreatedById"].ToString())) : item.CreatedById;
					item.CreatedDate = ar.Contains("CreatedDate") ? (dr["CreatedDate"].Equals(System.DBNull.Value) ? item.CreatedDate : DateTime.Parse(dr["CreatedDate"].ToString())) : item.CreatedDate;
					item.UpdatedById = ar.Contains("UpdatedById") ? (dr["UpdatedById"].Equals(System.DBNull.Value) ? item.UpdatedById : long.Parse(dr["UpdatedById"].ToString())) : item.UpdatedById;
					item.UpdatedDate = ar.Contains("UpdatedDate") ? (dr["UpdatedDate"].Equals(System.DBNull.Value) ? item.UpdatedDate : DateTime.Parse(dr["UpdatedDate"].ToString())) : item.UpdatedDate;
					//DataReader NextItem
					base.Add(item);
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
				case LoadOption.LoadAll:
					return getLoadAllCmd(lOption);
				case LoadOption.LoadWithNoneSelected:
					return getLoadAllCmd(lOption);

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
				param2.Value = this.Item.CustomerId;
				cmd.Parameters.Add(param2);

				return cmd;
			}
			catch (Exception ex)
			{
				GSACaughtException.GSACaughtException ce = new GSACaughtException.GSACaughtException(ex, new StackFrame().GetMethod().Name, (long)GSACaughtException.GSACaughtException.GlobalSeverityLevel.HighSeverity);
				throw ce;
			}
		}

		private SqlCommand getLoadAllCmd(LoadOption lOption)
		{
			try
			{
				SqlCommand cmd = new SqlCommand("spGetCustomer");
				cmd.CommandType = CommandType.StoredProcedure;

				SqlParameter param1 = new SqlParameter("@QryOption", SqlDbType.Int);
				param1.Value = lOption;
				cmd.Parameters.Add(param1);

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

		#region save code
		public void Save(CustomerItem.SaveOption sOption)
		{
			try
			{
				foreach(CustomerItem item in this)
				{
					item.Save(sOption);
				}
			}
			catch (Exception ex)
			{
				GSACaughtException.GSACaughtException ce = new GSACaughtException.GSACaughtException(ex, new StackFrame().GetMethod().Name, (long)GSACaughtException.GSACaughtException.GlobalSeverityLevel.HighSeverity);
				throw ce;
			}
		}
		#endregion
	}
