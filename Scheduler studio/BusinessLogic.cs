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
        
        public static void AddWorker(string fname, string lname, string address, string phone, DateTime regdate, string other)
        {
            try
            {
                
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
