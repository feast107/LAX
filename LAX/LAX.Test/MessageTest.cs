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
        private IAsyncCommunicator<ChatPrompt[]> Communicator { get; set; }
        private IExtensibleAsyncOperator<DistributeMessageListModel?> Server { get; set; }
        private Gpt35ClientOperator<DistributeMessageListModel?> Client { get; set; }

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
                Identifier = "You are center server : [Server]",
            };
            Server.Add(Client = new Gpt35ClientOperator<DistributeMessageListModel?>()
            {
                Identifier = "Client1",
                Description = "This is my client"
            });
            Server.Add(new Gpt35ClientOperator<DistributeMessageListModel?>()
            {
                Identifier = "Central unit",
                Description = "This is Central unit"
            });
            Client.Proxy = Server;
        }

        [Test]
        public async Task Test1()
        {
            var clientMessage = "�����ǰ���е��豸���;�����Ϣ��'Error , SOS'";
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