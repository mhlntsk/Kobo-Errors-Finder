using KoboErrorFinder.Entities.Errors;
using KoboErrorFinder.Entities.Errors.Abstractions;
using KoboErrorFinder.Entities.Rows;
using KoboErrorFinder.Models;

namespace KoboErrorFinder.TablesExtensions.Printers
{
    public class MHGroupPrinter : AbstractPrinter, IPrinter<MHGroupPrinter>
    {
        public override void MakeSpecificPrinting(List<IError> errors, List<IMyRow> rows)
        {
            var castedErrors = errors.Cast<MHGroupError>().DistinctBy(obj => obj.UniqueEntityId).ToList();
            var castedRows = rows.Cast<MHGroupRow>().ToList();

            var joinedErrorsAndRows = from error in castedErrors
                                      join row in castedRows on error.UniqueEntityId equals row.UniqueEntityId
                                      select new { Error = error, Row = row };

            int counter = 1;

            foreach (var joinedErrorsAndRow in joinedErrorsAndRows)
            {
                Console.WriteLine($"\n{counter}) {joinedErrorsAndRow.Row.Date}");

                if (joinedErrorsAndRow.Error.ParticipantsBySexError == true)
                {
                    Console.WriteLine("\tTotal participants count is not match with sum by sex!");
                }

                if (joinedErrorsAndRow.Error.ParticipantsByAgeError == true)
                {
                    Console.WriteLine("\tTotal Participants count is not match with sum by age!");
                }

                if (joinedErrorsAndRow.Error.ParticipantsByPatientStatusError == true)
                {
                    Console.WriteLine("\tTotal Participants count is not match with sum by patient status!");
                }

                if (joinedErrorsAndRow.Error.DateError == true)
                {
                    Console.WriteLine("\tThe date is not specified!");
                }

                int totalBySex = joinedErrorsAndRow.Row.Male + joinedErrorsAndRow.Row.Female;

                int totalByPatientStatus = joinedErrorsAndRow.Row.IDPCount + 
                                           joinedErrorsAndRow.Row.HostCount + 
                                           joinedErrorsAndRow.Row.ReturneeCount;

                int totalByAge = joinedErrorsAndRow.Row.Y0_4 +
                                 joinedErrorsAndRow.Row.Y5_9 +
                                 joinedErrorsAndRow.Row.Y10_14 +
                                 joinedErrorsAndRow.Row.Y15_19 +
                                 joinedErrorsAndRow.Row.Y20_44 +
                                 joinedErrorsAndRow.Row.Y45_64 +
                                 joinedErrorsAndRow.Row.Y65_Plus;

                Console.WriteLine($"\t\t {joinedErrorsAndRow.Row.ProviderCode} | total: {joinedErrorsAndRow.Row.TotalNumberOfParticipants} | totalBySex: {totalBySex} | totalByAge: {totalByAge} | totalByPatientStatus: {totalByPatientStatus}");

                counter++;
            }
        }
    }
}
