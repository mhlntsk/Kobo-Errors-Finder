using FluentAssertions;
using KoboErrorFinder.Entities.Errors;
using KoboErrorFinder.Entities.Errors.Abstractions;
using KoboErrorFinder.Entities.Rows;
using KoboErrorFinder.TablesExtensions.Operators;

namespace KoboErrorFinderTests.TablesExtensionsTests.OperatorsTests
{
    [TestFixture]
    public class MHGroupOperatorTests : BaseTest
    {
        [Test]
        public void CheckParticipantsCountBySex_WithValidData_ErrorsShouldBeEmpty()
        {
            // Arrange
            var operatorUnderTest = new MHGroupOperator();
            var rows = new List<IMyRow>
            {
                new MHGroupRow { Male = 50, Female = 50, TotalNumberOfParticipants = 100 }
            };
            var errors = new List<IError>();

            // Act
            operatorUnderTest.CheckParticipantsCountBySex(rows, errors);

            // Assert
            errors.Should().BeEmpty();
        }

        [Test]
        public void CheckParticipantsCountBySex_WithInvalidData_ShouldAddErrorsToList()
        {
            // Arrange
            var operatorUnderTest = new MHGroupOperator();

            int randomNum = Random.Next(10, int.MaxValue);

            var rows = new List<IMyRow>
            {
                new MHGroupRow { Male = 2, Female = 5, TotalNumberOfParticipants = randomNum }
            };
            var errors = new List<IError>();

            // Act
            operatorUnderTest.CheckParticipantsCountBySex(rows, errors);

            // Assert
            Assert.Multiple(() => {
                errors.Should().HaveCount(1);
                errors.Should().ContainSingle(error => error is MHGroupError && ((MHGroupError)error).ParticipantsBySexError);
            });
        }



        [Test]
        public void CheckParticipantsCountByAge_WithValidData_ErrorsShouldBeEmpty()
        {
            // Arrange
            var operatorUnderTest = new MHGroupOperator();
            var rows = new List<IMyRow>
            {
                new MHGroupRow { Y0_4 = 20, Y5_9 = 30, Y10_14 = 10, Y15_19 = 15, Y20_44 = 10, Y45_64 = 10, Y65_Plus = 5, TotalNumberOfParticipants = 100 }
            };
            var errors = new List<IError>();

            // Act
            operatorUnderTest.CheckParticipantsCountByAge(rows, errors);

            // Assert
            errors.Should().BeEmpty();
        }

        [Test]
        public void CheckParticipantsCountByAge_WithInvalidData_ShouldAddErrorsToList()
        {
            // Arrange
            var operatorUnderTest = new MHGroupOperator();
            var rows = new List<IMyRow>
    {
        new MHGroupRow { Y0_4 = 20, Y5_9 = 30, Y10_14 = 10, Y15_19 = 15, Y20_44 = 10, Y45_64 = 10, Y65_Plus = 0, TotalNumberOfParticipants = 100 }
    };
            var errors = new List<IError>();

            // Act
            operatorUnderTest.CheckParticipantsCountByAge(rows, errors);

            // Assert
            errors.Should().HaveCount(1);
            errors.Should().ContainSingle(e => e is MHGroupError && ((MHGroupError)e).ParticipantsByAgeError);
        }

    }
}
