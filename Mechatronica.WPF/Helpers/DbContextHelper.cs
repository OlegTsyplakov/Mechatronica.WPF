using Mechatronica.DB;
using Microsoft.Extensions.DependencyInjection;

namespace Mechatronica.WPF.Helpers
{
    public class DbContextHelper
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        private DbContextHelper(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;

        }
        
    
        public AppDbContext GetDbContext()
        {
            var _scope = _serviceScopeFactory.CreateScope();

            return _scope.ServiceProvider.GetRequiredService<AppDbContext>();
        }
    }
}
