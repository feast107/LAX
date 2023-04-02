using LAX.Abstraction;

namespace LAX.Transmission.Json;

public class DistributeMessageListModel : IExceptional
{
    [Description("""
                 Please fill in your reply in '[this]'. This is a list type.
                 Determine how many clients you need to reply to from the context of the client request.
                  and for each of these objects, you need to follow the following as :
                 [then]
                 """, typeof(DistributeMessageModel))]
    public List<DistributeMessageModel>? ReplyMessages { get; set; }

    public Exception? Exception { get; set; }
}