using System;
using System.Collections.Generic;
using System.Text;

namespace AiController.Abstraction.Operation
{
    public interface IProxied<TProxy>
    {
        TProxy Proxy { get; set; }
    }
}
