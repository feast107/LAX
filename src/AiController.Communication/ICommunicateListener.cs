using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AiController.Communication
{
    internal interface ICommunicateListener
    {
        void Send(string message);

        delegate void MessageHandler(string message);

        event MessageHandler OnReceiveMessage;
    }
}
