using Mechatronica.DB;
using Mechatronica.DB.Interfaces;
using Mechatronica.WPF.Helpers;
using Mechatronica.WPF.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Xunit.Abstractions;

namespace Mechatronica.Tests
{
    public class DbTest
    {

        private readonly ITestOutputHelper _testOutputHelper;

        public DbTest(ITestOutputHelper testOutputHelper)
        {

            _testOutputHelper = testOutputHelper;

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
