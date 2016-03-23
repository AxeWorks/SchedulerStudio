using System;
using System.Collections.Generic;
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

namespace Testground
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(
          object sender, EventArgs e)
        {
            this.regionTableAdapter.Fill(
              this.northwindDataSet.Region);
            // resize the column once, but allow the
            // users to change it.
            this.regionDataGridView.AutoResizeColumns(
              DataGridViewAutoSizeColumnsMode.AllCells);
        }

        //tracks for PositionChanged event last row
        private DataRow LastDataRow = null;

        /// <SUMMARY>
        /// Checks if there is a row with changes and
        /// writes it to the database
        /// </SUMMARY>
        private void UpdateRowToDatabase()
        {
            if (LastDataRow != null)
            {
                if (LastDataRow.RowState ==
                    DataRowState.Modified)
                {
                    regionTableAdapter.Update(LastDataRow);
                }
            }
        }

        private void regionBindingSource_PositionChanged(
          object sender, EventArgs e)
        {
            // if the user moves to a new row, check if the 
            // last row was changed
            BindingSource thisBindingSource =
              (BindingSource)sender;
            DataRow ThisDataRow =
              ((DataRowView)thisBindingSource.Current).Row;
            if (ThisDataRow == LastDataRow)
            {
                // we need to avoid to write a datarow to the 
                // database when it is still processed. Otherwise
                // we get a problem with the event handling of 
                //the DataTable.
                throw new ApplicationException("It seems the" +
                  " PositionChanged event was fired twice for" +
                  " the same row");
            }

            UpdateRowToDatabase();
            // track the current row for next 
            // PositionChanged event
            LastDataRow = ThisDataRow;
        }

        private void MainForm_FormClosed(
          object sender, FormClosedEventArgs e)
        {
            UpdateRowToDatabase();
        }
    }
}
