namespace KoboErrorFinder.Entities.Rows
{
    public interface IMyRow
    {
        public DateOnly Date { get; set; }
        public string UniqueEntityId { get; }
    }
}
