using EasterRaces.Models.Cars.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace EasterRaces.Models.Cars.Entities
{
    public abstract class Car : ICar
    {
        private string model;
        private int horsePower;
        private int minHorse;
        private int maxHorse;
        private double cubicCentimeters;
        protected Car(string model, int horsePower, double cubicCentimeters, int minHorsePower, int maxHorsePower)
        {
            this.Model = model;
            this.HorsePower = horsePower;
            this.minHorse = minHorsePower;
            this.maxHorse = maxHorsePower;
            this.CubicCentimeters = cubicCentimeters;
            
        }
        public string Model 
        {
            get 
            {
                return this.model;
            }
            private set 
            {
                if (string.IsNullOrWhiteSpace(value) || value.Length<4)
                {
                    
                    throw new ArgumentException($"Model {model} cannot be less than 4 symbols.");
                }
                this.model = value;
            }
        }

        public int HorsePower 
        {
            get 
            {
                return this.horsePower;
            }
            private set 
            {
                if (minHorse>value || maxHorse<value)
                {
                    throw new ArgumentException(Utilities.Messages.ExceptionMessages.InvalidHorsePower, this.HorsePower.ToString());
                }
                this.horsePower = value;
            }

        }

        public double CubicCentimeters 
        {
            get 
            {
                return this.cubicCentimeters;
            }
           protected set 
            {
                this.cubicCentimeters = value;
            }
        }

        public double CalculateRacePoints(int laps)
        {
            return this.CubicCentimeters / this.HorsePower * laps;
        }
    }
}
