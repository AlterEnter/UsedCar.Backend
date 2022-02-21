using System;
using Xunit;

namespace UsedCar.Backend.Domains.Users.ValueObjects
{
    public class IDassIdTest
    {
        [Fact(DisplayName = "正常に値を取得すること。")]
        public void IDassId01()
        {
            string expectedIDassId = Guid.NewGuid().ToString();

            IdaasId iIdaasId = new(expectedIDassId);

            Assert.Equal(expectedIDassId, iIdaasId.Value);
        }

        [Theory(DisplayName = "NullかEmptyの場合")]
        [InlineData("")]
        [InlineData(null)]
        public void IDassId02(string iDassId)
        {
            Assert.Throws<ArgumentNullException>(() => new IdaasId(iDassId));
        }
    }
}
