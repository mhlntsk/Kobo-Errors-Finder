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
    public class AmbulanceExecutorTests : BaseTest
    {
        IMapper<AmbulanceMapper> _mapper;
        IOperator<AmbulanceOperator> _operator;
        IPrinter<AmbulancePrinter> _printer;

        [SetUp]
        public void SetupBeforeEachTest()
        {
            _mapper = Substitute.For<IMapper<AmbulanceMapper>>();
            _operator = Substitute.For<IOperator<AmbulanceOperator>>();
            _printer = Substitute.For<IPrinter<AmbulancePrinter>>();
        }

        [Test]
        public void Execute_ValidData_ShouldCallMethods()
        {
            // Arrange
            var executor = new AmbulanceExecutor(_mapper, _operator, _printer);

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
