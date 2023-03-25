using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq.Expressions;
using System.Text;

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
