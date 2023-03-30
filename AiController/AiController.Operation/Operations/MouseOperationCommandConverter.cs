using AiController.Abstraction;
using AiController.Abstraction.Conversion;
using System;
using System.CommandLine;
using System.Drawing;

namespace AiController.Conversion.Converters
{
    public abstract class MouseOperationCommandConverter :
        IOperator<Action<PointF>>
    {
        public abstract object Description { get; set; }
        public abstract object Subject { get; init; }
        public abstract Action<PointF> Send(object ask);
    }
}
