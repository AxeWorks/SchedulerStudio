using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Diagnostics;

namespace Scheduler_studio
{
     public static class DBWorker
    {
        public static DataTable GetAllWorkersData(int tableIdentifier)
        {
            try
            {
                DataTable dt = new DataTable();
                using (SQLiteConnection conn = new SQLiteConnection(Scheduler_studio.Properties.Settings.Default.ConnectionString))
                {
                    string sqlString;
                    conn.Open();
                    if (tableIdentifier == 1) {                                         //worker table
                        sqlString = "SELECT * FROM worker";
                    } else if (tableIdentifier == 2) {
                        sqlString = "SELECT * FROM reservation";                        //reservation table
                    } else {
                        sqlString = "";                                                 //default
                    }

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

        public static void UpdateWorker(DataTable dt, int tableIdentifier)
        {
            try
            {
                //caller indicates the table we're working with, and at the same time defines the amount of columns to be used. 6 for worker, x for reservation, x for notepad
               
                
                SQLiteDataAdapter da = new SQLiteDataAdapter();
                using (SQLiteConnection conn = new SQLiteConnection(Scheduler_studio.Properties.Settings.Default.ConnectionString))
                {
                    string[] columns;
                    string[] data;
                    string[] oldData;
                    string[] newData;

                    switch (tableIdentifier) {
                        case 1:
                            //if worker table
                            Trace.WriteLine("CASE 1 WORKER TABLE!! TableIdentifier: " + tableIdentifier);

                            columns = new string[] { "Fname", "Lname", "Addr", "Phone", "RegDate", "Other" };
                            data = new string[] { "@Fname", "@Lname", "@Addr", "@Phone", "@RegDate", "@Other" };
                            oldData = new string[] { "@oldFname", "@oldLname", "@oldAddr", "@oldPhone", "@oldRegDate", "@oldOther" };
                            newData = new string[] { "@newFname", "@newLname", "@newAddr", "@newPhone", "@newRegDate", "@newOther" };

                            da.InsertCommand = new SQLiteCommand("INSERT INTO worker (Fname, Lname, Addr, Phone, RegDate, Other) VALUES (@Fname, @Lname, @Addr, @Phone, @RegDate, @Other)", conn);

                            da.UpdateCommand = new SQLiteCommand("UPDATE worker SET Fname = @newFname, Lname = @newLname, Addr = @newAddr, Phone = @newPhone, Other = @newOther " +
                                "WHERE (Fname = @oldFname) AND (Lname = @oldLname) AND (Addr = @oldAddr) AND (Phone = @oldPhone) AND (Other = @oldOther)", conn);

                            da.DeleteCommand = new SQLiteCommand("DELETE FROM worker WHERE (Fname IS @Fname) AND (Lname IS @Lname) AND (Addr IS @Addr) AND (Phone IS @Phone) AND (RegDate IS @RegDate) AND (Other IS @Other)", conn);
                            break;
                        case 2:
                            //if reservation table

                            Trace.WriteLine("CASE 2 RESERVATION TABLE!!");

                            columns = new string[] { "Operation", "ReservationTime", "Employee", "RegCustomer", "UnregCustomer" };
                            data = new string[] { "@Operation", "@ReservationTime", "@Employee", "@RegCustomer", "@UnregCustomer" };
                            oldData = new string[] { "@oldOperation", "@oldReservationTime", "@oldEmployee", "@oldRegCustomer", "@oldUnregCustomer" };
                            newData = new string[] { "@newOperation", "@newReservationTime", "@newEmployee", "@newRegCustomer", "@newUnregCustomer" };

                            //Virhe ajettaessa
                            da.InsertCommand = new SQLiteCommand("INSERT INTO reservation (Operation, ReservationTime, Employee, RegCustomer, UnregCustomer) VALUES (@Operation, @ReservationTime, @Employee, @RegCustomer, @UnregCustomer)", conn);

                            da.UpdateCommand = new SQLiteCommand("UPDATE reservation SET ReservationTime = @newReservationTime, Employee = @newEmployee, RegCustomer = @newRegCustomer, UnregCustomer = @newUnregCustomer" +
                                "WHERE (Operation = @oldOperation) AND (ReservationTime = @oldReservationTime) AND (Employee = @oldEmployee) AND (RegCustomer = @oldRegCustomer) AND (UnregCustomer = @o)", conn);

                            da.DeleteCommand = new SQLiteCommand("DELETE FROM reservation WHERE (Operation IS @Operation) AND (ReservationTime IS @ReservationTime) AND (Employee IS @Employee) AND (RegCustomer IS @RegCustomer) AND (UnregCustomer IS @UnregCustomer)", conn);
                            break;
                        default:
                            columns = new string[0];
                            data = new string[0];
                            oldData = new string[0];
                            newData = new string[0];
                            break;  
                    }

                    conn.Open();

                    #region insert

                    da.InsertCommand.Parameters.Add(data[0], DbType.String, 20, columns[0]);
                    da.InsertCommand.Parameters.Add(data[1], DbType.String, 30, columns[1]);
                    da.InsertCommand.Parameters.Add(data[2], DbType.String, 80, columns[2]);
                    da.InsertCommand.Parameters.Add(data[3], DbType.String, 20, columns[3]);
                    da.InsertCommand.Parameters.Add(data[4], DbType.String, 10, columns[4]);

                    if (tableIdentifier == 1) {
                        da.InsertCommand.Parameters.Add(data[5], DbType.String, 40, columns[5]);
                    }
                    #endregion

                    #region update

                    SQLiteParameter param1 = da.UpdateCommand.Parameters.Add(oldData[0], DbType.String, 20, columns[0]);
                    param1.SourceVersion = DataRowVersion.Original;
                    Trace.WriteLine("PARAM1: old: " + oldData[0] + ", column: " + columns[0]);

                    SQLiteParameter param2 = da.UpdateCommand.Parameters.Add(oldData[1], DbType.String, 30, columns[1]);
                    param2.SourceVersion = DataRowVersion.Original;
                    Trace.WriteLine("PARAM2: old: " + oldData[1] + ", column: " + columns[2]);

                    SQLiteParameter param3 = da.UpdateCommand.Parameters.Add(oldData[2], DbType.String, 80, columns[2]);
                    param3.SourceVersion = DataRowVersion.Original;
                    Trace.WriteLine("PARAM3: old: " + oldData[2] + ", column: " + columns[2]);

                    SQLiteParameter param4 = da.UpdateCommand.Parameters.Add(oldData[3], DbType.String, 20, columns[3]);
                    param4.SourceVersion = DataRowVersion.Original;
                    Trace.WriteLine("PARAM4: old: " + oldData[3] + ", column: " + columns[3]);

                    if (tableIdentifier == 1) {
                        SQLiteParameter param5 = da.UpdateCommand.Parameters.Add(oldData[5], DbType.String, 40, columns[5]);
                        param5.SourceVersion = DataRowVersion.Original;
                        Trace.WriteLine("PARAM5: old: " + oldData[5] + ", column: " + columns[5]);
                    } else {
                        SQLiteParameter param5 = da.UpdateCommand.Parameters.Add(oldData[4], DbType.String, 40, columns[4]);
                        param5.SourceVersion = DataRowVersion.Original;
                        Trace.WriteLine("PARAM5: old: " + oldData[4] + ", column: " + columns[4]);
                    }

                    SQLiteParameter paramA = da.UpdateCommand.Parameters.Add(newData[0], DbType.String, 20, columns[0]);
                    paramA.SourceVersion = DataRowVersion.Current;
                    Trace.WriteLine("PARAMA: new: " + newData[0] + ", column: " + columns[0]);

                    SQLiteParameter paramB = da.UpdateCommand.Parameters.Add(newData[1], DbType.String, 30, columns[1]);
                    paramB.SourceVersion = DataRowVersion.Current;
                    Trace.WriteLine("PARAMB: new: " + newData[1] + ", column: " + columns[1]);

                    SQLiteParameter paramC = da.UpdateCommand.Parameters.Add(newData[2], DbType.String, 80, columns[2]);
                    paramC.SourceVersion = DataRowVersion.Current;
                    Trace.WriteLine("PARAMC: new: " + newData[2] + ", column: " + columns[2]);

                    SQLiteParameter paramD = da.UpdateCommand.Parameters.Add(newData[3], DbType.String, 20, columns[3]);
                    paramD.SourceVersion = DataRowVersion.Current;
                    Trace.WriteLine("PARAMD: new: " + newData[3] + ", column: " + columns[3]);

                    if (tableIdentifier == 1) {
                        SQLiteParameter paramE = da.UpdateCommand.Parameters.Add(newData[5], DbType.String, 40, columns[5]);
                        paramE.SourceVersion = DataRowVersion.Current;
                        Trace.WriteLine("PARAME: new: " + newData[5] + ", column: " + columns[5]);
                    } else {
                        SQLiteParameter paramE = da.UpdateCommand.Parameters.Add(newData[4], DbType.String, 40, columns[4]);
                        paramE.SourceVersion = DataRowVersion.Current;
                        Trace.WriteLine("PARAME: new: " + newData[4] + ", column: " + columns[4]);
                    }
                    

                    #endregion

                    #region delete

                    da.DeleteCommand.Parameters.Add(data[0], DbType.String, 20, columns[0]);
                    da.DeleteCommand.Parameters.Add(data[1], DbType.String, 30, columns[1]);
                    da.DeleteCommand.Parameters.Add(data[2], DbType.String, 80, columns[2]);
                    da.DeleteCommand.Parameters.Add(data[3], DbType.String, 20, columns[3]);
                    da.DeleteCommand.Parameters.Add(data[4], DbType.String, 10, columns[4]);
                    if (tableIdentifier == 1) {
                        da.DeleteCommand.Parameters.Add(data[5], DbType.String, 40, columns[5]);
                    }

                    #endregion

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
