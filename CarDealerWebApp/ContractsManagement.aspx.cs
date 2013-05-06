using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CarDealer;
namespace CarDealerWebApp
{
    public partial class ContractsManagement : System.Web.UI.Page
    {
        static List<Customer> customersList;
        static List<Vehicule> vehicileList;
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
            showTextEvent += new FeedbackDelegate(showInfo);
        }

        void showClients(Boolean? isPrivate)
        {
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
            try
            {
                Boolean? isPrivate = clientTypeRadio.SelectedValue.Equals("Private");
                showClients(isPrivate);
                showVehiciles();
                showContracts(isPrivate);
            }
            catch (Exception ex)
            {
                if (showTextEvent != null)
                {
                    showTextEvent(ex.ToString(),true);
                }
            }
        }

        private void showContracts(bool? isPrivate)
        {
            List<ContractsParser> parser = new List<ContractsParser>();
            if (isPrivate == true)
            {
                List<Contract> contractsList = Global.dealer.getContractsList();
                foreach (Contract c in contractsList)
                {
                    parser.Add(new ContractsParser(c.id.ToString(), c.myCar.idVehicule, c.myPrivate.name, c.startDate,c.endDate));
                }
            }
            if (isPrivate == false)
            {
                List<Leasing> leasingList = Global.dealer.getLeasingsList();
                foreach (Leasing l in leasingList)
                {
                    parser.Add(new ContractsParser(l.id.ToString(),l.myTruck.idVehicule,l.myBusiness.nameCompany,l.startDate,l.endDate));
                }
            }
            GridView1.DataSource = parser;
            GridView1.DataBind();
        }

        protected void customersListBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void showVehiciles()
        {
            vehicileList = Global.dealer.getVehicules();
            vehicilesListBox.Items.Clear();
            Boolean? isPrivate = clientTypeRadio.SelectedValue.Equals("Private");
            foreach (Vehicule v in vehicileList)
            {
                if (v.currentVehiculeState == Vehicule.vehiculeState.free)
                {
                    if (isPrivate == true)
                    {
                        if (v is Small && SmallCarCheck.Checked == true)
                        {
                            vehicilesListBox.Items.Add(v.idVehicule);
                        }
                        if (v is Large && LargeCarCheck.Checked == true)
                        {
                            vehicilesListBox.Items.Add(v.idVehicule);
                        }
                    }
                    else
                    {
                        if (v is Truck)
                        {
                            vehicilesListBox.Items.Add(v.idVehicule);
                        }
                    }
                }
            }
        }



        protected void SubmitContract(object sender, EventArgs e)
        {
            try
            {
                String cname = customersListBox.SelectedItem.Value;
                String vid = vehicilesListBox.SelectedItem.Value;
                if(cname == null || vid == null)
                {
                    throw(new Exception("you must select customer and vehicile"));
                }
                
                Vehicule v = Global.dealer.getVehiculeById(vid);
                Customer c = Global.dealer.getCustomerByName(cname);
                if(clientTypeRadio.SelectedValue.Equals("Private"))
                {
                    Global.dealer.addContract(CalendarFrom.SelectedDate.Date ,CalendarTo.SelectedDate.Date,(PrivateCustomer)c, (Car)v);
                }
                else
                {
                    Global.dealer.addLeasing(CalendarFrom.SelectedDate.Date ,CalendarTo.SelectedDate.Date,(Business)c, (Truck)v);
                }
                showTextEvent("Contract added",false);
            }
            catch (Exception ex)
            {
                if (showTextEvent != null)
                {
                    showTextEvent("problem with submitting" + "select appropriate fields\n("  + ex + ")",true);
                }
            }
            showClients(clientTypeRadio.SelectedValue.Equals("Private"));
            showVehiciles();
            showContracts(clientTypeRadio.SelectedValue.Equals("Private"));
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

    }
}