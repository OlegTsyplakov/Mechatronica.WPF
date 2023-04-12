using Mechatronica.DB.Interfaces;
using Mechatronica.DB.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;

namespace Mechatronica.DB.Repository
{
    public class Repository : IRepository
    {
        private readonly AppDbContext _appDbContext;

        public Repository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task AddCarAsync(CarDbModel car)
        {
                _appDbContext.Cars.Add(car);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task AddPersonAsync(PersonDbModel person)
        {
                _appDbContext.Persons.Add(person);
            await _appDbContext.SaveChangesAsync();
        }

        public IQueryable<CarDbModel> GetAllCars()
        {
            return _appDbContext.Cars;
        }

        public IQueryable<PersonDbModel> GetAllPersons()
        {
            return _appDbContext.Persons;
        }

        public async Task AddMainAsync(MainDbModel main)
        {
                _appDbContext.Main.Add(main);
            await _appDbContext.SaveChangesAsync();
        }

        public IQueryable<MainDbModel> GetAll()
        {
            return _appDbContext.Main;
        }
        public ObservableCollection<MainDbModel> GetObservableCollectionMainDbModel()
        {
            return _appDbContext.Main.Local.ToObservableCollection();
        }
        public async Task UpdateMainAsync(MainDbModel model)
        {
            var mainDbModel = GetAll().First(x => x.Date == model.Date);
            if (mainDbModel != null)
            {
                mainDbModel.Person = model.Person;
                mainDbModel.Car = model.Car;
               await _appDbContext.SaveChangesAsync();
            }
            
        }

        public void AddCar(CarDbModel car)
        {
            _appDbContext.Cars.Add(car);
             _appDbContext.SaveChanges();
        }

        public void AddPerson(PersonDbModel person)
        {
            _appDbContext.Persons.Add(person);
            _appDbContext.SaveChanges();
        }
        public void AddMain(MainDbModel main)
        {
            _appDbContext.Main.Add(main);
            _appDbContext.SaveChanges();
        }

        public void UpdateMain(MainDbModel model)
        {
            var mainDbModel = GetAll().First(x => x.Date == model.Date);
            if (mainDbModel != null)
            {
                mainDbModel.Person = model.Person;
                mainDbModel.Car = model.Car;
                _appDbContext.SaveChanges();
            }
        }

    
    }
}
