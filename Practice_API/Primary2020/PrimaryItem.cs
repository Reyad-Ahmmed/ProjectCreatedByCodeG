using System;
using System.Reflection;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Text;
namespace Primary2020
{
    /// <summary>
    /// This class should be inherited by all item classes
    /// It uses reflection to assign and get values from properties
    /// </summary>
    public class PrimaryItem
    {
        #region private variables
        private string _strClassName;
        private string _strAppName;
        private bool _blnFound;
        #endregion

        #region public properties
        public string ClassName
        {
            get { return _strClassName; }
            set { _strClassName = value; }
        }
        public string AppName
        {
            get { return _strAppName; }
            set { _strAppName = value; }
        }
        public bool Found
        {
            get { return _blnFound; }
            set { _blnFound = value; }
        }
        #endregion

        #region public cinstructors
        public PrimaryItem()
        {
            AppSettings app = new AppSettings();
            app.LoadSettings();
            this.AppName = app.AppName;
        }
        public PrimaryItem(string cName)
            : this()
        {
            this.ClassName = cName;
        }
        public PrimaryItem(string cName, string aName)
        {
            this.ClassName = cName;
            this.AppName = aName;
        }
        #endregion

        #region property reflection code
        public void AssignPropertyValue(string propertyName, object propertyValue)
        {
            try
            {
                PropertyInfo property = this.GetType().GetProperty(propertyName);
                Type type = Type.GetType(Convert.ToString(property.PropertyType));
                switch (Convert.ToString(property.PropertyType))
                {
                    case "System.DateTime":
                        if (Convert.ToString(propertyValue) == "")
                        {
                            propertyValue = "1/1/1900";
                        }
                        property.SetValue(this, Convert.ToDateTime(propertyValue), null);
                        break;
                    case "System.String":
                        property.SetValue(this, Convert.ToString(propertyValue), null);
                        break;
                    case "System.Int32":
                        property.SetValue(this, Convert.ToInt32(propertyValue), null);
                        break;
                    case "System.Int64":
                        property.SetValue(this, Convert.ToInt64(propertyValue), null);
                        break;
                    case "System.Double":
                        property.SetValue(this, Convert.ToDouble(propertyValue), null);
                        break;
                    case "System.Decimal":
                        property.SetValue(this, Convert.ToDecimal(propertyValue), null);
                        break;
                    case "System.Boolean":
                        property.SetValue(this, Convert.ToBoolean(CommonFunctions.ConvertToBoolean(propertyValue.ToString())), null);
                        break;
                    case "System.Single":
                        property.SetValue(this, Convert.ToSingle(propertyValue), null);
                        break;                      
                    case "System":
                        property.SetValue(this, Convert.ToBoolean(CommonFunctions.ConvertToBoolean(propertyValue.ToString())), null);
                        break;
                    default:
                        break;
                }
            }
            catch (CaughtException ce)
            {
                throw ce;
            }
            catch (Exception ex)
            {
                string err = "PropertyName = " + propertyName + " PropertyValue = " + propertyValue.ToString();
                err = err + System.Environment.NewLine + " " + ex.Message;
                CaughtException ce = new CaughtException(err, this, "AssignPropertyValue");
                throw ce;
            }
        }
        public object GetPropertyValue(string propertyName, ref string propertyType)
        {
            try
            {
                if (propertyName != "")
                {
                    PropertyInfo property = this.GetType().GetProperty(propertyName);
                    propertyType = property.GetType().FullName;
                    return property.GetValue(this, null);
                }
                else
                {
                    return "";
                }
            }
            catch (CaughtException ce)
            {
                throw ce;
            }
            catch (Exception ex)
            {
                CaughtException ce = new CaughtException(ex, this, "GetPropertyValue");
                throw ce;
            }
        }
        public object GetPropertyValue(string propertyName)
        {
            try
            {
                if ((propertyName != "") && (propertyName != null))
                {
                    string[] arrPropertyName = propertyName.Split('.');
                    if (arrPropertyName.Length == 1)
                    {
                        PropertyInfo property = this.GetType().GetProperty(propertyName);
                        return property.GetValue(this, null);
                    }
                    else
                    {
                        //this code is used when an object contains another object
                        //the propertname  is then prop1.prop2...and so on
                        //the code returns the propert value of the last item in
                        //the array of property names
                        object objProp = this;
                        int iCounter = 0;
                        foreach (string propName in arrPropertyName)
                        {
                            iCounter++; //increment counter. it is used
                            PropertyInfo property = objProp.GetType().GetProperty(propName);

                            if (iCounter == arrPropertyName.Length)
                            {
                                return property.GetValue(objProp, null);
                            }
                            objProp = property.GetValue(objProp, null);
                        }
                        string msg = "Invalid property name.";
                        CaughtException ce = new CaughtException(msg, this, "GetPropertyValue");
                        throw ce;
                    }
                }
                else
                {
                    return "";
                }
            }
            catch (CaughtException ce)
            {
                throw ce;
            }
            catch (Exception ex)
            {
                CaughtException ce = new CaughtException(ex, this, "GetPropertyValue");
                throw ce;
            }
        }
        #endregion

