using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using graduationProject.Models; 
using Microsoft.EntityFrameworkCore;

namespace graduationProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MuscleGroupsController : ControllerBase
    {
        private readonly AppDbContext _dbContext;

        public MuscleGroupsController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult GetAllMuscleGroups()
        {
            var muscleGroups = _dbContext.MuscleGroups
                .Select(m => new
                {
                    m.Id,
                    m.Name
                }).ToList();

            return Ok(muscleGroups);
        }
    }
}