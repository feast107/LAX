using AiController.Abstraction;
using AiController.Abstraction.Operation;
using System;
using System.CommandLine;
using System.Drawing;

namespace AiController.Operation.Operators
{
    public abstract class MouseOperationCommandConverter :
        IOperator<Action<PointF>>
    {
        public abstract object Description { get; set; }
        public abstract object Identifier { get; init; }
        public abstract Action<PointF> Send(object ask);
    }
}
