using AiController.Abstraction.Conversion;
using System;

namespace AiController.Conversion.Converters.Base
{
    public abstract class Gpt35BasedOperator<TOperation> : IOperator<TOperation>
    {
        public object Context { get; set; }

        public string ToMessage(object ask)
        {
            throw new NotImplementedException();
        }

        public string ToMessage(object subject, object ask)
        {
            throw new NotImplementedException();
        }

        public abstract TOperation ToOperation(string reply);
    }
}
