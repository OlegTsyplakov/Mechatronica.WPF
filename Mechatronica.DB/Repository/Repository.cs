using Mechatronica.DB.Interfaces;
using Mechatronica.DB.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace Mechatronica.DB.Repository
{
    public class Repository : IRepository
    {
        private readonly AppDbContext _appDbContext;

        public Repository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public void AddCar(CarDbModel car)
        {
            try
            {
                _appDbContext.Cars.Add(car);
            }
            catch (Exception)
            {


            }
        }

        public void AddPerson(PersonDbModel person)
        {
            try
            {
                _appDbContext.Persons.Add(person);
            }
            catch (Exception)
            {


            }
        }

        public IQueryable<CarDbModel> GetAllCars()
        {
            return _appDbContext.Cars;
        }

        public IQueryable<PersonDbModel> GetAllPersons()
        {
            return _appDbContext.Persons;
        }

        public void Dummy()
        {
            Debug.WriteLine("Dymmy");
            Console.WriteLine("Dymmy");
        }
    }
}
