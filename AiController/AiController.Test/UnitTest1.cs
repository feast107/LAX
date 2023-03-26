using AiController.Communication.GPT3;

namespace AiController.Test
{
    public class Tests
    {
        private IStreamCommunicator Communicator { get; set; }
        [SetUp]
        public void Setup()
        {
            Communicator = new Gpt3Communicator(
                "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",
                "openaiapi.elecho.top",
                "gpt-3.5-turbo",
                0
            );
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