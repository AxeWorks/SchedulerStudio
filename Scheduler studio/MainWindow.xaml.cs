using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Diagnostics;
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

        DataView dvWorkers;
        DataTable dtWorkers;
        DataView dvReservations;
        DataTable dtReservations;
        DataView dvCustomers;
        DataTable dtCustomers;
        DataRow dr;
        List<Note> notes;
        List<Customer> customers;
        List<Worker> workers;
        DataView view;


        public MainWindow()
        {
            InitializeComponent();
            InitMyStuff();        
        }

        private void InitMyStuff()
        {
            try
            {
                view = new DataView();
                workers = new List<Worker>();
                customers = new List<Customer>();
                RefreshWorkers();
                RefreshReservations();
                RefreshCustomers();
                notes = new List<Note>();
                notes = Studio.GetNotesList();

                foreach (Note note in notes)
                {
                    AppendMessage(note);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void RefreshReservations()
        {
            try
            {
                dtReservations = Studio.GetReservations();
                dvReservations = dtReservations.DefaultView;
                dgReservations.DataContext = null;
                dgReservations.DataContext = dvReservations;
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void SomeSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var comboBox = sender as ComboBox;
            var selectedItem = this.dgReservations.CurrentItem;

        }

        private void RefreshCustomers()
        {
            try
            {
                customers.Clear();
                customers = Studio.GetCustomersList();
                cbReservationRegCustomer.ItemsSource = null;

                cbReservationRegCustomer.Items.Add(new Customer());
                foreach (Customer customer in customers)
                {
                    cbReservationRegCustomer.Items.Add(customer);
                }
                //cbRegCustomer.ItemsSource = customers;

                dgcReservationRegCustomer.ItemsSource = customers;

                dtCustomers = Studio.GetCustomersTable();
                dvCustomers = dtCustomers.DefaultView;
                dgCustomerList.DataContext = dvCustomers; 
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void RefreshWorkers()
        {
            try
            {
                dtWorkers = Studio.GetWorkersTable();
                dvWorkers = dtWorkers.DefaultView;
                dgWorkerList.DataContext = dvWorkers;

                cbNotesEmployeeSelector.ItemsSource = null;
                cbWorkerFilter.ItemsSource = null;
                cbReservationEmployee.ItemsSource = null;
                workers.Clear();
                workers = Studio.GetWorkersList(dtWorkers);

                cbWorkerFilter.Items.Add(new Worker());
                foreach (Worker worker in workers)
                {
                    cbWorkerFilter.Items.Add(worker);
                }
                cbNotesEmployeeSelector.ItemsSource = workers;
                cbReservationEmployee.ItemsSource = workers;
                dgcReservationRegEmployee.ItemsSource = workers;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void DeleteNoteContainer(object sender, RoutedEventArgs e)
        {
            try
            {
                 Studio.DeleteNote((Note)((sender as Button).Parent as StackPanel).DataContext);
                ((sender as Button).Parent as StackPanel).Children.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void AppendMessage(Note note)
        {
            try
            {

                TextBlock txtWorker = new TextBlock();
                TextBlock txtNote = new TextBlock();
                Button btnDelNote = new Button();
                btnDelNote.Content = "Delete";
                btnDelNote.Click += DeleteNoteContainer;
                StackPanel spContainer = new StackPanel();

                txtWorker.Text = note.NoteAuthor;
                txtNote.Text = note.Message;

                txtNote.Foreground = new SolidColorBrush(Colors.Red);

                spContainer.CanVerticallyScroll = true;
                spContainer.DataContext = note;
                spContainer.Orientation = Orientation.Horizontal;
                spContainer.Children.Add(txtNote);
                spContainer.Children.Add(txtWorker);
                spContainer.Children.Add(btnDelNote);
                spSubmittedNotes.Children.Add(spContainer);
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #region PANELS
        private void btnStaff_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SetVisibile("worker");
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
                SetVisibile("notebook");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnCustomers_Click(object sender, RoutedEventArgs e) {
            try {
                SetVisibile("customer");
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }



        private void btnReservations_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SetVisibile("reservation");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnShowSavePanel_Click(object sender, RoutedEventArgs e) //työntekijän tallennuspaneelin näyttäminen
        {
            try
            {
                spAddWorker.Visibility = Visibility.Visible;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnCShowSavePanel_Click(object sender, RoutedEventArgs e) { //asiakkaan tallennuspaneelin näyttäminen
            try {
                spAddCustomer.Visibility = Visibility.Visible;
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }

        private void SetVisibile(string panel) //näkymien vaihtorakenne
        {
            try
            {
                switch (panel)
                {
                    case "worker":
                        spWorkerView.Visibility = Visibility.Visible;
                        spNoteView.Visibility = Visibility.Collapsed;
                        spReservationView.Visibility = Visibility.Collapsed;
                        spCustomerView.Visibility = Visibility.Collapsed;
                        break;
                    case "notebook":
                        spNoteView.Visibility = Visibility.Visible;
                        spReservationView.Visibility = Visibility.Collapsed;
                        spWorkerView.Visibility = Visibility.Collapsed;
                        spCustomerView.Visibility = Visibility.Collapsed;
                        break;
                    case "reservation":
                        spReservationView.Visibility = Visibility.Visible;
                        spNoteView.Visibility = Visibility.Collapsed;
                        spWorkerView.Visibility = Visibility.Collapsed;
                        spCustomerView.Visibility = Visibility.Collapsed;
                        break;
                    case "customer":
                        spCustomerView.Visibility = Visibility.Visible;
                        spReservationView.Visibility = Visibility.Collapsed;
                        spNoteView.Visibility = Visibility.Collapsed;
                        spWorkerView.Visibility = Visibility.Collapsed;
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion

        #region SAVEFUNCTIONS

        private void btnSaveReservation_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bool ok;
                ok = true;
                if (txtReservationUnregCustomer.Text == "" && cbReservationRegCustomer.SelectedIndex == -1 || cbReservationEmployee.SelectedIndex == -1 || txtReservationService.Text == "" || txtReservationTime.Text == "" || !dpReservationDate.SelectedDate.HasValue)
                {

                    MessageBox.Show("Tarpeellisia tietoja jätetty pois!");
                    ok = false;
                }
                else if (MessageBox.Show("Haluatko varmasti lisätä tämän varauksen?\n" + "Palvelu: " + txtReservationService.Text + "\n" + "Rekisteröity käyttäjä: " + cbReservationRegCustomer.SelectedValue + "\n" + "Rekisteröimätön käyttäjä: " + txtReservationUnregCustomer.Text + "\n" + "Pvm: " + dpReservationDate.SelectedDate.Value.Date + "\n" + "Aika: " + txtReservationTime.Text + "\n" + "Työntekijä: " + cbReservationEmployee.SelectedValue, "Varmistus", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    string unregcustomer = null;
                    Nullable<int> regcustomer = null;

                    if (txtReservationUnregCustomer.Text == "" && cbReservationRegCustomer.SelectedIndex != -1)
                    {
                        regcustomer = Convert.ToInt32(cbReservationRegCustomer.SelectedValue);

                    }

                    if (txtReservationUnregCustomer.Text != "" && cbReservationRegCustomer.SelectedIndex == -1)
                    {
                        unregcustomer = txtReservationUnregCustomer.Text;
                    }

                    if (ok == true)
                    {
                        Reservation reservation = new Reservation(Convert.ToInt32(cbReservationEmployee.SelectedValue), regcustomer, txtReservationService.Text, unregcustomer, dpReservationDate.SelectedDate.Value.Date, txtReservationTime.Text);
                        txtReservationService.Text = "";
                        cbReservationRegCustomer.SelectedIndex = -1;
                        txtReservationUnregCustomer.Text = "";
                        cbReservationEmployee.SelectedIndex = -1;
                        txtReservationTime.Text = "";

                        Studio.SaveReservation(reservation);
                        RefreshReservations();
                    }                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSaveNote_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Note note = new Note(txtNote.Text, cbNotesEmployeeSelector.Text, Convert.ToInt32(cbNotesEmployeeSelector.SelectedValue));
                notes.Add(note);
                Studio.SaveNote(note);
                AppendMessage(note);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSaveChanges_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int rowcount = DBStudio.UpdateWorker(dtWorkers);
                MessageBox.Show(rowcount + " riviä muokattu.");
                RefreshWorkers();
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
                if (MessageBox.Show("Haluatko varmasti lisätä tämän käyttäjän?\n" + "Nimi: " + txtFname.Text + " " + txtLname.Text + "\n" + "Osoite: " + txtAddress.Text + "\n" + "Puhelinnumero: " + txtPhone.Text + "\n" + "Muu tieto: " + txtOther.Text, "Varmistus", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    dr = dtWorkers.NewRow();
                    dr["fname"] = txtFname.Text;
                    dr["lname"] = txtLname.Text;
                    dr["addr"] = txtAddress.Text;
                    dr["phone"] = txtPhone.Text;
                    dr["regdate"] =  DateTime.Now.Day + "." + DateTime.Now.Month + "." + DateTime.Now.Year;
                    dr["other"] = txtOther.Text;
                    dtWorkers.Rows.Add(dr);
                    spAddWorker.Visibility = Visibility.Collapsed;
                    txtFname.Text = "";
                    txtLname.Text = "";
                    txtAddress.Text = "";
                    txtPhone.Text = "";
                    txtOther.Text = "";

                    DBStudio.UpdateWorker(dtWorkers);
                    RefreshWorkers();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion

        private void btnUpdateReservations_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int rows = Studio.UpdateReservations(dtReservations);
                MessageBox.Show(rows + " riviä päivitetty");
                RefreshReservations();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnDeleteReservation_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                int pkey = Convert.ToInt32((dgReservations.SelectedItem as DataRowView)["PKey"].ToString());

                int effectedRows = Studio.RemoveReservation(pkey);
                MessageBox.Show(effectedRows + " riviä poistettu!");
                RefreshReservations();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cbWorkerFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if(cbWorkerFilter.SelectedIndex == 0)
                {
                    dvReservations.RowFilter = null;
                }
                else
                {
                    dvReservations.RowFilter = "Employee =" + cbWorkerFilter.SelectedValue;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cbCustomerFilter_TextChanged(object sender, TextChangedEventArgs e) {
            try {
                MessageBox.Show("Eventti ampuu.");
                if (false) {
                    dvReservations.RowFilter = null;
                }
                else {

                    DataTable dtCustomers = Scheduler_studio.DBStudio.getCustomerNames();
                    DataTable dtReservations = Scheduler_studio.DBStudio.GetReservations();
                    //dvReservations.RowFilter = null;

                    List<int> PKeys = new List<int>();

                    foreach (DataRow row in dtCustomers.Rows) {
                        MessageBox.Show("For eachissa. CustomFilterText = " + cbCustomerFilter.Text + ".\n Tutkittava nimi = " + row[1].ToString() + ".\n sisältyykö = " + row[1].ToString().Contains(cbCustomerFilter.Text));
                        /*foreach (var value in row.ItemArray) {
                            MessageBox.Show(value.ToString());
                        }*/

                        //row[1].ToString() == cbCustomerFilter.Text || row[2].ToString() == cbCustomerFilter.Text
                        if (row[1].ToString().StartsWith(cbCustomerFilter.Text) || row[2].ToString().StartsWith(cbCustomerFilter.Text)) {
                            MessageBox.Show("iffissä.");
                            PKeys.Add(Int32.Parse(row[0].ToString()));
                            Trace.WriteLine("PKeys Count: " + PKeys.Count);
                            //dvReservations.RowFilter = "RegCustomer =" + row[0] + "OR UnregCustomer = " + row[2];
                            //dvReservations.RowFilter = "RegCustomer =" + "'" + row[0] + "'"; 

                        }
                    }
                    for (int iterator = 0; iterator < PKeys.Count; iterator++) {
                        
                        dvReservations.RowFilter.Insert(iterator, "RegCustomer =" + "'" + PKeys[iterator] + "'");
                        Trace.WriteLine("RowFilterin indeksissä[" + iterator + "] on = " + dvReservations.RowFilter[iterator] + ". RowFilterin koko = " + dvReservations.RowFilter.Length + ". Iteraattori: " + iterator + ". PKeys Count: " + PKeys.Count + ". Pkeys[Iterator] = " + PKeys[iterator]);
                    }
                }
            }
            catch (Exception ex) {
                MessageBox.Show(ex.ToString());
            }
        }

        private void cbDateFilter_SelectedDateChanged(object sender, SelectionChangedEventArgs e) {

            //MessageBox.Show(cbDateFilter.SelectedDate.ToString());
            string date = cbDateFilter.SelectedDate.Value.Year.ToString() + "-" + cbDateFilter.SelectedDate.Value.Month.ToString() + "-" + cbDateFilter.SelectedDate.Value.Day.ToString();
            //MessageBox.Show(date);
            dvReservations.RowFilter = "ReservationDate >=" + "#" + date + "#";
        }

        private void btnCSaveChanges_Click(object sender, RoutedEventArgs e) {
            try {
                int rowcount = DBStudio.UpdateCustomer(dtCustomers);
                MessageBox.Show(rowcount + " riviä muokattu.");
                RefreshCustomers();
            }
            catch (Exception ex) {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnSaveCustomer_Click(object sender, RoutedEventArgs e) {
            try {

                if (txtCFname.Text.Any(char.IsDigit) || txtCLname.Text.Any(char.IsDigit)) {
                    MessageBox.Show("Nimi kentissä ei voi olla numeroita.");
                    return;
                }

                if (txtCPhone.Text.Any(char.IsLetter)) {
                    MessageBox.Show("Puhelinnumero kentässä ei voi olla kirjaimia.");
                    return;
                }

                if (txtCFname.Text.Length > 20 || txtCLname.Text.Length > 20 || txtCPhone.Text.Length > 13 
                    || txtCPrivilege.Text.Length > 50 || txtCBD.Text.Length > 10) {

                    MessageBox.Show("Kenttä on liian pitkä..\n" +
                                    "Etunimi voi olla 20 merkkiä.\n" +
                                    "Sukunimi voi olla 20 merkkiä.\n" +
                                    "Puhelinnumero voi olla 13 merkkiä.\n" +
                                    "Etu voi olla 50 merkkiä.\n" +
                                    "Syntymäaika voi olla 10 merkkiä.");
                    return;
                }

                if (txtCFname.Text == "" || txtCLname.Text == "" || txtCFname.Text == "" || txtCPhone.Text == "" || txtCPrivilege.Text == "" || txtCBD.SelectedDate == null) {
                    MessageBox.Show("Kaikkien kenttien täytyy olla täytetty."); 
                }

                if (MessageBox.Show("Haluatko varmasti lisätä tämän Asiakkaan?\n" + "Nimi: " + txtCFname.Text + " " + txtCLname.Text + "\n" + "Puhelinnumero: " + txtCPhone.Text + "\n" + "Etuus: " + txtCPrivilege.Text + "\n" + "Syntymäaika:" + txtCBD.Text, "Varmistus", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes) {

                    string zeroMonth = "";
                    string zeroDay = "";

                    if (txtCBD.SelectedDate.Value.Month < 10) {
                        zeroMonth = "0";
                    }

                    if (txtCBD.SelectedDate.Value.Day < 10) {
                        zeroDay = "0";
                    }

                    dr = dtCustomers.NewRow();
                    dr["fname"] = txtCFname.Text;
                    dr["lname"] = txtCLname.Text;
                    dr["phone"] = txtCPhone.Text;
                    dr["Birthdate"] = txtCBD.SelectedDate.Value.Year + "-" + zeroMonth + txtCBD.SelectedDate.Value.Month + "-" + zeroDay + txtCBD.SelectedDate.Value.Day;
                    dr["Privilege"] = txtCPrivilege.Text;

                    zeroDay = "";
                    zeroMonth = "";

                    if (DateTime.Now.Month < 10) {
                        zeroMonth = "0";
                    }

                    if (DateTime.Now.Day < 10) { 
                        zeroDay = "0";
                    }

                    dr["regdate"] = DateTime.Now.Year + "-" + zeroMonth + DateTime.Now.Month + "-" + zeroDay + DateTime.Now.Day;
                    dtCustomers.Rows.Add(dr);
                    spAddCustomer.Visibility = Visibility.Collapsed;
                    txtCFname.Text = "";
                    txtCLname.Text = "";
                    txtCPhone.Text = "";
                    txtCBD.Text = "";
                    txtCPrivilege.Text = "";

                    DBStudio.UpdateCustomer(dtCustomers);
                    RefreshCustomers();
                }
            }
            catch (Exception ex) {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnCDeleteCustomer_Click(object sender, RoutedEventArgs e) {
            dvCustomers.Delete(dgCustomerList.SelectedIndex);

            DBStudio.UpdateCustomer(dtCustomers);
        }
    }
}
