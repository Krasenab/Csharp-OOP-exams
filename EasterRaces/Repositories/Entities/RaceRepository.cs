using EasterRaces.Models.Races.Contracts;
using EasterRaces.Repositories.Contracts;
using EasterRaces.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EasterRaces.Repositories.Entities
{
   public class RaceRepository:IRepository<IRace>
    {
        private Dictionary<string, IRace> raceByName;
        public RaceRepository()
        {
            raceByName = new Dictionary<string, IRace>();
        }

        public void Add(IRace model)
        {
            if (raceByName.ContainsKey(model.Name))
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.RaceExists, model.Name));
            }

            raceByName.Add(model.Name, model);
        }

        public IReadOnlyCollection<IRace> GetAll()
        {
            return this.raceByName.Values.ToList();
        }

        public IRace GetByName(string name)
        {
            return raceByName.GetValueOrDefault(name);
        }

        public bool Remove(IRace model)
        {
            if (!raceByName.ContainsKey(model.Name))
            {
                return false;
            }

            raceByName.Remove(model.Name);
            return true;
        }
    }
}
