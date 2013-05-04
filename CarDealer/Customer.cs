using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace CarDealer
{
    /* Class Customer
     * Constructors:
     * Customer()
     * Customer(string adress, string phone)
     * Methods:
     * changeFields:
     * params:
     * string phone
     * string address
     * returning void
     */
    [Serializable]
    public class Customer
    {
        public string adress;
        public string phone;
        private static long idCounter=0;

        /// <summary>
        /// Constructor
        /// </summary>
        public Customer()
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public Customer(string adress, string phone)
        {
            this.adress = adress;
            this.phone = phone;
        }


        public void changeFields(string adress, string phone)
        {
            this.adress = adress;
            this.phone = phone;
        }

    }
}
