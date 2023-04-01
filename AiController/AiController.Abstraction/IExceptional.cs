using System;
using System.Collections.Generic;
using System.Text;

namespace AiController.Abstraction
{
    public interface IExceptional
    {
#nullable enable
        public Exception? Exception { get; set; }
    }
}
