using System;
using Netdx.Packets.Base;
using Xunit;

namespace PacketDecodersTests
{

    
    public class Dns
    {
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        public void Test1(int x)
        {
            Assert.InRange(x, 0, 10);
        }
    }
}
