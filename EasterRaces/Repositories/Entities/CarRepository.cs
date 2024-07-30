using EasterRaces.Models.Cars.Contracts;
using EasterRaces.Repositories.Contracts;
using EasterRaces.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EasterRaces.Repositories.Entities
{
   public class CarRepository:IRepository<ICar>
    {
        private  Dictionary<string, ICar> carByModel;

        public CarRepository()
        {
            carByModel = new Dictionary<string, ICar>();
        }

        public ICar GetByName(string name)
        {
            return this.carByModel.GetValueOrDefault(name);

            //ICar car = null;
            //if (this.carByModel.ContainsKey(name))
            //{
            //    car = this.carByModel[name];
            //}

            //return car;
        }

        public IReadOnlyCollection<ICar> GetAll() => this.carByModel.Values.ToList();

        public void Add(ICar model)
        {
            if (this.carByModel.ContainsKey(model.Model))
            {
                throw new ArgumentException(string.Format(ExceptionMessages.CarExists, model.Model));
            }

            this.carByModel.Add(model.Model, model);
        }

        public bool Remove(ICar model) => this.carByModel.Remove(model.Model);
    }
}
