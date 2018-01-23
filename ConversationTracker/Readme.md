# Conversation Tracker


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
It produces files in ```gen-csharp\Netdx\COnversationTracker```.

