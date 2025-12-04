
namespace Weterynaria.Data
{
    public class Animals
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Species { get; set; } = string.Empty;
        public string Breed { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public int OwnerId { get; set; }
        public Users Owner { get; set; } = null!;
        public List<Visits> Visits { get; set; } = new();
    }
}