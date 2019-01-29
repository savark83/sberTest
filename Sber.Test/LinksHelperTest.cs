using System;
using Xunit;

namespace Sber.Test
{
    using Sber.App.Helpers;

    public class LinksHelperTest
    {
        [Fact]
        public void LinksHelper_GetShortValue()
        {
            var result = LinksHelper.GetShortValue(100);
            Assert.Equal("Be", result);
        }
    }
}
