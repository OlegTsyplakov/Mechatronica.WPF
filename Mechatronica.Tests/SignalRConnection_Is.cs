using Mechatronica.WPF.Interfaces;
using Xunit.Abstractions;


namespace Mechatronica.Tests
{
    public class SignalRConnection_Is 
    {
        private readonly ISignalRConnection _connection;
        private readonly ITestOutputHelper _testOutputHelper;
        public SignalRConnection_Is(ISignalRConnection connection, ITestOutputHelper testOutputHelper)
        {
        
            _testOutputHelper = testOutputHelper;
            _connection = connection;
        }
        [Fact]
        public async Task SignalRConnection_Is_ConnectedAsync()
        {
            // arrange 

            // act

            var exception = await Record.ExceptionAsync(async () =>  await _connection.Start());
  
            bool condition = exception is null;
            string param = condition ? "" : "не";
            _testOutputHelper.WriteLine(String.Format("Соединение {0} установлено.", param));
    
            // assert
            Assert.True(condition);

        }
        [Theory]
        [InlineData("Привет")]
        public async Task SignalRConnection_Is_SendAsync(string messageToSend)
        {

            // arrange 

            // act
            var exception = await Record.ExceptionAsync(async () => await _connection.Send(messageToSend));

            bool condition = exception is null;
            string param = condition ? "" : "не";
            _testOutputHelper.WriteLine(String.Format("Соединение {0} установлено.", param));
            // assert
            Assert.True(condition);

        }

    }
}
