using NZWalks.API.Model.Domain;
using NZWalks.API.Model.DTO;
using NZWalks.API.Model.InPutDto;

namespace NZWalks.API.Repository
{
    public interface IWalkRepository
    {
        Task<List<Walk>> GetAll();
        Task<List<Walk>> GetAllByFiltering(string? filterOn = null, string? filterQuery = null, string? sortBy = null, bool isAccending = true, int pageNumber = 1, int pageSize = 1000);

        Task<Walk> Create(Walk walk);
        Task<Walk> Update(WalkInputDto walkDto);
    }
}
