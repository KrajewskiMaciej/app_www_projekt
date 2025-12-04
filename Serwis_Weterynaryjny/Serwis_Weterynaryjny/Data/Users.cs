
namespace Weterynaria.Data
{
    public class Users
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty;
        public List<Animals>? Animals { get; set; } = new();
    }
}
