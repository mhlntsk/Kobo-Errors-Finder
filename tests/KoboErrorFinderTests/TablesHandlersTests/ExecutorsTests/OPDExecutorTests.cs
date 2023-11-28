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
    public class OPDExecutorTests : BaseTest
    {
        IMapper<BasicMapper> _mapper;
        IOperator<BasicOperator> _operator;
        IPrinter<OpdPrinter> _printer;

        [SetUp]
        public void SetupBeforeEachTest()
        {
            _mapper = Substitute.For<IMapper<BasicMapper>>();
            _operator = Substitute.For<IOperator<BasicOperator>>();
            _printer = Substitute.For<IPrinter<OpdPrinter>>();
        }

        [Test]
        public void Execute_ValidData_ShouldCallMethods()
        {
            // Arrange
            var executor = new OpdExecutor(_mapper, _operator, _printer);

            Dictionary<string, int> headersOfSheet = new Dictionary<string, int>();

            // Act
            executor.Execute(sheet, headersOfSheet);

            // Assert

            Received.InOrder(() =>
            {
                _mapper.Received(1).Map(sheet, headersOfSheet);
                _operator.Received(1).Check(Arg.Any<List<IMyRow>>());
                _printer.Received(1).Print(Arg.Any<List<IError>>(), Arg.Any<List<IMyRow>>());
            });
        }
    }
}
