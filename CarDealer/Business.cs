using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CarDealer
{
    [Serializable]
    public class Business : Customer
    {

        public string nameCompany { get; set; }
        public string contactPersorn { get; set; }
        public int se_no { get; set; }
        public string fax { get; set; }
        public List<Truck> truckList = new List<Truck>();

        /// <summary>
        /// Constructor
        /// </summary>
        public Business()
            : base()
        {
            truckList = null;
        }


        /// <summary>
        /// Constructor
        /// </summary>
        public Business(string adress, string phone, string nameCompany, string contactPersorn, int se_no, string fax)
            : base(adress, phone)
        {
            this.nameCompany = nameCompany;
            this.contactPersorn = contactPersorn;
            this.se_no = se_no;
            this.fax = fax;
        }



        public void changeFields(string adress, string phone, string nameCompany, string contactPersorn, int se_no, string fax)
        {
            base.changeFields(adress, phone);
            this.nameCompany = nameCompany;
            this.contactPersorn = contactPersorn;
            this.se_no = se_no;
            this.fax = fax;
        }

      }
}
