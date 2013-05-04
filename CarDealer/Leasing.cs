using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CarDealer
{
    [Serializable]
    public class Leasing
    {
        public DateTime startDate;
        public DateTime endDate;
        public long id { get; private set; }
        private static long IDcounter;

        public Business myBusiness { private set; get; }
        public Truck myTruck { private set; get; }

        /// <summary>
        /// Constructor
        /// </summary>
        Leasing()
        {
            id = IDcounter++;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public Leasing(DateTime endDate, DateTime startDate, Business myBusiness, Truck myTruck)
        {
            id = IDcounter++;
            this.startDate = startDate;
            this.endDate = endDate;
            this.myBusiness = myBusiness;
            this.myTruck = myTruck;
        }
    }
}
