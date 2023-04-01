using System;
using System.Collections.Generic;
using System.CommandLine;
using System.CommandLine.Builder;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AiController.Test
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
