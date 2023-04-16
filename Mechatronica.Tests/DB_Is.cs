using Mechatronica.DB;
using Mechatronica.DB.Interfaces;
using Mechatronica.DB.Models;
using Mechatronica.WPF.Helpers;
using Mechatronica.WPF.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Xunit.Abstractions;

namespace Mechatronica.Tests
{
    public class DB_Is
    {
        private readonly IRepository _repository;
        private readonly ITestOutputHelper _testOutputHelper;
        private readonly IHost _Apphost;
        private readonly AppDbContext _appDbContext;
        public DB_Is(IRepository repository, ITestOutputHelper testOutputHelper, IHost Apphost)
        {
            _repository = repository;
            _testOutputHelper = testOutputHelper;
            _Apphost = Apphost;
            _appDbContext = _Apphost.Services.GetService<AppDbContext>();
        }


        [Fact]
        public void DB_Is_Connected()
        {
            // arrange 

            // act
           bool condition = _appDbContext.Database.CanConnect();
            string param = condition ? "" : "не";
            _testOutputHelper.WriteLine(String.Format("Соединение {0} установлено.", param));

            // assert
            Assert.True(condition);

        }
        [Theory]
        [ClassData(typeof(CarModelTest))]
        public void DB_Is_MapToCarDbModel(CarModel carModel)
        {
            // arrange 
            // act
            var exception = Record.Exception(() => DbHelper.MapToCarDbModel(carModel));
            bool condition = exception is null;
            string param = condition ? "" : "не";
            _testOutputHelper.WriteLine(String.Format("Соответствие {0} установлено.", param));

            // assert
            Assert.True(condition);

        }

        public class CarModelTest : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] {
                new CarModel
                {
                  Name="CarTest",
                  Date=DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") ,

                }
            };
            }
            System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();
        }

    }
}
