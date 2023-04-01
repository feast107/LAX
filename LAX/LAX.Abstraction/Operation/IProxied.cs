namespace LAX.Abstraction.Operation
{
    public interface IProxied<TProxy>
    {
        TProxy Proxy { get; set; }
    }
}
