using Mechatronica.DB.Models;
using System.Collections.ObjectModel;

namespace Mechatronica.DB.Interfaces
{
    public interface IRepository
    {
        void AddCar(CarDbModel car);
        IQueryable<CarDbModel> GetAllCars();
        void AddPerson(PersonDbModel person);
        IQueryable<PersonDbModel> GetAllPersons();

        void AddMain(MainDbModel main);

        void UpdateMain(MainDbModel main);
        IQueryable<MainDbModel> GetAll();
        ObservableCollection<MainDbModel> GetObservableCollectionMainDbModel();
        void Dummy();
    }
}
