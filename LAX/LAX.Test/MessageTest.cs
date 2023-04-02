using LAX.Abstraction.Communication;
using LAX.Communication.GPT35;
using LAX.Operation.Operators;
using LAX.Operation.Operators.Direct;
using LAX.Operation.Operators.Indirect;
using LAX.Transmission.Json;
using OpenAI.Chat;

namespace LAX.Test
{
    public class Tests
    {
        private IAsyncCommunicator<ChatPrompt[]> Communicator { get; set; } = null!;
        private IExtensibleAsyncOperator<DistributeMessageListModel?> Server { get; set; } = null!;
        private Gpt35ClientOperator<DistributeMessageListModel?> Client { get; set; } = null!;

        [SetUp]
        public void Setup()
        {
            Communicator = new Gpt35AsyncCommunicator()
            {
               Temperature = 0,
               ModelName = "gpt-3.5-turbo"
            };
            Server = new Gpt35DistributeAsyncOperator<DistributeMessageListModel>(Communicator)
            {
            };
            Server.Add(Client = new Gpt35ClientOperator<DistributeMessageListModel?>()
            {
                Identifier = "Client1",
                Description = "This is my client"
            });
            Server.Add(new Gpt35ClientOperator<DistributeMessageListModel?>()
            {
                Identifier = "Unit unit",
                Description = "This is unit"
            });
            Client.Proxy = Server;
        }

        [Test]
        public async Task Test1()
        {
            var clientMessage = "�����ǰ���еĿͻ��˷��;�����Ϣ��'Error , SOS'";
            try
            {
                var res = await Client.SendAsync(clientMessage);
                Assert.NotNull(res);
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
            Assert.Pass();
        }
    }
}