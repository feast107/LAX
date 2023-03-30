using AiController.Abstraction.Operation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AiController.Operation.Operators
{
    internal class MultiClientOperator
        : IEventOperator<Tuple<string, string>?>
    {
        public required List<string> ClientNames { get; init; }

        public string Context { get => $"""
            我现在有 [{ClientNames.Aggregate(new StringBuilder(), (s, c) => s.Append(',' + c)).Remove(0, 1)}] {ClientNames.Count}个设备。
            在接下来的回复中，请显式标注我所指代的设备，先声明设备名称，接着用符号"|"分割，再接着衔接回复的内容。
            """; set => context = value; }
        public string Description
        {
            get => description ??= $"This is the server named {Identifier}";
            set => description = value;
        }
        private string? description;
        public required string Identifier { get; init; }
        public IEventOperator<Tuple<string, string>?>.OperationHandler? OnReceiveOperation { get; set; }

        private string? context;

        public void Send(string ask)
        {
            throw new NotImplementedException();
        }
    }
}
