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
     public static class DBStudio
    {

        public static DataTable GetNotes()
        {
            try
            {
                DataTable dt = new DataTable();
                using (SQLiteConnection conn = new SQLiteConnection(Scheduler_studio.Properties.Settings.Default.ConnectionString))
                {
                    conn.Open();
                    string sqlString = "SELECT notebook.Note, notebook.Employee, worker.Fname, worker.Lname FROM notebook JOIN worker ON notebook.Employee = worker.PKey";
                   // SQLiteCommand command = new SQLiteCommand(sqlString, conn);
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

        public static int SaveNote(string msg, int FKey)
        {
            try
            {
                int count;
                using (SQLiteConnection conn = new SQLiteConnection(Scheduler_studio.Properties.Settings.Default.ConnectionString))
                {
                    conn.Open();
                    string sqlString = string.Format("INSERT INTO notebook (Note, Employee) VALUES (@Note, {0})", FKey);

                    SQLiteCommand command = new SQLiteCommand(sqlString, conn);
                    SQLiteParameter param = new SQLiteParameter("Note", DbType.String);
                    param.Value = msg;
                    command.Parameters.Add(param);

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


        public static int DeleteNote(string msg, int FKey)
        {
            try
            {
                int count;
                using (SQLiteConnection conn = new SQLiteConnection(Scheduler_studio.Properties.Settings.Default.ConnectionString))
                {
                    conn.Open();
                    string sqlString = string.Format("DELETE FROM notebook WHERE (Note = @Note) AND (Employee = {0})", FKey);

                    SQLiteCommand command = new SQLiteCommand(sqlString, conn);
                    SQLiteParameter param = new SQLiteParameter("Note", DbType.String);
                    param.Value = msg;
                    command.Parameters.Add(param);

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


        public static DataTable GetAllWorkersData()
        {
            try
            {
                DataTable dt = new DataTable();
                using (SQLiteConnection conn = new SQLiteConnection(Scheduler_studio.Properties.Settings.Default.ConnectionString))
                {
                    conn.Open();
                    string sqlString = "SELECT * FROM worker";
                  //  SQLiteCommand command = new SQLiteCommand(sqlString, conn);
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
        
        public static int UpdateWorker(DataTable dt)
        {
            try
            {
                int rowcount;       
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
                        "WHERE PKey = @PKey", conn);

                   /* SQLiteParameter param1 = da.UpdateCommand.Parameters.Add("@oldFname", DbType.String, 20, "Fname");
                    param1.SourceVersion = DataRowVersion.Original;
                    SQLiteParameter param2 = da.UpdateCommand.Parameters.Add("@oldLname", DbType.String, 30, "Lname");
                    param2.SourceVersion = DataRowVersion.Original;
                    SQLiteParameter param3 = da.UpdateCommand.Parameters.Add("@oldFullName", DbType.String, 100, "FullName");
                    param3.SourceVersion = DataRowVersion.Original;
                    SQLiteParameter param4 = da.UpdateCommand.Parameters.Add("@oldAddr", DbType.String, 80, "Addr");
                    param4.SourceVersion = DataRowVersion.Original;
                    SQLiteParameter param5 = da.UpdateCommand.Parameters.Add("@oldPhone", DbType.String, 20, "Phone");
                    param5.SourceVersion = DataRowVersion.Original;
                    SQLiteParameter param6 = da.UpdateCommand.Parameters.Add("@oldOther", DbType.String, 100, "Other");
                    param6.SourceVersion = DataRowVersion.Original;*/



                    SQLiteParameter paramA = da.UpdateCommand.Parameters.Add("@newFname", DbType.String, 20, "Fname");
                    paramA.SourceVersion = DataRowVersion.Current;
                    SQLiteParameter paramB = da.UpdateCommand.Parameters.Add("@newLname", DbType.String, 30, "Lname");
                    paramB.SourceVersion = DataRowVersion.Current;
                    SQLiteParameter paramD = da.UpdateCommand.Parameters.Add("@newAddr", DbType.String, 80, "Addr");
                    paramD.SourceVersion = DataRowVersion.Current;
                    SQLiteParameter paramE = da.UpdateCommand.Parameters.Add("@newPhone", DbType.String, 20, "Phone");
                    paramE.SourceVersion = DataRowVersion.Current;
                    SQLiteParameter paramF = da.UpdateCommand.Parameters.Add("@newOther", DbType.String, 100, "Other");
                    paramF.SourceVersion = DataRowVersion.Current;
                    SQLiteParameter paramG = da.UpdateCommand.Parameters.Add("@PKey", DbType.Int32, 100000, "PKey");

                    da.DeleteCommand = new SQLiteCommand("DELETE FROM worker WHERE PKey = @PKey", conn);

                    da.DeleteCommand.Parameters.Add("@PKey", DbType.Int32, 100000, "PKey");

                    /* da.DeleteCommand.Parameters.Add("@Fname", DbType.String, 20, "Fname");
                     da.DeleteCommand.Parameters.Add("@Lname", DbType.String, 30, "Lname");
                     da.DeleteCommand.Parameters.Add("@Addr", DbType.String, 80, "Addr");
                     da.DeleteCommand.Parameters.Add("@Phone", DbType.String, 20, "Phone");
                     da.DeleteCommand.Parameters.Add("@RegDate", DbType.String, 10, "RegDate");
                     da.DeleteCommand.Parameters.Add("@Other", DbType.String, 100, "Other");*/

                    rowcount = da.Update(dt);
                    conn.Close();
                    
                }
                return rowcount;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static DataTable GetReservations()
        {
            try
            {
                DataTable dt = new DataTable();
                using (SQLiteConnection conn = new SQLiteConnection(Scheduler_studio.Properties.Settings.Default.ConnectionString))
                {
                    conn.Open();
                    string sqlString = "SELECT r.Service, r.Employee, r.ReservationTime, r.ReservationDate, r.RegCustomer, r.UnregCustomer FROM reservation as r JOIN worker as w ON r.Employee = w.PKey JOIN customer as c ON r.RegCustomer = c.PKey";
                   // SQLiteCommand command = new SQLiteCommand(sqlString, conn);
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

        public static int InsertReservation(string operation, string ResTime, string ResDate, string unregcustomer, int regcustomer, int employee)
        {
            try
            {
                int count = 0;
                using (SQLiteConnection conn = new SQLiteConnection(Scheduler_studio.Properties.Settings.Default.ConnectionString))
                {
                    conn.Open();

                    string sqlString = string.Format("INSERT INTO reservation (Service, ReservationTime, ReservationDate, UnregCustomer, RegCustomer, Employee) VALUES (@Service, @ReservationTime, @ReservationDate, @UnregCustomer, {0}, {1})", regcustomer, employee);
                    SQLiteCommand command = new SQLiteCommand(sqlString, conn);

                    SQLiteParameter param;

                    param = new SQLiteParameter("@Service", DbType.String, "Service");
                    param.Value = operation;
                    command.Parameters.Add(param);

                    param = new SQLiteParameter("@ReservationTime", DbType.String, "ReservationTime");
                    param.Value = ResTime;
                    command.Parameters.Add(param);

                    param = new SQLiteParameter("@ReservationDate", DbType.String, "ReservationDate");
                    param.Value = ResDate;
                    command.Parameters.Add(param);

                    param = new SQLiteParameter("@UnregCustomer", DbType.String, "UnregCustomer");
                    param.Value = unregcustomer;
                    command.Parameters.Add(param);

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


        public static DataTable GetCustomers()
        {
            try
            {
                DataTable dt = new DataTable();
                using (SQLiteConnection conn = new SQLiteConnection(Scheduler_studio.Properties.Settings.Default.ConnectionString))
                {
                    conn.Open();
                    string sqlString = "SELECT * FROM customer";
                    // SQLiteCommand command = new SQLiteCommand(sqlString, conn);
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
        /* public static int AddWorker(string Fname, string Lname, string Addr, string Phone, DateTime RegDate, string Other)
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
         }*/
    }
}
