using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace graduationProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProfileController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/profile/user/{userId} - Retrieve full profile with user data, workout, and diet plans
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetUserProfile(int userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null)
                return NotFound("User not found.");

            // Fetch generated plans for the user
            var workoutPlan = await _context.GeneratedWorkoutPlans
                .FirstOrDefaultAsync(w => w.UserId == userId);
            var dietPlan = await _context.GeneratedDietPlans
                .FirstOrDefaultAsync(d => d.UserId == userId);

            var profile = new
            {
                User = user,
                WorkoutPlan = workoutPlan?.PlanDetails,
                DietPlan = dietPlan?.MealPlan
            };

            return Ok(profile);
        }
    }
}
