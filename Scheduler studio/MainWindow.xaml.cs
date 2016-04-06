using System;
using System.Collections;
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
        DataView dvWorkers;
        DataTable dtWorkers;
        DataView dvReservations;
        DataTable dtReservations;
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
                MessageBox.Show(ex.Message);
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

                dgcReservationRegCustomer.ItemsSource = customers;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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
                cbNotesEmployeeSelector.Items.Clear();
                cbWorkerFilter.Items.Clear();
                cbReservationEmployee.Items.Clear();
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
                RefreshReservations();
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
                 ((((sender as Button).Parent as StackPanel).Parent as StackPanel).Parent as Border).Visibility = Visibility.Collapsed;
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
                ScrollViewer scrollview = new ScrollViewer();
                StackPanel spContainer = new StackPanel();
                StackPanel spMessage = new StackPanel();
                StackPanel spHeader = new StackPanel();
                Button btnDelNote = new Button();
                Border border = new Border();

                btnDelNote.Background = Brushes.WhiteSmoke;
                txtNote.Width = 390;
                spContainer.Width = 500;
                spContainer.Height = 100;
                scrollview.Height = 100;
                scrollview.CanContentScroll = true;
                scrollview.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
                txtNote.TextWrapping = TextWrapping.Wrap;
                spContainer.Orientation = Orientation.Horizontal;
                spHeader.Orientation = Orientation.Vertical;

                btnDelNote.Content = "Delete";
                btnDelNote.Click += DeleteNoteContainer;
                spHeader.Margin = new Thickness(5, 5, 5, 5);
                border.BorderBrush = Brushes.Black;
                border.BorderThickness = new Thickness(1, 1, 1, 1);
                border.Background = Brushes.LightYellow;
                border.CornerRadius = new CornerRadius(5);
                border.Margin = new Thickness(2, 2, 2, 2);
             
                txtWorker.Text = note.NoteAuthor;
                txtNote.Text = note.Message;
 
                spContainer.DataContext = note;
                scrollview.Content = txtNote;
                txtNote.Padding = new Thickness(2, 2, 2, 2);
                spMessage.Children.Add(scrollview);
                spHeader.Children.Add(txtWorker);
                spHeader.Children.Add(btnDelNote);
                spContainer.Children.Add(spHeader);
                spContainer.Children.Add(spMessage);
                border.Child = spContainer;
                wpSubmittedNotes.Children.Add(border);                
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

        private void btnShowSavePanel_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                spAddWorker.Visibility = Visibility.Visible;
                btnShowSavePanel.IsEnabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void SetVisibile(string panel)
        {
            try
            {
                switch (panel)
                {
                    case "worker":
                        spWorkerView.Visibility = Visibility.Visible;
                        spNoteView.Visibility = Visibility.Collapsed;
                        spReservationView.Visibility = Visibility.Collapsed;
                        btnStaff.IsEnabled = false;
                        btnReservations.IsEnabled = true;
                        btnNotes.IsEnabled = true;
                        btnCustomers.IsEnabled = true;
                        break;
                    case "notebook":
                        spNoteView.Visibility = Visibility.Visible;
                        spReservationView.Visibility = Visibility.Collapsed;
                        spWorkerView.Visibility = Visibility.Collapsed;
                        btnStaff.IsEnabled = true;
                        btnReservations.IsEnabled = true;
                        btnNotes.IsEnabled = false;
                        btnCustomers.IsEnabled = true;
                        break;
                    case "reservation":
                        spReservationView.Visibility = Visibility.Visible;
                        spNoteView.Visibility = Visibility.Collapsed;
                        spWorkerView.Visibility = Visibility.Collapsed;
                        btnStaff.IsEnabled = true;
                        btnReservations.IsEnabled = false;
                        btnNotes.IsEnabled = true;
                        btnCustomers.IsEnabled = true;
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
                if (txtReservationUnregCustomer.Text == "" && cbReservationRegCustomer.SelectedIndex == -1 || cbReservationEmployee.SelectedIndex == -1 || txtReservationService.Text == "" || txtReservationTime.Text == "" || !dpReservationDate.SelectedDate.HasValue)
                {

                    MessageBox.Show("Tarpeellisia tietoja jätetty pois!");
                }
                
                else
                {
                    if (Studio.IsValidTime(txtReservationTime.Text))
                    {
                        if (MessageBox.Show("Haluatko varmasti lisätä tämän varauksen?\n" + "Palvelu: " + txtReservationService.Text + "\n" + "Rekisteröity käyttäjä: " + cbReservationRegCustomer.SelectedValue + "\n" + "Rekisteröimätön käyttäjä: " + txtReservationUnregCustomer.Text + "\n" + "Pvm: " + dpReservationDate.SelectedDate.Value.Day + "." + dpReservationDate.SelectedDate.Value.Month + "." + dpReservationDate.SelectedDate.Value.Year + "\n" + "Aika: " + txtReservationTime.Text + "\n" + "Työntekijä: " + cbReservationEmployee.SelectedValue, "Varmistus", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
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

                            Reservation reservation = new Reservation(Convert.ToInt32(cbReservationEmployee.SelectedValue), regcustomer, txtReservationService.Text, unregcustomer, dpReservationDate.SelectedDate.Value.Date, txtReservationTime.Text);
                            txtReservationService.Text = "";
                            cbReservationRegCustomer.SelectedIndex = -1;
                            txtReservationUnregCustomer.Text = "";
                            cbReservationEmployee.SelectedIndex = -1;
                            txtReservationTime.Text = "";
                            spAddReservation.Visibility = Visibility.Collapsed;
                            btnOpenReservationAddingSP.IsEnabled = true;

                            Studio.SaveReservation(reservation);
                            RefreshReservations();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Aikaformaatti väärä. Formaatti on TT:MM.");
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
                if (txtNote.Text == "" || cbNotesEmployeeSelector.SelectedIndex == -1)
                {
                    MessageBox.Show("Tarpeellisia tietoja jätetty pois!");
                }
                else
                {
                    Note note = new Note(txtNote.Text, cbNotesEmployeeSelector.Text, Convert.ToInt32(cbNotesEmployeeSelector.SelectedValue));
                    notes.Add(note);
                    Studio.SaveNote(note);
                    AppendMessage(note);
                }
               
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
                if (txtFname.Text == "" || txtLname.Text == "" || txtAddress.Text == "" || txtPhone.Text == "")
                {
                    MessageBox.Show("Tarpeellisia tietoja jätetty pois!");
                }
                else
                {
                    if (MessageBox.Show("Haluatko varmasti lisätä tämän käyttäjän?\n" + "Nimi: " + txtFname.Text + " " + txtLname.Text + "\n" + "Osoite: " + txtAddress.Text + "\n" + "Puhelinnumero: " + txtPhone.Text + "\n" + "Muu tieto: " + txtOther.Text, "Varmistus", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                    {
                        dr = dtWorkers.NewRow();
                        dr["fname"] = txtFname.Text;
                        dr["lname"] = txtLname.Text;
                        dr["addr"] = txtAddress.Text;
                        dr["phone"] = txtPhone.Text;
                        dr["regdate"] = DateTime.Now.Day + "." + DateTime.Now.Month + "." + DateTime.Now.Year;
                        dr["other"] = txtOther.Text;
                        dtWorkers.Rows.Add(dr);
                        spAddWorker.Visibility = Visibility.Collapsed;
                        btnShowSavePanel.IsEnabled = true;
                        txtFname.Text = "";
                        txtLname.Text = "";
                        txtAddress.Text = "";
                        txtPhone.Text = "";
                        txtOther.Text = "";

                        DBStudio.UpdateWorker(dtWorkers);
                        RefreshWorkers();
                    }
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

        private void btnRemoveWorker_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                dvWorkers.Delete(dgWorkerList.SelectedIndex);
                
                int result = DBStudio.UpdateWorker(dtWorkers);
                MessageBox.Show(result + " tietue poistettu.");
                RefreshWorkers();
            }
            catch (Exception ex )
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnOpenReservationAddingSP_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                spAddReservation.Visibility = Visibility.Visible;
                btnOpenReservationAddingSP.IsEnabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
