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
            CheckParticipantsCountByStatus(rows, errors);

            CheckDate(rows, errors);

            return errors;
        }
        public void CheckParticipantsCountByAge(List<IMyRow> rows, List<IError> errors)
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
                            ParticipantsByAgeError = true
                        };
                    }
                    else
                    {
                        error.ParticipantsByAgeError = true;
                    }

                    errors.Add(error);
                }
            }
        }
        public void CheckParticipantsCountBySex(List<IMyRow> rows, List<IError> errors)
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
                            ParticipantsBySexError = true
                        };
                    }
                    else
                    {
                        error.ParticipantsBySexError = true;
                    }

                    errors.Add(error);
                }
            }
        }
        public void CheckParticipantsCountByStatus(List<IMyRow> rows, List<IError> errors)
        {
            List<MHGroupRow> MHGroupRows = rows.Cast<MHGroupRow>().ToList();

            foreach (var basicRow in MHGroupRows)
            {
                DateOnly dateWhenColumsWereAdded = DateOnly.Parse("15-11-2023");

                if (basicRow.Date > dateWhenColumsWereAdded)
                {
                    if (basicRow.TotalNumberOfParticipants != (basicRow.IDPCount + basicRow.ReturneeCount + basicRow.HostCount))
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
                                ParticipantsByPatientStatusError = true
                            };
                        }
                        else
                        {
                            error.ParticipantsByPatientStatusError = true;
                        }

                        errors.Add(error);
                    }
                }
            }
        }

        public override IError GetError()
        {
            return new MHGroupError();
        }
    }
}
