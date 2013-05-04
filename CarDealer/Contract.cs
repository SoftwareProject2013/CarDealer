
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CarDealer
{
    [Serializable]
    public class Contract
    {
        public DateTime startDate;
        public DateTime endDate;
        public long id { get; private set; }
        private static long IDcounter;
        public PrivateCustomer myPrivate{get; set;}
        public Car myCar { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public Contract()
        {
            id = IDcounter++;
        }


        /// <summary>
        /// Constructor
        /// </summary>
        public Contract(DateTime startDate, DateTime endDate, PrivateCustomer privateCustomer, Car myCar)
        {
            this.startDate = startDate;
            this.endDate = endDate;
            this.myPrivate = privateCustomer;
            this.myCar = myCar;
            id = IDcounter++;
        }

    }
}
