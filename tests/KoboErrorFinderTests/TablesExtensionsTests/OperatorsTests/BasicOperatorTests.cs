using FluentAssertions;
using KoboErrorFinder.Entities.Errors;
using KoboErrorFinder.Entities.Rows;
using KoboErrorFinder.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using KoboErrorFinder.TablesExtensions.Operators;
using NSubstitute;
using KoboErrorFinder.Models;
using KoboErrorFinderTests.FakeData;

namespace KoboErrorFinderTests.TablesExtensionsTests.OperatorsTests
{
    public class BasicOperatorTests
    {
        [Test]
        public void CheckIds_WithValidData_ErrorsShouldBeEmpty()
        {
            // Arrange
            var errors = new List<IError>();
            var rows = new List<IMyRow>();

            var faker = new FakeIDsGenerator();

            var row = Substitute.For<BasicRow>();
            row.MSFPatientID = faker.GetValidIds();
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

            var faker = new FakeIDsGenerator();

            var row = Substitute.For<BasicRow>();
            row.MSFPatientID = faker.GetInvalidIds();
            rows.Add(row);

            var basicOperator = new BasicOperator();

            // Act
            basicOperator.CheckIds(rows, errors);

            // Assert
            errors.All(error => ((BasicError)error).IdError).Should().BeTrue();
        }
    }
}
