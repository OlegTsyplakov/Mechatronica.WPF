using Mechatronica.DB;
using Mechatronica.DB.Interfaces;
using Mechatronica.WPF.Helpers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

    }
}
