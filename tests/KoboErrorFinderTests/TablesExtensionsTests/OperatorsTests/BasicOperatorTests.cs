using FluentAssertions;
using KoboErrorFinder.Entities;
using KoboErrorFinder.Entities.Errors;
using KoboErrorFinder.Entities.Rows;
using KoboErrorFinder.Models;
using KoboErrorFinder.TablesExtensions.Operators;
using KoboErrorFinderTests.FakeData;
using NSubstitute;

namespace KoboErrorFinderTests.TablesExtensionsTests.OperatorsTests
{
    public class BasicOperatorTests :  BaseTest
    {
        // ToDo test for Check-method

        [Test]
        public void CheckIds_WithValidData_ErrorsShouldBeEmpty()
        {
            // Arrange
            var errors = new List<IError>();
            var rows = new List<IMyRow>();

            var row = Substitute.For<BasicRow>();
            row.MSFPatientID = FakeDataGenerator.GetValidIds();
            rows.Add(row);

            var basicOperator = new BasicOperator();

            // Act
            basicOperator.CheckIds(rows, errors);

            // Assert
            errors.Should().BeEmpty();
        }

        [Test]
        public void CheckIds_WithInvalidData_ShouldAddErrorsToList()
        {
            // Arrange
            var errors = new List<IError>();
            var rows = new List<IMyRow>();

            var row = Substitute.For<BasicRow>();
            row.MSFPatientID = FakeDataGenerator.GetInvalidIds();
            rows.Add(row);

            var basicOperator = new BasicOperator();

            // Act
            basicOperator.CheckIds(rows, errors);

            // Assert
            errors.All(error => ((BasicError)error).IdError).Should().BeTrue();
        }

        [Test]
        public void CheckIds_WithNullData_ShouldThrowException()
        {
            // Arrange
            var errors = new List<IError>();
            var rows = new List<IMyRow>();

            var row = Substitute.For<BasicRow>();
            row.MSFPatientID = null;
            rows.Add(row);

            var basicOperator = new BasicOperator();

            // Act and Assert
            Action action = () => basicOperator.CheckIds(rows, errors);
            action.Should().Throw<ArgumentNullException>();
        }




        [Test]
        public void CheckAge_WithValidData_ErrorsShouldBeEmpty()
        {
            // Arrange
            var errors = new List<IError>();
            var rows = new List<IMyRow>();

            var pairOfValues = FakeDataGenerator.GetValidAgePair();

            var row1 = Substitute.For<BasicRow>();
            row1.AgeValue = pairOfValues.key;
            rows.Add(row1);

            var row2 = Substitute.For<BasicRow>();
            row2.AgeValue = pairOfValues.value;
            rows.Add(row2);

            var basicOperator = new BasicOperator();

            // Act
            basicOperator.CheckAge(rows, errors);

            // Assert
            errors.Should().BeEmpty();
        }

        [Test]
        public void CheckAge_WithInvalidData_ShouldAddErrorsToList()
        {
            // Arrange
            var errors = new List<IError>();
            var rows = new List<IMyRow>();

            var pairOfValues = FakeDataGenerator.GetInvalidAgePair();

            var row1 = Substitute.For<BasicRow>();
            row1.AgeValue = pairOfValues.key;
            rows.Add(row1);

            var row2 = Substitute.For<BasicRow>();
            row2.AgeValue = pairOfValues.value;
            rows.Add(row2);

            var basicOperator = new BasicOperator();

            // Act
            basicOperator.CheckAge(rows, errors);

            // Assert
            errors.All(error => ((BasicError)error).AgeError).Should().BeTrue();
        }

        [Test]
        public void CheckAge_WithEmptyData_ShouldThrowException()
        {
            // Arrange
            var errors = new List<IError>();
            var rows = new List<IMyRow>();

            var pairOfValues = FakeDataGenerator.GetEmptyAgePair();

            var row1 = Substitute.For<BasicRow>();
            row1.AgeValue = pairOfValues.key;
            rows.Add(row1);

            var row2 = Substitute.For<BasicRow>();
            row2.AgeValue = pairOfValues.value;
            rows.Add(row2);

            var basicOperator = new BasicOperator();

            // Act and Assert
            Action action = () => basicOperator.CheckAge(rows, errors);
            action.Should().Throw<FormatException>();

        }



        [Test]
        public void CheckSex_WithValidData_ErrorsShouldBeEmpty()
        {
            // Arrange
            var errors = new List<IError>();
            var rows = new List<IMyRow>();

            var pairOfValues = FakeDataGenerator.GetValidSexSequence();

            var row1 = Substitute.For<BasicRow>();
            row1.AgeValue = pairOfValues.key;
            rows.Add(row1);

            var row2 = Substitute.For<BasicRow>();
            row2.AgeValue = pairOfValues.value1;
            rows.Add(row2);

            var row3 = Substitute.For<BasicRow>();
            row3.AgeValue = pairOfValues.value2;
            rows.Add(row3);

            var basicOperator = new BasicOperator();

            // Act
            basicOperator.CheckSex(rows, errors);

            // Assert
            errors.Should().BeEmpty();
        }

        [Test]
        public void CheckSex_WithInvalidData_ShouldAddErrorsToList()
        {
            // Arrange
            var errors = new List<IError>();
            var rows = new List<IMyRow>();

            var pairOfValues = FakeDataGenerator.GetInvalidSexSequence();

            var row1 = Substitute.For<BasicRow>();
            row1.AgeValue = pairOfValues.key;
            rows.Add(row1);

            var row2 = Substitute.For<BasicRow>();
            row2.AgeValue = pairOfValues.value1;
            rows.Add(row2);

            var row3 = Substitute.For<BasicRow>();
            row3.AgeValue = pairOfValues.value2;
            rows.Add(row3);

            var basicOperator = new BasicOperator();

            // Act
            basicOperator.CheckSex(rows, errors);

            // Assert
            errors.All(error => ((BasicError)error).SexError).Should().BeTrue();
        }
    }
}
