using System;
using System.Collections.Generic;
using System.Linq;
using EasterRaces.Repositories.Contracts;


namespace EasterRaces.Repositories.Entities
{
    public class Repository<T> : IRepository<T>
    {
        private List<T> models;
        protected Repository()
        {
            this.models = new List<T>();
        }
        public void Add(T model)
        {
            models.Add(model);
        }

        public IReadOnlyCollection<T> GetAll()
        {
            IReadOnlyCollection<T> ts = null;
            ts = models;

            return ts;
        }

        public T GetByName(string name)
        {
            T names = models.FirstOrDefault(x => x.GetType().Name == name);

            return names;
        }

        public bool Remove(T model)
        {
            T entityForRemove = models.FirstOrDefault(x => x.GetType().Name == model.GetType().Name);
            models.Remove(entityForRemove);
            return true;
        }
    }
}
