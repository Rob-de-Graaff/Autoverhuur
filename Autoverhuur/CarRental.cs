using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autoverhuur
{
    class CarRental
    {
        private string name;
        private double price;
        private double distangeCharge;
        private int milage;
        private double fuelCapacity;

        public string Name
        {
            set { name = value; }
            get { return name; }
        }

        public double Price
        {
            set { price = value; }
            get { return price; }
            
        }

        public double DistangeCharge
        {
            set { distangeCharge = value; }
            get { return distangeCharge; }
        }

        public int Milage
        {
            set { milage = value; }
            get { return milage; }
        }

        public double FuelCapacity
        {
            set { fuelCapacity = value; }
            get { return fuelCapacity; }
        }

        public CarRental(string carSubstriptionName, double carSubscriptionPrice, double carDistanceCharge,
            int carMilage, double carFuelCapacity)
        {
            name = carSubstriptionName;
            price = carSubscriptionPrice;
            distangeCharge = carDistanceCharge;
            milage = carMilage;
            fuelCapacity = carFuelCapacity;
        }
    }
}
