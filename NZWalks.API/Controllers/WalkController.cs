using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Model.Domain;
using NZWalks.API.Model.DTO;
using NZWalks.API.Model.InPutDto;
using NZWalks.API.Repository;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalkController : ControllerBase
    {
        public IWalkRepository WalkRepository { get; }
       // public IMapper mapper { get; }

        public WalkController(IWalkRepository walkRepository)
           // IMapper mapper)
        {
            this.WalkRepository = walkRepository;
          //  this.mapper = mapper;
        }

        //[HttpGet]

        //public async Task<IActionResult> GetAll()
        //{
        //    var walkModels = await WalkRepository.GetAll();

        //    if(walkModels == null || walkModels.Count == 0)
        //    { return NotFound(); }

        //    List<WalkDto> walkDtoList = new List<WalkDto>();

        //    foreach(var model in walkModels)
        //    {
        //        var walkDto = new WalkDto();
        //        walkDto.Id = model.Id;
        //        walkDto.Name = model.Name;
        //        walkDto.Description = model.Description;
        //        walkDto.Difficulty = model.Difficilty;
        //        walkDto.WalkImageUrl = model.WalkImageUrl;

        //        walkDtoList.Add(walkDto);
        //    }

        //    return Ok(walkDtoList);
        //}

        [HttpGet]

        public async Task<IActionResult> GetAllByFilter([FromQuery] string? filterOn, [FromQuery] string? filterQuery,
            [FromQuery] string? sortBy, [FromQuery] bool? isAccending,
            [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 1000)
        {
            var walkModels = await WalkRepository.GetAllByFiltering(filterOn, filterQuery, sortBy, isAccending ?? true, pageNumber, pageSize);

            if (walkModels == null || walkModels.Count == 0)
            { return NotFound(); }

            List<WalkDto> walkDtoList = new List<WalkDto>();

            foreach (var model in walkModels)
            {
                var walkDto = new WalkDto();
                walkDto.Id = model.Id;
                walkDto.Name = model.Name;
                walkDto.Description = model.Description;
                walkDto.Difficulty = model.Difficilty;
                walkDto.WalkImageUrl = model.WalkImageUrl;

                walkDtoList.Add(walkDto);
            }

            return Ok(walkDtoList);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] WalkInputDto walkInputDto)
        {
            var walkModel = new Walk()
            {
               // Id = walkInputDto.Id,
                Name = walkInputDto.Name,
                Description = walkInputDto.Description,
                LengthInKm = walkInputDto.LengthInKm,
                WalkImageUrl = walkInputDto.WalkImageUrl,
                Difficilty = walkInputDto.Difficilty,
                RegionId = walkInputDto.RegionId,
            };

            var walk = await WalkRepository.Create(walkModel);

            return Ok(walk);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] WalkInputDto walkInputDto)
        {
           

            var walk = await WalkRepository.Update(walkInputDto);

            var walkDto = new WalkDto()
            {
                 Id = walkInputDto.Id,
                Name = walkInputDto.Name,
                Description = walkInputDto.Description,
                LengthInKm = walkInputDto.LengthInKm,
                WalkImageUrl = walkInputDto.WalkImageUrl,
                Difficulty = walkInputDto.Difficilty,
                RegionId = walkInputDto.RegionId,
            };

            return Ok(walkDto);
        }
    }
}
