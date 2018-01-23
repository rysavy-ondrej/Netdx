using System;
namespace Netdx.ConversationTracker
{

    /// <summary>
    /// Defines interface for key providers. The key provider returns 
    /// <see cref="FlowKey"/> for the packet.  
    /// </summary>
    public interface IKeyProvider<TPacket>
    {
        FlowKey GetKey(TPacket packet);        
    }
}
