namespace LAX.Abstraction.Communication;

public interface ICommunicator : ICommunicator<string> { }
public interface ICommunicator<TMessage>
{
    string Send(TMessage message);
}