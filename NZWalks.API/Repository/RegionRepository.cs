using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Model.Domain;

namespace NZWalks.API.Repository
{
    public class RegionRepository : IRegionRepository
    {
        public RegionRepository(NZWalksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public NZWalksDbContext dbContext { get; }

        public async Task<List<Region>> GetAllRegions()
        {
            return await dbContext.Regions.ToListAsync();
        }
    }
}
