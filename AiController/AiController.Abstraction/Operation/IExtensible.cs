using System;
using System.Collections.Generic;
using System.Text;

namespace AiController.Abstraction.Operation
{
    public interface IExtensible
    {
        void Add(IDescriptor descriptor);

        void Remove(IDescriptor descriptor);
    }
}
