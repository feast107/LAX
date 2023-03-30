using System;
using System.Collections.Generic;
using System.Text;

namespace AiController.Abstraction.Operation
{
    public interface IExtensibleSemanteme
    {
        void Add(IDescriptor descriptor);

        void Remove(IDescriptor descriptor);
    }
}
