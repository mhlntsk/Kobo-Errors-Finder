using KoboErrorFinderTests;
using NPOI.SS.UserModel;
using NSubstitute;
using Org.BouncyCastle.Crypto.Prng;

namespace KoboErrorFinderTests.ModulsTests.MappersTests
{
    [TestFixture]
    public class AbstractMapperTest : BaseTest
    {
        protected IRow Row { get; private set; }
        protected ICell Cell { get; private set; }

        [SetUp]
        public void Setup()
        {
            Cell = Substitute.For<ICell>();
            Cell.ToString().Returns("SubstitutedCellValue");
            Row = Substitute.For<IRow>();
            Row.GetCell(Arg.Any<int>()).Returns(Cell);


        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }
    }
}
