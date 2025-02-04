using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using graduationProject.Models; 
using Microsoft.EntityFrameworkCore;

namespace graduationProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExercisesController : ControllerBase
    {
        private readonly AppDbContext _dbContext;

        public ExercisesController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet("{muscleGroupId}")]
        public IActionResult GetExercisesByMuscleGroup(int muscleGroupId)
        {
            var exercises = _dbContext.nExercises
                .Where(e => e.MuscleGroupId == muscleGroupId)
                .Select(e => new
                {
                    e.Id,
                    e.Name,
                    e.Sets,
                    e.Icon,
                    e.VideoUrl
                }).ToList();

            return Ok(exercises);
        }
    }
}