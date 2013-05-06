using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CarDealer;

namespace CarDealerWebApp
{
    public partial class CustomersTab : System.Web.UI.Page
    {
        static List<Customer> customersList;
        static Customer selectedCustomer;
        static Boolean editingCustomer;
        public static event FeedbackDelegate showTextEvent;


        public void showInfo(String info, Boolean isError)
        {
            Label l = (Label)Page.Master.FindControl("Feedback");
            l.Text = info;
            if (isError == true)
            {
                l.ForeColor = System.Drawing.Color.Red;
            }
            else
            {
                l.ForeColor = System.Drawing.Color.Green;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            SubmitButton.Enabled = false;
            List<Customer> customersList = new List<Customer>();
            showTextEvent += new FeedbackDelegate(showInfo);
            disableCustomerFields();
        }
        
        void disableCustomerFields()
        {
            CustomerNameTextBox.ReadOnly = true;
            CustomerPhoneTextBox.ReadOnly = true;
            AddressTextBox.ReadOnly = true;
            SENoTextBox.ReadOnly = true;
            FaxTextBox.ReadOnly = true;
            ContactPersonTextBox.ReadOnly = true;          
            AgeTextBox.ReadOnly = true;
            SubmitButton.Enabled = false;
            SexRadioButtonList.Enabled = false;
        }
        void enableCustomerFields()
        {
            CustomerNameTextBox.ReadOnly = false;
            CustomerPhoneTextBox.ReadOnly = false;
            AddressTextBox.ReadOnly = false;
            SENoTextBox.ReadOnly = false;
            FaxTextBox.ReadOnly = false;
            ContactPersonTextBox.ReadOnly = false;
            AgeTextBox.ReadOnly = false;
            SubmitButton.Enabled = true;
            SexRadioButtonList.Enabled = true;
        }
        void clearCustomerFields()
        {
            CustomerNameTextBox.Text = "";
            CustomerPhoneTextBox.Text = "";
            AddressTextBox.Text = "";
            SENoTextBox.Text = "";
            FaxTextBox.Text = "";
            ContactPersonTextBox.Text = "";
            AgeTextBox.Text = "";
        }

        void showClients(Boolean? isPrivate)
        {
            clearCustomerFields();
            disableCustomerFields();
            ListBox clientListBox = customersListBox;
            customersListBox.Items.Clear();
            
            try
            {
                customersList = Global.dealer.getCustomers();
                if (customersList.Count < 1 || customersList == null)
                {
                    return;
                }
                foreach (Customer c in customersList)
                {
                    if ((isPrivate == true) && c is PrivateCustomer)
                    {
                        customersListBox.Items.Add(((PrivateCustomer)c).name);
                    }
                    if ((isPrivate == false) && c is Business)
                    {
                        customersListBox.Items.Add(((Business)c).nameCompany);
                    }
                }
              }
            catch (Exception ex)
            {
                if (showTextEvent != null)
                {
                    showTextEvent(ex.ToString(),true);
                }
            }
        }

        protected void CustomersTabPrivateRadioCheckedChanged(object sender, EventArgs e)
        {
            Boolean? isPrivate = clientTypeRadio.SelectedValue.Equals("Private");
            if (isPrivate == false)
            {
                privatePartDiv.Style.Add("display", "none");
                businessPartDiv.Style.Add("display", "inline");
            }
            else
            {
                privatePartDiv.Style.Add("display", "inline");
                businessPartDiv.Style.Add("display", "none");
            }
            showClients(isPrivate);
        }

        protected void customersListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListBox listBox = (ListBox)sender;
            
            try
            {
                var v = listBox.SelectedItem.Value;
                if (v == null)
                {
                    throw (new Exception("No item selected"));
                }
                String name = (string)v;
                Customer c = Global.dealer.getCustomerByName(name);
                selectedCustomer = c;
                CustomerPhoneTextBox.Text = c.phone;
                AddressTextBox.Text = c.adress;
                if (c is Business)
                {
                    Business b = (Business)c;
                    CustomerNameTextBox.Text = b.nameCompany;
                    FaxTextBox.Text = b.fax;
                    ContactPersonTextBox.Text = b.contactPersorn;
                    SENoTextBox.Text = b.se_no.ToString();
                }
                if (c is PrivateCustomer)
                {
                    PrivateCustomer p = (PrivateCustomer)c;
                    CustomerNameTextBox.Text = p.name;
                    AgeTextBox.Text = p.age.ToString();
                    if (p.sex == PrivateCustomer.customerSex.male)
                    {
                        SexRadioButtonList.SelectedIndex = 0;
                    }
                    else
                    {
                        SexRadioButtonList.SelectedIndex = 1;
                    }
                    
                }


            }
            catch(Exception ex)
            {
                if (showTextEvent != null)
                {
                    showTextEvent(ex.ToString(),true);
                }
            }
        }

