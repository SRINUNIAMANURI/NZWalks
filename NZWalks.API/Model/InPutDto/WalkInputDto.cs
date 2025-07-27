using NZWalks.API.Model.Domain;

namespace NZWalks.API.Model.InPutDto
{
    public class WalkInputDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double LengthInKm { get; set; }
        public string? WalkImageUrl { get; set; }
        public Difficulty Difficilty { get; set; }
        public int RegionId { get; set; }
    }
}
