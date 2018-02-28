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
  public partial class Conversation : TBase
  {
    private FlowKey _ConversationKey;
    private int _ConversationId;
    private int _ParentId;
    private long _FirstSeen;
    private long _LastSeen;

    public FlowKey ConversationKey
    {
      get
      {
        return _ConversationKey;
      }
      set
      {
        __isset.ConversationKey = true;
        this._ConversationKey = value;
      }
    }

    public int ConversationId
    {
      get
      {
        return _ConversationId;
      }
      set
      {
        __isset.ConversationId = true;
        this._ConversationId = value;
      }
    }

    public int ParentId
    {
      get
      {
        return _ParentId;
      }
      set
      {
        __isset.ParentId = true;
        this._ParentId = value;
      }
    }

    public long FirstSeen
    {
      get
      {
        return _FirstSeen;
      }
      set
      {
        __isset.FirstSeen = true;
        this._FirstSeen = value;
      }
    }

    public long LastSeen
    {
      get
      {
        return _LastSeen;
      }
      set
      {
        __isset.LastSeen = true;
        this._LastSeen = value;
      }
    }


    public Isset __isset;
    #if !SILVERLIGHT
    [Serializable]
    #endif
    public struct Isset {
      public bool ConversationKey;
      public bool ConversationId;
      public bool ParentId;
      public bool FirstSeen;
      public bool LastSeen;
    }

    public Conversation() {
      this._FirstSeen = 1;
      this.__isset.FirstSeen = true;
      this._LastSeen = 2;
      this.__isset.LastSeen = true;
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
              if (field.Type == TType.Struct) {
                ConversationKey = new FlowKey();
                ConversationKey.Read(iprot);
              } else { 
                TProtocolUtil.Skip(iprot, field.Type);
              }
              break;
            case 2:
              if (field.Type == TType.I32) {
                ConversationId = iprot.ReadI32();
              } else { 
                TProtocolUtil.Skip(iprot, field.Type);
              }
              break;
            case 3:
              if (field.Type == TType.I32) {
                ParentId = iprot.ReadI32();
              } else { 
                TProtocolUtil.Skip(iprot, field.Type);
              }
              break;
            case 4:
              if (field.Type == TType.I64) {
                FirstSeen = iprot.ReadI64();
              } else { 
                TProtocolUtil.Skip(iprot, field.Type);
              }
              break;
            case 5:
              if (field.Type == TType.I64) {
                LastSeen = iprot.ReadI64();
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
        TStruct struc = new TStruct("Conversation");
        oprot.WriteStructBegin(struc);
        TField field = new TField();
        if (ConversationKey != null && __isset.ConversationKey) {
          field.Name = "ConversationKey";
          field.Type = TType.Struct;
          field.ID = 1;
          oprot.WriteFieldBegin(field);
          ConversationKey.Write(oprot);
          oprot.WriteFieldEnd();
        }
        if (__isset.ConversationId) {
          field.Name = "ConversationId";
          field.Type = TType.I32;
          field.ID = 2;
          oprot.WriteFieldBegin(field);
          oprot.WriteI32(ConversationId);
          oprot.WriteFieldEnd();
        }
        if (__isset.ParentId) {
          field.Name = "ParentId";
          field.Type = TType.I32;
          field.ID = 3;
          oprot.WriteFieldBegin(field);
          oprot.WriteI32(ParentId);
          oprot.WriteFieldEnd();
        }
        if (__isset.FirstSeen) {
          field.Name = "FirstSeen";
          field.Type = TType.I64;
          field.ID = 4;
          oprot.WriteFieldBegin(field);
          oprot.WriteI64(FirstSeen);
          oprot.WriteFieldEnd();
        }
        if (__isset.LastSeen) {
          field.Name = "LastSeen";
          field.Type = TType.I64;
          field.ID = 5;
          oprot.WriteFieldBegin(field);
          oprot.WriteI64(LastSeen);
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
      StringBuilder __sb = new StringBuilder("Conversation(");
      bool __first = true;
      if (ConversationKey != null && __isset.ConversationKey) {
        if(!__first) { __sb.Append(", "); }
        __first = false;
        __sb.Append("ConversationKey: ");
        __sb.Append(ConversationKey== null ? "<null>" : ConversationKey.ToString());
      }
      if (__isset.ConversationId) {
        if(!__first) { __sb.Append(", "); }
        __first = false;
        __sb.Append("ConversationId: ");
        __sb.Append(ConversationId);
      }
      if (__isset.ParentId) {
        if(!__first) { __sb.Append(", "); }
        __first = false;
        __sb.Append("ParentId: ");
        __sb.Append(ParentId);
      }
      if (__isset.FirstSeen) {
        if(!__first) { __sb.Append(", "); }
        __first = false;
        __sb.Append("FirstSeen: ");
        __sb.Append(FirstSeen);
      }
      if (__isset.LastSeen) {
        if(!__first) { __sb.Append(", "); }
        __first = false;
        __sb.Append("LastSeen: ");
        __sb.Append(LastSeen);
      }
      __sb.Append(")");
      return __sb.ToString();
    }

  }

}
