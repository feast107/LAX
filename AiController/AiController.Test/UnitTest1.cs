using AiController.Communication.GPT35;

namespace AiController.Test
{
    public class Tests
    {
        private IStreamCommunicator Communicator { get; set; }
        [SetUp]
        public void Setup()
        {
            Communicator = new Gpt35Communicator
            {
                ApiKey = "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",
                ApiHost = "openaiapi.elecho.top",
                ModelName = "gpt-3.5-turbo",
                Temperature = 0
            };
        }

        [Test]
        public async Task Test1()
        {
            await Communicator.Send("ÄãÔÚÂð", (s) =>
            {
                Console.Write(s);
            });
            Assert.Pass();
        }
    }
}