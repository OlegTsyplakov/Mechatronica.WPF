using Mechatronica.WPF.ViewModels;
using Xunit.Abstractions;
using System.Timers;
using Mechatronica.WPF.Models;
using Mechatronica.DB.Interfaces;
using Moq;
using Mechatronica.WPF.Interfaces;
using Mechatronica.DB.Models;
using System.Collections.ObjectModel;

namespace Mechatronica.Tests
{
    [Collection("Non-Parallel Collection")]
    [CollectionDefinition("Non-Parallel Collection", DisableParallelization = true)]
    public class MainViewModelTest
    {

        private readonly ITestOutputHelper _testOutputHelper;

        private readonly MainViewModel _mainViewModel;
      
        private readonly Mock<IRepository> _mockRepository = new();
        private readonly Mock<ISignalRConnection> _mockSignalRConnection = new();
        public MainViewModelTest(ITestOutputHelper testOutputHelper)
        {

            _mainViewModel= new(_mockRepository.Object,_mockSignalRConnection.Object);
            _testOutputHelper = testOutputHelper;
 
           
  
        }

        [Theory]
        [InlineData(0, 4)]
        [InlineData(2, 4)]
        [InlineData(3, 6)]
  
        public void MainViewModel_Is_CarsCountAsync(int expected, int interval)
        {
            // arrange

         int _count = 0;
        int actual = -1;
            IQueryable<MainDbModel> mainDbModels = Enumerable.Empty<MainDbModel>().AsQueryable();
            _mockRepository.Setup(x => x.GetAll()).Returns(mainDbModels);
            ObservableCollection<MainDbModel> DbData = new ObservableCollection<MainDbModel>(); 
            _mockRepository.Setup(x => x.GetObservableCollectionMainDbModel()).Returns(DbData);
            CustomTimer.Subscribe(TimerElapsed);

            // act
            while (_count <= interval)
            {

                if (_count == interval)
                {
                    _testOutputHelper.WriteLine(String.Format("count1 {0}", _count));
                    actual = _mainViewModel.Cars.Count;

                    _count = 0;
                    break;
                }

            }
            // assert
            _testOutputHelper.WriteLine(String.Format("expected {0},  actual {1}", expected, actual));
            Assert.Equal(expected, actual);
            CustomTimer.UnSubscribe(TimerElapsed);
            void TimerElapsed(object? sender, ElapsedEventArgs args)
            {
                _count++;
            }
        }
      

    }
}
