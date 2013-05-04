using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CarDealer
{
    [Serializable]
    public class Car : Vehicule
    {
        public int placeSeat;


        /// <summary>
        /// Constructor
        /// </summary>
        public Car()
            : base()
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public Car(string model, string color, int price, string idVehicule, vehiculeState currentVehiculeState, string registerNumber, string brand)
            : base(model, color, price, idVehicule, currentVehiculeState, registerNumber, brand)
        {
            this.price = price;
        }

        public override string details()
        {
            return base.details() + "\nSeat place nr: " + placeSeat;
        }

    }
}
