using System;
using System.Collections.Generic;
using System.Text;

namespace AiController.Abstraction.Operation
{
    internal interface IEventOperator<TOperation>
    {
        void Send(string ask);

        delegate void OperationHandler(TOperation operation);
        OperationHandler OnReceiveOperation { get; set; } 
    }
}
