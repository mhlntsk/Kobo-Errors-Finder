using FluentAssertions;
using KoboErrorFinder.Entities.Errors;
using KoboErrorFinder.Entities.Rows;
using KoboErrorFinder.Models;
using KoboErrorFinder.TablesExtensions.Operators;
using NSubstitute;

namespace KoboErrorFinderTests.TablesExtensionsTests.OperatorsTests
{
    [TestFixture]
    public class AmbulanceOperatorTests : BaseTest
    {
        // ToDo test for Check-method

        [Test]
        public void CheckRegionOfPatient_WithValidData_ErrorsShouldBeEmpty()
        {
            // Arrange
            var errors = new List<IError>();
            var rows = new List<IMyRow>();

            var row = Substitute.For<AmbulanceRow>();
            row.TypeOfRequester = "Health facility";
            row.RegionOfPatient = Faker.Person.Address.City;
            row.OtherPatientLocationSpecify = Faker.Person.Address.City;
            rows.Add(row);

            var ambulanceOperator = new AmbulanceOperator();

            // Act
            ambulanceOperator.CheckRegionOfPatient(rows, errors);

            // Assert
            errors.Should().BeEmpty();
        }

        [Test]
        public void CheckRegionOfPatient_WithInvalidData_ShouldAddErrorsToList()
        {
            // Arrange
            var errors = new List<IError>();
            var rows = new List<IMyRow>();

            var row = Substitute.For<AmbulanceRow>();
            row.TypeOfRequester = "Health facility";
            row.RegionOfPatient = "Other location";
            row.OtherPatientLocationSpecify = null;
            rows.Add(row);

            var ambulanceOperator = new AmbulanceOperator();

            // Act
            ambulanceOperator.CheckRegionOfPatient(rows, errors);

            // Assert
            errors.Any(error => ((AmbulanceError)error).OtherPatientLocationSpecifyIsEmpty).Should().BeTrue();


        }
    }
}
