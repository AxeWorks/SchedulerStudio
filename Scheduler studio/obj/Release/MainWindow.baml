   M S B A M L   `   `   `  ���� K  GScheduler studio, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:Scheduler_studioScheduler_studio  !    Scheduler_studio.MainWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     0localclr-namespace:Scheduler_studio       � Title$  Scheduler Studio��$��611.914��$��1450.584�� � 
ResizeMode$ NoResize=� � 	Resources    dgcTimeTemplate�� %*   &	        �� .R��� ���� *Phone+.��� .+��� 
������� �� Orientation$ Vertical=�.+��� $ 
Horizontal=�$��5,5,5,5q�$��49��$��Center=�.+��� -    ��Name$ btnNotes��
�������$��20(�$��5,5,5,5q�$��Muistio��$	��140���� -   $ btnStaff��
�������$��20(�$��5,5,5,5q�$��Henkilökunta��$	��140���� -   $ btnCustomers��
�������$��20(�$��5,5,5,5q�$��	Asiakkaat��$	��140���� -   $ btnReservations��
�������$��20(�$��5,5,5,0q�$��	Varaukset��$	��140��$��39��$	��Top=��� -    ��Name$ 
spNoteView��$y�	Collapsed=�$ Vertical=�$	��523��.+�� -    �Name$ wpSubmittedNotes�� � Orientation$ Vertical=�$	��427���� $ Vertical=�$��98��.+��� -   $ 
spNoteForm��$ 
Horizontal=�$��84��.+��� -    ��Name$ txtNote��$��5,5,5,5q�$
��1310��$��Kirjoita viesti��	 �� TextWrapping$
	 Wrap=�$
��Auto=���. �� $��0,5,5,5q�$ Vertical=�.+��� -	   $ btnSaveNote��$��5,5,5,5q�$��Tallenna viesti���� $��Lähettäjä���� -
   
 ��Name$
 cbNotesEmployeeSelector��$��5,5,5,5q� �� DisplayMemberPath$ FullName�� �� SelectedValuePath$
 PKey��$	��100���� -   $ spWorkerView��$y�	Collapsed=�$ Vertical=�$	��520��.+�&   System.Windows.Controls.DataGrid  -     Name$ dgWorkerList��
�������   ColumnWidth5  /System.Windows.Controls.DataGridLengthConverter$	 100 $
��Auto=�   CanUserAddRows .  $	��383��   AutoGenerateColumns .  ����    Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Header$ Etunimi��   Width$	 100 1  +System.Windows.Controls.DataGridBoundColumn   Binding �� *Fname+  $ Sukunimi��$	 100  �� *Lname+  $ Osoite��$	 300  �� *Addr+  $	 Puh��$	 110  �� *Phone+  $ Rekisteröintynyt��   
IsReadOnly . $	 100  �� *	RegDate+  $ Muuta tietoa��$	 715  �� *Other+�� $��5,5,5,5q�$ Vertical=�$��Bottom=�$��Center=�.+��� -   $ spAddWorker��$��5,5,5,5q�$y�	Collapsed=�$ 
Horizontal=�$��27��.+��� $��5,5,5,5q�$��Etunimi:���� -   $ txtFname��$	��100���� $��5,5,5,5q�$��	Sukunimi:���� -   $ txtLname��$	��100���� $��5,5,5,5q�$��Osoite:���� -   $ 
txtAddress��$	��200���� $��5,5,5,5q�$��Puhelin:���� -   $ txtPhone��$	��100���� $��5,5,5,5q�$��Muuta:���� -   $ txtOther��$	��100���� -   $ btnSaveWorker��
�������$��5,0,5,0q�$��25��$	��100��$��Tallenna���� $��5,5,5,5q�$��Center=�$ 
Horizontal=�.+��� -   $ btnSaveChanges��
�������$��5,5,5,5q�$��Tallenna muutokset��$��29��$	��130���� -   $ btnShowWorkerSavePanel��
�������$��5,5,5,5q�$��Lisää työntekijä��$��29��$	��130���� -   $ btnRemoveWorker��
�������$��5,5,5,5q�$��Poista työntekijä��$��29��$	��130���� -   $ spCustomerView��$y�	Collapsed=�$	��524��$ Vertical=�.+��� $ 
Horizontal=�$��5,5,5,5q�$��Center=�.+��� $��Haku asiakkaan nimellä:��$��5,5,5,5q��� -   $# txtCustomerViewCustomerFilter��$��5,5,5,5q�$	��200��  -   $ dgCustomerList��
�������$	 100 $
��Auto=� .  $	��383�� .  ����    $ Etunimi��$	 150  �� *Fname+  $ Sukunimi��$	 150  �� *Lname+  $	 Puh��$	 150  �� *Phone+  $ Etuus��$	 100  �� *	Privilege+  $ Syntymäpäivä��$	 400  �� *	Birthdate+  $ Rekisteröitymispäivä��$	 400  .  �� *	RegDate+�� $ Vertical=�$��Bottom=�$��Center=�.+��� -   $ spAddCustomer��$y�	Collapsed=�$ 
Horizontal=�$��27��$��5,5,5,5q�.+��� $��5,5,5,5q�$��Etunimi:���� -   $ 	txtCFname��$	��100���� $��5,5,5,5q�$��	Sukunimi:���� -   $ 	txtCLname��$	��100���� $��5,5,5,5q�$��Puhelin:���� -   $ 	txtCPhone��$	��200���� $��5,5,5,5q�$��Etuus:���� -   $ txtCPrivilege��$	��100���� $��5,5,5,5q�$��Syntymäaika:��(  "System.Windows.Controls.DatePicker  -     Name$ dpCustomerBD��$	��110���� -    $ btnSaveCustomer��$��5,0,5,0q�$	��130��$��25��
�������$��Tallenna���� $��Center=�$ 
Horizontal=�$��Bottom=�$��5,5,5,5q�.+��� -!   $ btnSaveCustomerChanges��
�������$	��130��$��29��$��5,5,5,5q�$��Tallenna muutokset��$��Bottom=��� -"   $ btnCShowSavePanel��
�������$	��130��$��29��$��5,5,5,5q�$��Lisää asiakas��$��Bottom=��� -#   $ btnCDeleteCustomer��
�������$	��130��$��29��$��5,5,5,5q�$��Poista asiakas��$��Bottom=��� -$   $ spReservationView��$y�Visible=�$	��525��$ Vertical=�.+��� $ 
Horizontal=�$��5,5,5,5q�$��Center=�.+��� $��15,5,5,5q�$�� Hae työntekijällä:���� -%   $
 cbWorkerFilter��$ FullName��$
 PKey��$	��120���� $��15,5,5,5q�$��Hae asiakkaalla:���� -&   $ txtCustomerFilter��$	��155���� $��15,5,5,5q�$!��Hae päivästä eteenpäin:��  -'   $ dpDateFilter��$	��110���� $ Vertical=�$	��361��.+�  -(   $ dgReservations��
�������$	 100 $
��Auto=� .  $	��350�� .  ����    $ Palvelu��$	 400  �� *	Service+4  .System.Windows.Controls.DataGridComboBoxColumn  -)   dgcReservationRegEmployee��   DisplayMemberPath$ FullName��   SelectedValuePath$
 PKey��$ Työntekijä��$	 200    SelectedValueBinding �� *
Employee+  -*   dgcReservationRegCustomer��$ FullName��$
 PKey��$ Asiakas (reg)��$	 200  �� *RegCustomer+  $ Asiakas (unreg)��$	 200  �� *UnregCustomer+  $ Asiakkaan etu�� �� *	Privilege+  $
 Aika��$	 200  �� *ReservationTime+  $	 Pvm��$	 100  �� *ReservationDate+�� $ Vertical=�$	��109��.+��� -+   $ spAddReservation��$��5,5,5,5q�$y�	Collapsed=�$ 
Horizontal=�$��27��$��Center=�.+��� $��5,5,5,5q�$��Rekisteröity asiakas:���� -,   $
 cbReservationRegCustomer��$	��100��$ 
Identifier��$
 PKey���� $��5,5,5,5q�$!��Rekisteröimätön asiakas:���� --   $! txtReservationUnregCustomer��$	��100���� $��5,5,5,5q�$��Palvelu:���� -.   $ txtReservationService��$	��200���� $��5,5,5,5q�$
��Pvm:��  -/   $ dpReservationDate��$	��110���� $��5,5,5,5q�$��Aika:���� -0   $ txtReservationTime��$	��150���� $��5,5,5,5q�$��Työntekijä���� -1   $
 cbReservationEmployee��$ FullName��$
 PKey��$	��150���� -2   $ btnSaveReservation��$��5,0,5,0q�
�������$��25��$	��100��$��Tallenna���� $ 
Horizontal=�$��Center=�$��512,5,511,5q�.+��� -3   $ btnUpdateReservations��$��5,5,5,5q�
�������$��Päivitä muutokset��$��29��$	��130���� -4   $  btnOpenReservationAddingSP��$��5,5,5,5q�
�������$��Lisää varaus��$��29��$	��130���� -5   $ btnDeleteReservation��$��5,5,5,5q�
�������$��Poista varaus��$��29��$	��130��