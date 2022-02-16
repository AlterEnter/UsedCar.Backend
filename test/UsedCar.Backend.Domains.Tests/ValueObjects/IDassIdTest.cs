﻿using System;
using UsedCar.Backend.Domains.Users.ValueObjects;
using Xunit;

namespace UsedCar.Backend.Domains.ValueObjects
{
    public class IDassIdTest
    {
        [Fact(DisplayName = "正常に値を取得すること。")]
        public void IDassId01()
        {
            string expectedIDassId = Guid.NewGuid().ToString();

            IDassId iDassId = new(expectedIDassId);

            Assert.Equal(expectedIDassId, iDassId.Value);
        }

        [Theory(DisplayName = "NullかEmptyの場合")]
        [InlineData("")]
        [InlineData(null)]
        public void IDassId02(string iDassId)
        {
            Assert.Throws<ArgumentNullException>(() => new IDassId(iDassId));
        }
    }
}
