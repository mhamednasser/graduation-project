using graduationProject.Models;
using graduationProject.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using System.Threading.Tasks;
using System.Linq;

namespace graduationProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FoodDetectionController : ControllerBase
    {
        private readonly PythonApiService _pythonApiService;
        private readonly AppDbContext _dbContext;

        public FoodDetectionController(PythonApiService pythonApiService, AppDbContext dbContext)
        {
            _pythonApiService = pythonApiService;
            _dbContext = dbContext;
        }

        // POST: Upload image, call the Python API, and save nutritional data for a specific user
        [HttpPost("scan")]
        public async Task<IActionResult> ScanFood(IFormFile image, int userId)
        {
            // Validate the image file
            if (image == null || image.Length == 0)
            {
                return BadRequest("No image uploaded.");
            }

            if (!image.ContentType.StartsWith("image/"))
            {
                return BadRequest("Uploaded file is not an image.");
            }

            // Validate the user
            var user = await _dbContext.Users.FindAsync(userId);
            if (user == null)
            {
                return NotFound("User not found.");
            }

            try
            {
                // Call the Python API to get nutritional data as a raw JSON string
                var rawJson = await _pythonApiService.PredictNutritionalInfoAsync(image);

                // Deserialize the raw JSON string into a structured object (NutritionalInfoWrapper)
                var nutritionalInfoWrapper = JsonSerializer.Deserialize<NutritionalInfoWrapper>(rawJson);

                // Create a new NutritionalInfo object and store the raw JSON string
                var nutritionalInfo = new NutritionalInfo
                {
                    JsonData = rawJson,  // Save the raw JSON data (as a string)
                    UserId = userId      // Link to the user
                };

                // Save the data to the database
                _dbContext.NutritionalInfos.Add(nutritionalInfo);
                await _dbContext.SaveChangesAsync();

                // Return the deserialized data as JSON in the response
                return CreatedAtAction(nameof(GetNutritionalInfo), new { id = nutritionalInfo.Id }, new
                {
                    id = nutritionalInfo.Id,   // Include the id of the created resource
                    nutritional_info = nutritionalInfoWrapper.NutritionalInfo
                });

            


            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: Retrieve nutritional data by ID (scoped to the user)
        [HttpGet("{id}")]
        public async Task<IActionResult> GetNutritionalInfo(int id, int userId)
        {
            var nutritionalInfo = await _dbContext.NutritionalInfos
                .FirstOrDefaultAsync(n => n.Id == id && n.UserId == userId);

            if (nutritionalInfo == null)
            {
                return NotFound();
            }

            // Deserialize the raw JSON string stored in JsonData into a structured object
            var nutritionalInfoWrapper = JsonSerializer.Deserialize<NutritionalInfoWrapper>(nutritionalInfo.JsonData);

            // Return the deserialized data as JSON in the response
            return Ok(nutritionalInfoWrapper);
        }

        // GET: Retrieve all nutritional data for a specific user
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetNutritionalInfoByUser(int userId)
        {
            var nutritionalInfos = await _dbContext.NutritionalInfos
                .Where(n => n.UserId == userId)
                .ToListAsync();

            // Deserialize each raw JSON string and return as a list of NutritionalInfoWrapper
            var nutritionalInfoWrappers = nutritionalInfos.Select(n => JsonSerializer.Deserialize<NutritionalInfoWrapper>(n.JsonData)).ToList();

            return Ok(nutritionalInfoWrappers);
        }

        // DELETE: Remove nutritional data by ID (scoped to the user)
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNutritionalInfo(int id, int userId)
        {
            var nutritionalInfo = await _dbContext.NutritionalInfos
                .FirstOrDefaultAsync(n => n.Id == id && n.UserId == userId);

            if (nutritionalInfo == null)
            {
                return NotFound(new { message = "Nutritional information not found." });
            }

            _dbContext.NutritionalInfos.Remove(nutritionalInfo);
            await _dbContext.SaveChangesAsync();

            return Ok(new { message = "Nutritional information successfully deleted." });
        }
    }
}
