using KoboErrorFinder.Entities.Errors;
using KoboErrorFinder.Entities.Errors.Abstractions;
using KoboErrorFinder.Entities.Rows;
using KoboErrorFinder;
using KoboErrorFinder.Entities;
using KoboErrorFinder.Models;
using Microsoft.Extensions.FileSystemGlobbing.Internal;
using System.Text.RegularExpressions;

namespace KoboErrorFinder.TablesExtensions.Operators
{
    public class MHGroupOperator : AbstractOperator, IOperator<MHGroupOperator>
    {
        public List<IError> errors { get; set; } = new List<IError>();
        public List<IError> Check(List<IMyRow> rows)
        {
            CheckParticipantsCountBySex(rows, errors);
            CheckParticipantsCountByAge(rows, errors);

            CheckDate(rows, errors);

            return errors;
        }
        public void CheckParticipantsCountBySex(List<IMyRow> rows, List<IError> errors)
        {
            List<MHGroupRow> MHGroupRows = rows.Cast<MHGroupRow>().ToList();

            foreach (var basicRow in MHGroupRows)
            {
                if (basicRow.TotalNumberOfParticipants != (basicRow.Y0_4 + 
                                                           basicRow.Y5_9 +
                                                           basicRow.Y10_14 +
                                                           basicRow.Y15_19 +
                                                           basicRow.Y20_44 +
                                                           basicRow.Y45_64 +
                                                           basicRow.Y65_Plus))
                {
                    MHGroupError error = null;

                    if (errors != null && errors.Count != 0)
                    {
                        error = (MHGroupError)errors.FirstOrDefault(e => e.UniqueEntityId == basicRow.UniqueEntityId);
                    }

                    if (error == null)
                    {
                        error = new MHGroupError()
                        {
                            UniqueEntityId = basicRow.UniqueEntityId,
                            ParticipantByAgeError = true
                        };
                    }
                    else
                    {
                        error.ParticipantByAgeError = true;
                    }

                    errors.Add(error);
                }
            }
        }
        public void CheckParticipantsCountByAge(List<IMyRow> rows, List<IError> errors)
        {
            List<MHGroupRow> MHGroupRows = rows.Cast<MHGroupRow>().ToList();

            foreach (var basicRow in MHGroupRows)
            {
                if (basicRow.TotalNumberOfParticipants != (basicRow.Male + basicRow.Female))
                {
                    MHGroupError error = null;

                    if (errors != null && errors.Count != 0)
                    {
                        error = (MHGroupError)errors.FirstOrDefault(e => e.UniqueEntityId == basicRow.UniqueEntityId);
                    }

                    if (error == null)
                    {
                        error = new MHGroupError()
                        {
                            UniqueEntityId = basicRow.UniqueEntityId,
                            ParticipantBySexError = true
                        };
                    }
                    else
                    {
                        error.ParticipantBySexError = true;
                    }

                    errors.Add(error);
                }
            }
        }
        public override IError GetError()
        {
            return new MHGroupError();
        }
    }
}
