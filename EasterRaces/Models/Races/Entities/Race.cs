using EasterRaces.Models.Drivers.Contracts;
using EasterRaces.Models.Races.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EasterRaces.Models.Races.Entities
{
    public class Race : IRace
    {
        private string name;
        private int laps;
        private List<IDriver> drivers;
        public Race(string name,int laps)
        {
            this.Name = name;
            this.Laps = laps;
            this.drivers = new List<IDriver>();
        }
        public string Name
        {
            get 
            {
                return this.name;
            }
            private set 
            {
                if (string.IsNullOrEmpty(value) || value.Length<5)
                {
                    throw new ArgumentException($"Name {name} cannot be less than 5 symbols.");
                }
                this.name = value;
            }
        }

        public int Laps
        {
            get 
            {
                return this.laps;
            }
            private set 
            {
                if (value<1)
                {
                    throw new ArgumentException("Laps cannot be less than 1.");
                }

                this.laps = value;
            }
        }

        public IReadOnlyCollection<IDriver> Drivers => drivers.ToList();

        public void AddDriver(IDriver driver)
        {
            IDriver d = drivers.FirstOrDefault(x => x == driver);
            if (d==null)
            {
                throw new ArgumentNullException($"Driver {driver.Name} could not participate in race.");
            }
            bool isExist = drivers.Any(d => d == driver);
            if (isExist)
            {
                throw new ArgumentNullException($"Driver {driver.Name} is already added in {this.Name} race.");
            }

            drivers.Add(driver);
        }
    }
}