        protected void EditCustomerButtonClick(object sender, EventArgs e)
        {
            editingCustomer = true;
            enableCustomerFields();
        }

        protected void AddNewCustomerButtonClick(object sender, EventArgs e)
        {
            editingCustomer = false;
            enableCustomerFields();
            clearCustomerFields();
        }

        protected void SubmitClientButton(object sender, EventArgs e)
        {
            try
            {
                if(CustomerNameTextBox.Text.Equals(""))
                {
                    throw(new Exception("you must fufil name field"));
                }
                if (editingCustomer == true)
                {
                    if (clientTypeRadio.SelectedValue.Equals("Private"))
                    {
                        PrivateCustomer p = (PrivateCustomer)selectedCustomer;
                        p.changeFields(AddressTextBox.Text, CustomerPhoneTextBox.Text, CustomerNameTextBox.Text,
                            (SexRadioButtonList.SelectedIndex == 0) ? PrivateCustomer.customerSex.male : PrivateCustomer.customerSex.female
                             , int.Parse(AgeTextBox.Text));
                    }
                    if (clientTypeRadio.SelectedValue.Equals("Business"))
                    {
                        Business b = (Business)selectedCustomer;
                        b.changeFields(AddressTextBox.Text, CustomerPhoneTextBox.Text, CustomerNameTextBox.Text,
                                ContactPersonTextBox.Text, int.Parse(SENoTextBox.Text), FaxTextBox.Text);
                    }
                    editingCustomer = false;
                    showTextEvent("Client information changed", false);
                }
                else
                {
                    if (clientTypeRadio.SelectedValue.Equals("Private"))
                    {
                        Global.dealer.addCustomer(new PrivateCustomer(AddressTextBox.Text, CustomerPhoneTextBox.Text, CustomerNameTextBox.Text,
                            (SexRadioButtonList.SelectedIndex == 0) ? PrivateCustomer.customerSex.male : PrivateCustomer.customerSex.female
                             , int.Parse(AgeTextBox.Text)));
                    }
                    if (clientTypeRadio.SelectedValue.Equals("Business"))
                    {

                        Global.dealer.addCustomer(new Business(AddressTextBox.Text, CustomerPhoneTextBox.Text, CustomerNameTextBox.Text,
                                ContactPersonTextBox.Text, int.Parse(SENoTextBox.Text), FaxTextBox.Text));
                    }
                    showTextEvent("Client added", false);
                }
            }
            catch (Exception ex)
            {
                if (showTextEvent != null)
                {
                    showTextEvent("wrong informations in inbox" + ex ,true);
                }
            }
            clearCustomerFields();
            disableCustomerFields();
            showClients(clientTypeRadio.SelectedValue.Equals("Private")?true:false);
        }

        protected void RemoveSelectedCustomerButtonClick(object sender, EventArgs e)
        {
            try
            {
                Global.dealer.removeCustomer(selectedCustomer);
                clearCustomerFields();
                disableCustomerFields();
                showClients(clientTypeRadio.SelectedValue.Equals("Private") ? true : false);
                showTextEvent("Client removed", false);
            }
            catch (Exception ex)
            {
                if (showTextEvent != null)
                {
                    showTextEvent(ex.ToString(),true);
                }
            }
        }


    }
}