using System.CommandLine;

namespace LAX.Test
{
    internal class CommandTest
    {
        [Test]
        public void Test()
        {
            Command command = new Command("app -myoption 123");
            Assert.Pass(command.Arguments[0].ToString());
        }
    }
}
