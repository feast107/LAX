using AiController.Abstraction.Communication;
using AiController.Abstraction.Operation;
using AiController.Communication.GPT35;
using AiController.Operation.Operators.Direct;
using AiController.Transmission.SignalR;
using OpenAI.Chat;

namespace AiController.Test
{
    public class Tests
    {
        private IAsyncCommunicator<ChatPrompt[]> Communicator { get; set; }
        private IAsyncOperator<DistributeMessageModel?> Operator { get; set; }
        [SetUp]
        public void Setup()
        {
            Communicator = new Gpt35AsyncCommunicator()
            {
                
            };
            Operator = new Gpt35DistributeAsyncOperator<DistributeMessageModel>(Communicator)
            {
                Identifier = "这是中心服务器:[Server]",
                Description = """
                现在有 [comp1,comp2] 2台设备。在接下来的客户端发起的请求中，请仅使用JSON格式进行回复，比如：
                { "device" :"comp1" , "reply":"mkdir d:" }，
                请显式标注下文所指代的设备并填入到JSON的device中,接着将回复客户端的内容按照客户端的要求填入到reply中，
                无论如何，请不要回复除了JSON文本以外的内容
                """
            };
        }

        [Test]
        public async Task Test1()
        {
            var groupContext = """
                现在有 [comp1,comp2] 2台设备。在接下来的回复中，请仅使用JSON格式进行回复，比如 { "device" :"comp1" , "reply":"mkdir d:" }
                请显式标注下文所指代的设备并填入到JSON的device中,接着将回复的字段按照要求填入到reply中，无论如何，请不要回复除了JSON文本以外的内容
                """;
            var groupMessage = "接下来这则消息来自客户端 comp1 ：\n";
            var clientMessage = "针对这段要求的回复，请转换成简短的命令行：\"在另一台设备中的 d:MyDir 目录下创建一个名为Access的目录\" ";
            try
            {
                var res = await Operator.SendAsync(
                    groupMessage +
                    clientMessage);
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