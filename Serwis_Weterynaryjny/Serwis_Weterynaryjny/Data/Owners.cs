
namespace Weterynaria.Data
{
    public class Owners
    {
        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public List<Animals> Animals { get; set; } = new();
    }
}