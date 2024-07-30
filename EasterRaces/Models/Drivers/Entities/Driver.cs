using EasterRaces.Models.Cars.Contracts;
using EasterRaces.Models.Drivers.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace EasterRaces.Models.Drivers.Entities
{
    public class Driver : IDriver
    {
        private string name;
        public Driver(string name)
        {
            this.Name = name;
            this.CanParticipate = false;
        }
        public string Name 
        {
            get { return this.name; }
            private set 
            {
                if (string.IsNullOrEmpty(value)|| value.Length<5)
                {
                    throw new ArgumentException($"Name {name} cannot be less than 5 symbols.");
                }
                this.name = value;
            }
        }

        public ICar Car { get; private set; }

        public int NumberOfWins { get; private set; }

        public bool CanParticipate { get; private set; }

        public void AddCar(ICar car)
        {
            if (car == null)
            {
                throw new ArgumentNullException(Utilities.Messages.ExceptionMessages.CarInvalid);
            }
            this.Car = car;

        }

        public void WinRace()
        {
            this.NumberOfWins++;
        }
    }
}
