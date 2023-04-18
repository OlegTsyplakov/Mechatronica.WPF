using Mechatronica.WPF.ViewModels;
using Xunit.Abstractions;
using System.Timers;
using Mechatronica.WPF.Models;
using Mechatronica.DB.Interfaces;
using Moq;
using Mechatronica.WPF.Interfaces;
using Mechatronica.DB.Models;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace Mechatronica.Tests
{
    //[Collection("Non-Parallel Collection")]
    //[CollectionDefinition("Non-Parallel Collection", DisableParallelization = true)]
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
            IQueryable<MainDbModel> mainDbModels = Enumerable.Empty<MainDbModel>().AsQueryable();
            _mockRepository.Setup(x => x.GetAll()).Returns(mainDbModels);
            ObservableCollection<MainDbModel> DbData = new ObservableCollection<MainDbModel>();
            _mockRepository.Setup(x => x.GetObservableCollectionMainDbModel()).Returns(DbData);


        }

        [Theory]
        [InlineData(2, 4)]


        public async Task MainViewModel_Is_CarsCountMatchAsync(int expected, int interval)
        {
            // arrange
            int actual = -1;
            int start = CustomTimer.Ticks;
            // act
            await Task.Delay(interval * 1000);
            _testOutputHelper.WriteLine(String.Format("start tick: {0}", start));
            _testOutputHelper.WriteLine(String.Format("end tick: {0}", CustomTimer.Ticks));
            _testOutputHelper.WriteLine(String.Format("duration tick: {0}", CustomTimer.Ticks - start));
            actual = _mainViewModel.Cars.Count;
            // assert
            _testOutputHelper.WriteLine(String.Format("expected {0},  actual {1}", expected, actual));
            Assert.Equal(expected, actual);


        }
        [Theory]
        [InlineData(1, 6)]

        public async Task MainViewModel_Is_MainModelsCountMatchAsync(int expected, int interval)
        {
            // arrange
            int actual = -1;
            // act
            await Task.Delay(interval*1000);
                    _testOutputHelper.WriteLine(String.Format("ticks: {0}", CustomTimer.Ticks));
                    actual = _mainViewModel.MainModels.Count;
            // assert
            _testOutputHelper.WriteLine(String.Format("expected {0},  actual {1}", expected, actual));
            Assert.Equal(expected, actual);
        }
        [Theory]
        [InlineData(0, 4)]
        [InlineData(1, 3)]
        [InlineData(2, 6)]

        public async Task MainViewModel_Is_PersonsCountMatchAsync(int expected, int interval)
        {
            // arrange
            int actual = -1;
            // act
            await Task.Delay(interval * 1000);
            _testOutputHelper.WriteLine(String.Format("ticks: {0}", CustomTimer.Ticks));
            actual = _mainViewModel.Persons.Count;
            // assert
            _testOutputHelper.WriteLine(String.Format("expected {0},  actual {1}", expected, actual));
            Assert.Equal(expected, actual);
        }
    }
}
