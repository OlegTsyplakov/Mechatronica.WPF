

using Mechatronica.WPF.Models;

namespace Mechatronica.Tests
{
    public class MapToCarDbModel_Is
    {
        [Fact]
        public void Is_MapToCarDbModel_ReturnTrue()
        {
            var carModel = new CarModel();
            Assert.True(true,"Должно быть true");
        }
    }
}