using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Scheduler_studio
{
    
    class Worker : INotifyPropertyChanged
    {
        #region PROPERTIES

        public string FullName
        {
            get { return firstname + " " + lastname; }
        }

        private int pkey;

        public int PKey
        {
            get { return pkey; }
            set { pkey = value; }
        }
        
        private string firstname;

        public string Firstname
        {
            get { return firstname;  }
            set { firstname = value; Notify("Fname"); }
            
        }

        private string lastname;

        public string Lastname
        {
            get { return lastname; }
            set { lastname = value; Notify("Lname"); }
        }

        private string address;

        public string Address
        {
            get { return address; }
            set { address = value; Notify("Address"); }
        }

        private string phone;

        public string Phone
        {
            get { return phone; }
            set { phone = value; Notify("Phone"); }
        }

        private DateTime regdate;

        public DateTime RegDate
        {
            get { return regdate; }
            set { regdate = value; Notify("RegDate"); }
        }

        private string otherInfo;

        public string OtherInfo
        {
            get { return otherInfo; }
            set { otherInfo = value; Notify("Other"); }
        }
        
        #endregion
        #region CONSTRUCTORS
        public Worker() { }
        public Worker(int id, string fname, string lname, string addr, string phone, DateTime rdate, string other)
        {
            pkey = id;
            firstname = fname;
            lastname = lname;
            address = addr;
            regdate = rdate;
            otherInfo = other;
        }
        #endregion
        #region METHODS
        public event PropertyChangedEventHandler PropertyChanged;

        void Notify(string propName)
        {
            if (PropertyChanged != null)
            {                
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }

        public override string ToString()
        {
            return firstname + " " + lastname;
        }
        #endregion
    }

    public class Note
    {
        private string noteauthor;

        public string NoteAuthor
        {
            get { return noteauthor; }
            set { noteauthor = value; }
        }

        private int fkey;

        public int FKey
        {
            get { return fkey; }
            set { fkey = value; }
        }


        private string message;

        public string Message
        {
            get { return message; }
            set { message = value; }
        }

        public Note(string msg, string user, int primarykey)
        {
            this.fkey = primarykey;
            this.message = msg;
            this.noteauthor = user;
        }

        public Note(string msg, string user)
        {
            this.message = msg;
            this.noteauthor = user;
        }

    }

    static class Studio
    {

        public static void SaveNote(Note note)
        {
            try
            {
                DBStudio.SaveNote(note.Message, note.FKey);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void DeleteNote(Note note)
        {
            try
            {
                DBStudio.DeleteNote(note.Message, note.FKey);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<Note> GetNotesList()
        {
            try
            {
                DataTable dt;
                dt = DBStudio.GetNotes();

                List<Note> notes = new List<Note>();

                foreach(DataRow row in dt.Rows)
                {
                    notes.Add(new Note(row["Note"].ToString(), row["Fname"].ToString() + " " + row["Lname"].ToString(), Convert.ToInt32(row["Employee"].ToString())));
                }
                return notes;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region WORKER

        public static DataTable GetWorkersTable()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = DBStudio.GetAllWorkersData();
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public static List<Worker> GetWorkersList(DataTable dt)
        {
            try
            {
                List<Worker> workers = new List<Worker>();

                foreach(DataRow row in dt.Rows)
                {
                    workers.Add(new Worker(Convert.ToInt32(row["PKey"].ToString()), row["Fname"].ToString(), row["Lname"].ToString(), row["Addr"].ToString(), row["Phone"].ToString(), Convert.ToDateTime(row["RegDate"].ToString()), row["Other"].ToString()));
                }

                return workers;
            }
            catch (Exception ex)
            {


                throw ex;
            }
        }

        #endregion
    }

    /* public class MyData
 {
     public MyData(string name, bool data) { nameData = name; showData = data; }
     public string nameData { get; set; }
     public bool showData { get; set; }
 }
 public class MyDataGridTemplateColumn : DataGridTemplateColumn
 {
     public string ColumnName
     {
         get;
         set;
     }

     protected override System.Windows.FrameworkElement GenerateElement(DataGridCell cell, object dataItem)
     {
         ContentPresenter cp = (ContentPresenter)base.GenerateElement(cell, dataItem);
         BindingOperations.SetBinding(cp, ContentPresenter.ContentProperty, new Binding(this.ColumnName));
         return cp;
     }
 }*/
}
