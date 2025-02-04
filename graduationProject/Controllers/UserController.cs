using graduationProject.Interfaces;
using graduationProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace graduationProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IPlanGenerationService _planGenerationService;

        public UserController(AppDbContext context, IPlanGenerationService planGenerationService) // Add the parameter
        {
            _context = context;
            _planGenerationService = planGenerationService; // Initialize the service
        }

        // POST: api/user - Creates a new user in the database
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            // Generate and save the diet plan
            var dietPlan = await _planGenerationService.GenerateDietPlanAsync(user);
            dietPlan.UserId = user.Id; // Link the diet plan to the user
            _context.StructuredDietPlans.Add(dietPlan);

            // Generate and save the workout plan
            var workoutPlan = await _planGenerationService.GenerateWorkoutPlanAsync(user);
            workoutPlan.UserId = user.Id; // Link the workout plan to the user
            _context.StructuredWorkoutPlans.Add(workoutPlan);

            // Save the plans to the database
            await _context.SaveChangesAsync();

            return Ok(new { User = user, DietPlan = dietPlan, WorkoutPlan = workoutPlan });
        }

        // GET: api/user/{id} - Retrieves a user by their ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] User updatedUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound("User not found.");
            }

            // Update all user data
  
            user.Age = updatedUser.Age;
            user.Height = updatedUser.Height;
            user.Weight = updatedUser.Weight;
            user.FitnessLevel = updatedUser.FitnessLevel;
            user.WeeklyWorkoutDays = updatedUser.WeeklyWorkoutDays;
            user.WorkoutDuration = updatedUser.WorkoutDuration;
            user.Goal = updatedUser.Goal;
            user.DietaryRestrictions = updatedUser.DietaryRestrictions;
            user.PreferredDiet = updatedUser.PreferredDiet;
            user.MedicalConditions = updatedUser.MedicalConditions;

            // Regenerate both diet and workout plans
            var dietPlan = await _planGenerationService.GenerateDietPlanAsync(user);
            var workoutPlan = await _planGenerationService.GenerateWorkoutPlanAsync(user);

            // Update or add diet plan
            var existingDietPlan = await _context.StructuredDietPlans.FirstOrDefaultAsync(p => p.UserId == user.Id);
            if (existingDietPlan != null)
            {
                existingDietPlan.PlanJson = dietPlan.PlanJson;
                _context.StructuredDietPlans.Update(existingDietPlan);
            }
            else
            {
                dietPlan.UserId = user.Id;
                _context.StructuredDietPlans.Add(dietPlan);
            }

            // Update or add workout plan
            var existingWorkoutPlan = await _context.StructuredWorkoutPlans.FirstOrDefaultAsync(p => p.UserId == user.Id);
            if (existingWorkoutPlan != null)
            {
                existingWorkoutPlan.PlanJson = workoutPlan.PlanJson;
                _context.StructuredWorkoutPlans.Update(existingWorkoutPlan);
            }
            else
            {
                workoutPlan.UserId = user.Id;
                _context.StructuredWorkoutPlans.Add(workoutPlan);
            }

            await _context.SaveChangesAsync();

            return Ok(new { User = user, DietPlan = dietPlan, WorkoutPlan = workoutPlan });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound("User not found.");
            }

            // Delete associated diet plans
            var dietPlans = _context.StructuredDietPlans.Where(p => p.UserId == id);
            _context.StructuredDietPlans.RemoveRange(dietPlans);

            // Delete associated workout plans
            var workoutPlans = _context.StructuredWorkoutPlans.Where(p => p.UserId == id);
            _context.StructuredWorkoutPlans.RemoveRange(workoutPlans);

            // Delete the user
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return Ok("User and associated plans deleted successfully.");
        }
    }
}
