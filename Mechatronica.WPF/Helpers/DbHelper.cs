using Mechatronica.DB.Models;
using Mechatronica.WPF.Interfaces;
using Mechatronica.WPF.Models;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mechatronica.WPF.Helpers
{
    public class DbHelper
    {
        public static CarDbModel MapToCarDbModel(CarModel carModel)
        {
            CarDbModel carDbModel = new CarDbModel()
            {
                CarName = carModel.Name,
                Date = carModel.Date
            
            };
            return carDbModel;
        }
        public static PersonDbModel MapToPersonDbModel(PersonModel personModel)
        {
            PersonDbModel personDbModel = new PersonDbModel()
            {
                Name = personModel.Name,
                Date = personModel.Date

            };
            return personDbModel;
        }
    }
}
