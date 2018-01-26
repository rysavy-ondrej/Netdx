namespace Netdx.ConversationTracker
{
    public interface IRecordProvider<TPacket, TFlowRecord>
    {
        TFlowRecord GetRecord(TPacket packet);
    }
}