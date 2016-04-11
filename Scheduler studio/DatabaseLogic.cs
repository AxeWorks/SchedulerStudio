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
        //Palauttaa työntekijöillä täytetyn datatablen
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
        //Päivittää työntekijöiden tiedot annetusta datatablesta
        public static int UpdateWorker(DataTable dt)
        {
            try
            {
                int rowcount;
                // luodaan yhteysobjekti
                using (SQLiteConnection conn = new SQLiteConnection(Scheduler_studio.Properties.Settings.Default.ConnectionString))
                {
                    // avataan yhteys
                    conn.Open();
                    SQLiteDataAdapter da = new SQLiteDataAdapter();
                    // asetetaan adapterille komento uusia rivejä varten
                    da.InsertCommand = new SQLiteCommand("INSERT INTO worker (Fname, Lname, Addr, Phone, RegDate, Other) VALUES (@Fname, @Lname, @Addr, @Phone, @RegDate, @Other)", conn);

                    // määritetään insert-komentolauseen muuttujia
                    da.InsertCommand.Parameters.Add("@Fname", DbType.String, 20, "Fname");
                    da.InsertCommand.Parameters.Add("@Lname", DbType.String, 30, "Lname");
                    da.InsertCommand.Parameters.Add("@Addr", DbType.String, 80, "Addr");
                    da.InsertCommand.Parameters.Add("@Phone", DbType.String, 20, "Phone");
                    da.InsertCommand.Parameters.Add("@RegDate", DbType.String, 10, "RegDate");
                    da.InsertCommand.Parameters.Add("@Other", DbType.String, 100, "Other");

                    // asetetaan adapterille komento modifoituja rivejä varten
                    da.UpdateCommand = new SQLiteCommand("UPDATE worker SET Fname = @newFname, Lname = @newLname, Addr = @newAddr, Phone = @newPhone, Other = @newOther " +
                        "WHERE PKey = @PKey", conn);

                    // määritetään update-komentolauseen muuttujia
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

                    // asetetaan adapterille komentolause rivien poistamista varten
                    da.DeleteCommand = new SQLiteCommand("DELETE FROM worker WHERE PKey = @PKey", conn);

                    // määritetään delete-komentolauseen muuttuja
                    da.DeleteCommand.Parameters.Add("@PKey", DbType.Int32, 100000, "PKey");

                    // ajetaan adapterin funktio, joka ottaa vastaan datatablen ja iteroi sen rivien läpi ja päättää mitä kullekkin riville tehdään sen rowstate-parametrin perusteella
                    // funktio palauttaa rivien lukumäärän jolle tehtiin operaatio
                    rowcount = da.Update(dt);

                    //suljetaan yhteys
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
        //Hakee varaukset ja asiakkaiden edut datatablena
        public static DataTable GetReservations()
        {
            try
            {                
                DataTable dt = new DataTable();
                // luodaan yhteysobjekti
                using (SQLiteConnection conn = new SQLiteConnection(Scheduler_studio.Properties.Settings.Default.ConnectionString))
                {
                    //avataan yhteys
                    conn.Open();
                    //määritetään query
                    string sqlString = "SELECT reservation.*, customer.Privilege FROM reservation LEFT OUTER JOIN customer ON customer.PKey = reservation.RegCustomer";
                    //Luodaan data-adapteri jolla on yhteysobjekti ja query
                    SQLiteDataAdapter da = new SQLiteDataAdapter(sqlString, conn);

                    //täytetään datatable
                    da.Fill(dt);
                    //suljetaan yhteys
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
        //Päivittää kantaan varausten tiedot saadun datatablen perusteella
        public static int UpdateReservations(DataTable table)
        {
            try
            {
                int count = 0;
                // luodaan yhteysobjekti
                using (SQLiteConnection conn = new SQLiteConnection(Scheduler_studio.Properties.Settings.Default.ConnectionString))
                {
                    //avataan yhteys
                    conn.Open();

                    SQLiteDataAdapter da = new SQLiteDataAdapter();
                    // määritetään adapterin update-query
                    da.UpdateCommand = new SQLiteCommand("UPDATE reservation SET Service = @Service, UnregCustomer = @UnregCustomer, ReservationTime = @ReservationTime, ReservationDate = @ReservationDate, RegCustomer = @RegCustomer, Employee = @Employee WHERE PKey = @PKey", conn);
                    // määritetään adapterin delete-query
                    da.DeleteCommand = new SQLiteCommand("DELETE FROM reservation WHERE PKey = @PKey", conn);

                    // määritetään update-queryn muuttujat
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

                    // määritetään delete-queryn muuttujat
                    da.DeleteCommand.Parameters.Add("@PKey", DbType.Int32, 100000, "PKey");

                    // ajetaan adapterin funktio, joka ottaa vastaan datatablen, iteroi sen rivien läpi ja päättää mitä kullekkin riville tehdään rivin rowstate-parametrin perusteella
                    // funktio palauttaa rivien lukumäärän joille tehtiin operaatio
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
        // poistaa varauksen annetun avaimen perusteella
        public static int DeleteReservation(int pkey)
        {
            try
            {
                int rowcount;
                // luodaan yhteys-objekti
                using (SQLiteConnection conn = new SQLiteConnection(Scheduler_studio.Properties.Settings.Default.ConnectionString))
                {
                    // avataan yhteys kantaan
                    conn.Open();
                    // määritetään sql-lause ja muuttujat
                    string sqlString = string.Format("DELETE FROM reservation WHERE PKey = {0}", pkey);
                    SQLiteCommand command = new SQLiteCommand(sqlString, conn);
                    // suoritetaan poisto
                    rowcount = command.ExecuteNonQuery();

                    conn.Close();
                }
                // palautetaan poistettujen rivien lukumäärä (aina 1 jos onnistuu)
                return rowcount;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //Janne
        // lisätään varaus kantaan annetuilla parametreilla
        public static int InsertReservation(string operation, string ResTime, string ResDate, string unregcustomer, Nullable<int> regcustomer, int employee)
        {
            try
            {
                int count = 0;
                // luodaan yhteysobjekti
                using (SQLiteConnection conn = new SQLiteConnection(Scheduler_studio.Properties.Settings.Default.ConnectionString))
                {
                    // avataan yhteys kantaan
                    conn.Open();

                    // määritetään query string
                    string sqlString = string.Format("INSERT INTO reservation (Service, ReservationTime, ReservationDate, UnregCustomer, RegCustomer, Employee) VALUES (@Service, @ReservationTime, @ReservationDate, @UnregCustomer, @RegCustomer, {0})", employee);
                    // luodaan komento-objekti jolle annetaan yhteysobjekti ja query string
                    SQLiteCommand command = new SQLiteCommand(sqlString, conn);

                    // määritetään parametreja
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

                    // suoritetaan query
                    count = command.ExecuteNonQuery();
                    // suljetaan yhteys
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
        // Hakee ja palauttaa muistiodatan datatablena
        public static DataTable GetNotes()
        {
            try
            {
                DataTable dt = new DataTable();
                // luodaan yhteysobjekti
                using (SQLiteConnection conn = new SQLiteConnection(Scheduler_studio.Properties.Settings.Default.ConnectionString))
                {
                    // avataan yhteys
                    conn.Open();
                    // määritetään query string
                    string sqlString = "SELECT notebook.Note, notebook.Employee, worker.Fname, worker.Lname FROM notebook JOIN worker ON notebook.Employee = worker.PKey";
                    // luodaan dataadapteri jolle annetaan query string ja yhteys objekti
                    SQLiteDataAdapter da = new SQLiteDataAdapter(sqlString, conn);

                    // täyttää datatablen
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
        // Tallentaa kantaan muistiomerkinnän
        public static int SaveNote(string msg, int FKey)
        {
            try
            {
                int count;
                // luodaan yhteysobjekti
                using (SQLiteConnection conn = new SQLiteConnection(Scheduler_studio.Properties.Settings.Default.ConnectionString))
                {
                    //avataan yhteys
                    conn.Open();
                    // määritetään query string
                    string sqlString = string.Format("INSERT INTO notebook (Note, Employee) VALUES (@Note, {0})", FKey);
                    // luodaan dataadapteri jolle annetaan query string ja yhteys objekti
                    SQLiteCommand command = new SQLiteCommand(sqlString, conn);
                    //asetetaan queryn muuttuja note
                    SQLiteParameter param = new SQLiteParameter("Note", DbType.String);
                    param.Value = msg;
                    command.Parameters.Add(param);
                    
                    // ajetaan query
                    count = command.ExecuteNonQuery();
                    // suljetaan yhteys
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
        //Poistaa muistiomerkinnän kannasta annetuilla paremetreilla
        public static int DeleteNote(string msg, int FKey)
        {
            try
            {
                int count;
                // luodaan yhteysobjekti
                using (SQLiteConnection conn = new SQLiteConnection(Scheduler_studio.Properties.Settings.Default.ConnectionString))
                {
                    // avataan yhteys
                    conn.Open();
                    // määritetään query string
                    string sqlString = string.Format("DELETE FROM notebook WHERE (Note = @Note) AND (Employee = {0})", FKey);
                    // määritetään queryn parametri
                    SQLiteCommand command = new SQLiteCommand(sqlString, conn);
                    SQLiteParameter param = new SQLiteParameter("Note", DbType.String);
                    param.Value = msg;
                    command.Parameters.Add(param);
                    
                    // suoritetaan poisto
                    count = command.ExecuteNonQuery();
                    // suljetaan yhteys
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

        #region pois_jätetty_modulaarinen_SQL-funktio
        /*        
        Käyttö ja poisjättämisen syy dokumentaatiossa. Lyhyesti: Piti olla modulaarinen tapa hoitaa update, insert ja delete yhdessä funktiossa, riippumatta muokattavata taulusta tai kutsuvasta kohteesta. 
        Kaatui concurrency erroriin, jota ei saatu järkevässä ajassa korjattua.
        
        public static void UpdateWorker(DataTable dt, int tableIdentifier){
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
       }*/
        #endregion
    }
}
