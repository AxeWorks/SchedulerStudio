using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace Scheduler_studio
{
     public static class DBWorker
    {
        public static DataTable GetAllWorkersData()
        {
            try
            {
                DataTable dt = new DataTable();
                using (SQLiteConnection conn = new SQLiteConnection(Scheduler_studio.Properties.Settings.Default.ConnectionString))
                {
                    conn.Open();
                    string sqlString = "SELECT * FROM worker";
                    SQLiteCommand command = new SQLiteCommand(sqlString, conn);
                    SQLiteDataAdapter da = new SQLiteDataAdapter(sqlString, conn);
                    
                    da.Fill(dt);
                    conn.Close();
                }                
                return dt;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        
        public static void UpdateWorker(DataTable dt)
        {
            try
            {                
                using (SQLiteConnection conn = new SQLiteConnection(Scheduler_studio.Properties.Settings.Default.ConnectionString))
                {
                    conn.Open();
                    SQLiteDataAdapter da = new SQLiteDataAdapter();
                    da.InsertCommand = new SQLiteCommand("INSERT INTO worker (Fname, Lname, Addr, Phone, RegDate, Other) VALUES (@Fname, @Lname, @Addr, @Phone, @RegDate, @Other)", conn);


                    da.InsertCommand.Parameters.Add("@Fname", DbType.String, 20, "Fname");
                    da.InsertCommand.Parameters.Add("@Lname", DbType.String, 30, "Lname");
                    da.InsertCommand.Parameters.Add("@Addr", DbType.String, 80, "Addr");
                    da.InsertCommand.Parameters.Add("@Phone", DbType.String, 20, "Phone");
                    da.InsertCommand.Parameters.Add("@RegDate", DbType.String, 10, "RegDate");
                    da.InsertCommand.Parameters.Add("@Other", DbType.String, 100, "Other");


                    da.UpdateCommand = new SQLiteCommand("UPDATE worker SET Fname = @newFname, Lname = @newLname, Addr = @newAddr, Phone = @newPhone, Other = @newOther " +
                        "WHERE (Fname = @oldFname) AND (Lname = @oldLname) AND (Addr = @oldAddr) AND (Phone = @oldPhone) AND (Other = @oldOther)", conn);

                    SQLiteParameter param1 = da.UpdateCommand.Parameters.Add("@oldFname", DbType.String, 20, "Fname");
                    param1.SourceVersion = DataRowVersion.Original;
                    SQLiteParameter param2 = da.UpdateCommand.Parameters.Add("@oldLname", DbType.String, 30, "Lname");
                    param2.SourceVersion = DataRowVersion.Original;
                    SQLiteParameter param3 = da.UpdateCommand.Parameters.Add("@oldAddr", DbType.String, 80, "Addr");
                    param3.SourceVersion = DataRowVersion.Original;
                    SQLiteParameter param4 = da.UpdateCommand.Parameters.Add("@oldPhone", DbType.String, 20, "Phone");
                    param4.SourceVersion = DataRowVersion.Original;
                    SQLiteParameter param5 = da.UpdateCommand.Parameters.Add("@oldOther", DbType.String, 100, "Other");
                    param5.SourceVersion = DataRowVersion.Original;

                    SQLiteParameter paramA = da.UpdateCommand.Parameters.Add("@newFname", DbType.String, 20, "Fname");
                    paramA.SourceVersion = DataRowVersion.Current;
                    SQLiteParameter paramB = da.UpdateCommand.Parameters.Add("@newLname", DbType.String, 30, "Lname");
                    paramB.SourceVersion = DataRowVersion.Current;
                    SQLiteParameter paramC = da.UpdateCommand.Parameters.Add("@newAddr", DbType.String, 80, "Addr");
                    paramC.SourceVersion = DataRowVersion.Current;
                    SQLiteParameter paramD = da.UpdateCommand.Parameters.Add("@newPhone", DbType.String, 20, "Phone");
                    paramD.SourceVersion = DataRowVersion.Current;
                    SQLiteParameter paramE = da.UpdateCommand.Parameters.Add("@newOther", DbType.String, 100, "Other");
                    paramE.SourceVersion = DataRowVersion.Current;

                    da.DeleteCommand = new SQLiteCommand("DELETE FROM worker WHERE (Fname IS @Fname) AND (Lname IS @Lname) AND (Addr IS @Addr) AND (Phone IS @Phone) AND (RegDate IS @RegDate) AND (Other IS @Other)", conn);

                    da.DeleteCommand.Parameters.Add("@Fname", DbType.String, 20, "Fname");
                    da.DeleteCommand.Parameters.Add("@Lname", DbType.String, 30, "Lname");
                    da.DeleteCommand.Parameters.Add("@Addr", DbType.String, 80, "Addr");
                    da.DeleteCommand.Parameters.Add("@Phone", DbType.String, 20, "Phone");
                    da.DeleteCommand.Parameters.Add("@RegDate", DbType.String, 10, "RegDate");
                    da.DeleteCommand.Parameters.Add("@Other", DbType.String, 100, "Other");

                    da.Update(dt);
                    conn.Close();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static int AddWorker(string Fname, string Lname, string Addr, string Phone, DateTime RegDate, string Other)
        {
            try
            {
                string date = RegDate.Year + "-" + RegDate.Month + "-" + RegDate.Day;
                int count;
                using (SQLiteConnection conn = new SQLiteConnection(Scheduler_studio.Properties.Settings.Default.ConnectionString))
                {
                    conn.Open();
                    string sqlString = 
                        "INSERT INTO worker (Fname, Lname, Addr, Phone, RegDate, Other) VALUES ('"+Fname+"','"+Lname+"','"+Addr+"','"+Phone+"','"+date+"','"+Other+"')";

                    SQLiteCommand command = new SQLiteCommand(sqlString, conn);
                    /*SQLiteParameter param;

                    param = new SQLiteParameter("fname", SqlDbType.NVarChar);
                    param.Value = Fname;
                    command.Parameters.Add(param);

                    param = new SQLiteParameter("lname", SqlDbType.NVarChar);
                    param.Value = Lname;
                    command.Parameters.Add(param);

                    param = new SQLiteParameter("address", SqlDbType.NVarChar);
                    param.Value = Addr;
                    command.Parameters.Add(param);

                    param = new SQLiteParameter("phone", SqlDbType.NVarChar);
                    param.Value = Phone;
                    command.Parameters.Add(param);

                    param = new SQLiteParameter("date", SqlDbType.NVarChar);
                    param.Value = date;
                    command.Parameters.Add(param);

                    param = new SQLiteParameter("other", SqlDbType.NVarChar);
                    param.Value = Other;
                    command.Parameters.Add(param);*/

                    // Tutki kannattaako käyttää .executenonqueryasync
                    count = command.ExecuteNonQuery();
                    conn.Close();
                }
                return count;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void RemoveWorker(string Fname, string Lname, string Addr, string Phone, DateTime RegDate, string Other)
        {
            try
            {
                string otherinfo;
                if(Other == "")
                {
                    otherinfo = null;
                }
                else
                {
                    otherinfo = Other;
                }
                string date = RegDate.Year + "-" + RegDate.Month + "-" + RegDate.Day;
                using (SQLiteConnection conn = new SQLiteConnection(Scheduler_studio.Properties.Settings.Default.ConnectionString))
                {
                    conn.Open();
                    string sqlString = "DELETE FROM worker WHERE (Fname IS '" + Fname + "') AND (Lname IS '" + Lname + "') AND (Addr IS '" + Addr + "') AND (Phone IS '" + Phone + "') AND (RegDate IS '" + date + "') AND (Other IS '" + otherinfo + "')";
                    SQLiteCommand command = new SQLiteCommand(sqlString, conn);
                    command.ExecuteNonQuery();
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
