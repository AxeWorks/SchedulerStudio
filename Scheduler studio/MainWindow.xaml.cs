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
                dgReservations.ItemsSource = null;
                dgReservations.ItemsSource = dvReservations;
                
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
                cbRegCustomer.ItemsSource = null;

                cbRegCustomer.Items.Add(new Customer());
                foreach (Customer customer in customers)
                {
                    cbRegCustomer.Items.Add(customer);
                }
                //cbRegCustomer.ItemsSource = customers;
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
                dgWorkerList.ItemsSource = dvWorkers;

                cbNotesWorkerSelector.ItemsSource = null;
                cbWorkerFilter.ItemsSource = null;
                cbReservationEmployee.ItemsSource = null;
                workers.Clear();
                workers = Studio.GetWorkersList(dtWorkers);

                cbWorkerFilter.Items.Add(new Worker());
                foreach (Worker worker in workers)
                {
                    cbWorkerFilter.Items.Add(worker);
                }
                cbNotesWorkerSelector.ItemsSource = workers;
                //cbWorkerFilter.ItemsSource = workers;
                cbReservationEmployee.ItemsSource = workers;
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
                        break;
                    case "notebook":
                        spNoteView.Visibility = Visibility.Visible;
                        spReservationView.Visibility = Visibility.Collapsed;
                        spWorkerView.Visibility = Visibility.Collapsed;
                        break;
                    case "reservation":
                        spReservationView.Visibility = Visibility.Visible;
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

        private void btnSaveNote_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Note note = new Note(txtNote.Text, cbNotesWorkerSelector.Text, Convert.ToInt32(cbNotesWorkerSelector.SelectedValue));
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
                if (MessageBox.Show("Haluatko varmasti lisätä tämän käyttäjän?\n" + "Nimi: " + txtFname.Text + " " + txtLname.Text + "\n" + "Osoite: " + txtAddress.Text + "\n" + "Puhelinnumero: " + txtPhone.Text + "\n" + "Rekisteröintipäivä: " + dpRegDate.SelectedDate.Value.Day + "." + dpRegDate.SelectedDate.Value.Month + "." + dpRegDate.SelectedDate.Value.Year + "\n" + "Muu tieto: " + txtOther.Text, "Varmistus", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    dr = dtWorkers.NewRow();
                    dr["fname"] = txtFname.Text;
                    dr["lname"] = txtLname.Text;
                    dr["addr"] = txtAddress.Text;
                    dr["phone"] = txtPhone.Text;
                    dr["regdate"] = dpRegDate.SelectedDate.Value.Day + "." + dpRegDate.SelectedDate.Value.Month + "." + dpRegDate.SelectedDate.Value.Year;
                    dr["other"] = txtOther.Text;
                    dr["fullname"] = txtFname.Text + " " + txtLname.Text;
                    dtWorkers.Rows.Add(dr);
                    spAddWorker.Visibility = Visibility.Collapsed;
                    txtFname.Text = "";
                    txtLname.Text = "";
                    txtAddress.Text = "";
                    txtPhone.Text = "";
                    dpRegDate.Text = "";
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

        private void btnSaveReservation_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (MessageBox.Show("Haluatko varmasti lisätä tämän varauksen?\n" + "Palvelu: " + txtService.Text + "\n" + "Rekisteröity käyttäjä: " + cbRegCustomer.SelectedValue + "\n" + "Rekisteröimätön käyttäjä: " + txtUnregCustomer.Text + "\n" + "Pvm: " + dpReservation.SelectedDate + "\n" + "Aika: " + txtTime.Text + "\n" + "Työntekijä: " + cbReservationEmployee.SelectedValue, "Varmistus", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    string unregcustomer = null;
                    if (txtUnregCustomer.Text != "")
                    {
                        unregcustomer = txtUnregCustomer.Text;
                    }
                    Reservation reservation = new Reservation(Convert.ToInt32(cbReservationEmployee.SelectedValue), Convert.ToInt32(cbRegCustomer.SelectedValue), txtService.Text, unregcustomer, dpReservation.SelectedDate.Value, txtTime.Text);
                    txtService.Text = "";
                    cbRegCustomer.SelectedIndex = -1;
                    txtUnregCustomer.Text = "";
                    cbReservationEmployee.SelectedIndex = -1;
                    txtTime.Text = "";

                    Studio.SaveReservation(reservation);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
