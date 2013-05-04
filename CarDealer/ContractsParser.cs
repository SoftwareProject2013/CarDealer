using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CarDealer
{
    public class ContractsParser
    {
        public string id { set; get; }
        public string vehicule { set; get; }
        public string customer { set; get; }
        public DateTime startDate { set; get; }
        public DateTime endDate { set; get; }

        /// <summary>
        /// Constructor
        /// </summary>
        public ContractsParser() { }

        /// <summary>
        /// Constructor
        /// </summary>
        public ContractsParser(string id, string vehicule, string customer, DateTime startDate, DateTime endDate)
        {
            this.id = id;
            this.vehicule = vehicule;
            this.customer = customer;
            this.startDate = startDate;
            this.endDate = endDate;
        }
    }
}
