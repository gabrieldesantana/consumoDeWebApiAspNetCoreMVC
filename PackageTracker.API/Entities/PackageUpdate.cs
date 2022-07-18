namespace PackageTracker.API.Entities
{
    public class PackageUpdate
    {
    public PackageUpdate(string status, int id)
    {
      Status = status;
      PackageId = id;
      UpdateDate = DateTime.Now;
    }
        public int Id { get; private set; }
        public int PackageId { get; private set; }
        public string Status { get; private set; }
        public DateTime UpdateDate { get; private set; }
    }
}