namespace Weterynaria.Data
{
    public class Visits
    {
        public int Id { get; set; }
        public DateTime VisitDate { get; set; }
        public string Description { get; set; } = string.Empty;
        public double Price { get; set; }
        public int AnimalId { get; set; }
        public Animals Animal { get; set; } = null!;
    }
}