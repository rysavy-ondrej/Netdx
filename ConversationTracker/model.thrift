namespace netcore Netdx.ConversationTracker
namespace csharp Netdx.ConversationTracker

struct FlowKey {
    1: i32 Protocol;
    2: binary SourcePoint;  
    3: binary DestinationPoint;
}

// Conversation object. 
struct Conversation {
    1: FlowKey ConversationKey;
    2: i32 ConversationId;
    3: i32 ParentId;
    4: i64 FirstSeen = 1;
    5: i64 LastSeen = 2;
} 