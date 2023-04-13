using Mechatronica.DB.Models;
using Mechatronica.WPF.Interfaces;
using Mechatronica.WPF.Models;
using System.Collections.Generic;


namespace Mechatronica.WPF.Helpers
{
    public class DbHelper
    {
        public static CarDbModel MapToCarDbModel(CarModel carModel)
        {
            CarDbModel carDbModel = new()
            {
                CarName = carModel.Name,
                Date = carModel.Date
            
            };
            return carDbModel;
        }
        public static PersonDbModel MapToPersonDbModel(PersonModel personModel)
        {
            PersonDbModel personDbModel = new()
            {
                Name = personModel.Name,
                Date = personModel.Date

            };
            return personDbModel;
        }
        public static MainDbModel MapToMainDbModel(MainModel mainModel)
        {
            MainDbModel mainDbModel = new()
            {
                Car = mainModel.Car,
                Person = mainModel.Person,
                Date = mainModel.Date

            };
            return mainDbModel;
        }

        public static MainModel MapToMainModel(KeyValuePair<string, IModel> item, string? previousName)
        {
            MainModel mainModel = new()
            {
                Date = item.Key,
            };
            if (item.Value.GetType().Name != nameof(CarModel))
            {
                mainModel.Person = item.Value.Name;
                mainModel.Car = previousName;
            }
            else
            {
                mainModel.Car = item.Value.Name;
                mainModel.Person = previousName;
            }
            return mainModel;
        }

    }
}
