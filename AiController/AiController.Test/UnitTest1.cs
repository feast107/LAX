using AiController.Communication.GPT35;

namespace AiController.Test
{
    public class Tests
    {
        private IAsyncCommunicator Communicator { get; set; }
        [SetUp]
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
                var res = await Communicator.SendAsync("你能做到什么");
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