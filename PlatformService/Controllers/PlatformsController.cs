using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlatformService.Data;
using PlatformService.Dtos;
using PlatformService.Models;
using PlatformService.SyncDataServices.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace PlatformService.Controllers
{
    public class PlatformsController : ControllerBase
    {
		[Route("api/[controller]")]
		[ApiController]

		private readonly IPlatformRepo _repository;
        private readonly IMapper _mapper;
		private readonly ICommandDataClient _commandDataClient;

		public PlatformsController(
            IPlatformRepo repository, 
            IMapper mapper,
            ICommandDataClient commandDataClient)
        {
            _repository = repository;
            _mapper = mapper;
            _commandDataClient = commandDataClient;
        }

        [HttpGet]
        public ActionResult<IEnumerable<PlatformReadDto>> GetPlatforms() 
        {
            Console.WriteLine("--> Getting Platforms....");

            var platformItem = _repository.GetAllPlatforms();

            return Ok(_mapper.Map<IEnumerable<PlatformReadDto>>(platformItem));
        }

        [HttpGet("{id}", Name = "GetPlatformById")]
        public ActionResult<PlatformReadDto> GetPlatformById(int id)
        {
           var platformItem = _repository.GetPlatformById(id);
            if(platformItem !=  null) 
            {
                return Ok(_mapper.Map<PlatformReadDto>(platformItem));    
            }

            return NotFound();
        }

		public async Task<ActionResult<PlatformReadDto>> CreatePlatform(PlatformCreateDto platformCreateDto)
		{
			var platformModel = _mapper.Map<Platform>(platformCreateDto);
			_repository.CreatePlatform(platformModel);
			_repository.SaveChanges();

			var platformReadDto = _mapper.Map<PlatformReadDto>(platformModel);

			try
			{
				await _commandDataClient.SendPlatformToCommand(platformReadDto);
			}

			catch (Exception ex)
			{
				Console.WriteLine($"--> Could not send synchronously: {ex.Message}");
			}

			return CreatedAtRoute(nameof(GetPlatformById), new { Id = platformReadDto.Id }, platformReadDto);
        }
	}
}
