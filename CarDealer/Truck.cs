using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CarDealer
{
    [Serializable]
    public class Truck : Vehicule
    {
        public string lenght {set; get;}
        public string weight { set; get; }
        public string capacity { set; get; }

        /// <summary>
        /// Constructor
        /// </summary>
        public Truck()
            : base()
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public Truck(string model, string color, int price, string idVehicule, string lenght, string weight, vehiculeState currentVehiculeState, string capacity, string registerNumber, string brand)
            : base(model, color, price, idVehicule, currentVehiculeState, registerNumber, brand)
        {
            this.lenght = lenght;
            this.weight = weight;
            this.capacity = capacity;

        }


        public override string details()
        {
            return base.details() + "\nLength: " + lenght + "\nWeight: " + weight + "\nCapacity: " + capacity;
        }
    }
}
