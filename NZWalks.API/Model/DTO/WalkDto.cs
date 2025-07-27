using NZWalks.API.Model.Domain;

namespace NZWalks.API.Model.DTO
{
    public class WalkDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double LengthInKm { get; set; }
        public string? WalkImageUrl { get; set; }
        public Difficulty Difficulty { get; set; }
        public int RegionId { get; set; }
       // public RegionDto Region { get; set; }
    }
}
