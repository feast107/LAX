using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AiController.Abstraction.Operation
{
    internal interface IAsyncOperator<TOperation> : IDescriptor
    {
        Task<TOperation> SendAsync(object ask);
    }
}
