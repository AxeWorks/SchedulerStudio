using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler_studio
{



    class Worker : INotifyPropertyChanged
    {
        #region PROPERTIES
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
        public Worker(string fname, string lname, string addr, string phone, DateTime rdate, string other)
        {
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
        #endregion
    }

    static class BLData
    {
        #region WORKER

        public static void UpdateWorker(DataTable dt)
        {

           

        }

       /* public static void AddWorker(string fname, string lname, string address, string phone, DateTime regdate, string other)
        {
            try
            {
                DBWorker.InsertIntoWorker(fname, lname, address, phone, regdate, other);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static DataView GetWorkerData()
        {
            try
            {
                return DBWorker.GetWorkerData();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void RemoveWorker(string fname, string lname, string address, string phone, DateTime regdate, string other)
        {
            try
            {
                DBWorker.RemoveWorker(fname, lname, address, phone, regdate, other);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void UpdateWorker(DataRow dr)
        {
            try
            {

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }*/
        #endregion
    }
}