        #region save code
        protected long SaveItem(SqlConnection cnn, SqlCommand cmd, bool returnId)
        {
            try
            {
                cmd.Connection = cnn;
                //execute the query
                cmd.ExecuteNonQuery();
                long id;
                if (returnId)
                {
                    id = Convert.ToInt32(cmd.Parameters["@IdentityValue"].Value);
                }
                else
                {
                    id = 0;
                }
                //close objects
                cmd.Dispose();
                cmd = null;
                return id;
            }
            catch (Exception ex)
            {
                CaughtException ce = new CaughtException(ex, this, "SaveItem");
                throw ce;
            }
        }

        protected long SaveItem(SqlCommand cmd, bool returnId)
        {
            try
            {
                AppSettings app = new AppSettings();
                app.LoadSettings();
                SqlConnection cnn = new SqlConnection(app.DBCnnString);
                cnn.Open();
                cmd.Connection = cnn;
                //execute the query
                cmd.ExecuteNonQuery();
                long id;
                if (returnId)
                {
                    id = cmd.Parameters["@IdentityValue"].Value == DBNull.Value? 0 : Convert.ToInt32(cmd.Parameters["@IdentityValue"].Value);
                }
                else
                {
                    id = 0;
                }
                //close objects
                cnn.Close();
                cmd.Dispose();
                cmd = null;
                return id;
            }
            catch (Exception ex)
            {
                CaughtException ce = new CaughtException(ex, this, "SaveItem");
                throw ce;
            }
        }

        public virtual void Save(object sOption, ref long id)
        {

        }
        public virtual void Save(object sOption)
        { }


        #endregion

        #region load code
        public virtual void Load(object lOption)
        {

        }
        #endregion

        #region datasets
        public virtual DataSet GetDataSet(object loption)
        {
            return null;
        }
        #endregion

        #region validation code
        /// <summary>
        /// sbm - September 2nd, 2005
        /// Used to make sure that the field is of a valid data type.
        /// </summary>
        /// <param name="strType"></param>
        /// <param name="strValue"></param>
        /// <param name="strFieldName"></param>
        /// <returns></returns>
        public string ValidateFieldType(string strType, string strValue, string strFieldName)
        {
            try
            {
                switch (strType)
                {
                    case "long":
                        int i = Convert.ToInt32(strValue);
                        break;
                    case "float":
                        float f = Convert.ToSingle(strValue);
                        break;
                    case "Decimal":
                        Decimal d = Convert.ToDecimal(strValue);
                        break;
                    case "bool":
                        bool b = Convert.ToBoolean(strValue);
                        break;
                    case "string":
                        string str = Convert.ToString(strValue);
                        break;
                    case "DateTime":
                        if (strValue.Trim() == "")
                        {
                            strValue = "1/1/1900";
                        }
                        DateTime dt = Convert.ToDateTime(strValue);
                        break;
                    default:
                        break;
                }
                return strValue;
            }
            catch (Exception)
            {
                CaughtException ce = new CaughtException();
                ce.ErrorMessage = "Invalid data type. Field: " + strFieldName + ". Expected " + strType + " value type.";
                ce.SetAttributes("ValidateFieldType", this.AppName, "ValidateFieldType");
                throw ce;
            }
        }
        #endregion



    }
}
