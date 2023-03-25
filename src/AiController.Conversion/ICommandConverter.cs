using System;

namespace AiController.Conversion
{
    public interface ICommandConverter : ICommandConverter<object> { }

    public interface ICommandConverter<out TOperation>
    {
        string ToMessage(string ask);

        TOperation ToOperation(string reply);
    }
}
