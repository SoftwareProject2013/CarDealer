using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using CarDealer;

namespace WpfApplicationCarDealer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
            init();
        }

        /// <summary>
        /// method to create inintial variables and set initial properties
        /// </summary>
        public void init()
        {

            try
            {
                dealer = new Cardealer();             
            }
            catch(Exception exception)
            {
                MessageBox.Show(exception.Message);
            }



            validNewVehiculeButton.Visibility = Visibility.Hidden;
            cancelNewVehiculeButton.Visibility = Visibility.Hidden;
            stateComboBox.Visibility = Visibility.Hidden;
            // dealer.loadVehicule();
            fillComboBox();
            label22.Visibility = Visibility.Hidden;
            vehiculeIDTextBox.Visibility = Visibility.Hidden;

        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        #region ClientRegion
        /// <summary>
        /// showClients method check on whitch tab user is and whitch type of users he wants (private or business) then add selected customers
        /// to the appropriate listbox to show to the user
        /// <params> none</params>
        /// <return> void </return>
        /// </summary>
        private void showClients()
        {
            ListBox currentListBox = customersListBox;
            Boolean? isPrivate = false;
            try
            {
                if (tabController.SelectedIndex == 0)
                {
                    currentListBox = customersListBox;
                    isPrivate = customerPrivateRadioButton.IsChecked;
                }
                if (tabController.SelectedIndex == 2)
                {
                    currentListBox = customerDisplayedListBox;
                    isPrivate = privateRadioButton.IsChecked;
                }
                currentListBox.Items.Clear();
                List<Customer> customerList = new List<Customer>(); ;
                try
                {
                    customerList = dealer.getCustomers();
                }
                catch (Exception exception)
                {
                }
                if(customerList.Count < 1 || customersListBox == null)
                {
                    return;
                }
                foreach(Customer customer in customerList)
                {

                    if ((isPrivate == false) && customer is Business)
                    {
                        currentListBox.Items.Add(((Business)customer).nameCompany);
                    }
                    if ((isPrivate == true) && customer is PrivateCustomer )
                    {
                        currentListBox.Items.Add(((PrivateCustomer)customer).name);                    
                    }

                }
            }

            catch (Exception exception)
            {
                throw(new Exception("Error in show clients" + exception.Message));
            }

        }

        /// <summary>
        /// method is called by button Edit Customer it enable text field with customer properties to edition and change flag to edit mode
        /// </summary>
        /// params:
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// rerurns : void
        private void editCustomersButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (customersListBox.SelectedItem == null)
                {
                    throw(new Exception("customer is not selected"));
                }
                makeCustomerFieldsEnabled();
                clientEditOrAddFlag = editOrAddModeFlag.editMode;
            }
            catch (Exception exception)
            {
                MessageBox.Show("Error during editing customer " + exception.Message);
            }
        }

        /// <summary>
        /// method disable customer text fields to edition 
        /// </summary>
        /// <params> none </params>
        /// <returns>void</returns> 
        private void makeCustomerFieldsDisabled()
        {
            customersNameTextBox.IsEnabled = false;
            customerAddressTexBox.IsEnabled = false;
            customerPhoneNumberTextBox.IsEnabled = false;
            customerFaxOrAgeTextBox.IsEnabled = false;
            customerSENSEXoTextBox.IsEnabled = false;
            customerSexGroupBox.IsEnabled = false;
            customerContactPerson.IsEnabled = false;

        }

        /// <summary>
        /// method enable customer text fields to edition
        /// </summary>
        /// <params> none </params>
        /// <returns> void </returns>
        private void makeCustomerFieldsEnabled()
        {
            customersNameTextBox.IsEnabled = true;
            customerAddressTexBox.IsEnabled = true;
            customerPhoneNumberTextBox.IsEnabled = true;
            customerFaxOrAgeTextBox.IsEnabled = true;
            customerSENSEXoTextBox.IsEnabled = true;
            customerSexGroupBox.IsEnabled = true;
            customerContactPerson.IsEnabled = true;
        }

        /// <summary>
        /// method enable set to default customer texboxes and disable it again 
        /// in addition it set flag to noSubmitableMode so user can't submit clear buttons
        /// </summary>
        /// <params> none </params>
        /// <returns>void </returns>
        private void clearCustomerFields()
        {
            makeCustomerFieldsEnabled();
            customerIDTextBox.Text = "";
            customersNameTextBox.Text = "";
            customerAddressTexBox.Text = "";
            customerPhoneNumberTextBox.Text = "";
            customerFaxOrAgeTextBox.Text = "";
            customerSENSEXoTextBox.Text = "";
            customerContactPerson.Text = "";
            clientCarsGridData.ItemsSource = null;
            makeCustomerFieldsDisabled();
            clientEditOrAddFlag = editOrAddModeFlag.noSubmitableMode;
        }

        /// <summary>
        /// method is called when user choose private customer by clicking private client radio button
        /// it change labels to appropriate for private cutomer hide business customer fields and show private customers fields
        /// also method set customer describing texboxes to default by calling method clearCustomerFields()
        /// and it call showClients method to display all private client in client list box
        /// </summary>
        /// <params>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// </params>
        /// <returns>void</returns>
        private void customerPrivateRadioButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SEorSexLabel.Content = "Sex:";
                FaxOrAgeLabel.Content = "Age:";
                customerContactPerson.Visibility = Visibility.Hidden;
                customerContactPersonLabel.Visibility = Visibility.Hidden;
                customerSexGroupBox.Visibility = Visibility.Visible;
                customerSENSEXoTextBox.Visibility = Visibility.Hidden;
                clearCustomerFields();
                showClients();
            }
            catch (Exception exception)
            {
                MessageBox.Show("Error during private button click" + exception.Message);
            }
        }
        /// <summary>
        /// method is called when user choose business customer by clicking business client radio button
        /// it change labels to appropriate for business cutomer hide private customer fields and show business customers fields
        /// also method set customer describing texboxes to default by calling method clearCustomerFields()
        /// and it call showClients method to display all business client in client list box
        /// </summary>
        /// <params>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// </params>
        /// <returns>void</returns>
        private void customerBusinessRadioButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SEorSexLabel.Content = "SE-no:";
                FaxOrAgeLabel.Content = "Fax:";
                customerContactPerson.Visibility = Visibility.Visible;
                customerContactPersonLabel.Visibility = Visibility.Visible;
                customerSexGroupBox.Visibility = Visibility.Hidden;
                customerSENSEXoTextBox.Visibility = Visibility.Visible;
                clearCustomerFields();
                showClients();
            }
            catch (Exception exception)
            {
                MessageBox.Show("Error during radio button click" + exception.Message);
            }
        }

        /// <summary>
        /// method is called when user click on listbox with client and by that select one
        /// it gets customer name form listbox and use getCustomerById method from CarDelaer to get customer object
        /// then it cast it on PrivateCustomer od Business (depends on selected radio button)
        /// and fill text fields and cars data grid describing private or business customer properties and contacts
        /// </summary>
        /// <params>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// </params>
        /// <returns>
        /// void
        /// </returns> 
        private void customersListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                var v = customersListBox.SelectedItem;
                if (v == null)
                {
                    return;
                }
                string selectedCustomer = (string)customersListBox.SelectedItem;
                if (selectedCustomer.Length < 1)
                {
                    throw(new Exception("No client selected"));
                }
                
                    makeCustomerFieldsDisabled();
                    clientCarsGridData.ItemsSource = null;
                    carList.Clear();
                    truckList.Clear();
              
                Customer customer = dealer.getCustomerByName(selectedCustomer);
                if (customer == null)
                {
                    throw(new Exception("problem with client"));
                }
                if (customer is Business && ((Business)customer).nameCompany == selectedCustomer)
                {

                    Business privateCustumer = (Business)customer;
                    customersNameTextBox.Text = privateCustumer.nameCompany;
                    customerAddressTexBox.Text = privateCustumer.adress;
                    customerPhoneNumberTextBox.Text = privateCustumer.phone;
                    customerFaxOrAgeTextBox.Text = privateCustumer.fax.ToString();
                    customerSENSEXoTextBox.Text = privateCustumer.se_no.ToString();
                    customerContactPerson.Text = privateCustumer.contactPersorn;
                    foreach (Leasing leasing in dealer.findLeasingsByClientName(privateCustumer.nameCompany))
                    {
                        truckList.Add(leasing.myTruck);
                    }
                }
                if (customer is PrivateCustomer && ((PrivateCustomer)customer).name == selectedCustomer)
                {
                    PrivateCustomer privateCustumer = (PrivateCustomer)customer;
                    customersNameTextBox.Text = privateCustumer.name;
                    customerAddressTexBox.Text = privateCustumer.adress;
                    customerPhoneNumberTextBox.Text = privateCustumer.phone;
                    customerFaxOrAgeTextBox.Text = privateCustumer.age.ToString();
                    customerSENSEXoTextBox.Text = privateCustumer.sex.ToString();
                    clientCarsGridData.AutoGenerateColumns = false;

                    foreach (Contract contract in dealer.findContractsByClientName(privateCustumer.name))
                    {
                        carList.Add(contract.myCar);
                    }
                    clientCarsGridData.ItemsSource = carList;

                }
            }
            catch (Exception exception)
            {
                MessageBox.Show("Exception during client selection" + exception.Message);
            }
        }
        
        /// <summary>
        /// method is called by double click on cars data grid object it calls virtual method details to show informations about the car
        /// </summary>
        /// <params>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// </params>
        /// <returns>void</returns>
        private void clientCarsGridData_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Vehicule selectedCar = clientCarsGridData.SelectedItem as Vehicule;
            MessageBox.Show(selectedCar.details());
        }

        /// <summary>
        /// method delete cliens selected in client listbox - it's searching client by name( getClientByName form CarDealer method)
        /// ant then delete it (removeClient form CarDealer method)
        /// then it's clear textboxes and car data grid whitch describing appropriate client
        /// and rerender clientListBox 
        /// if customer, client has a contract it throw an exception and removing is impossible
        /// </summary>
        /// <params>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// </params>
        /// <return>void</return>
        private void customerErase_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string customerName = (string)customersListBox.SelectedItem;

                if (customerName.Length > 0)
                {
                    Customer customer = dealer.getCustomerByName(customerName);
                    if( customer is PrivateCustomer )
                    {
                        if( dealer.findContractsByClientName(((PrivateCustomer)customer).name) != null )
                        {
                             throw(new Exception("Can't delete client who has contract/leasing"));
                        }
                    }
                    if( customer is Business)
                    {
                        if (  dealer.findLeasingsByClientName( ((Business)customer).nameCompany) != null)
                        {
                            throw(new Exception("Can't delete client who has contract/leasing"));
                        }
                    }
                    dealer.removeCustomer(customer);
                    clearCustomerFields();
                    showClients();
                }
                else throw (new Exception("name length shorter than 1")); 

            }
            catch (Exception exception)
            {
                MessageBox.Show("Error during remove customer" + exception.Message);
            }
        }

        /// <summary>
        /// method is called when user press add new customer button it's prepare application to add new client it:
        /// clear customer textboxes and enable it - clearCustomerFields and makeCustomerFieldsEnabled methods
        /// and clear customerListBox then it set current tab mode to adding
        /// </summary>
        /// <params>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// </params>
        /// <return>void</return>
        private void addNewCustomerButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                clearCustomerFields();
                customersListBox.Items.Clear();
                makeCustomerFieldsEnabled();
                clientEditOrAddFlag = editOrAddModeFlag.addMode;
            }
            catch (Exception exception)
            {
                MessageBox.Show("Error during adding customer" + exception.Message);
            }
        }

        /// <summary>
        /// method is called when user click submit button it's get values from texboxes then
        /// create new client or change old one (by tab state flag) if user doesn't click before edit or add client button it just show messagebox
        /// that he should do that
        /// </summary>
        /// <params>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// </params>
        /// <return>
        /// void</return>
        private void customerSubmit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (clientEditOrAddFlag == editOrAddModeFlag.noSubmitableMode)
                {
                    MessageBox.Show("Click add client or edit client if you want submit");
                    return;
                }
                if (clientEditOrAddFlag == editOrAddModeFlag.addMode)
                {
                    try
                    {
                        if (customerPrivateRadioButton.IsChecked == true)
                        {

                            dealer.addCustomer(new PrivateCustomer(customerAddressTexBox.Text,
                                customerPhoneNumberTextBox.Text, customersNameTextBox.Text,
                                (customerRadioButtonMale.IsChecked == true) ?
                                            PrivateCustomer.customerSex.male : PrivateCustomer.customerSex.female
                                , int.Parse(customerFaxOrAgeTextBox.Text)));
                        }
                        if (customerBusinessRadioButton.IsChecked == true)
                        {
                            dealer.addCustomer(new Business(customerAddressTexBox.Text,
                                customerPhoneNumberTextBox.Text, customersNameTextBox.Text, customerContactPerson.Text,
                                int.Parse(customerSENSEXoTextBox.Text), int.Parse(customerFaxOrAgeTextBox.Text)));

                        }
                    }
                    catch (Exception exception)
                    {
                        MessageBox.Show("Parsing problem while adding new customer data in customerSubmit_Click " + exception.Message);
                    }
                }
                if (clientEditOrAddFlag == editOrAddModeFlag.editMode)
                {
                    try
                    {
                        string selectedCustomer = (string)customersListBox.SelectedItem;
                        if (selectedCustomer.Length < 1)
                        {
                            throw(new Exception("selected customer length is 0"));
                        }
                        Customer customer = dealer.getCustomerByName(selectedCustomer);
                        if (customerPrivateRadioButton.IsChecked == true)
                        {
                            try
                            {
                                PrivateCustomer privateCustomer = (PrivateCustomer)customer;
                                privateCustomer.changeFields(customerAddressTexBox.Text,
                                    customerPhoneNumberTextBox.Text, customersNameTextBox.Text,
                                    (customerRadioButtonMale.IsChecked == true) ?
                                                PrivateCustomer.customerSex.male : PrivateCustomer.customerSex.female
                                    , int.Parse(customerFaxOrAgeTextBox.Text));
                            }
                            catch (Exception exception)
                            {
                                throw(new Exception("Error during parsing customer to Private Customer and changing fields" + exception.Message));
                            }
                        }
                        if (customerBusinessRadioButton.IsChecked == true)
                        {
                            try
                            {
                                Business business = (Business)customer;
                                business.changeFields(customerAddressTexBox.Text,
                                customerPhoneNumberTextBox.Text, customersNameTextBox.Text, customerContactPerson.Text,
                                int.Parse(customerSENSEXoTextBox.Text), int.Parse(customerFaxOrAgeTextBox.Text));
                            }
                            catch (Exception exception)
                            {
                                throw (new Exception("Error during parsing customer to Business customer and changing fields" + exception.Message));
                            }
                        }
                    }
                    catch (Exception exception)
                    {
                        MessageBox.Show("Error while editing client" + exception.Message);
                    }

                }
                clearCustomerFields();
                showClients();
            }
            catch (Exception exception)
            {
                MessageBox.Show("Error during submiting" + exception.Message);
            }
        }
        #endregion

        #region ContractsRegion
        /// <summary>
        /// the method is called when user click on private radio button or change small/large checkbox or when new contract was added in rent/lease tab -
        /// it means he wants to show him cars because only cars can be contracted by private client
        /// only free cars are shown because we can't rent not free car
        /// or small , large checkbox to filter cars
        /// is add appropriate cars  to carsListBox in contracts tab by checking what kind of cars user want
        /// and then getting it form CarDealer and add to litbox
        /// </summary>
        /// <params>none</params>
        /// <return>void</return>
        private void showCars()
        {
            try
            {
                vehiculeDisplayedListBox.Items.Clear();
                List<Vehicule> vehiculeList = null;
                if (smallCarRadioButton.IsChecked == true && largeCarRadioButton.IsChecked == true)
                {
                    vehiculeList = dealer.getFreeCarsList();
                }
                else if (smallCarRadioButton.IsChecked == true)
                {
                    vehiculeList = dealer.getFreeSmallCarsList();
                }
                else if (largeCarRadioButton.IsChecked == true)
                {
                    vehiculeList = dealer.getFreeCarsList();
                }
                else
                {
                    vehiculeList = null;
                }
                if (vehiculeList != null)
                {
                    foreach (Vehicule vehicule in vehiculeList)
                    {
                        vehiculeDisplayedListBox.Items.Add(vehicule.idVehicule);
                    }
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show("Error in method showCars()" + exception.Message);
            }
        }

        /// <summary>
        /// method is called when user click on buiness radio button in lease/rent tab or new contract was added so -
        /// it means he wants truck because only truck can be leased by businnes customer
        /// then it gets all free trucks and add to carsListBox
        /// </summary>
        /// <params>none</params>
        /// <return>void</return>
        private void showTrucks()
        {
            try
            {
                vehiculeDisplayedListBox.Items.Clear();
                List<Vehicule> truckList = dealer.getFreeTruckList();
                if (truckList.Count > 0)
                {
                    foreach (Vehicule vehicule in dealer.getFreeTruckList())
                    {
                        vehiculeDisplayedListBox.Items.Add(vehicule.idVehicule);
                    }
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show("Error during showing cars" + exception.Message);
            }
        }

        /// <summary>
        /// method is called when user check private radio button in rent/lease tab then it show small/large cars checkbox
        /// and show all private client and free cars and rerender all rent contracts
        /// </summary>
        /// <params>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// </params>
        /// <return>void</return>
        private void privateRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                concratcShowSmallLargeGroupBox.Visibility = Visibility.Visible;
                showClients();
                showCars();
                clearContractFields();
                showContracts();
            }
            catch (Exception exception)
            {
                MessageBox.Show("Error after private radio button clicked" + exception.Message);
            }
        }

        /// <summary>
        /// method is called when user click on business radio button then it hide small/large cars groupbox
        /// it shows all Business clients and all free trucks and rerender all leasing contracts
        /// </summary>
        /// <params>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// </params>
        /// <return>void</return>
        private void businessRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                concratcShowSmallLargeGroupBox.Visibility = Visibility.Hidden;
                showTrucks();
                showClients();
                clearContractFields();
                showContracts();
            }
            catch (Exception exception)
            {
                MessageBox.Show("Error during business radio button clicek" + exception.Message);
            }

        }

        /// <summary>
        /// it gets all leasing or rent contracts - depends on whitch radio button is checked and create new obiect basing by them to
        /// show them in contracts data grid
        /// </summary>
        /// <params>none</params>
        /// <return>void</return>
        private void showContracts()
        {
            try
            {

                List<ContractsParser> contractsList = new List<ContractsParser>();
                if (privateRadioButton.IsChecked == true)
                {
                    foreach (Contract contract in dealer.getContractsList())
                    {
                        contractsList.Add(new ContractsParser(contract.id.ToString(), contract.myCar.idVehicule, contract.myPrivate.name, contract.startDate, contract.endDate));
                    }
                }
                if (businessRadioButton.IsChecked == true)
                {
                    foreach (Leasing leasing in dealer.getLeasingsList())
                    {
                        contractsList.Add(new ContractsParser(leasing.id.ToString(), leasing.myTruck.idVehicule, leasing.myBusiness.nameCompany, leasing.startDate, leasing.endDate));
                    }
                }
                contractManagementContractsDataGrid.ItemsSource = contractsList;
            }
            catch (Exception exception)
            {
                MessageBox.Show("Error in method show contracts " + exception.Message);
            }
        }

        /// <summary>
        /// method is clearing all fields describing contract to prepare it to add new one it is called when contract was added or at the 
        /// beggining of application
        /// </summary>
        /// <params>none</params>
        /// <return>void</return>
        private void clearContractFields()
        {
            contractLargeCheckBox.IsChecked = true;
            contractSmallCheckBox.IsChecked = true;
            vehiculeIDLRTextBox.Text = "";
            colorLRTextBox.Text = "";
            priceMonthTextBox.Text = "";
            startDatePicker.SelectedDate = DateTime.Now;
            toDatePicker.SelectedDate = DateTime.Now;

        }

        /// <summary>
        /// metthod is called when user want add new contract and click submit
        /// it gets customer and vehicule from appropriate list , start date and end date from DatePicker
        /// and create new leasing/contract object and call cardealer method to add it to appropriate list
        /// </summary>
        /// <params>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// </params>
        /// <return>void</return>
        private void submitButton_Click(object sender, RoutedEventArgs e)
        {
            Customer customer = null;
            Vehicule vehicule = null;
            try
            {
                try
                {
                    customer = dealer.getCustomerByName(customerDisplayedListBox.SelectedItem.ToString());
                    if (customer == null)
                    {
                        throw (new Exception("Couldn't find customer"));
                    }
                }
                catch (Exception exception)
                {
                    throw(new Exception("Choose client before add" + exception.Message));
                }
                try
                {
                    vehicule = dealer.getVehiculeById(vehiculeDisplayedListBox.SelectedItem.ToString());
                    if (vehicule == null)
                    {
                        throw (new Exception("Couldn't find customer"));
                    }
                }
                catch (Exception exception)
                {
                    throw( new Exception("Choose vehicule before add" + exception.Message));
                }
                if (privateRadioButton.IsChecked == true)
                {
                    try
                    {
                        dealer.addContract(startDatePicker.SelectedDate.Value, toDatePicker.SelectedDate.Value, (PrivateCustomer)customer, (Car)vehicule);
                        showCars();

                    }
                    catch (Exception exception)
                    {
                        throw(new Exception("Error during adding contract" + exception.Message));
                    }
                }
                if (businessRadioButton.IsChecked == true)
                {
                    try
                    {
                        dealer.addLeasing(toDatePicker.SelectedDate.Value, startDatePicker.SelectedDate.Value,
                            (Business)customer, (Truck)vehicule);
                        showTrucks();
                    }
                    catch (Exception exception)
                    {
                        throw(new Exception("Error during adding Leasing" + exception.Message));
                    }
                }
                MessageBox.Show("Lease/Rent Added");
                clearContractFields();
                vehiculeDisplayedListBox.Items.Remove(vehicule);
                showContracts();
            }
            catch(Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        /// <summary>
        /// method add to texboxes describing a vehicule information about selected in listbox vehicile 
        /// </summary>
        /// <params>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// </params>
        /// <return>void</return>
        private void vehiculeDisplayedListBox_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            Vehicule vehicule;
            try
            {
                object selectedItem = vehiculeDisplayedListBox.SelectedItem;
                if (selectedItem != null)
                {
                    string selectedVehicule = vehiculeDisplayedListBox.SelectedItem.ToString();
                    if (selectedVehicule.Length > 0)
                    {
                        vehicule = dealer.getVehiculeById(selectedVehicule);
                        vehiculeIDLRTextBox.Text = vehicule.idVehicule;
                        colorLRTextBox.Text = vehicule.idVehicule;
                        priceMonthTextBox.Text = vehicule.price.ToString();
                    }
                }
            }
            catch(Exception exception)
            {
                MessageBox.Show("Error during selection vehicule in lease menagement" + exception.Message);
            }

        }
        /// <summary>
        /// method call showCars() method to update informations it's is called when user select or deselect smallcheckboxes
        /// </summary>
        /// <params>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// </params>
        /// <return>void</return>
        private void contractSmallCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            showCars();
        }
        /// <summary>
        /// method call showCars() method to update informations it's is called when user select or deselect largecheckboxes
        /// </summary>
        /// <params>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// </params>
        /// <return>void</return>
        private void contractLargeCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            showCars();
        }

        #endregion

        #region VehiculesRegion
        /// <summary>
        /// Allows to display vehicules when radiobuttons selected and show free vehicules with the checkbox
        /// </summary>
        #region private void showVehicules()
        private void showVehicules()
        {
            try
            {
                vehiculeListBox.Items.Clear();
                vehiculeList = dealer.getVehicules().ToArray();

                if (showAvairiableCheckBox.IsChecked == false)
                {
                    foreach (Vehicule vehicule in vehiculeList)
                    {
                        if (truckRadioButton.IsChecked == true && vehicule is Truck)
                        {
                            vehiculeListBox.Items.Add(((Truck)vehicule).idVehicule);
                            label12.Content = "Lenght";
                            label13.Content = "Weight";
                            label14.Content = "Capacity";
                            capacityTextBox.Visibility = Visibility.Visible;
                            weightTextBox.Visibility = Visibility.Visible;
                            // clearVehiculeField();
                        }

                        if (smallCarRadioButton.IsChecked == true && vehicule is Small)
                        {
                            vehiculeListBox.Items.Add(((Small)vehicule).idVehicule);
                            label12.Content = "Places";
                            label13.Content = "Car type";
                            label14.Content = "";
                            capacityTextBox.Visibility = Visibility.Hidden;
                            weightTextBox.Visibility = Visibility.Visible;
                            showStateVehiculeComboBox();
                            // clearVehiculeField();
                        }

                        if (largeCarRadioButton.IsChecked == true && vehicule is Large)
                        {
                            vehiculeListBox.Items.Add(((Large)vehicule).idVehicule);
                            label12.Content = "Capacity";
                            label13.Content = "";
                            label14.Content = "";
                            capacityTextBox.Visibility = Visibility.Hidden;
                            weightTextBox.Visibility = Visibility.Hidden;
                            // clearVehiculeField();
                        }
                        clearVehiculeField();

                    }
                }

                if (showAvairiableCheckBox.IsChecked == true)
                {
                    foreach (Vehicule vehicule in vehiculeList)
                    {
                        if (truckRadioButton.IsChecked == true && vehicule is Truck && vehicule.currentVehiculeState == Truck.vehiculeState.free)
                        {
                            vehiculeListBox.Items.Add(((Truck)vehicule).idVehicule);
                            label12.Content = "Lenght";
                            label13.Content = "Weight";
                            label14.Content = "Capacity";
                            capacityTextBox.Visibility = Visibility.Visible;
                            //clearVehiculeField();
                        }

                        if (smallCarRadioButton.IsChecked == true && vehicule is Small && vehicule.currentVehiculeState == Small.vehiculeState.free)
                        {
                            vehiculeListBox.Items.Add(((Small)vehicule).idVehicule);
                            label12.Content = "Places";
                            label13.Content = "Car type";
                            label14.Content = "";
                            capacityTextBox.Visibility = Visibility.Hidden;
                            showStateVehiculeComboBox();
                            //clearVehiculeField();
                        }

                        if (largeCarRadioButton.IsChecked == true && vehicule is Large && vehicule.currentVehiculeState == Large.vehiculeState.free)
                        {
                            vehiculeListBox.Items.Add(((Large)vehicule).idVehicule);
                            label12.Content = "Capacity";
                            label13.Content = "";
                            label14.Content = "";
                            capacityTextBox.Visibility = Visibility.Hidden;
                            weightTextBox.Visibility = Visibility.Hidden;
                            //clearVehiculeField();
                        }
                        clearVehiculeField();

                    }
                }



            }
            catch (Exception exception)
            {
                MessageBox.Show("showVehicules method: " + exception.Message);
            }

        }
        #endregion



        /// <summary>
        /// 
        /// </summary>
        #region private void vehiculeTruckRadiobutton_Click
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void vehiculeTruckRadiobutton_Click(object sender, RoutedEventArgs e)
        {
            showVehicules();
        }
        #endregion



        /// <summary>
        /// Allows to clear the vehicules' fields
        /// </summary>
        #region public void clearVehiculeField()
        public void clearVehiculeField()
        {
            registerNumberTextBox.Text = "";
            modelTextBox.Text = "";
            brandTextBox.Text = "";
            colorTextBox.Text = "";
            capacityTextBox.Text = "";
            weightTextBox.Text = "";
            lenghtTextBox.Text = "";
            rentedByTextBox.Text = "";
            priceTextBox.Text = "";
            endOfRentTextBox.Text = "";
            stateTextBox.Text = "";
            vehiculeIDTextBox.Text = "";
        }
        #endregion


        #region vehicules
        /// <summary>
        /// Allow to display information of different vehicules when id vehicules are selected
        ///     - trucks
        ///     - small cars
        ///     - large cars
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void vehiculeListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        private void vehiculeListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string selectedVehicule = (string)vehiculeListBox.SelectedItem;
            foreach (Vehicule vehicule in vehiculeList)
            {
                if (vehicule is Truck && ((Truck)vehicule).idVehicule == selectedVehicule)
                {
                    label12.Content = "Lenght";
                    label13.Content = "Weight";
                    label14.Content = "Capacity";

                    Truck truckVehicule = (Truck)vehicule;
                    registerNumberTextBox.Text = truckVehicule.registerNumber.ToString();
                    brandTextBox.Text = truckVehicule.brand.ToString();
                    modelTextBox.Text = truckVehicule.model.ToString();
                    colorTextBox.Text = truckVehicule.color.ToString();
                    lenghtTextBox.Text = truckVehicule.lenght.ToString();
                    capacityTextBox.Text = truckVehicule.capacity.ToString();
                    weightTextBox.Text = truckVehicule.weight.ToString();
                    stateTextBox.Text = truckVehicule.currentVehiculeState.ToString();
                    priceTextBox.Text = truckVehicule.price.ToString();
                    Leasing leasing = dealer.getLeasingByVehiculeId(truckVehicule.idVehicule);
                    if(leasing != null)
                    {
                        rentedByTextBox.Text = leasing.myBusiness.nameCompany;
                        endOfRentTextBox.Text = leasing.endDate.ToString();
                    }
                   // ContractsParser contract;
                  //  rentedByTextBox.Text = (dealer.getLeasingByVehiculeId(truckVehicule.idVehicule).myBusiness.nameCompany);

               //     endOfRentTextBox.Text = (dealer.getLeasingByVehiculeId(vehicule.idVehicule).endDate).toString();


                }

                if (vehicule is Small && ((Small)vehicule).idVehicule == selectedVehicule)
                {
                    label12.Content = "Places";
                    label13.Content = "Car type";
                    label14.Content = "";

                    Small smallVehicule = (Small)vehicule;
                    registerNumberTextBox.Text = smallVehicule.registerNumber.ToString();
                    brandTextBox.Text = smallVehicule.brand.ToString();
                    modelTextBox.Text = smallVehicule.model.ToString();
                    colorTextBox.Text = smallVehicule.color.ToString();
                    weightTextBox.Text = smallVehicule.carType.ToString(); //The same TextBox as weight for currentstate
                    stateTextBox.Text = smallVehicule.currentVehiculeState.ToString();
                    priceTextBox.Text = smallVehicule.price.ToString();
                    Contract contract = dealer.getContractByVehiculeId(smallVehicule.idVehicule);
                    if(contract != null)
                    {
                        rentedByTextBox.Text = contract.myPrivate.name;
                        endOfRentTextBox.Text = contract.endDate.ToString();
                    }
                 //   rentedByTextBox.Text = (dealer.getContractByVehiculeId(smallVehicule.idVehicule).myPrivate.name);
                 //   endOfRentTextBox.Text = (dealer.getContractByVehiculeId(vehicule.idVehicule).endDate).ToString;


                }


                if (vehicule is Large && ((Large)vehicule).idVehicule == selectedVehicule)
                {
                    label12.Content = "Capacity";
                    label13.Content = "";
                    label14.Content = "";

                    Large largelVehicule = (Large)vehicule;
                    registerNumberTextBox.Text = largelVehicule.registerNumber.ToString();
                    brandTextBox.Text = largelVehicule.brand.ToString();
                    modelTextBox.Text = largelVehicule.model.ToString();
                    colorTextBox.Text = largelVehicule.color.ToString();
                    lenghtTextBox.Text = largelVehicule.capacity.ToString(); //The same TextBox as lenght for capacity
                    capacityTextBox.Text = largelVehicule.capacity.ToString();
                    stateTextBox.Text = largelVehicule.currentVehiculeState.ToString();
                    priceTextBox.Text = largelVehicule.price.ToString();
                    //    rentedByTextBox.Text = .ToString();
                   // rentedByTextBox.Text = (dealer.getContractByVehiculeId(largelVehicule.idVehicule).myPrivate.name);
                    Contract contract = dealer.getContractByVehiculeId(largelVehicule.idVehicule);
                    if (contract != null)
                    {
                        rentedByTextBox.Text = contract.myPrivate.name;
                        endOfRentTextBox.Text = contract.endDate.ToString();
                    }
                }

            }

        }
        #endregion



        /// <summary>
        /// Allows to show the free vehicules
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void showAvairiableCheckBox_Checked(object sender, RoutedEventArgs e)
        private void showAvairiableCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            vehiculeListBox.Items.Clear();
            showVehicules();
        }
        #endregion



        /// <summary>
        /// Valid the adding of the new vehicule and save data in the file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void validNewVehiculeButton_Click(object sender, RoutedEventArgs e)
        private void validNewVehiculeButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                takenVehiculesFields();
                validNewVehiculeButton.Visibility = Visibility.Hidden;
                cancelNewVehiculeButton.Visibility = Visibility.Hidden;
                stateTextBox.Visibility = Visibility.Visible;
                stateComboBox.Visibility = Visibility.Hidden;
                clearVehiculeField();
                label22.Visibility = Visibility.Hidden;
                vehiculeIDTextBox.Visibility = Visibility.Hidden;
                showVehicules();
            }
            catch (Exception exception)
            {
                MessageBox.Show("Error in validNewVehicule" + exception.Message);
            }
        }
        #endregion



        /// <summary>
        /// Add a new vehicule and displayed the buttons valid and cancel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void addVehiculeButton_Click(object sender, RoutedEventArgs e)
        private void addVehiculeButton_Click(object sender, RoutedEventArgs e)
        {
            validNewVehiculeButton.Visibility = Visibility.Visible;
            cancelNewVehiculeButton.Visibility = Visibility.Visible;
            stateTextBox.Visibility = Visibility.Hidden;
            stateComboBox.Visibility = Visibility.Visible;
            label22.Visibility = Visibility.Visible;
            vehiculeIDTextBox.Visibility = Visibility.Visible;
            clearVehiculeField();
        }
        #endregion



        /// <summary>
        /// Canceled the new adding of vehicule and hide the buttons valid and cancel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void button1_Click_1(object sender, RoutedEventArgs e)
        private void button1_Click_1(object sender, RoutedEventArgs e)
        {
            validNewVehiculeButton.Visibility = Visibility.Hidden;
            cancelNewVehiculeButton.Visibility = Visibility.Hidden;
            stateTextBox.Visibility = Visibility.Visible;
            stateComboBox.Visibility = Visibility.Hidden;
            clearVehiculeField();
            label22.Visibility = Visibility.Hidden;
            vehiculeIDTextBox.Visibility = Visibility.Hidden;
        }
        #endregion



        /// <summary>
        /// Allows to get the takens write in the fields
        /// </summary>
        #region private void takenVehiculesFields()
        private void takenVehiculesFields()
        {

            string registerNumber = registerNumberTextBox.Text;
            string brand = brandTextBox.Text;
            string model = modelTextBox.Text;
            string color = colorTextBox.Text;
            string lenght = lenghtTextBox.Text;
            string weight = weightTextBox.Text;
            string capacity = capacityTextBox.Text;
            string rentBy = rentedByTextBox.Text;
            // string state = stateComboBox.SelectedValue.ToString();
            string endOfrent = rentedByTextBox.Text;
            ///////////////////////////////////////////////
            int price;
            if (priceTextBox.Text == "")
            {
                priceTextBox.Text = "0";
            }
             price = int.Parse(priceTextBox.Text);
            //////////////////////////////////////////////
            string idVehicule = vehiculeIDTextBox.Text;
            Vehicule vehicule;

           


            if (smallCarRadioButton.IsChecked == true)
            {
                countId++;
                dealer.addVehicule(vehicule = new Small(model, color, price, idVehicule, lenght, vehiculeState.currentVehiculeState, weight, registerNumber, brand));
            }

            if (truckRadioButton.IsChecked == true)
            {
                countId++;
                dealer.addVehicule(vehicule = new Truck(model, color, price, idVehicule, lenght, weight, vehiculeState.currentVehiculeState, capacity, registerNumber, brand));
            }

            if (largeCarRadioButton.IsChecked == true)
            {
                countId++;
                dealer.addVehicule(vehicule = new Large(model, color, price, idVehicule, vehiculeState.currentVehiculeState, lenght, registerNumber, brand));
            }

            // MessageBox.Show(state);

            // dealer.addVehicule(vehicule);

        }
        #endregion



        /// <summary>
        /// Allows to get action when item is checked in the comboBox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void stateComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        private void stateComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            addStateVehiculeComboBox();
        }
        #endregion



        /// <summary>
        /// Allows to remove a vehicule
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region private void removeVehiculeButton_Click(object sender, RoutedEventArgs e)
        private void removeVehiculeButton_Click(object sender, RoutedEventArgs e)
        {
        }
        #endregion



        /// <summary>
        /// Allows to fill the states in the comboBox
        /// </summary>
        #region public void fillComboBox()
        public void fillComboBox()
        {
            stateComboBox.Items.Add("leased");
            stateComboBox.Items.Add("free");
            stateComboBox.Items.Add("commission");
            stateComboBox.Items.Add("sold");
        }
        #endregion



        /// <summary>
        /// Allows to add the state for a new vehicule
        /// </summary>
        #region public void addStateVehiculeComboBox()
        public void addStateVehiculeComboBox()
        {
            string myState = stateComboBox.SelectedItem.ToString();


            if (String.Compare(myState, "leased") == 0)
            {
                vehiculeState.currentVehiculeState = Vehicule.vehiculeState.leased;
            }
            if (String.Compare(myState, "free") == 0)
            {
                vehiculeState.currentVehiculeState = Vehicule.vehiculeState.free;
            }
            if (String.Compare(myState, "commission") == 0)
            {
                vehiculeState.currentVehiculeState = Vehicule.vehiculeState.commission;
            }
            if (String.Compare(myState, "sold") == 0)
            {
                vehiculeState.currentVehiculeState = Vehicule.vehiculeState.sold;
            }
        }
        #endregion



        /// <summary>
        /// Allows to show the vehicule's state
        /// </summary>
        #region public void showStateVehiculeComboBox()
        public void showStateVehiculeComboBox()
        {
            if (vehiculeState.currentVehiculeState == Vehicule.vehiculeState.leased)
            {
                stateTextBox.Text = "leased";
            }

            if (vehiculeState.currentVehiculeState == Vehicule.vehiculeState.sold)
            {
                stateTextBox.Text = "sold";
            }

            if (vehiculeState.currentVehiculeState == Vehicule.vehiculeState.commission)
            {
                stateTextBox.Text = "commission";
            }

            if (vehiculeState.currentVehiculeState == Vehicule.vehiculeState.free)
            {
                stateTextBox.Text = "free";
            }
        }
        #endregion

        #endregion
        #endregion

        #region properties
        /// <summary>
        /// variable added to check on whitch mode is application adding/editing customer or nosubmittable
        /// </summary>
        editOrAddModeFlag clientEditOrAddFlag = editOrAddModeFlag.noSubmitableMode;

        /// <summary>
        /// car dealer object whitch contains all logic
        /// </summary>
        Cardealer dealer;
        /// <summary>
        /// counter to get id for new created vehicule
        /// </summary>
        static int countId;
        /// <summary>
        /// varible whitch contains vehicule to transfer information between methods
        /// </summary>
        Vehicule vehiculeState = new Vehicule();
        /// <summary>
        /// lit of the cars to transfer informations between methods
        /// </summary>
        private List<Car> carList = new List<Car>();
        /// <summary>
        /// lit of the trucks to transfer informations between methods
        /// </summary>
        private List<Truck> truckList = new List<Truck>();
        /// <summary>
        /// lit of the vehicules to transfer informations between methods
        /// </summary>
        private Vehicule[] vehiculeList;



        #endregion
        #region types
        private enum editOrAddModeFlag
        {
            editMode,
            addMode,
            noSubmitableMode,
        }
        #endregion

        private void customerBusinessRadioButton_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void contractManagementContractsDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
