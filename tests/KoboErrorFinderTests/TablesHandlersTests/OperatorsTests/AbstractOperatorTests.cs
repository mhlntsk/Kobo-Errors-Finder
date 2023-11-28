using FluentAssertions;
using KoboErrorFinder.Entities;
using KoboErrorFinder.Entities.Errors;
using KoboErrorFinder.Entities.Rows;
using KoboErrorFinder.Models;
using KoboErrorFinder.TablesExtensions.Operators;
using NSubstitute;

namespace KoboErrorFinderTests.TablesHandlersTests.OperatorsTests
{
    [TestFixture]
    public class AbstractOperatorTests : BaseTest
    {
        [Test]
        public void CheckDate_WithValidData_ErrorsShouldBeEmpty()
        {
            // Arrange
            var errors = new List<IError>();
            var rows = new List<IMyRow>();

            var row = Substitute.For<BasicRow>();
            row.Date = Faker.Date.PastDateOnly();
            rows.Add(row);

            var basicOperator = new BasicOperator();

            // Act
            basicOperator.CheckDate(rows, errors);

            // Assert
            errors.Should().BeEmpty();
        }

        [Test]
        public void CheckDate_WithInvalidData_ShouldAddErrorsToList()
        {
            // Arrange
            var errors = new List<IError>();
            var rows = new List<IMyRow>();

            var row = Substitute.For<BasicRow>();
            row.Date = Faker.Date.FutureDateOnly();
            rows.Add(row);

            var basicOperator = new BasicOperator();

            // Act
            basicOperator.CheckDate(rows, errors);

            // Assert
            errors.Any(error => ((BasicError)error).DateError).Should().BeTrue();
        }
    }
}
