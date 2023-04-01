using System;

namespace LAX.Abstraction
{
    public interface IExceptional
    {
#nullable enable
        public Exception? Exception { get; set; }
    }
}
