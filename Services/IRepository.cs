using System.Collections.Generic;

namespace Mazada.Services
{
    interface IRepository<T>
    {
        //Create an entity
        void Add(T entity);
        //Update an entity
        void Update(T entity);
        //Delete an entity
        void Delete(T entity);
        //Get an entity by Id
        T GetById(int id);
        //Get all the list
        IEnumerable<T> GetAll();

    }
}
