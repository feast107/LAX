namespace AiController.Abstraction.Operation
{
    public interface IExtensible
    {
        void Add(IDescriptor descriptor);

        void Remove(IDescriptor descriptor);
    }
}
