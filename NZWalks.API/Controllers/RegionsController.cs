using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Model.Domain;
using NZWalks.API.Model.DTO;
using NZWalks.API.Model.InPutDto;
using NZWalks.API.Repository;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RegionsController : ControllerBase
    {
        private NZWalksDbContext dbContext;
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;

        public RegionsController(NZWalksDbContext dbContext, IRegionRepository regionRepository, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.regionRepository = regionRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public  async Task<IActionResult> GetAll()
        {
            // Database to domain model
            //var regions = dbContext.Regions;

            var regions = await regionRepository.GetAllRegions();

            // domain model to Dto
            var regionDtos = new List<RegionDto>();

            //foreach (var region in regions)
            //{
            //    regionDtos.Add(new RegionDto()
            //    {
            //        Id = region.Id,
            //        Name = region.Name,
            //        Code = region.Code,
            //        RegionImageUrl = region.RegionImageUrl,
            //    });
            //}

            return Ok(mapper.Map<List<RegionDto>>(regions));
        }

        [HttpGet("Id")]
        public IActionResult Get(int id)
        {
            var region = dbContext.Regions.FirstOrDefault(x => x.Id == id);

            if (region == null) { return NotFound(); }

            var regionDto = new RegionDto()
            {
                Id = region.Id,
                Name = region.Name,
                Code = region.Code,
                RegionImageUrl = region.RegionImageUrl,
            };

            return Ok(region);
        }


        [HttpPost]
        public IActionResult Create([FromBody] RegionInputDto regionInputDto)
        {
            //Map input dto to domain model
            var region = new Region
            {
                Code = regionInputDto.Code,
                Name = regionInputDto.Name,
                RegionImageUrl = regionInputDto.RegionImageUrl,
            };

            // save changes to db

            dbContext.Regions.Add(region);
            dbContext.SaveChanges();

            // send back domain model to dto

            var regionDto = new RegionDto
            {
                Id = region.Id,
                Name = region.Name,
                Code = region.Code,
                RegionImageUrl = region.RegionImageUrl,
            };

            return CreatedAtAction(nameof(Get), new { id = regionDto.Id }, regionDto);
        }


        [Route("{id:int}")]
        [HttpPut]
        public IActionResult Update([FromRoute] int id, [FromBody] RegionInputDto regionInputDto)
        {
            // get domain model from db based on id

            var region = dbContext.Regions.FirstOrDefault(x => x.Id == id);

            if (region == null) { return NotFound(); }

            // Domain Model and save changes to Db

            region.Code = regionInputDto.Code;
            region.Name = regionInputDto.Name;
            region.RegionImageUrl = regionInputDto.RegionImageUrl;

            dbContext.SaveChanges();

            // map domain model to Dto

            var regionDto = new RegionDto { Id = region.Id, RegionImageUrl = region.RegionImageUrl,
                Code = region.Code,
                Name = region.Name, };

            return Ok(regionDto);
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id, [FromBody] RegionInputDto regionInputDto)
        {
            var region = await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            if (region == null) { return NotFound(); }

            // remove does't have async
            dbContext.Regions.Remove(region);
           await dbContext.SaveChangesAsync();

            var regionDto = new RegionDto { Id = region.Id, Name = region.Name, RegionImageUrl = region.RegionImageUrl, Code = region.Code, };

            return Ok(regionDto);
        }

    }
}
