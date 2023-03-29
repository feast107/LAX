using AiController.Abstraction.Communication;
using AiController.Communication.GPT35;

namespace AiController.Test
{
    public class Tests
    {
        private IAsyncCommunicator Communicator { get; set; }
        [SetUp()]
        public void Setup()
        {
            Communicator = new Gpt35AsyncCommunicator()
            {
                ApiKey = "",
                ApiHost = "openaiapi.elecho.top",
                ModelName = "gpt-3.5-turbo",
                Temperature = 0
            };
        }

        [Test]
        public async Task Test1()
        {
            try
            {
                var res = await Communicator.SendAsync("" +
                    "针对这段要求的回复，请转换成简短的命令行：在 d:MyDir 目录下创建一个名为Access的目录");
                Assert.IsNotNull(res);
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
            Assert.Pass();
        }
    }
}