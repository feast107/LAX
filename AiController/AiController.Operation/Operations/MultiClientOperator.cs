using AiController.Abstraction;
using AiController.Abstraction.Conversion;
using AiController.Abstraction.Operation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AiController.Conversion.Converters
{
    internal class MultiClientOperator 
        : IEventOperator<Tuple<string, string>?>
    {
        public required List<string> ClientNames { get; init; }

        public object Context { get => $"""
            我现在有 [{ClientNames.Aggregate(new StringBuilder(), (s, c) => s.Append(',' + c)).Remove(0, 1)}] {ClientNames.Count}个设备。
            在接下来的回复中，请显式标注我所指代的设备，先声明设备名称，接着用符号"|"分割，再接着衔接回复的内容。
            """; set => context = value; }
        public IEventOperator<Tuple<string, string>?>.OperationHandler OnReceiveOperation { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        private object? context;

        public string ToMessage(object ask)
        {
            return $"""
                {Context}
                {ask}
                """;
        }

        public Tuple<string, string>? ToOperation(string reply)
        {
            var res = reply.Split('|');
            var name = res[0].Trim();
            if (name.Length > 0 && ClientNames.Contains(name))
            {
                return new (name, res[1]);
            }
            else
            {
                return null;
            }
        }

        public void Send(string ask)
        {
            throw new NotImplementedException();
        }
    }
}
