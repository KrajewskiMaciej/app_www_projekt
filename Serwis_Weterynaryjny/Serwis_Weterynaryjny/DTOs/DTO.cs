
namespace Weterynaria.DTO
{
    public class UserDto
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }

    public class CreateVisitDto
    {
        public string OwnerName { get; set; } = string.Empty;
        public string OwnerPhone { get; set; } = string.Empty;
        public string AnimalName { get; set; } = string.Empty;
        public string AnimalSpecies { get; set; } = string.Empty;
        public int ServicesTypesId { get; set; }
        public DateTime VisitDate { get; set; }
        public string Description { get; set; } = string.Empty;
    }

    public class VisitUpdateDto
    {
        public int Id { get; set; }
        public DateTime VisitDate { get; set; }
        public string Description { get; set; } = string.Empty;
        public double Price { get; set; }
    }

    public class AnimalDto
    {
        public string Name { get; set; } = string.Empty;
        public string Species { get; set; } = string.Empty;
        public string Breed { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public int? OwnerId { get; set; }
    }

    public class MonthlySummaryDto
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public int TotalVisits { get; set; }
        public double TotalRevenue { get; set; }
    }

}