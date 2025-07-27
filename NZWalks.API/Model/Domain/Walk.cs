namespace NZWalks.API.Model.Domain
{
    public class Walk
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double LengthInKm { get; set; }
        public string? WalkImageUrl { get; set; }
        public Difficulty Difficilty { get; set; }
        public int RegionId { get; set; }

        // navigation properties

        public Region Region { get; set; }
    }
}
