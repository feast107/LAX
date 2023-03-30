using AiController.Abstraction.Conversion;
using System;
using System.CommandLine;
using System.Drawing;

namespace AiController.Conversion.Converters
{
    public class MouseOperationCommandConverter :
        IOperator<Action<PointF>>,
        ISubject
    {
        public string ToMessage(object ask)
        {
            return ask.ToString();
        }

        public Action<PointF> ToOperation(string reply)
        {
            throw new NotImplementedException();
        }

        public string ToMessage(object subject, object ask)
        {
            throw new NotImplementedException();
        }

        public object? Context { get; }
        object ISubject.Context { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
