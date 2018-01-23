using System;
using System.Collections.Generic;
using System.Text;
using Thrift.Protocol;

namespace Netdx.ConversationTracker
{
    class Utils
    {
        public static bool Equals(byte[] lhs, byte[] rhs)
        {
            if (ReferenceEquals(lhs, rhs))
            {
                return true;
            }
            if (ReferenceEquals(lhs, null) || ReferenceEquals(rhs, null))
            {
                return false;
            }
            if (lhs.Length != rhs.Length)
            {
                return false;
            }
            for (int i = 0; i < lhs.Length; i++)
            {
                if (rhs[i] != lhs[i])
                {
                    return false;
                }
            }
            return true;
        }
        public static int GetHashCode(byte []bytes)
        {
            int ret = 23;
            foreach (byte b in bytes)
            {
                ret = (ret * 31) + b;
            }
            return ret;
        }

        /// <summary>
        /// Gets bytes that represents the current object.
        /// </summary>
        /// <returns></returns>
        public static byte[] GetBytes<T>(T tobj) where T : TAbstractBase
        {
            using (var trans = new Thrift.Transport.TMemoryBuffer())
            {
                using (var oprot = new Thrift.Protocol.TBinaryProtocol(trans))
                {
                    tobj.Write(oprot);
                    return trans.GetBuffer();
                }
            }
        }
        /// <summary>
        /// Creates a new instance from the byte array provided.
        /// </summary>
        /// <param name="bytes"></param>
        public static T CreateObject<T>(byte[] bytes) where T : TBase, new()
        {
            using (var trans = new Thrift.Transport.TMemoryBuffer(bytes))
            {
                using (var oprot = new Thrift.Protocol.TBinaryProtocol(trans))
                {
                    var tobj = new T();

                    tobj.Read(oprot);
                    return tobj;
                }
            }          
        }
    }
}
