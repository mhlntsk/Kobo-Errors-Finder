using FluentAssertions;
using KoboErrorFinder.Entities.Errors;
using KoboErrorFinder.Entities.Rows;
using KoboErrorFinder.Entities;
using KoboErrorFinder.TablesExtensions.Operators;
using NSubstitute;
using KoboErrorFinder.Models;

namespace KoboErrorFinderTests.TablesExtensionsTests.OperatorsTests
{
    [TestFixture]
    public class AbstractAgeValueOperatorTests : BaseTest
    {
        // ToDo test for Check-method

        [Test]
        public void CheckMonthsCount_WithValidData_ErrorsShouldBeEmpty()
        {
            // Arrange
            var errors = new List<IError>();
            var rows = new List<IMyRow>();

            var row = Substitute.For<BasicRow>();
            row.AgeUnit = "Month";
            row.AgeValue = Convert.ToString(Random.Next(0, 12));
            rows.Add(row);

            var basicOperator = new BasicOperator();

            // Act
            basicOperator.CheckMonthsCount(rows, errors);

            // Assert
            errors.Should().BeEmpty();
        }

        [Test]
        public void CheckMonthsCount_WithInvalidData_ShouldAddErrorsToList()
        {
            // Arrange
            var errors = new List<IError>();
            var rows = new List<IMyRow>();

            var row = Substitute.For<BasicRow>();
            row.AgeUnit = "Months";
            row.AgeValue = Convert.ToString(Random.Next(12, int.MaxValue));
            rows.Add(row);

            var basicOperator = new BasicOperator();

            // Act
            basicOperator.CheckMonthsCount(rows, errors);

            // Assert
            errors.Any(error => ((BasicError)error).AgeMoreThan11MonthError).Should().BeTrue();
        }
    }
}
