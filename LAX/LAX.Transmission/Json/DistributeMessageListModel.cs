using LAX.Abstraction;

namespace LAX.Transmission.Json;

public class DistributeMessageListModel : IExceptional
{
    [Description("""
                 You will fill in your reply in '[this]'. This is a list type.
                 You can be smart about which clients to reply to from the context of the client request.
                 and for each of these objects, you will follow the following as :
                 [then]
                 """, typeof(DistributeMessageModel))]
    public List<DistributeMessageModel>? ReplyMessages { get; set; }

    public Exception? Exception { get; set; }
}