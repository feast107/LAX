using AiController.Abstraction.Conversion;
using System;
using System.Drawing;

namespace AiController.Conversion.Converters
{
    public class MouseCommandConverter : ICommandConverter<Action<PointF>>
    {
        public string ToMessage(string ask)
        {
            return ask;
        }

        public Action<PointF> ToOperation(string reply)
        {
            throw new NotImplementedException();
        }
    }
}
