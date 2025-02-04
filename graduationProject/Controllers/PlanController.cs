using Microsoft.AspNetCore.Mvc;
using graduationProject.Interfaces;
using graduationProject.Models;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;


namespace graduationProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlanController : ControllerBase
    {
        private readonly IPlanGenerationService _planGenerationService;
        private readonly AppDbContext _context;

        public PlanController(IPlanGenerationService planGenerationService, AppDbContext context)
        {
            _planGenerationService = planGenerationService;
            _context = context;
        }

        [HttpPost("diet-plan")]
        public async Task<IActionResult> GetDietPlan([FromBody] GeneratePlanRequest request)
        {
            // Fetch the user from the database
            var user = await _context.Users.FindAsync(request.UserId);
            if (user == null)
            {
                return NotFound("User not found.");
            }

            // Generate the diet plan
            var structuredDietPlan = await _planGenerationService.GenerateDietPlanAsync(user);


            if (string.IsNullOrEmpty(structuredDietPlan.PlanJson))
            {
                return BadRequest("Failed to generate a valid diet plan.");
            }

            // Save the plan to the database
            //_context.StructuredDietPlans.Add(structuredDietPlan);
            //await _context.SaveChangesAsync();

            // Deserialize the PlanJson to return it in the response
            //var dietPlanData = JsonSerializer.Deserialize<DietPlanData>(structuredDietPlan.PlanJson);

            return Ok(structuredDietPlan.PlanData);
        }

        [HttpGet("diet-plan/{userId}")]
        public async Task<IActionResult> GetDietPlan(int userId)
        {
            var dietPlan = await _context.StructuredDietPlans.FirstOrDefaultAsync(p => p.UserId == userId);
            if (dietPlan == null)
            {
                return NotFound();
            }
            //  return Ok(dietPlan); koloooooooo rage3
            return Ok(dietPlan.PlanData);
        }



        // update data user >> regenerate diet >> keep workout 
        [HttpPut("update-diet-plan/{userId}")]
        public async Task<IActionResult> UpdateDietPlan(int userId, [FromBody] User updatedUser)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null)
            {
                return NotFound("User not found.");
            }

            // Update user data
           
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

            // Regenerate diet plan
            var dietPlan = await _planGenerationService.GenerateDietPlanAsync(user);

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

            await _context.SaveChangesAsync();

            return Ok(new { User = user, DietPlan = dietPlan });
        }



        [HttpDelete("delete-diet-plan/{userId}")]
        public async Task<IActionResult> DeleteDietPlanByUserId(int userId)
        {
            var dietPlan = await _context.StructuredDietPlans.FirstOrDefaultAsync(p => p.UserId == userId);
            if (dietPlan == null)
            {
                return NotFound("Diet plan not found.");
            }

            _context.StructuredDietPlans.Remove(dietPlan);
            await _context.SaveChangesAsync();

            return Ok("Diet plan deleted successfully.");
        }




        [HttpPost("workout-plan")]
        public async Task<IActionResult> GetWorkoutPlan([FromBody] GeneratePlanRequest request)
        {
            // Fetch the user from the database
            var user = await _context.Users.FindAsync(request.UserId);
            if (user == null)
            {
                return NotFound("User not found.");
            }

            // Generate the workout plan
            var structuredWorkoutPlan = await _planGenerationService.GenerateWorkoutPlanAsync(user);

            // Validate the AI response
            if (string.IsNullOrEmpty(structuredWorkoutPlan.PlanJson))
            {
                return BadRequest("Failed to generate a valid workout plan.");
            }

            // Save the plan to the database
            _context.StructuredWorkoutPlans.Add(structuredWorkoutPlan);
            await _context.SaveChangesAsync();

            // Deserialize the PlanJson to return it in the response
            var workoutPlanData = JsonSerializer.Deserialize<WorkoutPlanData>(structuredWorkoutPlan.PlanJson);

            return Ok(workoutPlanData);
        }


        [HttpGet("workout-plan/{userId}")]
        public async Task<IActionResult> GetWorkoutPlan(int userId)
        {
            var workoutPlan = await _context.StructuredWorkoutPlans.FirstOrDefaultAsync(p => p.UserId == userId);
            if (workoutPlan == null)
            {
                return NotFound();
            }
            //  return Ok(workoutPlan); koloooo rage3
            return Ok(workoutPlan.PlanData);
        }



        // update data user >> regenerate workout >> keep diet (ehhhh y denia 3yza meny ehhhhhhhhh)
        [HttpPut("update-workout-plan/{userId}")]
        public async Task<IActionResult> UpdateWorkoutPlan(int userId, [FromBody] User updatedUser)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null)
            {
                return NotFound("User not found.");
            }

            // Update user data
          
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

            // Regenerate workout plan
            var workoutPlan = await _planGenerationService.GenerateWorkoutPlanAsync(user);

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

            return Ok(new { User = user, WorkoutPlan = workoutPlan });
        }



        [HttpDelete("delete-workout-plan/{userId}")]
        public async Task<IActionResult> DeleteWorkoutPlanByUserId(int userId)
        {
            var workoutPlan = await _context.StructuredWorkoutPlans.FirstOrDefaultAsync(p => p.UserId == userId);
            if (workoutPlan == null)
            {
                return NotFound("Workout plan not found.");
            }

            _context.StructuredWorkoutPlans.Remove(workoutPlan);
            await _context.SaveChangesAsync();

            return Ok("Workout plan deleted successfully.");
        }
    }
}