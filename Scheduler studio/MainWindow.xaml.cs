using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Scheduler_studio
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        DataView dv;
        DataTable dt;
        DataRow dr;

        public MainWindow()
        {
            InitializeComponent();            
        }

        private void Row_Changed(object sender, DataRowChangeEventArgs e)
        {
            try
            {
                //e.Row.
                MessageBox.Show(e.Row.RowState.ToString());
               // MessageBox.Show("ROW CHANGED, dr[Lname].ToString():" + dr["Lname"].ToString() + ",\n    e.Row[Lname].ToString(): " + e.Row["Lname"].ToString());

               /* string property = "";
                string newData = "";

                if (dr["Fname"].ToString() != e.Row["Fname"].ToString())
                {
                    property = "Fname";
                    newData = e.Row["Fname"].ToString();
                }

                else if (dr["Lname"].ToString() != e.Row["Lname"].ToString())
                {
                    property = "Lname";
                    newData = e.Row["Lname"].ToString();
                }

                else if (dr["Addr"].ToString() != e.Row["Addr"].ToString())
                {
                    property = "Addr";
                    newData = e.Row["Addr"].ToString();
                }

                else if (dr["Phone"].ToString() != e.Row["Phone"].ToString())
                {
                    property = "Phone";
                    newData = e.Row["Phone"].ToString();
                }
                else if (dr["RegDate"].ToString() != e.Row["RegDate"].ToString())
                {
                    property = "RegDate";
                    DateTime RegDate = Convert.ToDateTime(e.Row["RegDate"].ToString());
                    newData = RegDate.Year + "-" + RegDate.Month + "-" + RegDate.Day;
                }
                else if (dr["Other"].ToString() != e.Row["Other"].ToString())
                {
                    property = "Other";
                    newData = e.Row["Other"].ToString();
                }

                MessageBox.Show("Property: " + property + ", newData: "+ newData);
                BLData.UpdateWorker(dr, e.Row);*/
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnStaff_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                dt = DBWorker.GetAllWorkersData();
                dv = dt.DefaultView;
                dgWorkerList.DataContext = dv;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }            
        }        

        private void btnNotes_Click(object sender, RoutedEventArgs e)
        {
            try
            {
               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /* private void dgWorkerList_SelectionChanged(object sender, SelectionChangedEventArgs e)
         {
             try
             {
                 sbWorkerDetails.DataContext = dgWorkerList.SelectedItem;
             }
             catch (Exception ex)
             {
                 MessageBox.Show(ex.Message);                
             }
         }


         private void btnAddWorker_Click(object sender, RoutedEventArgs e)
         {
             try
             {
                 DBWorker.AddWorker(txtFirstName.Text, txtLastName.Text, txtAddress.Text, txtPhone.Text, dpDate.SelectedDate.Value, txtOther.Text);
                 dt = DBWorker.GetAllWorkersData();
                 dv = dt.DefaultView;
                 dgWorkerList.DataContext = dv;
             }
             catch (Exception ex)
             {
                 MessageBox.Show(ex.Message);
             }
         }

        private void btnDeleteWorker_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DateTime date = Convert.ToDateTime(dpDate.SelectedDate.Value);
                DBWorker.RemoveWorker(txtFirstName.Text, txtLastName.Text, txtAddress.Text, txtPhone.Text, date, txtOther.Text);
                dt = DBWorker.GetAllWorkersData();
                dv = dt.DefaultView;
                dgWorkerList.DataContext = dv;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }*/

        public string[] dates = { "RegDate", "ReservationDate" };


        public void addColumnTemplates(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {

            string header = e.Column.Header.ToString();

            if (dates.Contains(header))
            {
                MyDataGridTemplateColumn col = new MyDataGridTemplateColumn();
                col.ColumnName = e.PropertyName;
                col.CellTemplate = (DataTemplate)FindResource("datePickerTemplate");
                e.Column = col;
                e.Column.Header = e.PropertyName;
            }

        }
        private void btnSaveChanges_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DBWorker.UpdateWorker(dt);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnShowSavePanel_Click(object sender, RoutedEventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSaveWorker_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //DataRow 
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
