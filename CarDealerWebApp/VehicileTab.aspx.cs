using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CarDealer;


namespace CarDealerWebApp
{

    public partial class VehicileTab : System.Web.UI.Page
    {
        static List<Vehicule> vehicileList;
        static Vehicule currentVehicile;
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
            showTextEvent += new FeedbackDelegate(showInfo);
        }

        protected void VehicilesListBoxSelectionChanged(object sender, EventArgs e)
        {
            try
            {
                string vehicileName = vehicilesListBox.SelectedValue;
                Vehicule v = Global.dealer.getVehiculeById(vehicileName);
                currentVehicile = v;
                VehicileIdTextBox.Text = v.idVehicule;
                RegisterNumberTextBox.Text = v.registerNumber;
                BrandTextBox.Text = v.brand;
                ModelTextBox.Text = v.model;
                PriceTextBox.Text = v.price.ToString();
                ColorTextBox.Text = v.color;
                if(v.currentVehiculeState == Vehicule.vehiculeState.free)
                {
                    VehicileStateRadioList.SelectedIndex = 0;
                }
                if (v.currentVehiculeState == Vehicule.vehiculeState.commission)
                {
                    VehicileStateRadioList.SelectedIndex = 1;
                }
                if (v.currentVehiculeState == Vehicule.vehiculeState.leased)
                {
                    VehicileStateRadioList.SelectedIndex = 2;
                }
                if (v.currentVehiculeState == Vehicule.vehiculeState.sold)
                {
                    VehicileStateRadioList.SelectedIndex = 3;
                }

                if (vehicileTypeRadio.SelectedValue.Equals("Trucks") && v is Truck)
                {
                    Truck t = (Truck)v;
                    CapacityTextBox2.Text = t.capacity;
                    LengthTextBox.Text = t.lenght;
                    WeightTextBox.Text = t.weight;
                }
                if (vehicileTypeRadio.SelectedValue.Equals("Big Cars") && v is Large)
                {
                    Large l = (Large)v;
                    CapacityTextBox2.Text = l.capacity;
                }
                disableVehicileFields();
            }
            catch (Exception ex)
            {
                if (showTextEvent != null)
                {
                    showTextEvent(ex.ToString(),true);
                }
            }
        }

        protected void VehicilesTabPrivateRadioCheckedChanged(object sender, EventArgs e)
        {
            if (vehicileTypeRadio.SelectedValue.Equals("Trucks") )
            {
                TruckPartDiv.Style.Add("display", "inline");
                capacityPartDiv.Style.Add("display", "inline");
            }
            if (vehicileTypeRadio.SelectedValue.Equals("Small Cars"))
            {
                TruckPartDiv.Style.Add("display", "none");
                capacityPartDiv.Style.Add("display", "none");
            }
            if (vehicileTypeRadio.SelectedValue.Equals("Big Cars"))
            {
                TruckPartDiv.Style.Add("display", "none");
                capacityPartDiv.Style.Add("display", "inline");
            }
            showVehiciles();

        }

        private void showVehiciles()
        {
            try
            {
                vehicileList = Global.dealer.getVehicules();
                vehicilesListBox.Items.Clear();
                foreach (Vehicule v in vehicileList)
                {
                    if (vehicileTypeRadio.SelectedValue.Equals("Trucks") && v is Truck)
                    {
                        vehicilesListBox.Items.Add(v.idVehicule);
                    }
                    if (vehicileTypeRadio.SelectedValue.Equals("Small Cars") && v is Small)
                    {
                        vehicilesListBox.Items.Add(v.idVehicule);
                    }
                    if (vehicileTypeRadio.SelectedValue.Equals("Big Cars") && v is Large)
                    {
                        vehicilesListBox.Items.Add(v.idVehicule);
                    }
                }
                disableVehicileFields();
                clearVehicileFields();
            }
            catch (Exception ex)
            {
                if (showTextEvent != null)
                {
                    showTextEvent(ex.ToString(),true);
                }
            }
        }

