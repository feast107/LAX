﻿using AiController.Abstraction.Conversion;
using System;
using System.CommandLine;
using System.Drawing;

namespace AiController.Conversion.Converters
{
    public class MouseOperationCommandConverter :
        IOperationConverter<Action<PointF>>,
        ICommandContext
    {
        public string ToMessage(object ask)
        {
            return ask.ToString();
        }

        public Action<PointF> ToOperation(string reply)
        {
            throw new NotImplementedException();
        }

        public object? Context { get; }
    }
}
