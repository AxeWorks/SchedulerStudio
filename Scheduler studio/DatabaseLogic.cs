using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Windows;

namespace Scheduler_studio
{
     public static class DBStudio
    {
        #region WORKER
        //Janne
        public static DataTable GetAllWorkersData()
        {
            try
            {
                DataTable dt = new DataTable();
                using (SQLiteConnection conn = new SQLiteConnection(Scheduler_studio.Properties.Settings.Default.ConnectionString))
                {
                    conn.Open();
                    string sqlString = "SELECT * FROM worker";
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

        //Janne
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
        #endregion
        #region RESERVATION
        //Janne
        public static DataTable GetReservations()
        {
            try
            {
                DataTable dt = new DataTable();
                using (SQLiteConnection conn = new SQLiteConnection(Scheduler_studio.Properties.Settings.Default.ConnectionString))
                {
                    conn.Open();
                    string sqlString = "SELECT reservation.*, customer.Privilege FROM reservation LEFT OUTER JOIN customer ON customer.PKey = reservation.RegCustomer";

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
        //Janne
        public static int UpdateReservations(DataTable table)
        {
            try
            {
                int count = 0;
                using (SQLiteConnection conn = new SQLiteConnection(Scheduler_studio.Properties.Settings.Default.ConnectionString))
                {
                    conn.Open();

                    SQLiteDataAdapter da = new SQLiteDataAdapter();
                    da.UpdateCommand = new SQLiteCommand("UPDATE reservation SET Service = @Service, UnregCustomer = @UnregCustomer, ReservationTime = @ReservationTime, ReservationDate = @ReservationDate, RegCustomer = @RegCustomer, Employee = @Employee WHERE PKey = @PKey", conn);
                    da.DeleteCommand = new SQLiteCommand("DELETE FROM reservation WHERE PKey = @PKey", conn);

                    SQLiteParameter paramA = da.UpdateCommand.Parameters.Add("@Service", DbType.String, 100, "Service");
                    paramA.SourceVersion = DataRowVersion.Current;
                    SQLiteParameter paramB = da.UpdateCommand.Parameters.Add("@UnregCustomer", DbType.String, 50, "UnregCustomer");
                    paramB.SourceVersion = DataRowVersion.Current;
                    SQLiteParameter paramD = da.UpdateCommand.Parameters.Add("@ReservationTime", DbType.String, 13, "ReservationTime");
                    paramD.SourceVersion = DataRowVersion.Current;
                    SQLiteParameter paramE = da.UpdateCommand.Parameters.Add("@ReservationDate", DbType.String, 13, "ReservationDate");
                    paramE.SourceVersion = DataRowVersion.Current;
                    SQLiteParameter paramF = da.UpdateCommand.Parameters.Add("@RegCustomer", DbType.Int32, 100000, "RegCustomer");
                    paramF.SourceVersion = DataRowVersion.Current;
                    SQLiteParameter paramG = da.UpdateCommand.Parameters.Add("@Employee", DbType.Int32, 100000, "Employee");
                    paramF.SourceVersion = DataRowVersion.Current;
                    SQLiteParameter paramH = da.UpdateCommand.Parameters.Add("@PKey", DbType.Int32, 100000, "PKey");

                    da.DeleteCommand = new SQLiteCommand("DELETE FROM reservation WHERE PKey = @PKey", conn);

                    da.DeleteCommand.Parameters.Add("@PKey", DbType.Int32, 100000, "PKey");

                    count = da.Update(table);
                    conn.Close();
                }
                return count;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //Janne
        public static int DeleteReservation(int pkey)
        {
            try
            {
                int rowcount;
                using (SQLiteConnection conn = new SQLiteConnection(Scheduler_studio.Properties.Settings.Default.ConnectionString))
                {
                    conn.Open();
                    string sqlString = string.Format("DELETE FROM reservation WHERE PKey = {0}", pkey);
                    SQLiteCommand command = new SQLiteCommand(sqlString, conn);
                    rowcount = command.ExecuteNonQuery();

                    conn.Close();
                }
                return rowcount;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //Janne
        public static int InsertReservation(string operation, string ResTime, string ResDate, string unregcustomer, Nullable<int> regcustomer, int employee)
        {
            try
            {
                int count = 0;
                using (SQLiteConnection conn = new SQLiteConnection(Scheduler_studio.Properties.Settings.Default.ConnectionString))
                {
                    conn.Open();

                    string sqlString = string.Format("INSERT INTO reservation (Service, ReservationTime, ReservationDate, UnregCustomer, RegCustomer, Employee) VALUES (@Service, @ReservationTime, @ReservationDate, @UnregCustomer, @RegCustomer, {0})", employee);
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

                    param = new SQLiteParameter("@RegCustomer", DbType.String, "RegCustomer");
                    param.Value = regcustomer;
                    command.Parameters.Add(param);

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
        #endregion
        #region CUSTOMER
        //Aleksi
        public static DataTable GetCustomers()
        {
            try
            {
                DataTable dt = new DataTable();
                using (SQLiteConnection conn = new SQLiteConnection(Scheduler_studio.Properties.Settings.Default.ConnectionString))
                {
                    conn.Open();
                    string sqlString = "SELECT * FROM customer";
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
        // Aleksi
        public static DataTable getCustomerNames()
        {

            try
            {
                DataTable dt = new DataTable();
                using (SQLiteConnection conn = new SQLiteConnection(Scheduler_studio.Properties.Settings.Default.ConnectionString))
                {
                    conn.Open();
                    string sqlString = "SELECT PKey, Fname, Lname FROM customer";
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

        //Aleksi
        public static int UpdateCustomer(DataTable dt)
        {
            try
            {
                int rowcount;
                using (SQLiteConnection conn = new SQLiteConnection(Scheduler_studio.Properties.Settings.Default.ConnectionString))
                {
                    conn.Open();
                    SQLiteDataAdapter da = new SQLiteDataAdapter();
                    da.InsertCommand = new SQLiteCommand("INSERT INTO customer (Fname, Lname, Phone, Birthdate, Privilege, RegDate) VALUES (@Fname, @Lname, @Phone, @BirthDate, @Privilege, @RegDate)", conn);

                    da.InsertCommand.Parameters.Add("@Fname", DbType.String, 20, "Fname");
                    da.InsertCommand.Parameters.Add("@Lname", DbType.String, 30, "Lname");
                    da.InsertCommand.Parameters.Add("@Phone", DbType.String, 20, "Phone");
                    da.InsertCommand.Parameters.Add("@Birthdate", DbType.String, 80, "Birthdate");
                    da.InsertCommand.Parameters.Add("@Privilege", DbType.String, 80, "Privilege");
                    da.InsertCommand.Parameters.Add("@RegDate", DbType.String, 10, "RegDate");

                    da.UpdateCommand = new SQLiteCommand("UPDATE customer SET Fname = @newFname, Lname = @newLname, Phone = @newPhone, Birthdate = @newBirthdate, Privilege = @newPrivilege " +
                        "WHERE PKey = @PKey", conn);

                    SQLiteParameter paramA = da.UpdateCommand.Parameters.Add("@newFname", DbType.String, 20, "Fname");
                    paramA.SourceVersion = DataRowVersion.Current;
                    SQLiteParameter paramB = da.UpdateCommand.Parameters.Add("@newLname", DbType.String, 30, "Lname");
                    paramB.SourceVersion = DataRowVersion.Current;
                    SQLiteParameter paramD = da.UpdateCommand.Parameters.Add("@newPhone", DbType.String, 80, "Phone");
                    paramD.SourceVersion = DataRowVersion.Current;
                    SQLiteParameter paramE = da.UpdateCommand.Parameters.Add("@newBirthdate", DbType.String, 20, "Birthdate");
                    paramE.SourceVersion = DataRowVersion.Current;
                    SQLiteParameter paramF = da.UpdateCommand.Parameters.Add("@newPrivilege", DbType.String, 100, "Privilege");
                    paramF.SourceVersion = DataRowVersion.Current;
                    SQLiteParameter paramG = da.UpdateCommand.Parameters.Add("@PKey", DbType.Int32, 100000, "PKey");

                    da.DeleteCommand = new SQLiteCommand("DELETE FROM customer WHERE PKey = @PKey", conn);

                    da.DeleteCommand.Parameters.Add("@PKey", DbType.Int32, 100000, "PKey");

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
        #endregion
        #region NOTE
        //Janne
        public static DataTable GetNotes()
        {
            try
            {
                DataTable dt = new DataTable();
                using (SQLiteConnection conn = new SQLiteConnection(Scheduler_studio.Properties.Settings.Default.ConnectionString))
                {
                    conn.Open();
                    string sqlString = "SELECT notebook.Note, notebook.Employee, worker.Fname, worker.Lname FROM notebook JOIN worker ON notebook.Employee = worker.PKey";
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
        //Janne
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
        //Janne
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

        #endregion
    }
}
