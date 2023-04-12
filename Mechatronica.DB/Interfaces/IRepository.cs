using Mechatronica.DB.Models;
using System.Collections.ObjectModel;

namespace Mechatronica.DB.Interfaces
{
    public interface IRepository
    {
        Task AddCarAsync(CarDbModel car);
        void AddCar(CarDbModel car);
        IQueryable<CarDbModel> GetAllCars();
        Task AddPersonAsync(PersonDbModel person);
        void AddPerson(PersonDbModel person);
        IQueryable<PersonDbModel> GetAllPersons();

        Task AddMainAsync(MainDbModel main);
        void AddMain(MainDbModel main);

        Task UpdateMainAsync(MainDbModel main);
        void UpdateMain(MainDbModel main);
        IQueryable<MainDbModel> GetAll();
        ObservableCollection<MainDbModel> GetObservableCollectionMainDbModel();
    }
}
