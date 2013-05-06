using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CarDealer
{
    [Serializable]
    public class Small : Car
    {
      //  public carTypes carType { get; set; }
        /// <summary>
        /// Constructor
        /// </summary>
         public Small()
            : base()
        {
        }

         /// <summary>
         /// Constructor
         /// </summary>
         public Small(string model, string color, int price, string idVehicule, vehiculeState currentVehiculeState, string registerNumber, string brand)
             : base(model, color, price, idVehicule, currentVehiculeState, registerNumber, brand)
         {
         }

     /*   public enum carTypes
        {
            sedan,
            limusine,
            sport,
        }*/
        public override string details()
        {
            return base.details() + "\nCar type: ";
        }
    }
}
