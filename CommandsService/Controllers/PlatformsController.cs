using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using PlatformService.Models;
using PlatformService.Data;

namespace CommandsService.Controllers
{
	[Route("api/c/[controller]")]
	[ApiController]
	public class PlatformsController : ControllerBase
	{
        private 

        public PlatformsController(IPlatformRepo repository, IMapper mapper,)
        {
                
        }

        [HttpPost]
        public ActionResult TestInboundConnection() 
        {
            Console.WriteLine("--> Inbound POST # Command Service");

            return Ok("Inbound test of from Platforms Controller");
        }
    }
}
