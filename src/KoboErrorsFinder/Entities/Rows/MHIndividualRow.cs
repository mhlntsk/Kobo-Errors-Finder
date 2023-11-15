using KoboErrorFinder.Entities.Rows.Abstractions;

namespace KoboErrorFinder.Entities.Rows
{
    public class MHIndividualRow : BasicRow, IProviderCodeRow
    {
        public string ProviderCode { get; set; }
    }
}
