using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace LoggingKata.Test
{
    [TestFixture]
    public class TacoParserTestFixture
    {
        [Test]
        public void ShouldParseLine()

        {   // Arrange

            var TacoParser = new TacoParser();
            var lessThan3 = "1234, 1234";
            var longNotNumber = "ac, 1234, "
            //TODO: Complete ShouldParseLine
        }
    }
}
