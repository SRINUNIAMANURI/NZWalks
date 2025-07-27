using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Model.Domain;
using NZWalks.API.Model.InPutDto;

namespace NZWalks.API.Repository
{
    public class WalkRepository : IWalkRepository
    {
        public NZWalksDbContext dbContext { get; }

        public WalkRepository(NZWalksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }


        public async Task<List<Walk>> GetAll()
        {
             return await dbContext.Walks.ToListAsync();
        }

        public async Task<List<Walk>> GetAllByFiltering(string? filterOn = null, string? filterQuery = null, string? sortBy = null, bool isAccending = true,
            int pageNumber = 1, int pageSize = 1000)
        {
            var walks =  dbContext.Walks.AsQueryable();
            //filtering
            if(string.IsNullOrWhiteSpace(filterOn) == false && string.IsNullOrWhiteSpace(filterQuery) == false)
            {
                if(filterOn.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                        walks = walks.Where(x => x.Name.Contains(filterQuery));
                }
            }
            //sorting
            if(string.IsNullOrWhiteSpace(sortBy)== false)
            {
                if(sortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    walks = isAccending ? walks.OrderBy(x => x.Name) : walks.OrderByDescending(x => x.Name);
                }
            }
            //Pagination

            var skipResult = (pageNumber - 1) * pageSize;


            return await walks.Skip(skipResult).Take(pageSize).ToListAsync();
        }

        public async Task<Walk> Create (Walk walk)
        {
           await dbContext.Walks.AddAsync(walk);
            await dbContext.SaveChangesAsync();

            return walk;
        }

        public async Task<Walk> Update(WalkInputDto walkDto)
        {
            var walkModel = await dbContext.Walks.FirstOrDefaultAsync(x => x.Id == walkDto.Id);

            walkModel.Description = walkDto.Description;
           // walkModel.Id = walk.Id;
           walkModel.Name = walkDto.Name;
            walkModel.Description = walkDto.Description;
            walkModel.Difficilty = walkDto.Difficilty;
            walkModel.RegionId = walkDto.RegionId;
            walkModel.LengthInKm = walkDto.LengthInKm;
            walkModel.WalkImageUrl = walkDto.WalkImageUrl;

            await dbContext.SaveChangesAsync();

            return walkModel;
        }
    }
}
