# Conversation Tracker


Conversation tracker enables:

* Identifying bidirectional flows

* Grouping packets in flows

* 

To identify bi-directional flows, each packet is analyzed. FlowKey is extracted and it is used as a key 
in conversation table. Conversation table contains conversation items. 



## Building 



### Building model files
To build model files from IDL definition, thrift tool is required.
* Windows: 
```
choco install thrift
```
* MacOS:
``` 
brew install boost

brew install libevent

brew install thrift
```

To regenerate model files, use thrift tool:
```
thrift --gen csharp model.thrift 
```
It produces files in ```gen-csharp\Netdx\ConversationTracker```.

