using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CarDealer
{
    [Serializable]
    public class Large : Car
    {
        public string capacity;

        /// <summary>
        /// Constructor
        /// </summary>
        public Large()
            : base()
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public Large(string model, string color, int price, string idVehicule, vehiculeState currentVehiculeState, string capacity, string registerNumber, string brand)
            : base(model, color, price, idVehicule, currentVehiculeState, registerNumber, brand)
        {
            this.capacity = capacity;
        }


        public override string details()
        {
            return base.details() + "\nCapacity: " + capacity;
        }

    }
}
