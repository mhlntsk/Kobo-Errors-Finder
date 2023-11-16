using KoboErrorFinder.Entities.Errors;
using KoboErrorFinder.Entities.Rows;
using KoboErrorFinder.TablesExtensions.Executors;
using KoboErrorFinder.TablesExtensions.Mappers;
using KoboErrorFinder.TablesExtensions.Operators;
using KoboErrorFinder.TablesExtensions.Printers;
using NSubstitute;

namespace KoboErrorFinderTests.TablesHandlersTests.ExecutorsTests
{
    [TestFixture]
    public class MHGroupExecutorTests : BaseTest
    {
        IMapper<MHGroupMapper> _mapper;
        IOperator<MHGroupOperator> _operator;
        IPrinter<MHGroupPrinter> _printer;

        [SetUp]
        public void SetupBeforeEachTest()
        {
            _mapper = Substitute.For<IMapper<MHGroupMapper>>();
            _operator = Substitute.For<IOperator<MHGroupOperator>>();
            _printer = Substitute.For<IPrinter<MHGroupPrinter>>();
        }

        [Test]
        public void Execute_ValidData_ShouldCallMethods()
        {
            // Arrange
            var executor = new MHGroupExecutor(_mapper, _operator, _printer);

            Dictionary<string, int> headersOfSheet = new Dictionary<string, int>();

            // Act
            executor.Execute(sheet, headersOfSheet);

            // Assert

            Received.InOrder(() =>
            {
                _mapper.Map(sheet, headersOfSheet);
                _operator.Received().Check(Arg.Any<List<IMyRow>>());
                _printer.Print(Arg.Any<List<IError>>(), Arg.Any<List<IMyRow>>());
            });
        }
    }
}