        private void disableVehicileFields()
        {
            VehicileIdTextBox.ReadOnly = true;
            RegisterNumberTextBox.ReadOnly = true;
            BrandTextBox.ReadOnly = true;
            ModelTextBox.ReadOnly = true;
            PriceTextBox.ReadOnly = true;
            ColorTextBox.ReadOnly = true;
            RentetOrLeasedTextBox.ReadOnly = true;
            CapacityTextBox2.ReadOnly = true;
            LengthTextBox.ReadOnly = true;
            WeightTextBox.ReadOnly = true;
            VehicileStateRadioList.Enabled = false;
            SubmitButton.Enabled = false;
        }
        private void enableVehicileFields()
        {
            SubmitButton.Enabled = true;
            VehicileIdTextBox.ReadOnly = false;
            RegisterNumberTextBox.ReadOnly = false;
            BrandTextBox.ReadOnly = false;
            ModelTextBox.ReadOnly = false;
            PriceTextBox.ReadOnly = false;
            ColorTextBox.ReadOnly = false;
            RentetOrLeasedTextBox.ReadOnly = false;
            CapacityTextBox2.ReadOnly = false;
            LengthTextBox.ReadOnly = false;
            WeightTextBox.ReadOnly = false;
            VehicileStateRadioList.Enabled = true;
        }
        private void clearVehicileFields()
        {
            VehicileIdTextBox.Text = "";
            RegisterNumberTextBox.Text = "";
            BrandTextBox.Text = "";
            ModelTextBox.Text = "";
            PriceTextBox.Text = "";
            ColorTextBox.Text = "";
            RentetOrLeasedTextBox.Text = "";
            CapacityTextBox2.Text = "";
            LengthTextBox.Text = "";
            WeightTextBox.Text = "";
        }

        protected void SubmitVehicule(object sender, EventArgs e)
        {
            try
            {
                if(VehicileIdTextBox.Text.Equals(""))
                {
                    throw(new Exception("you must fufil name field"));
                }
                if (vehicileTypeRadio.SelectedValue.Equals("Trucks"))
                {

                    Global.dealer.addVehicule(new Truck(ModelTextBox.Text,ColorTextBox.Text,int.Parse(PriceTextBox.Text), 
                        VehicileIdTextBox.Text, LengthTextBox.Text,WeightTextBox.Text,
                        (Vehicule.vehiculeState)VehicileStateRadioList.SelectedIndex ,CapacityTextBox2.Text,
                        RegisterNumberTextBox.Text, BrandTextBox.Text));
                }
                if (vehicileTypeRadio.SelectedValue.Equals("Small Cars"))
                {
                    Global.dealer.addVehicule(new Small(ModelTextBox.Text , ColorTextBox.Text, int.Parse(PriceTextBox.Text), 
                        VehicileIdTextBox.Text, (Vehicule.vehiculeState)VehicileStateRadioList.SelectedIndex ,
                            RegisterNumberTextBox.Text, BrandTextBox.Text));
                }
                if (vehicileTypeRadio.SelectedValue.Equals("Big Cars"))
                {
                    Global.dealer.addVehicule(new Large(ModelTextBox.Text, ColorTextBox.Text, int.Parse(PriceTextBox.Text),
                      VehicileIdTextBox.Text, (Vehicule.vehiculeState)VehicileStateRadioList.SelectedIndex, CapacityTextBox2.Text,
                      RegisterNumberTextBox.Text, BrandTextBox.Text));
                }
                disableVehicileFields();
                showVehiciles();
                showTextEvent("Vehicile added", false);
            }
            catch (Exception ex)
            {
                if (showTextEvent != null)
                {
                    showTextEvent("problem with fields" + ex,true);
                }
            }
        }
        protected void AddNewVehicileButtonClick(object sender, EventArgs e)
        {
            enableVehicileFields();
            clearVehicileFields();
        }

        protected void RemoveSelectedVehicileButtonClick(object sender, EventArgs e)
        {
            try
            {
                Global.dealer.removeVehicule(currentVehicile);
                clearVehicileFields();
                disableVehicileFields();
                showVehiciles();
                showTextEvent("Vehicile removed", false);
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