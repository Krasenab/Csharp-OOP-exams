using EasterRaces.Core.Contracts;
using EasterRaces.Models.Cars.Contracts;
using EasterRaces.Models.Cars.Entities;
using EasterRaces.Models.Drivers.Contracts;
using EasterRaces.Models.Drivers.Entities;
using EasterRaces.Models.Races.Contracts;
using EasterRaces.Models.Races.Entities;
using EasterRaces.Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EasterRaces.Core.Entities
{
    public class ChampionshipController : IChampionshipController
    {
        private DriverRepository driverRepository;
        private CarRepository carRepository;
        private RaceRepository raceRepository;
        public ChampionshipController()
        {
            driverRepository = new DriverRepository();
            carRepository = new CarRepository();
            raceRepository = new RaceRepository();
        }
        public string AddCarToDriver(string driverName, string carModel)
        {
            List<IDriver> drivers = driverRepository.GetAll().ToList();
            if (!drivers.Any(x=>x.Name==driverName))
            {
                throw new InvalidOperationException($"Driver {driverName} could not be found.");
            }

            List<ICar> cars = carRepository.GetAll().ToList();
            if (!cars.Any(x=>x.Model==carModel))
            {
                throw new InvalidOperationException($"Car {carModel} could not be found.");
            }

            IDriver driver = driverRepository.GetByName(driverName);
            ICar car = carRepository.GetByName(carModel);
            driver.AddCar(car);


            return $"Driver {driverName} received car {carModel}.";
        }

        public string AddDriverToRace(string raceName, string driverName)
        {
            List<IRace> r = raceRepository.GetAll().ToList();

            if (!r.Any(c=>c.Name==raceName))
            {
                throw new InvalidOperationException($"Race {raceName} could not be found.");
            }

            List<IDriver> dr = driverRepository.GetAll().ToList();

            if (!dr.Any(d=>d.Name==driverName))
            {
                throw new InvalidOperationException($"Driver {driverName} could not be found.");
            }

            IDriver driver = driverRepository.GetByName(driverName);
            IRace race = raceRepository.GetByName(raceName);

            race.AddDriver(driver);

            return $"Driver {driver.Name} added in {race.Name} race.";
        }

        public string CreateCar(string type, string model, int horsePower)
        {
            ICar c = null;
            if (type=="MuscleCar")
            {
                c = new MuscleCar(model, horsePower);
            }
            if (type=="SportsCar")
            {
                c = new SportsCar(model, horsePower);
            }
            List<ICar> cList = carRepository.GetAll().ToList();
            if (cList.Any(x=>x==c))
            {
                throw new ArgumentException("Car {model} is already created.");
            }
            carRepository.Add(c);

            return $"{c.GetType().Name} {model} is created.";
        }

        public string CreateDriver(string driverName)
        {
            List<IDriver> d = driverRepository.GetAll().ToList();
            if (d.Any(x=>x.Name==driverName))
            {
                throw new ArgumentException($"Driver {driverName} is already created.");
            }

            IDriver drive = new Driver(driverName);
            driverRepository.Add(drive);

            return $"Driver {drive.Name} is created.";
        }

        public string CreateRace(string name, int laps)
        {
            IRace r = new Race(name, laps);

            List<IRace> races = raceRepository.GetAll().ToList();
            if (races.Any(x=>x==r))
            {
                throw new InvalidOperationException($"Race {name} is already create.");
            }

            raceRepository.Add(r);

            return $"Race {name} is created.";
        }

        public string StartRace(string raceName)
        {
            IRace r = raceRepository.GetByName(raceName);
            if (r==null)
            {
                throw new ArgumentException($"Race {raceName} could not be found.");
            }
            List<IDriver> drivers = driverRepository.GetAll().OrderByDescending(x => x.Car.CalculateRacePoints(r.Laps)).Take(3).ToList();
            raceRepository.Remove(r);
            StringBuilder sb = new StringBuilder();
            int count = 0;

            for (int i = 0; i < drivers.Count; i++)
            {
                if (count==0)
                {
                    sb.AppendLine($"Driver {drivers[i].Name} wins {raceName} race.");
                }
                else if (count==1)
                {
                    sb.AppendLine($"Driver {drivers[i].Name} is second in {raceName} race.");
                }
                else if (count==2)
                {
                    sb.AppendLine($"Driver {drivers[i].Name} is third in {raceName} race.");
                }
            }

            return sb.ToString().TrimEnd();
           
        }
    }
}
