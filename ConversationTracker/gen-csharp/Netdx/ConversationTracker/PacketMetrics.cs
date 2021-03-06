/**
 * Autogenerated by Thrift Compiler (0.9.3)
 *
 * DO NOT EDIT UNLESS YOU ARE SURE THAT YOU KNOW WHAT YOU ARE DOING
 *  @generated
 */
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Thrift;
using Thrift.Collections;
using System.Runtime.Serialization;
using Thrift.Protocol;
using Thrift.Transport;

namespace Netdx.ConversationTracker
{

  #if !SILVERLIGHT
  [Serializable]
  #endif
  public partial class PacketMetrics : TBase
  {
    private long _Timeval;
    private int _SegmentSize;

    public long Timeval
    {
      get
      {
        return _Timeval;
      }
      set
      {
        __isset.Timeval = true;
        this._Timeval = value;
      }
    }

    public int SegmentSize
    {
      get
      {
        return _SegmentSize;
      }
      set
      {
        __isset.SegmentSize = true;
        this._SegmentSize = value;
      }
    }


    public Isset __isset;
    #if !SILVERLIGHT
    [Serializable]
    #endif
    public struct Isset {
      public bool Timeval;
      public bool SegmentSize;
    }

    public PacketMetrics() {
    }

    public void Read (TProtocol iprot)
    {
      iprot.IncrementRecursionDepth();
      try
      {
        TField field;
        iprot.ReadStructBegin();
        while (true)
        {
          field = iprot.ReadFieldBegin();
          if (field.Type == TType.Stop) { 
            break;
          }
          switch (field.ID)
          {
            case 1:
              if (field.Type == TType.I64) {
                Timeval = iprot.ReadI64();
              } else { 
                TProtocolUtil.Skip(iprot, field.Type);
              }
              break;
            case 2:
              if (field.Type == TType.I32) {
                SegmentSize = iprot.ReadI32();
              } else { 
                TProtocolUtil.Skip(iprot, field.Type);
              }
              break;
            default: 
              TProtocolUtil.Skip(iprot, field.Type);
              break;
          }
          iprot.ReadFieldEnd();
        }
        iprot.ReadStructEnd();
      }
      finally
      {
        iprot.DecrementRecursionDepth();
      }
    }

    public void Write(TProtocol oprot) {
      oprot.IncrementRecursionDepth();
      try
      {
        TStruct struc = new TStruct("PacketMetrics");
        oprot.WriteStructBegin(struc);
        TField field = new TField();
        if (__isset.Timeval) {
          field.Name = "Timeval";
          field.Type = TType.I64;
          field.ID = 1;
          oprot.WriteFieldBegin(field);
          oprot.WriteI64(Timeval);
          oprot.WriteFieldEnd();
        }
        if (__isset.SegmentSize) {
          field.Name = "SegmentSize";
          field.Type = TType.I32;
          field.ID = 2;
          oprot.WriteFieldBegin(field);
          oprot.WriteI32(SegmentSize);
          oprot.WriteFieldEnd();
        }
        oprot.WriteFieldStop();
        oprot.WriteStructEnd();
      }
      finally
      {
        oprot.DecrementRecursionDepth();
      }
    }

    public override string ToString() {
      StringBuilder __sb = new StringBuilder("PacketMetrics(");
      bool __first = true;
      if (__isset.Timeval) {
        if(!__first) { __sb.Append(", "); }
        __first = false;
        __sb.Append("Timeval: ");
        __sb.Append(Timeval);
      }
      if (__isset.SegmentSize) {
        if(!__first) { __sb.Append(", "); }
        __first = false;
        __sb.Append("SegmentSize: ");
        __sb.Append(SegmentSize);
      }
      __sb.Append(")");
      return __sb.ToString();
    }

  }

}
