using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CarDealer
{
    [Serializable]
    public class PrivateCustomer : Customer
    {

        public string name { get; set; }
        public customerSex sex { get; set; }
        public int age { get; set; }
        private List<Car> carList = new List<Car>();

        /// <summary>
        /// Constructor
        /// </summary>
        public PrivateCustomer()
            : base()
        {
            carList = null;
        }


        /// <summary>
        /// Constructor
        /// </summary>
        public PrivateCustomer(string adress, string phone, string name, customerSex sex, int age)
            : base(adress, phone)
        {
            this.name = name;
            this.sex = sex;
            this.age = age;
        }


        /// <summary>
        /// Method to add car objects in the carList
        /// </summary>
        public void addCar(Car car)
        {
            carList.Add(car);            
        }

        /// <summary>
        /// Method to remove car objects in the carList
        /// </summary>
        public void removeCar(Car car)
        {
            carList.Remove(car);
        }


        /// <summary>
        /// Ennumeration of customers' sex
        /// </summary>
        public enum customerSex
        {
            male,
            female,
        }


        public void changeFields(string adress, string phone, string name, customerSex sex, int age)
        {
            this.adress = adress;
            this.phone = phone;
            this.name = name;
            this.sex = sex;
            this.age = age;
        }
    }
}
