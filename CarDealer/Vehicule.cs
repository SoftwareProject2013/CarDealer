using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CarDealer
{
    [Serializable]
    public class Vehicule
    {
        public string brand { set; get; }
        public string model { set; get; }
        public string color { set; get; }
        public int price { set; get; }
        public string idVehicule { set; get; }
        public string registerNumber { set; get; }
        public vehiculeState currentVehiculeState;

        /// <summary>
        /// Constructor
        /// </summary>
        public Vehicule()
        {
            currentVehiculeState = vehiculeState.free;

        }

        public virtual string details()
        {
            string message = "ID: " + idVehicule + "\nModel" + model;
            message += "\nPrice: " + price + "\nState: " + currentVehiculeState.ToString();
            return message;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public Vehicule(string model, string color, int price, string idVehicule, vehiculeState currentVehiculeState, string registerNumber, string brand)
        {
            this.brand = brand;
            this.model = model;
            this.color = color;
            this.price = price;
            this.idVehicule = idVehicule;
            this.currentVehiculeState = currentVehiculeState;
            this.registerNumber = registerNumber;
        }

        /// <summary>
        ///  //Enumeration of vehicule states
        /// </summary>
        public enum vehiculeState
        {
            commission,
            sold,
            leased,
            free,
        }
        
    }
}
