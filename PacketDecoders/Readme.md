# Ndx.Packets

The project provides an implementation of commonly used application protocol parsers. Binary protocol parsers are generated
from Kaitai specification. 

## Usage
The following code shows parsing SNMP packet from the raw frame data with the help of PacketDotNet. 

```csharp
var bytes = ...RAW FRAME BYTES...
var packet = Packet.ParsePacket(LinkLayers.Ethernet, bytes);
var app = packet.Extract(typeof(PacketDotNet.ApplicationPacket)) as PacketDotNet.ApplicationPacket;
var snmp = new Snmp(new KaitaiStream(app.Bytes));
```



## Compile Decoders
The Kaitai Struct compiler needs to be installed on the system in order to recompile parsers:

* To recompile DLMS parsers:
```
Kaitai/dlms# kaitai-struct-compiler -t csharp --dotnet-namespace Netdx.Packets.Industrial -d ../../Industrial/dlms *.ksy
```


* To recompile common interne protocols
```
Kaitai/common# kaitai-struct-compiler -t csharp --dotnet-namespace Netdx.Packets.Common -d ../../Common *.ksy
```