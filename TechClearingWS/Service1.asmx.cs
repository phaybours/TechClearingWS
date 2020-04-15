using GenericDapper.Model;
using SwiftMessageMAN.Models;
using Sybase.Data.AseClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Services;
using TechClearingProject.Data.ServiceUtility;

namespace TechClearingWS
{
	/// <summary>
	/// Summary description for Service1
	/// </summary>
	[WebService(Namespace = "http://tempuri.org/")]
	[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
	[System.ComponentModel.ToolboxItem(false)]
	// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
	// [System.Web.Script.Services.ScriptService]
	public class Service1 : System.Web.Services.WebService
	{

        public DataSet SqlDsTest(string SqlString, string TableName, List<AseParameter> parameterPasses, int i)
        {
            DataSet ds = new DataSet();
            TechClearingContext ctx = new TechClearingContext();

            var de = ctx.admConnectionSetups.Where(x => x.Name == "sybconnectionEncrypt").FirstOrDefault();

            if (de != null)
            {

                //details de = d.GetDetails();

                string connstring = System.Configuration.ConfigurationManager.ConnectionStrings["sybconnectionEncrypt"].ToString();

                connstring = connstring.Replace("{{Data Source}}", de.DatabaseServer);

                connstring = connstring.Replace("{{port}}", de.DatabasePort.ToString());

                connstring = connstring.Replace("{{database}}", de.Databasename);

                connstring = connstring.Replace("{{uid}}", de.DatabaseUserName);

                //byte[] encData_byte = new byte[de.DatabasePassword.Length];
                //encData_byte = System.Text.Encoding.UTF8.GetBytes(de.DatabasePassword);
                //string encodedData = Convert.ToBase64String(encData_byte);


                ////Decrypt Password

                //////parse base64 string
                ////byte[] data = Convert.FromBase64String(de.DatabasePassword);

                //////decrypt data
                ////byte[] decrypted = ProtectedData.Unprotect(data, null, Scope);
                ////string pwd = Encoding.Unicode.GetString(decrypted);

                System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();
                System.Text.Decoder utf8Decode = encoder.GetDecoder();
                byte[] todecode_byte = Convert.FromBase64String(de.DatabasePassword);
                int charCount = utf8Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
                char[] decoded_char = new char[charCount];
                utf8Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
                string result = new String(decoded_char);

                connstring = connstring.Replace("{{pwd}}", result);



                using (AseConnection theCons = new AseConnection(connstring))
                {


                    theCons.Open();
                    // d.SmartObject.SaveLog("EConnection Opened Sucessfully");
                    try
                    {

                        AseCommand cmd = new AseCommand();
                        cmd.Connection = theCons;

                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.Text;//i == 0 ? CommandType.StoredProcedure : CommandType.Text;
                        //if (parameterPasses != null)
                        //  cmd.Parameters.AddRange(parameterPasses.ToArray());
                        string qry = string.Empty;
                        List<string> sep = new List<string>();
                        sep.Add("AnsiString");// DateTime";
                        sep.Add("Date");
                        string fdf = string.Empty;
                        if (parameterPasses != null)
                        {
                            foreach (var t in parameterPasses.ToArray())
                            {
                                //sep=if t.DbType in () then "'" else ""
                                //sep = string.Empty;
                                fdf = string.Empty;
                                if (sep.Contains(t.DbType.ToString()))
                                {
                                    fdf = "'" + t.Value.ToString() + "',";
                                }
                                else
                                {
                                    if (string.IsNullOrEmpty(t.Value.ToString()) || t.Value.ToString() == "0")
                                    {
                                        fdf = "NULL,";
                                    }
                                    else
                                    {
                                        fdf = t.Value.ToString() + ",";
                                    }
                                }//+ "'," : t.Value.ToString().Contains("''") ? "NULL": t.Value.ToString()   + ",";

                                qry += t.ToString() + "=" + fdf;

                            }

                            cmd.CommandText = SqlString.Replace("''", "NULL") + " " + qry.TrimEnd(',');
                            //cmd.CommandText.Replace("''", "NULL");
                            // d.SmartObject.SaveLog("Parameter is not null " + cmd.CommandText);
                        }
                        else
                        {
                            cmd.CommandText = SqlString;
                            // d.SmartObject.SaveLog("Parameter is null " + cmd.CommandText);
                        }

                        //  d.SmartObject.SaveLog("Before Execution");

                        AseDataAdapter adapter = null;
                        adapter = new AseDataAdapter(cmd);
                        adapter.Fill(ds);
                        //adapter.Fill(ds);
                        //AseDataReader reader = cmd.ExecuteReader();
                        //SqlDataAdapter
                        //ds.Load(reader, LoadOption.OverwriteChanges, TableName);

                        // d.SmartObject.SaveLog("Reader Read Sucessfully");
                        //reader.Close();
                        theCons.Close();

                    }
                    catch (Exception ex)
                    {
                        // d.SmartObject.SaveLog("Exception from Procedure call " + ex.Message);
                    }

                    return ds;
                }
            }
            return ds;
        }


		public DataSet SqlDs(string SqlString, string TableName, List<AseParameter> parameterPasses)
		{

            //DataFields d = new DataFields();

            DataSet ds = new DataSet();

            TechClearingContext ctx = new TechClearingContext();

            var de = ctx.admConnectionSetups.Where(x => x.Name == "sybconnectionEncrypt").FirstOrDefault();

            if (de != null)
            {

                //details de = d.GetDetails();

                string connstring = System.Configuration.ConfigurationManager.ConnectionStrings["sybconnectionEncrypt"].ToString();

                connstring = connstring.Replace("{{Data Source}}", de.DatabaseServer);

                connstring = connstring.Replace("{{port}}", de.DatabasePort.ToString());

                connstring = connstring.Replace("{{database}}", de.Databasename);

                connstring = connstring.Replace("{{uid}}", de.DatabaseUserName);

                //byte[] encData_byte = new byte[de.DatabasePassword.Length];
                //encData_byte = System.Text.Encoding.UTF8.GetBytes(de.DatabasePassword);
                //string encodedData = Convert.ToBase64String(encData_byte);


                ////Decrypt Password

                //////parse base64 string
                ////byte[] data = Convert.FromBase64String(de.DatabasePassword);

                //////decrypt data
                ////byte[] decrypted = ProtectedData.Unprotect(data, null, Scope);
                ////string pwd = Encoding.Unicode.GetString(decrypted);

                System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();
                System.Text.Decoder utf8Decode = encoder.GetDecoder();
                byte[] todecode_byte = Convert.FromBase64String(de.DatabasePassword);
                int charCount = utf8Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
                char[] decoded_char = new char[charCount];
                utf8Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
                string result = new String(decoded_char);

                connstring = connstring.Replace("{{pwd}}", result);

                using (AseConnection theCons = new AseConnection(connstring))
                {


                    theCons.Open();

                    //SmartObject.SaveLog("EConnection Opened Sucessfully");

                    try
                    {



                        AseCommand cmd = new AseCommand();

                        cmd.Connection = theCons;



                        cmd.CommandTimeout = 0;

                        cmd.CommandType = CommandType.Text;//i == 0 ? CommandType.StoredProcedure : CommandType.Text;

                        //if (parameterPasses != null)

                        //  cmd.Parameters.AddRange(parameterPasses.ToArray());

                        string qry = string.Empty;

                        List<string> sep = new List<string>();

                        sep.Add("AnsiString");// DateTime";

                        sep.Add("Date");

                        //sep.Add("Byte");

                        //sep.Add("Int32");

                        string fdf = string.Empty;

                        if (parameterPasses != null)
                        {

                            foreach (var t in parameterPasses.ToArray())
                            {

                                //sep=if t.DbType in () then "'" else ""

                                //sep = string.Empty;

                                fdf = string.Empty;

                                if (sep.Contains(t.DbType.ToString()))
                                {
                                    if (t.Value == null)
                                    {
                                        fdf = "NULL,";
                                    }
                                    else if (t.Value.ToString() == "")
                                    {
                                        fdf = "NULL,";
                                    }
                                    else
                                    {
                                        fdf = "'" + t.Value.ToString() + "',";
                                    }

                                }

                                else
                                {

                                    if (t.Value == null)
                                    {

                                        fdf = "NULL,";

                                    }
                                    else if (t.Value == null)
                                    {
                                        fdf = "NULL,";
                                    }
                                    else if (t.Value.ToString() == "")
                                    {
                                        fdf = "NULL,";
                                    }

                                    else
                                    {

                                        fdf = t.Value.ToString() + ",";

                                    }

                                }



                                qry += t.ToString() + "=" + fdf;



                            }



                            cmd.CommandText = SqlString + " " + qry.TrimEnd(',');

                            //SmartObject.SaveLog("Procedure call " + cmd.CommandText);

                        }

                        else
                        {

                            cmd.CommandText = SqlString;

                            //SmartObject.SaveLog("Parameter is null " + cmd.CommandText);

                        }



                        //SmartObject.SaveLog("Before Execution");



                        IDataReader reader = cmd.ExecuteReader();



                        ds.Load(reader, LoadOption.OverwriteChanges, TableName);

                        //if (reader.RecordsAffected > 0)
                        //{

                        //    SmartObject.SaveLog("Reader Does have rows");

                        //}

                        //else
                        //{

                        //    SmartObject.SaveLog("Reader Doesnt have rows");

                        //}



                        //SmartObject.SaveLog("Reader Read Sucessfully");

                        reader.Close();

                        theCons.Close();



                    }

                    catch (Exception ex)
                    {

                        SmartObject.SaveLog("Exception from Procedure call " + ex.Message);

                    }


                }
            }


            return ds;

        }




		public DataSet SqlDsAuthUsername(string SqlString, string TableName, List<AseParameter> parameterPasses,string ConnectionString)
		{

			//DataFields d = new DataFields();

			//details de = d.GetDetails();



			//connstring = connstring.Replace("{{Data Source}}", de.server);

			//connstring = connstring.Replace("{{port}}", de.port.ToString());

			//connstring = connstring.Replace("{{database}}", de.databasename);

			//connstring = connstring.Replace("{{uid}}", de.userid);

			//connstring = connstring.Replace("{{pwd}}", de.password);

			using (AseConnection theCons = new AseConnection(ConnectionString))
			{

				DataSet ds = new DataSet();



				theCons.Open();

				//SmartObject.SaveLog("EConnection Opened Sucessfully");

				try
				{



					AseCommand cmd = new AseCommand();

					cmd.Connection = theCons;



					cmd.CommandTimeout = 0;

					cmd.CommandType = CommandType.Text;//i == 0 ? CommandType.StoredProcedure : CommandType.Text;

					//if (parameterPasses != null)

					//  cmd.Parameters.AddRange(parameterPasses.ToArray());

					string qry = string.Empty;

					List<string> sep = new List<string>();

					sep.Add("AnsiString");// DateTime";

					sep.Add("Date");

					//sep.Add("Byte");

					//sep.Add("Int32");

					string fdf = string.Empty;

					if (parameterPasses != null)
					{

						foreach (var t in parameterPasses.ToArray())
						{

							//sep=if t.DbType in () then "'" else ""

							//sep = string.Empty;

							fdf = string.Empty;

							if (sep.Contains(t.DbType.ToString()))
							{
								if (t.Value == null)
								{
									fdf = "NULL,";
								}
								else if (t.Value.ToString() == "")
								{
									fdf = "NULL,";
								}
								else
								{
									fdf = "'" + t.Value.ToString() + "',";
								}

							}

							else
							{

								if (t.Value == null)
								{

									fdf = "NULL,";

								}
								else if (t.Value == null)
								{
									fdf = "NULL,";
								}
								else if (t.Value.ToString() == "")
								{
									fdf = "NULL,";
								}

								else
								{

									fdf = t.Value.ToString() + ",";

								}

							}



							qry += t.ToString() + "=" + fdf;



						}



						cmd.CommandText = SqlString + " " + qry.TrimEnd(',');

						//SmartObject.SaveLog("Procedure call " + cmd.CommandText);

					}

					else
					{

						cmd.CommandText = SqlString;

						//SmartObject.SaveLog("Parameter is null " + cmd.CommandText);

					}



					//SmartObject.SaveLog("Before Execution");



					IDataReader reader = cmd.ExecuteReader();



					ds.Load(reader, LoadOption.OverwriteChanges, TableName);

					//if (reader.RecordsAffected > 0)
					//{

					//    SmartObject.SaveLog("Reader Does have rows");

					//}

					//else
					//{

					//    SmartObject.SaveLog("Reader Doesnt have rows");

					//}



					//SmartObject.SaveLog("Reader Read Sucessfully");

					reader.Close();

					theCons.Close();



				}

				catch (Exception ex)
				{

					SmartObject.SaveLog("Exception from Procedure call " + ex.Message);

				}



				return ds;

			}

		}


		[WebMethod]
		public DataSet ValidateAccount(string AcctType, string AcctNo, string CrncyCode, string Username)
		{

			//AseConnection c = new AseConnection(a);
			try
			{
				// c.Open();
				List<AseParameter> sp = new List<AseParameter>()
				{
					new AseParameter() {ParameterName = "@psTransactionRef", AseDbType = AseDbType.VarChar, Value= "1"},
					new AseParameter() {ParameterName = "@psAccountType", AseDbType = AseDbType.VarChar, Value= AcctType},
					new AseParameter() {ParameterName = "@psAccountNo", AseDbType = AseDbType.VarChar, Value= AcctNo},
					new AseParameter() {ParameterName = "@psUserName", AseDbType = AseDbType.VarChar, Value= Username},
					new AseParameter() {ParameterName = "@psCrncyCode", AseDbType = AseDbType.VarChar, Value= CrncyCode},
					new AseParameter() {ParameterName = "@pnChequeNo", AseDbType = AseDbType.VarChar, Value= DBNull.Value},
				};
				var dt = SqlDs("isp_tci_validate", "ValidateAccount", sp);
				return dt;
				// c.Open();

				//var post = new List<AcctValResponse>();
				//post.Add(new AcctValResponse()
				//{
				//    /// CbsTranId = "11",
				//    nBalance = 10000000,
				//    nBranch = "001",
				//    nErrorCode = 0,
				//    sErrorText = "Success",
				//    sName = "Testing static data",
				//    sStatus = "Active",
				//    sAccountType = "CA",
				//    sAddress = "Test Address",
				//    sChequeStatus = "",
				//    sCrncyIso = "SLL",
				//    sTransNature = "1",
				//    sProductCode = AcctNo == "6090100137" ? "102" : "101"
				//});
				// var dt = DatasetHelper.ToDataSet<AcctValResponse>(post, "ValidateAccount");
			}
			catch (Exception ex)
			{

			}
			return null;

			// return 0;
		}


		[WebMethod]
		public DataSet GetFxRate(string BaseCurrency, string MinorCurrency)
		{

			//AseConnection c = new AseConnection(a);
			try
			{
				// c.Open();
				//List<SqlParameter> sp = new List<SqlParameter>()
				//{
				//    new SqlParameter() {ParameterName = "@psCurrencyIso", AseDbType = AseDbType.VarChar, Value= CrncyIso},
				//};
				//var dt = SqlDs("isp_GetFxRate", "GetFxRate", sp);

				var dt = conDs(null, "exec isp_GetFxRate " + BaseCurrency + "," + MinorCurrency, 1);
				return dt;

			}
			catch (Exception ex)
			{

			}
			return null;

		}

        [WebMethod]
        public DataSet GetFxRate2(string BaseCurrency, string MinorCurrency, string RateType)
        {

            //AseConnection c = new AseConnection(a);
            try
            {
                // c.Open();
                //List<SqlParameter> sp = new List<SqlParameter>()
                //{
                //    new SqlParameter() {ParameterName = "@psCurrencyIso", AseDbType = AseDbType.VarChar, Value= CrncyIso},
                //};
                //var dt = SqlDs("isp_GetFxRate", "GetFxRate", sp);

                var dt = conDs(null, "exec isp_GetFxRate2 " + "'" + BaseCurrency + "'" + "," + "'" + MinorCurrency + "'" + "," + "'" + RateType +"'", 1);
                return dt;

            }
            catch (Exception ex)
            {

            }
            return null;

        }

        public DataSet conDs(List<SqlParameter> parameter, string procname, int sqltype)
		{
            //oSmartObject = new SmartObject();
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            string connstring = System.Configuration.ConfigurationManager.ConnectionStrings["GetRateConnection"].ToString();
            TechClearingContext ctx = new TechClearingContext();
            var de = ctx.admConnectionSetups.Where(x => x.Name == "GetRateConnection").FirstOrDefault();

            if (de != null)
            {
                connstring = connstring.Replace("{{Data Source}}", de.DatabaseServer);

                connstring = connstring.Replace("{{database}}", de.Databasename);

                connstring = connstring.Replace("{{uid}}", de.DatabaseUserName);

                System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();
                System.Text.Decoder utf8Decode = encoder.GetDecoder();
                byte[] todecode_byte = Convert.FromBase64String(de.DatabasePassword);
                int charCount = utf8Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
                char[] decoded_char = new char[charCount];
                utf8Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
                string result = new String(decoded_char);

                connstring = connstring.Replace("{{pwd}}", result);

                try
                {
                    using (var con = new SqlConnection(connstring))
                    {
                        con.Open();

                        //SmartObject.SaveLog("inside sqlConn");

                        using (var cmd = new SqlCommand())
                        {
                            cmd.Connection = con;
                            cmd.CommandTimeout = 0;
                            cmd.CommandType = sqltype == 0 ? CommandType.StoredProcedure : CommandType.Text;
                            cmd.CommandText = procname;
                            if (parameter != null)
                                cmd.Parameters.AddRange(parameter.ToArray());
                            //for (int i = 0; i < cmd.Parameters.Count; i++)
                            //    oSmartObject.SmartObject.SaveLog(cmd.Parameters[i].ParameterName.ToString() + "-" + cmd.Parameters[i].Value.ToString());
                            SqlDataReader dr = cmd.ExecuteReader();

                            ds.Load(dr, LoadOption.OverwriteChanges, "Results");
                            //dt = ds.Tables[0];
                            //oSmartObject.SmartObject.SaveLog("Reports get data " + dt.Rows.Count);

                            dr.Close();

                        }
                        con.Close();
                    }
                }
                catch (Exception ex)
                {

                    SmartObject.SaveLog("Error while fetching rates " + ex.Message == null ? ex.InnerException.Message : ex.Message);

                }
            }

			return ds;
		}

        [WebMethod]
        public DataSet AccountDetails(string acctNo, string accttype, string username)
        {

            //AseConnection c = new AseConnection(a);
            try
            {
                // c.Open();
                List<AseParameter> sp = new List<AseParameter>()
                {
                    new AseParameter() {ParameterName = "@psAccountNo", AseDbType = AseDbType.VarChar, Value= acctNo},
                    new AseParameter() {ParameterName = "@psAcctType", AseDbType = AseDbType.VarChar, Value= accttype},
                    new AseParameter() {ParameterName = "@psUserName", AseDbType = AseDbType.VarChar, Value= username},


                };

                var dt = SqlDs("zenbase..isp_acct_enquiry", "BalanceEnquiry", sp);
                return dt;
            }
            catch (Exception ex)
            {

            }
            return null;

            // return 0;
        }

        [WebMethod]
        public DataSet UnCollectedFundsDetails(string acctNo, string accttype)
        {

            //AseConnection c = new AseConnection(a);
            try
            {

                List<AseParameter> sp = new List<AseParameter>()
                {
                    new AseParameter() {ParameterName = "@psAccountNo", AseDbType = AseDbType.VarChar, Value= acctNo},
                    new AseParameter() {ParameterName = "@psAccountType", AseDbType = AseDbType.VarChar, Value= accttype},

                };

                var dt = SqlDs("isp_get_float", "FloatEnquiry", sp);
                return dt;
            }
            catch (Exception ex)
            {

            }
            return null;

            // return 0;
        }





        static object CheckNull<T>(T value)
        {
            return value == null ? (object)DBNull.Value : value;
        }

        private object CheckDBNullValue(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return DBNull.Value;
            else
                return value;
        }

        //      [WebMethod]
        //public DataSet TransactionPosting(string TransRef, string DrAccountNo, string DrAcctType,
        //	int DrAcctTC, string DrAcctNarration, decimal TranAmount, string CrAcctNo,
        //	string CrAcctType,
        //	int CrAcctTC, string CrAcctNarration, string CurrencyISO, DateTime PostDate, DateTime ValueDate, string UserName,
        //	int? ChequeNo, string SupervisorName, int? ChannelId, short? ForcePostFlag, short? Reversal,
        //	int? TranBatchID, int? ChargeCode, decimal? ChargeAmt, decimal? TaxAmt, int? DrAcctChargeCode, decimal? DrAcctChargeAmt, decimal? DrAcctTaxAmt,
        //	int? DrAcctChequeNo, string DrAcctChgDescr, int? CrAcctChargeCode, decimal? CrAcctChargeAmt, decimal? CrAcctTaxAmt, int? CrAcctChequeNo,
        //	string CrAcctChgDescr, string TransTracer, string ParentTransRef, string RoutingNo, int? FloatDays, int? RimNo, string Direction, string ChargeAcct)
        //{

        //	//AseConnection c = new AseConnection(a);
        //	try
        //	{
        //		// c.Open();
        //		List<AseParameter> sp = new List<AseParameter>()
        //		{
        //			new AseParameter() {ParameterName = "@psTransactionRef", AseDbType = AseDbType.VarChar, Value= TransRef},
        //			new AseParameter() {ParameterName = "@psDrAcctNo", AseDbType = AseDbType.VarChar, Value= DrAccountNo},
        //			new AseParameter() {ParameterName = "@psDrAcctType", AseDbType = AseDbType.VarChar, Value= DrAcctType},
        //			new AseParameter() {ParameterName = "@pnDrAcctTC", AseDbType = AseDbType.Integer, Value= DrAcctTC},
        //			new AseParameter() {ParameterName = "@psDrAcctNarration", AseDbType = AseDbType.VarChar, Value= DrAcctNarration},
        //			new AseParameter() {ParameterName = "@pnTranAmount", AseDbType = AseDbType.Decimal, Value= TranAmount},
        //			new AseParameter() {ParameterName = "@psCrAcctNo", AseDbType = AseDbType.VarChar, Value= CrAcctNo},
        //			new AseParameter() {ParameterName = "@psCrAcctType", AseDbType = AseDbType.VarChar, Value= CrAcctType},
        //			new AseParameter() {ParameterName = "@pnCrAcctTC", AseDbType = AseDbType.Integer, Value= CrAcctTC},
        //			new AseParameter() {ParameterName = "@psCrAcctNarration", AseDbType = AseDbType.VarChar, Value= CrAcctNarration},
        //			new AseParameter() {ParameterName = "@psCurrencyISO", AseDbType = AseDbType.VarChar, Value= CurrencyISO},
        //			new AseParameter() {ParameterName = "@pdtPostDate", AseDbType = AseDbType.Date, Value= PostDate},
        //			new AseParameter() {ParameterName = "@pdtValueDate", AseDbType = AseDbType.Date, Value= ValueDate},
        //			new AseParameter() {ParameterName = "@psUserName", AseDbType = AseDbType.VarChar, Value= UserName},
        //			new AseParameter() {ParameterName = "@pnFloatDays", AseDbType = AseDbType.SmallInt, Value= FloatDays},
        //			new AseParameter() {ParameterName = "@psRoutingNo", AseDbType = AseDbType.VarChar, Value= RoutingNo},
        //		  //  new AseParameter() {ParameterName = "@pnChequeNo", AseDbType = AseDbType.Integer, Value= ChequeNo},
        //			new AseParameter() {ParameterName = "@psSupervisorName", AseDbType = AseDbType.VarChar, Value= SupervisorName == "" ? null : SupervisorName},
        //			new AseParameter() {ParameterName = "@pnChannelId", AseDbType = AseDbType.SmallInt, Value= ChannelId},
        //		  //  new AseParameter() {ParameterName = "@pnRemoveOldFloat", AseDbType = AseDbType.TinyInt, Value= RemoveOldFloat},
        //			new AseParameter() {ParameterName = "@pnForcePostFlag", AseDbType = AseDbType.TinyInt, Value= ForcePostFlag},
        //			new AseParameter() {ParameterName = "@pnReversal", AseDbType = AseDbType.TinyInt, Value= Reversal == null? 0 : Reversal},
        //			new AseParameter() {ParameterName = "@pnTranBatchID", AseDbType = AseDbType.Integer, Value= TranBatchID},
        //		 //   new AseParameter() {ParameterName = "@pnChargeCode", AseDbType = AseDbType.Integer, Value= ChargeCode},
        //		 //   new AseParameter() {ParameterName = "@pnChargeAmt", AseDbType = AseDbType.Decimal, Value= ChargeAmt},
        //		 //   new AseParameter() {ParameterName = "@pnTaxAmt", AseDbType = AseDbType.Decimal, Value= TaxAmt},
        //			new AseParameter() {ParameterName = "@psParentTransRef", AseDbType = AseDbType.VarChar, Value= ParentTransRef == "" ? null : ParentTransRef},
        //			new AseParameter() {ParameterName = "@pnDirection", AseDbType = AseDbType.Integer, Value= Direction},
        //		 //   new AseParameter() {ParameterName = "@psChargeAcct", AseDbType = AseDbType.VarChar, Value= ChargeAcct == "" ? null : ChargeAcct},
        //			new AseParameter() {ParameterName = "@pnRimNo", AseDbType = AseDbType.Integer, Value= RimNo },
        //			new AseParameter() {ParameterName = "@pnDrAcctChargeCode", AseDbType = AseDbType.Integer, Value= DrAcctChargeCode},
        //			new AseParameter() {ParameterName = "@pnDrAcctChargeAmt", AseDbType = AseDbType.Decimal, Value= DrAcctChargeAmt},
        //			new AseParameter() {ParameterName = "@pnDrAcctTaxAmt", AseDbType = AseDbType.Decimal, Value= DrAcctTaxAmt},
        //			new AseParameter() {ParameterName = "@pnDrAcctChequeNo", AseDbType = AseDbType.Integer, Value= DrAcctChequeNo},
        //			new AseParameter() {ParameterName = "@pnCrAcctChargeCode", AseDbType = AseDbType.Integer, Value= CrAcctChargeCode},
        //			new AseParameter() {ParameterName = "@pnCrAcctChargeAmt", AseDbType = AseDbType.Decimal, Value= CrAcctChargeAmt},
        //			new AseParameter() {ParameterName = "@pnCrAcctTaxAmt", AseDbType = AseDbType.Decimal, Value= CrAcctTaxAmt},
        //			new AseParameter() {ParameterName = "@pnCrAcctChequeNo", AseDbType = AseDbType.Integer, Value= CrAcctChequeNo},
        //			new AseParameter() {ParameterName = "@psTransTracer", AseDbType = AseDbType.VarChar, Value= TransTracer == "" ? null : TransTracer},
        //			new AseParameter() {ParameterName = "@psDrAcctChgDescr", AseDbType = AseDbType.VarChar, Value= DrAcctChgDescr},
        //			new AseParameter() {ParameterName = "@psCrAcctChgDescr", AseDbType = AseDbType.VarChar, Value= CrAcctChgDescr},

        //		};
        //		var dt = SqlDs("isp_tci_post3", "TransactionPosting", sp);
        //		// start demo
        //		//var post = new List<PostingResponse>();
        //		//post.Add(new PostingResponse()
        //		//    {
        //		//        CbsTranId = "11",
        //		//        nBalance = 50000,
        //		//        nBranch = "001",
        //		//        nErrorCode = 1,
        //		//        sErrorText = "Failed",
        //		//        sName = "Testing static data",
        //		//        sStatus = "Active"
        //		//    });
        //		//var dt = DatasetHelper.ToDataSet<PostingResponse>(post, "TransactionPosting");
        //		return dt;
        //	}
        //	catch (Exception ex)
        //	{

        //	}
        //	return null;

        //	// return 0;
        //}

        //      [WebMethod]
        //      public DataSet TransactionPostingAsync(string TransRef, string DrAccountNo, string DrAcctType,
        //         string DrAcctTC, string DrAcctNarration, string TranAmount, string CrAcctNo,
        //         string CrAcctType, string CrAcctTC, string CrAcctNarration, string CurrencyISO, string PostDate, string ValueDate, string UserName,
        //         string ChequeNo, string SupervisorName, string ChannelId, string ForcePostFlag, string Reversal,
        //         string TranBatchID, string ChargeCode, string ChargeAmt, string TaxAmt, string DrAcctChargeCode, string DrAcctChargeAmt, string DrAcctTaxAmt,
        //         string DrAcctChequeNo, string DrAcctChgDescr, string CrAcctChargeCode, string CrAcctChargeAmt, string CrAcctTaxAmt, string CrAcctChequeNo,
        //         string CrAcctChgDescr, string TransTracer, string ParentTransRef, string RoutingNo, string FloatDays, string RimNo, string Direction, string ChargeAcct,
        //         string DrAcctCashAmt, string CrAcctCashAmt, string DrAcctChargeBranch, string CrAcctChargeBranch)
        //      {

        //          //AseConnection c = new AseConnection(a);
        //          try
        //          {
        //              // c.Open();
        //              List<AseParameter> sp = new List<AseParameter>()
        //              {
        //                  new AseParameter() {ParameterName = "@psTransactionRef", AseDbType = AseDbType.VarChar, Value= TransRef},
        //                  new AseParameter() {ParameterName = "@psDrAcctNo", AseDbType = AseDbType.VarChar, Value= DrAccountNo},
        //                  new AseParameter() {ParameterName = "@psDrAcctType", AseDbType = AseDbType.VarChar, Value= DrAcctType},
        //                  new AseParameter() {ParameterName = "@pnDrAcctTC", AseDbType = AseDbType.Integer, Value= DrAcctTC},
        //                  new AseParameter() {ParameterName = "@psDrAcctNarration", AseDbType = AseDbType.VarChar, Value= DrAcctNarration},
        //                  new AseParameter() {ParameterName = "@pnTranAmount", AseDbType = AseDbType.Decimal, Value= TranAmount},
        //                  new AseParameter() {ParameterName = "@psCrAcctNo", AseDbType = AseDbType.VarChar, Value= CrAcctNo},
        //                  new AseParameter() {ParameterName = "@psCrAcctType", AseDbType = AseDbType.VarChar, Value= CrAcctType},
        //                  new AseParameter() {ParameterName = "@pnCrAcctTC", AseDbType = AseDbType.Integer, Value= CrAcctTC},
        //                  new AseParameter() {ParameterName = "@psCrAcctNarration", AseDbType = AseDbType.VarChar, Value= CrAcctNarration},
        //                  new AseParameter() {ParameterName = "@psCurrencyISO", AseDbType = AseDbType.VarChar, Value= CurrencyISO},
        //                  new AseParameter() {ParameterName = "@pdtPostDate", AseDbType = AseDbType.Date, Value= PostDate},
        //                  new AseParameter() {ParameterName = "@pdtValueDate", AseDbType = AseDbType.Date, Value= ValueDate},
        //                  new AseParameter() {ParameterName = "@psUserName", AseDbType = AseDbType.VarChar, Value= UserName},
        //                  new AseParameter() {ParameterName = "@pnFloatDays", AseDbType = AseDbType.SmallInt, Value= CheckDBNullValue(FloatDays)},
        //                  new AseParameter() {ParameterName = "@psRoutingNo", AseDbType = AseDbType.VarChar, Value= CheckDBNullValue(RoutingNo)},
        //		  //  new AseParameter() {ParameterName = "@pnChequeNo", AseDbType = AseDbType.Integer, Value= ChequeNo},
        //			new AseParameter() {ParameterName = "@psSupervisorName", AseDbType = AseDbType.VarChar, Value= CheckDBNullValue(SupervisorName)},
        //                  new AseParameter() {ParameterName = "@pnChannelId", AseDbType = AseDbType.SmallInt, Value= CheckDBNullValue(ChannelId)},
        //		  //  new AseParameter() {ParameterName = "@pnRemoveOldFloat", AseDbType = AseDbType.TinyInt, Value= RemoveOldFloat},
        //			new AseParameter() {ParameterName = "@pnForcePostFlag", AseDbType = AseDbType.TinyInt, Value= CheckDBNullValue(ForcePostFlag)},
        //                  new AseParameter() {ParameterName = "@pnReversal", AseDbType = AseDbType.TinyInt, Value= CheckDBNullValue(Reversal)},
        //                  new AseParameter() {ParameterName = "@pnTranBatchID", AseDbType = AseDbType.Integer, Value= CheckDBNullValue(TranBatchID)},
        //		 //   new AseParameter() {ParameterName = "@pnChargeCode", AseDbType = AseDbType.Integer, Value= ChargeCode},
        //		 //   new AseParameter() {ParameterName = "@pnChargeAmt", AseDbType = AseDbType.Decimal, Value= ChargeAmt},
        //		 //   new AseParameter() {ParameterName = "@pnTaxAmt", AseDbType = AseDbType.Decimal, Value= TaxAmt},
        //			new AseParameter() {ParameterName = "@psParentTransRef", AseDbType = AseDbType.VarChar, Value= CheckDBNullValue(ParentTransRef)},
        //                  new AseParameter() {ParameterName = "@pnDirection", AseDbType = AseDbType.Integer, Value= CheckDBNullValue(Direction)},
        //		 //   new AseParameter() {ParameterName = "@psChargeAcct", AseDbType = AseDbType.VarChar, Value= ChargeAcct == "" ? null : ChargeAcct},
        //			new AseParameter() {ParameterName = "@pnRimNo", AseDbType = AseDbType.Integer, Value= CheckDBNullValue(RimNo) },
        //                  new AseParameter() {ParameterName = "@pnDrAcctChargeCode", AseDbType = AseDbType.Integer, Value= CheckDBNullValue(DrAcctChargeCode)},
        //                  new AseParameter() {ParameterName = "@pnDrAcctChargeAmt", AseDbType = AseDbType.Decimal, Value= CheckDBNullValue(DrAcctChargeAmt)},
        //                  new AseParameter() {ParameterName = "@pnDrAcctTaxAmt", AseDbType = AseDbType.Decimal, Value= CheckDBNullValue(DrAcctTaxAmt)},
        //                  new AseParameter() {ParameterName = "@pnDrAcctChequeNo", AseDbType = AseDbType.Integer, Value= CheckDBNullValue(DrAcctChequeNo)},
        //                  new AseParameter() {ParameterName = "@pnCrAcctChargeCode", AseDbType = AseDbType.Integer, Value= CheckDBNullValue(CrAcctChargeCode)},
        //                  new AseParameter() {ParameterName = "@pnCrAcctChargeAmt", AseDbType = AseDbType.Decimal, Value= CheckDBNullValue(CrAcctChargeAmt)},
        //                  new AseParameter() {ParameterName = "@pnCrAcctTaxAmt", AseDbType = AseDbType.Decimal, Value= CheckDBNullValue(CrAcctTaxAmt)},
        //                  new AseParameter() {ParameterName = "@pnCrAcctChequeNo", AseDbType = AseDbType.Integer, Value= CheckDBNullValue(CrAcctChequeNo)},
        //                  new AseParameter() {ParameterName = "@psTransTracer", AseDbType = AseDbType.VarChar, Value= CheckDBNullValue(TransTracer)},
        //                  new AseParameter() {ParameterName = "@psDrAcctChgDescr", AseDbType = AseDbType.VarChar, Value= CheckDBNullValue(DrAcctChgDescr)},
        //                  new AseParameter() {ParameterName = "@psCrAcctChgDescr", AseDbType = AseDbType.VarChar, Value= CheckDBNullValue(CrAcctChgDescr)},
        //                  new AseParameter() {ParameterName = "@pnDrAcctCashAmt", AseDbType = AseDbType.Decimal, Value= CheckDBNullValue(DrAcctCashAmt)},
        //                  new AseParameter() {ParameterName = "@pnCrAcctCashAmt", AseDbType = AseDbType.Decimal, Value= CheckDBNullValue(CrAcctCashAmt)},
        //                  new AseParameter() {ParameterName = "@psDrAcctChgBranch", AseDbType = AseDbType.Integer, Value= CheckDBNullValue(DrAcctChargeBranch)},
        //                  new AseParameter() {ParameterName = "@psCrAcctChgBranch", AseDbType = AseDbType.Integer, Value= CheckDBNullValue(CrAcctChargeBranch)}

        //              };
        //              var dt = SqlDs("isp_tci_post3", "TransactionPosting", sp);
        //              // start demo
        //              //var post = new List<PostingResponse>();
        //              //post.Add(new PostingResponse()
        //              //    {
        //              //        CbsTranId = "11",
        //              //        nBalance = 50000,
        //              //        nBranch = "001",
        //              //        nErrorCode = 1,
        //              //        sErrorText = "Failed",
        //              //        sName = "Testing static data",
        //              //        sStatus = "Active"
        //              //    });
        //              //var dt = DatasetHelper.ToDataSet<PostingResponse>(post, "TransactionPosting");
        //              return dt;
        //          }
        //          catch (Exception ex)
        //          {

        //          }
        //          return null;

        //          // return 0;
        //      }
        //public DataSet TransactionPosting(string ItbId, string TransactionRef, string DrAcctNo, string DrAcctType, string DrAcctTC,
        //                            string DrAcctNarration, string TranAmount, string CrAcctNo, string CrAcctType, string CrAcctTC,
        //                            string CrAcctNarration, string CurrencyISO, string PostDate, string ValueDate, string UserName,
        //                            string FloatDays, string RoutingNo, string SupervisorName, string ChannelId, string ForcePostFlag,
        //                            string Reversal, string TranBatchID, string ParentTransRef, string Direction, string RimNo,
        //                            string DrAcctChequeNo, string DrAcctChargeCode, string DrAcctChargeAmt, string DrAcctTaxAmt,
        //                            string CrAcctChequeNo, string CrAcctChargeCode, string CrAcctChargeAmt, string CrAcctTaxAmt,
        //                            string TransTracer, string DrAcctChgDescr, string CrAcctChgDescr, string DrAcctCashAmt,
        //                            string CrAcctCashAmt, string EquivAmt, string OrigExchRate, string ExchRate, string DrAcctChgBranch,
        //                            string CrAcctChgBranch, string DrAcctOffshoreAmt, string CrAcctOffshoreAmt)

        [WebMethod]
        public DataSet TransactionPosting(PostingRequest pr)
        {

            //AseConnection c = new AseConnection(a);
            //int? itbid = int.Parse(pr.ItbId);
            int? itbid = pr.ItbId;
            try
            {
                // c.Open();
                List<AseParameter> sp = new List<AseParameter>()
                {
                    new AseParameter() {ParameterName = "@psTransactionRef", AseDbType = AseDbType.VarChar, Value= pr.TransactionRef},
                    new AseParameter() {ParameterName = "@psDrAcctNo", AseDbType = AseDbType.VarChar, Value= pr.DrAcctNo},
                    new AseParameter() {ParameterName = "@psDrAcctType", AseDbType = AseDbType.VarChar, Value= pr.DrAcctType},
                    new AseParameter() {ParameterName = "@pnDrAcctTC", AseDbType = AseDbType.Integer, Value=pr.DrAcctTC},
                    new AseParameter() {ParameterName = "@psDrAcctNarration", AseDbType = AseDbType.VarChar, Value= pr.DrAcctNarration},
                    new AseParameter() {ParameterName = "@pnTranAmount", AseDbType = AseDbType.Decimal, Value= pr.TranAmount},
                    new AseParameter() {ParameterName = "@psCrAcctNo", AseDbType = AseDbType.VarChar, Value= pr.CrAcctNo},
                    new AseParameter() {ParameterName = "@psCrAcctType", AseDbType = AseDbType.VarChar, Value= pr.CrAcctType},
                    new AseParameter() {ParameterName = "@pnCrAcctTC", AseDbType = AseDbType.Integer, Value= pr.CrAcctTC},
                    new AseParameter() {ParameterName = "@psCrAcctNarration", AseDbType = AseDbType.VarChar, Value= pr.CrAcctNarration},
                    new AseParameter() {ParameterName = "@psCurrencyISO", AseDbType = AseDbType.VarChar, Value= pr.CurrencyISO},
                    new AseParameter() {ParameterName = "@pdtPostDate", AseDbType = AseDbType.Date, Value= pr.PostDate},
                    new AseParameter() {ParameterName = "@pdtValueDate", AseDbType = AseDbType.Date, Value= pr.ValueDate},
                    new AseParameter() {ParameterName = "@psUserName", AseDbType = AseDbType.VarChar, Value= pr.UserName},
                    new AseParameter() {ParameterName = "@pnFloatDays", AseDbType = AseDbType.Integer, Value= pr.FloatDays},
                    new AseParameter() {ParameterName = "@psRoutingNo", AseDbType = AseDbType.VarChar, Value= pr.RoutingNo},
                    new AseParameter() {ParameterName = "@psSupervisorName", AseDbType = AseDbType.VarChar, Value= pr.SupervisorName},
                    new AseParameter() {ParameterName = "@pnChannelId", AseDbType = AseDbType.Integer, Value= pr.ChannelId},
                    new AseParameter() {ParameterName = "@pnForcePostFlag", AseDbType = AseDbType.Integer, Value= pr.ForcePostFlag},
                    new AseParameter() {ParameterName = "@pnReversal", AseDbType = AseDbType.Integer, Value= pr.Reversal},
                    new AseParameter() {ParameterName = "@pnTranBatchID", AseDbType = AseDbType.Integer,IsNullable=true, Value= pr.TranBatchID},
                    new AseParameter() {ParameterName = "@psParentTransRef", AseDbType = AseDbType.VarChar, Value= pr.ParentTransRef},
                    new AseParameter() {ParameterName = "@pnDirection", AseDbType = AseDbType.Integer, Value= pr.Direction},
                    new AseParameter() {ParameterName = "@pnRimNo", AseDbType = AseDbType.Integer,IsNullable=true, Value= pr.RimNo},
                    new AseParameter() {ParameterName = "@pnDrAcctChequeNo", AseDbType = AseDbType.Integer,IsNullable=true, Value= pr.DrAcctChequeNo},
                    new AseParameter() {ParameterName = "@pnDrAcctChargeCode", AseDbType = AseDbType.Integer,IsNullable=true, Value= pr.DrAcctChargeCode},
                    new AseParameter() {ParameterName = "@pnDrAcctChargeAmt", AseDbType = AseDbType.Decimal,IsNullable=true, Value= pr.DrAcctChargeAmt},
                    new AseParameter() {ParameterName = "@pnDrAcctTaxAmt", AseDbType = AseDbType.Decimal,IsNullable=true, Value= pr.DrAcctTaxAmt},
                    new AseParameter() {ParameterName = "@pnCrAcctChequeNo", AseDbType = AseDbType.Integer,IsNullable=true, Value= pr.CrAcctChequeNo},
                    new AseParameter() {ParameterName = "@pnCrAcctChargeCode", AseDbType = AseDbType.Integer,IsNullable=true, Value= pr.CrAcctChargeCode},
                    new AseParameter() {ParameterName = "@pnCrAcctChargeAmt", AseDbType = AseDbType.Decimal,IsNullable=true, Value= pr.CrAcctChargeAmt},
                    new AseParameter() {ParameterName = "@pnCrAcctTaxAmt", AseDbType = AseDbType.Decimal,IsNullable=true, Value= pr.CrAcctTaxAmt},
                    new AseParameter() {ParameterName = "@psTransTracer", AseDbType = AseDbType.VarChar, Value= pr.TransTracer},
                    new AseParameter() {ParameterName = "@psDrAcctChgDescr", AseDbType = AseDbType.VarChar, Value= pr.DrAcctChgDescr},
                    new AseParameter() {ParameterName = "@psCrAcctChgDescr", AseDbType = AseDbType.VarChar, Value= pr.CrAcctChgDescr},
                    new AseParameter() {ParameterName = "@pnDrAcctCashAmt", AseDbType = AseDbType.Decimal,IsNullable=true, Value= pr.DrAcctCashAmt},
                    new AseParameter() {ParameterName = "@pnCrAcctCashAmt", AseDbType = AseDbType.Decimal,IsNullable=true, Value= pr.CrAcctCashAmt},
                    new AseParameter() {ParameterName = "@pnEquivAmt", AseDbType = AseDbType.Decimal,IsNullable=true, Value= pr.EquivAmt},
                    new AseParameter() {ParameterName = "@pnOrigExchRate", AseDbType = AseDbType.Decimal,IsNullable=true, Value= pr.OrigExchRate},
                    new AseParameter() {ParameterName = "@pnExchRate", AseDbType = AseDbType.Decimal,IsNullable=true, Value= pr.ExchRate},
                    new AseParameter() {ParameterName = "@psDrAcctChgBranch", AseDbType = AseDbType.Integer,IsNullable=true, Value= pr.DrAcctChgBranch},
                    new AseParameter() {ParameterName = "@psCrAcctChgBranch", AseDbType = AseDbType.Integer,IsNullable=true, Value= pr.CrAcctChgBranch},
                    new AseParameter() {ParameterName = "@pnDrAcctOffshoreAmt", AseDbType = AseDbType.Decimal,IsNullable=true, Value= pr.DrAcctOffshoreAmt},
                    new AseParameter() {ParameterName = "@pnCrAcctOffshoreAmt", AseDbType = AseDbType.Decimal, IsNullable=true, Value= pr.CrAcctOffshoreAmt},


                };
                var dt = SqlDs("isp_tci_post3", "TransactionPosting", sp);
                // start demo
                //var post = new List<PostingResponse>();
                //post.Add(new PostingResponse()
                //    {
                //        CbsTranId = "11",
                //        nBalance = 50000,
                //        nBranch = "001",
                //        nErrorCode = 1,
                //        sErrorText = "Failed",
                //        sName = "Testing static data",
                //        sStatus = "Active"
                //    });
                //var dt = DatasetHelper.ToDataSet<PostingResponse>(post, "TransactionPosting");
                return dt;
            }
            catch (Exception ex)
            {

            }
            return null;

            // return 0;
        }


        [WebMethod] //DataSet
        public PostingResponse TransactionPostingAsync(string ItbId, string TransactionRef, string DrAcctNo, string DrAcctType, string DrAcctTC,
                                    string DrAcctNarration, string TranAmount, string CrAcctNo, string CrAcctType, string CrAcctTC,
                                    string CrAcctNarration, string CurrencyISO, string PostDate, string ValueDate, string UserName,
                                    string FloatDays, string RoutingNo, string SupervisorName, string ChannelId, string ForcePostFlag,
                                    string Reversal, string TranBatchID, string ParentTransRef, string Direction, string RimNo,
                                    string DrAcctChequeNo, string DrAcctChargeCode, string DrAcctChargeAmt, string DrAcctTaxAmt,
                                    string CrAcctChequeNo, string CrAcctChargeCode, string CrAcctChargeAmt, string CrAcctTaxAmt,
                                    string TransTracer, string DrAcctChgDescr, string CrAcctChgDescr, string DrAcctCashAmt,
                                    string CrAcctCashAmt, string EquivAmt, string OrigExchRate, string ExchRate, string DrAcctChgBranch,
                                    string CrAcctChgBranch, string DrAcctOffshoreAmt, string CrAcctOffshoreAmt)
        {

            //AseConnection c = new AseConnection(a);
            string itbid = ItbId;
            try
            {
                // c.Open();
                List<AseParameter> sp = new List<AseParameter>()
                {
                    new AseParameter() {ParameterName = "@psTransactionRef", AseDbType = AseDbType.VarChar, Value= TransactionRef},
                    new AseParameter() {ParameterName = "@psDrAcctNo", AseDbType = AseDbType.VarChar, Value= DrAcctNo},
                    new AseParameter() {ParameterName = "@psDrAcctType", AseDbType = AseDbType.VarChar, Value= DrAcctType},
                    new AseParameter() {ParameterName = "@pnDrAcctTC", AseDbType = AseDbType.Integer, Value= DrAcctTC},
                    new AseParameter() {ParameterName = "@psDrAcctNarration", AseDbType = AseDbType.VarChar, Value= DrAcctNarration},
                    new AseParameter() {ParameterName = "@pnTranAmount", AseDbType = AseDbType.Decimal, Value= TranAmount},
                    new AseParameter() {ParameterName = "@psCrAcctNo", AseDbType = AseDbType.VarChar, Value= CrAcctNo},
                    new AseParameter() {ParameterName = "@psCrAcctType", AseDbType = AseDbType.VarChar, Value= CrAcctType},
                    new AseParameter() {ParameterName = "@pnCrAcctTC", AseDbType = AseDbType.Integer, Value= CrAcctTC},
                    new AseParameter() {ParameterName = "@psCrAcctNarration", AseDbType = AseDbType.VarChar, Value= CrAcctNarration},
                    new AseParameter() {ParameterName = "@psCurrencyISO", AseDbType = AseDbType.VarChar, Value= CurrencyISO},
                    new AseParameter() {ParameterName = "@pdtPostDate", AseDbType = AseDbType.Date, Value= PostDate},
                    new AseParameter() {ParameterName = "@pdtValueDate", AseDbType = AseDbType.Date, Value= ValueDate},
                    new AseParameter() {ParameterName = "@psUserName", AseDbType = AseDbType.VarChar, Value= UserName},
                    new AseParameter() {ParameterName = "@pnFloatDays", AseDbType = AseDbType.Integer, Value= FloatDays},
                    new AseParameter() {ParameterName = "@psRoutingNo", AseDbType = AseDbType.VarChar, Value= RoutingNo},
                    new AseParameter() {ParameterName = "@psSupervisorName", AseDbType = AseDbType.VarChar, Value= SupervisorName},
                    new AseParameter() {ParameterName = "@pnChannelId", AseDbType = AseDbType.Integer, Value= ChannelId},
                    new AseParameter() {ParameterName = "@pnForcePostFlag", AseDbType = AseDbType.Integer, Value= ForcePostFlag},
                    new AseParameter() {ParameterName = "@pnReversal", AseDbType = AseDbType.Integer, Value= Reversal},
                    new AseParameter() {ParameterName = "@pnTranBatchID", AseDbType = AseDbType.Integer,IsNullable=true, Value= TranBatchID},
                    new AseParameter() {ParameterName = "@psParentTransRef", AseDbType = AseDbType.VarChar, Value= ParentTransRef},
                    new AseParameter() {ParameterName = "@pnDirection", AseDbType = AseDbType.Integer, Value= Direction},
                    new AseParameter() {ParameterName = "@pnRimNo", AseDbType = AseDbType.Integer,IsNullable=true, Value= RimNo},
                    new AseParameter() {ParameterName = "@pnDrAcctChequeNo", AseDbType = AseDbType.Integer,IsNullable=true, Value= DrAcctChequeNo},
                    new AseParameter() {ParameterName = "@pnDrAcctChargeCode", AseDbType = AseDbType.Integer,IsNullable=true, Value= DrAcctChargeCode},
                    new AseParameter() {ParameterName = "@pnDrAcctChargeAmt", AseDbType = AseDbType.Decimal,IsNullable=true, Value= DrAcctChargeAmt},
                    new AseParameter() {ParameterName = "@pnDrAcctTaxAmt", AseDbType = AseDbType.Decimal,IsNullable=true, Value= DrAcctTaxAmt},
                    new AseParameter() {ParameterName = "@pnCrAcctChequeNo", AseDbType = AseDbType.Integer,IsNullable=true, Value= CrAcctChequeNo},
                    new AseParameter() {ParameterName = "@pnCrAcctChargeCode", AseDbType = AseDbType.Integer,IsNullable=true, Value= CrAcctChargeCode},
                    new AseParameter() {ParameterName = "@pnCrAcctChargeAmt", AseDbType = AseDbType.Decimal,IsNullable=true, Value= CrAcctChargeAmt},
                    new AseParameter() {ParameterName = "@pnCrAcctTaxAmt", AseDbType = AseDbType.Decimal,IsNullable=true, Value= CrAcctTaxAmt},
                    new AseParameter() {ParameterName = "@psTransTracer", AseDbType = AseDbType.VarChar, Value= TransTracer},
                    new AseParameter() {ParameterName = "@psDrAcctChgDescr", AseDbType = AseDbType.VarChar, Value= DrAcctChgDescr},
                    new AseParameter() {ParameterName = "@psCrAcctChgDescr", AseDbType = AseDbType.VarChar, Value= CrAcctChgDescr},
                    new AseParameter() {ParameterName = "@pnDrAcctCashAmt", AseDbType = AseDbType.Decimal,IsNullable=true, Value= DrAcctCashAmt},
                    new AseParameter() {ParameterName = "@pnCrAcctCashAmt", AseDbType = AseDbType.Decimal,IsNullable=true, Value= CrAcctCashAmt},
                    new AseParameter() {ParameterName = "@pnEquivAmt", AseDbType = AseDbType.Decimal,IsNullable=true, Value= EquivAmt},
                    new AseParameter() {ParameterName = "@pnOrigExchRate", AseDbType = AseDbType.Decimal,IsNullable=true, Value= OrigExchRate},
                    new AseParameter() {ParameterName = "@pnExchRate", AseDbType = AseDbType.Decimal,IsNullable=true, Value= ExchRate},
                    new AseParameter() {ParameterName = "@psDrAcctChgBranch", AseDbType = AseDbType.Integer,IsNullable=true, Value= DrAcctChgBranch},
                    new AseParameter() {ParameterName = "@psCrAcctChgBranch", AseDbType = AseDbType.Integer,IsNullable=true, Value= CrAcctChgBranch},
                    new AseParameter() {ParameterName = "@pnDrAcctOffshoreAmt", AseDbType = AseDbType.Decimal,IsNullable=true, Value= DrAcctOffshoreAmt},
                    new AseParameter() {ParameterName = "@pnCrAcctOffshoreAmt", AseDbType = AseDbType.Decimal, IsNullable=true, Value= CrAcctOffshoreAmt},

                };
                var dt = SqlDs("isp_tci_post3", "TransactionPosting", sp);
                // start demo
                //var post = new List<PostingResponse>();
                //post.Add(new PostingResponse()
                //    {
                //        CbsTranId = "11",
                //        nBalance = 50000,
                //        nBranch = "001",
                //        nErrorCode = 1,
                //        sErrorText = "Failed",
                //        sName = "Testing static data",
                //        sStatus = "Active"
                //    });
                //var dt = DatasetHelper.ToDataSet<PostingResponse>(post, "TransactionPosting");
                var post = new PostingResponse();
                post.nErrorCode = int.Parse(dt.Tables[0].Rows[0][0].ToString());
                post.sErrorText = dt.Tables[0].Rows[0][1].ToString();
                post.nDrAcctBalance = decimal.Parse(dt.Tables[0].Rows[0][2].ToString());
                post.nCrAcctBalance = decimal.Parse(dt.Tables[0].Rows[0][3].ToString());
                post.sName = dt.Tables[0].Rows[0][4].ToString();
                post.sStatus = dt.Tables[0].Rows[0][5].ToString();
                post.nBranch = dt.Tables[0].Rows[0][6].ToString();
                post.nCbsTranId = dt.Tables[0].Rows[0][7].ToString();
                post.sValueDate = dt.Tables[0].Rows[0][8].ToString();


                //return dt;
                return post;
            }
            catch (Exception ex)
            {

            }
            return null;

            // return 0;
        }


        [WebMethod]
        public DataSet ValidateLedgerAccount(string LedgerNo, string Currency)
        {

            //AseConnection c = new AseConnection(a);
            try
            {
                // c.Open();
                List<AseParameter> sp = new List<AseParameter>()
                {
                    new AseParameter() {ParameterName = "@psLedgerNo", AseDbType = AseDbType.VarChar, Value= LedgerNo},
                    new AseParameter() {ParameterName = "@psCrncyCode", AseDbType = AseDbType.VarChar, Value= Currency},


                };
                var dt = SqlDs("zenbase..Isp_GetPosBalance", "ValidateLedgerAccount", sp);
                return dt;
            }
            catch (Exception ex)
            {

            }
            return null;

            // return 0;
        }




        [WebMethod]
        public DataSet FxFundRelease(string TransactionRef, string AccountType, string AccountNo, int? FloatDays)
        {

            //AseConnection c = new AseConnection(a);
            try
            {
                // c.Open();
                List<AseParameter> sp = new List<AseParameter>()
                {
                    new AseParameter() {ParameterName = "@psTransactionRef", AseDbType = AseDbType.VarChar, Value= TransactionRef},
                    new AseParameter() {ParameterName = "@psAccountType", AseDbType = AseDbType.VarChar, Value= AccountType},
                    new AseParameter() {ParameterName = "@psAccountNo", AseDbType = AseDbType.VarChar, Value= AccountNo},
                    new AseParameter() {ParameterName = "@pnFloatDays", AseDbType = AseDbType.Integer, Value=CheckNull(FloatDays)},


                };
                var dt = SqlDs("isp_tci_fund_release", "FundRelease", sp);
                return dt;
            }
            catch (Exception ex)
            {

            }
            return null;

            // return 0;
        }

        [WebMethod]
		public DataSet TransactionHolding(string TransactionRef, string AccountType, string AccountNo, int? HoldId, decimal HoldAmt, string Username, string HoldReason)
		{

			//AseConnection c = new AseConnection(a);
			try
			{
				// c.Open();
				List<AseParameter> sp = new List<AseParameter>()
				{
					new AseParameter() {ParameterName = "@psTransactionRef", AseDbType = AseDbType.VarChar, Value= TransactionRef},
					new AseParameter() {ParameterName = "@psAccountType", AseDbType = AseDbType.VarChar, Value= AccountType},
					new AseParameter() {ParameterName = "@psAccountNo", AseDbType = AseDbType.VarChar, Value= AccountNo},
					new AseParameter() {ParameterName = "@pnHoldID", AseDbType = AseDbType.Integer, Value= HoldId},
					new AseParameter() {ParameterName = "@pnHoldAmt", AseDbType = AseDbType.Decimal, Value= HoldAmt},
					new AseParameter() {ParameterName = "@psUserName", AseDbType = AseDbType.VarChar, Value= Username},
					new AseParameter() {ParameterName = "@psHoldReason", AseDbType = AseDbType.VarChar, Value= HoldReason},


				};
				var dt = SqlDs("isp_tci_post3_hold", "TransactionHolding", sp);
				return dt;
			}
			catch (Exception ex)
			{

			}
			return null;

			// return 0;
		}

		//[WebMethod]
		//public DataSet TransactionPosting(string TransRef, string DrAccountNo, string DrAcctType,
		//    int DrAcctTC, string DrAcctNarration, decimal TranAmount, string CrAcctNo,
		//    string CrAcctType,
		//    int CrAcctTC, string CrAcctNarration, string CurrencyISO, string PostDate, string ValueDate, string UserName,
		//    int? ChequeNo, string SupervisorName, short? ChannelId, short? ForcePostFlag, short? Reversal,
		//    int? TranBatchID, int? ChargeCode, decimal? ChargeAmt, decimal? TaxAmt, string ParentTransRef, string RoutingNo, int? FloatDays,
		//    short? Direction, string ChargeAcct)
		//{

		//    //AseConnection c = new AseConnection(a);
		//    try
		//    {
		//        // c.Open();
		//        List<AseParameter> sp = new List<AseParameter>()
		//        {


		//            new AseParameter() {ParameterName = "@psTransactionRef", AseDbType = AseDbType.VarChar, Value= TransRef},
		//            new AseParameter() {ParameterName = "@psDrAcctNo", AseDbType = AseDbType.VarChar, Value= DrAccountNo},
		//            new AseParameter() {ParameterName = "@psDrAcctType", AseDbType = AseDbType.VarChar, Value= DrAcctType},
		//            new AseParameter() {ParameterName = "@pnDrAcctTC", AseDbType = AseDbType.Integer, Value= DrAcctTC},
		//            new AseParameter() {ParameterName = "@psDrAcctNarration", AseDbType = AseDbType.VarChar, Value= DrAcctNarration},
		//            new AseParameter() {ParameterName = "@pnTranAmount", AseDbType = AseDbType.Decimal, Value= TranAmount},
		//            new AseParameter() {ParameterName = "@psCrAcctNo", AseDbType = AseDbType.VarChar, Value= CrAcctNo},


		//            new AseParameter() {ParameterName = "@psCrAcctType", AseDbType = AseDbType.VarChar, Value= CrAcctType},
		//            new AseParameter() {ParameterName = "@pnCrAcctTC", AseDbType = AseDbType.Integer, Value= CrAcctTC},
		//            new AseParameter() {ParameterName = "@psCrAcctNarration", AseDbType = AseDbType.VarChar, Value= CrAcctNarration},
		//            new AseParameter() {ParameterName = "@psCurrencyISO", AseDbType = AseDbType.VarChar, Value= CurrencyISO},
		//            new AseParameter() {ParameterName = "@pdtPostDate", AseDbType = AseDbType.Date, Value= PostDate},
		//            new AseParameter() {ParameterName = "@pdtValueDate", AseDbType = AseDbType.Date, Value= ValueDate},
		//            new AseParameter() {ParameterName = "@psUserName", AseDbType = AseDbType.VarChar, Value= UserName},
		//            new AseParameter() {ParameterName = "@pnFloatDays", AseDbType = AseDbType.SmallInt, Value=FloatDays},
		//            new AseParameter() {ParameterName = "@psRoutingNo", AseDbType = AseDbType.VarChar, Value= RoutingNo},
		//            new AseParameter() {ParameterName = "@pnChequeNo", AseDbType = AseDbType.Integer, Value= ChequeNo},

		//            new AseParameter() {ParameterName = "@psSupervisorName", AseDbType = AseDbType.VarChar, Value= SupervisorName},
		//            new AseParameter() {ParameterName = "@pnChannelId", AseDbType = AseDbType.TinyInt, Value= ChannelId},
		//          //  new AseParameter() {ParameterName = "@pnRemoveOldFloat", AseDbType = AseDbType.TinyInt, Value= RemoveOldFloat},
		//            new AseParameter() {ParameterName = "@pnForcePostFlag", AseDbType = AseDbType.TinyInt, Value= ForcePostFlag},
		//            new AseParameter() {ParameterName = "@pnReversal", AseDbType = AseDbType.TinyInt, Value= Reversal},
		//            new AseParameter() {ParameterName = "@pnTranBatchID", AseDbType = AseDbType.Integer, Value= TranBatchID},
		//            new AseParameter() {ParameterName = "@pnChargeCode", AseDbType = AseDbType.Integer, Value= ChargeCode},
		//            new AseParameter() {ParameterName = "@pnChargeAmt", AseDbType = AseDbType.Decimal, Value= ChargeAmt},
		//            new AseParameter() {ParameterName = "@pnTaxAmt", AseDbType = AseDbType.Decimal, Value= TaxAmt},
		//            new AseParameter() {ParameterName = "@pnParentTransRef", AseDbType = AseDbType.VarChar, Value= ParentTransRef},
		//            new AseParameter() {ParameterName = "@pnDirection", AseDbType = AseDbType.Integer, Value= Direction},
		//            new AseParameter() {ParameterName = "@psChargeAcct", AseDbType = AseDbType.VarChar, Value= ChargeAcct},





		//        };
		//        var dt = SqlDs("isp_tci_post2", "TransactionPosting", sp);
		//        // start demo
		//        //var post = new List<PostingResponse>();
		//        //post.Add(new PostingResponse()
		//        //{
		//        //    CbsTranId = "11",
		//        //    nBalance = 50000,
		//        //    nBranch = "001",
		//        //    nErrorCode = 0,
		//        //    sErrorText = "",
		//        //    sName = "Testing static data",
		//        //    sStatus = "Active"
		//        //});
		//        //var dt = DatasetHelper.ToDataSet<PostingResponse>(post, "TransactionPosting");
		//        return dt;
		//    }
		//    catch (Exception ex)
		//    {

		//    }
		//    return null;

		//    // return 0;
		//}

		[WebMethod]
		public DataSet FundRelease(string TransactionRef, int pnInstrument, int Direction, string AccountType, string AccountNo)
		{

			//AseConnection c = new AseConnection(a);
			try
			{
				// c.Open();
				List<AseParameter> sp = new List<AseParameter>()
				{
					new AseParameter() {ParameterName = "@psTransactionRef", AseDbType = AseDbType.VarChar, Value= TransactionRef},
					new AseParameter() {ParameterName = "@pnDirection", AseDbType = AseDbType.Integer, Value= Direction},
					new AseParameter() {ParameterName = "@psAccountType", AseDbType = AseDbType.VarChar, Value= AccountType},
					new AseParameter() {ParameterName = "@psAccountNo", AseDbType = AseDbType.VarChar, Value= AccountNo},
					new AseParameter() {ParameterName = "@pnInstrument", AseDbType = AseDbType.Integer, Value= pnInstrument},


				};
				var dt = SqlDs("isp_tci_fund_release", "FundRelease", sp);
				return dt;
			}
			catch (Exception ex)
			{

			}
			return null;

			// return 0;
		}


        [WebMethod]
        public DataSet Mandate(string AccountNo, string AccountType)
        {

            //AseConnection c = new AseConnection(a);
            try
            {
                // c.Open();
                List<AseParameter> sp = new List<AseParameter>()
                {
                    new AseParameter() {ParameterName = "@psAcctType", AseDbType = AseDbType.VarChar, Value= AccountType},
                    new AseParameter() {ParameterName = "@psAcctNo", AseDbType = AseDbType.VarChar, Value= AccountNo},


                };
                var dt = SqlDs("isp_GetMandate", "Mandate", sp);
                return dt;
            }
            catch (Exception ex)
            {

            }
            return null;

            // return 0;
        }
        [WebMethod]
		public DataSet StartDay()
		{

			//AseConnection c = new AseConnection(a);
			try
			{

				var dt = SqlDs("isp_tci_startday", "StartDay", null);
				//var post = new List<ProcessingDateResponse>();
				//post.Add(new ProcessingDateResponse()
				//{
				//    /// CbsTranId = "11",

				//    nErrorCode = 0,
				//    sErrorText = "",
				//    dDate = DateTime.Now,

				//});
				//var dt = DatasetHelper.ToDataSet<ProcessingDateResponse>(post, "StartDay");
				return dt;
			}
			catch (Exception ex)
			{

			}
			return null;

			// return 0;
		}
		[WebMethod]
		public string TestCon()
		{

			//AseConnection c = new AseConnection(a);
			try
			{
                TechClearingContext ctx = new TechClearingContext();

                var de = ctx.admConnectionSetups.Where(x => x.Name == "sybconnectionEncrypt").FirstOrDefault();

                if (de != null)
                {
                    //string sSQLcon = System.Configuration.ConfigurationManager.ConnectionStrings["sybconnection"].ToString();

                    string connstring = System.Configuration.ConfigurationManager.ConnectionStrings["sybconnectionEncrypt"].ToString();

                    connstring = connstring.Replace("{{Data Source}}", de.DatabaseServer);

                    connstring = connstring.Replace("{{port}}", de.DatabasePort.ToString());

                    connstring = connstring.Replace("{{database}}", de.Databasename);

                    connstring = connstring.Replace("{{uid}}", de.DatabaseUserName);

                    //byte[] encData_byte = new byte[de.DatabasePassword.Length];
                    //encData_byte = System.Text.Encoding.UTF8.GetBytes(de.DatabasePassword);
                    //string encodedData = Convert.ToBase64String(encData_byte);


                    ////Decrypt Password

                    //////parse base64 string
                    ////byte[] data = Convert.FromBase64String(de.DatabasePassword);

                    //////decrypt data
                    ////byte[] decrypted = ProtectedData.Unprotect(data, null, Scope);
                    ////string pwd = Encoding.Unicode.GetString(decrypted);

                    System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();
                    System.Text.Decoder utf8Decode = encoder.GetDecoder();
                    byte[] todecode_byte = Convert.FromBase64String(de.DatabasePassword);
                    int charCount = utf8Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
                    char[] decoded_char = new char[charCount];
                    utf8Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
                    string result = new String(decoded_char);

                    connstring = connstring.Replace("{{pwd}}", result);

                    using (AseConnection theCons = new AseConnection(connstring))
                    {
                        DataSet ds = new DataSet();

                        theCons.Open();
                        return "connection success";
                    }
                }
                else
                {
                    return "connection failed";
                }
			}
			catch (Exception ex)
			{
				return ex.Message;
			}
			// return null;

			// return 0;
		}



		[WebMethod]
        public DataSet AuthenticateUser(string userName, string userPassword)
		{

			try
			{
                //SmartObject.SaveLog( "username" + userName);
                //SmartObject.SaveLog("Password" + userPassword);
				bool isdbuser = false;

				string ETHIXConString = System.Configuration.ConfigurationManager.ConnectionStrings["sybconnection1"].ToString();

				ETHIXConString = ETHIXConString.Replace("User Id=*****", "User Id=" + userName);
				ETHIXConString = ETHIXConString.Replace("Password=*****", "Password=" + userPassword);

				using (AseConnection theCons = new AseConnection(ETHIXConString))
				{

					try
					{
						DataSet ds = new DataSet();

                        theCons.Open();
                        isdbuser = true;


					}
					catch(Exception ex)
					{

						isdbuser = false;
                        SmartObject.SaveLog(userName + " Failed to Autheticate " + DateTime.Now);
                        return null;

					}

				}

				if (!isdbuser)
				{
					return null;
				}


				//string sql = "SELECT * FROM ad_gb_rsm WHERE user_name = @user_name";
				string sql = "SELECT EmployeeId = r.employee_id ,UserName = r.user_name ,FullName = r.name ,UserStatus = r.status ,BranchNo = r.branch_no FROM ad_gb_rsm r WHERE r.user_name = @user_name";
				sql = sql.Replace("@user_name", "'" + userName + "'");


				var td = SqlDsAuthUsername(sql, "Autheticate", null, ETHIXConString);


				return td;

			}
			catch (Exception)
			{



			}

			return null;
		}

        [WebMethod]
        public DataSet ValidateCheque(string accountNo, string accountType, string chequeNo)
        {

            //AseConnection c = new AseConnection(a);
            try
            {
                // c.Open();
                List<AseParameter> sp = new List<AseParameter>()
				{
					new AseParameter() {ParameterName = "@psAcctNo", AseDbType = AseDbType.VarChar, Value= accountNo},
					new AseParameter() {ParameterName = "@psAcctType", AseDbType = AseDbType.VarChar, Value= accountType},
					new AseParameter() {ParameterName = "@pnChqNo", AseDbType = AseDbType.Integer, Value= chequeNo},


				};
                var dt = SqlDs("isp_validateChqNo", "ValidateCheque", sp);
                return dt;
            }
            catch (Exception ex)
            {

            }
            return null;

            // return 0;
        }



        [WebMethod]
        public DataSet ValidateUser(string userName)
        {

            try
            {
                //string sql = "SELECT * FROM ad_gb_rsm WHERE user_name = @user_name";
                string sql = "SELECT EmployeeId = r.employee_id ,UserName = r.user_name ,Email = r.email,FullName = r.name ,UserStatus = r.status ,BranchNo = r.branch_no FROM phoenix..ad_gb_rsm r WHERE r.user_name = @user_name";
                sql = sql.Replace("@user_name", "'" + userName + "'");

                var dt = SqlDs(sql, "ValidateUser", null);
                return dt;

            }
            catch (Exception ex)
            {

            }

            return null;
        }







        [WebMethod]
        public DataSet GetAcctMemo(string AcctNo, string AcctType)
        {

            //AseConnection c = new AseConnection(a);
            try
            {
                // c.Open();
                List<AseParameter> sp = new List<AseParameter>()
                {
                    new AseParameter() {ParameterName = "@psAcctNo", AseDbType = AseDbType.VarChar, Value= AcctNo},
                    new AseParameter() {ParameterName = "@psAcctType", AseDbType = AseDbType.VarChar, Value= AcctType},
                };
                var dt = SqlDsTest("isp_AcctMemo", "GetMemo", sp,0);
                return dt;

            }
            catch (Exception ex)
            {

            }
            return null;

        }

        [WebMethod]
        public DataSet GetUserLimits(string Username, string Currency)
        {

            //AseConnection c = new AseConnection(a);
            try
            {
                // c.Open();
                List<AseParameter> sp = new List<AseParameter>()
                {
                    new AseParameter() {ParameterName = "@psUserName", AseDbType = AseDbType.VarChar, Value= Username},
                    new AseParameter() {ParameterName = "@psIsoCurrency", AseDbType = AseDbType.VarChar, Value= Currency},
                };
                var dt = SqlDs("isp_RsmLimits", "GetLimits", sp);
                return dt;

            }
            catch (Exception ex)
            {

            }
            return null;

        }

		public class UserAccount
		{

			public bool IsAuthentic { get; set; }
			public int EmployeeId { get; set; }
			public string UserName { get; set; }
			public string FullName { get; set; }
			public int BranchNo { get; set; }
			public DateTime LastLoginAt { get; set; }



		}
	}

}